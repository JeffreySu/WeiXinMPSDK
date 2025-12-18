/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：ApplyEventRequest.cs
    文件功能描述：提交审批申请 请求参数
    
    
    创建标识：Senparc - 20251218
    
    修改标识：Senparc - 20251218
    修改描述：添加对所有控件类型的支持，包括明细控件（Table）的 children 属性
    
----------------------------------------------------------------*/

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

    /// <summary>
    /// 控件值，不同控件类型对应不同的参数
    /// 参考文档：https://developer.work.weixin.qq.com/document/path/91853
    /// </summary>
    public class ApplyEventRequest_ApplyData_Contents_Value
    {
        /// <summary>
        /// 文本/多行文本控件（control为Text或Textarea）的值
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 数字控件（control为Number）的值
        /// </summary>
        public string new_number { get; set; }

        /// <summary>
        /// 金额控件（control为Money）的值
        /// </summary>
        public string new_money { get; set; }

        /// <summary>
        /// 日期/日期+时间控件（control为Date）的值
        /// </summary>
        public ApplyEventRequest_Date date { get; set; }

        /// <summary>
        /// 单选/多选控件（control为Selector）的值
        /// </summary>
        public ApplyEventRequest_Selector selector { get; set; }

        /// <summary>
        /// 成员控件（control为Contact，mode为user）的值
        /// </summary>
        public List<ApplyEventRequest_Member> members { get; set; }

        /// <summary>
        /// 部门控件（control为Contact，mode为department）的值
        /// </summary>
        public List<ApplyEventRequest_Department> departments { get; set; }

        /// <summary>
        /// 附件控件（control为File）的值
        /// </summary>
        public List<ApplyEventRequest_File> files { get; set; }

        /// <summary>
        /// 明细控件（control为Table）的值，包含多行子控件数据
        /// </summary>
        public List<ApplyEventRequest_TableChildren> children { get; set; }

        /// <summary>
        /// 位置控件（control为Location）的值
        /// </summary>
        public ApplyEventRequest_Location location { get; set; }

        /// <summary>
        /// 关联审批单控件（control为RelatedApproval）的值
        /// </summary>
        public List<ApplyEventRequest_RelatedApproval> related_approval { get; set; }

        /// <summary>
        /// 时长控件（control为DateRange）的值
        /// </summary>
        public ApplyEventRequest_DateRange date_range { get; set; }

        /// <summary>
        /// 公式控件（control为Formula）的值
        /// </summary>
        public string formula { get; set; }

        /// <summary>
        /// 假勤控件-请假（control为Vacation）的值
        /// </summary>
        public ApplyEventRequest_Vacation vacation { get; set; }

        /// <summary>
        /// 假勤控件-出差/外出/加班（control为Attendance）的值
        /// </summary>
        public ApplyEventRequest_Attendance attendance { get; set; }
    }

    #region 各控件类型的值类

    /// <summary>
    /// 日期控件的值
    /// </summary>
    public class ApplyEventRequest_Date
    {
        /// <summary>
        /// 时间展示类型：day-日期；hour-日期+时间
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 时间戳，填写UTC时间戳
        /// </summary>
        public long s_timestamp { get; set; }
    }

    /// <summary>
    /// 单选/多选控件的值
    /// </summary>
    public class ApplyEventRequest_Selector
    {
        /// <summary>
        /// 选择方式：single-单选；multi-多选
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 选中的选项
        /// </summary>
        public List<ApplyEventRequest_SelectorOption> options { get; set; }
    }

    /// <summary>
    /// 单选/多选控件的选项
    /// </summary>
    public class ApplyEventRequest_SelectorOption
    {
        /// <summary>
        /// 选项key，可通过"获取审批模板详情"接口获得
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 选项的值（用于填写其他类型选项的值）
        /// </summary>
        public List<ApplyEventRequest_TextLang> value { get; set; }
    }

    /// <summary>
    /// 成员控件的成员值
    /// </summary>
    public class ApplyEventRequest_Member
    {
        /// <summary>
        /// 成员userid
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 成员名称，若接口没有返回此字段可不填
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 部门控件的部门值
    /// </summary>
    public class ApplyEventRequest_Department
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public string openapi_id { get; set; }

        /// <summary>
        /// 部门名称，若接口没有返回此字段可不填
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 附件控件的附件值
    /// </summary>
    public class ApplyEventRequest_File
    {
        /// <summary>
        /// 文件id，通过上传临时素材接口获取
        /// </summary>
        public string file_id { get; set; }
    }

    /// <summary>
    /// 明细控件的子控件行数据（一行）
    /// </summary>
    public class ApplyEventRequest_TableChildren
    {
        /// <summary>
        /// 明细行中每个子控件的值
        /// </summary>
        public List<ApplyEventRequest_ApplyData_Contents> list { get; set; }
    }

    /// <summary>
    /// 位置控件的值
    /// </summary>
    public class ApplyEventRequest_Location
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 地点标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 地点详细地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 选择的打卡时间（秒），仅假勤组件的外出地点返回
        /// </summary>
        public long time { get; set; }
    }

    /// <summary>
    /// 关联审批单控件的值
    /// </summary>
    public class ApplyEventRequest_RelatedApproval
    {
        /// <summary>
        /// 关联审批单号
        /// </summary>
        public string sp_no { get; set; }
    }

    /// <summary>
    /// 时长控件的值
    /// </summary>
    public class ApplyEventRequest_DateRange
    {
        /// <summary>
        /// 时间展示类型：halfday-日期；hour-日期+时间
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public ApplyEventRequest_DateRangeData new_begin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public ApplyEventRequest_DateRangeData new_end { get; set; }

        /// <summary>
        /// 时长秒数
        /// </summary>
        public long new_duration { get; set; }
    }

    /// <summary>
    /// 时长控件的时间范围数据
    /// </summary>
    public class ApplyEventRequest_DateRangeData
    {
        /// <summary>
        /// 时间戳，填写UTC时间戳
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 类型：0-上午；1-下午；仅当type为halfday时有效
        /// </summary>
        public int time_type { get; set; }
    }

    /// <summary>
    /// 假勤控件-请假的值
    /// </summary>
    public class ApplyEventRequest_Vacation
    {
        /// <summary>
        /// 请假类型key，可通过获取审批模板详情接口获取
        /// </summary>
        public ApplyEventRequest_VacationSelector selector { get; set; }

        /// <summary>
        /// 请假的日期范围
        /// </summary>
        public ApplyEventRequest_VacationAttendance attendance { get; set; }
    }

    /// <summary>
    /// 请假类型选择器
    /// </summary>
    public class ApplyEventRequest_VacationSelector
    {
        /// <summary>
        /// 选择方式，固定为single
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 选择的请假类型
        /// </summary>
        public List<ApplyEventRequest_SelectorOption> options { get; set; }
    }

    /// <summary>
    /// 请假日期范围信息
    /// </summary>
    public class ApplyEventRequest_VacationAttendance
    {
        /// <summary>
        /// 时间范围
        /// </summary>
        public ApplyEventRequest_AttendanceDateRange date_range { get; set; }
    }

    /// <summary>
    /// 假勤控件-出差/外出/加班的值
    /// </summary>
    public class ApplyEventRequest_Attendance
    {
        /// <summary>
        /// 时长控件
        /// </summary>
        public ApplyEventRequest_AttendanceDateRange date_range { get; set; }
    }

    /// <summary>
    /// 假勤控件的日期范围
    /// </summary>
    public class ApplyEventRequest_AttendanceDateRange
    {
        /// <summary>
        /// 时间展示类型：halfday-日期；hour-日期+时间
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public ApplyEventRequest_AttendanceDateRangeData new_begin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public ApplyEventRequest_AttendanceDateRangeData new_end { get; set; }

        /// <summary>
        /// 时长秒数
        /// </summary>
        public long new_duration { get; set; }
    }

    /// <summary>
    /// 假勤日期范围数据
    /// </summary>
    public class ApplyEventRequest_AttendanceDateRangeData
    {
        /// <summary>
        /// 时间戳，填写UTC时间戳
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 类型：0-上午；1-下午；仅当type为halfday时有效
        /// </summary>
        public int time_type { get; set; }
    }

    #endregion

    public class ApplyEventRequest_SummaryList
    {
        /// <summary>
        /// 摘要行信息，用于定义某一行摘要显示的内容
        /// </summary>
        public List<ApplyEventRequest_TextLang> summary_info { get; set; }
    }
}
