namespace Senparc.Weixin.QY.Entities.Request.KF
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