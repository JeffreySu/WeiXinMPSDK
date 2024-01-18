namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 获取投诉详情
    /// </summary>
    public class GetComplaintDetailRequestData
    {
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
