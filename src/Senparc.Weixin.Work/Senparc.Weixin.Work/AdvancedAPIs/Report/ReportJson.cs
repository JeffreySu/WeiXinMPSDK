/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ReportJson.cs
    文件功能描述：企业微信汇报强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐汇报表单控件强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    /// <summary>汇报记录筛选条件。</summary>
    public class ReportRecordFilter
    {
        /// <summary>筛选键：creator、department 或 template_id。</summary>
        public string key { get; set; }

        /// <summary>筛选值。</summary>
        public string value { get; set; }
    }

    /// <summary>批量获取汇报记录单号请求。</summary>
    public class GetReportRecordListRequest
    {
        /// <summary>查询开始时间的 Unix 时间戳。</summary>
        public uint starttime { get; set; }

        /// <summary>查询结束时间的 Unix 时间戳，与开始时间跨度不能超过一个月。</summary>
        public uint endtime { get; set; }

        /// <summary>分页游标，首次请求填写 0。</summary>
        public uint cursor { get; set; }

        /// <summary>每页数量，最大为 100。</summary>
        public uint limit { get; set; }

        /// <summary>可选筛选条件。</summary>
        public IList<ReportRecordFilter> filters { get; set; }
    }

    /// <summary>批量获取汇报记录单号结果。</summary>
    public class GetReportRecordListResult : WorkJsonResult
    {
        /// <summary>汇报记录单号列表。</summary>
        public IList<string> journaluuid_list { get; set; }

        /// <summary>下一页游标。</summary>
        public uint next_cursor { get; set; }

        /// <summary>结束标记：0 还有数据，1 已结束。</summary>
        public uint endflag { get; set; }
    }

    /// <summary>获取汇报记录详情请求。</summary>
    public class GetReportRecordDetailRequest
    {
        /// <summary>汇报记录单号。</summary>
        public string journaluuid { get; set; }
    }

    /// <summary>汇报用户。</summary>
    public class ReportUser
    {
        /// <summary>成员 UserId。</summary>
        public string userid { get; set; }
    }

    /// <summary>汇报表单中的本地化文本。</summary>
    public class ReportLocalizedText
    {
        /// <summary>文本内容。</summary>
        public string text { get; set; }
    }

    /// <summary>日期控件值。</summary>
    public class ReportDateValue
    {
        /// <summary>展示类型：day、hour、month 或 minute。</summary>
        public string type { get; set; }

        /// <summary>字符串形式的 Unix 时间戳。</summary>
        public string s_timestamp { get; set; }
    }

    /// <summary>选择控件选项。</summary>
    public class ReportSelectorOption
    {
        /// <summary>选项键。</summary>
        public string key { get; set; }

        /// <summary>选项的本地化文本。</summary>
        public IList<ReportLocalizedText> value { get; set; }
    }

    /// <summary>选择控件值。</summary>
    public class ReportSelectorValue
    {
        /// <summary>选择类型：single 或 multi。</summary>
        public string type { get; set; }

        /// <summary>被选中的选项。</summary>
        public IList<ReportSelectorOption> options { get; set; }
    }

    /// <summary>汇报部门。</summary>
    public class ReportDepartment
    {
        /// <summary>部门的开放接口 ID。</summary>
        public string openapi_id { get; set; }
    }

    /// <summary>汇报附件。</summary>
    public class ReportFile
    {
        /// <summary>文件的临时素材 MediaId。</summary>
        public string file_id { get; set; }
    }

    /// <summary>明细控件中的一行。</summary>
    public class ReportTableRow
    {
        /// <summary>该行中的子控件。</summary>
        public IList<ReportContent> list { get; set; }
    }

    /// <summary>时长控件值。</summary>
    public class ReportDateRangeValue
    {
        /// <summary>展示类型：halfday 或 hour。</summary>
        public string type { get; set; }

        /// <summary>开始时间的 Unix 时间戳。</summary>
        public ulong new_begin { get; set; }

        /// <summary>结束时间的 Unix 时间戳。</summary>
        public ulong new_end { get; set; }

        /// <summary>时长，单位为秒。</summary>
        public ulong new_duration { get; set; }
    }

    /// <summary>位置控件值。</summary>
    public class ReportLocationValue
    {
        /// <summary>纬度字符串，精确到六位小数。</summary>
        public string latitude { get; set; }

        /// <summary>经度字符串，精确到六位小数。</summary>
        public string longitude { get; set; }

        /// <summary>地点标题。</summary>
        public string title { get; set; }

        /// <summary>地点详细地址。</summary>
        public string address { get; set; }

        /// <summary>选择地点时间的 Unix 时间戳；官方已标记废弃。</summary>
        public ulong time { get; set; }
    }

    /// <summary>公式控件值。</summary>
    public class ReportFormulaValue
    {
        /// <summary>公式计算结果。</summary>
        public string value { get; set; }
    }

    /// <summary>学生控件值。</summary>
    public class ReportStudent
    {
        /// <summary>学生姓名。</summary>
        public string name { get; set; }
    }

    /// <summary>班级控件值。</summary>
    public class ReportClass
    {
        /// <summary>班级名称。</summary>
        public string name { get; set; }
    }

    /// <summary>文档控件值。</summary>
    public class ReportDocument
    {
        /// <summary>文档 ID。</summary>
        public string docid { get; set; }

        /// <summary>文档访问地址。</summary>
        public string doc_url { get; set; }
    }

    /// <summary>微盘附件控件值。</summary>
    public class ReportWeDriveFile
    {
        /// <summary>微盘文件 ID。</summary>
        public string fileid { get; set; }
    }

    /// <summary>
    /// 官方文档未公开元素结构的兼容占位值。
    /// </summary>
    /// <remarks>用于保留统一返回示例中的空数组字段，并避免使用 object 破坏 AOT 友好性。</remarks>
    public class ReportUnspecifiedValue
    {
    }

    /// <summary>汇报表单控件值。</summary>
    public class ReportControlValue
    {
        /// <summary>文本或多行文本值。</summary>
        public string text { get; set; }

        /// <summary>数字控件的字符串值。</summary>
        public string new_number { get; set; }

        /// <summary>金额控件的字符串值。</summary>
        public string new_money { get; set; }

        /// <summary>日期控件值。</summary>
        public ReportDateValue date { get; set; }

        /// <summary>单选或多选控件值。</summary>
        public ReportSelectorValue selector { get; set; }

        /// <summary>成员控件值。</summary>
        public IList<ReportUser> members { get; set; }

        /// <summary>部门控件值。</summary>
        public IList<ReportDepartment> departments { get; set; }

        /// <summary>说明文字兼容字段；正式控件的 value 为空。</summary>
        public IList<ReportUnspecifiedValue> tips { get; set; }

        /// <summary>附件控件值。</summary>
        public IList<ReportFile> files { get; set; }

        /// <summary>明细控件值。</summary>
        public IList<ReportTableRow> children { get; set; }

        /// <summary>时长控件值。</summary>
        public ReportDateRangeValue date_range { get; set; }

        /// <summary>位置控件值。</summary>
        public ReportLocationValue location { get; set; }

        /// <summary>公式控件值。</summary>
        public ReportFormulaValue formula { get; set; }

        /// <summary>学生控件值。</summary>
        public IList<ReportStudent> students { get; set; }

        /// <summary>班级控件值。</summary>
        public IList<ReportClass> classes { get; set; }

        /// <summary>文档控件值。</summary>
        public IList<ReportDocument> docs { get; set; }

        /// <summary>微盘附件控件值。</summary>
        public IList<ReportWeDriveFile> wedrive_files { get; set; }

        /// <summary>官方仅在统一示例中以空数组给出的统计字段。</summary>
        public IList<ReportUnspecifiedValue> stat_field { get; set; }

        /// <summary>官方仅在统一示例中以空数组给出的合计字段。</summary>
        public IList<ReportUnspecifiedValue> sum_field { get; set; }

        /// <summary>官方仅在统一示例中以空数组给出的关联审批字段。</summary>
        public IList<ReportUnspecifiedValue> related_approval { get; set; }
    }

    /// <summary>汇报表单中的一个控件。</summary>
    public class ReportContent
    {
        /// <summary>控件类型。</summary>
        public string control { get; set; }

        /// <summary>控件 ID。</summary>
        public string id { get; set; }

        /// <summary>控件标题。</summary>
        public IList<ReportLocalizedText> title { get; set; }

        /// <summary>控件值。</summary>
        public ReportControlValue value { get; set; }
    }

    /// <summary>汇报表单数据。</summary>
    public class ReportApplyData
    {
        /// <summary>表单控件列表。</summary>
        public IList<ReportContent> contents { get; set; }
    }

    /// <summary>汇报评论。</summary>
    public class ReportComment
    {
        /// <summary>评论 ID。</summary>
        public ulong commentid { get; set; }

        /// <summary>被回复的评论 ID；0 表示直接评论汇报。</summary>
        public ulong tocommentid { get; set; }

        /// <summary>评论者信息。</summary>
        public ReportUser comment_userinfo { get; set; }

        /// <summary>评论内容。</summary>
        public string content { get; set; }

        /// <summary>评论时间的 Unix 时间戳。</summary>
        public ulong comment_time { get; set; }
    }

    /// <summary>汇报记录详情。</summary>
    public class ReportRecordInfo
    {
        /// <summary>汇报记录单号。</summary>
        public string journal_uuid { get; set; }

        /// <summary>汇报模板名称。</summary>
        public string template_name { get; set; }

        /// <summary>汇报模板 ID。</summary>
        public string template_id { get; set; }

        /// <summary>汇报时间的 Unix 时间戳。</summary>
        public ulong report_time { get; set; }

        /// <summary>提交者。</summary>
        public ReportUser submitter { get; set; }

        /// <summary>接收者列表。</summary>
        public IList<ReportUser> receivers { get; set; }

        /// <summary>已读接收者列表。</summary>
        public IList<ReportUser> readed_receivers { get; set; }

        /// <summary>表单数据。</summary>
        public ReportApplyData apply_data { get; set; }

        /// <summary>系统生成的富文本汇报内容。</summary>
        public string sys_journal_data { get; set; }

        /// <summary>评论列表。</summary>
        public IList<ReportComment> comments { get; set; }
    }

    /// <summary>获取汇报记录详情结果。</summary>
    public class GetReportRecordDetailResult : WorkJsonResult
    {
        /// <summary>汇报记录详情。</summary>
        public ReportRecordInfo info { get; set; }
    }

    /// <summary>获取汇报统计数据请求。</summary>
    public class GetReportStatListRequest
    {
        /// <summary>汇报模板 ID。</summary>
        public string template_id { get; set; }

        /// <summary>统计开始时间的 Unix 时间戳。</summary>
        public ulong starttime { get; set; }

        /// <summary>统计结束时间的 Unix 时间戳，与开始时间跨度不能超过一年。</summary>
        public ulong endtime { get; set; }
    }

    /// <summary>汇报范围中的部门。</summary>
    public class ReportParty
    {
        /// <summary>部门的开放 ID。</summary>
        public string open_partyid { get; set; }
    }

    /// <summary>汇报范围中的标签。</summary>
    public class ReportTag
    {
        /// <summary>标签的开放 ID。</summary>
        public string open_tagid { get; set; }
    }

    /// <summary>汇报模板的可见或白名单范围。</summary>
    public class ReportRange
    {
        /// <summary>成员列表。</summary>
        public IList<ReportUser> user_list { get; set; }

        /// <summary>部门列表。</summary>
        public IList<ReportParty> party_list { get; set; }

        /// <summary>标签列表。</summary>
        public IList<ReportTag> tag_list { get; set; }
    }

    /// <summary>汇报接收人中的负责人级别。</summary>
    public class ReportLeader
    {
        /// <summary>负责人级别。</summary>
        public ulong level { get; set; }
    }

    /// <summary>汇报接收人范围。</summary>
    public class ReportReceiverRange
    {
        /// <summary>成员列表。</summary>
        public IList<ReportUser> user_list { get; set; }

        /// <summary>标签列表。</summary>
        public IList<ReportTag> tag_list { get; set; }

        /// <summary>负责人级别列表。</summary>
        public IList<ReportLeader> leader_list { get; set; }
    }

    /// <summary>统计中的单条汇报记录。</summary>
    public class ReportStatItem
    {
        /// <summary>汇报记录单号。</summary>
        public string journaluuid { get; set; }

        /// <summary>汇报时间的 Unix 时间戳。</summary>
        public ulong reporttime { get; set; }

        /// <summary>汇报状态标记。</summary>
        public uint flag { get; set; }
    }

    /// <summary>成员的汇报统计。</summary>
    public class ReportUserStat
    {
        /// <summary>成员信息。</summary>
        public ReportUser user { get; set; }

        /// <summary>成员的汇报记录列表。</summary>
        public IList<ReportStatItem> itemlist { get; set; }
    }

    /// <summary>单个汇报模板的统计数据。</summary>
    public class ReportStatistics
    {
        /// <summary>汇报模板 ID。</summary>
        public string template_id { get; set; }

        /// <summary>汇报模板名称。</summary>
        public string template_name { get; set; }

        /// <summary>汇报范围。</summary>
        public ReportRange report_range { get; set; }

        /// <summary>白名单范围。</summary>
        public ReportRange white_range { get; set; }

        /// <summary>接收人范围。</summary>
        public ReportReceiverRange receivers { get; set; }

        /// <summary>汇报周期开始时间。</summary>
        public ulong cycle_begin_time { get; set; }

        /// <summary>汇报周期结束时间。</summary>
        public ulong cycle_end_time { get; set; }

        /// <summary>统计开始时间。</summary>
        public ulong stat_begin_time { get; set; }

        /// <summary>统计结束时间。</summary>
        public ulong stat_end_time { get; set; }

        /// <summary>已汇报成员列表。</summary>
        public IList<ReportUserStat> report_list { get; set; }

        /// <summary>未汇报成员列表。</summary>
        public IList<ReportUserStat> unreport_list { get; set; }

        /// <summary>汇报类型：2 日报、3 周报、4 月报。</summary>
        public uint report_type { get; set; }
    }

    /// <summary>获取汇报统计数据结果。</summary>
    public class GetReportStatListResult : WorkJsonResult
    {
        /// <summary>汇报统计列表。</summary>
        public IList<ReportStatistics> stat_list { get; set; }
    }
}
