using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Context
{
    public interface IMessageContext<TRequestMessageContainer, TResponseMessageContainer, TIRequestMessageBase, TIResponseMessageBase, TWeixinContextRemovedEventArgs>
        where TRequestMessageContainer : MessageContainer<TIRequestMessageBase>, new()
        where TResponseMessageContainer : MessageContainer<TIResponseMessageBase>, new()
        where TIRequestMessageBase : IRequestMessageBase
        where TIResponseMessageBase : IResponseMessageBase
        where TWeixinContextRemovedEventArgs : WeixinContextRemovedEventArgs, new()
    {
        /// <summary>
        /// 用户名（OpenID）
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// 最后一次活动时间（用户主动发送Resquest请求的时间）
        /// </summary>
        DateTime LastActiveTime { get; set; }
        /// <summary>
        /// 接收消息记录
        /// </summary>
        TRequestMessageContainer RequestMessages { get; set; }
        /// <summary>
        /// 响应消息记录
        /// </summary>
        TResponseMessageContainer ResponseMessages { get; set; }
        /// <summary>
        /// 最大储存容量（分别针对RequestMessages和ResponseMessages）
        /// </summary>
        int MaxRecordCount { get; set; }
        /// <summary>
        /// 临时储存数据，如用户状态等，出于保持.net 3.5版本，这里暂不使用dynamic
        /// </summary>
        object StorageData { get; set; }

        /// <summary>
        /// 用于覆盖WeixinContext所设置的默认过期时间
        /// </summary>
        Double? ExpireMinutes { get; set; }

        event EventHandler<TWeixinContextRemovedEventArgs> MessageContextRemoved;

        void OnRemoved();
    }

    /// <summary>
    /// 微信消息上下文（单个用户）
    /// </summary>
    public class MessageContext<TRequestMessageContainer, TResponseMessageContainer, TIRequestMessageBase, TIResponseMessageBase, TWeixinContextRemovedEventArgs>
        : IMessageContext<TRequestMessageContainer, TResponseMessageContainer, TIRequestMessageBase, TIResponseMessageBase, TWeixinContextRemovedEventArgs>
        where TRequestMessageContainer : MessageContainer<TIRequestMessageBase>, new()
        where TResponseMessageContainer : MessageContainer<TIResponseMessageBase>, new()
        where TIRequestMessageBase : IRequestMessageBase
        where TIResponseMessageBase : IResponseMessageBase
        where TWeixinContextRemovedEventArgs : WeixinContextRemovedEventArgs, new()
    {
        private int _maxRecordCount;

        public string UserName { get; set; }
        public DateTime LastActiveTime { get; set; }
        public TRequestMessageContainer RequestMessages { get; set; }
        public TResponseMessageContainer ResponseMessages { get; set; }
        public int MaxRecordCount
        {
            get
            {
                return _maxRecordCount;
            }
            set
            {
                RequestMessages.MaxRecordCount = value;
                ResponseMessages.MaxRecordCount = value;

                _maxRecordCount = value;
            }
        }
        public object StorageData { get; set; }

        public Double? ExpireMinutes { get; set; }

        public virtual event EventHandler<TWeixinContextRemovedEventArgs> MessageContextRemoved = null;

        /// <summary>
        /// 执行上下文被移除的事件
        /// 注意：此事件不是实时触发的，而是等过期后任意一个人发过来的下一条消息执行之前触发。
        /// </summary>
        /// <param name="e"></param>
        private void OnMessageContextRemoved(TWeixinContextRemovedEventArgs e)
        {
            EventHandler<TWeixinContextRemovedEventArgs> temp = MessageContextRemoved;

            if (temp != null)
            {
                temp(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxRecordCount">maxRecordCount如果小于等于0，则不限制</param>
        public MessageContext()
        {
            /*
             * 注意：即使使用其他类实现IMessageContext，
             * 也务必在这里进行下面的初始化，尤其是设置当前时间，
             * 这个时间关系到及时从缓存中移除过期的消息，节约内存使用
             */

            RequestMessages = new TRequestMessageContainer() { MaxRecordCount = MaxRecordCount };
            ResponseMessages = new TResponseMessageContainer() { MaxRecordCount = MaxRecordCount };
            LastActiveTime = DateTime.Now;
        }

        public virtual void OnRemoved()
        {
            var onRemovedArg = new TWeixinContextRemovedEventArgs() { MessageContext = this as MessageContext<MessageContainer<IRequestMessageBase>, MessageContainer<IResponseMessageBase>, IRequestMessageBase, IResponseMessageBase, WeixinContextRemovedEventArgs> };
            OnMessageContextRemoved(onRemovedArg);
        }
    }
}
