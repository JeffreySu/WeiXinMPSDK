/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalP1Api.cs
    文件功能描述：ExternalP1Api 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 企业微信客户联系补充接口。
    /// </summary>
    public static partial class ExternalApi
    {
        /// <summary>
        /// 分配离职成员的客户群。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static OnJobTransferGroupChatResult OnJobTransferGroupChat(string accessTokenOrAppKey,
            OnJobTransferGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<OnJobTransferGroupChatResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/groupchat/onjob_transfer", request, timeOut);

        /// <summary>
        /// 异步分配离职成员的客户群。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<OnJobTransferGroupChatResult> OnJobTransferGroupChatAsync(string accessTokenOrAppKey,
            OnJobTransferGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<OnJobTransferGroupChatResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/groupchat/onjob_transfer", request, timeOut);

        /// <summary>
        /// 提醒成员发送企业群发消息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult RemindGroupMessageSend(string accessTokenOrAppKey,
            GroupMessageOperationRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/remind_groupmsg_send", request, timeOut);

        /// <summary>
        /// 异步提醒成员发送企业群发消息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> RemindGroupMessageSendAsync(string accessTokenOrAppKey,
            GroupMessageOperationRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/remind_groupmsg_send", request, timeOut);

        /// <summary>
        /// 停止发送企业群发消息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult CancelGroupMessageSend(string accessTokenOrAppKey,
            GroupMessageOperationRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/cancel_groupmsg_send", request, timeOut);

        /// <summary>
        /// 异步停止发送企业群发消息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> CancelGroupMessageSendAsync(string accessTokenOrAppKey,
            GroupMessageOperationRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/cancel_groupmsg_send", request, timeOut);

        /// <summary>
        /// 停止发表企业朋友圈。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult CancelMomentTask(string accessTokenOrAppKey,
            CancelMomentTaskRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/cancel_moment_task", request, timeOut);

        /// <summary>
        /// 异步停止发表企业朋友圈。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> CancelMomentTaskAsync(string accessTokenOrAppKey,
            CancelMomentTaskRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/cancel_moment_task", request, timeOut);

        /// <summary>
        /// 获取微信客服已服务客户列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ServedExternalContactListResult GetServedExternalContactList(string accessTokenOrAppKey,
            ServedExternalContactListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<ServedExternalContactListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/contact_list", request, timeOut);

        /// <summary>
        /// 异步获取微信客服已服务客户列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ServedExternalContactListResult> GetServedExternalContactListAsync(string accessTokenOrAppKey,
            ServedExternalContactListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<ServedExternalContactListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/contact_list", request, timeOut);

        private static T PostP1<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostP1Async<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);
    }
}
