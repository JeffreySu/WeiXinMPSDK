namespace Senparc.Weixin.QY.Entities.Request.KF
{
    public class RequestMessageVoice : RequestMessageFile
    {
        public RequestMessageVoice()
        {
            this.MsgType = RequestMsgType.Voice;
        }
    }
}