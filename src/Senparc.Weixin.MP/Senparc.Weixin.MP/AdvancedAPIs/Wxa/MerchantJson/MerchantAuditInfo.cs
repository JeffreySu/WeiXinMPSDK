using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    public class MerchantAuditInfo
    {
        /// <summary>
        /// 审核单id
        /// </summary>
        public long audit_id { get; set; }
        /// <summary>
        /// 审核状态，0：未提交审核，1：审核成功，2：审核中，3：审核失败，4：管理员拒绝
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 审核状态为3或者4时，reason列出审核失败的原因
        /// </summary>
        public string reason { get; set; }
    }
    /// <summary>
    /// 门店小程序审核结果
    /// </summary>
    public class GetMerchantAuditInfoJson : WxJsonResult
    {
        /// <summary>
        /// 审核结果数据
        /// </summary>
        public MerchantAuditInfo data { get; set; }
    }
}
