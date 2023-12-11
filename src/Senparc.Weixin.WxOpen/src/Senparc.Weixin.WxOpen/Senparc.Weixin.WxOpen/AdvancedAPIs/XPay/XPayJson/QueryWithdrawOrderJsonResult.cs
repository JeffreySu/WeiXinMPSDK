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
    public class QueryWithdrawOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 提现单号
        /// </summary>
        public string withdraw_no { get; set; }

        /// <summary>
        /// 提现单的微信侧单号1-创建成功，提现中 2-提现成功 3-提现失败
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public string withdraw_amount { get; set; }

        /// <summary>
        /// 提现单的微信侧单号
        /// </summary>
        public string wx_withdraw_no { get; set; }

        /// <summary>
        /// 提现单成功的秒级时间戳
        /// </summary>
        public string withdraw_success_timestamp { get; set; }

        /// <summary>
        /// 提现单创建时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 提现失败的原因
        /// </summary>
        public string fail_reason { get; set; }
    }
}
