using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson
{
    /// <summary>
    /// 获取运单数据请求参数
    /// </summary>
    public class GetOrderModel
    {
        /// <summary>
        /// 订单 ID，需保证全局唯一
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 该参数仅在getOrder接口生效，batchGetOrder接口不生效。用户openid，当add_source=2时无需填写（不发送物流服务通知）
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 快递公司ID，参见getAllDelivery, 必须和waybill_id对应
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 运单ID
        /// </summary>
        public string waybill_id { get; set; }
        /// <summary>
        /// 该参数仅在getOrder接口生效，batchGetOrder接口不生效。获取打印面单类型【1：一联单，0：二联单】，默认获取二联单
        /// </summary>
        public int print_type { get; set; }
        /// <summary>
        /// 快递备注信息，比如"易碎物品"，不超过1024字节
        /// </summary>
        public string custom_remark { get; set; }
    }
}
