using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Context
{
    //public interface IWeixinContext : Senparc.Weixin.Context.WeixinContext<IRequestMessageBase, IResponseMessageBase>
    //{
    //    /// <summary>
    //    /// 接收消息记录
    //    /// </summary>
    //    new MessageContainer<IRequestMessageBase> RequestMessages { get; set; }
    //    /// <summary>
    //    /// 响应消息记录
    //    /// </summary>
    //    new MessageContainer<IResponseMessageBase> ResponseMessages { get; set; }
    //}

    public class WeixinContext : Weixin.Context.WeixinContext<MessageContext<IRequestMessageBase, IResponseMessageBase>, IRequestMessageBase, IResponseMessageBase> 
        //, IWeixinContext
    {
      
    }
}
