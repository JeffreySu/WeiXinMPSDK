#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：MessageHandlerAsync.cs
    文件功能描述：微信请求【异步方法】的集中处理方法
    
    
    创建标识：Senparc - 20180122
   
    
    修改标识：Senparc - 20191003
    修改描述：优化 DefaultAsyncMethod 
    
----------------------------------------------------------------*/


using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.MessageHandlers
{


    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin.MP的基础功能，只为简化代码而设。
    /// </summary>
    public abstract partial class MessageHandler<TMC> :
        MessageHandler<TMC, IRequestMessageBase, IResponseMessageBase>, IMessageHandler
        where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        /// <summary>
        /// 自动判断默认异步方法调用（在没有override的情况下调用的默认方法）
        /// </summary>
        /// <param name="requestMessage">requestMessage</param>
        /// <param name="syncMethod">同名的同步方法(DefaultMessageHandlerAsyncEvent值为SelfSynicMethod时调用)</param>
        /// <returns></returns>
        private async Task<IResponseMessageBase> DefaultAsyncMethod(IRequestMessageBase requestMessage, Func<IResponseMessageBase> syncMethod)
        {
            switch (base.DefaultMessageHandlerAsyncEvent)
            {
                case DefaultMessageHandlerAsyncEvent.DefaultResponseMessageAsync:
                    //返回默认信息
                    return await DefaultResponseMessageAsync(requestMessage).ConfigureAwait(false);
                case DefaultMessageHandlerAsyncEvent.SelfSynicMethod:
                    //返回同步信息
                    return await Task.Run(syncMethod).ConfigureAwait(false);
                default:
                    throw new MessageHandlerException($"DefaultMessageHandlerAsyncEvent 类型未作处理：{base.DefaultMessageHandlerAsyncEvent.ToString()}");
            }
        }

        /// <summary>
        /// 【异步方法】执行微信请求
        /// </summary>
        public override async Task BuildResponseMessageAsync(CancellationToken cancellationToken)
        {
            #region NeuChar 执行过程

            var weixinAppId = this._postModel == null ? "" : this._postModel.AppId;

            switch (RequestMessage.MsgType)
            {
                case RequestMsgType.Text:
                    {
                        try
                        {
                            var requestMessage = RequestMessage as RequestMessageText;
                            ResponseMessage = (await CurrentMessageHandlerNode.ExecuteAsync(requestMessage, this, weixinAppId).ConfigureAwait(false)
                                               ?? (await OnTextOrEventRequestAsync(requestMessage).ConfigureAwait(false)))
                                                   ?? (await OnTextRequestAsync(requestMessage).ConfigureAwait(false));
                        }
                        catch (Exception ex)
                        {
                            SenparcTrace.SendCustomLog("mp-response error", ex.Message + "\r\n|||\r\n" + (ex.InnerException != null ? ex.InnerException.ToString() : ""));
                        }
                    }
                    break;
                case RequestMsgType.Location:
                    ResponseMessage = await OnLocationRequestAsync(RequestMessage as RequestMessageLocation).ConfigureAwait(false);
                    break;
                case RequestMsgType.Image:
                    //WeixinTrace.SendCustomLog("NeuChar Image", $"appid:{weixinAppId}");
                    ResponseMessage = await CurrentMessageHandlerNode.ExecuteAsync(RequestMessage, this, weixinAppId).ConfigureAwait(false)
                                            ?? await OnImageRequestAsync(RequestMessage as RequestMessageImage).ConfigureAwait(false);
                    break;
                case RequestMsgType.Voice:
                    ResponseMessage = await OnVoiceRequestAsync(RequestMessage as RequestMessageVoice).ConfigureAwait(false);
                    break;
                case RequestMsgType.Video:
                    ResponseMessage = await OnVideoRequestAsync(RequestMessage as RequestMessageVideo).ConfigureAwait(false);
                    break;
                case RequestMsgType.Link:
                    ResponseMessage = await OnLinkRequestAsync(RequestMessage as RequestMessageLink).ConfigureAwait(false);
                    break;
                case RequestMsgType.ShortVideo:
                    ResponseMessage = await OnShortVideoRequestAsync(RequestMessage as RequestMessageShortVideo).ConfigureAwait(false);
                    break;
                case RequestMsgType.File:
                    ResponseMessage = await OnFileRequestAsync(RequestMessage as RequestMessageFile).ConfigureAwait(false);
                    break;
                case RequestMsgType.NeuChar:
                    ResponseMessage = await OnNeuCharRequestAsync(RequestMessage as RequestMessageNeuChar).ConfigureAwait(false);
                    break;
                case RequestMsgType.Unknown:
                    Weixin.WeixinTrace.SendCustomLog("RequestMsgType.Unknown调试", "RequestMessageDocument：" + ResponseDocument?.ToString());
                    Weixin.WeixinTrace.SendCustomLog("RequestMsgType.Unknown调试", "RequestMessage：" + RequestMessage?.ToJson(true));

                    ResponseMessage = await OnUnknownTypeRequestAsync(RequestMessage as RequestMessageUnknownType).ConfigureAwait(false);
                    break;
                case RequestMsgType.Event:
                    {
                        var requestMessageText = (RequestMessage as IRequestMessageEventBase).ConvertToRequestMessageText();
                        ResponseMessage = (await CurrentMessageHandlerNode.ExecuteAsync(RequestMessage, this, weixinAppId).ConfigureAwait(false)
                                           ?? await OnTextOrEventRequestAsync(requestMessageText).ConfigureAwait(false))
                                              ?? (await OnEventRequestAsync(RequestMessage as IRequestMessageEventBase).ConfigureAwait(false));
                    }
                    break;

                default:
                    Weixin.WeixinTrace.SendCustomLog("NeuChar", "未知的MsgType请求类型" + RequestMessage.MsgType);
                    //throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
                    break;
            }

            #endregion
        }

        /// <summary>
        /// 【异步方法】OnExecutingAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task OnExecutingAsync(CancellationToken cancellationToken)
        {
            /* 
            * 此处原消息去重逻辑已经转移到 基类CommonInitialize() 方法中（执行完 Init() 方法之后进行判断）。
            * 原因是插入RequestMessage过程发生在Init中，从Init执行到此处的时间内，
            * 如果有新消息加入，将导致去重算法失效。
            * （当然这样情况发生的概率极低，一般只在测试中会发生，
            * 为了确保各种测试环境下的可靠性，作此修改。  —— Jeffrey Su 2018.1.23
            */

            /* 
            * 已经启用以异步方法优先的策略，将原有 OnExecuting() 方法在此处执行  —— Jeffrey Su 20191004
            */

            //#endregion
            if (CancelExecute)
            {
                return;
            }

            await base.OnExecutingAsync(cancellationToken).ConfigureAwait(false);

            var currentMessageContext = await GetCurrentMessageContext().ConfigureAwait(false);

            //判断是否已经接入开发者信息
            if (DeveloperInfo != null || currentMessageContext.AppStoreState == AppStoreState.Enter)
            {
                //优先请求云端应用商店资源
            }
        }

        /// <summary>
        /// 【异步方法】OnExecutedAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task OnExecutedAsync(CancellationToken cancellationToken)
        {
            await base.OnExecutedAsync(cancellationToken).ConfigureAwait(false);
        }

    }
}
