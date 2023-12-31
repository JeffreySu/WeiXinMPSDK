/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：ApprovalCreateTemplateRequest.cs
    文件功能描述：创建审批模板 请求


    创建标识：Senparc - 20230224

    修改标识：Senparc - 20231128
    修改描述：修复：template_names 命名更正为：template_name

----------------------------------------------------------------*/
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 创建审批模板 请求
    /// </summary>
    public class ApprovalCreateTemplateRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ApprovalCreateTemplateRequest_TextAndLang> template_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent template_content { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ApprovalCreateTemplateRequest_TextAndLang
    {
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lang { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ApprovalCreateTemplateRequest_TemplateContent
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ApprovalCreateTemplateRequest_TemplateContent_Controls> controls { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls
    {
        /// <summary>
        /// 模板控件属性，包含了模板内控件的各种属性信息
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Property property { get; set; }

        /// <summary>
        /// 模板控件配置，包含了部分控件类型的附加类型、属性，详见附录说明。目前有配置信息的控件类型有：Date-日期/日期+时间；Selector-单选/多选；Contact-成员/部门；Table-明细；Attendance-假勤组件（请假、外出、出差、加班）
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config config { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Property
    {
        /// <summary>
        /// 控件类型：Text-文本；Textarea-多行文本；Number-数字；Money-金额；Date-日期/日期+时间；Selector-单选/多选；Contact-成员/部门；Tips-说明文字；File-附件；Table-明细；Attendance-假勤控件；Vacation-请假控件；Location-位置；RelatedApproval-关联审批单；Formula-公式；DateRange-时长
        /// </summary>
        public string control { get; set; }

        /// <summary>
        /// 控件id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 控件名称，若配置了多语言则会包含中英文的控件名称，默认为zh_CN中文
        /// </summary>
        public List<ApprovalCreateTemplateRequest_TextAndLang> title { get; set; }

        /// <summary>
        /// 控件说明，向申请者展示的控件填写说明，若配置了多语言则会包含中英文的控件说明，默认为zh_CN中文
        /// </summary>
        public List<ApprovalCreateTemplateRequest_TextAndLang> placeholder { get; set; }

        /// <summary>
        /// 是否必填：1-必填；0-非必填
        /// </summary>
        public int require { get; set; }

        /// <summary>
        /// 是否参与打印：1-不参与打印；0-参与打印
        /// </summary>
        public int un_print { get; set; }
    }

    /// <summary>
    /// 附1 文本/多行文本控件（control参数为Text或Textarea） 文本/多行文本控件中config不需要填写
    /// 附2 数字控件（control参数为Number） 数字控件中config不需要填写
    /// 附3 金额控件（control参数为Money） 金额控件中config不需要填写
    /// 附4 日期/日期+时间控件（control参数为Date）
    /// 附5 单选/多选控件（control参数为Selector）
    /// 附6 成员控件（control参数为Contact）
    /// 附7 说明文字控件（control参数为Tips） 说明文字控件中config不需要填写
    /// 附8 附件控件（control参数为File，且value参数为files）
    /// 附9 明细控件（control参数为Table）
    /// 附10 假勤组件-请假组件（control参数为Vacation）
    /// 附11 假勤组件-出差/外出/加班组件（control参数为Attendance）
    /// 附12 位置控件（control参数为Location）
    /// 附13 关联审批单控件（control参数为RelatedApproval）
    /// 
    /// </summary>
    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config
    {
        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Date date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Selector selector { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Contact contact { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_File file { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table table { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Attendance attendance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Location location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_RelatedApproval related_approval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_DateRange date_range { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Date
    {
        /// <summary>
        /// 时间展示类型：day-日期；hour-日期+时间 ，和对应模板控件属性一致
        /// </summary>
        public string type { get; set; }

        public List<ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Selector_Options> options { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Selector
    {
        /// <summary>
        /// 选择方式：single-单选；multi-多选
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 多选选项，多选属性的选择控件允许输入多个
        /// </summary>
        public List<ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Selector_Options> options { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Selector_Options
    {
        /// <summary>
        /// 选项key
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 选项说明，text和lang规则同上
        /// </summary>
        public List<ApprovalCreateTemplateRequest_TextAndLang> value { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Contact
    {
        /// <summary>
        /// single-单选、multi-多选
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// user-成员、department-部门
        /// </summary>
        public string mode { get; set; }
    }


    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_File
    {
        /// <summary>
        /// 是否只允许拍照，1--是， 0--否
        /// </summary>
        public int is_only_photo { get; set; }
    }


    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table
    {
        /// <summary>
        /// 是否只允许拍照，1--是， 0--否
        /// </summary>
        public int print_format { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table_Children> children { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table_Children
    {
        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table_Children_Property property { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table_Children_Config config { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table_Children_Property
    {
        /// <summary>
        /// 是否只允许拍照，1--是， 0--否
        /// </summary>
        public string control { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ApplyEventRequest_TextLang> title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ApplyEventRequest_TextLang> placeholder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int require { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int un_print { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table_Children_Config
    {
        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table_Children_Config_File file { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Table_Children_Config_File
    {
        /// <summary>
        /// 
        /// </summary>
        public int is_only_photo { get; set; }
    }


    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Attendance
    {
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Attendance_DateRange date_range { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Attendance_DateRange
    {
        /// <summary>
        /// 时间展示类型：halfday-日期；hour-日期+时间
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 0-自然日；1-工作日
        /// </summary>
        public int official_holiday { get; set; }

        /// <summary>
        /// 一天的时长（单位为秒），必须大于0小于等于86400
        /// </summary>
        public int perday_duration { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_Location
    {
        /// <summary>
        /// 距离，目前支持100、200、300
        /// </summary>
        public int distance { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_RelatedApproval
    {
        /// <summary>
        /// 关联审批单的template_id ，不填时表示可以关联所有模版，该template_id可通过获取审批模版接口获取
        /// </summary>
        public List<string> template_id { get; set; }
    }

    public class ApprovalCreateTemplateRequest_TemplateContent_Controls_Config_DateRange
    {
        /// <summary>
        /// 时间展示类型：halfday-日期；hour-日期+时间
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 0-自然日；1-工作日
        /// </summary>
        public int official_holiday { get; set; }

        /// <summary>
        /// 一天的时长（单位为秒），必须大于0小于等于86400
        /// </summary>
        public int perday_duration { get; set; }
    }
}
