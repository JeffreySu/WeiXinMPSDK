using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Context
{
    /// <summary>
    /// 微信消息列队（针对单个账号的往来消息）
    /// </summary>
    /// <typeparam name="TM"></typeparam>
    public class MessageQueue<TM,TRest, TResp> : List<TM> 
        where TM : class, IMessageContext<TRest, TResp>, new()
        where TRest : IRequestMessageBase
        where TResp : IResponseMessageBase
    {

    }
}
