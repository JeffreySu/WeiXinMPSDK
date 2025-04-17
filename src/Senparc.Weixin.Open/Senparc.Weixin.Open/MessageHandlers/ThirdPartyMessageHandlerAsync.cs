/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
  
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

    修改标识：xbotter - 20250401
    修改描述：新增ThirdPartyMessageHandlerAsync类，支持异步处理消息

----------------------------------------------------------------*/


using System;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities.Request;
using Senparc.Weixin.Tencent;
using System.Threading;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.MessageHandlers;

namespace Senparc.Weixin.Open.MessageHandlers
{
    public abstract partial class ThirdPartyMessageHandler
    {

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (CancelExecute)
            {
                return;
            }

            await OnExecutingAsync(cancellationToken);

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
                            ResponseMessageText = await OnComponentVerifyTicketRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.unauthorized:
                        {
                            var requestMessage = RequestMessage as RequestMessageUnauthorized;
                            ResponseMessageText = await OnUnauthorizedRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.authorized:
                        {
                            var requestMessage = RequestMessage as RequestMessageAuthorized;
                            ResponseMessageText = await OnAuthorizedRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.updateauthorized:
                        {
                            var requestMessage = RequestMessage as RequestMessageUpdateAuthorized;
                            ResponseMessageText = await OnUpdateAuthorizedRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.notify_third_fasteregister:
                        {
                            var requestMessage = RequestMessage as RequestMessageThirdFasteRegister;
                            ResponseMessageText = await OnThirdFastRegisterRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.notify_third_fastverifybetaapp:
                        {
                            var requestMessage = RequestMessage as RequestMessageFastVerifyBetaApp;
                            ResponseMessageText = await OnFastVerifyBetaAppRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.notify_third_fastregisterbetaapp:
                        {
                            var requestMessage = RequestMessage as RequestMessageFastRegisterBetaAppApp;
                            ResponseMessageText = await OnFastRegisterBetaAppRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.notify_icpfiling_verify_result:
                        {
                            var requestMessage = RequestMessage as RequestMessageIcpFilingVerify;
                            ResponseMessageText = await OnIcpFilingVerifyRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.notify_apply_icpfiling_result:
                        {
                            var requestMessage = RequestMessage as RequestMessageIcpFilingApply;
                            ResponseMessageText = await OnIcpFilingApplyRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.notify_3rd_wxa_auth:
                        {
                            var requestMessage = RequestMessage as RequestMessage3rdWxaAuth;
                            ResponseMessageText = await On3rdWxaAuthRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.notify_3rd_wxa_wxverify:
                        {
                            var requestMessage = RequestMessage as RequestMessage3rdWxaWxVerify;
                            ResponseMessageText = await On3rdWxaWxVerifyRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.order_path_apply_result_notify:
                        {
                            var requestMessage = RequestMessage as RequestMessageOrderPathApplyResultNotify;
                            ResponseMessageText = await OnOrderPathApplyResultNotifyRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    case RequestInfoType.order_path_audit_result_notify:
                        {
                            var requestMessage = RequestMessage as RequestMessageOrderPathAuditResultNotify;
                            ResponseMessageText = await OnOrderPathAuditResultNotifyRequestAsync(requestMessage, cancellationToken);
                        }
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException("未知的InfoType请求类型", null);
                }

            }
            catch (Exception ex)
            {
                throw new MessageHandlerException("ThirdPartyMessageHandler中ExecuteAsync()过程发生错误：" + ex.Message, ex);
            }
            finally
            {
                await OnExecutedAsync(cancellationToken);
            }
        }

        public virtual Task OnExecutingAsync(CancellationToken cancellationToken)
        {
            OnExecuting();
            return Task.CompletedTask;
        }

        public virtual Task OnExecutedAsync(CancellationToken cancellationToken)
        {
            OnExecuted();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 小程序订单页设置申请通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnOrderPathApplyResultNotifyRequestAsync(RequestMessageOrderPathApplyResultNotify requestMessage, CancellationToken cancellationToken)
        {
             var result = OnOrderPathApplyResultNorifyRequest(requestMessage);
             return Task.FromResult(result);
        }

        /// <summary>
        /// 小程序订单页设置审核结果通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnOrderPathAuditResultNotifyRequestAsync(RequestMessageOrderPathAuditResultNotify requestMessage, CancellationToken cancellationToken)
        {
            var result = OnOrderPathAuditResultNotifyRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 小程序认证年审和过期能力限制提醒推送事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> On3rdWxaWxVerifyRequestAsync(RequestMessage3rdWxaWxVerify requestMessage, CancellationToken cancellationToken)
        {
            var result = On3rdWxaWxVerifyRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 微信认证推送事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> On3rdWxaAuthRequestAsync(RequestMessage3rdWxaAuth requestMessage, CancellationToken cancellationToken)
        {
            var result = On3rdWxaAuthRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 当备案审核被驳回或通过时会推送通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnIcpFilingApplyRequestAsync(RequestMessageIcpFilingApply requestMessage, CancellationToken cancellationToken)
        {
            var result = OnIcpFilingApplyRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 小程序管理员人脸核身完成事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnIcpFilingVerifyRequestAsync(RequestMessageIcpFilingVerify requestMessage, CancellationToken cancellationToken)
        {
            var result = OnIcpFilingVerifyRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 推送component_verify_ticket协议
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnComponentVerifyTicketRequestAsync(RequestMessageComponentVerifyTicket requestMessage, CancellationToken cancellationToken)
        {
            var result = OnComponentVerifyTicketRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 推送取消授权通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnUnauthorizedRequestAsync(RequestMessageUnauthorized requestMessage, CancellationToken cancellationToken)
        {
            var result = OnUnauthorizedRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 授权成功通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnAuthorizedRequestAsync(RequestMessageAuthorized requestMessage, CancellationToken cancellationToken)
        {
            var result = OnAuthorizedRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 授权更新通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnUpdateAuthorizedRequestAsync(RequestMessageUpdateAuthorized requestMessage, CancellationToken cancellationToken)
        {
            var result = OnUpdateAuthorizedRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 小程序注册审核事件通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnThirdFastRegisterRequestAsync(RequestMessageThirdFasteRegister requestMessage, CancellationToken cancellationToken)
        {
            var result = OnThirdFasteRegisterRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 试用小程序快速认证事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnFastVerifyBetaAppRequestAsync(RequestMessageFastVerifyBetaApp requestMessage, CancellationToken cancellationToken)
        {
            var result = OnFastVerifyBetaAppRequest(requestMessage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 创建试用小程序成功/失败的事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> OnFastRegisterBetaAppRequestAsync(RequestMessageFastRegisterBetaAppApp requestMessage, CancellationToken cancellationToken)
        {
            var result = OnFastRegisterBetaAppRequest(requestMessage);
            return Task.FromResult(result);
        }
    }
}
