using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public class CancelMessageHandlers : MessageHandler<MessageContext<IRequestMessageBase,IResponseMessageBase>>
    {
        public string RunStep { get; set; }

        public CancelMessageHandlers(XDocument requestDoc)
            : base(requestDoc)
        {
        }

        public override void OnExecuting()
        {
            RunStep = "OnExecuting";
            CancelExcute = true;//取消执行
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            RunStep = "OnExecuted";
            base.OnExecuted();
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            RunStep = "Execute";
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "文字信息";
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
    public class MessageHandlers_CancelTest
    {
        string xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>1357986928</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[TNT2]]></Content>
    <MsgId>5832509444155992350</MsgId>
</xml>
";

        [TestMethod]
        public void CancelTest()
        {
            {
                //一开始就取消
                var messageHandler = new CancelMessageHandlers(XDocument.Parse(xmlText));
                messageHandler.CancelExcute = true;
                messageHandler.Execute();

                Assert.AreEqual(null, messageHandler.RunStep);
            }

            {
                //OnExecuting中途取消
                var messageHandler = new CancelMessageHandlers(XDocument.Parse(xmlText));
                messageHandler.Execute();

                Assert.AreEqual("OnExecuting", messageHandler.RunStep);
            }
        }
    }
}
