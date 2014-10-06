using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Context
{
    public interface IWeixinContext : Senparc.Weixin.Context.IMessageContext
    {
        /// <summary>
        /// 接收消息记录
        /// </summary>
        new MessageContainer<IRequestMessageBase> RequestMessages { get; set; }
        /// <summary>
        /// 响应消息记录
        /// </summary>
        new MessageContainer<IResponseMessageBase> ResponseMessages { get; set; }
    }

    public class WeixinContext : Weixin.Context.MessageContext, IWeixinContext
    {
       public MessageContainer<IRequestMessageBase> RequestMessages
       {
           get
           {
               return base.RequestMessages as MessageContainer<IRequestMessageBase>;
           }
           set
           {
               base.RequestMessages = value as MessageContainer<Senparc.Weixin.Entities.IRequestMessageBase>;
           }
       }
        public MessageContainer<IResponseMessageBase> ResponseMessages { get; set; }
    }
}
