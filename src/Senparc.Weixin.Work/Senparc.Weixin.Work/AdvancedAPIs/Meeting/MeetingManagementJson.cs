/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingManagementJson.cs
    文件功能描述：企业微信会议取消、详情与嘉宾管理强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 补齐会议取消、详情及嘉宾管理模型；补齐会议设备参会检查模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 取消会议请求。
    /// </summary>
    public class CancelMeetingRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置子会议 ID。</summary>
        public string sub_meetingid { get; set; }
    }

    /// <summary>
    /// 取消会议结果。
    /// </summary>
    public class CancelMeetingResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 获取会议详情请求。
    /// </summary>
    public class GetMeetingInfoRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议号。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置子会议 ID。</summary>
        public string sub_meetingid { get; set; }
    }

    /// <summary>
    /// 会议中的企业成员参会摘要。
    /// </summary>
    public class MeetingInfoMemberAttendee
    {
        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置与会状态。</summary>
        public int status { get; set; }

        /// <summary>获取或设置首次入会 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? first_join_time { get; set; }

        /// <summary>获取或设置最后离会 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? last_quit_time { get; set; }

        /// <summary>获取或设置入会次数。</summary>
        public int total_join_count { get; set; }

        /// <summary>获取或设置累计参会时长，单位为秒。</summary>
        public int cumulative_time { get; set; }
    }

    /// <summary>
    /// 会议中的外部联系人参会摘要。
    /// </summary>
    public class MeetingInfoExternalAttendee
    {
        /// <summary>获取或设置外部联系人临时 ID。</summary>
        public string tmp_external_userid { get; set; }

        /// <summary>获取或设置与会状态。</summary>
        public int status { get; set; }

        /// <summary>获取或设置首次入会 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? first_join_time { get; set; }

        /// <summary>获取或设置最后离会 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? last_quit_time { get; set; }

        /// <summary>获取或设置入会次数。</summary>
        public int total_join_count { get; set; }

        /// <summary>获取或设置累计参会时长，单位为秒。</summary>
        public int cumulative_time { get; set; }
    }

    /// <summary>
    /// 会议中的设备参会摘要。
    /// </summary>
    public class MeetingInfoDeviceAttendee
    {
        /// <summary>获取或设置设备序列号。</summary>
        public string device_sn { get; set; }

        /// <summary>获取或设置与会状态。</summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 会议详情中的参会人员分组。
    /// </summary>
    public class MeetingInfoAttendees
    {
        /// <summary>获取或设置企业成员列表。</summary>
        public IList<MeetingInfoMemberAttendee> member { get; set; }

        /// <summary>获取或设置外部联系人列表。</summary>
        public IList<MeetingInfoExternalAttendee> tmp_external_user { get; set; }

        /// <summary>获取或设置设备列表。</summary>
        public IList<MeetingInfoDeviceAttendee> device { get; set; }
    }

    /// <summary>
    /// 会议详情中的权限、录制和入会设置。
    /// </summary>
    public class MeetingInfoSettings
    {
        /// <summary>获取或设置是否需要入会密码。</summary>
        public bool? need_password { get; set; }

        /// <summary>获取或设置入会密码。</summary>
        public string password { get; set; }

        /// <summary>获取或设置是否开启等候室。</summary>
        public bool? enable_waiting_room { get; set; }

        /// <summary>获取或设置是否允许主持人入会前加入。</summary>
        public bool? allow_enter_before_host { get; set; }

        /// <summary>获取或设置是否允许企业外成员入会。</summary>
        public bool? allow_external_user { get; set; }

        /// <summary>获取或设置入会静音模式。</summary>
        public int? enable_enter_mute { get; set; }

        /// <summary>获取或设置是否允许参会者自行解除静音。</summary>
        public bool? allow_unmute_self { get; set; }

        /// <summary>获取或设置是否全体静音。</summary>
        public bool? mute_all { get; set; }

        /// <summary>获取或设置是否开启屏幕水印。</summary>
        public bool? enable_screen_watermark { get; set; }

        /// <summary>获取或设置水印类型。</summary>
        public int? watermark_type { get; set; }

        /// <summary>获取或设置自动录制类型。</summary>
        public string auto_record_type { get; set; }

        /// <summary>获取或设置是否在参会成员入会时立即开始云录制。</summary>
        public bool? attendee_join_auto_record { get; set; }

        /// <summary>获取或设置是否允许主持人暂停或停止云录制。</summary>
        public bool? enable_host_pause_auto_record { get; set; }

        /// <summary>获取或设置是否开启同声传译。</summary>
        public bool? enable_interpreter { get; set; }

        /// <summary>获取或设置是否允许成员上传文档。</summary>
        public bool? enable_doc_upload_permission { get; set; }

        /// <summary>获取或设置是否开启会议报名。</summary>
        public bool? enable_enroll { get; set; }

        /// <summary>获取或设置是否开启主持人密钥。</summary>
        public bool? enable_host_key { get; set; }

        /// <summary>获取或设置主持人密钥。</summary>
        public string host_key { get; set; }

        /// <summary>获取或设置会议开始提醒范围。</summary>
        public int? remind_scope { get; set; }

        /// <summary>获取或设置会议主持人成员。</summary>
        public MeetingUserGroup hosts { get; set; }

        /// <summary>获取或设置当前主持人成员。</summary>
        public MeetingUserGroup current_hosts { get; set; }

        /// <summary>获取或设置联席主持人成员。</summary>
        public MeetingUserGroup co_hosts { get; set; }

        /// <summary>获取或设置需要响铃提醒的成员。</summary>
        public MeetingUserGroup ring_users { get; set; }
    }

    /// <summary>
    /// 周期会议中的子会议摘要。
    /// </summary>
    public class MeetingInfoSubMeeting
    {
        /// <summary>获取或设置子会议 ID。</summary>
        public string sub_meetingid { get; set; }

        /// <summary>获取或设置子会议标题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置子会议状态。</summary>
        public int status { get; set; }

        /// <summary>获取或设置开始 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long start_time { get; set; }

        /// <summary>获取或设置结束 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long end_time { get; set; }

        /// <summary>获取或设置周期会议分段 ID。</summary>
        public string repeat_id { get; set; }
    }

    /// <summary>
    /// 周期会议的分段重复规则。
    /// </summary>
    public class MeetingInfoSubRepeat
    {
        /// <summary>获取或设置周期会议分段 ID。</summary>
        public string repeat_id { get; set; }

        /// <summary>获取或设置重复类型。</summary>
        public int repeat_type { get; set; }

        /// <summary>获取或设置是否使用自定义重复规则，取值为 0 或 1。</summary>
        public int? is_custom_repeat { get; set; }

        /// <summary>获取或设置重复间隔。</summary>
        public int repeat_interval { get; set; }

        /// <summary>获取或设置每周重复的星期列表。</summary>
        public IList<int> repeat_day_of_week { get; set; }

        /// <summary>获取或设置每月重复的日期列表。</summary>
        public IList<int> repeat_day_of_month { get; set; }

        /// <summary>获取或设置重复结束类型。</summary>
        public int repeat_until_type { get; set; }

        /// <summary>获取或设置周期会议限定次数。</summary>
        public int? repeat_until_count { get; set; }

        /// <summary>获取或设置重复结束 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? repeat_until { get; set; }
    }

    /// <summary>
    /// 获取会议详情结果。
    /// </summary>
    public class GetMeetingInfoResult : WorkJsonResult
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议管理员 UserId。</summary>
        public string admin_userid { get; set; }

        /// <summary>获取或设置发起人所在主部门 ID。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long? main_department { get; set; }

        /// <summary>获取或设置会议标题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置会议开始 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long meeting_start { get; set; }

        /// <summary>获取或设置会议持续时长，单位为秒。</summary>
        public int meeting_duration { get; set; }

        /// <summary>获取或设置会议状态。</summary>
        public int status { get; set; }

        /// <summary>获取或设置会议类型。</summary>
        public int meeting_type { get; set; }

        /// <summary>获取或设置会议描述。</summary>
        public string description { get; set; }

        /// <summary>获取或设置会议地点。</summary>
        public string location { get; set; }

        /// <summary>获取或设置企业微信日历 ID。</summary>
        public string cal_id { get; set; }

        /// <summary>获取或设置参会人员分组。</summary>
        public MeetingInfoAttendees attendees { get; set; }

        /// <summary>获取或设置外部嘉宾列表。</summary>
        public IList<MeetingGuest> guests { get; set; }

        /// <summary>获取或设置会议权限、录制和入会设置。</summary>
        public MeetingInfoSettings settings { get; set; }

        /// <summary>获取或设置会议重复和提醒规则。</summary>
        public MeetingReminder reminders { get; set; }

        /// <summary>获取或设置会议号。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置会议链接。</summary>
        public string meeting_link { get; set; }

        /// <summary>获取或设置是否包含投票。</summary>
        public bool? has_vote { get; set; }

        /// <summary>获取或设置子会议列表。</summary>
        public IList<MeetingInfoSubMeeting> sub_meetings { get; set; }

        /// <summary>获取或设置是否还有更多子会议，取值为 0 或 1。</summary>
        public int? has_more_sub_meeting { get; set; }

        /// <summary>获取或设置剩余子会议数量。</summary>
        public int? remain_sub_meetings { get; set; }

        /// <summary>获取或设置当前子会议 ID。</summary>
        public string current_sub_meetingid { get; set; }

        /// <summary>获取或设置周期会议分段规则列表。</summary>
        public IList<MeetingInfoSubRepeat> sub_repeat_list { get; set; }
    }

    /// <summary>
    /// 检查成员终端设备是否在会议中请求。
    /// </summary>
    public class CheckDeviceInMeetingRequest
    {
        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置需要检查的终端设备类型列表。</summary>
        public IList<int> instance_id_list { get; set; }

        /// <summary>获取或设置需要检查的会议 ID 列表。</summary>
        public IList<string> meetingid_list { get; set; }
    }

    /// <summary>
    /// 成员终端设备所在会议的匹配结果。
    /// </summary>
    public class DeviceInMeetingItem
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置终端设备类型。</summary>
        public int instance_id { get; set; }
    }

    /// <summary>
    /// 检查成员终端设备是否在会议中结果。
    /// </summary>
    public class CheckDeviceInMeetingResult : WorkJsonResult
    {
        /// <summary>获取或设置匹配到的会议和终端设备列表。</summary>
        public IList<DeviceInMeetingItem> result_list { get; set; }
    }

    /// <summary>
    /// 获取会议嘉宾列表请求。
    /// </summary>
    public class GetMeetingGuestsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 获取会议嘉宾列表结果。
    /// </summary>
    public class GetMeetingGuestsResult : WorkJsonResult
    {
        /// <summary>获取或设置嘉宾列表。</summary>
        public IList<MeetingGuest> guests { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议号。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置会议主题。</summary>
        public string title { get; set; }
    }

    /// <summary>
    /// 设置会议嘉宾列表请求。
    /// </summary>
    public class SetMeetingGuestsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要替换的嘉宾列表。</summary>
        public IList<MeetingGuest> guests { get; set; }
    }

    /// <summary>
    /// 设置会议嘉宾列表结果。
    /// </summary>
    public class SetMeetingGuestsResult : WorkJsonResult
    {
    }
}
