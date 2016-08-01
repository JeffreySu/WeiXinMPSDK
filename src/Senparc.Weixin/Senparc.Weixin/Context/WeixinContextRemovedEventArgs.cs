/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WeixinContextRemovedEventArgs.cs
    文件功能描述：对话上下文被删除时触发事件的事件数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Context
{

    /// <summary>
    /// 对话上下文被删除时触发事件的事件数据
    /// </summary>
    public class WeixinContextRemovedEventArgs<TRequest,TResponse> : EventArgs
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
        /// <summary>
        /// 该用户的OpenId
        /// </summary>
        public string OpenId
        {
            get
            {
                return MessageContext.UserName;
            }
        }
        /// <summary>
        /// 最后一次响应时间
        /// </summary>
        public DateTime LastActiveTime
        {
            get
            {
                return MessageContext.LastActiveTime;
            }
        }

        /// <summary>
        /// 上下文对象
        /// </summary>
        public IMessageContext<TRequest, TResponse> MessageContext { get; set; }

        public WeixinContextRemovedEventArgs(IMessageContext<TRequest, TResponse> messageContext)
        {
            MessageContext = messageContext;
        }
    }
}
