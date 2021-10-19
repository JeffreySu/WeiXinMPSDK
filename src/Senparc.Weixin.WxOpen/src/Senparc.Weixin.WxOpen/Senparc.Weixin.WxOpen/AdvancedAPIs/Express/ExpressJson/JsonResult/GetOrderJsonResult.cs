using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class GetOrderJsonResult : ExpressJsonResult
    {
        /// <summary>
        /// 配送状态
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// 配送单号
        /// </summary>
        public string waybill_id { get; set; }
        /// <summary>
        /// 骑手姓名
        /// </summary>
        public string rider_name { get; set; }
        /// <summary>
        /// 骑手电话
        /// </summary>
        public string rider_phone { get; set; }
        /// <summary>
        /// 骑手位置经度, 配送中时返回
        /// </summary>
        public decimal rider_lng { get; set; }
        /// <summary>
        /// 骑手位置纬度, 配送中时返回
        /// </summary>
        public decimal rider_lat { get; set; }
        /// <summary>
        /// 预计还剩多久送达时间, 配送中时返回，单位秒， 已取货配送中需返回，比如5分钟后送达，填300
        /// </summary>
        public int reach_time { get; set; }
    }
}
