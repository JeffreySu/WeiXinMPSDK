using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 完成投诉处理
    /// </summary>
    public class CompleteComplaintRequestData
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
