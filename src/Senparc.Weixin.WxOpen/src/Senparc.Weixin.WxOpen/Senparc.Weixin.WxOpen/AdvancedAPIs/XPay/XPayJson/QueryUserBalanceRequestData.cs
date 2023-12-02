using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询用户代币余额
    /// </summary>
    public class QueryUserBalanceRequestData
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
    }
}
