using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public interface IMessageBase : Weixin.Entities.IMessageBase
    {

    }

    /// <summary>
    /// 所有Request和Response消息的基类
    /// </summary>
    public class MessageBase : Weixin.Entities.MessageBase, IMessageBase
    {
        public override string ToUserName { get; set; }
        public override string FromUserName { get; set; }
        public override DateTime CreateTime { get; set; }
    }
}
