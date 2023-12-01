using Senparc.Weixin.Entities;
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
    public class CurrencyPayJsonResult : WxJsonResult
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 总余额，包括有价和赠送部分
        /// </summary>
        public int balance { get; set; }

        /// <summary>
        /// 使用赠送部分的代币数量
        /// </summary>
        public int used_present_amount { get; set; }
    }
}
