using Senparc.NeuChar;
namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestMessageLink : RequestMessage
    {
        public RequestMessageLink()
        {
            this.MsgType = RequestMsgType.Link;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string PicUrl { get; set; }
    }
}