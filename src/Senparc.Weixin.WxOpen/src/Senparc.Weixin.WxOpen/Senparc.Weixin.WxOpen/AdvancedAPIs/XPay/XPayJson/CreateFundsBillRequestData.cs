using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 充值广告金
    /// </summary>
    public class CreateFundsBillRequestData
    {
        /// <summary>
        /// 充值金额，单位分
        /// </summary>
        public int transfer_amount { get; set; }

        /// <summary>
        /// 充值账户 uid
        /// </summary>
        public long transfer_account_uid { get; set; }

        /// <summary>
        /// 充值账户名称
        /// </summary>
        public string transfer_account_name { get; set; }

        /// <summary>
        /// 充值账户服务商账号 id
        /// </summary>
        public long transfer_account_agency_id { get; set; }

        /// <summary>
        /// 用户定义每一次请求的唯一 id，相同 id 的不同请求视为重复请求(不超过 1024 个字符)
        /// </summary>
        public string request_id { get; set; }

        /// <summary>
        /// 充值所使用的广告金对应的结算周期开始时间，unix秒级时间戳
        /// </summary>
        public long settle_begin { get; set; }

        /// <summary>
        /// 充值所使用的广告金对应的结算周期结束时间，unix秒级时间戳
        /// </summary>
        public long settle_end { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }

        /// <summary>
        /// 是否授权广告数据, 0:否，1:是
        /// </summary>
        public int authorize_advertise { get; set; }

        /// <summary>
        /// 广告金发放原因， 0:广告激励，1:通用赠送
        /// </summary>
        public int fund_type { get; set; }
    }
}
