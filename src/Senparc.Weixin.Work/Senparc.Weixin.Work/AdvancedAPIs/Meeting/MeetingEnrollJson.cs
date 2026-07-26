/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingEnrollJson.cs
    文件功能描述：企业微信会议报名管理强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 补齐会议报名查询、审批、导入和删除模型；补齐会议报名配置强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 会议报名问题选项。
    /// </summary>
    public class MeetingEnrollmentQuestionOption
    {
        /// <summary>获取或设置选项内容。</summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 会议报名问题。
    /// </summary>
    public class MeetingEnrollmentQuestion
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
        public IList<MeetingEnrollmentQuestionOption> option_list { get; set; }
    }

    /// <summary>
    /// 设置会议报名配置请求。
    /// </summary>
    public class SetMeetingEnrollmentConfigRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置报名审批类型。</summary>
        public int? approve_type { get; set; }

        /// <summary>获取或设置是否收集报名问题的类型值。</summary>
        public int? is_collect_question { get; set; }

        /// <summary>获取或设置报名问题列表。</summary>
        public IList<MeetingEnrollmentQuestion> question_list { get; set; }

        /// <summary>获取或设置企业成员是否无需报名。</summary>
        public bool? no_registration_needed_for_staff { get; set; }
    }

    /// <summary>
    /// 设置会议报名配置结果。
    /// </summary>
    public class SetMeetingEnrollmentConfigResult : WorkJsonResult
    {
        /// <summary>获取或设置已保存的报名问题数量。</summary>
        public int question_count { get; set; }
    }

    /// <summary>
    /// 获取会议报名配置请求。
    /// </summary>
    public class GetMeetingEnrollmentConfigRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 获取会议报名配置结果。
    /// </summary>
    public class GetMeetingEnrollmentConfigResult : WorkJsonResult
    {
        /// <summary>获取或设置报名审批类型。</summary>
        public int approve_type { get; set; }

        /// <summary>获取或设置是否收集报名问题的类型值。</summary>
        public int is_collect_question { get; set; }

        /// <summary>获取或设置报名问题列表。</summary>
        public IList<MeetingEnrollmentQuestion> question_list { get; set; }

        /// <summary>获取或设置企业成员是否无需报名。</summary>
        public bool no_registration_needed_for_staff { get; set; }
    }

    /// <summary>
    /// 根据临时 OpenId 查询会议报名 ID 请求。
    /// </summary>
    public class QueryMeetingEnrollmentsByTempOpenIdsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置返回结果排序规则。</summary>
        public int? sorting_rules { get; set; }

        /// <summary>获取或设置会议成员临时 OpenId 列表。</summary>
        public IList<string> tmp_openid_list { get; set; }
    }

    /// <summary>
    /// 会议成员临时 OpenId 与报名 ID 的对应关系。
    /// </summary>
    public class MeetingEnrollmentIdMapping
    {
        /// <summary>获取或设置会议成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }

        /// <summary>获取或设置报名 ID。</summary>
        public string enroll_id { get; set; }
    }

    /// <summary>
    /// 根据临时 OpenId 查询会议报名 ID 结果。
    /// </summary>
    public class QueryMeetingEnrollmentsByTempOpenIdsResult : WorkJsonResult
    {
        /// <summary>获取或设置临时 OpenId 与报名 ID 对应关系列表。</summary>
        public IList<MeetingEnrollmentIdMapping> enroll_id_list { get; set; }
    }

    /// <summary>
    /// 获取会议报名列表请求。
    /// </summary>
    public class GetMeetingEnrollmentsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要筛选的报名审批状态。</summary>
        public int? status { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 会议报名问题答案。
    /// </summary>
    public class MeetingEnrollmentAnswer
    {
        /// <summary>获取或设置答案内容列表。</summary>
        public IList<string> answer_content { get; set; }

        /// <summary>获取或设置问题是否必填的类型值。</summary>
        public int is_required { get; set; }

        /// <summary>获取或设置问题类型。</summary>
        public int question_type { get; set; }

        /// <summary>获取或设置特殊问题类型。</summary>
        public int special_type { get; set; }

        /// <summary>获取或设置问题序号。</summary>
        public int question_num { get; set; }

        /// <summary>获取或设置问题标题。</summary>
        public string question_title { get; set; }
    }

    /// <summary>
    /// 单条会议报名记录。
    /// </summary>
    public class MeetingEnrollment
    {
        /// <summary>获取或设置报名 ID；协议可能以数字或字符串返回。</summary>
        public string enroll_id { get; set; }

        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置会议成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }

        /// <summary>获取或设置报名时间字符串，格式为 yyyy/MM/dd HH:mm。</summary>
        public string enroll_time { get; set; }

        /// <summary>获取或设置报名来源类型。</summary>
        public int enroll_source_type { get; set; }

        /// <summary>获取或设置报名人昵称。</summary>
        public string nick_name { get; set; }

        /// <summary>获取或设置报名状态。</summary>
        public int status { get; set; }

        /// <summary>获取或设置报名码。</summary>
        public string enroll_code { get; set; }

        /// <summary>获取或设置报名问题答案列表。</summary>
        public IList<MeetingEnrollmentAnswer> answer_list { get; set; }
    }

    /// <summary>
    /// 获取会议报名列表结果。
    /// </summary>
    public class GetMeetingEnrollmentsResult : WorkJsonResult
    {
        /// <summary>获取或设置会议报名记录列表。</summary>
        public IList<MeetingEnrollment> enroll_list { get; set; }

        /// <summary>获取或设置是否还有更多数据。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 批量审批会议报名请求。
    /// </summary>
    public class ApproveMeetingEnrollmentsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置审批动作。</summary>
        public int action { get; set; }

        /// <summary>获取或设置报名 ID 列表。</summary>
        public IList<string> enroll_id_list { get; set; }
    }

    /// <summary>
    /// 批量审批会议报名结果。
    /// </summary>
    public class ApproveMeetingEnrollmentsResult : WorkJsonResult
    {
        /// <summary>获取或设置成功处理的报名数量。</summary>
        public int handled_count { get; set; }
    }

    /// <summary>
    /// 待导入的会议报名人。
    /// </summary>
    public class MeetingEnrollmentImportItem
    {
        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置国家或地区代码。</summary>
        public string area { get; set; }

        /// <summary>获取或设置电话号码。</summary>
        public string phone_number { get; set; }

        /// <summary>获取或设置报名人昵称。</summary>
        public string nick_name { get; set; }
    }

    /// <summary>
    /// 批量导入会议报名请求。
    /// </summary>
    public class ImportMeetingEnrollmentsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置待导入的报名人列表。</summary>
        public IList<MeetingEnrollmentImportItem> enroll_list { get; set; }
    }

    /// <summary>
    /// 成功导入的会议报名记录。
    /// </summary>
    public class MeetingEnrollmentImportResultItem : MeetingEnrollmentImportItem
    {
        /// <summary>获取或设置报名 ID。</summary>
        public string enroll_id { get; set; }

        /// <summary>获取或设置报名码。</summary>
        public string enroll_code { get; set; }
    }

    /// <summary>
    /// 批量导入会议报名结果。
    /// </summary>
    public class ImportMeetingEnrollmentsResult : WorkJsonResult
    {
        /// <summary>获取或设置成功导入的报名数量。</summary>
        public int total_count { get; set; }

        /// <summary>获取或设置成功导入的报名记录列表。</summary>
        public IList<MeetingEnrollmentImportResultItem> enroll_list { get; set; }
    }

    /// <summary>
    /// 待删除的会议报名 ID 对象。
    /// </summary>
    public class MeetingEnrollmentDeleteItem
    {
        /// <summary>获取或设置报名 ID。</summary>
        public string enroll_id { get; set; }
    }

    /// <summary>
    /// 批量删除会议报名请求。
    /// </summary>
    public class DeleteMeetingEnrollmentsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置待删除的报名 ID 对象列表。</summary>
        public IList<MeetingEnrollmentDeleteItem> enroll_id_list { get; set; }
    }

    /// <summary>
    /// 批量删除会议报名结果。
    /// </summary>
    public class DeleteMeetingEnrollmentsResult : WorkJsonResult
    {
        /// <summary>获取或设置成功删除的报名数量。</summary>
        public int total_count { get; set; }
    }
}
