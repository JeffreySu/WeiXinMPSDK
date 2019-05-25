using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    public class WxCloudFunctionJsonResult : WxJsonResult
    {
        /// <summary>
        /// 云函数返回的buffer
        /// </summary>
        public string resp_data { get; set; }
    }
}
