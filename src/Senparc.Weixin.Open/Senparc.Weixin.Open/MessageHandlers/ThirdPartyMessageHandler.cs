/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
  
    文件名：ThirdPartyMessageHandler.cs
    文件功能描述：开放平台消息处理器
    
    
    创建标识：Senparc - 20150211

    修改标识：Senparc - 20160813
    修改描述：v2.3.0 添加authorized和updateauthorized两种通知类型的处理

    修改标识：Senparc - 20181030
    修改描述：v4.1.15 优化 MessageHandler 构造函数，提供 PostModel 默认值

    修改标识：mc7246 - 20220402
    修改描述：v4.13.9 添加试用小程序接口及事件

    修改标识：mc7246 - 20231211
    修改描述：添加小程序微信认证事件第三方通知推送

----------------------------------------------------------------*/


using System;
using System.IO;
using System.Xml.Linq;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities.Request;
using Senparc.Weixin.Tencent;

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

        public bool CancelExecute { get; set; }

        public ThirdPartyMessageHandler(Stream inputStream, PostModel postModel = null)
        {
            EcryptRequestDocument = XmlUtility.Convert(inputStream);//原始加密XML转成XDocument

            Init(postModel);
        }

        public ThirdPartyMessageHandler(XDocument ecryptRequestDocument, PostModel postModel = null)
        {
            EcryptRequestDocument = ecryptRequestDocument;//原始加密XML转成XDocument

            Init(postModel);
        }

        public XDocument Init(IEncryptPostModel postModel)
        {
            _postModel = postModel as PostModel ?? new PostModel();

            //解密XML信息
            var postDataStr = EcryptRequestDocument.ToString();

            WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.AppId);
            string msgXml = null;
            var result = msgCrype.DecryptMsg(_postModel.Msg_Signature, _postModel.Timestamp, _postModel.Nonce, postDataStr, ref msgXml);

            //判断result类型
            if (result != 0)
            {
                //验证没有通过，取消执行
                CancelExecute = true;
                return null;
            }

            RequestDocument = XDocument.Parse(msgXml);//完成解密
            RequestMessage = RequestMessageFactory.GetRequestEntity(RequestDocument);

            //((RequestMessageBase)RequestMessage).FillEntityWithXml(RequestDocument);

            return RequestDocument;
        }

        public void Execute()
        {
            if (CancelExecute)
            {
                return;
            }

            OnExecuting();

            if (CancelExecute)
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
                    case RequestInfoType.notify_third_fasteregister:
                        {
                            var requestMessage = RequestMessage as RequestMessageThirdFasteRegister;
                            ResponseMessageText = OnThirdFasteRegisterRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.wxa_nickname_audit:
                        {
                            var requestMessage = RequestMessage as RequestMessageNicknameAudit;
                            ResponseMessageText = OnNicknameAuditRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.notify_third_fastverifybetaapp:
                        {
                            var requestMessage = RequestMessage as RequestMessageFastVerifyBetaApp;
                            ResponseMessageText = OnFastVerifyBetaAppRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.notify_third_fastregisterbetaapp:
                        {
                            var requestMessage = RequestMessage as RequestMessageFastRegisterBetaAppApp;
                            ResponseMessageText = OnFastRegisterBetaAppRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.notify_icpfiling_verify_result:
                        {
                            var requestMessage = RequestMessage as RequestMessageIcpFilingVerify;
                            ResponseMessageText = OnIcpFilingVerifyRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.notify_apply_icpfiling_result:
                        {
                            var requestMessage = RequestMessage as RequestMessageIcpFilingApply;
                            ResponseMessageText = OnIcpFilingApplyRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.notify_3rd_wxa_auth:
                        {
                            var requestMessage = RequestMessage as RequestMessage3rdWxaAuth;
                            ResponseMessageText = On3rdWxaAuthRequest(requestMessage);
                        }
                        break;
                    case RequestInfoType.notify_3rd_wxa_wxverify:
                        {
                            var requestMessage = RequestMessage as RequestMessage3rdWxaWxVerify;
                            ResponseMessageText = On3rdWxaWxVerifyRequest(requestMessage);
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

        public virtual string On3rdWxaWxVerifyRequest(RequestMessage3rdWxaWxVerify requestMessage)
        {
            return "success";
        }


        public virtual string On3rdWxaAuthRequest(RequestMessage3rdWxaAuth requestMessage)
        {
            return "success";
        }

        public virtual string OnIcpFilingApplyRequest(RequestMessageIcpFilingApply requestMessage) 
        {
            return "success";
        }


        public virtual string OnIcpFilingVerifyRequest(RequestMessageIcpFilingVerify requestMessage)
        {
            return "success";

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

        public virtual string OnThirdFasteRegisterRequest(RequestMessageThirdFasteRegister requestMessage)
        {
            return "success";
        }


        [Obsolete("此事件请在小程序SDK处理")]
        public virtual string OnNicknameAuditRequest(RequestMessageNicknameAudit requestMessage)
        {
            return "success";
        }

        public virtual string OnFastVerifyBetaAppRequest(RequestMessageFastVerifyBetaApp requestMessage)
        {
            return "success";
        }

        public virtual string OnFastRegisterBetaAppRequest(RequestMessageFastRegisterBetaAppApp requestMessage)
        {
            return "success";
        }


    }
}
