using Senparc.NeuChar;
namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestMessageImage : RequestMessageFile
    {
        public RequestMessageImage()
        {
            this.MsgType = RequestMsgType.Image;
        }
        public string PicUrl { get; set; }
    }
}