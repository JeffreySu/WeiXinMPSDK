using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 代币支付退款(currency_pay接口的逆操作)
    /// </summary>
    public class CancelCurrencyPayRequestData
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
        /// 代币支付(调用currency_pay接口时)时传的order_id
        /// </summary>
        public string pay_order_id { get; set; }

        /// <summary>
        /// 本次退款单的单号
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 平台类型1-安卓 2-苹果
        /// </summary>
        public int device_type { get; set; }
    }
}
