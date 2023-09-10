using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    public class GetOrderListInfoModel
    {
        /// <summary>
        /// 支付时间所属范围
        /// </summary>
        public PayTimeRangeModel pay_time_range { get; set; }

        /// <summary>
        /// 订单状态枚举：(1) 待发货；(2) 已发货；(3) 确认收货；(4) 交易完成；(5) 已退款。
        /// </summary>
        public int order_state { get; set; }

        /// <summary>
        /// 支付者openid。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 翻页时使用，获取第一页时不用传入，如果查询结果中 has_more 字段为 true，则传入该次查询结果中返回的 last_index 字段可获取下一页
        /// </summary>
        public string last_index { get; set; }

        /// <summary>
        /// 翻页时使用，返回列表的长度，默认为100
        /// </summary>
        public int page_size { get; set; }

    }

    /// <summary>
    /// 支付时间所属范围
    /// </summary>
    public class PayTimeRangeModel
    {
        /// <summary>
        /// 起始时间，时间戳形式，不填则视为从0开始。
        /// </summary>
        public long begin_time { get; set; }

        /// <summary>
        /// 结束时间（含），时间戳形式，不填则视为32位无符号整型的最大值。
        /// </summary>
        public long end_time { get; set;}
    }
}
