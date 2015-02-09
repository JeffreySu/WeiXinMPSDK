using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 变更授权的通知
    /// </summary>
    public class RequestMessageInfo_Change_Auth : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public override ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.CHANGE_AUTH; }
        }

        /// <summary>
        /// 授权方企业号的corpid
        /// </summary>
        public string AuthCorpId { get; set; }
    }
}
