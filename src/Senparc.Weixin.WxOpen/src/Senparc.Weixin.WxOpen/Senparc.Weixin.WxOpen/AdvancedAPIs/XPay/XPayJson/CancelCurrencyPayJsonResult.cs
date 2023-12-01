using Senparc.Weixin.Entities;
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
    public class CancelCurrencyPayJsonResult : WxJsonResult
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_id { get; set; }
    }
}
