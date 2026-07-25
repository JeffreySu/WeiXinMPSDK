/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailAppJson.cs
    文件功能描述：企业微信邮件应用邮箱接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐邮件发送、读取和应用邮箱别名模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 企业邮件收件人、抄送人或密送人。
    /// </summary>
    public class ExmailRecipient
    {
        /// <summary>
        /// 外部或企业邮箱地址列表。
        /// </summary>
        public IList<string> emails { get; set; }

        /// <summary>
        /// 企业成员 UserID 列表。
        /// </summary>
        public IList<string> userids { get; set; }
    }

    /// <summary>
    /// 企业邮件附件。
    /// </summary>
    public class ExmailAttachment
    {
        /// <summary>
        /// 附件文件名。
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        /// Base64 编码的附件内容，不包含 data URI 前缀。
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 邮件日程提醒和重复规则。
    /// </summary>
    public class ExmailScheduleReminder
    {
        /// <summary>
        /// 是否提醒：0 不提醒，1 提醒。
        /// </summary>
        public int? is_remind { get; set; }

        /// <summary>
        /// 日程开始前多少分钟提醒。
        /// </summary>
        public int? remind_before_event_mins { get; set; }

        /// <summary>
        /// 是否为重复日程：0 否，1 是。
        /// </summary>
        public int? is_repeat { get; set; }

        /// <summary>
        /// 是否使用自定义重复规则：0 否，1 是。
        /// </summary>
        public int? is_custom_repeat { get; set; }

        /// <summary>
        /// UTC 时区偏移量，东区为正数。
        /// </summary>
        public int? timezone { get; set; }

        /// <summary>
        /// 重复间隔。
        /// </summary>
        public int? repeat_interval { get; set; }

        /// <summary>
        /// 重复类型。
        /// </summary>
        public int? repeat_type { get; set; }

        /// <summary>
        /// 每周重复的星期列表，取值 1 至 7。
        /// </summary>
        public IList<int> repeat_day_of_week { get; set; }

        /// <summary>
        /// 每月重复的日期列表。
        /// </summary>
        public IList<int> repeat_day_of_month { get; set; }

        /// <summary>
        /// 每月重复的周次列表。
        /// </summary>
        public IList<int> repeat_week_of_month { get; set; }

        /// <summary>
        /// 每年重复的月份列表。
        /// </summary>
        public IList<int> repeat_month_of_year { get; set; }

        /// <summary>
        /// 重复结束时间的 Unix 时间戳（秒）。
        /// </summary>
        public long? repeat_until { get; set; }
    }

    /// <summary>
    /// 企业邮件携带的日程邀请。
    /// </summary>
    public class ExmailSchedule
    {
        /// <summary>
        /// 日程 ID；更新已有日程时填写。
        /// </summary>
        public string schedule_id { get; set; }

        /// <summary>
        /// iCalendar 方法，例如 request 或 cancel。
        /// </summary>
        public string method { get; set; }

        /// <summary>
        /// 日程开始时间的 Unix 时间戳（秒）。
        /// </summary>
        public long start_time { get; set; }

        /// <summary>
        /// 日程结束时间的 Unix 时间戳（秒）。
        /// </summary>
        public long end_time { get; set; }

        /// <summary>
        /// 日程地点。
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 日程提醒和重复规则。
        /// </summary>
        public ExmailScheduleReminder reminders { get; set; }
    }

    /// <summary>
    /// 邮件中附带的会议设置。
    /// </summary>
    public class ExmailMeetingOption
    {
        /// <summary>
        /// 入会密码。
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 自动录制模式。
        /// </summary>
        public int? auto_record { get; set; }

        /// <summary>
        /// 是否开启等候室。
        /// </summary>
        public bool? enable_waiting_room { get; set; }

        /// <summary>
        /// 是否允许成员在主持人进会前加入。
        /// </summary>
        public bool? allow_enter_before_host { get; set; }

        /// <summary>
        /// 是否开启屏幕水印。
        /// </summary>
        public bool? enable_screen_watermark { get; set; }

        /// <summary>
        /// 入会范围限制模式。
        /// </summary>
        public int? enter_restraint { get; set; }

        /// <summary>
        /// 成员入会静音模式。
        /// </summary>
        public int? enable_enter_mute { get; set; }

        /// <summary>
        /// 会议开始提醒范围。
        /// </summary>
        public int? remind_scope { get; set; }

        /// <summary>
        /// 水印类型。
        /// </summary>
        public int? water_mark_type { get; set; }
    }

    /// <summary>
    /// 企业成员 UserID 列表包装对象。
    /// </summary>
    public class ExmailUserIdList
    {
        /// <summary>
        /// 企业成员 UserID 列表。
        /// </summary>
        public IList<string> userids { get; set; }
    }

    /// <summary>
    /// 邮件中附带的企业微信会议。
    /// </summary>
    public class ExmailMeeting
    {
        /// <summary>
        /// 会议选项。
        /// </summary>
        public ExmailMeetingOption option { get; set; }

        /// <summary>
        /// 主持人 UserID 列表。
        /// </summary>
        public ExmailUserIdList hosts { get; set; }

        /// <summary>
        /// 会议管理员 UserID 列表。
        /// </summary>
        public ExmailUserIdList meeting_admins { get; set; }
    }

    /// <summary>
    /// 使用应用邮箱发送邮件请求。
    /// </summary>
    public class ExmailComposeSendRequest
    {
        /// <summary>
        /// 收件人，邮箱地址与成员 UserID 至少填写一种。
        /// </summary>
        public ExmailRecipient to { get; set; }

        /// <summary>
        /// 抄送人。
        /// </summary>
        public ExmailRecipient cc { get; set; }

        /// <summary>
        /// 密送人。
        /// </summary>
        public ExmailRecipient bcc { get; set; }

        /// <summary>
        /// 邮件主题。
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// 邮件正文。
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 正文类型，例如 text/plain 或 text/html。
        /// </summary>
        public string content_type { get; set; }

        /// <summary>
        /// 附件列表。
        /// </summary>
        public IList<ExmailAttachment> attachment_list { get; set; }

        /// <summary>
        /// 随邮件发送的日程邀请。
        /// </summary>
        public ExmailSchedule schedule { get; set; }

        /// <summary>
        /// 随邮件创建的企业微信会议。
        /// </summary>
        public ExmailMeeting meeting { get; set; }

        /// <summary>
        /// 是否开启成员 ID 转译：0 关闭，1 开启。
        /// </summary>
        public int? enable_id_trans { get; set; }
    }

    /// <summary>
    /// 获取应用邮箱邮件 ID 列表请求。
    /// </summary>
    public class ExmailAppMailListRequest
    {
        /// <summary>
        /// 查询开始时间的 Unix 时间戳（秒）。
        /// </summary>
        public long? begin_time { get; set; }

        /// <summary>
        /// 查询结束时间的 Unix 时间戳（秒）。
        /// </summary>
        public long? end_time { get; set; }

        /// <summary>
        /// 每页数量。
        /// </summary>
        public int? limit { get; set; }

        /// <summary>
        /// 分页游标，首次调用可不传。
        /// </summary>
        public string cursor { get; set; }
    }

    /// <summary>
    /// 应用邮箱邮件 ID 条目。
    /// </summary>
    public class ExmailAppMailItem
    {
        /// <summary>
        /// 邮件 ID。
        /// </summary>
        public string mail_id { get; set; }
    }

    /// <summary>
    /// 获取应用邮箱邮件 ID 列表结果。
    /// </summary>
    public class ExmailAppMailListResult : WorkJsonResult
    {
        /// <summary>
        /// 邮件 ID 列表。
        /// </summary>
        public IList<ExmailAppMailItem> mail_list { get; set; }

        /// <summary>
        /// 是否还有更多数据：0 否，1 是。
        /// </summary>
        public int has_more { get; set; }

        /// <summary>
        /// 下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 读取应用邮箱邮件请求。
    /// </summary>
    public class ExmailReadMailRequest
    {
        /// <summary>
        /// 邮件 ID。
        /// </summary>
        public string mail_id { get; set; }
    }

    /// <summary>
    /// 读取应用邮箱邮件结果。
    /// </summary>
    public class ExmailReadMailResult : WorkJsonResult
    {
        /// <summary>
        /// 邮件 EML 原始内容。
        /// </summary>
        public string mail_data { get; set; }
    }

    /// <summary>
    /// 修改应用邮箱地址请求。
    /// </summary>
    public class ExmailUpdateAppEmailAliasRequest
    {
        /// <summary>
        /// 新的应用邮箱地址。
        /// </summary>
        public string new_email { get; set; }
    }

    /// <summary>
    /// 获取应用邮箱地址及别名结果。
    /// </summary>
    public class ExmailAppEmailAliasResult : WorkJsonResult
    {
        /// <summary>
        /// 当前应用邮箱地址。
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 应用邮箱别名列表。
        /// </summary>
        public IList<string> alias_list { get; set; }
    }
}
