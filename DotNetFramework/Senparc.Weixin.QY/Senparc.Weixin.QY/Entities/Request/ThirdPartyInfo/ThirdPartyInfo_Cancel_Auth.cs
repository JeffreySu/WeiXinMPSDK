/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ThirdPartyInfo_Cancel_Auth.cs
    文件功能描述：取消授权的通知
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 取消授权的通知
    /// </summary>
    public class RequestMessageInfo_Cancel_Auth : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public override ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.CANCEL_AUTH; }
        }

        /// <summary>
        /// 授权方企业号的corpid内容
        /// </summary>
        public string AuthCorpId { get; set; }
    }
}
