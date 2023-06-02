using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    /// <summary>
    /// 批量获取运单数据返回参数
    /// </summary>
    public class BatchGetOrderJsonResult:WxJsonResult
    {
        /// <summary>
        /// 运单列表
        /// </summary>
        public List<GetOrderJsonResult> order_list { get; set; }
    }
}
