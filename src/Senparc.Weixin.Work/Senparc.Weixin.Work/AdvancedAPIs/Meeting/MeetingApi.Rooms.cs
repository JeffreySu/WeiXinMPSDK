/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Rooms.cs
    文件功能描述：企业微信会议 Rooms 会议室管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议 Rooms 预定、设备、控制器及呼叫接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string BookMeetingRoomsPath = "/cgi-bin/meeting/rooms/book";
        private const string ReleaseMeetingRoomsPath = "/cgi-bin/meeting/rooms/release";
        private const string GetMeetingRoomsPath = "/cgi-bin/meeting/rooms/list";
        private const string GetMeetingRoomInfoPath = "/cgi-bin/meeting/rooms/get_info";
        private const string GetMeetingRoomConfigPath = "/cgi-bin/meeting/rooms/get_config";
        private const string GetMeetingRoomMeetingsPath = "/cgi-bin/meeting/rooms/list_meetings";
        private const string GetMeetingRoomDevicesPath = "/cgi-bin/meeting/rooms/list_devices";
        private const string GetMeetingRoomControllersPath =
            "/cgi-bin/meeting/rooms/list_controllers";
        private const string GetMeetingRoomInventoryPath =
            "/cgi-bin/meeting/rooms/get_inventory";
        private const string CallMeetingRoomPath = "/cgi-bin/meeting/rooms/call";
        private const string CancelMeetingRoomCallPath = "/cgi-bin/meeting/rooms/cancel_call";
        private const string GetMeetingRoomResponseStatusPath =
            "/cgi-bin/meeting/rooms/get_response_status";

        /// <summary>
        /// 为指定会议预定一个或多个 Rooms 会议室。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98791"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、会议室 ID 列表及主题展示设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功预定的会议室信息列表。</returns>
        public static BookMeetingRoomsResult BookMeetingRooms(string accessTokenOrAppKey,
            BookMeetingRoomsRequest request, int timeOut = Config.TIME_OUT)
            => Post<BookMeetingRoomsResult>(accessTokenOrAppKey, BookMeetingRoomsPath,
                request, timeOut);

        /// <summary>
        /// 异步为指定会议预定一个或多个 Rooms 会议室。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98791"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、会议室 ID 列表及主题展示设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功预定的会议室信息列表。</returns>
        public static Task<BookMeetingRoomsResult> BookMeetingRoomsAsync(string accessTokenOrAppKey,
            BookMeetingRoomsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<BookMeetingRoomsResult>(accessTokenOrAppKey, BookMeetingRoomsPath,
                request, timeOut);

        /// <summary>
        /// 释放指定会议已经预定的 Rooms 会议室。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98792"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要释放的会议室 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>释放结果。</returns>
        public static WorkJsonResult ReleaseMeetingRooms(string accessTokenOrAppKey,
            ReleaseMeetingRoomsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ReleaseMeetingRoomsPath, request, timeOut);

        /// <summary>
        /// 异步释放指定会议已经预定的 Rooms 会议室。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98792"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要释放的会议室 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>释放结果。</returns>
        public static Task<WorkJsonResult> ReleaseMeetingRoomsAsync(string accessTokenOrAppKey,
            ReleaseMeetingRoomsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ReleaseMeetingRoomsPath,
                request, timeOut);

        /// <summary>
        /// 分页获取企业的 Rooms 会议室列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98795"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议室名称筛选和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议室列表、分页状态及下一页游标。</returns>
        public static GetMeetingRoomsResult GetMeetingRooms(string accessTokenOrAppKey,
            GetMeetingRoomsRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomsResult>(accessTokenOrAppKey, GetMeetingRoomsPath,
                request, timeOut);

        /// <summary>
        /// 异步分页获取企业的 Rooms 会议室列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98795"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议室名称筛选和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议室列表、分页状态及下一页游标。</returns>
        public static Task<GetMeetingRoomsResult> GetMeetingRoomsAsync(string accessTokenOrAppKey,
            GetMeetingRoomsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomsResult>(accessTokenOrAppKey, GetMeetingRoomsPath,
                request, timeOut);

        /// <summary>
        /// 获取指定 Rooms 会议室的基础、账号、硬件和 PMI 信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98793"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的会议室 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议室详情、监控状态、预定状态及呼叫能力。</returns>
        public static GetMeetingRoomInfoResult GetMeetingRoomInfo(string accessTokenOrAppKey,
            GetMeetingRoomInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomInfoResult>(accessTokenOrAppKey, GetMeetingRoomInfoPath,
                request, timeOut);

        /// <summary>
        /// 异步获取指定 Rooms 会议室的基础、账号、硬件和 PMI 信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98793"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的会议室 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议室详情、监控状态、预定状态及呼叫能力。</returns>
        public static Task<GetMeetingRoomInfoResult> GetMeetingRoomInfoAsync(
            string accessTokenOrAppKey, GetMeetingRoomInfoRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomInfoResult>(accessTokenOrAppKey, GetMeetingRoomInfoPath,
                request, timeOut);

        /// <summary>
        /// 获取指定 Rooms 会议室的会议与录制配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98802"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的会议室 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议设置和云录制设置。</returns>
        public static GetMeetingRoomConfigResult GetMeetingRoomConfig(string accessTokenOrAppKey,
            GetMeetingRoomConfigRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomConfigResult>(accessTokenOrAppKey, GetMeetingRoomConfigPath,
                request, timeOut);

        /// <summary>
        /// 异步获取指定 Rooms 会议室的会议与录制配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98802"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的会议室 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议设置和云录制设置。</returns>
        public static Task<GetMeetingRoomConfigResult> GetMeetingRoomConfigAsync(
            string accessTokenOrAppKey, GetMeetingRoomConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomConfigResult>(accessTokenOrAppKey, GetMeetingRoomConfigPath,
                request, timeOut);

        /// <summary>
        /// 分页获取指定 Rooms 会议室的会议列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98796"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议室或 Rooms ID、时间范围和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议列表、分页状态及下一页游标。</returns>
        public static GetMeetingRoomMeetingsResult GetMeetingRoomMeetings(
            string accessTokenOrAppKey, GetMeetingRoomMeetingsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomMeetingsResult>(accessTokenOrAppKey,
                GetMeetingRoomMeetingsPath, request, timeOut);

        /// <summary>
        /// 异步分页获取指定 Rooms 会议室的会议列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98796"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议室或 Rooms ID、时间范围和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议列表、分页状态及下一页游标。</returns>
        public static Task<GetMeetingRoomMeetingsResult> GetMeetingRoomMeetingsAsync(
            string accessTokenOrAppKey, GetMeetingRoomMeetingsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomMeetingsResult>(accessTokenOrAppKey,
                GetMeetingRoomMeetingsPath, request, timeOut);

        /// <summary>
        /// 分页获取 Rooms 会议室设备列表及健康状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98798"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议室名称筛选和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>设备列表、健康信息及分页数据。</returns>
        public static GetMeetingRoomDevicesResult GetMeetingRoomDevices(string accessTokenOrAppKey,
            GetMeetingRoomDevicesRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomDevicesResult>(accessTokenOrAppKey, GetMeetingRoomDevicesPath,
                request, timeOut);

        /// <summary>
        /// 异步分页获取 Rooms 会议室设备列表及健康状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98798"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议室名称筛选和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>设备列表、健康信息及分页数据。</returns>
        public static Task<GetMeetingRoomDevicesResult> GetMeetingRoomDevicesAsync(
            string accessTokenOrAppKey, GetMeetingRoomDevicesRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomDevicesResult>(accessTokenOrAppKey,
                GetMeetingRoomDevicesPath, request, timeOut);

        /// <summary>
        /// 分页获取 Rooms 会议室控制器列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98799"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">控制器名称筛选和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>控制器硬件、网络、版本及分页信息。</returns>
        public static GetMeetingRoomControllersResult GetMeetingRoomControllers(
            string accessTokenOrAppKey, GetMeetingRoomControllersRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomControllersResult>(accessTokenOrAppKey,
                GetMeetingRoomControllersPath, request, timeOut);

        /// <summary>
        /// 异步分页获取 Rooms 会议室控制器列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98799"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">控制器名称筛选和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>控制器硬件、网络、版本及分页信息。</returns>
        public static Task<GetMeetingRoomControllersResult> GetMeetingRoomControllersAsync(
            string accessTokenOrAppKey, GetMeetingRoomControllersRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomControllersResult>(accessTokenOrAppKey,
                GetMeetingRoomControllersPath, request, timeOut);

        /// <summary>
        /// 获取企业 Rooms 会议室账号库存和使用情况。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98809"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议室库存查询请求。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>普通及专款账号的总数、使用数和过期数。</returns>
        public static GetMeetingRoomInventoryResult GetMeetingRoomInventory(
            string accessTokenOrAppKey, GetMeetingRoomInventoryRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomInventoryResult>(accessTokenOrAppKey,
                GetMeetingRoomInventoryPath, request, timeOut);

        /// <summary>
        /// 异步获取企业 Rooms 会议室账号库存和使用情况。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98809"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议室库存查询请求。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>普通及专款账号的总数、使用数和过期数。</returns>
        public static Task<GetMeetingRoomInventoryResult> GetMeetingRoomInventoryAsync(
            string accessTokenOrAppKey, GetMeetingRoomInventoryRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomInventoryResult>(accessTokenOrAppKey,
                GetMeetingRoomInventoryPath, request, timeOut);

        /// <summary>
        /// 从会议中呼叫指定 Rooms 会议室或 MRA 地址。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98804"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID，以及会议室 ID 或 MRA 地址。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>用于取消呼叫的邀请 ID。</returns>
        public static CallMeetingRoomResult CallMeetingRoom(string accessTokenOrAppKey,
            CallMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => Post<CallMeetingRoomResult>(accessTokenOrAppKey, CallMeetingRoomPath,
                request, timeOut);

        /// <summary>
        /// 异步从会议中呼叫指定 Rooms 会议室或 MRA 地址。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98804"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID，以及会议室 ID 或 MRA 地址。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>用于取消呼叫的邀请 ID。</returns>
        public static Task<CallMeetingRoomResult> CallMeetingRoomAsync(string accessTokenOrAppKey,
            CallMeetingRoomRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<CallMeetingRoomResult>(accessTokenOrAppKey, CallMeetingRoomPath,
                request, timeOut);

        /// <summary>
        /// 取消对指定 Rooms 会议室或 MRA 地址的呼叫。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98805"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、邀请 ID，以及会议室 ID 或 MRA 地址。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>取消呼叫结果。</returns>
        public static WorkJsonResult CancelMeetingRoomCall(string accessTokenOrAppKey,
            CancelMeetingRoomCallRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, CancelMeetingRoomCallPath,
                request, timeOut);

        /// <summary>
        /// 异步取消对指定 Rooms 会议室或 MRA 地址的呼叫。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98805"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、邀请 ID，以及会议室 ID 或 MRA 地址。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>取消呼叫结果。</returns>
        public static Task<WorkJsonResult> CancelMeetingRoomCallAsync(string accessTokenOrAppKey,
            CancelMeetingRoomCallRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, CancelMeetingRoomCallPath,
                request, timeOut);

        /// <summary>
        /// 获取指定 Rooms 会议室或 MRA 地址最近一次呼叫应答状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98806"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID，以及会议室 ID 或 MRA 地址。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>应答状态和最近应答时间。</returns>
        public static GetMeetingRoomResponseStatusResult GetMeetingRoomResponseStatus(
            string accessTokenOrAppKey, GetMeetingRoomResponseStatusRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRoomResponseStatusResult>(accessTokenOrAppKey,
                GetMeetingRoomResponseStatusPath, request, timeOut);

        /// <summary>
        /// 异步获取指定 Rooms 会议室或 MRA 地址最近一次呼叫应答状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98806"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID，以及会议室 ID 或 MRA 地址。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>应答状态和最近应答时间。</returns>
        public static Task<GetMeetingRoomResponseStatusResult> GetMeetingRoomResponseStatusAsync(
            string accessTokenOrAppKey, GetMeetingRoomResponseStatusRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRoomResponseStatusResult>(accessTokenOrAppKey,
                GetMeetingRoomResponseStatusPath, request, timeOut);
    }
}
