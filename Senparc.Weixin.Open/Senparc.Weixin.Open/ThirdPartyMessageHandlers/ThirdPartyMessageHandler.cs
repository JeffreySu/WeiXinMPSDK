using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Senparc.Weixin.Context;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Helpers;

namespace Senparc.Weixin.Open.MessageHandlers
{
    public abstract class ThirdPartyMessageHandler
    {
        public XDocument RequestDocument { get; set; }
        public RequestMessageBase RequestMessage { get; set; }

        public string ResponseMessageText { get; set; }

        public bool CancelExcute { get; set; }

        public ThirdPartyMessageHandler(Stream inputStream)
        {
            RequestDocument = XmlUtility.XmlUtility.Convert(inputStream);//转成XDocument

            //转成实体
            RequestMessageBase requestMessage = null;
            InfoType infoType;
            try
            {
                infoType = InfoTypeHelper.GetRequestInfoType(RequestDocument);
                switch (infoType)
                {
                    case InfoType.component_verify_ticket:
                        requestMessage = new RequestMessageComponentVerifyTicket();
                        break;
                    case InfoType.unauthorized:
                        requestMessage = new RequestMessageUnauthorized();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            requestMessage.FillEntityWithXml(RequestDocument);

            RequestMessage = requestMessage;
        }

        public void Excute()
        {
            if (CancelExcute)
            {
                return;
            }

            OnExecuting();

            if (CancelExcute)
            {
                return;
            }

            try
            {
                if (RequestMessage == null)
                {
                    return;
                }

                switch (RequestMessage.InfoType)
                {
                    case InfoType.component_verify_ticket:
                        {
                            var requestMessage = RequestMessage as RequestMessageComponentVerifyTicket;
                            ResponseMessageText = OnComponentVerifyTicketRequest(requestMessage);
                        }
                        break;
                    case InfoType.unauthorized:
                        {
                            var requestMessage = RequestMessage as RequestMessageUnauthorized;
                            ResponseMessageText = OnUnauthorizedRequest(requestMessage);
                        }
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException("未知的InfoType请求类型", null);
                }

            }
            catch (Exception ex)
            {
                throw new MessageHandlerException("ThirdPartyMessageHandler中Execute()过程发生错误：" + ex.Message, ex);
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
        /// 推送component_verify_ticket协议
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual string OnComponentVerifyTicketRequest(RequestMessageComponentVerifyTicket requestMessage)
        {
            return "success";
        }

        /// <summary>
        /// 推送取消授权通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual string OnUnauthorizedRequest(RequestMessageUnauthorized requestMessage)
        {
            return "success";
        }
    }
}
