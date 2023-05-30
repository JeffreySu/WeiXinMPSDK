using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson
{
    /// <summary>
    /// 查询运单轨迹请求参数
    /// </summary>
    public class GetPathModel
    {
        /// <summary>
        /// 用户openid，当add_source=2时无需填写（不发送物流服务通知）
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 快递公司ID，参见getAllDelivery
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 运单ID
        /// </summary>
        public string waybill_id { get; set; }
    }
}
