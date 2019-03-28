namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_ModifyStoreAuditInfo : RequestMessageEvent_ApplyMerchantAuditInfo
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.modify_store_audit_info; }
        }
    }
}
