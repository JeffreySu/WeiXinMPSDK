using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Sample.CommonService.TemplateMessage
{
    public interface IWeixinTemplateBase
    {
        string TemplateId { get; set; }
        string TemplateName { get; set; }
    }
    public class WeixinTemplateBase : IWeixinTemplateBase
    {
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }

        public WeixinTemplateBase(string templateId, string templateName)
        {
            TemplateId = templateId;
            TemplateName = templateName;
        }
    }
}
