using Senparc.NeuChar;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.NeuChar
{
    /// <summary>
    /// MessageHandler 的神经节点
    /// </summary>
    public class MessageHandlerNode : BaseNeuralNode
    {
        public override string Version { get; set; }


        /// <summary>
        /// 设置信息（系统约定Config为固定名称）
        /// </summary>
        new public MessageReply Config { get; set; }

        public MessageHandlerNode()
        {
            Config = new MessageReply();
        }

        /// <summary>
        /// 生成响应信息
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <param name="responseConfig">返回消息</param>
        /// <returns></returns>
        private IResponseMessageBase RenderResponseMessage(IRequestMessageBase requestMessage, Response responseConfig)
        {
            IResponseMessageBase responseMessage = null;
            switch (responseConfig.Type)
            {
                case ResponseMsgType.Text:
                    {
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                        responseMessage = strongResponseMessage;
                        strongResponseMessage.Content = responseConfig.Content.Replace("{now}", DateTime.Now.ToString());
                    }
                    break;

                case ResponseMsgType.Image:
                    {
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageImage>();
                        responseMessage = strongResponseMessage;
                        strongResponseMessage.Image.MediaId = "";
                    }
                    break;
                //case ResponseMsgType.NoResponse:
                //    break;
                //case ResponseMsgType.SuccessResponse:
                //    break;
                default:
                    throw new UnknownRequestMsgTypeException("NeuChar未支持的的MsgType响应类型", null);
            }
            return responseMessage;

        }
        /// <summary>
        /// 获取响应消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="defaultProcess">默认流程</param>
        /// <returns></returns>
        public async Task<IResponseMessageBase> GetResponseMessageAsync(IRequestMessageBase requestMessage, Func<Task<IResponseMessageBase>> defaultProcess)
        {
            IResponseMessageBase responseMessage = null;

            switch (requestMessage.MsgType)
            {
                case RequestMsgType.Text:
                    {
                        var textRequestMessage = requestMessage as RequestMessageText;
                        //遍历所有的消息设置
                        foreach (var messagePair in Config.MessagePair)
                        {
                            //遍历每一个消息设置中的关键词
                            foreach (var keyword in messagePair.Request.Keywords)
                            {
                                if (keyword.Equals(textRequestMessage.Content, StringComparison.OrdinalIgnoreCase))//TODO:加入大小写敏感设计
                                {
                                    responseMessage = RenderResponseMessage(requestMessage, messagePair.Response);
                                    break;
                                }
                            }
                            if (responseMessage != null)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case RequestMsgType.Image:
                    {
                        var imageRequestMessage = requestMessage as RequestMessageImage;
                        //遍历所有的消息设置
                        foreach (var messagePair in Config.MessagePair)
                        {
                            if (messagePair.Request.Type == RequestMsgType.Image)
                            {
                                responseMessage = RenderResponseMessage(requestMessage, messagePair.Response);
                            }

                            if (responseMessage != null)
                            {
                                break;
                            }
                        }
                    }
                    break;
                default:
                    //不作处理

                    //throw new UnknownRequestMsgTypeException("NeuChar未支持的的MsgType请求类型："+ requestMessage.MsgType, null);
                    break;

            }

            return responseMessage;
        }

    }

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
