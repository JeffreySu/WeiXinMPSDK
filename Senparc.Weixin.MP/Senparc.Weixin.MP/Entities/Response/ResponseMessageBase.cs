using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public interface IResponseMessageBase : IMessageBase
    {
        ResponseMsgType MsgType { get; }
        //string Content { get; set; }
        bool FuncFlag { get; set; }
    }

    /// <summary>
    /// 响应回复消息
    /// </summary>
    public class ResponseMessageBase : MessageBase, IResponseMessageBase
    {
        public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Text; }
        }
        //public string Content { get; set; }
        public bool FuncFlag { get; set; }

        /// <summary>
        /// 获取响应类型实例，并初始化
        /// </summary>
        /// <param name="requestMessage">请求</param>
        /// <param name="msgType">响应类型</param>
        /// <returns></returns>
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
                                                 CreateTime = DateTime.Now,//使用当前最新时间
                                                 //MsgType = msgType
                                             };
                        break;
                    case ResponseMsgType.News:
                        responseMessage = new ResponseMessageNews()
                                               {
                                                   ToUserName = requestMessage.FromUserName,
                                                   FromUserName = requestMessage.ToUserName,
                                                   CreateTime = DateTime.Now,//使用当前最新时间
                                                   //MsgType = msgType
                                               };
                        break; break;
                    case ResponseMsgType.Music:
                        responseMessage = new ResponseMessageMusic()
                                              {
                                                  ToUserName = requestMessage.FromUserName,
                                                  FromUserName = requestMessage.ToUserName,
                                                  CreateTime = DateTime.Now,//使用当前最新事件
                                                  //MsgType = msgType
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

        /// <summary>
        /// 获取响应类型实例，并初始化
        /// </summary>
        /// <typeparam name="T">需要返回的类型</typeparam>
        /// <param name="requestMessage">请求数据</param>
        /// <returns></returns>
        public static T CreateFromRequestMessage<T>(IRequestMessageBase requestMessage) where T : ResponseMessageBase
        {
            try
            {
                var tType = typeof(T);
                var responseName = tType.Name.Replace("ResponseMessage", ""); //请求名称
                ResponseMsgType msgType = (ResponseMsgType)Enum.Parse(typeof(ResponseMsgType), responseName);
                return CreateFromRequestMessage(requestMessage, msgType) as T;
            }
            catch (Exception ex)
            {
                throw new WeixinException("ResponseMessageBase.CreateFromRequestMessage<T>过程发生异常！", ex);
            }
        }
    }
}
