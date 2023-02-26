

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 上传临时媒体文件返回结果
    /// </summary>
    public class GetTemplateDetailResult : WorkJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetTemplateDetailResult_TextAndLang> template_names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetTemplateDetailResult_TemplateContent template_content { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetTemplateDetailResult_TextAndLang
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
    public class GetTemplateDetailResult_TemplateContent
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetTemplateDetailResult_TemplateContent_Controls> controls { get; set; }
    }

    public class GetTemplateDetailResult_TemplateContent_Controls
    {
        /// <summary>
        /// 模板控件属性，包含了模板内控件的各种属性信息
        /// </summary>
        public GetTemplateDetailResult_TemplateContent_Controls_Property property { get; set; }

        /// <summary>
        /// 模板控件配置，包含了部分控件类型的附加类型、属性，详见附录说明。目前有配置信息的控件类型有：Date-日期/日期+时间；Selector-单选/多选；Contact-成员/部门；Table-明细；Attendance-假勤组件（请假、外出、出差、加班）
        /// </summary>
        public GetTemplateDetailResult_TemplateContent_Controls_Config config { get; set; }
    }

    public class GetTemplateDetailResult_TemplateContent_Controls_Property
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
        public List<GetTemplateDetailResult_TextAndLang> title { get; set; }

        /// <summary>
        /// 控件说明，向申请者展示的控件填写说明，若配置了多语言则会包含中英文的控件说明，默认为zh_CN中文
        /// </summary>
        public List<GetTemplateDetailResult_TextAndLang> placeholder { get; set; }

        /// <summary>
        /// 是否必填：1-必填；0-非必填
        /// </summary>
        public int require { get; set; }

        /// <summary>
        /// 是否参与打印：1-不参与打印；0-参与打印
        /// </summary>
        public int un_print { get; set; }
    }


    public class GetTemplateDetailResult_TemplateContent_Controls_Config
    {
        /// <summary>
        /// 
        /// </summary>
        public GetTemplateDetailResult_TemplateContent_Controls_Config_Selector selector { get; set; }
    }

    public class GetTemplateDetailResult_TemplateContent_Controls_Config_Selector
    {
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }

        public int exp_type { get; set; }

        public List<GetTemplateDetailResult_TemplateContent_Controls_Config_Selector_Options> options { get; set; }
    }

    public class GetTemplateDetailResult_TemplateContent_Controls_Config_Selector_Options
    {
        /// <summary>
        /// 
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetTemplateDetailResult_TextAndLang> value { get; set; }
    }

}
