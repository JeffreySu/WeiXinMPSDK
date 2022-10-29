namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class GetOrderModel
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
        /// 商家门店编号，在配送公司登记，美团、闪送必填
        /// </summary>
        public string shop_no { get; set; }
        /// <summary>
        /// 用配送公司提供的appSecret加密的校验串说明
        /// </summary>
        public string delivery_sign { get; set; }
    }
}
