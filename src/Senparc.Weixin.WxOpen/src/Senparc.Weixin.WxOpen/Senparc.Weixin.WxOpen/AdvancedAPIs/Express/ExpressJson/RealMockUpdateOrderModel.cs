namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class RealMockUpdateOrderModel
    {
        /// <summary>
        /// 商家id， 由配送公司分配的appkey
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 唯一标识订单的 ID，由商户生成, 不超过128字节
        /// </summary>
        public string shop_order_id { get; set; }
        /// <summary>
        /// 状态变更时间点，Unix秒级时间戳
        /// </summary>
        public long action_time { get; set; }
        /// <summary>
        /// 配送状态，枚举值
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// 附加信息
        /// 非必填
        /// </summary>
        public string action_msg { get; set; }
        /// <summary>
        /// 用配送公司提供的appSecret加密的校验串说明
        /// </summary>
        public string delivery_sign { get; set; }
    }
}
