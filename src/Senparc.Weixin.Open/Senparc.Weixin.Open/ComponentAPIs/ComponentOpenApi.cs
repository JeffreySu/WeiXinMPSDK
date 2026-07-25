#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ComponentOpenApi.cs
    文件功能描述：ComponentOpenApi 微信接口封装


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 第三方平台 openApi 管理接口。
    /// </summary>
    public static class ComponentOpenApi
    {
        /// <summary>
        /// 启动第三方平台票据推送服务。
        /// </summary>
        /// <param name="componentAppId">第三方平台 AppId。</param>
        /// <param name="componentSecret">第三方平台 AppSecret。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult StartPushTicket(string componentAppId, string componentSecret,
            int timeOut = Config.TIME_OUT)
        {
            var url = Config.ApiMpHost + "/cgi-bin/component/api_start_push_ticket";
            var data = new { component_appid = componentAppId, component_secret = componentSecret };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步启动第三方平台票据推送。
        /// </summary>
        /// <param name="componentAppId">第三方平台 AppId。</param>
        /// <param name="componentSecret">第三方平台 AppSecret。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> StartPushTicketAsync(string componentAppId, string componentSecret,
            int timeOut = Config.TIME_OUT)
        {
            var url = Config.ApiMpHost + "/cgi-bin/component/api_start_push_ticket";
            var data = new { component_appid = componentAppId, component_secret = componentSecret };
            return CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询第三方平台接口调用额度。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ComponentQuotaGetJsonResult GetComponentQuota(string componentAccessToken, string cgiPath,
            int timeOut = Config.TIME_OUT) => GetQuota(componentAccessToken, cgiPath, timeOut);

        /// <summary>
        /// 异步查询第三方平台接口调用额度。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ComponentQuotaGetJsonResult> GetComponentQuotaAsync(string componentAccessToken, string cgiPath,
            int timeOut = Config.TIME_OUT) => GetQuotaAsync(componentAccessToken, cgiPath, timeOut);

        /// <summary>
        /// 查询授权账号接口调用额度。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ComponentQuotaGetJsonResult GetAuthorizerQuota(string authorizerAccessToken, string cgiPath,
            int timeOut = Config.TIME_OUT) => GetQuota(authorizerAccessToken, cgiPath, timeOut);

        /// <summary>
        /// 异步查询授权账号接口调用额度。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ComponentQuotaGetJsonResult> GetAuthorizerQuotaAsync(string authorizerAccessToken, string cgiPath,
            int timeOut = Config.TIME_OUT) => GetQuotaAsync(authorizerAccessToken, cgiPath, timeOut);

        /// <summary>
        /// 查询第三方平台调用请求详情。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="rid">微信返回的请求 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ComponentRidGetJsonResult GetComponentRid(string componentAccessToken, string rid,
            int timeOut = Config.TIME_OUT) => GetRid(componentAccessToken, rid, timeOut);

        /// <summary>
        /// 异步查询第三方平台调用请求详情。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="rid">微信返回的请求 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ComponentRidGetJsonResult> GetComponentRidAsync(string componentAccessToken, string rid,
            int timeOut = Config.TIME_OUT) => GetRidAsync(componentAccessToken, rid, timeOut);

        /// <summary>
        /// 查询授权账号调用请求详情。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="rid">微信返回的请求 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ComponentRidGetJsonResult GetAuthorizerRid(string authorizerAccessToken, string rid,
            int timeOut = Config.TIME_OUT) => GetRid(authorizerAccessToken, rid, timeOut);

        /// <summary>
        /// 异步查询授权账号调用请求详情。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="rid">微信返回的请求 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ComponentRidGetJsonResult> GetAuthorizerRidAsync(string authorizerAccessToken, string rid,
            int timeOut = Config.TIME_OUT) => GetRidAsync(authorizerAccessToken, rid, timeOut);

        /// <summary>
        /// 清空第三方平台接口调用次数。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ClearComponentQuota(string componentAccessToken, string cgiPath,
            int timeOut = Config.TIME_OUT) => ClearQuota(componentAccessToken, cgiPath, timeOut);

        /// <summary>
        /// 异步清空第三方平台接口调用次数。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ClearComponentQuotaAsync(string componentAccessToken, string cgiPath,
            int timeOut = Config.TIME_OUT) => ClearQuotaAsync(componentAccessToken, cgiPath, timeOut);

        /// <summary>
        /// 清空授权账号接口调用次数。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ClearAuthorizerQuota(string authorizerAccessToken, string cgiPath,
            int timeOut = Config.TIME_OUT) => ClearQuota(authorizerAccessToken, cgiPath, timeOut);

        /// <summary>
        /// 异步清空授权账号接口调用次数。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ClearAuthorizerQuotaAsync(string authorizerAccessToken, string cgiPath,
            int timeOut = Config.TIME_OUT) => ClearQuotaAsync(authorizerAccessToken, cgiPath, timeOut);

        /// <summary>
        /// 使用第三方平台 AppSecret 重置调用次数，不依赖 component_access_token。
        /// </summary>
        /// <param name="componentAppId">第三方平台 AppId。</param>
        /// <param name="componentSecret">第三方平台 AppSecret。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ClearComponentQuotaByAppSecret(string componentAppId, string componentSecret,
            string appId = null, int timeOut = Config.TIME_OUT)
        {
            var url = Config.ApiMpHost + "/cgi-bin/component/clear_quota/v2";
            var data = new { appid = appId, component_appid = componentAppId, appsecret = componentSecret };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步使用 AppSecret 清空第三方平台接口调用次数。
        /// </summary>
        /// <param name="componentAppId">第三方平台 AppId。</param>
        /// <param name="componentSecret">第三方平台 AppSecret。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ClearComponentQuotaByAppSecretAsync(string componentAppId, string componentSecret,
            string appId = null, int timeOut = Config.TIME_OUT)
        {
            var url = Config.ApiMpHost + "/cgi-bin/component/clear_quota/v2";
            var data = new { appid = appId, component_appid = componentAppId, appsecret = componentSecret };
            return CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 检查第三方平台回调地址。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="checkOperator">网络通信检测运营商。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ComponentCallbackCheckJsonResult CheckComponentCallback(string componentAccessToken,
            string action = "all", string checkOperator = "DEFAULT", int timeOut = Config.TIME_OUT) =>
            CallbackCheck(componentAccessToken, action, checkOperator, timeOut);

        /// <summary>
        /// 异步检查第三方平台回调地址。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="checkOperator">网络通信检测运营商。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ComponentCallbackCheckJsonResult> CheckComponentCallbackAsync(string componentAccessToken,
            string action = "all", string checkOperator = "DEFAULT", int timeOut = Config.TIME_OUT) =>
            CallbackCheckAsync(componentAccessToken, action, checkOperator, timeOut);

        /// <summary>
        /// 检查授权账号回调地址。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="checkOperator">网络通信检测运营商。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ComponentCallbackCheckJsonResult CheckAuthorizerCallback(string authorizerAccessToken,
            string action = "all", string checkOperator = "DEFAULT", int timeOut = Config.TIME_OUT) =>
            CallbackCheck(authorizerAccessToken, action, checkOperator, timeOut);

        /// <summary>
        /// 异步检查授权账号回调地址。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="checkOperator">网络通信检测运营商。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ComponentCallbackCheckJsonResult> CheckAuthorizerCallbackAsync(string authorizerAccessToken,
            string action = "all", string checkOperator = "DEFAULT", int timeOut = Config.TIME_OUT) =>
            CallbackCheckAsync(authorizerAccessToken, action, checkOperator, timeOut);

        /// <summary>
        /// 获取第三方平台 API 域名 IP。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ApiDomainIpJsonResult GetComponentApiDomainIp(string componentAccessToken,
            int timeOut = Config.TIME_OUT) => GetApiDomainIp(componentAccessToken, timeOut);

        /// <summary>
        /// 异步获取第三方平台 API 域名 IP。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ApiDomainIpJsonResult> GetComponentApiDomainIpAsync(string componentAccessToken,
            int timeOut = Config.TIME_OUT) => GetApiDomainIpAsync(componentAccessToken, timeOut);

        /// <summary>
        /// 获取授权账号 API 域名 IP。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ApiDomainIpJsonResult GetAuthorizerApiDomainIp(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => GetApiDomainIp(authorizerAccessToken, timeOut);

        /// <summary>
        /// 异步获取授权账号 API 域名 IP。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ApiDomainIpJsonResult> GetAuthorizerApiDomainIpAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => GetApiDomainIpAsync(authorizerAccessToken, timeOut);

        private static ComponentQuotaGetJsonResult GetQuota(string accessToken, string cgiPath, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/openapi/quota/get?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<ComponentQuotaGetJsonResult>(null, url, new { cgi_path = cgiPath },
                CommonJsonSendType.POST, timeOut);
        }

        private static Task<ComponentQuotaGetJsonResult> GetQuotaAsync(string accessToken, string cgiPath, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/openapi/quota/get?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<ComponentQuotaGetJsonResult>(null, url, new { cgi_path = cgiPath },
                CommonJsonSendType.POST, timeOut);
        }

        private static ComponentRidGetJsonResult GetRid(string accessToken, string rid, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/openapi/rid/get?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<ComponentRidGetJsonResult>(null, url, new { rid }, CommonJsonSendType.POST, timeOut);
        }

        private static Task<ComponentRidGetJsonResult> GetRidAsync(string accessToken, string rid, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/openapi/rid/get?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<ComponentRidGetJsonResult>(null, url, new { rid }, CommonJsonSendType.POST, timeOut);
        }

        private static WxJsonResult ClearQuota(string accessToken, string cgiPath, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/openapi/quota/clear?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<WxJsonResult>(null, url, new { cgi_path = cgiPath }, CommonJsonSendType.POST, timeOut);
        }

        private static Task<WxJsonResult> ClearQuotaAsync(string accessToken, string cgiPath, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/openapi/quota/clear?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<WxJsonResult>(null, url, new { cgi_path = cgiPath }, CommonJsonSendType.POST, timeOut);
        }

        private static ComponentCallbackCheckJsonResult CallbackCheck(string accessToken, string action,
            string checkOperator, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/callback/check?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<ComponentCallbackCheckJsonResult>(null, url,
                new { action, check_operator = checkOperator }, CommonJsonSendType.POST, timeOut);
        }

        private static Task<ComponentCallbackCheckJsonResult> CallbackCheckAsync(string accessToken, string action,
            string checkOperator, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/callback/check?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<ComponentCallbackCheckJsonResult>(null, url,
                new { action, check_operator = checkOperator }, CommonJsonSendType.POST, timeOut);
        }

        private static ApiDomainIpJsonResult GetApiDomainIp(string accessToken, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/get_api_domain_ip?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<ApiDomainIpJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        private static Task<ApiDomainIpJsonResult> GetApiDomainIpAsync(string accessToken, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/get_api_domain_ip?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<ApiDomainIpJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }
    }

    /// <summary>
    /// ComponentQuotaGet 接口返回结果。
    /// </summary>
    public class ComponentQuotaGetJsonResult : WxJsonResult
    {
        public ComponentQuota quota { get; set; }
        public ComponentRateLimit rate_limit { get; set; }
        public ComponentRateLimit component_rate_limit { get; set; }
    }

    /// <summary>
    /// ComponentQuota 微信接口数据模型。
    /// </summary>
    public class ComponentQuota
    {
        public int daily_limit { get; set; }
        public int used { get; set; }
        public int remain { get; set; }
    }

    /// <summary>
    /// ComponentRateLimit 微信接口数据模型。
    /// </summary>
    public class ComponentRateLimit
    {
        public int call_count { get; set; }
        public int refresh_second { get; set; }
    }

    /// <summary>
    /// ComponentRidGet 接口返回结果。
    /// </summary>
    public class ComponentRidGetJsonResult : WxJsonResult
    {
        public ComponentRidRequest request { get; set; }
    }

    /// <summary>
    /// ComponentRid 接口请求参数。
    /// </summary>
    public class ComponentRidRequest
    {
        public long invoke_time { get; set; }
        public int cost_in_ms { get; set; }
        public string request_url { get; set; }
        public string request_body { get; set; }
        public string response_body { get; set; }
        public string client_ip { get; set; }
    }

    /// <summary>
    /// ComponentCallbackCheck 接口返回结果。
    /// </summary>
    public class ComponentCallbackCheckJsonResult : WxJsonResult
    {
        public ComponentCallbackDnsResult[] dns { get; set; }
        public ComponentCallbackPingResult[] ping { get; set; }
    }

    /// <summary>
    /// ComponentCallbackDns 接口返回结果。
    /// </summary>
    public class ComponentCallbackDnsResult
    {
        public string ip { get; set; }
        public string real_operator { get; set; }
    }

    /// <summary>
    /// ComponentCallbackPing 接口返回结果。
    /// </summary>
    public class ComponentCallbackPingResult
    {
        public string ip { get; set; }
        public string from_operator { get; set; }
        public string package_loss { get; set; }
        public string time { get; set; }
    }

    /// <summary>
    /// ApiDomainIp 接口返回结果。
    /// </summary>
    public class ApiDomainIpJsonResult : WxJsonResult
    {
        public string[] ip_list { get; set; }
    }
}
