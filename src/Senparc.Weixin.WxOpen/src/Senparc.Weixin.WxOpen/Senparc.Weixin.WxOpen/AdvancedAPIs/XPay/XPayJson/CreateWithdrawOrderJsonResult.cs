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
    public class CreateWithdrawOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 提现单号
        /// </summary>
        public string withdraw_no { get; set; }

        /// <summary>
        /// 提现单的微信侧单号
        /// </summary>
        public string wx_withdraw_no { get; set; }
    }
}
