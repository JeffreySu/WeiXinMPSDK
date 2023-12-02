using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 绑定广告金充值账户
    /// </summary>
    public class BindTransferAccoutRequestData
    {
        /// <summary>
        /// 充值账户 uid
        /// </summary>
        public long transfer_account_uid { get; set; }

        /// <summary>
        /// 充值账户主体名称
        /// </summary>
        public string transfer_account_org_name { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
