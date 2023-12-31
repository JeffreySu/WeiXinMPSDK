/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：OAuth2Controller.cs
    文件功能描述：提供OAuth2.0授权测试（关注微信公众号：盛派网络小助手，点击菜单【功能体验】 【OAuth2.0授权测试】即可体验）
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

//DPBMARK_FILE MP
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System.Text;

namespace Senparc.Weixin.Sample.MP.Controllers
{
    public class OAuth2Controller : BaseController
    {
        //下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        public readonly string appId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        private readonly string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;//与微信公众账号后台的AppId设置保持一致，区分大小写。

        /// <summary>
        /// 登录入口
        /// </summary>
        /// <param name="returnUrl">用户尝试进入的需要登录的页面</param>
        /// <returns></returns>
        public ActionResult Index(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            //此页面引导用户点击授权
            ViewData["UrlUserInfo"] =
                OAuthApi.GetAuthorizeUrl(appId,
                "http://sdk.weixin.senparc.com/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode(),
                null, OAuthScope.snsapi_userinfo);//snsapi_userinfo方式回调地址

            ViewData["UrlBase"] =
                OAuthApi.GetAuthorizeUrl(appId,
                "http://sdk.weixin.senparc.com/oauth2/BaseCallback?returnUrl=" + returnUrl.UrlEncode(),
                null, OAuthScope.snsapi_base);//snsapi_base方式回调地址
            return View();
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="returnUrl">用户最初尝试进入的页面</param>
        /// <returns></returns>
        public ActionResult UserInfoCallback(string code, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(appId, appSecret, code);
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
            HttpContext.Session.SetString("OAuthAccessTokenStartTime", SystemTime.Now.ToString());
            HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            try
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
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
        /// <param name="returnUrl">用户最初尝试进入的页面</param>
        /// <returns></returns>
        public ActionResult BaseCallback(string code, string returnUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return Content("您拒绝了授权！");
                }

                //通过，用code换取access_token
                var result = OAuthApi.GetAccessToken(appId, appSecret, code);
                if (result.errcode != ReturnCode.请求成功)
                {
                    return Content("错误：" + result.errmsg);
                }

                //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
                //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
                HttpContext.Session.SetString("OAuthAccessTokenStartTime", SystemTime.Now.ToString());
                HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());

                //因为这里还不确定用户是否关注本微信，所以只能试探性地获取一下
                OAuthUserInfo userInfo = null;
                try
                {
                    //已关注，可以得到详细信息
                    userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }


                    ViewData["ByBase"] = true;
                    return View("UserInfoCallback", userInfo);
                }
                catch (ErrorJsonResultException ex)
                {
                    //未关注，只能授权，无法得到详细信息
                    //这里的 ex.JsonResult 可能为："{\"errcode\":40003,\"errmsg\":\"invalid openid\"}"
                    return Content("用户已授权，授权Token：" + result, "text/html", Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                WeixinTrace.SendCustomLog("BaseCallback 发生错误", ex.ToString());
                return Content("发生错误：" + ex.ToString());
            }
        }
    }
}