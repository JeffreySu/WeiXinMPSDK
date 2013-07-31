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
        /// 最后一次活动时间（用户主动发送Resquest请求的时间）
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
            /*
             * 注意：即使使用其他类实现IMessageContext，
             * 也务必在这里进行下面的初始化，尤其是设置当前时间，
             * 这个时间关系到及时从缓存中移除过期的消息，节约内存使用
             */
            RequestMessages = new List<IRequestMessageBase>();
            ResponseMessages = new List<IResponseMessageBase>();
            LastActiveTime = DateTime.Now;
        }
    }
}
