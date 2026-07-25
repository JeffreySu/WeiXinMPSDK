/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingRoomApi.cs
    文件功能描述：企业微信会议室管理与预定接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐会议室管理与预定接口

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐按会议 ID 查询会议室预定接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MeetingRoom
{
    /// <summary>
    /// 企业微信会议室管理与预定接口。
    /// </summary>
    public static class MeetingRoomApi
    {
        private const string AddPath = "/cgi-bin/oa/meetingroom/add";
        private const string ListPath = "/cgi-bin/oa/meetingroom/list";
        private const string EditPath = "/cgi-bin/oa/meetingroom/edit";
        private const string DeletePath = "/cgi-bin/oa/meetingroom/del";
        private const string GetBookingInfoPath = "/cgi-bin/oa/meetingroom/get_booking_info";
        private const string GetBookingInfoByMeetingIdPath =
            "/cgi-bin/oa/meetingroom/get_booking_info_by_meeting_id";
        private const string BookPath = "/cgi-bin/oa/meetingroom/book";
        private const string BookBySchedulePath = "/cgi-bin/oa/meetingroom/book_by_schedule";
        private const string BookByMeetingPath = "/cgi-bin/oa/meetingroom/book_by_meeting";
        private const string CancelBookPath = "/cgi-bin/oa/meetingroom/cancel_book";
        private const string GetBookInfoPath = "/cgi-bin/oa/meetingroom/bookinfo/get";

        /// <summary>添加会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室名称、容量、位置、设备和使用范围。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新会议室 ID。</returns>
        public static AddMeetingRoomResult AddMeetingRoom(string accessTokenOrAppKey,
            AddMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => Post<AddMeetingRoomResult>(accessTokenOrAppKey, AddPath, request, timeOut);

        /// <summary>异步添加会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室名称、容量、位置、设备和使用范围。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新会议室 ID。</returns>
        public static Task<AddMeetingRoomResult> AddMeetingRoomAsync(string accessTokenOrAppKey,
            AddMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<AddMeetingRoomResult>(accessTokenOrAppKey, AddPath, request, timeOut);

        /// <summary>查询会议室列表。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">可选的位置和设备筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>会议室列表。</returns>
        public static GetMeetingRoomListResult GetMeetingRoomList(string accessTokenOrAppKey,
            GetMeetingRoomListRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomListResult>(accessTokenOrAppKey, ListPath, request, timeOut);

        /// <summary>异步查询会议室列表。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">可选的位置和设备筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>会议室列表。</returns>
        public static Task<GetMeetingRoomListResult> GetMeetingRoomListAsync(string accessTokenOrAppKey,
            GetMeetingRoomListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomListResult>(accessTokenOrAppKey, ListPath, request, timeOut);

        /// <summary>更新会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室 ID 和需要替换的字段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult UpdateMeetingRoom(string accessTokenOrAppKey,
            UpdateMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, EditPath, request, timeOut);

        /// <summary>异步更新会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室 ID 和需要替换的字段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> UpdateMeetingRoomAsync(string accessTokenOrAppKey,
            UpdateMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, EditPath, request, timeOut);

        /// <summary>删除会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">需要删除的会议室 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteMeetingRoom(string accessTokenOrAppKey,
            DeleteMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeletePath, request, timeOut);

        /// <summary>异步删除会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">需要删除的会议室 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteMeetingRoomAsync(string accessTokenOrAppKey,
            DeleteMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeletePath, request, timeOut);

        /// <summary>查询指定时间段的会议室预定信息；官方不支持跨天查询。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室、时间段或位置筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>会议室及其预定日程列表。</returns>
        public static GetMeetingRoomBookingInfoResult GetMeetingRoomBookingInfo(string accessTokenOrAppKey,
            GetMeetingRoomBookingInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomBookingInfoResult>(accessTokenOrAppKey, GetBookingInfoPath, request, timeOut);

        /// <summary>异步查询指定时间段的会议室预定信息；官方不支持跨天查询。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室、时间段或位置筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>会议室及其预定日程列表。</returns>
        public static Task<GetMeetingRoomBookingInfoResult> GetMeetingRoomBookingInfoAsync(
            string accessTokenOrAppKey, GetMeetingRoomBookingInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomBookingInfoResult>(accessTokenOrAppKey, GetBookingInfoPath, request, timeOut);

        /// <summary>
        /// 根据会议 ID 查询指定会议室的预定信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93620"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室 ID 和同一应用创建的会议 ID。</param>
        /// <param name="timeOut">代理请求超时时间，单位为毫秒。</param>
        /// <returns>会议室 ID 及与会议关联的预定排期。</returns>
        public static GetMeetingRoomBookingInfoByMeetingIdResult GetMeetingRoomBookingInfoByMeetingId(
            string accessTokenOrAppKey, GetMeetingRoomBookingInfoByMeetingIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomBookingInfoByMeetingIdResult>(accessTokenOrAppKey,
                GetBookingInfoByMeetingIdPath, request, timeOut);

        /// <summary>
        /// 异步根据会议 ID 查询指定会议室的预定信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93620"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室 ID 和同一应用创建的会议 ID。</param>
        /// <param name="timeOut">代理请求超时时间，单位为毫秒。</param>
        /// <returns>会议室 ID 及与会议关联的预定排期。</returns>
        public static Task<GetMeetingRoomBookingInfoByMeetingIdResult> GetMeetingRoomBookingInfoByMeetingIdAsync(
            string accessTokenOrAppKey, GetMeetingRoomBookingInfoByMeetingIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomBookingInfoByMeetingIdResult>(accessTokenOrAppKey,
                GetBookingInfoByMeetingIdPath, request, timeOut);

        /// <summary>预定无需审批的会议室并自动关联日程。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室、时间、预定人和参与人。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预定 ID 和关联日程 ID。</returns>
        public static BookMeetingRoomResult BookMeetingRoom(string accessTokenOrAppKey,
            BookMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => Post<BookMeetingRoomResult>(accessTokenOrAppKey, BookPath, request, timeOut);

        /// <summary>异步预定无需审批的会议室并自动关联日程。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室、时间、预定人和参与人。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预定 ID 和关联日程 ID。</returns>
        public static Task<BookMeetingRoomResult> BookMeetingRoomAsync(string accessTokenOrAppKey,
            BookMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<BookMeetingRoomResult>(accessTokenOrAppKey, BookPath, request, timeOut);

        /// <summary>为同一应用创建的日程预定会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室、日程 ID 和预定人。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预定 ID 和重复日程冲突日期。</returns>
        public static BookMeetingRoomForRecurringResult BookMeetingRoomBySchedule(string accessTokenOrAppKey,
            BookMeetingRoomByScheduleRequest request, int timeOut = Config.TIME_OUT)
            => Post<BookMeetingRoomForRecurringResult>(accessTokenOrAppKey, BookBySchedulePath, request, timeOut);

        /// <summary>异步为同一应用创建的日程预定会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室、日程 ID 和预定人。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预定 ID 和重复日程冲突日期。</returns>
        public static Task<BookMeetingRoomForRecurringResult> BookMeetingRoomByScheduleAsync(
            string accessTokenOrAppKey, BookMeetingRoomByScheduleRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<BookMeetingRoomForRecurringResult>(accessTokenOrAppKey, BookBySchedulePath, request, timeOut);

        /// <summary>为同一应用创建的会议预定会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室、会议 ID 和预定人。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预定 ID 和重复会议冲突日期。</returns>
        public static BookMeetingRoomForRecurringResult BookMeetingRoomByMeeting(string accessTokenOrAppKey,
            BookMeetingRoomByMeetingRequest request, int timeOut = Config.TIME_OUT)
            => Post<BookMeetingRoomForRecurringResult>(accessTokenOrAppKey, BookByMeetingPath, request, timeOut);

        /// <summary>异步为同一应用创建的会议预定会议室。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室、会议 ID 和预定人。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预定 ID 和重复会议冲突日期。</returns>
        public static Task<BookMeetingRoomForRecurringResult> BookMeetingRoomByMeetingAsync(
            string accessTokenOrAppKey, BookMeetingRoomByMeetingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<BookMeetingRoomForRecurringResult>(accessTokenOrAppKey, BookByMeetingPath, request, timeOut);

        /// <summary>取消会议室预定。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">预定 ID、日程保留策略和可选的重复预定日期。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult CancelMeetingRoomBooking(string accessTokenOrAppKey,
            CancelMeetingRoomBookingRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, CancelBookPath, request, timeOut);

        /// <summary>异步取消会议室预定。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">预定 ID、日程保留策略和可选的重复预定日期。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> CancelMeetingRoomBookingAsync(string accessTokenOrAppKey,
            CancelMeetingRoomBookingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, CancelBookPath, request, timeOut);

        /// <summary>根据会议室 ID 和预定 ID 查询预定详情。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室 ID 和预定 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>会议室预定详情。</returns>
        public static GetMeetingRoomBookingDetailResult GetMeetingRoomBookingDetail(string accessTokenOrAppKey,
            GetMeetingRoomBookingDetailRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomBookingDetailResult>(accessTokenOrAppKey, GetBookInfoPath, request, timeOut);

        /// <summary>异步根据会议室 ID 和预定 ID 查询预定详情。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">会议室 ID 和预定 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>会议室预定详情。</returns>
        public static Task<GetMeetingRoomBookingDetailResult> GetMeetingRoomBookingDetailAsync(
            string accessTokenOrAppKey, GetMeetingRoomBookingDetailRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomBookingDetailResult>(accessTokenOrAppKey, GetBookInfoPath, request, timeOut);

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
