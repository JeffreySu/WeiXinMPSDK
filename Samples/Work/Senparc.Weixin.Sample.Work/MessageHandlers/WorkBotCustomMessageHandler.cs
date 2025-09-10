using System;
using System.IO;
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageContexts;
using Senparc.Weixin.Work.MessageHandlers;

namespace Senparc.Weixin.Sample.Work.MessageHandlers
{
    /// <summary>
    /// 自定义的企业微信机器人消息处理程序
    /// </summary>
    public class WorkBotCustomMessageHandler : WorkBotMessageHandler<DefaultWorkMessageContext>
    {
        /// <summary>
        /// 为中间件提供生成当前类的委托
        /// </summary>
        public static Func<Stream, PostModel, int, IServiceProvider, WorkBotCustomMessageHandler> GenerateMessageHandler =
            (stream, postModel, maxRecordCount, serviceProvider) => new WorkBotCustomMessageHandler(stream, postModel, maxRecordCount, serviceProvider);

        public WorkBotCustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, serviceProvider: serviceProvider)
        {
        }

        public override IWorkResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = $"您发送了消息：{requestMessage.Content}";
            return responseMessage;
        }

        public override IWorkResponseMessageBase DefaultResponseMessage(IWorkRequestMessageBase requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这是一条默认的 Bot 消息。";
            return responseMessage;
        }
    }
}
