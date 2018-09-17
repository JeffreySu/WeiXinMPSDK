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
    /// <summary>
    /// 公众号 MessageEntityEnlightener
    /// </summary>
    public class MpMessageEntityEnlightener
    {
        //说明：此处不需要强制使用单例模式

        /// <summary>
        /// MpMessageEntityEnlightener 全局对象
        /// </summary>
        public static MessageEntityEnlightener Instance = new MessageEntityEnlightener(NeuChar.PlatformType.WeChat_OfficialAccount)
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


            NewResponseMessageText = () => new ResponseMessageText(),
            NewResponseMessageNews = () => new ResponseMessageNews(),
            NewResponseMessageMusic = () => new ResponseMessageMusic(),
            NewResponseMessageImage = () => new ResponseMessageImage(),
            NewResponseMessageVoice = () => new ResponseMessageVoice(),
            NewResponseMessageVideo = () => new ResponseMessageVideo(),
            NewResponseMessageTransfer_Customer_Service = () => new ResponseMessageTransfer_Customer_Service(),

        };
    }
}
