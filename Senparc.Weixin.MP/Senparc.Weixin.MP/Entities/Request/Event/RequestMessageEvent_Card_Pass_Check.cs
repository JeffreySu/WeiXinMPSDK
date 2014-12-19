using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Card_Pass_Check : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 卡券通过审核
        /// </summary>
        public override Event Event
        {
            get { return Event.card_pass_check; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
    }
}
