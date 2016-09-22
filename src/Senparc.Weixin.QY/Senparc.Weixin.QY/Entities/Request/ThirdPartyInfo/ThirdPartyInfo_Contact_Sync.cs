
namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 变更授权的通知
    /// </summary>
    public class RequestMessageInfo_Contact_Sync : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public override ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.CONTACT_SYNC; }
        }

        /// <summary>
        /// 授权方企业号的corpid
        /// </summary>
        public string AuthCorpId { get; set; }

        /// <summary>
        /// 当前序号
        /// </summary>
        public int Seq { get; set; }
    }
}
