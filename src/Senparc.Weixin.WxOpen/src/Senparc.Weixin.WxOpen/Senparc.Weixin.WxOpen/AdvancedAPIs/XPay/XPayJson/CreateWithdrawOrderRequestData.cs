using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 创建提现单
    /// </summary>
    public class CreateWithdrawOrderRequestData
    {
        /// <summary>
        /// 提现单单号，长度为[8,32]，字符只允许使用字母、数字、'_'、'-'
        /// </summary>
        public string withdraw_no { get; set; }

        /// <summary>
        /// 提现的金额，单位元，例如提现1分钱请使用0.01，允许不传，不传的情况下表示全额提现
        /// </summary>
        public string withdraw_amount { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
