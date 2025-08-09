/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotMessageContext.cs
    文件功能描述：企业微信智能机器人上下文
    
    
    创建标识：Wang Qian - 20250809

----------------------------------------------------------------*/

using System.Xml.Linq;
using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Work.Entities;
using Senparc.NeuChar.Exceptions;
using System;

namespace Senparc.Weixin.Work.MessageContext
{
    /// <summary>
    /// 企业微信智能机器人上下文
    /// </summary>
    public class WorkBotMessageContext : MessageContext<IWorkBotRequestMessageBase, IWorkBotResponseMessageBase>, IMessageContext<IWorkBotRequestMessageBase, IWorkBotResponseMessageBase>
    {
        /// <summary>
        /// 获取请求消息和实体之间的映射结果
        /// </summary>
        /// <param name="requestMsgType"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public override IWorkBotRequestMessageBase GetRequestEntityMappingResult(RequestMsgType requestMsgType, XDocument doc)
        {
            IWorkBotRequestMessageBase requestMessage;
            switch (requestMsgType)
            {
                case RequestMsgType.Text:
                    requestMessage = new BotRequestMessageText();
                    break;
                case RequestMsgType.Image:
                    requestMessage = new BotRequestMessageImage();
                    break;
                case RequestMsgType.Mixed:
                    requestMessage = new BotRequestMessageMixed();
                    break;
                case RequestMsgType.Stream:
                    requestMessage = new BotRequestMessageStream();
                    break;
                case RequestMsgType.Event:
                    switch(doc.Root.Element("event").Element("eventtype").Value.ToUpper())
                    {
                        case "ENTER_CHAT":
                            requestMessage = new BotRequestMessageEvent_Enter();
                            break;
                        case "TEMPLATE_CARD_EVENT":
                            requestMessage = new BotRequestMessageEvent_TemplateCardEvent();
                            break;
                        default:
                            throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在RequestMessageFactory中没有对应的处理程序！", requestMsgType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
                    }
                    break;
                default:
                    throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在RequestMessageFactory中没有对应的处理程序！", requestMsgType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
            }
            return requestMessage;
        }

        public override IWorkBotResponseMessageBase GetResponseEntityMappingResult(ResponseMsgType responseMsgType, XDocument doc)
        {
            IWorkBotResponseMessageBase responseMessage;
            switch (responseMsgType)
            {
                case ResponseMsgType.Text:
                    responseMessage = new WorkBotResponseMessageText();
                    break;
                case ResponseMsgType.Stream:
                    responseMessage = new WorkBotResponseMessageStream();
                    break;
                case ResponseMsgType.Unknown:
                    // 简单探测是否为模板卡片结构
                    if (doc != null)
                    {
                        var root = doc.Root;
                        var isTemplateCard = root.Element("template_card") != null;
                        if (isTemplateCard)
                        {
                            responseMessage = new WorkBotResponseMessageTemplateCard();
                            break;
                        }
                    }
                    throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在ResponseEntityMapping中没有对应的处理程序！", responseMsgType), new ArgumentOutOfRangeException());
                default:
                    throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在ResponseEntityMapping中没有对应的处理程序！", responseMsgType), new ArgumentOutOfRangeException());
            }

            return responseMessage;
        }
    }
}