using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP.Sample.CommonService.OpenTicket;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Containers;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{

    public class OpenOAuthController : BaseController
    {
        private string component_AppId = Config.SenparcWeixinSetting.Component_Appid;
        private string component_Secret = Config.SenparcWeixinSetting.Component_Secret;
        //private static string ComponentAccessToken = null;//需要授权获取，腾讯服务器会主动推送

        #region 开放平台入口及回调

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="auth_code"></param>
        /// <param name="expires_in"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult OpenOAuthCallback(string auth_code, int expires_in, string appId)
        {
            try
            {

                #region 直接调用API

                //string openTicket = OpenTicketHelper.GetOpenTicket(component_AppId);
                //var component_access_token = Open.ComponentAPIs.ComponentApi.GetComponentAccessToken(component_AppId, component_Secret, openTicket).component_access_token;
                //ComponentAccessToken = component_access_token;
                //var oauthResult = Open.ComponentAPIs.ComponentApi.QueryAuth(component_access_token, component_AppId, auth_code);

                ////TODO:储存oauthResult.authorization_info
                //var authInfoResult = Open.ComponentAPIs.ComponentApi.GetAuthorizerInfo(component_access_token, component_AppId,
                //     oauthResult.authorization_info.authorizer_appid);

                #endregion

                #region 使用ComponentContainer

                //获取OAuth授权结果
                QueryAuthResult queryAuthResult;
                try
                {
                    queryAuthResult = ComponentContainer.GetQueryAuthResult(component_AppId, auth_code);
                }
                catch (Exception ex)
                {
                    throw new Exception("QueryAuthResult：" + ex.Message);
                }
                #endregion

                var authorizerInfoResult = AuthorizerContainer.GetAuthorizerInfoResult(component_AppId,
                    queryAuthResult.authorization_info.authorizer_appid);

                ViewData["QueryAuthorizationInfo"] = queryAuthResult.authorization_info;
                ViewData["GetAuthorizerInfoResult"] = authorizerInfoResult.authorizer_info;

                return View();
            }
            catch (ErrorJsonResultException ex)
            {
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 公众号授权页入口
        /// </summary>
        /// <returns></returns>
        public ActionResult JumpToMpOAuth()
        {
            return View();
        }

        #endregion

        #region 微信用户授权相关

        public ActionResult Index(string appId)
        {
            //此页面引导用户点击授权
            ViewData["UrlUserInfo"] = Open.OAuthAPIs.OAuthApi.GetAuthorizeUrl(appId, component_AppId, "http://sdk.weixin.senparc.com/OpenOAuth/UserInfoCallback", "JeffreySu", new[] { Open.OAuthScope.snsapi_userinfo, Open.OAuthScope.snsapi_base });
            ViewData["UrlBase"] = Open.OAuthAPIs.OAuthApi.GetAuthorizeUrl(appId, component_AppId, "http://sdk.weixin.senparc.com/OpenOAuth/BaseCallback", "JeffreySu", new[] { Open.OAuthScope.snsapi_userinfo, Open.OAuthScope.snsapi_base });
            return View();
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult UserInfoCallback(string code, string state, string appId)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }

            Open.OAuthAPIs.OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                var componentAccessToken = ComponentContainer.GetComponentAccessToken(component_AppId);
                result = Open.OAuthAPIs.OAuthApi.GetAccessToken(appId, component_AppId, componentAccessToken, code);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            HttpContext.Session.SetString("OAuthAccessTokenStartTime", DateTime.Now.ToString());
            HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            try
            {
                Open.OAuthAPIs.OAuthUserInfo userInfo = Open.OAuthAPIs.OAuthApi.GetUserInfo(result.access_token, result.openid);
                return View(userInfo);
            }
            catch (ErrorJsonResultException ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// OAuthScope.snsapi_base方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult BaseCallback(string code, string state, string appId)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }

            //通过，用code换取access_token
            var componentAccessToken = ComponentContainer.TryGetComponentAccessToken(component_AppId, component_Secret);

            var result = Open.OAuthAPIs.OAuthApi.GetAccessToken(appId, component_AppId, componentAccessToken, code);//TODO:使用Container

            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            HttpContext.Session.SetString("OAuthAccessTokenStartTime", DateTime.Now.ToString());
            HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());


            //因为这里还不确定用户是否关注本微信，所以只能试探性地获取一下
            Open.OAuthAPIs.OAuthUserInfo userInfo = null;
            try
            {
                //已关注，可以得到详细信息
                userInfo = Open.OAuthAPIs.OAuthApi.GetUserInfo(result.access_token, result.openid);
                ViewData["ByBase"] = true;
                return View("UserInfoCallback", userInfo);
            }
            catch (ErrorJsonResultException ex)
            {
                //未关注，只能授权，无法得到详细信息
                //这里的 ex.JsonResult 可能为："{\"errcode\":40003,\"errmsg\":\"invalid openid\"}"
                return Content("用户已授权，授权Token：" + result);
            }
        }

        #endregion

        #region 授权信息

        public ActionResult GetAuthorizerInfoResult(string authorizerId)
        {
            var getAuthorizerInfoResult = AuthorizerContainer.GetAuthorizerInfoResult(component_AppId, authorizerId);
            return Json(getAuthorizerInfoResult);
        }

        public ActionResult RefreshAuthorizerAccessToken(string authorizerId)
        {
            var componentAccessToken = ComponentContainer.GetComponentAccessToken(component_AppId);
            var authorizationInfo = AuthorizerContainer.GetAuthorizationInfo(component_AppId, authorizerId);
            if (authorizationInfo == null)
            {
                return Content("授权信息读取失败！");
            }

            var refreshToken = authorizationInfo.authorizer_refresh_token;
            var result = AuthorizerContainer.RefreshAuthorizerToken(componentAccessToken, component_AppId, authorizerId,
                refreshToken);
            return Json(result);
        }

        #endregion
    }
}

