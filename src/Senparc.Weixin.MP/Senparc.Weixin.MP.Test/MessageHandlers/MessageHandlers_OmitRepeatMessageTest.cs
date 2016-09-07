using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public class OmitRepeatMessageMessageHandlers : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public string RunStep { get; set; }

        public OmitRepeatMessageMessageHandlers(XDocument requestDoc)
            : base(requestDoc)
        {
        }

        public override void OnExecuting()
        {
            base.OnExecuting();
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = requestMessage.Content;
            return responseMessage;
        }

        /// <summary>
        /// 默认消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送的消息类型暂未被识别。";
            return responseMessage;
        }
    }

    [TestClass]
    public class MessageHandlers_OmitRepeatMessage
    {
        string xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>1357986928</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[{1}]]></Content>
    <MsgId>{0}</MsgId>
</xml>
";

        [TestMethod]
        public void OmitMessageTest()
        {
            {
                //发送两条不同MsgId的消息
                var messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "1", "Jeffrey")));
                messageHandler.OmitRepeatedMessage = true;
                messageHandler.Execute();
                Assert.IsNotNull(messageHandler.ResponseMessage);
                Assert.AreEqual("Jeffrey", (messageHandler.ResponseMessage as ResponseMessageText).Content);

                messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "2", "Su")));
                messageHandler.OmitRepeatedMessage = true;
                messageHandler.Execute();
                Assert.IsNotNull(messageHandler.ResponseMessage);
                Assert.AreEqual("Su", (messageHandler.ResponseMessage as ResponseMessageText).Content);
            }

            {
                //发送两条相同MsgId的消息
                var messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "1", "Jeffrey")));
                messageHandler.OmitRepeatedMessage = true;
                messageHandler.Execute();
                Assert.IsNotNull(messageHandler.ResponseMessage);
                Assert.AreEqual("Jeffrey", (messageHandler.ResponseMessage as ResponseMessageText).Content);

                messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "1", "Su")));
                messageHandler.OmitRepeatedMessage = true;
                messageHandler.Execute();
                Assert.IsNull(messageHandler.ResponseMessage);
            }

            {
                //发送两条相同MsgId的消息，但是不启用忽略
                var messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "1", "Jeffrey")));
                messageHandler.OmitRepeatedMessage = false;
                messageHandler.Execute();
                Assert.IsNotNull(messageHandler.ResponseMessage);
                Assert.AreEqual("Jeffrey", (messageHandler.ResponseMessage as ResponseMessageText).Content);

                messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "1", "Su")));
                messageHandler.OmitRepeatedMessage = false;
                messageHandler.Execute();
                Assert.IsNotNull(messageHandler.ResponseMessage);
                Assert.AreEqual("Su", (messageHandler.ResponseMessage as ResponseMessageText).Content);

            }
        }
    }
}
