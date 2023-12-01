using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 扣减代币（一般用于代币支付）
    /// </summary>
    public class CurrencyPayRequestData
    {
        /// <summary>
        /// 用户的openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }

        /// <summary>
        /// 用户ip，例如:1.1.1.1
        /// </summary>
        public string user_ip { get; set; }

        /// <summary>
        /// 支付的代币数量
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 物品信息。记录到账户流水中。如:[{"productid":"物品id", "unit_price": 单价, "quantity": 数量}]
        /// </summary>
        public string payitem { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 平台类型1-安卓 2-苹果
        /// </summary>
        public int device_type { get; set; }
    }
}
