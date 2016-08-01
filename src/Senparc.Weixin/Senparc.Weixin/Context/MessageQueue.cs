/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：MessageQueue.cs
    文件功能描述：微信消息列队（针对单个账号的往来消息）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Context
{
    /// <summary>
    /// 微信消息列队（针对单个账号的往来消息）
    /// </summary>
    /// <typeparam name="TM"></typeparam>
    public class MessageQueue<TM,TRequest, TResponse> : List<TM> 
        where TM : class, IMessageContext<TRequest, TResponse>, new()
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {

    }
}
