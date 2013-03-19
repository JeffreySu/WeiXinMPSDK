using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public interface IResponseMessageBase : IMessageBase
    {
        ResponseMsgType MsgType { get; set; }
        //string Content { get; set; }
        bool FuncFlag { get; set; }
    }

    /// <summary>
    /// 响应回复消息
    /// </summary>
    public class ResponseMessageBase : MessageBase, IResponseMessageBase
    {
        public ResponseMsgType MsgType { get; set; }
        //public string Content { get; set; }
        public bool FuncFlag { get; set; }

        public static ResponseMessageBase CreateFromRequestMessage(IRequestMessageBase requestMessage, ResponseMsgType msgType)
        {
            ResponseMessageBase responseMessage = null;
            try
            {
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
                    case ResponseMsgType.Music:
                        responseMessage = new ResponseMessageMusic()
                                              {
                                                  ToUserName = requestMessage.FromUserName,
                                                  FromUserName = requestMessage.ToUserName,
                                                  CreateTime = DateTime.Now,//使用当前最新事件
                                                  MsgType = msgType
                                              };
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException(string.Format("ResponseMsgType没有为 {0} 提供对应处理程序。", msgType), new ArgumentOutOfRangeException());
                }
            }
            catch (Exception ex)
            {
                throw new WeixinException("CreateFromRequestMessage过程发生异常", ex);
            }

            return responseMessage;
        }
    }
}
