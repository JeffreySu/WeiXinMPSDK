using Senparc.NeuChar;
namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestMessageVoice : RequestMessageFile
    {
        public RequestMessageVoice()
        {
            this.MsgType = RequestMsgType.Voice;
        }
    }
}