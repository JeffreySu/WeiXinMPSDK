using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Senparc.Weixin.Context;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities.Request;
using Senparc.Weixin.Open.Helpers;
using Tencent;

namespace Senparc.Weixin.Open.MessageHandlers
{
    public abstract class ThirdPartyMessageHandler
    {
        private PostModel _postModel;
        /// <summary>
        /// 加密（原始）的XML
        /// </summary>
        public XDocument EcryptRequestDocument { get; set; }
        /// <summary>
        /// 解密之后的XML
        /// </summary>
        public XDocument RequestDocument { get; set; }
        /// <summary>
        /// 请求消息，对应解密之之后的XML数据
        /// </summary>
        public IRequestMessageBase RequestMessage { get; set; }

        public string ResponseMessageText { get; set; }

        public bool CancelExcute { get; set; }

        public ThirdPartyMessageHandler(Stream inputStream, PostModel postModel = null)
        {
            _postModel = postModel;
            EcryptRequestDocument = XmlUtility.XmlUtility.Convert(inputStream);//原始加密XML转成XDocument

            //解密XML信息
            var postDataStr = EcryptRequestDocument.ToString();

            WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.AppId);
            string msgXml = null;
            var result = msgCrype.DecryptMsg(_postModel.Msg_Signature, _postModel.Timestamp, _postModel.Nonce, postDataStr, ref msgXml);

            //判断result类型
            if (result != 0)
            {
                //验证没有通过，取消执行
                CancelExcute = true;
                return;
            }

            RequestDocument = XDocument.Parse(msgXml);//完成解密
            RequestMessage = RequestMessageFactory.GetRequestEntity(RequestDocument);

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
                //此处可以记录日志
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
