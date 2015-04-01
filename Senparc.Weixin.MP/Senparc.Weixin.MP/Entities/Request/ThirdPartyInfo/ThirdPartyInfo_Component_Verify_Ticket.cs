/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ThirdPartyInfo_Component_Verify_Ticket.cs
    文件功能描述：推送component_verify_ticket协议
    
    
    创建标识：Senparc - 20150401
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ThirdPartyInfo_Component_Verify_Ticket : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public virtual ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.component_verify_ticket; }
        }

        /// <summary>
        /// Ticket内容
        /// </summary>
        public string ComponentVerifyTicket { get; set; }
    }
}
