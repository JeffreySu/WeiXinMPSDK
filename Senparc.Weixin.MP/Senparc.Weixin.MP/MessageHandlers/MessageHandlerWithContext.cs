using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.MessageHandlers
{
    public abstract class MessageHandler<TM> : MessageHandler
    {
        public MessageHandler(Stream inputStream)
            : base(inputStream)
        {

        }

        public MessageHandler(XDocument requestDocument)
            : base(requestDocument)
        {
        }


        /// <summary>
        /// 文字类型请求
        /// </summary>
        public abstract override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage);

        /// <summary>
        /// 位置类型请求
        /// </summary>
        public abstract override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage);

        /// <summary>
        /// 图片类型请求
        /// </summary>
        public abstract override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage);

        /// <summary>
        /// 语音类型请求
        /// </summary>
        public abstract override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage);

        /// <summary>
        /// 链接消息类型请求
        /// </summary>
        public abstract override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage);


        ///// <summary>
        ///// Event事件类型请求
        ///// </summary>
        //public virtual IResponseMessageBase OnEventRequest(RequestMessageEventBase requestMessage)
        //{
        //    var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
        //    IResponseMessageBase responseMessage = null;
        //    switch (strongRequestMessage.Event)
        //    {
        //        case Event.ENTER:
        //            responseMessage = OnEvent_EnterRequest(RequestMessage as RequestMessageEvent_Enter);
        //            break;
        //        case Event.LOCATION:
        //            responseMessage = OnEvent_LocationRequest(RequestMessage as RequestMessageEvent_Location);//目前实际无效
        //            break;
        //        case Event.subscribe://订阅
        //            responseMessage = OnEvent_SubscribeRequest(RequestMessage as RequestMessageEvent_Subscribe);
        //            break;
        //        case Event.unsubscribe://退订
        //            responseMessage = OnEvent_UnsubscribeRequest(RequestMessage as RequestMessageEvent_Unsubscribe);
        //            break;
        //        case Event.CLICK:
        //            responseMessage = OnEvent_ClickRequest(RequestMessage as RequestMessageEvent_Click);
        //            break;
        //        default:
        //            throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
        //    }
        //    return responseMessage;
        //}

        #region Event 下属分类

        /// <summary>
        /// Event事件类型请求之ENTER
        /// </summary>
        public abstract override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage);

        /// <summary>
        /// Event事件类型请求之LOCATION
        /// </summary>
        public abstract override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage);

        /// <summary>
        /// Event事件类型请求之subscribe
        /// </summary>
        public abstract override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage);

        /// <summary>
        /// Event事件类型请求之unsubscribe
        /// </summary>
        public abstract override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage);

        /// <summary>
        /// Event事件类型请求之CLICK
        /// </summary>
        public abstract override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage);

        #endregion
    }
}
