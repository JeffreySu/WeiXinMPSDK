using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;


namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// GetPrivateTemplateJsonResult
    /// </summary>
    public class GetPrivateTemplateJsonResult : WxJsonResult
    {
        public List<GetPrivateTemplate_TemplateItem> template_list { get; set; }
    }

    public class GetPrivateTemplate_TemplateItem
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 模板标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 模板所属行业的一级行业
        /// </summary>
        public string primary_industry { get; set; }
        /// <summary>
        /// 模板所属行业的二级行业
        /// </summary>
        public string deputy_industry { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 模板示例
        /// </summary>
        public string example { get; set; }
        public IndustryCode ConvertToIndustryCode()
        {
            var enumName = string.Format("{0}_{1}", primary_industry,
                deputy_industry.Replace("|", "_").Replace("/", "_"));
            IndustryCode code;
            if (!Enum.TryParse(enumName, true, out code))
            {
                return IndustryCode.其它_其它;//没有成功，此处也可以抛出异常
            }
            return code;
        }
       
    }
}
