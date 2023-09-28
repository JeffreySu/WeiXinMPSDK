using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    /// <summary>
    /// 查询订单列表结果
    /// </summary>
    public class GetOrderListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 翻页时使用。
        /// </summary>
        public string last_index { get; set; }

        /// <summary>
        /// 是否还有更多支付单。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 支付单信息列表。
        /// </summary>
        public List<OrderModel> order_list { get; set; }
    }
}
