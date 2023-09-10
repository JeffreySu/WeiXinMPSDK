using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    public class AddOrderJsonResult:WxJsonResult
    {
        /// <summary>
        /// 订单ID，下单成功时返回
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 运单ID，下单成功时返回
        /// </summary>
        public string waybill_id { get; set; }
        /// <summary>
        /// 快递侧错误码，下单失败时返回
        /// </summary>
        public int delivery_sesultcode { get; set; }
        /// <summary>
        /// 快递侧错误信息，下单失败时返回
        /// </summary>
        public string delivery_resultmsg { get; set; }
        /// <summary>
        /// 运单信息，下单成功时返回
        /// </summary>
        public List<WayBillDataModel> waybill_data { get; set; }
    }
    public class WayBillDataModel
    {
        /// <summary>
        /// 运单信息 key
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 运单信息 value
        /// </summary>
        public string value { get; set; }
    }
}
