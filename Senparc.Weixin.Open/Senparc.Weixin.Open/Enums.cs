using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Open
{
    public enum InfoType
    {
        /// <summary>
        /// 接收推送Ticket
        /// </summary>
        component_verify_ticket,
        /// <summary>
        ///   取消授权
        /// </summary>
        unauthorized
    }
}
