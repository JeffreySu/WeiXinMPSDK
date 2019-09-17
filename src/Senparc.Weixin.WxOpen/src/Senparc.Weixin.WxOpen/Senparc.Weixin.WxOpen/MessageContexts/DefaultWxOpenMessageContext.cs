using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
using Senparc.Weixin.WxOpen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.WxOpen.MessageContexts
{
    /// <summary>
    /// 小程序上下文消息的默认实现
    /// </summary>
    public class DefaultWxOpenMessageContext : MessageContext<IRequestMessageBase, IResponseMessageBase>
    {
        /// <summary>
        /// 获取请求消息和实体之间的映射结果
        /// </summary>
        /// <param name="requestMsgType"></param>
        /// <returns></returns>
        public override IRequestMessageBase GetRequestEntityMappingResult(RequestMsgType requestMsgType, XDocument doc = null)
        {
            RequestMessageBase requestMessage;
            switch (requestMsgType)
            {
                case RequestMsgType.Text:
                    requestMessage = new RequestMessageText();
                    break;
                case RequestMsgType.Image:
                    requestMessage = new RequestMessageImage();
                    break;
                case RequestMsgType.NeuChar:
                    requestMessage = new RequestMessageNeuChar();
                    break;
                case RequestMsgType.Event:
                    //判断Event类型
                    switch (doc.Root.Element("Event").Value.ToUpper())
                    {
                        case "USER_ENTER_TEMPSESSION"://进入客服会话
                            requestMessage = new RequestMessageEvent_UserEnterTempSession();
                            break;
                        default://其他意外类型（也可以选择抛出异常）
                            requestMessage = new RequestMessageEventBase();
                            break;
                    }
                    break;
                default:
                    throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在RequestMessageFactory中没有对应的处理程序！", requestMsgType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
            }
            return requestMessage;
        }

        /// <summary>
        /// 获取响应消息和实体之间的映射结果
        /// </summary>
        /// <param name="responseMsgType"></param>
        /// <returns></returns>
        public override IResponseMessageBase GetResponseEntityMappingResult(ResponseMsgType responseMsgType, XDocument doc = null)
        {
            IResponseMessageBase responseMessage;
            switch (responseMsgType)
            {
                case ResponseMsgType.Transfer_Customer_Service:
                    responseMessage = new ResponseMessageTransfer_Customer_Service();
                    break;

                case ResponseMsgType.NoResponse:
                    responseMessage = new ResponseMessageNoResponse();
                    break;
                case ResponseMsgType.SuccessResponse:
                    responseMessage = new SuccessResponseMessage();
                    break;


                #region 不支持
                case ResponseMsgType.Other:
                case ResponseMsgType.Unknown:
                case ResponseMsgType.Text:
                case ResponseMsgType.News:
                case ResponseMsgType.Music:
                case ResponseMsgType.Image:
                case ResponseMsgType.Voice:
                case ResponseMsgType.Video:
                case ResponseMsgType.MpNews:
                case ResponseMsgType.MultipleNews:
                case ResponseMsgType.LocationMessage:
                case ResponseMsgType.UseApi:
                #endregion
                default:
                    responseMessage = new ResponseMessageUnknownType()
                    {
                        ResponseDocument = doc
                    };
                    break;
            }
            return responseMessage;
        }
    }
}
