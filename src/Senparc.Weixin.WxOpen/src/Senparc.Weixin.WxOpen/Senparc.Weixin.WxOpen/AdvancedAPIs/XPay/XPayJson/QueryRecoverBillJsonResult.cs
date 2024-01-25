using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryRecoverBillJsonResult : WxJsonResult
    {
        /// <summary>
        /// 广告金回收记录列表
        /// </summary>
        public List<QueryRecoverBillItem> bill_list { get; set; }

        /// <summary>
        /// 查询命中总的页数
        /// </summary>
        public int total_page { get; set; }
    }

    /// <summary>
    /// 广告金回收记录列表
    /// </summary>
    public class QueryRecoverBillItem
    {
        /// <summary>
        /// 充值单 ID
        /// </summary>
        public string bill_id { get; set; }

        /// <summary>
        /// 回收时间，unix秒级时间戳
        /// </summary>
        public long recover_time { get; set; }

        /// <summary>
        /// 对应广告金结算周期开始时间，unix秒级时间戳
        /// </summary>
        public long settle_begin { get; set; }

        /// <summary>
        /// 对应广告金结算周期结束时间，unix秒级时间戳
        /// </summary>
        public long settle_end { get; set; }

        /// <summary>
        /// 对应广告金ID
        /// </summary>
        public string fund_id { get; set; }

        /// <summary>
        /// 回收广告金账户
        /// </summary>
        public string recover_account_name { get; set; }

        /// <summary>
        /// 回收金额，单位：分
        /// </summary>
        public int recover_amount { get; set; }

        /// <summary>
        /// 对应的退款订单 id
        /// </summary>
        public List<object> refund_order_list { get; set; }
    }
}
