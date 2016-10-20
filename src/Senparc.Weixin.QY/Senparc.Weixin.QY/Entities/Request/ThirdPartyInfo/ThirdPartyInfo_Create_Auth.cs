namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 变更授权的通知
    /// </summary>
    public class RequestMessageInfo_Create_Auth : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public override ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.CREATE_AUTH; }
        }

        /// <summary>
        /// 授权的auth_code,用于获取企业的永久授权码
        /// </summary>
        public string AuthCode { get; set; }
    }
}
