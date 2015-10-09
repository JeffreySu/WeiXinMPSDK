﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
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

namespace Senparc.Weixin.Open.ComponentAPIs
{
    public static class ComponentApi
    {
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
                    componentAppId, preAuthCode, redirectUrl.UrlEncode());

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
                    "https://api.weixin.qq.com/cgi-bin/component/api_query_auth?component_access_token={0}", componentAccessToken);

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
        public static RefreshAuthorizerTokenResult RefreshAuthorizerToken(string componentAccessToken, string componentAppId, string authorizerAppId, string authorizerRefreshToken, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    "https://api.weixin.qq.com/cgi-bin/component/api_authorizer_token?component_access_token={0}",
                    componentAccessToken);

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
                    componentAccessToken);

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
                    componentAccessToken);

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
                    componentAccessToken);

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                option_name = optionName,
                option_value = optionValue
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
    }
}
