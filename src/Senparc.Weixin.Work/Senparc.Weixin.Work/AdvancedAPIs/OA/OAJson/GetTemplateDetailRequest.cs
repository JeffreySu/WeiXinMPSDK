using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 获取审批模板详情 请求参数
    /// </summary>
    public class GetTemplateDetailRequest
    {
        /// <summary>
        /// 模板的唯一标识id。可在“获取审批单据详情”、“审批状态变化回调通知”中获得，也可在审批模板的模板编辑页面浏览器Url链接中获得。
        /// </summary>
        public string template_id { get; set; }
    }
}
