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
    }

    public class RequestMessageEventBase : RequestMessageBase, IRequestMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public Event Event { get; set; }
    }
}
