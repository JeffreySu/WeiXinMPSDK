using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class GetComplaintDetailJsonResult : WxJsonResult
    {
        /// <summary>
        /// 与get_complaint_list接口的complaints一致
        /// </summary>
        public ComplaintItem complaint { get; set; }
    }
}
