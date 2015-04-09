/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ThirdPartyInfo_AuthorizerAppId.cs
    文件功能描述：推送取消授权通知
    
    
    创建标识：Senparc - 20150401
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ThirdPartyInfo_AuthorizerAppId : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public virtual ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.unauthorized; }
        }

        /// <summary>
        /// 取消授权的公众号
        /// </summary>
        public string AuthorizerAppid { get; set; }
    }
}
