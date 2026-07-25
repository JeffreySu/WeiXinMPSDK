/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：UrgentNoticeApi.cs
    文件功能描述：企业微信紧急通知应用接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐紧急通知语音呼叫与接听状态接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.UrgentNotice
{
    /// <summary>
    /// 企业微信紧急通知应用接口。
    /// </summary>
    public static class UrgentNoticeApi
    {
        private const string StartCallPath = "/cgi-bin/pstncc/call";
        private const string GetCallStatePath = "/cgi-bin/pstncc/getstates";

        /// <summary>
        /// 发起紧急通知语音电话。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">需要呼叫的成员列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各成员的呼叫发起结果。</returns>
        public static StartUrgentCallResult StartCall(string accessTokenOrAppKey,
            StartUrgentCallRequest request, int timeOut = Config.TIME_OUT)
            => Post<StartUrgentCallResult>(accessTokenOrAppKey, StartCallPath, request, timeOut);

        /// <summary>
        /// 异步发起紧急通知语音电话。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">需要呼叫的成员列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各成员的呼叫发起结果。</returns>
        public static Task<StartUrgentCallResult> StartCallAsync(string accessTokenOrAppKey,
            StartUrgentCallRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<StartUrgentCallResult>(accessTokenOrAppKey, StartCallPath, request, timeOut);

        /// <summary>
        /// 获取七天内指定语音电话的接听状态。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 ID 与呼叫 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>呼叫和接听状态。</returns>
        public static GetUrgentCallStateResult GetCallState(string accessTokenOrAppKey,
            GetUrgentCallStateRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetUrgentCallStateResult>(accessTokenOrAppKey, GetCallStatePath, request, timeOut);

        /// <summary>
        /// 异步获取七天内指定语音电话的接听状态。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 ID 与呼叫 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>呼叫和接听状态。</returns>
        public static Task<GetUrgentCallStateResult> GetCallStateAsync(string accessTokenOrAppKey,
            GetUrgentCallStateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetUrgentCallStateResult>(accessTokenOrAppKey, GetCallStatePath, request, timeOut);

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
