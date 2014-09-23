using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Entities
{
    public interface IMessageBase
    {
        string ToUserName { get; set; }
        string FromUserName { get; set; }
        DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 所有Request和Response消息的基类
    /// </summary>
    public abstract class MessageBase
    {
        public abstract string ToUserName { get; set; }
        public abstract string FromUserName { get; set; }
        public abstract DateTime CreateTime { get; set; }
    }
}
