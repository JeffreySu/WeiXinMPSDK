using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestEvent : RequestBase
    {
        public RequestEvent()
        {
            this.MsgType = RequestMsgType.Event;
        }
        public Event Event { get; set; }
    }
}