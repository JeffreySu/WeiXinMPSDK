using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    public interface IRequestMessageBase : Senparc.Weixin.Entities.IRequestMessageBase
    {
        RequestMsgType MsgType { get; }
        int AgentID { get; set; }
    }

    /// <summary>
    /// 接收到请求的消息
    /// </summary>
    public class RequestMessageBase : Weixin.Entities.RequestMessageBase, IRequestMessageBase
    {
        public RequestMessageBase()
        {

        }

        public virtual RequestMsgType MsgType
        {
            get { return RequestMsgType.Text; }
        }

        public int AgentID { get; set; }
    }
}
