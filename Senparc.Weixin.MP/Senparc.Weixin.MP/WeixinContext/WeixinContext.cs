using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.WeixinContext
{
    /// <summary>
    /// 微信消息上下文（全局）
    /// </summary>
    public class WeixinContext<TM>
    {
        public object WeixinContextLock = new object();
        public Dictionary<string, MessageContext<TM>> MessageCollection = new Dictionary<string, MessageContext<TM>>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 每一个MessageContext过期时间
        /// </summary>
        public int ExpireMinutes = 90;
        public WeixinContext()
        {
        }
    }
}
