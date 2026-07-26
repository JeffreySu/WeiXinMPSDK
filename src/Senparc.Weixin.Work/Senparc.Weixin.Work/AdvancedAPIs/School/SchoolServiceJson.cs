/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SchoolServiceJson.cs
    文件功能描述：企业微信家校健康、直播、缴费与应用范围强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐家校健康、直播、缴费与应用范围强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.School
{
    /// <summary>SchoolHealthInfoRequest protocol model.</summary>
    public class SchoolHealthInfoRequest
    {
        /// <summary>date protocol field.</summary>
        public string date { get; set; }
        /// <summary>next_key protocol field.</summary>
        public string next_key { get; set; }
        /// <summary>limit protocol field.</summary>
        public int? limit { get; set; }
    }

    /// <summary>SchoolHealthReportValue protocol model.</summary>
    public class SchoolHealthReportValue
    {
        /// <summary>question_id protocol field.</summary>
        public int question_id { get; set; }
        /// <summary>single_chose protocol field.</summary>
        public int? single_chose { get; set; }
        /// <summary>text protocol field.</summary>
        public string text { get; set; }
    }

    /// <summary>SchoolHealthQuestionOption protocol model.</summary>
    public class SchoolHealthQuestionOption
    {
        /// <summary>option_id protocol field.</summary>
        public int option_id { get; set; }
        /// <summary>option_text protocol field.</summary>
        public string option_text { get; set; }
    }

    /// <summary>SchoolHealthQuestion protocol model.</summary>
    public class SchoolHealthQuestion
    {
        /// <summary>question_id protocol field.</summary>
        public int question_id { get; set; }
        /// <summary>question_type protocol field.</summary>
        public int question_type { get; set; }
        /// <summary>title protocol field.</summary>
        public string title { get; set; }
        /// <summary>is_must_fill protocol field.</summary>
        public int is_must_fill { get; set; }
        /// <summary>is_not_display protocol field.</summary>
        public int is_not_display { get; set; }
        /// <summary>option_list protocol field.</summary>
        public IList<SchoolHealthQuestionOption> option_list { get; set; }
    }

    /// <summary>SchoolHealthInfo protocol model.</summary>
    public class SchoolHealthInfo
    {
        /// <summary>userid protocol field.</summary>
        public string userid { get; set; }
        /// <summary>health_qrcode_status protocol field.</summary>
        public int health_qrcode_status { get; set; }
        /// <summary>self_submit protocol field.</summary>
        public int self_submit { get; set; }
        /// <summary>report_values protocol field.</summary>
        public IList<SchoolHealthReportValue> report_values { get; set; }
        /// <summary>question_templates protocol field.</summary>
        public IList<SchoolHealthQuestion> question_templates { get; set; }
    }

    /// <summary>SchoolHealthInfoResult protocol model.</summary>
    public class SchoolHealthInfoResult : WorkJsonResult
    {
        /// <summary>health_infos protocol field.</summary>
        public IList<SchoolHealthInfo> health_infos { get; set; }
        /// <summary>template_id protocol field.</summary>
        public string template_id { get; set; }
        /// <summary>next_key protocol field.</summary>
        public string next_key { get; set; }
        /// <summary>ending protocol field.</summary>
        public int ending { get; set; }
    }

    /// <summary>SchoolHealthQrCodeRequest protocol model.</summary>
    public class SchoolHealthQrCodeRequest
    {
        /// <summary>userids protocol field.</summary>
        public IList<string> userids { get; set; }
        /// <summary>type protocol field.</summary>
        public int type { get; set; }
    }

    /// <summary>SchoolHealthQrCodeItem protocol model.</summary>
    public class SchoolHealthQrCodeItem : WorkJsonResult
    {
        /// <summary>userid protocol field.</summary>
        public string userid { get; set; }
        /// <summary>qrcode_data protocol field.</summary>
        public string qrcode_data { get; set; }
    }

    /// <summary>SchoolHealthQrCodeResult protocol model.</summary>
    public class SchoolHealthQrCodeResult : WorkJsonResult
    {
        /// <summary>result_list protocol field.</summary>
        public IList<SchoolHealthQrCodeItem> result_list { get; set; }
    }

    /// <summary>SchoolLivingRange protocol model.</summary>
    public class SchoolLivingRange
    {
        /// <summary>partyids protocol field.</summary>
        public IList<long> partyids { get; set; }
        /// <summary>group_names protocol field.</summary>
        public IList<string> group_names { get; set; }
    }

    /// <summary>SchoolLivingInfo protocol model.</summary>
    public class SchoolLivingInfo
    {
        /// <summary>theme protocol field.</summary>
        public string theme { get; set; }
        /// <summary>living_start protocol field.</summary>
        public long living_start { get; set; }
        /// <summary>living_duration protocol field.</summary>
        public long living_duration { get; set; }
        /// <summary>anchor_userid protocol field.</summary>
        public string anchor_userid { get; set; }
        /// <summary>living_range protocol field.</summary>
        public SchoolLivingRange living_range { get; set; }
        /// <summary>viewer_num protocol field.</summary>
        public int viewer_num { get; set; }
        /// <summary>comment_num protocol field.</summary>
        public int comment_num { get; set; }
        /// <summary>open_replay protocol field.</summary>
        public int open_replay { get; set; }
        /// <summary>push_stream_url protocol field.</summary>
        public string push_stream_url { get; set; }
    }

    /// <summary>SchoolLivingInfoResult protocol model.</summary>
    public class SchoolLivingInfoResult : WorkJsonResult
    {
        /// <summary>living_info protocol field.</summary>
        public SchoolLivingInfo living_info { get; set; }
    }

    /// <summary>SchoolLivingStatisticsRequest protocol model.</summary>
    public class SchoolLivingStatisticsRequest
    {
        /// <summary>livingid protocol field.</summary>
        public string livingid { get; set; }
        /// <summary>next_key protocol field.</summary>
        public string next_key { get; set; }
    }

    /// <summary>SchoolLivingStudentStatistics protocol model.</summary>
    public class SchoolLivingStudentStatistics
    {
        /// <summary>student_userid protocol field.</summary>
        public string student_userid { get; set; }
        /// <summary>parent_userid protocol field.</summary>
        public string parent_userid { get; set; }
        /// <summary>partyids protocol field.</summary>
        public IList<long> partyids { get; set; }
        /// <summary>watch_time protocol field.</summary>
        public int? watch_time { get; set; }
        /// <summary>is_comment protocol field.</summary>
        public int? is_comment { get; set; }
        /// <summary>enter_time protocol field.</summary>
        public long? enter_time { get; set; }
        /// <summary>leave_time protocol field.</summary>
        public long? leave_time { get; set; }
    }

    /// <summary>SchoolLivingVisitorStatistics protocol model.</summary>
    public class SchoolLivingVisitorStatistics
    {
        /// <summary>nickname protocol field.</summary>
        public string nickname { get; set; }
        /// <summary>watch_time protocol field.</summary>
        public int watch_time { get; set; }
        /// <summary>is_comment protocol field.</summary>
        public int is_comment { get; set; }
        /// <summary>enter_time protocol field.</summary>
        public long enter_time { get; set; }
        /// <summary>leave_time protocol field.</summary>
        public long leave_time { get; set; }
    }

    /// <summary>SchoolLivingStatistics protocol model.</summary>
    public class SchoolLivingStatistics
    {
        /// <summary>students protocol field.</summary>
        public IList<SchoolLivingStudentStatistics> students { get; set; }
        /// <summary>visitors protocol field.</summary>
        public IList<SchoolLivingVisitorStatistics> visitors { get; set; }
    }

    /// <summary>SchoolLivingWatchResult protocol model.</summary>
    public class SchoolLivingWatchResult : WorkJsonResult
    {
        /// <summary>ending protocol field.</summary>
        public int ending { get; set; }
        /// <summary>next_key protocol field.</summary>
        public string next_key { get; set; }
        /// <summary>stat_infoes protocol field.</summary>
        public SchoolLivingStatistics stat_infoes { get; set; }
    }

    /// <summary>SchoolLivingUnwatchResult protocol model.</summary>
    public class SchoolLivingUnwatchResult : WorkJsonResult
    {
        /// <summary>ending protocol field.</summary>
        public int ending { get; set; }
        /// <summary>next_key protocol field.</summary>
        public string next_key { get; set; }
        /// <summary>stat_info protocol field.</summary>
        public SchoolLivingStatistics stat_info { get; set; }
    }

    /// <summary>新版家校直播统计请求。</summary>
    public class SchoolLivingStatisticsV2Request
    {
        /// <summary>直播 ID。</summary>
        public string livingid { get; set; }
        /// <summary>下一页游标；首次请求不填写。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>新版家校直播统计信息。</summary>
    public class SchoolLivingStatisticsV2
    {
        /// <summary>学生统计列表。</summary>
        public IList<SchoolLivingStudentStatistics> students { get; set; }
        /// <summary>家长统计列表。</summary>
        public IList<SchoolLivingStudentStatistics> parents { get; set; }
        /// <summary>外部访客统计列表；未观看统计不返回此字段。</summary>
        public IList<SchoolLivingVisitorStatistics> visitors { get; set; }
    }

    /// <summary>新版家校直播观看统计结果。</summary>
    public class SchoolLivingWatchV2Result : WorkJsonResult
    {
        /// <summary>统计信息。</summary>
        public SchoolLivingStatisticsV2 stat_info { get; set; }
        /// <summary>是否还有更多数据，0 表示否，1 表示是。</summary>
        public int has_more { get; set; }
        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>新版家校直播未观看统计结果。</summary>
    public class SchoolLivingUnwatchV2Result : WorkJsonResult
    {
        /// <summary>统计信息。</summary>
        public SchoolLivingStatisticsV2 stat_info { get; set; }
        /// <summary>是否还有更多数据，0 表示否，1 表示是。</summary>
        public int has_more { get; set; }
        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>SchoolPaymentRequest protocol model.</summary>
    public class SchoolPaymentRequest
    {
        /// <summary>payment_id protocol field.</summary>
        public string payment_id { get; set; }
    }

    /// <summary>SchoolPaymentItem protocol model.</summary>
    public class SchoolPaymentItem
    {
        /// <summary>student_userid protocol field.</summary>
        public string student_userid { get; set; }
        /// <summary>trade_state protocol field.</summary>
        public int trade_state { get; set; }
        /// <summary>trade_no protocol field.</summary>
        public string trade_no { get; set; }
        /// <summary>payer_parent_userid protocol field.</summary>
        public string payer_parent_userid { get; set; }
    }

    /// <summary>SchoolPaymentResult protocol model.</summary>
    public class SchoolPaymentResult : WorkJsonResult
    {
        /// <summary>project_name protocol field.</summary>
        public string project_name { get; set; }
        /// <summary>amount protocol field.</summary>
        public long amount { get; set; }
        /// <summary>payment_result protocol field.</summary>
        public IList<SchoolPaymentItem> payment_result { get; set; }
    }

    /// <summary>SchoolTradeRequest protocol model.</summary>
    public class SchoolTradeRequest
    {
        /// <summary>payment_id protocol field.</summary>
        public string payment_id { get; set; }
        /// <summary>trade_no protocol field.</summary>
        public string trade_no { get; set; }
    }

    /// <summary>SchoolTradeResult protocol model.</summary>
    public class SchoolTradeResult : WorkJsonResult
    {
        /// <summary>transaction_id protocol field.</summary>
        public string transaction_id { get; set; }
        /// <summary>pay_time protocol field.</summary>
        public long pay_time { get; set; }
    }

    /// <summary>SchoolAllowScopeStudent protocol model.</summary>
    public class SchoolAllowScopeStudent
    {
        /// <summary>userid protocol field.</summary>
        public string userid { get; set; }
    }

    /// <summary>SchoolAllowScope protocol model.</summary>
    public class SchoolAllowScope
    {
        /// <summary>students protocol field.</summary>
        public IList<SchoolAllowScopeStudent> students { get; set; }
        /// <summary>departments protocol field.</summary>
        public IList<long> departments { get; set; }
    }

    /// <summary>SchoolAllowScopeResult protocol model.</summary>
    public class SchoolAllowScopeResult : WorkJsonResult
    {
        /// <summary>allow_scope protocol field.</summary>
        public SchoolAllowScope allow_scope { get; set; }
    }
}
