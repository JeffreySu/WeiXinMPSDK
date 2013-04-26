using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.WeixinContext
{
    /// <summary>
    /// 微信消息上下文（单个用户）
    /// </summary>
    public class MessageContext
    {
        /// <summary>
        /// 用户名（OpenID）
        /// </summary>
        public string UserName { get; set; }
    }
}
