#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenApi.cs
    文件功能描述：OpenApi 微信接口封装


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.OpenAPIs.OpenApiJson;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.OpenAPIs
{
    /// <summary>
    /// 小程序 OpenApi 管理与基础诊断接口
    /// </summary>
    public static class OpenApi
    {
        #region 同步方法

        /// <summary>
        /// 查询指定 OpenApi 的调用配额
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static QuotaGetJsonResult QuotaGet(string accessTokenOrAppId, string cgiPath, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/quota/get?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<QuotaGetJsonResult>(null, url, new { cgi_path = cgiPath }, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询接口调用报错返回的 rid 详情
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="rid">微信返回的请求 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static RidGetJsonResult RidGet(string accessTokenOrAppId, string rid, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/rid/get?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<RidGetJsonResult>(null, url, new { rid }, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 重置指定 API 的每日调用次数
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ClearQuota(string accessTokenOrAppId, string cgiPath, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/quota/clear?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, new { cgi_path = cgiPath }, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 网络通信检测
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="checkOperator">网络通信检测运营商。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static CallbackCheckJsonResult CallbackCheck(string accessTokenOrAppId,
            CallbackCheckAction action = CallbackCheckAction.all,
            CallbackCheckOperator checkOperator = CallbackCheckOperator.DEFAULT,
            int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/callback/check?access_token={0}", accessToken.AsUrlData());
                var data = new { action = action.ToString(), check_operator = checkOperator.ToString() };
                return CommonJsonSend.Send<CallbackCheckJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取微信 API 服务器 IP 地址列表
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetApiDomainIpResult GetApiDomainIp(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/get_api_domain_ip?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<GetApiDomainIpResult>(null, url, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 使用 AppSecret 重置账号的 API 调用次数
        /// </summary>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="appSecret">小程序 AppSecret。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ClearQuotaByAppSecret(string appId, string appSecret, int timeOut = Config.TIME_OUT)
        {
            return Senparc.Weixin.MP.CommonAPIs.CommonApi.ClearQuotaByAppSecret(appId, appSecret, timeOut);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】查询指定 OpenApi 的调用配额
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<QuotaGetJsonResult> QuotaGetAsync(string accessTokenOrAppId, string cgiPath, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/quota/get?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<QuotaGetJsonResult>(null, url, new { cgi_path = cgiPath }, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询接口调用报错返回的 rid 详情
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="rid">微信返回的请求 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<RidGetJsonResult> RidGetAsync(string accessTokenOrAppId, string rid, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/rid/get?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<RidGetJsonResult>(null, url, new { rid }, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】重置指定 API 的每日调用次数
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="cgiPath">需要查询或清零额度的 API 路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> ClearQuotaAsync(string accessTokenOrAppId, string cgiPath, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/openapi/quota/clear?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, new { cgi_path = cgiPath }, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】网络通信检测
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="checkOperator">网络通信检测运营商。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<CallbackCheckJsonResult> CallbackCheckAsync(string accessTokenOrAppId,
            CallbackCheckAction action = CallbackCheckAction.all,
            CallbackCheckOperator checkOperator = CallbackCheckOperator.DEFAULT,
            int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/callback/check?access_token={0}", accessToken.AsUrlData());
                var data = new { action = action.ToString(), check_operator = checkOperator.ToString() };
                return await CommonJsonSend.SendAsync<CallbackCheckJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取微信 API 服务器 IP 地址列表
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetApiDomainIpResult> GetApiDomainIpAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/get_api_domain_ip?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<GetApiDomainIpResult>(null, url, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】使用 AppSecret 重置账号的 API 调用次数
        /// </summary>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="appSecret">小程序 AppSecret。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ClearQuotaByAppSecretAsync(string appId, string appSecret, int timeOut = Config.TIME_OUT)
        {
            return Senparc.Weixin.MP.CommonAPIs.CommonApi.ClearQuotaByAppSecretAsync(appId, appSecret, timeOut);
        }

        #endregion
    }
}
