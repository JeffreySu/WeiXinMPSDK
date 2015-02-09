using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
