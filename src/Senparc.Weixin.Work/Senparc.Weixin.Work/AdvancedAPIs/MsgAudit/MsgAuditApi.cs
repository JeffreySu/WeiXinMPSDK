/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MsgAuditApi.cs
    文件功能描述：企业微信会话内容存档 HTTP 接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会话内容存档成员、内部群和同意状态接口

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会话内容存档机器人信息接口

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加 Finance 原生 SDK 客户端入口说明

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MsgAudit
{
    /// <summary>
    /// 企业微信会话内容存档 HTTP 接口。
    /// <para>调用这些接口时，必须使用会话内容存档应用 Secret 获取的 access_token。</para>
    /// <para>聊天记录拉取、消息解密和媒体下载属于企业微信 Finance 原生 SDK，使用 <see cref="MsgAuditFinanceClient"/> 调用。</para>
    /// </summary>
    public static class MsgAuditApi
    {
        private const string GetPermitUserListPath = "/cgi-bin/msgaudit/get_permit_user_list";
        private const string GetGroupChatPath = "/cgi-bin/msgaudit/groupchat/get";
        private const string CheckSingleAgreePath = "/cgi-bin/msgaudit/check_single_agree";
        private const string CheckRoomAgreePath = "/cgi-bin/msgaudit/check_room_agree";
        private const string GetRobotInfoPath = "/cgi-bin/msgaudit/get_robot_info";

        /// <summary>
        /// 获取会话内容存档机器人信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/91774"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="robotId">待查询的机器人 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>机器人的 ID、名称和创建者 UserId。</returns>
        public static GetMsgAuditRobotInfoResult GetRobotInfo(string accessTokenOrAppKey,
            string robotId, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetMsgAuditRobotInfoResult>(null,
                    Config.ApiWorkHost + GetRobotInfoPath + "?access_token=" +
                    accessToken.AsUrlData() + "&robot_id=" + robotId.AsUrlData(), null,
                    CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        /// <summary>
        /// 异步获取会话内容存档机器人信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/91774"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="robotId">待查询的机器人 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>机器人的 ID、名称和创建者 UserId。</returns>
        public static Task<GetMsgAuditRobotInfoResult> GetRobotInfoAsync(
            string accessTokenOrAppKey, string robotId, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetMsgAuditRobotInfoResult>(null,
                    Config.ApiWorkHost + GetRobotInfoPath + "?access_token=" +
                    accessToken.AsUrlData() + "&robot_id=" + robotId.AsUrlData(), null,
                    CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        /// <summary>
        /// 获取已开启会话内容存档的成员列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="type">存档版本：1 表示办公版，2 表示服务版，3 表示企业版；不传时返回全部版本的成员。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已开启会话内容存档的成员 ID 列表。</returns>
        public static GetPermitUserListResult GetPermitUserList(string accessTokenOrAppKey, int? type = null,
            int timeOut = Config.TIME_OUT)
            => Post<GetPermitUserListResult>(accessTokenOrAppKey, GetPermitUserListPath,
                CreatePermitUserListRequest(type), timeOut);

        /// <summary>
        /// 异步获取已开启会话内容存档的成员列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="type">存档版本：1 表示办公版，2 表示服务版，3 表示企业版；不传时返回全部版本的成员。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已开启会话内容存档的成员 ID 列表。</returns>
        public static Task<GetPermitUserListResult> GetPermitUserListAsync(string accessTokenOrAppKey,
            int? type = null, int timeOut = Config.TIME_OUT)
            => PostAsync<GetPermitUserListResult>(accessTokenOrAppKey, GetPermitUserListPath,
                CreatePermitUserListRequest(type), timeOut);

        /// <summary>
        /// 获取会话内容存档中的企业内部群信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="roomId">待查询的内部群 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>内部群名称、群主、公告、创建时间和成员信息。</returns>
        public static GetGroupChatResult GetGroupChat(string accessTokenOrAppKey, string roomId,
            int timeOut = Config.TIME_OUT)
            => Post<GetGroupChatResult>(accessTokenOrAppKey, GetGroupChatPath,
                new MsgAuditRoomRequest { roomid = roomId }, timeOut);

        /// <summary>
        /// 异步获取会话内容存档中的企业内部群信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="roomId">待查询的内部群 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>内部群名称、群主、公告、创建时间和成员信息。</returns>
        public static Task<GetGroupChatResult> GetGroupChatAsync(string accessTokenOrAppKey, string roomId,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetGroupChatResult>(accessTokenOrAppKey, GetGroupChatPath,
                new MsgAuditRoomRequest { roomid = roomId }, timeOut);

        /// <summary>
        /// 获取单聊中外部成员的会话内容存档同意状态。
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待查询的企业成员与外部成员会话列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各单聊会话的同意状态和状态变更时间。</returns>
        public static CheckAgreeResult CheckSingleAgree(string accessTokenOrAppKey,
            CheckSingleAgreeRequest request, int timeOut = Config.TIME_OUT)
            => Post<CheckAgreeResult>(accessTokenOrAppKey, CheckSingleAgreePath, request, timeOut);

        /// <summary>
        /// 异步获取单聊中外部成员的会话内容存档同意状态。
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待查询的企业成员与外部成员会话列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各单聊会话的同意状态和状态变更时间。</returns>
        public static Task<CheckAgreeResult> CheckSingleAgreeAsync(string accessTokenOrAppKey,
            CheckSingleAgreeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<CheckAgreeResult>(accessTokenOrAppKey, CheckSingleAgreePath, request, timeOut);

        /// <summary>
        /// 获取群聊中外部成员的会话内容存档同意状态。
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="roomId">待查询的群聊 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>群聊成员的同意状态和状态变更时间。</returns>
        public static CheckAgreeResult CheckRoomAgree(string accessTokenOrAppKey, string roomId,
            int timeOut = Config.TIME_OUT)
            => Post<CheckAgreeResult>(accessTokenOrAppKey, CheckRoomAgreePath,
                new MsgAuditRoomRequest { roomid = roomId }, timeOut);

        /// <summary>
        /// 异步获取群聊中外部成员的会话内容存档同意状态。
        /// </summary>
        /// <param name="accessTokenOrAppKey">使用会话内容存档应用 Secret 获取的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="roomId">待查询的群聊 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>群聊成员的同意状态和状态变更时间。</returns>
        public static Task<CheckAgreeResult> CheckRoomAgreeAsync(string accessTokenOrAppKey, string roomId,
            int timeOut = Config.TIME_OUT)
            => PostAsync<CheckAgreeResult>(accessTokenOrAppKey, CheckRoomAgreePath,
                new MsgAuditRoomRequest { roomid = roomId }, timeOut);

        private static object CreatePermitUserListRequest(int? type)
            => type.HasValue ? new { type = type.Value } : new { };

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
