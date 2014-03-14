using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Context
{
    /// <summary>
    /// 微信消息列队（针对单个账号的往来消息）
    /// </summary>
    /// <typeparam name="TM"></typeparam>
    public class MessageQueue<TM> : List<TM> where TM : class, IMessageContext, new()
    {

    }
}
