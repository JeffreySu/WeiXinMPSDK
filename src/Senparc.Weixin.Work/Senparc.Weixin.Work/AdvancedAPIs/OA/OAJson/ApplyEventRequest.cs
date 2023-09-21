using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public class ApplyEventRequest
    {
        /// <summary>
        /// 申请人userid，此审批申请将以此员工身份提交，申请人需在应用可见范围内
        /// </summary>
        public string creator_userid { get; set; }

        /// <summary>
        /// 模板id。可在“获取审批申请详情”、“审批状态变化回调通知”中获得，也可在审批模板的模板编辑页面链接中获得。暂不支持通过接口提交[打卡补卡][调班]模板审批单。
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 审批人模式：0-通过接口指定审批人、抄送人（此时approver、notifyer等参数可用）; 1-使用此模板在管理后台设置的审批流程(需要保证审批流程中没有“申请人自选”节点)，支持条件审批。默认为0
        /// </summary>
        public int use_template_approver { get; set; }

        /// <summary>
        /// 提单者提单部门id，不填默认为主部门
        /// 非必填
        /// </summary>
        public int choose_department { get; set; }

        /// <summary>
        /// 审批流程信息，用于指定审批申请的审批流程，支持单人审批、多人会签、多人或签，可能有多个审批节点，仅use_template_approver为0时生效。
        /// </summary>
        public List<ApplyEventRequest_Approver> approver { get; set; }

        /// <summary>
        /// 抄送人节点userid列表，仅use_template_approver为0时生效。
        /// </summary>
        public List<string> notifyer { get; set; }

        /// <summary>
        /// 抄送方式：1-提单时抄送（默认值）； 2-单据通过后抄送；3-提单和单据通过后抄送。仅use_template_approver为0时生效。
        /// </summary>
        public int notify_type { get; set; }

        /// <summary>
        /// 审批申请数据，可定义审批申请中各个控件的值，其中必填项必须有值，选填项可为空，数据结构同“获取审批申请详情”接口返回值中同名参数“apply_data”
        /// </summary>
        public ApplyEventRequest_ApplyData apply_data { get; set; }

        /// <summary>
        /// 摘要信息，用于显示在审批通知卡片、审批列表的摘要信息，最多3行
        /// </summary>
        public List<ApplyEventRequest_SummaryList> summary_list { get; set; }
    }

    public class ApplyEventRequest_TextLang
    {
        /// <summary>
        /// 摘要行显示文字，用于记录列表和消息通知的显示，不要超过20个字符
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 摘要行显示语言，中文：zh_CN（注意不是zh-CN），英文：en。
        /// </summary>
        public string lang { get; set; }
    }

    public class ApplyEventRequest_Approver
    {
        /// <summary>
        /// 节点审批方式：1-或签；2-会签，仅在节点为多人审批时有效
        /// </summary>
        public int attr { get; set; }

        /// <summary>
        /// 审批节点审批人userid列表，若为多人会签、多人或签，需填写每个人的userid
        /// </summary>
        public List<string> userid { get; set; }
    }

    public class ApplyEventRequest_ApplyData
    {
        /// <summary>
        /// 审批申请详情，由多个表单控件及其内容组成，其中包含需要对控件赋值的信息
        /// </summary>
        public List<ApplyEventRequest_ApplyData_Contents> contents { get; set; }
    }

    public class ApplyEventRequest_ApplyData_Contents
    {
        /// <summary>
        /// 控件类型：Text-文本；Textarea-多行文本；Number-数字；Money-金额；Date-日期/日期+时间；Selector-单选/多选；；Contact-成员/部门；Tips-说明文字；File-附件；Table-明细；Location-位置；RelatedApproval-关联审批单；Formula-公式；DateRange-时长；
        /// </summary>
        public string control { get; set; }

        /// <summary>
        /// 控件id：控件的唯一id，可通过“获取审批模板详情”接口获取
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 控件值 ，需在此为申请人在各个控件中填写内容不同控件有不同的赋值参数，具体说明详见附录。模板配置的控件属性为必填时，对应value值需要有值。
        /// </summary>
        public ApplyEventRequest_ApplyData_Contents_Value value { get; set; }
    }

    public class ApplyEventRequest_ApplyData_Contents_Value
    {
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
    }

    public class ApplyEventRequest_SummaryList
    {
        /// <summary>
        /// 摘要行信息，用于定义某一行摘要显示的内容
        /// </summary>
        public List<ApplyEventRequest_TextLang> summary_info { get; set; }
    }
}
