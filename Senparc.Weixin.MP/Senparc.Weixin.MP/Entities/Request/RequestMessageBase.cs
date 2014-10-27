using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.Entities
{
    public interface IRequestMessageBase : Weixin.Entities.IRequestMessageBase
    {
        RequestMsgType MsgType { get; }
        string Encrypt { get; set; }
        long MsgId { get; set; }
    }

    /// <summary>
    /// 接收到请求的消息
    /// </summary>
    public class RequestMessageBase : Weixin.Entities.RequestMessageBase, IRequestMessageBase
    {
        public virtual RequestMsgType MsgType
        {
            get { return RequestMsgType.Text; }
        }

        public string Encrypt { get; set; }

        public RequestMessageBase()
        {

        }

        //public override RequestMsgType MsgType
        //{
        //    get { return RequestMsgType.Text; }
        //}

        public long MsgId { get; set; }
    }
}
