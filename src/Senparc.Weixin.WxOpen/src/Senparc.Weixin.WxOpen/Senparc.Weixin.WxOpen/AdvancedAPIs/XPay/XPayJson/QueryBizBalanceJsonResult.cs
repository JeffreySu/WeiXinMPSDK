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
    public class QueryBizBalanceJsonResult : WxJsonResult
    {
        /// <summary>
        /// 可提现余额
        /// </summary>
        public QueryBizBalanceAvailable balance_available { get; set; }
    }

    /// <summary>
    /// 发布的道具列表
    /// </summary>
    public class QueryBizBalanceAvailable
    {
        /// <summary>
        /// 可提现余额，单位元
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// 币种（一般为CNY）
        /// </summary>
        public string currency_code { get; set; }
    }
}
