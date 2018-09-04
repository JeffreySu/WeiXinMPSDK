using Senparc.NeuChar.Entities;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.MessageHandlers
{
    public class MpMessageEntityEnlighten
    {
        public static MessageEntityEnlighten Instance = new MessageEntityEnlighten()
        {
            NewRequestMessageText = () => new RequestMessageText(),
            NewRequestMessageLocation = () => new RequestMessageLocation(),
            NewRequestMessageImage = () => new RequestMessageImage(),
            NewRequestMessageVoice = () => new RequestMessageVoice(),
            NewRequestMessageVideo = () => new RequestMessageVideo(),
            NewRequestMessageLink = () => new RequestMessageLink(),
            NewRequestMessageShortVideo = () => new RequestMessageShortVideo(),
            NewRequestMessageEvent = () => new RequestMessageEventBase(),
            NewRequestMessageFile = () => new RequestMessageFile(),


            NewResponseMessageText = () => new  ResponseMessageText(),
            NewResponseMessageNews = () => new  ResponseMessageNews(),
            NewResponseMessageMusic = () => new ResponseMessageMusic(),
            NewResponseMessageImage = () => new ResponseMessageImage(),
            NewResponseMessageVoice = () => new ResponseMessageVoice(),
            NewResponseMessageVideo = () => new ResponseMessageVideo(),
            NewResponseMessageTransfer_Customer_Service = () => new ResponseMessageTransfer_Customer_Service(),

        };
    }
}
