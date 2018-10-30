using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.WxOpen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
   public class WxOpenMessageEntityEnlightener : MessageEntityEnlightener
    {
        public static MessageEntityEnlightener Instance = new WxOpenMessageEntityEnlightener(NeuChar.PlatformType.WeChat_MiniProgram);


        WxOpenMessageEntityEnlightener(NeuChar.PlatformType platformType) : base(platformType) { }

        public override IRequestMessageEvent NewRequestMessageEvent()
        {
            return new RequestMessageEventBase();
        }

        public override IRequestMessageFile NewRequestMessageFile()
        {
            throw new MessageHandlerException("微信公众号不支持 IRequestMessageFile 请求类型");
        }

        public override IRequestMessageImage NewRequestMessageImage()
        {
            return new RequestMessageImage();
        }

        public override IRequestMessageLink NewRequestMessageLink()
        {
            throw new MessageHandlerException("微信公众号不支持 IRequestMessageLink 请求类型");
        }

        public override IRequestMessageLocation NewRequestMessageLocation()
        {
            throw new MessageHandlerException("微信公众号不支持 IRequestMessageLocation 请求类型");
        }

        public override IRequestMessageShortVideo NewRequestMessageShortVideo()
        {
            throw new MessageHandlerException("微信公众号不支持 IRequestMessageShortVideo 请求类型");
        }

        public override IRequestMessageText NewRequestMessageText()
        {
            return new RequestMessageText();
        }

        public override IRequestMessageVideo NewRequestMessageVideo()
        {
            throw new MessageHandlerException("微信公众号不支持 IRequestMessageVideo 请求类型");
        }

        public override IRequestMessageVoice NewRequestMessageVoice()
        {
            throw new MessageHandlerException("微信公众号不支持 IRequestMessageVoice 请求类型");
        }

        public override IResponseMessageImage NewResponseMessageImage()
        {
            throw new MessageHandlerException("微信公众号不支持 IResponseMessageImage 响应类型");
        }

        public override IResponseMessageMpNews NewResponseMessageMpNews()
        {
            throw new MessageHandlerException("微信公众号不支持 IResponseMessageMpNews 响应类型");
        }

        public override IResponseMessageMusic NewResponseMessageMusic()
        {
            throw new MessageHandlerException("微信公众号不支持 IResponseMessageMusic 响应类型");
        }

        public override IResponseMessageNews NewResponseMessageNews()
        {
            throw new MessageHandlerException("微信公众号不支持 IResponseMessageNews 响应类型");
        }

        public override IResponseMessageText NewResponseMessageText()
        {
            return new Senparc.Weixin.MP.Entities.ResponseMessageText();
            //throw new MessageHandlerException("微信公众号不支持 IResponseMessageText 响应类型");
        }

        public override IResponseMessageTransfer_Customer_Service NewResponseMessageTransfer_Customer_Service()
        {
            return new ResponseMessageTransfer_Customer_Service();
        }

        public override IResponseMessageVideo NewResponseMessageVideo()
        {
            throw new MessageHandlerException("微信公众号不支持 IResponseMessageVideo 响应类型");
        }

        public override IResponseMessageVoice NewResponseMessageVoice()
        {
            throw new MessageHandlerException("微信公众号不支持 IResponseMessageVoice 响应类型");
        }

    }
}
