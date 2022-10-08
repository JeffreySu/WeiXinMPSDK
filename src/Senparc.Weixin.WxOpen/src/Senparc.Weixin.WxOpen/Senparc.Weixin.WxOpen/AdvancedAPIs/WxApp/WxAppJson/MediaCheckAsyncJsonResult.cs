using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    public class MediaCheckAsyncJsonResult : WxJsonResult
    {
        /// <summary>
        /// 唯一请求标识，标记单次请求，用于匹配异步推送结果
        /// </summary>
        public string trace_id { get; set; }
    }
}
