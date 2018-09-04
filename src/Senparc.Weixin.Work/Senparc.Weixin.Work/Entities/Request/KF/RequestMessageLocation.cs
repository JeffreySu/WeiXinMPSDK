using Senparc.NeuChar;
namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestMessageLocation : RequestMessage
    {
        public RequestMessageLocation()
        {
            this.MsgType = RequestMsgType.Location;
        }
        public double Location_X { get; set; }
        public double Location_Y { get; set; }
        public double Scale { get; set; }
        public string Label { get; set; }
    }
}