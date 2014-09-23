/*
 * V2.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Context
{
    /// <summary>
    /// 微信消息上下文（全局）
    /// 默认过期时间：90分钟
    /// </summary>
    public class WeixinContext<TM> : Weixin.Context.WeixinContext<TM> where TM : class, IMessageContext, new()
    {

    }
}
