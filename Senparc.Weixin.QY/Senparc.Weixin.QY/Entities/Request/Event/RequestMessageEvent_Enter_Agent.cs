using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 用户进入应用的事件推送(enter_agent)
    /// </summary>
    public class RequestMessageEvent_Enter_Agent : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.ENTER_AGENT; }
        }

        /// <summary>
        /// 事件KEY值，此事件该值为空
        /// </summary>
        public string EventKey { get; set; }
    }
}
