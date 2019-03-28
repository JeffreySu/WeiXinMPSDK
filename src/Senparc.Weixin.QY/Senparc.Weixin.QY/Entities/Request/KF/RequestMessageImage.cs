namespace Senparc.Weixin.QY.Entities.Request.KF
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