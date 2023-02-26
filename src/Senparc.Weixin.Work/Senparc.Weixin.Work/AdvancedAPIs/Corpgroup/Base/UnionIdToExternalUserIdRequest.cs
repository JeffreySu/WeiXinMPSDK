namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// 通过unionid和openid查询external_userid 请求参数
    /// </summary>
    public class UnionIdToExternalUserIdRequest
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

        /// <summary>
        /// 需要换取的企业corpid，不填则拉取所有企业
        /// 必填
        /// </summary>
        public string corpid { get; set; }
    }
}
