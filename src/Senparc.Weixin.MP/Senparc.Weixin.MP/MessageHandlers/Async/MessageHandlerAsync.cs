﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：MessageHandlerAsync.cs
    文件功能描述：微信请求【异步方法】的集中处理方法
    
    
    创建标识：Senparc - 20180122
   
----------------------------------------------------------------*/

#if !NET35 && !NET40
using System;
using System.IO;
using System.Xml.Linq;
using Senparc.NeuChar.Context;
using Senparc.Weixin.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.MP.AppStore;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.Tencent;
using System.Threading.Tasks;
using Senparc.NeuChar;
using System.Collections.Generic;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.MessageHandlers
{


    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin.MP的基础功能，只为简化代码而设。
    /// </summary>
    public abstract partial class MessageHandler<TC> :
        MessageHandler<TC, IRequestMessageBase, IResponseMessageBase>, IMessageHandler
        where TC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        /// <summary>
        /// 自动判断默认异步方法调用（在没有override的情况下调用的默认方法）
        /// </summary>
        /// <param name="requestMessage">requestMessage</param>
        /// <param name="syncMethod">同名的同步方法(DefaultMessageHandlerAsyncEvent值为SelfSynicMethod时调用)</param>
        /// <returns></returns>
        private async Task<IResponseMessageBase> DefaultAsyncMethod(IRequestMessageBase requestMessage, Func<IResponseMessageBase> syncMethod)
        {
            return (base.DefaultMessageHandlerAsyncEvent == DefaultMessageHandlerAsyncEvent.DefaultResponseMessageAsync
                            ? await DefaultResponseMessageAsync(requestMessage)
                            : await Task.Run(syncMethod));
        }

        /// <summary>
        /// 【异步方法】执行微信请求
        /// </summary>
        public override async Task ExecuteAsync()
        {
            if (CancelExcute)
            {
                return;
            }

            await OnExecutingAsync();

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


                #region NeuChar 执行过程

                //TODO:Neuchar：在这里先做一次NeuChar标准的判断

                var neuralSystem = NeuralSystem.Instance;

                //获取当前设置节点
                var messageHandlerNode = neuralSystem.GetNode("MessageHandlerNode") as MessageHandlerNode;

                switch (RequestMessage.MsgType)
                {
                    case RequestMsgType.Text:
                        {
                            try
                            {
                                var requestMessage = RequestMessage as RequestMessageText;
                                ResponseMessage = await messageHandlerNode.ExecuteAsync(requestMessage, this, Config.SenparcWeixinSetting.WeixinAppId) 
                                    ?? ((await (OnTextOrEventRequestAsync(requestMessage))
                                    ?? (await OnTextRequestAsync(requestMessage))));
                            }
                            catch (Exception ex)
                            {
                                SenparcTrace.SendCustomLog("mp-response error", ex.Message + "\r\n|||\r\n" + (ex.InnerException != null ? ex.InnerException.ToString() : ""));

                            }

                        }
                        break;
                    case RequestMsgType.Location:
                        ResponseMessage = await OnLocationRequestAsync(RequestMessage as RequestMessageLocation);
                        break;
                    case RequestMsgType.Image:

                        WeixinTrace.SendCustomLog("NeuChar Image", $"appid:{Config.SenparcWeixinSetting.WeixinAppId}");

                        ResponseMessage = await messageHandlerNode.ExecuteAsync(RequestMessage, this, Config.SenparcWeixinSetting.WeixinAppId) ?? await OnImageRequestAsync(RequestMessage as RequestMessageImage);
                        break;
                    case RequestMsgType.Voice:
                        ResponseMessage = await OnVoiceRequestAsync(RequestMessage as RequestMessageVoice);
                        break;
                    case RequestMsgType.Video:
                        ResponseMessage = await OnVideoRequestAsync(RequestMessage as RequestMessageVideo);
                        break;
                    case RequestMsgType.Link:
                        ResponseMessage = await OnLinkRequestAsync(RequestMessage as RequestMessageLink);
                        break;
                    case RequestMsgType.ShortVideo:
                        ResponseMessage = await OnShortVideoRequestAsync(RequestMessage as RequestMessageShortVideo);
                        break;
                    case RequestMsgType.File:
                        ResponseMessage = await OnFileRequestAsync(RequestMessage as RequestMessageFile);
                        break;
                    case RequestMsgType.NeuChar:
                        ResponseMessage = await OnNeuCharRequestAsync(RequestMessage as RequestMessageNeuChar);
                        break;
                    case RequestMsgType.Unknown:
                        ResponseMessage = await OnUnknownTypeRequestAsync(RequestMessage as RequestMessageUnknownType);
                        break;
                    case RequestMsgType.Event:
                        {
                            var requestMessageText = (RequestMessage as IRequestMessageEventBase).ConvertToRequestMessageText();
                            ResponseMessage = (await (OnTextOrEventRequestAsync(requestMessageText)))
                                ?? (await OnEventRequestAsync(RequestMessage as IRequestMessageEventBase));
                        }
                        break;

                    default:
                        Weixin.WeixinTrace.SendCustomLog("NeuChar", "未知的MsgType请求类型" + RequestMessage.MsgType);
                        //throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
                        break;
                }

                #endregion


                //记录上下文
                //此处修改
                if (MessageContextGlobalConfig.UseMessageContext && ResponseMessage != null && !string.IsNullOrEmpty(ResponseMessage.FromUserName))
                {
                    GlobalMessageContext.InsertMessage(ResponseMessage);
                }
            }
            catch (Exception ex)
            {
                throw new MessageHandlerException("MessageHandler中Execute()过程发生错误：" + ex.Message, ex);
            }
            finally
            {
                await OnExecutedAsync();
            }
        }

        /// <summary>
        /// 【异步方法】OnExecutingAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task OnExecutingAsync()
        {
            //已放入Init()方法中
            //#region 消息去重

            //if ((OmitRepeatedMessageFunc == null || OmitRepeatedMessageFunc(RequestMessage) == true)
            //&& OmitRepeatedMessage && CurrentMessageContext.RequestMessages.Count > 1
            ////&& !(RequestMessage is RequestMessageEvent_Merchant_Order)批量订单的MsgId可能会相同
            //)
            //{
            //    var currentIndex = CurrentMessageContext.RequestMessages.FindLastIndex(z=>z.)


            //    var lastMessage = CurrentMessageContext.RequestMessages[CurrentMessageContext.RequestMessages.Count - 2];

            //    if (
            //        //使用MsgId去重
            //        (lastMessage.MsgId != 0 && lastMessage.MsgId == RequestMessage.MsgId)
            //        //使用CreateTime去重（OpenId对象已经是同一个）
            //        || (lastMessage.MsgId == RequestMessage.MsgId
            //            && lastMessage.CreateTime == RequestMessage.CreateTime
            //            && lastMessage.MsgType == RequestMessage.MsgType)
            //        )
            //    {
            //        CancelExcute = true;//重复消息，取消执行
            //        MessageIsRepeated = true;
            //        return;
            //    }
            //}

            //#endregion

            await base.OnExecutingAsync();

            //判断是否已经接入开发者信息
            if (DeveloperInfo != null || CurrentMessageContext.AppStoreState == AppStoreState.Enter)
            {
                //优先请求云端应用商店资源
            }
        }

        /// <summary>
        /// 【异步方法】OnExecutedAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task OnExecutedAsync()
        {
            await base.OnExecutedAsync();
        }

    }
}
#endif