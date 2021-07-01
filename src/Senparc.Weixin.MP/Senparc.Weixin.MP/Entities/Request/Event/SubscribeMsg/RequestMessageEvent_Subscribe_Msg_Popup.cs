using System.Collections.Generic;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Subscribe_Msg_Popup : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.subscribe_msg_popup_event; }
        }


        public RequestMessageEvent_Subscribe_Msg_Popup()
        {
            SubscribeMsgPopupEvent = new List<SubscribeMsgPopupEvent>();
        }

        public List<SubscribeMsgPopupEvent> SubscribeMsgPopupEvent { get; set; }
    }
}