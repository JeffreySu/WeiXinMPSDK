using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 推送suite_ticket协议
    /// </summary>
    public class RequestMessageInfo_Suite_Ticket : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public override ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.SUITE_TICKET; }
        }

        /// <summary>
        /// Ticket内容
        /// </summary>
        public string SuiteTicket { get; set; }
    }
}
