using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public interface IResponseMessageBase
    {
        string ToUserName { get; set; }
        string FromUserName { get; set; }
        DateTime CreateTime { get; set; }
        ResponseMsgType MsgType { get; set; }
        string Content { get; set; }
        bool FuncFlag { get; set; }
    }

    /// <summary>
    /// 响应回复消息
    /// </summary>
    public class ResponseMessageBase : IResponseMessageBase
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public DateTime CreateTime { get; set; }
        public ResponseMsgType MsgType { get; set; }
        public string Content { get; set; }
        public bool FuncFlag { get; set; }

        public static ResponseMessageBase CreateFromRequestMessage(IRequestMessageBase requestMessage, ResponseMsgType msgType)
        {
            ResponseMessageBase responseMessage = null;
            switch (msgType)
            {
                case ResponseMsgType.Text:
                    responseMessage = new ResponseMessageText()
                                         {
                                             ToUserName = requestMessage.FromUserName,
                                             FromUserName = requestMessage.ToUserName,
                                             CreateTime = DateTime.Now,//使用当前最新事件
                                             MsgType = msgType
                                         };
                    break;
                case ResponseMsgType.News:
                    responseMessage = new ResponseMessageNews()
                                           {
                                               ToUserName = requestMessage.FromUserName,
                                               FromUserName = requestMessage.ToUserName,
                                               CreateTime = DateTime.Now,//使用当前最新事件
                                               MsgType = msgType
                                           };
                    break; break;
                default:
                    throw new ArgumentOutOfRangeException("msgType");
            }

            return responseMessage;
        }
    }
}
