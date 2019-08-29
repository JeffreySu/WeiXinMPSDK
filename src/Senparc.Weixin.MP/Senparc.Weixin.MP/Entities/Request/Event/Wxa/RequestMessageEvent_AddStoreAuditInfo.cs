namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_AddStoreAuditInfo : RequestMessageEvent_ApplyMerchantAuditInfo
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.add_store_audit_info; }
        }

     
        /// <summary>
        /// 0 表示创建门店
        /// 1 表示是补充门店
        /// </summary>
        public int is_upgrade { get; set; }
        /// <summary>
        /// 门店id  status=1 的时候才有效
        /// </summary>
        public string poiid { get; set; }
    }
}
