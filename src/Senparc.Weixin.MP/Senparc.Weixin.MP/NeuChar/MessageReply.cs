using Senparc.NeuChar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.NeuChar
{

    public class MessageReply : NeuralNodeConfig
    {
        public List<MessagePair> MessagePair { get; set; }
        public MessageReply()
        {
            MessagePair = new List<MessagePair>();
        }
    }

    public class MessagePair
    {
        public Request Request { get; set; }
        public Response Response { get; set; }

        public MessagePair()
        {
            Request = new Request();
            Response = new Response();
        }
    }

    public class Request
    {
        /// <summary>
        /// 说明：目前只支持Text和Image
        /// </summary>
        public RequestMsgType Type { get; set; }
        /// <summary>
        /// 文本、事件的关键字
        /// </summary>
        public List<string> Keywords { get; set; }

        public Request()
        {
            Type = RequestMsgType.Unknown;
            Keywords = new List<string>();
        }
    }

    public class Response
    {
        public ResponseMsgType Type { get; set; }
        public string Content { get; set; }

        public Response()
        {
            Type = ResponseMsgType.Text;
        }
    }
}
