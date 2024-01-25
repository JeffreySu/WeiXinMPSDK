namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 获取协商历史
    /// </summary>
    public class GetNegotiationHistoryRequestData
    {
        /// <summary>
        /// 筛选偏移，从0开始
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 筛选最多返回条数
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 投诉id，get_complaint_list接口返回
        /// </summary>
        public string complaint_id { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
