using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUploadFileSignJsonResult : WxJsonResult
    {
        /// <summary>
        /// 返回微信支付图片请求的Authorization头部值，具体使用方法可查看备注
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 当convert_cos为true时才有意义，返回转存后的url地址，30分钟有效
        /// </summary>
        public string cos_url { get; set; }
    }
}
