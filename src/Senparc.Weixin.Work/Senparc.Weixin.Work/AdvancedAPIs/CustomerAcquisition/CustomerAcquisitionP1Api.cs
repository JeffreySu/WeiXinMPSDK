/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CustomerAcquisitionP1Api.cs
    文件功能描述：CustomerAcquisitionP1Api 微信接口封装


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition.CustomerAcquisitionJson;

namespace Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition
{
    /// <summary>
    /// 企业微信获客助手客户、额度、统计及会话接口。
    /// </summary>
    public partial class CustomerAcquisitionApi
    {
        /// <summary>
        /// 获取获客助手客户列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetCustomerAcquisitionCustomerResult GetCustomers(string accessTokenOrAppKey,
            GetCustomerAcquisitionCustomerRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetCustomerAcquisitionCustomerResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_acquisition/customer", request, timeOut);

        /// <summary>
        /// 异步获取获客助手客户列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetCustomerAcquisitionCustomerResult> GetCustomersAsync(string accessTokenOrAppKey,
            GetCustomerAcquisitionCustomerRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetCustomerAcquisitionCustomerResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_acquisition/customer", request, timeOut);

        /// <summary>
        /// 获取获客助手使用量。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetCustomerAcquisitionQuotaResult GetQuota(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<GetCustomerAcquisitionQuotaResult>(null,
                Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition_quota?access_token=" + accessToken.AsUrlData(),
                null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        /// <summary>
        /// 异步获取获客助手使用量。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetCustomerAcquisitionQuotaResult> GetQuotaAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<GetCustomerAcquisitionQuotaResult>(null,
                Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition_quota?access_token=" + accessToken.AsUrlData(),
                null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        /// <summary>
        /// 获取获客助手统计数据。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetCustomerAcquisitionStatisticResult GetStatistic(string accessTokenOrAppKey,
            GetCustomerAcquisitionStatisticRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetCustomerAcquisitionStatisticResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_acquisition/statistic", request, timeOut);

        /// <summary>
        /// 异步获取获客助手统计数据。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetCustomerAcquisitionStatisticResult> GetStatisticAsync(string accessTokenOrAppKey,
            GetCustomerAcquisitionStatisticRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetCustomerAcquisitionStatisticResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_acquisition/statistic", request, timeOut);

        /// <summary>
        /// 获取获客助手会话信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetCustomerAcquisitionChatInfoResult GetChatInfo(string accessTokenOrAppKey,
            GetCustomerAcquisitionChatInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetCustomerAcquisitionChatInfoResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_acquisition/get_chat_info", request, timeOut);

        /// <summary>
        /// 异步获取获客助手会话信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetCustomerAcquisitionChatInfoResult> GetChatInfoAsync(string accessTokenOrAppKey,
            GetCustomerAcquisitionChatInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetCustomerAcquisitionChatInfoResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_acquisition/get_chat_info", request, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);
    }
}
