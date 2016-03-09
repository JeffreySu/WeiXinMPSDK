/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：OAuthJoinAPI.cs
    文件功能描述：公众号授权给第三方平台
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

/*
    官方文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419318587&lang=zh_CN
 */

using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.Open.Entities;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// ComponentApi
    /// </summary>
    public static class ComponentApi
    {
        /// <summary>
        /// 获取第三方平台access_token
        /// </summary>
        /// <param name="componentAppId">第三方平台appid</param>
        /// <param name="componentAppSecret">第三方平台appsecret</param>
        /// <param name="componentVerifyTicket">微信后台推送的ticket，此ticket会定时推送，具体请见本页末尾的推送说明</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ComponentAccessTokenResult GetComponentAccessToken(string componentAppId, string componentAppSecret, string componentVerifyTicket, int timeOut = Config.TIME_OUT)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/component/api_component_token";

            var data = new
            {
                component_appid = componentAppId,
                component_appsecret = componentAppSecret,
                component_verify_ticket = componentVerifyTicket
            };

            return CommonJsonSend.Send<ComponentAccessTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取预授权码
        /// </summary>
        /// <param name="componentAppId">第三方平台方appid</param>
        /// <param name="componentAccessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PreAuthCodeResult GetPreAuthCode(string componentAppId, string componentAccessToken, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId
            };

            return CommonJsonSend.Send<PreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }


        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="componentAppId">第三方平台方appid</param>
        /// <param name="preAuthCode">预授权码</param>
        /// <param name="redirectUrl">回调URL</param>
        /// <returns></returns>
        public static string GetComponentLoginPageUrl(string componentAppId, string preAuthCode, string redirectUrl)
        {
            /*
             * 授权流程完成后，会进入回调URI，并在URL参数中返回授权码和过期时间(redirect_url?auth_code=xxx&expires_in=600)
             */

            var url =
                string.Format(
                    "https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid={0}&pre_auth_code={1}&redirect_uri={2}",
                    componentAppId.AsUrlData(), preAuthCode.AsUrlData(), redirectUrl.AsUrlData());

            return url;
        }

        /// <summary>
        /// 使用授权码换取公众号的授权信息
        /// </summary>
        /// <param name="componentAppId">服务开发方的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="authorizationCode">授权code,会在授权成功时返回给第三方平台，详见第三方平台授权流程说明</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static QueryAuthResult QueryAuth(string componentAccessToken, string componentAppId, string authorizationCode, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/cgi-bin/component/api_query_auth?component_access_token={0}", componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorization_code = authorizationCode
            };

            return CommonJsonSend.Send<QueryAuthResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取（刷新）授权公众号的令牌
        /// 由于access_token拥有较短的有效期，当access_token超时后，可以使用refresh_token进行刷新，refresh_token拥有较长的有效期（30天），当refresh_token失效的后，需要用户重新授权。
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppId"></param>
        /// <param name="authorizerRefreshToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RefreshAuthorizerTokenResult ApiAuthorizerToken(string componentAccessToken, string componentAppId, string authorizerAppId, string authorizerRefreshToken = null, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/cgi-bin/component/api_authorizer_token?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                authorizer_refresh_token = authorizerRefreshToken
            };

            return CommonJsonSend.Send<RefreshAuthorizerTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取授权方信息
        /// 注意：此方法返回的JSON中，authorization_info.authorizer_appid等几个参数通常为空（哪怕公众号有权限）
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetAuthorizerInfoResult GetAuthorizerInfo(string componentAccessToken, string componentAppId, string authorizerAppId, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
            };

            return CommonJsonSend.Send<GetAuthorizerInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取授权方的选项设置信息
        /// </summary>
        /// <param name="componentAppId">服务开发商的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="authorizerAppId">授权公众号appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AuthorizerOptionResult GetAuthorizerOption(string componentAccessToken, string componentAppId, string authorizerAppId, OptionName optionName, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_option?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                option_name = optionName
            };

            return CommonJsonSend.Send<AuthorizerOptionResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 设置授权方的选项信息
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="componentAppId">服务开发商的appid</param>
        /// <param name="authorizerAppId">授权公众号appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="optionValue">设置的选项值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult SetAuthorizerOption(string componentAccessToken, string componentAppId, string authorizerAppId, OptionName optionName, int optionValue, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/cgi-bin/component/api_set_authorizer_option?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                option_name = optionName,
                option_value = optionValue
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        //////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1421823488&token=&lang=zh_CN
        /// 获取调用微信JS接口的临时票据 OPEN
        /// </summary>
        /// <param name="authorizerAccessToken">authorizer_access_token</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicket(string authorizerAccessToken, string type = "jsapi")
        {
            //获取第三方平台的授权公众号token（公众号授权给第三方平台后，第三方平台通过“接口说明”中的api_authorizer_token接口得到的token）
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                    authorizerAccessToken.AsUrlData(), type.AsUrlData());

            JsApiTicketResult result = Get.GetJson<JsApiTicketResult>(url);
            return result;
        }
        //////////////////////////////////////////////////////////////////////////////////
    }
}
