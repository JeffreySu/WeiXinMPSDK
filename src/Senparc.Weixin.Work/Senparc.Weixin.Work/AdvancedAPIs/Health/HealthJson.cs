/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：HealthJson.cs
    文件功能描述：企业微信健康上报强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐健康上报统计、任务和答案模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Health
{
    /// <summary>
    /// 获取健康上报使用统计请求。
    /// </summary>
    public class HealthGetReportStatisticsRequest
    {
        /// <summary>
        /// 查询日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string date { get; set; }
    }

    /// <summary>
    /// 获取健康上报使用统计结果。
    /// </summary>
    public class HealthGetReportStatisticsResult : WorkJsonResult
    {
        /// <summary>
        /// 应用使用次数。
        /// </summary>
        public int pv { get; set; }

        /// <summary>
        /// 应用使用人数。
        /// </summary>
        public int uv { get; set; }
    }

    /// <summary>
    /// 获取健康上报任务 ID 请求。
    /// </summary>
    public class HealthGetReportJobIdsRequest
    {
        /// <summary>
        /// 分页起始位置。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 每页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 获取健康上报任务 ID 结果。
    /// </summary>
    public class HealthGetReportJobIdsResult : WorkJsonResult
    {
        /// <summary>
        /// 任务 ID 列表。
        /// </summary>
        public IList<string> jobids { get; set; }

        /// <summary>
        /// 是否已到最后一页，官方协议使用 0 或 1。
        /// </summary>
        public int ending { get; set; }
    }

    /// <summary>
    /// 获取健康上报任务配置请求。
    /// </summary>
    public class HealthGetReportJobInfoRequest
    {
        /// <summary>
        /// 任务 ID。
        /// </summary>
        public string jobid { get; set; }

        /// <summary>
        /// 任务日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string date { get; set; }
    }

    /// <summary>
    /// 健康上报任务覆盖范围。
    /// </summary>
    public class HealthReportRange
    {
        /// <summary>
        /// 成员账号列表。
        /// </summary>
        public IList<string> userids { get; set; }

        /// <summary>
        /// 部门 ID 列表；使用 64 位整数兼容企业微信部门 ID。
        /// </summary>
        public IList<long> partyids { get; set; }
    }

    /// <summary>
    /// 健康上报任务汇报对象。
    /// </summary>
    public class HealthReportTo
    {
        /// <summary>
        /// 汇报对象的成员账号列表。
        /// </summary>
        public IList<string> userids { get; set; }
    }

    /// <summary>
    /// 健康上报问题选项。
    /// </summary>
    public class HealthQuestionOption
    {
        /// <summary>
        /// 选项 ID。
        /// </summary>
        public int option_id { get; set; }

        /// <summary>
        /// 选项文本。
        /// </summary>
        public string option_text { get; set; }
    }

    /// <summary>
    /// 健康上报问题模板。
    /// </summary>
    public class HealthQuestionTemplate
    {
        /// <summary>
        /// 问题 ID。
        /// </summary>
        public int question_id { get; set; }

        /// <summary>
        /// 问题标题。
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 问题类型。
        /// </summary>
        public int question_type { get; set; }

        /// <summary>
        /// 是否必填，官方协议使用 0 或 1。
        /// </summary>
        public int is_required { get; set; }

        /// <summary>
        /// 问题选项列表。
        /// </summary>
        public IList<HealthQuestionOption> option_list { get; set; }
    }

    /// <summary>
    /// 健康上报任务配置。
    /// </summary>
    public class HealthReportJob
    {
        /// <summary>
        /// 任务名称。
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 发起人成员账号。
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 填写人 ID 类型。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 已完成上报人数。
        /// </summary>
        public int finish_cnt { get; set; }

        /// <summary>
        /// 上报方式。
        /// </summary>
        public int report_type { get; set; }

        /// <summary>
        /// 任务覆盖范围。
        /// </summary>
        public HealthReportRange apply_range { get; set; }

        /// <summary>
        /// 汇报对象。
        /// </summary>
        public HealthReportTo report_to { get; set; }

        /// <summary>
        /// 非工作日是否跳过上报，官方协议使用 0 或 1。
        /// </summary>
        public int skip_weekend { get; set; }

        /// <summary>
        /// 问题模板列表。
        /// </summary>
        public IList<HealthQuestionTemplate> question_templates { get; set; }
    }

    /// <summary>
    /// 获取健康上报任务配置结果。
    /// </summary>
    public class HealthGetReportJobInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 健康上报任务配置。
        /// </summary>
        public HealthReportJob job_info { get; set; }
    }

    /// <summary>
    /// 获取健康上报答案请求。
    /// </summary>
    public class HealthGetReportAnswerRequest : HealthGetReportJobInfoRequest
    {
        /// <summary>
        /// 分页起始位置。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 每页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 健康上报单个问题的答案。
    /// </summary>
    public class HealthReportValue
    {
        /// <summary>
        /// 问题 ID。
        /// </summary>
        public int question_id { get; set; }

        /// <summary>
        /// 单选题选项 ID。
        /// </summary>
        public int? single_choice { get; set; }

        /// <summary>
        /// 多选题选项 ID 列表。
        /// </summary>
        public IList<int> multi_choice { get; set; }

        /// <summary>
        /// 填空题答案。
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 上传文件 ID 列表。
        /// </summary>
        public IList<string> fileid { get; set; }

        /// <summary>
        /// 行程卡类型。
        /// </summary>
        public int? itinerary_card_type { get; set; }

        /// <summary>
        /// 高风险行程信息。
        /// </summary>
        public string high_risk_area { get; set; }
    }

    /// <summary>
    /// 一名成员、学生或家长提交的健康上报答案。
    /// </summary>
    public class HealthReportAnswer
    {
        /// <summary>
        /// 填写人 ID 类型。
        /// </summary>
        public int id_type { get; set; }

        /// <summary>
        /// 填写人成员账号。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 填写人学生账号。
        /// </summary>
        public string student_userid { get; set; }

        /// <summary>
        /// 填写人家长账号。
        /// </summary>
        public string parent_userid { get; set; }

        /// <summary>
        /// 问题答案列表。
        /// </summary>
        public IList<HealthReportValue> report_values { get; set; }

        /// <summary>
        /// 上报时间戳，单位为秒。
        /// </summary>
        public long report_time { get; set; }
    }

    /// <summary>
    /// 获取健康上报答案结果。
    /// </summary>
    public class HealthGetReportAnswerResult : WorkJsonResult
    {
        /// <summary>
        /// 健康上报答案列表。
        /// </summary>
        public IList<HealthReportAnswer> answers { get; set; }
    }
}
