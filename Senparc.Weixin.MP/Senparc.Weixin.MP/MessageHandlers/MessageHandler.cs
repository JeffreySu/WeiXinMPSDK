using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.MessageHandlers
{
    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin.MP的基础功能，只为简化代码而设。
    /// </summary>
    public abstract class MessageHandler<TC> where TC : class, IMessageContext, new()
    {
        /// <summary>
        /// 上下文
        /// </summary>
        public static WeixinContext<TC> GlobalWeixinContext = new WeixinContext<TC>();

        /// <summary>
        /// 全局消息上下文
        /// </summary>
        public WeixinContext<TC> WeixinContext
        {
            get { return GlobalWeixinContext; }
        }

        /// <summary>
        /// 当前用户消息上下文
        /// </summary>
        public TC CurrentMessageContext
        {
            get { return WeixinContext.GetMessageContext(RequestMessage); }
        }

        /// <summary>
        /// 发送者用户名（OpenId）
        /// </summary>
        public string UserName
        {
            get
            {
                if (RequestMessage != null)
                {
                    return RequestMessage.FromUserName;
                }
                return null;
            }
        }

        /// <summary>
        /// 在构造函数中转换得到原始XML数据
        /// </summary>
        public XDocument RequestDocument { get; set; }

        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        public XDocument ResponseDocument
        {
            get
            {
                if (ResponseMessage == null)
                {
                    return null;
                }
                return EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase);
            }
        }

        //protected Stream InputStream { get; set; }
        /// <summary>
        /// 请求实体
        /// </summary>
        public IRequestMessageBase RequestMessage { get; set; }
        /// <summary>
        /// 响应实体
        /// 只有当执行Execute()方法后才可能有值
        /// </summary>
        public IResponseMessageBase ResponseMessage { get; set; }

        public MessageHandler(Stream inputStream)
        {
            using (XmlReader xr = XmlReader.Create(inputStream))
            {
                RequestDocument = XDocument.Load(xr);
                Init(RequestDocument);
            }
        }

        public MessageHandler(XDocument requestDocument)
        {
            Init(requestDocument);
        }

        private void Init(XDocument requestDocument)
        {
            RequestDocument = requestDocument;
            RequestMessage = RequestMessageFactory.GetRequestEntity(RequestDocument);

            //记录上下文
            if (WeixinContextGlobal.UseWeixinContext)
            {
                WeixinContext.InsertMessage(RequestMessage);
            }
        }

        /// <summary>
        /// 执行微信请求
        /// </summary>
        public void Execute()
        {
            OnExecuting();
            try
            {
                if (RequestMessage == null)
                {
                    return;
                }

                switch (RequestMessage.MsgType)
                {
                    case RequestMsgType.Text:
                        ResponseMessage = OnTextRequest(RequestMessage as RequestMessageText);
                        break;
                    case RequestMsgType.Location:
                        ResponseMessage = OnLocationRequest(RequestMessage as RequestMessageLocation);
                        break;
                    case RequestMsgType.Image:
                        ResponseMessage = OnImageRequest(RequestMessage as RequestMessageImage);
                        break;
                    case RequestMsgType.Voice:
                        ResponseMessage = OnVoiceRequest(RequestMessage as RequestMessageVoice);
                        break;
                    case RequestMsgType.Event:
                        ResponseMessage = OnEventRequest(RequestMessage as RequestMessageEventBase);
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
                }

                //记录上下文
                if (WeixinContextGlobal.UseWeixinContext && ResponseMessage != null)
                {
                    WeixinContext.InsertMessage(ResponseMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OnExecuted();
            }
        }

        public virtual void OnExecuting()
        {
        }

        public virtual void OnExecuted()
        {
        }


        /// <summary>
        /// 文字类型请求
        /// </summary>
        public abstract IResponseMessageBase OnTextRequest(RequestMessageText requestMessage);

        /// <summary>
        /// 位置类型请求
        /// </summary>
        public abstract IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage);

        /// <summary>
        /// 图片类型请求
        /// </summary>
        public abstract IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage);

        /// <summary>
        /// 语音类型请求
        /// </summary>
        public abstract IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage);

        /// <summary>
        /// 链接消息类型请求
        /// </summary>
        public abstract IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage);


        /// <summary>
        /// Event事件类型请求
        /// </summary>
        public virtual IResponseMessageBase OnEventRequest(RequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.ENTER:
                    responseMessage = OnEvent_EnterRequest(RequestMessage as RequestMessageEvent_Enter);
                    break;
                case Event.LOCATION:
                    responseMessage = OnEvent_LocationRequest(RequestMessage as RequestMessageEvent_Location);//目前实际无效
                    break;
                case Event.subscribe://订阅
                    responseMessage = OnEvent_SubscribeRequest(RequestMessage as RequestMessageEvent_Subscribe);
                    break;
                case Event.unsubscribe://退订
                    responseMessage = OnEvent_UnsubscribeRequest(RequestMessage as RequestMessageEvent_Unsubscribe);
                    break;
                case Event.CLICK:
                    responseMessage = OnEvent_ClickRequest(RequestMessage as RequestMessageEvent_Click);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event 下属分类

        /// <summary>
        /// Event事件类型请求之ENTER
        /// </summary>
        public abstract IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage);

        /// <summary>
        /// Event事件类型请求之LOCATION
        /// </summary>
        public abstract IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage);

        /// <summary>
        /// Event事件类型请求之subscribe
        /// </summary>
        public abstract IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage);

        /// <summary>
        /// Event事件类型请求之unsubscribe
        /// </summary>
        public abstract IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage);

        /// <summary>
        /// Event事件类型请求之CLICK
        /// </summary>
        public abstract IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage);

        #endregion
    }
}
