using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    public class GetQuotaJsonResult:WxJsonResult
    {
        /// <summary>
        /// 电子面单余额
        /// </summary>
        public int quota_num { get; set; }
    }
}
