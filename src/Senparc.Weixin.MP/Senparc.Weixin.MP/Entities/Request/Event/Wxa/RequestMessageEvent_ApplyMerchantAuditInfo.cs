namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_ApplyMerchantAuditInfo : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.apply_merchant_audit_info; }
        }
        /// <summary>
        /// 审核单id
        /// </summary>
        public long audit_id { get; set; }
        /// <summary>
        /// 1.审核通过
        /// 2.审核中
        /// 3.审核失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 审核不通过原因
        /// </summary>
        public string reason { get; set; }
    }
}
