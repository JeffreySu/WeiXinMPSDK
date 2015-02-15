using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
