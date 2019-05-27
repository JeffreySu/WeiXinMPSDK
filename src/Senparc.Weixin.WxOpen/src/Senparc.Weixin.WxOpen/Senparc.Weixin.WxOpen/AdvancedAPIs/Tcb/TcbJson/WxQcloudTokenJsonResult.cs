using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    public class WxQcloudTokenJsonResult : WxJsonResult
    {
        public string secretid { get; set; }
        public string secretkey { get; set; }
        public string token { get; set; }
        /// <summary>
        /// 过期时间戳
        /// </summary>
        public int expired_time { get; set; }
    }
}
