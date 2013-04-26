using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Context
{
    public interface IMessageContext
    {
        /// <summary>
        /// 用户名（OpenID）
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// 最后一次活动时间
        /// </summary>
        DateTime LastActiveTime { get; set; }
        /// <summary>
        /// 接收消息记录
        /// </summary>
        List<IRequestMessageBase> RequestMessages { get; set; }
        /// <summary>
        /// 响应消息记录
        /// </summary>
        List<IResponseMessageBase> ResponseMessages { get; set; }
        /// <summary>
        /// 临时储存数据，如用户状态等，出于保持.net 3.5版本，这里暂不使用dynamic
        /// </summary>
        object StorageData { get; set; }
    }

    /// <summary>
    /// 微信消息上下文（单个用户）
    /// </summary>
    public class MessageContext : IMessageContext
    {
        public string UserName { get; set; }
        public DateTime LastActiveTime { get; set; }
        public List<IRequestMessageBase> RequestMessages { get; set; }
        public List<IResponseMessageBase> ResponseMessages { get; set; }

        public object StorageData { get; set; }

        public MessageContext()
        {
            RequestMessages = new List<IRequestMessageBase>();
            ResponseMessages = new List<IResponseMessageBase>();
            LastActiveTime = DateTime.Now;
        }
    }
}
