using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Context
{
    /// <summary>
    /// 对话上下文被删除时触发事件的事件数据
    /// </summary>
    public class WeixinContextRemovedEventArgs : Weixin.Context.WeixinContextRemovedEventArgs /*EventArgs*/
    {
        public WeixinContextRemovedEventArgs(IMessageContext messageContext)
            : base(messageContext)
        {
        }
    }
}
