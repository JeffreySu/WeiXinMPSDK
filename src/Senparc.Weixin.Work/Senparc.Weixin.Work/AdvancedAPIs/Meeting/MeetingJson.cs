/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingJson.cs
    文件功能描述：企业微信会议基础管理强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 补齐会议创建、更新和成员会议列表模型；兼容会议详情中数字字符串形式的重复结束时间

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 会议受邀成员和受邀设备。
    /// </summary>
    public class MeetingInvitees
    {
        /// <summary>
        /// 获取或设置企业成员 UserId 列表。
        /// </summary>
        public IList<string> userid { get; set; }

        /// <summary>
        /// 获取或设置会议设备序列号列表。
        /// </summary>
        public IList<string> device_sn { get; set; }
    }

    /// <summary>
    /// 会议外部嘉宾。
    /// </summary>
    public class MeetingGuest
    {
        /// <summary>
        /// 获取或设置国家或地区代码。
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 获取或设置电话号码。
        /// </summary>
        public string phone_number { get; set; }

        /// <summary>
        /// 获取或设置嘉宾姓名。
        /// </summary>
        public string guest_name { get; set; }
    }

    /// <summary>
    /// 会议主持人或响铃成员集合。
    /// </summary>
    public class MeetingUserGroup
    {
        /// <summary>
        /// 获取或设置企业成员 UserId 列表。
        /// </summary>
        public IList<string> userid { get; set; }
    }

    /// <summary>
    /// 会议权限、录制和入会设置。
    /// </summary>
    public class MeetingSettings
    {
        /// <summary>
        /// 获取或设置入会密码。
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 获取或设置是否开启等候室。
        /// </summary>
        public bool? enable_waiting_room { get; set; }

        /// <summary>
        /// 获取或设置是否允许成员在主持人入会前加入。
        /// </summary>
        public bool? allow_enter_before_host { get; set; }

        /// <summary>
        /// 获取或设置是否允许企业外部成员入会。
        /// </summary>
        public bool? allow_external_user { get; set; }

        /// <summary>
        /// 获取或设置成员入会时的静音模式。
        /// </summary>
        public int? enable_enter_mute { get; set; }

        /// <summary>
        /// 获取或设置是否允许参会成员自行解除静音。
        /// </summary>
        public bool? allow_unmute_self { get; set; }

        /// <summary>
        /// 获取或设置是否全体静音。
        /// </summary>
        public bool? mute_all { get; set; }

        /// <summary>
        /// 获取或设置是否开启屏幕水印。
        /// </summary>
        public bool? enable_screen_watermark { get; set; }

        /// <summary>
        /// 获取或设置水印样式。
        /// </summary>
        public int? watermark_type { get; set; }

        /// <summary>
        /// 获取或设置自动录制类型。
        /// </summary>
        public string auto_record_type { get; set; }

        /// <summary>
        /// 获取或设置是否在参会成员入会时立即开始云录制。
        /// </summary>
        public bool? attendee_join_auto_record { get; set; }

        /// <summary>
        /// 获取或设置是否允许主持人暂停或停止云录制。
        /// </summary>
        public bool? enable_host_pause_auto_record { get; set; }

        /// <summary>
        /// 获取或设置是否开启同声传译。
        /// </summary>
        public bool? enable_interpreter { get; set; }

        /// <summary>
        /// 获取或设置是否允许成员上传文档。
        /// </summary>
        public bool? enable_doc_upload_permission { get; set; }

        /// <summary>
        /// 获取或设置是否开启会议报名。
        /// </summary>
        public bool? enable_enroll { get; set; }

        /// <summary>
        /// 获取或设置是否开启主持人密钥。
        /// </summary>
        public bool? enable_host_key { get; set; }

        /// <summary>
        /// 获取或设置主持人密钥。
        /// </summary>
        public string host_key { get; set; }

        /// <summary>
        /// 获取或设置会议开始提醒范围。
        /// </summary>
        public int? remind_scope { get; set; }

        /// <summary>
        /// 获取或设置会议主持人成员。
        /// </summary>
        public MeetingUserGroup hosts { get; set; }

        /// <summary>
        /// 获取或设置需要响铃提醒的成员。
        /// </summary>
        public MeetingUserGroup ring_users { get; set; }
    }

    /// <summary>
    /// 会议重复和提醒规则。
    /// </summary>
    public class MeetingReminder
    {
        /// <summary>
        /// 获取或设置是否为重复会议，取值为 0 或 1。
        /// </summary>
        public int? is_repeat { get; set; }

        /// <summary>
        /// 获取或设置重复类型。
        /// </summary>
        public int? repeat_type { get; set; }

        /// <summary>
        /// 获取或设置是否使用自定义重复规则，取值为 0 或 1。
        /// </summary>
        public int? is_custom_repeat { get; set; }

        /// <summary>
        /// 获取或设置重复结束类型。
        /// </summary>
        public int? repeat_until_type { get; set; }

        /// <summary>
        /// 获取或设置周期会议限定次数。
        /// </summary>
        public int? repeat_until_count { get; set; }

        /// <summary>
        /// 获取或设置重复结束 Unix 时间戳。
        /// </summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? repeat_until { get; set; }

        /// <summary>
        /// 获取或设置重复间隔。
        /// </summary>
        public int? repeat_interval { get; set; }

        /// <summary>
        /// 获取或设置每周重复的星期列表。
        /// </summary>
        public IList<int> repeat_day_of_week { get; set; }

        /// <summary>
        /// 获取或设置每月重复的日期列表。
        /// </summary>
        public IList<int> repeat_day_of_month { get; set; }

        /// <summary>
        /// 获取或设置会议开始前的提醒秒数列表。
        /// </summary>
        public IList<int> remind_before { get; set; }
    }

    /// <summary>
    /// 创建企业微信会议请求。
    /// </summary>
    public class CreateMeetingRequest
    {
        /// <summary>
        /// 获取或设置会议管理员 UserId。
        /// </summary>
        public string admin_userid { get; set; }

        /// <summary>
        /// 获取或设置会议标题。
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 获取或设置会议开始 Unix 时间戳。
        /// </summary>
        public long meeting_start { get; set; }

        /// <summary>
        /// 获取或设置会议持续时长，单位为秒。
        /// </summary>
        public int meeting_duration { get; set; }

        /// <summary>
        /// 获取或设置会议描述。
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 获取或设置会议地点。
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 获取或设置企业微信日历 ID。
        /// </summary>
        public string cal_id { get; set; }

        /// <summary>
        /// 获取或设置受邀成员和会议设备。
        /// </summary>
        public MeetingInvitees invitees { get; set; }

        /// <summary>
        /// 获取或设置外部嘉宾列表。
        /// </summary>
        public IList<MeetingGuest> guests { get; set; }

        /// <summary>
        /// 获取或设置会议权限、录制和入会设置。
        /// </summary>
        public MeetingSettings settings { get; set; }

        /// <summary>
        /// 获取或设置会议重复和提醒规则。
        /// </summary>
        public MeetingReminder reminders { get; set; }

        /// <summary>
        /// 获取或设置授权方安装的应用 AgentId。
        /// </summary>
        public int? agentid { get; set; }
    }

    /// <summary>
    /// 创建企业微信会议结果。
    /// </summary>
    public class CreateMeetingResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }

        /// <summary>
        /// 获取或设置会议号。
        /// </summary>
        public string meeting_code { get; set; }

        /// <summary>
        /// 获取或设置会议链接。
        /// </summary>
        public string meeting_link { get; set; }

        /// <summary>
        /// 获取或设置超出高级会议账号范围的 UserId 列表。
        /// </summary>
        public IList<string> excess_users { get; set; }
    }

    /// <summary>
    /// 更新企业微信会议请求。
    /// </summary>
    public class UpdateMeetingRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }

        /// <summary>
        /// 获取或设置会议标题。
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 获取或设置会议开始 Unix 时间戳。
        /// </summary>
        public long? meeting_start { get; set; }

        /// <summary>
        /// 获取或设置会议持续时长，单位为秒。
        /// </summary>
        public int? meeting_duration { get; set; }

        /// <summary>
        /// 获取或设置会议描述。
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 获取或设置会议地点。
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 获取或设置企业微信日历 ID。
        /// </summary>
        public string cal_id { get; set; }

        /// <summary>
        /// 获取或设置受邀成员和会议设备。
        /// </summary>
        public MeetingInvitees invitees { get; set; }

        /// <summary>
        /// 获取或设置外部嘉宾列表。
        /// </summary>
        public IList<MeetingGuest> guests { get; set; }

        /// <summary>
        /// 获取或设置会议权限、录制和入会设置。
        /// </summary>
        public MeetingSettings settings { get; set; }

        /// <summary>
        /// 获取或设置会议重复和提醒规则。
        /// </summary>
        public MeetingReminder reminders { get; set; }

        /// <summary>
        /// 获取或设置授权方安装的应用 AgentId。
        /// </summary>
        public int? agentid { get; set; }
    }

    /// <summary>
    /// 更新企业微信会议结果。
    /// </summary>
    public class UpdateMeetingResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置超出高级会议账号范围的 UserId 列表。
        /// </summary>
        public IList<string> excess_users { get; set; }
    }

    /// <summary>
    /// 获取成员会议 ID 列表请求。
    /// </summary>
    public class GetUserMeetingIdsRequest
    {
        /// <summary>
        /// 获取或设置成员 UserId。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 获取或设置筛选开始 Unix 时间戳。
        /// </summary>
        public long? begin_time { get; set; }

        /// <summary>
        /// 获取或设置筛选结束 Unix 时间戳。
        /// </summary>
        public long? end_time { get; set; }

        /// <summary>
        /// 获取或设置分页游标；首次请求默认为 <c>0</c>。
        /// </summary>
        public string cursor { get; set; } = "0";

        /// <summary>
        /// 获取或设置每页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 获取成员会议 ID 列表结果。
    /// </summary>
    public class GetUserMeetingIdsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置会议 ID 列表。
        /// </summary>
        public IList<string> meetingid_list { get; set; }

        /// <summary>
        /// 获取或设置下一页游标；为空表示没有下一页。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 会议受邀成员。
    /// </summary>
    public class MeetingInvitee
    {
        /// <summary>
        /// 获取或设置企业成员 UserId。
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 获取会议受邀成员请求。
    /// </summary>
    public class GetMeetingInviteesRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }

        /// <summary>
        /// 获取或设置分页游标；首次请求可不填。
        /// </summary>
        public string cursor { get; set; }
    }

    /// <summary>
    /// 获取会议受邀成员结果。
    /// </summary>
    public class GetMeetingInviteesResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置受邀成员列表。
        /// </summary>
        public IList<MeetingInvitee> invitees { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 获取或设置下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 设置会议受邀成员请求。
    /// </summary>
    public class SetMeetingInviteesRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }

        /// <summary>
        /// 获取或设置替换后的受邀成员列表。
        /// </summary>
        public IList<MeetingInvitee> invitees { get; set; }
    }

    /// <summary>
    /// 设置会议受邀成员结果。
    /// </summary>
    public class SetMeetingInviteesResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 用户专属参会短链接和自定义数据。
    /// </summary>
    public class MeetingCustomerShortUrlData
    {
        /// <summary>
        /// 获取或设置用户专属参会短链接。
        /// </summary>
        public string meeting_short_url { get; set; }

        /// <summary>
        /// 获取或设置调用方透传的用户自定义数据。
        /// </summary>
        public string customer_data { get; set; }
    }

    /// <summary>
    /// 创建用户专属参会短链接请求。
    /// </summary>
    public class CreateMeetingCustomerShortUrlRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }

        /// <summary>
        /// 获取或设置需要原样透传的用户自定义数据。
        /// </summary>
        public string customer_data { get; set; }
    }

    /// <summary>
    /// 创建用户专属参会短链接结果。
    /// </summary>
    public class CreateMeetingCustomerShortUrlResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置专属参会短链接及对应自定义数据。
        /// </summary>
        public MeetingCustomerShortUrlData meeting_short_url_customer_data { get; set; }
    }

    /// <summary>
    /// 获取用户专属参会短链接请求。
    /// </summary>
    public class GetMeetingCustomerShortUrlsRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 获取用户专属参会短链接结果。
    /// </summary>
    public class GetMeetingCustomerShortUrlsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置专属参会短链接及自定义数据列表。
        /// </summary>
        public IList<MeetingCustomerShortUrlData> meeting_short_url_customer_data_list { get; set; }
    }

    /// <summary>
    /// 获取实时参会成员请求。
    /// </summary>
    public class GetMeetingRealtimeAttendeesRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }

        /// <summary>
        /// 获取或设置周期会议中的子会议 ID。
        /// </summary>
        public string sub_meetingid { get; set; }

        /// <summary>
        /// 获取或设置分页游标。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 获取或设置每页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 会议中的实时参会成员和终端状态。
    /// </summary>
    public class MeetingRealtimeAttendee
    {
        /// <summary>
        /// 获取或设置企业成员 UserId。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 获取或设置外部成员临时 OpenId。
        /// </summary>
        public string tmp_openid { get; set; }

        /// <summary>
        /// 获取或设置终端设备类型。
        /// </summary>
        public int instance_id { get; set; }

        /// <summary>
        /// 获取或设置成员角色。
        /// </summary>
        public int role { get; set; }

        /// <summary>
        /// 获取或设置入会方式。
        /// </summary>
        public int join_type { get; set; }

        /// <summary>
        /// 获取或设置加入会议 Unix 时间戳；兼容官方响应中的数字字符串。
        /// </summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long join_time { get; set; }

        /// <summary>
        /// 获取或设置是否开启麦克风。
        /// </summary>
        public bool audio_state { get; set; }

        /// <summary>
        /// 获取或设置是否开启摄像头。
        /// </summary>
        public bool video_state { get; set; }

        /// <summary>
        /// 获取或设置是否正在共享屏幕。
        /// </summary>
        public bool screen_shared_state { get; set; }
    }

    /// <summary>
    /// 获取实时参会成员结果。
    /// </summary>
    public class GetMeetingRealtimeAttendeesResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置实时参会成员列表。
        /// </summary>
        public IList<MeetingRealtimeAttendee> attendees { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 获取或设置下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取历史参会成员请求。
    /// </summary>
    public class GetMeetingAttendeesRequest : GetMeetingRealtimeAttendeesRequest
    {
        /// <summary>
        /// 获取或设置参会时间范围的开始 Unix 时间戳。
        /// </summary>
        public long? start_time { get; set; }

        /// <summary>
        /// 获取或设置参会时间范围的结束 Unix 时间戳。
        /// </summary>
        public long? end_time { get; set; }
    }

    /// <summary>
    /// 历史参会成员明细。
    /// </summary>
    public class MeetingAttendee : MeetingRealtimeAttendee
    {
        /// <summary>
        /// 获取或设置离开会议 Unix 时间戳；兼容官方响应中的数字字符串。
        /// </summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? quit_time { get; set; }

        /// <summary>
        /// 获取或设置网络类型。
        /// </summary>
        public string net { get; set; }

        /// <summary>
        /// 获取或设置网络研讨会成员角色。
        /// </summary>
        public int? webinar_role { get; set; }

        /// <summary>
        /// 获取或设置专属参会链接关联的自定义数据。
        /// </summary>
        public string customer_data { get; set; }
    }

    /// <summary>
    /// 获取历史参会成员结果。
    /// </summary>
    public class GetMeetingAttendeesResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置历史参会成员列表。
        /// </summary>
        public IList<MeetingAttendee> attendees { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 获取或设置下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取会议等候室成员请求。
    /// </summary>
    public class GetMeetingWaitingRoomUsersRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }

        /// <summary>
        /// 获取或设置分页游标。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 获取或设置每页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 当前位于会议等候室的成员。
    /// </summary>
    public class MeetingWaitingRoomCurrentUser
    {
        /// <summary>
        /// 获取或设置企业成员 UserId。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 获取或设置外部成员临时 OpenId。
        /// </summary>
        public string tmp_openid { get; set; }

        /// <summary>
        /// 获取或设置终端设备类型。
        /// </summary>
        public int instance_id { get; set; }

        /// <summary>
        /// 获取或设置专属参会链接关联的自定义数据。
        /// </summary>
        public string customer_data { get; set; }
    }

    /// <summary>
    /// 获取当前等候室成员结果。
    /// </summary>
    public class GetCurrentMeetingWaitingRoomUsersResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置当前等候室成员列表。
        /// </summary>
        public IList<MeetingWaitingRoomCurrentUser> user_list { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 获取或设置下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 会议等候室成员的进入和离开记录。
    /// </summary>
    public class MeetingWaitingRoomUser
    {
        /// <summary>
        /// 获取或设置企业成员 UserId。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 获取或设置外部成员临时 OpenId。
        /// </summary>
        public string tmp_openid { get; set; }

        /// <summary>
        /// 获取或设置终端设备类型。
        /// </summary>
        public int instance_id { get; set; }

        /// <summary>
        /// 获取或设置进入等候室 Unix 时间戳；兼容官方响应中的数字字符串。
        /// </summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long join_time { get; set; }

        /// <summary>
        /// 获取或设置离开等候室 Unix 时间戳；兼容官方响应中的数字字符串。
        /// </summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? quit_time { get; set; }
    }

    /// <summary>
    /// 获取等候室成员记录结果。
    /// </summary>
    public class GetMeetingWaitingRoomUsersResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置等候室成员记录列表。
        /// </summary>
        public IList<MeetingWaitingRoomUser> user_list { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 获取或设置下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取会议质量数据请求。
    /// </summary>
    public class GetMeetingQualityRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        public string meetingid { get; set; }

        /// <summary>
        /// 获取或设置周期会议中的子会议 ID。
        /// </summary>
        public string sub_meetingid { get; set; }

        /// <summary>
        /// 获取或设置参会时间范围的开始 Unix 时间戳。
        /// </summary>
        public long? start_time { get; set; }

        /// <summary>
        /// 获取或设置分页游标。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 获取或设置每页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 会议音视频、共享屏幕和网络质量指标。
    /// </summary>
    public class MeetingQualityMetrics
    {
        /// <summary>
        /// 获取或设置综合健康度。
        /// </summary>
        public int quality { get; set; }

        /// <summary>
        /// 获取或设置音频质量。
        /// </summary>
        public int audio_quality { get; set; }

        /// <summary>
        /// 获取或设置视频质量。
        /// </summary>
        public int video_quality { get; set; }

        /// <summary>
        /// 获取或设置共享屏幕质量。
        /// </summary>
        public int screen_share_quality { get; set; }

        /// <summary>
        /// 获取或设置网络质量。
        /// </summary>
        public int network_quality { get; set; }

        /// <summary>
        /// 获取或设置具体告警问题列表。
        /// </summary>
        public IList<string> problems { get; set; }
    }

    /// <summary>
    /// 单个参会成员的会议质量指标。
    /// </summary>
    public class MeetingQualityAttendee : MeetingQualityMetrics
    {
        /// <summary>
        /// 获取或设置企业成员 UserId。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 获取或设置外部成员临时 OpenId。
        /// </summary>
        public string tmp_openid { get; set; }

        /// <summary>
        /// 获取或设置终端设备类型。
        /// </summary>
        public int instance_id { get; set; }
    }

    /// <summary>
    /// 获取会议质量数据结果。
    /// </summary>
    public class GetMeetingQualityResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置会议综合健康度。
        /// </summary>
        public int quality { get; set; }

        /// <summary>
        /// 获取或设置会议音频质量。
        /// </summary>
        public int audio_quality { get; set; }

        /// <summary>
        /// 获取或设置会议视频质量。
        /// </summary>
        public int video_quality { get; set; }

        /// <summary>
        /// 获取或设置会议共享屏幕质量。
        /// </summary>
        public int screen_share_quality { get; set; }

        /// <summary>
        /// 获取或设置会议网络质量。
        /// </summary>
        public int network_quality { get; set; }

        /// <summary>
        /// 获取或设置会议具体告警问题列表。
        /// </summary>
        public IList<string> problems { get; set; }

        /// <summary>
        /// 获取或设置参会成员质量列表。
        /// </summary>
        public IList<MeetingQualityAttendee> attendees { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多成员数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 获取或设置下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }
}
