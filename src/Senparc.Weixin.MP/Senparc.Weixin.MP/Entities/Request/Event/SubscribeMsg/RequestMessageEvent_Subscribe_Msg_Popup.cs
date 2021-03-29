namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Subscribe_Msg_Popup : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.subscribe_msg_popup_event; }
        }
    }
}