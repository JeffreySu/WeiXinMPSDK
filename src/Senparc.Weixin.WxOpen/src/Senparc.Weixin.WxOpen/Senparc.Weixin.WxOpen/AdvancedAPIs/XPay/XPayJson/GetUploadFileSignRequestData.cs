namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 获取微信支付反馈投诉图片的签名头部
    /// </summary>
    public class GetUploadFileSignRequestData
    {
        /// <summary>
        /// 微信支付的图片地址格式为"https://api.mch.weixin.qq.com/v3/merchant-service/images/{xxxxxx}"
        /// </summary>
        public string wxpay_url { get; set; }

        /// <summary>
        /// 是否转存到cos，转存后可以获得图片的临时下载地址，30分钟有效
        /// </summary>
        public bool convert_cos { get; set; }

        /// <summary>
        /// 对应的反馈投诉id
        /// </summary>
        public string complaint_id { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
