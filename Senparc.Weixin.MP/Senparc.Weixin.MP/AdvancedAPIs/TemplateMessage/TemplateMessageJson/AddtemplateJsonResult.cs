using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// AddtemplateJsonResult
    /// </summary>
    public class AddtemplateJsonResult : WxJsonResult
    {
        public string template_id { get; set; }

    }
}