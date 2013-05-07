using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public class CancelMessageHandlers : MessageHandler<MessageContext>
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

        #region 不用于测试，省略

        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            throw new NotImplementedException();
        }

        #endregion
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
