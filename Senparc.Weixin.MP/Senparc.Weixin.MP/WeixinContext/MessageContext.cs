using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.WeixinContext
{
    /// <summary>
    /// 微信消息上下文（单个用户）
    /// </summary>
    public class MessageContext<TM>
    {
        /// <summary>
        /// 用户名（OpenID）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 最后一次活动时间
        /// </summary>
        public DateTime LastActiveTime { get; set; }

        public List<IRequestMessageBase> RequestMessages { get; set; }
        public List<IResponseMessageBase> ResponseMessages { get; set; }

        /// <summary>
        /// 临时储存数据，如用户状态等
        /// </summary>
        public TM StorageData { get; set; }
    }
}
