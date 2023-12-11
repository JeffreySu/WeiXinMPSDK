using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class RefundOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 退款单号
        /// </summary>
        public string refund_order_id { get; set; }

        /// <summary>
        /// 退款单的微信侧单号
        /// </summary>
        public string refund_wx_order_id { get; set; }

        /// <summary>
        /// 该退款单对应的支付单单号
        /// </summary>
        public string pay_order_id { get; set; }

        /// <summary>
        /// 该退款单对应的支付单微信侧单号
        /// </summary>
        public string pay_wx_order_id { get; set; }
    }
}
