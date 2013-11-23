using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Context
{
    /// <summary>
    /// 对话上下文被删除时触发事件的事件数据
    /// </summary>
    public class WeixinContextRemovedEventArgs<TM> : EventArgs
        where TM : class, IMessageContext, new()
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
        public TM MessageContext { get; set; }

        public WeixinContextRemovedEventArgs(TM messageContext)
        {
            MessageContext = messageContext;
        }
    }
}
