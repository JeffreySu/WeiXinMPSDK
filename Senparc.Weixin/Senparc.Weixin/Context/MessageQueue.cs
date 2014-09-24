using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Context
{
    /// <summary>
    /// 微信消息列队（针对单个账号的往来消息）
    /// </summary>
    /// <typeparam name="TM"></typeparam>
    public class MessageQueue<TM,TRequestMessageContainer, TResponseMessageContainer, TIRequestMessageBase, TIResponseMessageBase, TWeixinContextRemovedEventArgs>
        : List<TM>
        where TM : class, IMessageContext<TRequestMessageContainer, TResponseMessageContainer, TIRequestMessageBase, TIResponseMessageBase, TWeixinContextRemovedEventArgs>, new()
        where TRequestMessageContainer : MessageContainer<TIRequestMessageBase>, new()
        where TResponseMessageContainer : MessageContainer<TIResponseMessageBase>, new()
        where TIRequestMessageBase : IRequestMessageBase
        where TIResponseMessageBase : IResponseMessageBase
        where TWeixinContextRemovedEventArgs : WeixinContextRemovedEventArgs, new() 
    {

    }
}
