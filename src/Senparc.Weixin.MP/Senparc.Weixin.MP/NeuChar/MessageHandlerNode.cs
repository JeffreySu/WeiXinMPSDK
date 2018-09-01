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
        /// 获取响应消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="defaultProcess"></param>
        /// <returns></returns>
        public IResponseMessageBase GetResponseMessage(IRequestMessageBase requestMessage, Func<IResponseMessageBase> defaultProcess)
        {
            IResponseMessageBase responseMessage = null;

            switch (requestMessage.MsgType)
            {
                case RequestMsgType.Text:
                    {
                        var textRequestMessage = requestMessage as RequestMessageText;
                        //遍历所有的消息设置
                        foreach (var messagePair in Config.MessagePair.Where(z => z.Request.Type == RequestMsgType.Text))//TODO：把foreach放到外面
                        {
                            //遍历每一个消息设置中的关键词
                            foreach (var keyword in messagePair.Request.Keywords)
                            {
                                if (keyword.Equals(textRequestMessage.Content, StringComparison.OrdinalIgnoreCase))//TODO:加入大小写敏感设计
                                {
                                    responseMessage = RenderResponseMessageText(requestMessage, messagePair.Response);
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
                        foreach (var messagePair in Config.MessagePair.Where(z => z.Request.Type == RequestMsgType.Image))
                        {
                            responseMessage = RenderResponseMessageImage(requestMessage, messagePair.Response, imageRequestMessage.MediaId);

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

#if !NET35 && !NET40
        /// <summary>
        /// 获取响应消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="defaultProcess">默认流程</param>
        /// <returns></returns>
        public async Task<IResponseMessageBase> GetResponseMessageAsync(IRequestMessageBase requestMessage,
            Func<IResponseMessageBase> defaultProcess)
        {
            return await Task.Run(() => GetResponseMessage(requestMessage, defaultProcess));
        }
#endif
        #region 返回信息

        private ResponseMessageText RenderResponseMessageText(IRequestMessageBase requestMessage, Response responseConfig)
        {
            var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            strongResponseMessage.Content = responseConfig.Content.Replace("{now}", DateTime.Now.ToString());
            return strongResponseMessage;
        }

        private ResponseMessageImage RenderResponseMessageImage(IRequestMessageBase requestMessage, Response responseConfig, string mediaId)
        {
            var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageImage>();
            strongResponseMessage.Image.MediaId = mediaId;
            return strongResponseMessage;
        }

        #endregion
    }

}
