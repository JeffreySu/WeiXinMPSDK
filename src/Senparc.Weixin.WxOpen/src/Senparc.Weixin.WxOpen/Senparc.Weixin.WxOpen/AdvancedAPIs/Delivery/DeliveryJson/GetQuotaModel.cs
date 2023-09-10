using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson
{
    /// <summary>
    /// 该接口用于获取电子面单余额
    /// </summary>
    public class GetQuotaModel
    {
        /// <summary>
        /// 快递公司ID，参见getAllDelivery
        /// </summary>
        public string delivery_id {  get; set; }
        /// <summary>
        /// 快递公司客户编码
        /// </summary>
        public string biz_id { get; set; }
    }
}
