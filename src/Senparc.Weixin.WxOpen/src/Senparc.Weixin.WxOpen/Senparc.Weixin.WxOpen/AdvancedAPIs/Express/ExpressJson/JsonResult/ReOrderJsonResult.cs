using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class ReOrderJsonResult : ExpressJsonResult
    {
        /// <summary>
        /// 实际运费(单位：元)，运费减去优惠券费用
        /// </summary>
        public decimal fee { get; set; }
        /// <summary>
        /// 运费(单位：元)
        /// </summary>
        public decimal deliverfee { get; set; }
        /// <summary>
        /// 优惠券费用(单位：元)
        /// </summary>
        public decimal couponfee { get; set; }
        /// <summary>
        /// 小费(单位：元)
        /// </summary>
        public decimal tips { get; set; }
        /// <summary>
        /// 保价费(单位：元)
        /// </summary>
        public decimal insurancefee { get; set; }
        /// <summary>
        /// 配送距离(整数单位：米)
        /// </summary>
        public decimal distance { get; set; }
        /// <summary>
        /// 配送单号
        /// </summary>
        public string waybill_id { get; set; }
        /// <summary>
        /// 配送状态
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// 收货码
        /// </summary>
        public int finish_code { get; set; }
        /// <summary>
        /// 取货码
        /// </summary>
        public int pickup_code { get; set; }
        /// <summary>
        /// 预计骑手接单时间，单位秒，比如5分钟，就填300, 无法预计填0
        /// </summary>
        public int dispatch_duration { get; set; }
    }
}
