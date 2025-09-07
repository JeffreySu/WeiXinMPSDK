/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc

    文件名：WorkBotMessageHandler.Async.cs
    文件功能描述：企业号智能机器人请求的集中处理方法


    创建标识：Wang Qian - 20250825
----------------------------------------------------------------*/

using Senparc.NeuChar.Context;
using Senparc.Weixin.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Work.Entities;
using Senparc.NeuChar;
using System.Threading.Tasks;
using System.Threading;
using Senparc.Weixin.Work.Entities.Request.Event;
using System;

namespace Senparc.Weixin.Work.MessageHandlers
{
    public abstract partial class WorkBotMessageHandler<TMC>
        : MessageHandler<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase>, IWorkBotMessageHandler
        where TMC : class, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
        {
            public override async Task BuildResponseMessageAsync(CancellationToken cancellationToken)
            {
                switch (RequestMessage.MsgType)
                {
                    //以下是普通信息
                    case RequestMsgType.Text:
                    {
                        var requestMessage = RequestMessage as RequestMessageText;

                        ResponseMessage = await OnTextRequestAsync(requestMessage);           
                    }
                        break;
                    default:
                        ResponseMessage = await DefaultResponseMessageAsync(RequestMessage);
                        break;
                }
            }

            #region 接收消息方法
            public virtual async Task<IWorkResponseMessageBase> DefaultResponseMessageAsync(IWorkRequestMessageBase requestMessage)
            {
                return await Task.Run(() => DefaultResponseMessage(requestMessage)).ConfigureAwait(false);
            }

            public virtual async Task<IWorkResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
            {
                return await Task.Run(() => OnTextRequest(requestMessage)).ConfigureAwait(false);
            }

            #endregion
        }
}