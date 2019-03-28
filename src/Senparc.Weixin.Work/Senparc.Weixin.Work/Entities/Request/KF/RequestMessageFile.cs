using Senparc.NeuChar;
namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestMessageFile : RequestMessage
    {
        public RequestMessageFile()
        {
            this.MsgType = RequestMsgType.File;
        }
        public string MediaId { get; set; }
    }
}