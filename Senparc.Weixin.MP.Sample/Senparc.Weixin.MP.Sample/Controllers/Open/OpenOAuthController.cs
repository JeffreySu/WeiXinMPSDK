using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Sample.CommonService.OpenTicket;

namespace Senparc.Weixin.MP.Sample.Controllers
{

    public class OpenOAuthController : Controller
    {
        private string component_AppId = WebConfigurationManager.AppSettings["Component_Appid"];
        private string component_Secret = WebConfigurationManager.AppSettings["Component_Secret"];
        private string componentAccessToken = null;//需要授权获取，腾讯服务器会主动推送


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
                string openTicket = OpenTicketHelper.GetOpenTicket(component_AppId);

                var component_access_token = Open.CommonAPIs.CommonApi.GetComponentAccessToken(component_AppId, component_Secret, openTicket).component_access_token;
                var oauthResult = Open.ComponentAPIs.ComponentApi.QueryAuth(component_access_token, component_AppId, auth_code);

                //TODO:储存oauthResult.authorization_info
                var authInfoResult = Open.ComponentAPIs.ComponentApi.GetAuthorizerInfo(component_access_token, component_AppId,
                     oauthResult.authorization_info.authorizer_appid);

                ViewData["QueryAuthInfo"] = oauthResult.authorization_info;
                ViewData["AuthorizerInfoResult"] = authInfoResult;


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

    }
}

