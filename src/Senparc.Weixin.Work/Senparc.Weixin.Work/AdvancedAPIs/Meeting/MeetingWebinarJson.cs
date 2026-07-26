/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingWebinarJson.cs
    文件功能描述：企业微信会议网络研讨会强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐网络研讨会及报名配置模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 网络研讨会主持人。
    /// </summary>
    public class WebinarHost
    {
        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 网络研讨会音视频、入会和录制设置。
    /// </summary>
    public class WebinarMediaSetting
    {
        /// <summary>获取或设置成员入会时是否静音。</summary>
        public bool? enable_enter_mute { get; set; }

        /// <summary>获取或设置是否允许成员自行解除静音。</summary>
        public bool? allow_unmute_self { get; set; }

        /// <summary>获取或设置是否允许成员在主持人入会前加入。</summary>
        public bool? allow_enter_before_host { get; set; }

        /// <summary>获取或设置是否开启屏幕水印。</summary>
        public bool? enable_screen_watermark { get; set; }

        /// <summary>获取或设置水印样式。</summary>
        public int? watermark_type { get; set; }

        /// <summary>获取或设置是否允许企业外部成员入会。</summary>
        public bool? allow_external_user { get; set; }

        /// <summary>获取或设置自动录制类型。</summary>
        public string auto_record_type { get; set; }

        /// <summary>获取或设置是否在参会成员入会时开始自动录制。</summary>
        public bool? attendee_join_auto_record { get; set; }

        /// <summary>获取或设置是否允许主持人暂停自动录制。</summary>
        public bool? enable_host_pause_auto_record { get; set; }
    }

    /// <summary>
    /// 创建网络研讨会请求。
    /// </summary>
    public class CreateWebinarRequest
    {
        /// <summary>获取或设置会议管理员 UserId。</summary>
        public string admin_userid { get; set; }

        /// <summary>获取或设置网络研讨会主题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置主办方名称。</summary>
        public string sponsor { get; set; }

        /// <summary>获取或设置开始 Unix 时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>获取或设置结束 Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>获取或设置主持人列表。</summary>
        public IList<WebinarHost> hosts { get; set; }

        /// <summary>获取或设置观众入会方式。</summary>
        public int admission_type { get; set; }

        /// <summary>获取或设置入会密码。</summary>
        public string password { get; set; }

        /// <summary>获取或设置封面图片 URL。</summary>
        public string cover_url { get; set; }

        /// <summary>获取或设置网络研讨会简介。</summary>
        public string description { get; set; }

        /// <summary>获取或设置是否允许嘉宾生成邀请链接。</summary>
        public bool? enable_guest_invite_link { get; set; }

        /// <summary>获取或设置音视频、入会和录制设置。</summary>
        public WebinarMediaSetting media_setting { get; set; }

        /// <summary>获取或设置是否开启问答。</summary>
        public bool? enable_qa { get; set; }

        /// <summary>获取或设置敏感词列表。</summary>
        public IList<string> sensitive_words { get; set; }

        /// <summary>获取或设置是否开启人工审核。</summary>
        public bool? enable_manual_check { get; set; }

        /// <summary>获取或设置是否开启活动页。</summary>
        public bool? activity_page { get; set; }

        /// <summary>获取或设置参会人数展示方式。</summary>
        public int? display_number_of_attendees { get; set; }

        /// <summary>获取或设置是否允许观众观看回放。</summary>
        public bool? playback_for_audience { get; set; }

        /// <summary>获取或设置是否开启准备模式。</summary>
        public bool? preparation_mode { get; set; }
    }

    /// <summary>
    /// 创建网络研讨会结果。
    /// </summary>
    public class CreateWebinarResult : WorkJsonResult
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议号。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置网络研讨会主题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置开始 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long start_time { get; set; }

        /// <summary>获取或设置结束 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long end_time { get; set; }

        /// <summary>获取或设置观众入会方式。</summary>
        public int admission_type { get; set; }

        /// <summary>获取或设置入会密码。</summary>
        public string password { get; set; }

        /// <summary>获取或设置观众参会链接。</summary>
        public string audience_join_link { get; set; }

        /// <summary>获取或设置嘉宾参会链接。</summary>
        public string guest_join_link { get; set; }

        /// <summary>获取或设置人工审核链接。</summary>
        public string manual_check_link { get; set; }

        /// <summary>获取或设置人工审核密码。</summary>
        public string manual_check_password { get; set; }
    }

    /// <summary>
    /// 更新网络研讨会请求。
    /// </summary>
    public class UpdateWebinarRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置网络研讨会主题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置主办方名称。</summary>
        public string sponsor { get; set; }

        /// <summary>获取或设置开始 Unix 时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>获取或设置结束 Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>获取或设置主持人列表。</summary>
        public IList<WebinarHost> hosts { get; set; }

        /// <summary>获取或设置观众入会方式。</summary>
        public int admission_type { get; set; }

        /// <summary>获取或设置入会密码。</summary>
        public string password { get; set; }

        /// <summary>获取或设置封面图片 URL。</summary>
        public string cover_url { get; set; }

        /// <summary>获取或设置网络研讨会简介。</summary>
        public string description { get; set; }

        /// <summary>获取或设置是否允许嘉宾生成邀请链接。</summary>
        public bool? enable_guest_invite_link { get; set; }

        /// <summary>获取或设置音视频、入会和录制设置。</summary>
        public WebinarMediaSetting media_setting { get; set; }

        /// <summary>获取或设置是否开启问答。</summary>
        public bool? enable_qa { get; set; }

        /// <summary>获取或设置敏感词列表。</summary>
        public IList<string> sensitive_words { get; set; }

        /// <summary>获取或设置是否开启人工审核。</summary>
        public bool? enable_manual_check { get; set; }

        /// <summary>获取或设置是否开启活动页。</summary>
        public bool? activity_page { get; set; }

        /// <summary>获取或设置参会人数展示方式。</summary>
        public int? display_number_of_attendees { get; set; }

        /// <summary>获取或设置是否允许观众观看回放。</summary>
        public bool? playback_for_audience { get; set; }

        /// <summary>获取或设置是否开启准备模式。</summary>
        public bool? preparation_mode { get; set; }
    }

    /// <summary>
    /// 取消网络研讨会请求。
    /// </summary>
    public class CancelWebinarRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 获取网络研讨会详情请求。
    /// </summary>
    public class GetWebinarRequest
    {
        /// <summary>获取或设置会议 ID；与会议号二选一。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议号；与会议 ID 二选一。</summary>
        public string meeting_code { get; set; }
    }

    /// <summary>
    /// 获取网络研讨会详情结果。
    /// </summary>
    public class GetWebinarResult : WorkJsonResult
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议号。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置网络研讨会主题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置主办方名称。</summary>
        public string sponsor { get; set; }

        /// <summary>获取或设置开始 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long start_time { get; set; }

        /// <summary>获取或设置结束 Unix 时间戳。</summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public long end_time { get; set; }

        /// <summary>获取或设置当前状态。</summary>
        public string status { get; set; }

        /// <summary>获取或设置主持人列表。</summary>
        public IList<WebinarHost> hosts { get; set; }

        /// <summary>获取或设置观众入会方式。</summary>
        public int admission_type { get; set; }

        /// <summary>获取或设置入会密码。</summary>
        public string password { get; set; }

        /// <summary>获取或设置封面图片 URL。</summary>
        public string cover_url { get; set; }

        /// <summary>获取或设置网络研讨会简介。</summary>
        public string description { get; set; }

        /// <summary>获取或设置是否允许嘉宾生成邀请链接。</summary>
        public bool enable_guest_invite_link { get; set; }

        /// <summary>获取或设置观众参会链接。</summary>
        public string audience_join_link { get; set; }

        /// <summary>获取或设置嘉宾参会链接。</summary>
        public string guest_join_link { get; set; }

        /// <summary>获取或设置人工审核链接。</summary>
        public string manual_check_link { get; set; }

        /// <summary>获取或设置人工审核密码。</summary>
        public string manual_check_password { get; set; }

        /// <summary>获取或设置音视频、入会和录制设置。</summary>
        public WebinarMediaSetting media_setting { get; set; }

        /// <summary>获取或设置是否开启问答。</summary>
        public bool enable_qa { get; set; }

        /// <summary>获取或设置是否开启活动页。</summary>
        public bool activity_page { get; set; }

        /// <summary>获取或设置参会人数展示方式。</summary>
        public int display_number_of_attendees { get; set; }

        /// <summary>获取或设置是否允许观众观看回放。</summary>
        public bool playback_for_audience { get; set; }

        /// <summary>获取或设置回放地址。</summary>
        public string playback_url { get; set; }

        /// <summary>获取或设置是否开启准备模式。</summary>
        public bool preparation_mode { get; set; }

        /// <summary>获取或设置暖场图片地址。</summary>
        public string warm_up_picture { get; set; }

        /// <summary>获取或设置暖场视频地址。</summary>
        public string warm_up_video { get; set; }

        /// <summary>获取或设置是否允许参会者邀请其他人。</summary>
        public bool allow_attendees_invite_others { get; set; }
    }

    /// <summary>
    /// 获取网络研讨会嘉宾列表请求。
    /// </summary>
    public class GetWebinarGuestsRequest : GetWebinarRequest
    {
    }

    /// <summary>
    /// 网络研讨会嘉宾。
    /// </summary>
    public class WebinarGuest
    {
        /// <summary>获取或设置嘉宾类型。</summary>
        public int guest_type { get; set; }

        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置国家或地区代码。</summary>
        public string area { get; set; }

        /// <summary>获取或设置电话号码。</summary>
        public string phone_number { get; set; }

        /// <summary>获取或设置电子邮箱。</summary>
        public string email { get; set; }

        /// <summary>获取或设置嘉宾姓名。</summary>
        public string guest_name { get; set; }
    }

    /// <summary>
    /// 获取网络研讨会嘉宾列表结果。
    /// </summary>
    public class GetWebinarGuestsResult : WorkJsonResult
    {
        /// <summary>获取或设置嘉宾列表。</summary>
        public IList<WebinarGuest> guests { get; set; }
    }

    /// <summary>
    /// 更新网络研讨会嘉宾列表请求。
    /// </summary>
    public class UpdateWebinarGuestsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置替换后的嘉宾列表。</summary>
        public IList<WebinarGuest> guests { get; set; }
    }

    /// <summary>
    /// 更新网络研讨会暖场配置请求。
    /// </summary>
    public class UpdateWebinarWarmUpRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置暖场图片地址。</summary>
        public string warm_up_picture { get; set; }

        /// <summary>获取或设置暖场视频地址。</summary>
        public string warm_up_video { get; set; }

        /// <summary>获取或设置是否允许参会者邀请其他人。</summary>
        public bool? allow_attendees_invite_others { get; set; }
    }

    /// <summary>
    /// 网络研讨会报名问题选项。
    /// </summary>
    public class WebinarEnrollmentQuestionOption
    {
        /// <summary>获取或设置选项内容。</summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 网络研讨会报名问题。
    /// </summary>
    public class WebinarEnrollmentQuestion
    {
        /// <summary>获取或设置问题是否必填的类型值。</summary>
        public int? is_required { get; set; }

        /// <summary>获取或设置问题类型。</summary>
        public int? question_type { get; set; }

        /// <summary>获取或设置特殊问题类型。</summary>
        public int? special_type { get; set; }

        /// <summary>获取或设置问题标题。</summary>
        public string question_title { get; set; }

        /// <summary>获取或设置问题选项列表。</summary>
        public IList<WebinarEnrollmentQuestionOption> option_list { get; set; }
    }

    /// <summary>
    /// 设置网络研讨会报名配置请求。
    /// </summary>
    public class SetWebinarEnrollmentConfigRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置报名审批类型。</summary>
        public int? approve_type { get; set; }

        /// <summary>获取或设置是否收集报名问题，取值遵循官方协议。</summary>
        public int? is_collect_question { get; set; }

        /// <summary>获取或设置报名问题列表。</summary>
        public IList<WebinarEnrollmentQuestion> question_list { get; set; }

        /// <summary>获取或设置企业成员是否无需报名。</summary>
        public bool? no_registration_needed_for_staff { get; set; }
    }

    /// <summary>
    /// 设置网络研讨会报名配置结果。
    /// </summary>
    public class SetWebinarEnrollmentConfigResult : WorkJsonResult
    {
        /// <summary>获取或设置已保存的问题数量。</summary>
        public int question_count { get; set; }
    }

    /// <summary>
    /// 获取网络研讨会报名配置请求。
    /// </summary>
    public class GetWebinarEnrollmentConfigRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 获取网络研讨会报名配置结果。
    /// </summary>
    public class GetWebinarEnrollmentConfigResult : WorkJsonResult
    {
        /// <summary>获取或设置报名审批类型。</summary>
        public int approve_type { get; set; }

        /// <summary>获取或设置是否收集报名问题的类型值。</summary>
        public int is_collect_question { get; set; }

        /// <summary>获取或设置报名问题列表。</summary>
        public IList<WebinarEnrollmentQuestion> question_list { get; set; }

        /// <summary>获取或设置企业成员是否无需报名。</summary>
        public bool no_registration_needed_for_staff { get; set; }
    }
}
