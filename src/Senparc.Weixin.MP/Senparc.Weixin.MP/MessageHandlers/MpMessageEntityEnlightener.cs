using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
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
    public class MpMessageEntityEnlightener : MessageEntityEnlightener
    {
        //说明：此处不需要强制使用单例模式

        /// <summary>
        /// MpMessageEntityEnlightener 全局对象
        /// </summary>
        public static MessageEntityEnlightener Instance = new MpMessageEntityEnlightener(NeuChar.PlatformType.WeChat_OfficialAccount);

        MpMessageEntityEnlightener(NeuChar.PlatformType platformType) : base(platformType) { }

        public override IRequestMessageEvent NewRequestMessageEvent()
        {
            return new RequestMessageEventBase();
        }

        public override IRequestMessageFile NewRequestMessageFile()
        {
            return new RequestMessageFile();
        }

        public override IRequestMessageImage NewRequestMessageImage()
        {
            return new RequestMessageImage();
        }

        public override IRequestMessageLink NewRequestMessageLink()
        {
            return new RequestMessageLink();
        }

        public override IRequestMessageLocation NewRequestMessageLocation()
        {
            return new RequestMessageLocation();
        }

        public override IRequestMessageShortVideo NewRequestMessageShortVideo()
        {
            return new RequestMessageShortVideo();
        }

        public override IRequestMessageText NewRequestMessageText()
        {
            return new RequestMessageText();
        }

        public override IRequestMessageVideo NewRequestMessageVideo()
        {
            return new RequestMessageVideo();
        }

        public override IRequestMessageVoice NewRequestMessageVoice()
        {
            return new RequestMessageVoice();
        }

        public override IResponseMessageImage NewResponseMessageImage()
        {
            return new ResponseMessageImage();
        }

        public override IResponseMessageMpNews NewResponseMessageMpNews()
        {
            throw new MessageHandlerException("微信公众号不支持 IResponseMessageMpNews 响应类型");
        }

        public override IResponseMessageMusic NewResponseMessageMusic()
        {
            return new ResponseMessageMusic();
        }

        public override IResponseMessageNews NewResponseMessageNews()
        {
            return new ResponseMessageNews();
        }

        public override IResponseMessageText NewResponseMessageText()
        {
            return new ResponseMessageText();
        }

        public override IResponseMessageTransfer_Customer_Service NewResponseMessageTransfer_Customer_Service()
        {
            return new ResponseMessageTransfer_Customer_Service();
        }

        public override IResponseMessageVideo NewResponseMessageVideo()
        {
            return new ResponseMessageVideo();
        }

        public override IResponseMessageVoice NewResponseMessageVoice()
        {
            return new ResponseMessageVoice();
        }
    }
}
