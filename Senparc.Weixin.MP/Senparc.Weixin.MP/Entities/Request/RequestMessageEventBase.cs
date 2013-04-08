using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public interface IRequestMessageEventBase : IRequestMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        Event Event { get; set; }
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        string EventKey { get; set; }
    }

    public class RequestMessageEventBase : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Event; }
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        public Event Event { get; set; }
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }
}
