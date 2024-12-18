/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：CustomMessageHandler.cs
    文件功能描述：微信公众号自定义MessageHandler


    创建标识：Senparc - 20150312

    修改标识：Senparc - 20171027
    修改描述：v14.8.3 添加OnUnknownTypeRequest()方法Demo

    修改标识：Senparc - 20191002
    修改描述：v16.9.102 提供 MessageHandler 中间件

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageContexts;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Senparc.Weixin.Sample.MP
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<DefaultMpMessageContext>
    {
        /// <summary>
        /// 为中间件提供生成当前类的委托
        /// </summary>
        public static Func<Stream, PostModel, int, IServiceProvider, CustomMessageHandler> GenerateMessageHandler = (stream, postModel, maxRecordCount, serviceProvider)
                         => new CustomMessageHandler(stream, postModel, maxRecordCount, false /* 是否只允许处理加密消息，以提高安全性 */, serviceProvider: serviceProvider);

        /// <summary>
        /// 自定义 MessageHandler
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="inputStream"></param>
        /// <param name="postModel"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="onlyAllowEncryptMessage"></param>
        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider: serviceProvider)
        {
        }

        public override async Task<IResponseMessageBase> OnTextOrEventRequestAsync(RequestMessageText requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = $"收到您发来的文字信息：{requestMessage.Content}";
            return responseMessage;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = $"收到一条您发来的信息，类型为：{requestMessage.MsgType}";
            return responseMessage;
        }
    }
}
