using Senparc.NeuChar;
using Senparc.NeuChar.ApiHandlers;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Helpers;
using Senparc.Weixin.Work.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.Work.Test.net6.MessageHandlers
{
    public class BotCustomMessageHandler : WorkBotMessageHandler<MessageContexts.DefaultWorkMessageContext>
    {
        public BotCustomMessageHandler(Stream inputStream, IEncryptPostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, IServiceProvider serviceProvider = null, bool useJson = true) : base(inputStream, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider, useJson)
        {
        }

        public override MessageEntityEnlightener MessageEntityEnlightener => throw new NotImplementedException();

        public override ApiEnlightener ApiEnlightener => throw new NotImplementedException();

        public override XDocument ResponseDocument => throw new NotImplementedException();

        public override XDocument FinalResponseDocument => throw new NotImplementedException();

        public override IWorkResponseMessageBase DefaultResponseMessage(IWorkRequestMessageBase requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这是一条默认消息。";
            return responseMessage;
        }


        //仅是为了实现抽象类不报错，不做使用
        public override XDocument Init(XDocument requestDocument, IEncryptPostModel postModel)
        {
            throw new NotImplementedException();
        }
    }
}