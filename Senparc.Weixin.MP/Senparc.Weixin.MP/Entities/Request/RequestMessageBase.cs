using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.Entities
{
    public interface IRequestMessageBase : Weixin.Entities.IRequestMessageBase
    {

    }

    /// <summary>
    /// 接收到请求的消息
    /// </summary>
    public class RequestMessageBase : Weixin.Entities.RequestMessageBase, IRequestMessageBase
    {
        public RequestMessageBase()
        {

        }

        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Text; }
        }

        public long MsgId { get; set; }
    }
}
