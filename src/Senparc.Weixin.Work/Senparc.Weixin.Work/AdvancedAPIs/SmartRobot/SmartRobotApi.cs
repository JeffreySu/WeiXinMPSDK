/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SmartRobotApi.cs
    文件功能描述：SmartRobotApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.SmartRobot
{
    /// <summary>企业微信智能机器人接口。</summary>
    public static class SmartRobotApi
    {
        /// <summary>使用回调中的临时 response_url 主动回复消息。</summary>
        /// <param name="responseUrl">认证结果回调地址。</param>
        /// <param name="reply">智能机器人回复内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult Reply(string responseUrl, SmartRobotReply reply, int timeOut = Config.TIME_OUT)
            => CommonJsonSend.Send<WorkJsonResult>(null, responseUrl, reply, CommonJsonSendType.POST, timeOut);

        /// <summary>
        /// 异步回复智能机器人消息。
        /// </summary>
        /// <param name="responseUrl">认证结果回调地址。</param>
        /// <param name="reply">智能机器人回复内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> ReplyAsync(string responseUrl, SmartRobotReply reply, int timeOut = Config.TIME_OUT)
            => CommonJsonSend.SendAsync<WorkJsonResult>(null, responseUrl, reply, CommonJsonSendType.POST, timeOut);

        /// <summary>
        /// 获取客户群列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static SmartSheetGroupChatListResult GetGroupChatList(string token,
            SmartSheetGroupChatListRequest request, int timeOut = Config.TIME_OUT)
            => Post<SmartSheetGroupChatListResult>(token, "/cgi-bin/wedoc/smartsheet/groupchat/list", request, timeOut);

        /// <summary>
        /// 异步获取客户群列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<SmartSheetGroupChatListResult> GetGroupChatListAsync(string token,
            SmartSheetGroupChatListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SmartSheetGroupChatListResult>(token, "/cgi-bin/wedoc/smartsheet/groupchat/list", request, timeOut);

        /// <summary>
        /// 获取客户群详情。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static SmartSheetGroupChatResult GetGroupChat(string token,
            SmartSheetGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => Post<SmartSheetGroupChatResult>(token, "/cgi-bin/wedoc/smartsheet/groupchat/get", request, timeOut);

        /// <summary>
        /// 异步获取客户群详情。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<SmartSheetGroupChatResult> GetGroupChatAsync(string token,
            SmartSheetGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SmartSheetGroupChatResult>(token, "/cgi-bin/wedoc/smartsheet/groupchat/get", request, timeOut);

        /// <summary>
        /// 更新客户群配置。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult UpdateGroupChat(string token,
            UpdateSmartSheetGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/wedoc/smartsheet/groupchat/update", request, timeOut);

        /// <summary>
        /// 异步更新客户群配置。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> UpdateGroupChatAsync(string token,
            UpdateSmartSheetGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/wedoc/smartsheet/groupchat/update", request, timeOut);

        private static T Post<T>(string token, string path, object request, int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut), token);

        private static Task<T> PostAsync<T>(string token, string path, object request, int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut), token);
    }
}
