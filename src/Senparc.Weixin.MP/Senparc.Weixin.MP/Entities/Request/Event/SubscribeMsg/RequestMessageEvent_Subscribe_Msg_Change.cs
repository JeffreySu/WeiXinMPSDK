using System.Collections.Generic;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Subscribe_Msg_Change : RequestMessageEventBase, IRequestMessageEventBase
    {

        public override Event Event => Event.subscribe_msg_change_event;
        
        
        public RequestMessageEvent_Subscribe_Msg_Change()
        {
            SubscribeMsgChangeEvent = new List<SubscribeMsgChangeEvent>();
        }

        public List<SubscribeMsgChangeEvent> SubscribeMsgChangeEvent { get; set; }
    }
}