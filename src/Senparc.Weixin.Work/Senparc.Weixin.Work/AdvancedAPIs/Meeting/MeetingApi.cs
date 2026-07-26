/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.cs
    文件功能描述：企业微信会议基础管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议创建、更新和成员会议列表接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 企业微信会议接口。
    /// </summary>
    public static partial class MeetingApi
    {
        private const string CreatePath = "/cgi-bin/meeting/create";
        private const string UpdatePath = "/cgi-bin/meeting/update";
        private const string GetUserMeetingIdsPath = "/cgi-bin/meeting/get_user_meetingid";
        private const string GetInviteesPath = "/cgi-bin/meeting/get_invitees";
        private const string SetInviteesPath = "/cgi-bin/meeting/set_invitees";
        private const string CreateCustomerShortUrlPath = "/cgi-bin/meeting/create_customer_short_url";
        private const string GetCustomerShortUrlPath = "/cgi-bin/meeting/get_customer_short_url";
        private const string GetRealtimeAttendeeListPath = "/cgi-bin/meeting/get_realtime_attendee_list";
        private const string GetAttendeeListPath = "/cgi-bin/meeting/get_attendee_list";
        private const string GetCurrentWaitingRoomUsersPath =
            "/cgi-bin/meeting/waitingroom/get_current_user_list";
        private const string GetWaitingRoomUsersPath = "/cgi-bin/meeting/waitingroom/get_user_list";
        private const string GetQualityPath = "/cgi-bin/meeting/get_quality";

        /// <summary>
        /// 创建企业微信会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93706"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98148"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议管理员、时间、参与者、会议设置和重复规则。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议 ID、会议号、会议链接及超出高级账号范围的成员。</returns>
        public static CreateMeetingResult CreateMeeting(string accessTokenOrAppKey,
            CreateMeetingRequest request, int timeOut = Config.TIME_OUT)
            => Post<CreateMeetingResult>(accessTokenOrAppKey, CreatePath, request, timeOut);

        /// <summary>
        /// 异步创建企业微信会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93706"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98148"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议管理员、时间、参与者、会议设置和重复规则。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议 ID、会议号、会议链接及超出高级账号范围的成员。</returns>
        public static Task<CreateMeetingResult> CreateMeetingAsync(string accessTokenOrAppKey,
            CreateMeetingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<CreateMeetingResult>(accessTokenOrAppKey, CreatePath, request, timeOut);

        /// <summary>
        /// 更新企业微信会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93710"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98154"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 及需要更新的会议字段。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果及超出高级账号范围的成员。</returns>
        public static UpdateMeetingResult UpdateMeeting(string accessTokenOrAppKey,
            UpdateMeetingRequest request, int timeOut = Config.TIME_OUT)
            => Post<UpdateMeetingResult>(accessTokenOrAppKey, UpdatePath, request, timeOut);

        /// <summary>
        /// 异步更新企业微信会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93710"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98154"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 及需要更新的会议字段。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果及超出高级账号范围的成员。</returns>
        public static Task<UpdateMeetingResult> UpdateMeetingAsync(string accessTokenOrAppKey,
            UpdateMeetingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<UpdateMeetingResult>(accessTokenOrAppKey, UpdatePath, request, timeOut);

        /// <summary>
        /// 分页获取成员指定时间范围内的会议 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93707"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98714"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">成员账号、时间范围及分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议 ID 列表和下一页游标。</returns>
        public static GetUserMeetingIdsResult GetUserMeetingIds(string accessTokenOrAppKey,
            GetUserMeetingIdsRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetUserMeetingIdsResult>(accessTokenOrAppKey, GetUserMeetingIdsPath, request, timeOut);

        /// <summary>
        /// 异步分页获取成员指定时间范围内的会议 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93707"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98714"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">成员账号、时间范围及分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议 ID 列表和下一页游标。</returns>
        public static Task<GetUserMeetingIdsResult> GetUserMeetingIdsAsync(string accessTokenOrAppKey,
            GetUserMeetingIdsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetUserMeetingIdsResult>(accessTokenOrAppKey, GetUserMeetingIdsPath, request, timeOut);

        /// <summary>
        /// 分页获取会议受邀成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98160"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和可选分页游标。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>受邀成员列表、是否还有更多数据及下一页游标。</returns>
        public static GetMeetingInviteesResult GetMeetingInvitees(string accessTokenOrAppKey,
            GetMeetingInviteesRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingInviteesResult>(accessTokenOrAppKey, GetInviteesPath, request, timeOut);

        /// <summary>
        /// 异步分页获取会议受邀成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98160"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和可选分页游标。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>受邀成员列表、是否还有更多数据及下一页游标。</returns>
        public static Task<GetMeetingInviteesResult> GetMeetingInviteesAsync(string accessTokenOrAppKey,
            GetMeetingInviteesRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingInviteesResult>(accessTokenOrAppKey, GetInviteesPath, request, timeOut);

        /// <summary>
        /// 设置会议受邀成员列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98162"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和替换后的受邀成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>设置结果。</returns>
        public static SetMeetingInviteesResult SetMeetingInvitees(string accessTokenOrAppKey,
            SetMeetingInviteesRequest request, int timeOut = Config.TIME_OUT)
            => Post<SetMeetingInviteesResult>(accessTokenOrAppKey, SetInviteesPath, request, timeOut);

        /// <summary>
        /// 异步设置会议受邀成员列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98162"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和替换后的受邀成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>设置结果。</returns>
        public static Task<SetMeetingInviteesResult> SetMeetingInviteesAsync(string accessTokenOrAppKey,
            SetMeetingInviteesRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SetMeetingInviteesResult>(accessTokenOrAppKey, SetInviteesPath, request, timeOut);

        /// <summary>
        /// 创建带自定义数据的用户专属参会短链接。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98818"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要透传的用户自定义数据。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>专属参会短链接及对应自定义数据。</returns>
        public static CreateMeetingCustomerShortUrlResult CreateMeetingCustomerShortUrl(
            string accessTokenOrAppKey, CreateMeetingCustomerShortUrlRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<CreateMeetingCustomerShortUrlResult>(accessTokenOrAppKey,
                CreateCustomerShortUrlPath, request, timeOut);

        /// <summary>
        /// 异步创建带自定义数据的用户专属参会短链接。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98818"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要透传的用户自定义数据。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>专属参会短链接及对应自定义数据。</returns>
        public static Task<CreateMeetingCustomerShortUrlResult> CreateMeetingCustomerShortUrlAsync(
            string accessTokenOrAppKey, CreateMeetingCustomerShortUrlRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<CreateMeetingCustomerShortUrlResult>(accessTokenOrAppKey,
                CreateCustomerShortUrlPath, request, timeOut);

        /// <summary>
        /// 获取会议已创建的用户专属参会短链接。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98819"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>专属参会短链接及自定义数据列表。</returns>
        public static GetMeetingCustomerShortUrlsResult GetMeetingCustomerShortUrls(string accessTokenOrAppKey,
            GetMeetingCustomerShortUrlsRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingCustomerShortUrlsResult>(accessTokenOrAppKey,
                GetCustomerShortUrlPath, request, timeOut);

        /// <summary>
        /// 异步获取会议已创建的用户专属参会短链接。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98819"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>专属参会短链接及自定义数据列表。</returns>
        public static Task<GetMeetingCustomerShortUrlsResult> GetMeetingCustomerShortUrlsAsync(
            string accessTokenOrAppKey, GetMeetingCustomerShortUrlsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingCustomerShortUrlsResult>(accessTokenOrAppKey,
                GetCustomerShortUrlPath, request, timeOut);

        /// <summary>
        /// 分页获取会议中的实时参会成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98157"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、可选子会议 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>实时参会成员和终端状态。</returns>
        public static GetMeetingRealtimeAttendeesResult GetMeetingRealtimeAttendees(string accessTokenOrAppKey,
            GetMeetingRealtimeAttendeesRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRealtimeAttendeesResult>(accessTokenOrAppKey,
                GetRealtimeAttendeeListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取会议中的实时参会成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98157"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、可选子会议 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>实时参会成员和终端状态。</returns>
        public static Task<GetMeetingRealtimeAttendeesResult> GetMeetingRealtimeAttendeesAsync(
            string accessTokenOrAppKey, GetMeetingRealtimeAttendeesRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRealtimeAttendeesResult>(accessTokenOrAppKey,
                GetRealtimeAttendeeListPath, request, timeOut);

        /// <summary>
        /// 分页获取会议历史参会成员明细。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98156"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、子会议、时间范围和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>历史参会成员、入离会时间和终端状态。</returns>
        public static GetMeetingAttendeesResult GetMeetingAttendees(string accessTokenOrAppKey,
            GetMeetingAttendeesRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingAttendeesResult>(accessTokenOrAppKey, GetAttendeeListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取会议历史参会成员明细。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98156"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、子会议、时间范围和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>历史参会成员、入离会时间和终端状态。</returns>
        public static Task<GetMeetingAttendeesResult> GetMeetingAttendeesAsync(string accessTokenOrAppKey,
            GetMeetingAttendeesRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingAttendeesResult>(accessTokenOrAppKey,
                GetAttendeeListPath, request, timeOut);

        /// <summary>
        /// 分页获取会议等候室中的当前成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98163"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>当前等候成员、终端类型和专属链接自定义数据。</returns>
        public static GetCurrentMeetingWaitingRoomUsersResult GetCurrentMeetingWaitingRoomUsers(
            string accessTokenOrAppKey, GetMeetingWaitingRoomUsersRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetCurrentMeetingWaitingRoomUsersResult>(accessTokenOrAppKey,
                GetCurrentWaitingRoomUsersPath, request, timeOut);

        /// <summary>
        /// 异步分页获取会议等候室中的当前成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98163"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>当前等候成员、终端类型和专属链接自定义数据。</returns>
        public static Task<GetCurrentMeetingWaitingRoomUsersResult> GetCurrentMeetingWaitingRoomUsersAsync(
            string accessTokenOrAppKey, GetMeetingWaitingRoomUsersRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetCurrentMeetingWaitingRoomUsersResult>(accessTokenOrAppKey,
                GetCurrentWaitingRoomUsersPath, request, timeOut);

        /// <summary>
        /// 分页获取会议等候室成员的进入和离开记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98164"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>等候室成员及其进入、离开时间。</returns>
        public static GetMeetingWaitingRoomUsersResult GetMeetingWaitingRoomUsers(string accessTokenOrAppKey,
            GetMeetingWaitingRoomUsersRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingWaitingRoomUsersResult>(accessTokenOrAppKey,
                GetWaitingRoomUsersPath, request, timeOut);

        /// <summary>
        /// 异步分页获取会议等候室成员的进入和离开记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98164"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>等候室成员及其进入、离开时间。</returns>
        public static Task<GetMeetingWaitingRoomUsersResult> GetMeetingWaitingRoomUsersAsync(
            string accessTokenOrAppKey, GetMeetingWaitingRoomUsersRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingWaitingRoomUsersResult>(accessTokenOrAppKey,
                GetWaitingRoomUsersPath, request, timeOut);

        /// <summary>
        /// 分页获取会议整体及参会成员的质量数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98821"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、子会议、开始时间和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议整体及成员的音视频、共享屏幕和网络质量。</returns>
        public static GetMeetingQualityResult GetMeetingQuality(string accessTokenOrAppKey,
            GetMeetingQualityRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingQualityResult>(accessTokenOrAppKey, GetQualityPath, request, timeOut);

        /// <summary>
        /// 异步分页获取会议整体及参会成员的质量数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98821"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、子会议、开始时间和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议整体及成员的音视频、共享屏幕和网络质量。</returns>
        public static Task<GetMeetingQualityResult> GetMeetingQualityAsync(string accessTokenOrAppKey,
            GetMeetingQualityRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingQualityResult>(accessTokenOrAppKey, GetQualityPath, request, timeOut);

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
