/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
  
    文件名：ThirdPartyMessageHandler.cs
    文件功能描述：开放平台消息处理器
    
    
    创建标识：Senparc - 20150211

    修改标识：Senparc - 20160813
    修改描述：v2.3.0 添加authorized和updateauthorized两种通知类型的处理

----------------------------------------------------------------*/


using System;
using System.IO;
using System.Xml.Linq;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities.Request;
using Senparc.Weixin.Open.Tencent;

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
            EcryptRequestDocument = XmlUtility.Convert(inputStream);//原始加密XML转成XDocument

            Init();
        }

        public ThirdPartyMessageHandler(XDocument ecryptRequestDocument, PostModel postModel = null)
        {
            _postModel = postModel;
            EcryptRequestDocument = ecryptRequestDocument;//原始加密XML转成XDocument

            Init();
        }

        public XDocument Init()
        {
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
                return null;
            }

            RequestDocument = XDocument.Parse(msgXml);//完成解密
            RequestMessage = RequestMessageFactory.GetRequestEntity(RequestDocument);

            //((RequestMessageBase)RequestMessage).FillEntityWithXml(RequestDocument);

            return RequestDocument;
        }

        public void Execute()
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
                    case RequestInfoType.component_verify_ticket:
                        {
                            var requestMessage = RequestMessage as RequestMessageComponentVerifyTicket;
                            ResponseMessageText = OnComponentVerifyTicketRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.unauthorized:
                        {
                            var requestMessage = RequestMessage as RequestMessageUnauthorized;
                            ResponseMessageText = OnUnauthorizedRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.authorized:
                        {
                            var requestMessage = RequestMessage as RequestMessageAuthorized;
                            ResponseMessageText = OnAuthorizedRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.updateauthorized:
                        {
                            var requestMessage = RequestMessage as RequestMessageUpdateAuthorized;
                            ResponseMessageText = OnUpdateAuthorizedRequest(requestMessage);
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

        /// <summary>
        /// 授权成功通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual string OnAuthorizedRequest(RequestMessageAuthorized requestMessage)
        {
            return "success";
        }

        /// <summary>
        /// 授权更新通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual string OnUpdateAuthorizedRequest(RequestMessageUpdateAuthorized requestMessage)
        {
            return "success";
        }
    }
}
