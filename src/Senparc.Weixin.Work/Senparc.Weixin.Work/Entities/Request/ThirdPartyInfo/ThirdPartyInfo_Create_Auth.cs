namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 变更授权的通知
    /// </summary>
    public class RequestMessageInfo_Create_Auth : ThirdPartyInfoBase, IThirdPartyInfoBase, IThirdServiceCorpBase
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

    /// <summary>
    /// 变更授权的通知
    /// </summary>
    public class RequestMessageInfo_Reset_Permanent_Code : ThirdPartyInfoBase, IThirdPartyInfoBase, IThirdServiceCorpBase
    {
        public override ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.RESET_PERMANENT_CODE; }
        }

        /// <summary>
        /// 授权的auth_code,用于获取企业的永久授权码
        /// </summary>
        public string AuthCode { get; set; }
    }
}
