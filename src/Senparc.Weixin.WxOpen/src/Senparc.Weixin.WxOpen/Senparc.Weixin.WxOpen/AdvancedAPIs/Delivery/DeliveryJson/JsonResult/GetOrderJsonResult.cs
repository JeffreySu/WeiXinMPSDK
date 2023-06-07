using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    public class GetOrderJsonResult:WxJsonResult
    {
        /// <summary>
        /// 运单 html 的 BASE64 结果
        /// </summary>
        public string print_html { get; set; }
        /// <summary>
        /// 运单信息
        /// </summary>
        public List<WayBillDataModel> waybill_data {  get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string waybill_id { get; set; }
        /// <summary>
        /// 运单状态, 0正常，1取消
        /// </summary>
        public int order_status { get; set; }
    }
}
