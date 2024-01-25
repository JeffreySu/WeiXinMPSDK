using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 回复用户
    /// </summary>
    public class ResponseComplaintRequestData
    {
        /// <summary>
        /// 投诉id，get_complaint_list接口返回
        /// </summary>
        public string complaint_id { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string response_content { get; set; }

        /// <summary>
        /// 每一项的内容为string，传upload_vp_file接口返回的file_id
        /// </summary>
        public List<string> response_images { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
