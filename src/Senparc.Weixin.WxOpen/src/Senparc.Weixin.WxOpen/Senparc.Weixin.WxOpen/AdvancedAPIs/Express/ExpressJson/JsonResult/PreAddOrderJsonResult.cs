using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class PreAddOrderJsonResult : ExpressJsonResult
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
        /// 预计骑手接单时间，单位秒，比如5分钟，就填300, 无法预计填0
        /// </summary>
        public int dispatch_duration { get; set; }
        /// <summary>
        /// 配送公司可以返回此字段，当用户下单时候带上这个字段，保证在一段时间内运费不变
        /// </summary>
        public string delivery_token { get; set; }
    }
}
