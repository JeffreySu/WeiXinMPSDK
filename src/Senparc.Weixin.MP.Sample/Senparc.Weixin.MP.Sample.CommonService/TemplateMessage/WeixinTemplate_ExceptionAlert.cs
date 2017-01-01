using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace Senparc.Weixin.MP.Sample.CommonService.TemplateMessage
{
    public class WeixinTemplate_ExceptionAlert : WeixinTemplateBase
    {
        const string TEMPLATE_ID = "ur6TqESOo-32FEUk4qJxeWZZVt4KEOPjqbAFDGWw6gg";//每个公众号都不同，需要根据实际情况修改

        public TemplateDataItem first { get; set; }
        public TemplateDataItem Time { get; set; }
        public TemplateDataItem Host { get; set; }
        public TemplateDataItem Service { get; set; }
        public TemplateDataItem Status { get; set; }
        public TemplateDataItem Message { get; set; }
        public TemplateDataItem remark { get; set; }

        public WeixinTemplate_ExceptionAlert(string _first, string host, string service, string status, string message,
            string _remark, string templateId = TEMPLATE_ID)
            : base(templateId, "系统异常告警通知")
        {
            first = new TemplateDataItem(_first);
            Time = new TemplateDataItem(DateTime.Now.ToString());
            Host = new TemplateDataItem(host);
            Service = new TemplateDataItem(service);
            Status = new TemplateDataItem(status);
            Message = new TemplateDataItem(message);
            remark = new TemplateDataItem(_remark);
        }
    }
}
