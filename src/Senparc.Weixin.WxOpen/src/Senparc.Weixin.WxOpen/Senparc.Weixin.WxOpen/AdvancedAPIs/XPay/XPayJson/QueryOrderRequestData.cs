using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询创建的订单（现金单，非代币单）
    /// </summary>
    public class QueryOrderRequestData
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
        /// 创建的订单号
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 微信内部单号(与order_id二选一)
        /// </summary>
        public string wx_order_id { get; set; }
    }
}
