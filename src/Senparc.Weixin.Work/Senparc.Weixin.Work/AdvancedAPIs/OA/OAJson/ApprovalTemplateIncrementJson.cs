/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ApprovalTemplateIncrementJson.cs
    文件功能描述：审批模板新增控件配置模型

    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐审批模板新增控件配置模型
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>审批模板富文本说明配置。</summary>
    public class ApprovalTemplateTipsConfig
    {
        /// <summary>不同语言的说明文字。</summary>
        public List<ApprovalTemplateTipsContent> tips_content { get; set; }
    }

    /// <summary>单一语言的审批模板说明文字。</summary>
    public class ApprovalTemplateTipsContent
    {
        /// <summary>富文本内容。</summary>
        public ApprovalTemplateRichText text { get; set; }

        /// <summary>语言标识。</summary>
        public string lang { get; set; }
    }

    /// <summary>审批模板富文本。</summary>
    public class ApprovalTemplateRichText
    {
        /// <summary>富文本分段。</summary>
        public List<ApprovalTemplateRichTextSegment> sub_text { get; set; }
    }

    /// <summary>审批模板富文本分段。</summary>
    public class ApprovalTemplateRichTextSegment
    {
        /// <summary>文本类型：1-纯文本；2-链接。</summary>
        public int type { get; set; }

        /// <summary>分段内容。</summary>
        public ApprovalTemplateRichTextSegmentContent content { get; set; }
    }

    /// <summary>审批模板富文本分段内容。</summary>
    public class ApprovalTemplateRichTextSegmentContent
    {
        /// <summary>纯文本内容。</summary>
        public ApprovalTemplatePlainText plain_text { get; set; }

        /// <summary>链接内容。</summary>
        public ApprovalTemplateLink link { get; set; }
    }

    /// <summary>审批模板纯文本。</summary>
    public class ApprovalTemplatePlainText
    {
        /// <summary>文本内容。</summary>
        public string content { get; set; }
    }

    /// <summary>审批模板链接。</summary>
    public class ApprovalTemplateLink
    {
        /// <summary>链接标题。</summary>
        public string title { get; set; }

        /// <summary>链接地址。</summary>
        public string url { get; set; }
    }

    /// <summary>审批模板详情中的日期配置。</summary>
    public class ApprovalTemplateDateConfig
    {
        /// <summary>时间展示类型：day-日期；hour-日期和时间。</summary>
        public string type { get; set; }
    }

    /// <summary>审批模板详情中的成员或部门配置。</summary>
    public class ApprovalTemplateContactConfig
    {
        /// <summary>选择方式：single 或 multi。</summary>
        public string type { get; set; }

        /// <summary>选择对象：user 或 department。</summary>
        public string mode { get; set; }
    }

    /// <summary>审批模板详情中的明细配置。</summary>
    public class ApprovalTemplateTableConfig
    {
        /// <summary>明细内的子控件。</summary>
        public List<GetTemplateDetailResult_TemplateContent_Controls> children { get; set; }

        /// <summary>统计字段 ID。</summary>
        public List<string> stat_field { get; set; }
    }

    /// <summary>审批模板详情中的假勤配置。</summary>
    public class ApprovalTemplateAttendanceConfig
    {
        /// <summary>假勤控件类型。</summary>
        public int type { get; set; }

        /// <summary>时间范围配置。</summary>
        public ApprovalTemplateAttendanceDateRange date_range { get; set; }
    }

    /// <summary>审批模板假勤时间范围配置。</summary>
    public class ApprovalTemplateAttendanceDateRange
    {
        /// <summary>时间刻度：hour 或 halfday。</summary>
        public string type { get; set; }
    }

    /// <summary>审批模板详情中的假期类型列表。</summary>
    public class ApprovalTemplateVacationList
    {
        /// <summary>假期类型。</summary>
        public List<ApprovalTemplateVacationItem> item { get; set; }
    }

    /// <summary>审批模板假期类型。</summary>
    public class ApprovalTemplateVacationItem
    {
        /// <summary>假期类型 ID。</summary>
        public int id { get; set; }

        /// <summary>假期类型多语言名称。</summary>
        public List<GetTemplateDetailResult_TextAndLang> name { get; set; }
    }

    /// <summary>审批模板选择项的控件关联。</summary>
    public class ApprovalTemplateOptionRelation
    {
        /// <summary>选项 Key。</summary>
        public string key { get; set; }

        /// <summary>关联控件。</summary>
        public List<ApprovalTemplateRelatedControl> relation_list { get; set; }
    }

    /// <summary>审批模板关联控件。</summary>
    public class ApprovalTemplateRelatedControl
    {
        /// <summary>关联控件 ID。</summary>
        public string related_control_id { get; set; }

        /// <summary>关联动作。</summary>
        public int action { get; set; }
    }
}
