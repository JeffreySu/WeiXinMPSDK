using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.WeixinContext
{
    /// <summary>
    /// 微信消息上下文（全局）
    /// </summary>
    public class WeixinContext
    {
        public static object WeixinContextLock = new object();
        public static Dictionary<string, MessageContext> MessageCollection = new Dictionary<string, MessageContext>(StringComparer.OrdinalIgnoreCase);

        public WeixinContext()
        {
        }
    }
}
