using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询提现单
    /// </summary>
    public class QueryWithdrawOrderRequestData
    {
        /// <summary>
        /// 提现单单号，长度为[8,32]，字符只允许使用字母、数字、'_'、'-'
        /// </summary>
        public string withdraw_no { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
