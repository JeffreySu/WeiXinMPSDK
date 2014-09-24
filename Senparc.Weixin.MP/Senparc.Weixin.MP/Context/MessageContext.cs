using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Context
{
    public interface IMessageContext : Weixin.Context.IMessageContext
    {

    }

    /// <summary>
    /// 微信消息上下文（单个用户）
    /// </summary>
    public class MessageContext : Weixin.Context.MessageContext, IMessageContext
    {
        public MessageContext()
        {

        }
    }
}
