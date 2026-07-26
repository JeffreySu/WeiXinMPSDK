/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingRoomsJson.cs
    文件功能描述：企业微信会议 Rooms 请求与结果强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议 Rooms 预定、设备、控制器及呼叫模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 预定企业微信会议 Rooms 会议室请求。
    /// </summary>
    public class BookMeetingRoomsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要预定的会议室 ID 列表。</summary>
        public IList<string> meeting_room_id_list { get; set; }

        /// <summary>获取或设置是否在会议室终端显示会议主题。</summary>
        public bool? subject_visible { get; set; }
    }

    /// <summary>
    /// 已预定的企业微信会议 Rooms 会议室信息。
    /// </summary>
    public class BookedMeetingRoomInfo
    {
        /// <summary>获取或设置会议室 ID。</summary>
        public string meeting_room_id { get; set; }

        /// <summary>获取或设置会议室名称。</summary>
        public string meeting_room_name { get; set; }

        /// <summary>获取或设置会议室地址。</summary>
        public string meeting_room_location { get; set; }
    }

    /// <summary>
    /// 预定企业微信会议 Rooms 会议室结果。
    /// </summary>
    public class BookMeetingRoomsResult : WorkJsonResult
    {
        /// <summary>获取或设置成功预定的会议室列表。</summary>
        public IList<BookedMeetingRoomInfo> meeting_room_list { get; set; }
    }

    /// <summary>
    /// 释放企业微信会议 Rooms 会议室请求。
    /// </summary>
    public class ReleaseMeetingRoomsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要释放的会议室 ID 列表。</summary>
        public IList<string> meeting_room_id_list { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室列表请求。
    /// </summary>
    public class GetMeetingRoomsRequest
    {
        /// <summary>获取或设置用于筛选的会议室名称。</summary>
        public string meeting_room_name { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 会议室列表项。
    /// </summary>
    public class MeetingRoomListItem
    {
        /// <summary>获取或设置会议室 ID。</summary>
        public string meeting_room_id { get; set; }

        /// <summary>获取或设置会议室名称。</summary>
        public string meeting_room_name { get; set; }

        /// <summary>获取或设置会议室地址。</summary>
        public string meeting_room_location { get; set; }

        /// <summary>获取或设置会议室账号类型。</summary>
        public int account_type { get; set; }

        /// <summary>获取或设置会议室激活码。</summary>
        public string active_code { get; set; }

        /// <summary>获取或设置会议室可容纳人数。</summary>
        public int participant_number { get; set; }

        /// <summary>获取或设置会议室在线状态。</summary>
        public int meeting_room_status { get; set; }

        /// <summary>获取或设置会议室预定状态。</summary>
        public int scheduled_status { get; set; }

        /// <summary>获取或设置会议室是否允许被呼叫。</summary>
        public bool is_allow_call { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室列表结果。
    /// </summary>
    public class GetMeetingRoomsResult : WorkJsonResult
    {
        /// <summary>获取或设置会议室列表。</summary>
        public IList<MeetingRoomListItem> meeting_room_list { get; set; }

        /// <summary>获取或设置是否还有更多数据。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室详情请求。
    /// </summary>
    public class GetMeetingRoomInfoRequest
    {
        /// <summary>获取或设置会议室 ID。</summary>
        public string meeting_room_id { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 会议室基础信息。
    /// </summary>
    public class MeetingRoomBasicInfo
    {
        /// <summary>获取或设置与会议室绑定的 Rooms ID 列表。</summary>
        public IList<string> rooms_id_list { get; set; }

        /// <summary>获取或设置会议室名称。</summary>
        public string meeting_room_name { get; set; }

        /// <summary>获取或设置会议室所在城市。</summary>
        public string city { get; set; }

        /// <summary>获取或设置会议室所在建筑。</summary>
        public string building { get; set; }

        /// <summary>获取或设置会议室所在楼层。</summary>
        public string floor { get; set; }

        /// <summary>获取或设置会议室可容纳人数。</summary>
        public int participant_number { get; set; }

        /// <summary>获取或设置会议室设备描述。</summary>
        public string device { get; set; }

        /// <summary>获取或设置会议室说明。</summary>
        public string desc { get; set; }

        /// <summary>获取或设置会议室管理员密码。</summary>
        public string password { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 会议室账号信息。
    /// </summary>
    public class MeetingRoomAccountInfo
    {
        /// <summary>获取或设置会议室账号类型。</summary>
        public int account_type { get; set; }

        /// <summary>获取或设置账号有效期字符串。</summary>
        public string valid_period { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 会议室硬件信息。
    /// </summary>
    public class MeetingRoomHardwareInfo
    {
        /// <summary>获取或设置设备厂商。</summary>
        public string factory { get; set; }

        /// <summary>获取或设置设备型号。</summary>
        public string device_model { get; set; }

        /// <summary>获取或设置设备序列号。</summary>
        public string sn { get; set; }

        /// <summary>获取或设置设备 IP 地址。</summary>
        public string ip { get; set; }

        /// <summary>获取或设置设备 MAC 地址。</summary>
        public string mac { get; set; }

        /// <summary>获取或设置 Rooms 应用版本。</summary>
        public string rooms_version { get; set; }

        /// <summary>获取或设置固件版本。</summary>
        public string firmware_version { get; set; }

        /// <summary>获取或设置设备健康状态。</summary>
        public string health_status { get; set; }

        /// <summary>获取或设置设备系统类型。</summary>
        public string system_type { get; set; }

        /// <summary>获取或设置会议室在线状态。</summary>
        public int meeting_room_status { get; set; }

        /// <summary>获取或设置设备激活时间字符串。</summary>
        public string active_time { get; set; }

        /// <summary>获取或设置 CPU 信息。</summary>
        public string cpu_info { get; set; }

        /// <summary>获取或设置 CPU 使用情况。</summary>
        public string cpu_usage { get; set; }

        /// <summary>获取或设置 GPU 信息。</summary>
        public string gpu_info { get; set; }

        /// <summary>获取或设置网络类型。</summary>
        public string net_type { get; set; }

        /// <summary>获取或设置内存信息。</summary>
        public string memory_info { get; set; }

        /// <summary>获取或设置显示器刷新率；兼容数字或字符串响应。</summary>
        [JsonConverter(typeof(MeetingStringOrNumberJsonConverter))]
        public string monitor_frequency { get; set; }

        /// <summary>获取或设置摄像头型号。</summary>
        public string camera_model { get; set; }

        /// <summary>获取或设置是否开启视频镜像。</summary>
        public bool enable_video_mirror { get; set; }

        /// <summary>获取或设置麦克风信息。</summary>
        public string microphone_info { get; set; }

        /// <summary>获取或设置扬声器信息。</summary>
        public string speaker_info { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 会议室专属会议号信息。
    /// </summary>
    public class MeetingRoomPmiInfo
    {
        /// <summary>获取或设置会议室专属会议号。</summary>
        public string pmi_code { get; set; }

        /// <summary>获取或设置会议室专属会议号密码。</summary>
        public string pmi_pwd { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室详情结果。
    /// </summary>
    public class GetMeetingRoomInfoResult : WorkJsonResult
    {
        /// <summary>获取或设置会议室基础信息。</summary>
        public MeetingRoomBasicInfo basic_info { get; set; }

        /// <summary>获取或设置会议室账号信息。</summary>
        public MeetingRoomAccountInfo account_info { get; set; }

        /// <summary>获取或设置会议室硬件信息。</summary>
        public MeetingRoomHardwareInfo hardware_info { get; set; }

        /// <summary>获取或设置会议室专属会议号信息。</summary>
        public MeetingRoomPmiInfo pmi_info { get; set; }

        /// <summary>获取或设置会议室告警通知状态。</summary>
        public int monitor_status { get; set; }

        /// <summary>获取或设置会议室预定状态。</summary>
        public int scheduled_status { get; set; }

        /// <summary>获取或设置会议室是否允许被呼叫。</summary>
        public bool is_allow_call { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室配置请求。
    /// </summary>
    public class GetMeetingRoomConfigRequest
    {
        /// <summary>获取或设置会议室 ID。</summary>
        public string meeting_room_id { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 会议室会议设置。
    /// </summary>
    public class MeetingRoomMeetingSettings
    {
        /// <summary>获取或设置会议水印状态。</summary>
        public int water_mark { get; set; }

        /// <summary>获取或设置自动接听状态。</summary>
        public int auto_response { get; set; }

        /// <summary>获取或设置是否开启字幕。</summary>
        public bool caption { get; set; }

        /// <summary>获取或设置是否启用会议室专属会议号。</summary>
        public bool room_pmi { get; set; }

        /// <summary>获取或设置是否展示会议室消息通知。</summary>
        public bool room_notification { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 会议室录制设置。
    /// </summary>
    public class MeetingRoomRecordSettings
    {
        /// <summary>获取或设置云录制分享状态。</summary>
        public int share_record { get; set; }

        /// <summary>获取或设置是否允许下载云录制。</summary>
        public bool download_record { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室配置结果。
    /// </summary>
    public class GetMeetingRoomConfigResult : WorkJsonResult
    {
        /// <summary>获取或设置会议配置。</summary>
        public MeetingRoomMeetingSettings meeting_settings { get; set; }

        /// <summary>获取或设置录制配置。</summary>
        public MeetingRoomRecordSettings record_settings { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室会议列表请求。
    /// </summary>
    public class GetMeetingRoomMeetingsRequest
    {
        /// <summary>获取或设置会议室 ID；与 <see cref="rooms_id"/> 二选一。</summary>
        public string meeting_room_id { get; set; }

        /// <summary>获取或设置 Rooms ID；与 <see cref="meeting_room_id"/> 二选一。</summary>
        public string rooms_id { get; set; }

        /// <summary>获取或设置查询起始 Unix 时间戳。</summary>
        public long? start_time { get; set; }

        /// <summary>获取或设置查询结束 Unix 时间戳。</summary>
        public long? end_time { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 会议室关联会议信息。
    /// </summary>
    public class MeetingRoomMeetingInfo
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议号。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置会议主题。</summary>
        public string subject { get; set; }

        /// <summary>获取或设置会议类型。</summary>
        public int meeting_type { get; set; }

        /// <summary>获取或设置会议状态。</summary>
        public string status { get; set; }

        /// <summary>获取或设置会议开始 Unix 时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>获取或设置会议结束 Unix 时间戳。</summary>
        public long end_time { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室会议列表结果。
    /// </summary>
    public class GetMeetingRoomMeetingsResult : WorkJsonResult
    {
        /// <summary>获取或设置会议列表。</summary>
        public IList<MeetingRoomMeetingInfo> meeting_info_list { get; set; }

        /// <summary>获取或设置是否还有更多数据。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 设备列表请求。
    /// </summary>
    public class GetMeetingRoomDevicesRequest
    {
        /// <summary>获取或设置用于筛选的会议室名称。</summary>
        public string meeting_room_name { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 设备监控信息。
    /// </summary>
    public class MeetingRoomDeviceMonitorInfo
    {
        /// <summary>获取或设置摄像头是否正常。</summary>
        public bool camera_status { get; set; }

        /// <summary>获取或设置麦克风是否正常。</summary>
        public bool microphone_status { get; set; }

        /// <summary>获取或设置扬声器是否正常。</summary>
        public bool speaker_status { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 设备信息。
    /// </summary>
    public class MeetingRoomDeviceInfo
    {
        /// <summary>获取或设置 Rooms ID。</summary>
        public string rooms_id { get; set; }

        /// <summary>获取或设置会议室 ID。</summary>
        public string meeting_room_id { get; set; }

        /// <summary>获取或设置会议室名称。</summary>
        public string meeting_room_name { get; set; }

        /// <summary>获取或设置会议室地址。</summary>
        public string meeting_room_location { get; set; }

        /// <summary>获取或设置设备型号。</summary>
        public string device_model { get; set; }

        /// <summary>获取或设置 Rooms 应用版本。</summary>
        public string app_version { get; set; }

        /// <summary>获取或设置会议室在线状态。</summary>
        public int meeting_room_status { get; set; }

        /// <summary>获取或设置设备监控信息。</summary>
        public MeetingRoomDeviceMonitorInfo device_monitor_info { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 设备列表结果。
    /// </summary>
    public class GetMeetingRoomDevicesResult : WorkJsonResult
    {
        /// <summary>获取或设置 Rooms 设备列表。</summary>
        public IList<MeetingRoomDeviceInfo> device_info_list { get; set; }

        /// <summary>获取或设置是否还有更多数据。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 控制器列表请求。
    /// </summary>
    public class GetMeetingRoomControllersRequest
    {
        /// <summary>获取或设置用于筛选的控制器名称。</summary>
        public string controller_name { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms 控制器信息。
    /// </summary>
    public class MeetingRoomControllerInfo
    {
        /// <summary>获取或设置 Rooms ID。</summary>
        public string rooms_id { get; set; }

        /// <summary>获取或设置会议室名称。</summary>
        public string meeting_room_name { get; set; }

        /// <summary>获取或设置会议室地址。</summary>
        public string meeting_room_location { get; set; }

        /// <summary>获取或设置控制器厂商名称。</summary>
        public string manufacture_name { get; set; }

        /// <summary>获取或设置控制器名称。</summary>
        public string controller_name { get; set; }

        /// <summary>获取或设置控制器型号。</summary>
        public string controller_model { get; set; }

        /// <summary>获取或设置控制器应用版本。</summary>
        public string app_version { get; set; }

        /// <summary>获取或设置控制器固件版本。</summary>
        public string framework_version { get; set; }

        /// <summary>获取或设置控制器状态；兼容数字或字符串响应。</summary>
        [JsonConverter(typeof(MeetingStringOrNumberJsonConverter))]
        public string status { get; set; }

        /// <summary>获取或设置控制器 IP 地址。</summary>
        public string ip_address { get; set; }

        /// <summary>获取或设置控制器 MAC 地址。</summary>
        public string mac_address { get; set; }

        /// <summary>获取或设置控制器 CPU 类型。</summary>
        public string cpu_type { get; set; }

        /// <summary>获取或设置控制器 CPU 使用情况。</summary>
        public string cpu_usage { get; set; }

        /// <summary>获取或设置控制器网络类型。</summary>
        public string network_type { get; set; }

        /// <summary>获取或设置控制器内存使用情况。</summary>
        public string mem_usage { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 控制器列表结果。
    /// </summary>
    public class GetMeetingRoomControllersResult : WorkJsonResult
    {
        /// <summary>获取或设置 Rooms 控制器列表。</summary>
        public IList<MeetingRoomControllerInfo> controller_info_list { get; set; }

        /// <summary>获取或设置是否还有更多数据。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 账号库存请求。
    /// </summary>
    public class GetMeetingRoomInventoryRequest
    {
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 账号库存结果。
    /// </summary>
    public class GetMeetingRoomInventoryResult : WorkJsonResult
    {
        /// <summary>获取或设置普通会议室账号总数。</summary>
        public int normal_count { get; set; }

        /// <summary>获取或设置专款会议室账号总数。</summary>
        public int special_count { get; set; }

        /// <summary>获取或设置已使用的普通会议室账号数。</summary>
        public int normal_used_count { get; set; }

        /// <summary>获取或设置已使用的专款会议室账号数。</summary>
        public int special_used_count { get; set; }

        /// <summary>获取或设置已过期的普通会议室账号数。</summary>
        public int normal_expired_count { get; set; }

        /// <summary>获取或设置已过期的专款会议室账号数。</summary>
        public int special_expired_count { get; set; }
    }

    /// <summary>
    /// 企业微信会议 Rooms MRA 呼叫地址。
    /// </summary>
    public class MeetingRoomMraAddress
    {
        /// <summary>获取或设置 MRA 信令协议。</summary>
        public int protocol { get; set; }

        /// <summary>获取或设置 MRA 呼叫字符串。</summary>
        public string dial_string { get; set; }
    }

    /// <summary>
    /// 呼叫企业微信会议 Rooms 会议室请求。
    /// </summary>
    public class CallMeetingRoomRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议室 ID；与 <see cref="mra_address"/> 二选一。</summary>
        public string meeting_room_id { get; set; }

        /// <summary>获取或设置 MRA 地址；与 <see cref="meeting_room_id"/> 二选一。</summary>
        public MeetingRoomMraAddress mra_address { get; set; }
    }

    /// <summary>
    /// 呼叫企业微信会议 Rooms 会议室结果。
    /// </summary>
    public class CallMeetingRoomResult : WorkJsonResult
    {
        /// <summary>获取或设置本次呼叫的邀请 ID。</summary>
        public string invite_id { get; set; }
    }

    /// <summary>
    /// 取消企业微信会议 Rooms 会议室呼叫请求。
    /// </summary>
    public class CancelMeetingRoomCallRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置呼叫接口返回的邀请 ID。</summary>
        public string invite_id { get; set; }

        /// <summary>获取或设置会议室 ID；与 <see cref="mra_address"/> 二选一。</summary>
        public string meeting_room_id { get; set; }

        /// <summary>获取或设置 MRA 地址；与 <see cref="meeting_room_id"/> 二选一。</summary>
        public MeetingRoomMraAddress mra_address { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室呼叫应答状态请求。
    /// </summary>
    public class GetMeetingRoomResponseStatusRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议室 ID；与 <see cref="mra_address"/> 二选一。</summary>
        public string meeting_room_id { get; set; }

        /// <summary>获取或设置 MRA 地址；与 <see cref="meeting_room_id"/> 二选一。</summary>
        public MeetingRoomMraAddress mra_address { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议 Rooms 会议室呼叫应答状态结果。
    /// </summary>
    public class GetMeetingRoomResponseStatusResult : WorkJsonResult
    {
        /// <summary>获取或设置呼叫应答状态。</summary>
        public int status { get; set; }

        /// <summary>获取或设置最近一次应答时间字符串，格式为 yyyy/MM/dd HH:mm:ss。</summary>
        public string response_time { get; set; }
    }
}
