namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// unionid查询pending_id 请求参数
    /// </summary>
    public class UnionIdToPendingIdRequest
    {
        /// <summary>
        /// 微信客户的unionid
        /// 必填
        /// </summary>
        public string unionid { get; set; }

        /// <summary>
        /// 微信客户的openid
        /// 必填
        /// </summary>
        public string openid { get; set; }
    }
}
