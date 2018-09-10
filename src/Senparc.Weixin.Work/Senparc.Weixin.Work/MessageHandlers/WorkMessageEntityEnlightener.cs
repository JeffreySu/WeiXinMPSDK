using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.MessageHandlers
{
    public class WorkMessageEntityEnlightener
    {
        public static MessageEntityEnlightener Instance = new MessageEntityEnlightener(NeuChar.PlatformType.WeChat_Work)
        {
            NewRequestMessageText = () => new RequestMessageText(),
            NewRequestMessageLocation = () => new RequestMessageLocation(),
            NewRequestMessageImage = () => new RequestMessageImage(),
            NewRequestMessageVoice = () => new RequestMessageVoice(),
            NewRequestMessageVideo = () => new RequestMessageVideo(),
            NewRequestMessageShortVideo = () => new RequestMessageShortVideo(),
            NewRequestMessageEvent = () => new RequestMessageEventBase(),
            NewRequestMessageFile = () => new RequestMessageFile(),
            //TODO：客服消息

            NewResponseMessageText = () => new ResponseMessageText(),
            NewResponseMessageNews = () => new ResponseMessageNews(),
            NewResponseMessageMpNews = () => new ResponseMessageMpNews(),
            NewResponseMessageImage = () => new ResponseMessageImage(),
            NewResponseMessageVoice = () => new ResponseMessageVoice(),
            NewResponseMessageVideo = () => new ResponseMessageVideo(),
        };
    }
}


