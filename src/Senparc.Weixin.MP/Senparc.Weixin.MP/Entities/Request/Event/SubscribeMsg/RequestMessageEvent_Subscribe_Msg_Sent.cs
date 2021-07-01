using System.Collections.Generic;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Subscribe_Msg_Sent : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event => Event.subscribe_msg_sent_event;

        public List<SubscribeMsgSentEvent> SubscribeMsgSentEvent { get; set; }

        public RequestMessageEvent_Subscribe_Msg_Sent()
        {
            SubscribeMsgSentEvent = new List<SubscribeMsgSentEvent>();
        }

    }
}