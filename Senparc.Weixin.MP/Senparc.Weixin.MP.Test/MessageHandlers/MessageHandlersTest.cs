using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public class CustomerMessageHandlers : MessageHandler<MessageContext>
    {
        public CustomerMessageHandlers(XDocument requestDoc)
            : base(requestDoc)
        {
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage =
               ResponseMessageBase.CreateFromRequestMessage(RequestMessage, ResponseMsgType.Text) as
               ResponseMessageText;
            responseMessage.Content = "文字信息";
            return responseMessage;
        }

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
    }

    [TestClass]
    public class MessageHandlersTest
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
        public void TextMessageRequestTest()
        {
            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xmlText));
            Assert.IsNotNull(messageHandlers.RequestDocument);
            messageHandlers.Execute();
            Assert.IsNotNull(messageHandlers.ResponseMessage);
            Assert.IsNotNull(messageHandlers.ResponseDocument);
            Console.Write(messageHandlers.ResponseDocument.ToString());

            Assert.AreEqual("gh_a96a4a619366", messageHandlers.ResponseMessage.FromUserName);

            var responseMessage = messageHandlers.ResponseMessage as ResponseMessageText;
            Assert.IsNotNull(responseMessage);
            Assert.AreEqual("文字信息", responseMessage.Content);
        }

        [TestMethod]
        public void SyncTest()
        {
            //测试缓存同步
            var messageHandlers1 = new CustomerMessageHandlers(XDocument.Parse(xmlText));
            var messageHandlers2 = new CustomerMessageHandlers(XDocument.Parse(xmlText));
            messageHandlers1.Execute();
            Assert.AreEqual(messageHandlers1.WeixinContext.GetHashCode(), messageHandlers2.WeixinContext.GetHashCode());
        }

        [TestMethod]
        public void ContextTest()
        {
            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xmlText));
            messageHandlers.Execute();
            var messageContext = messageHandlers.WeixinContext.GetMessageContext(messageHandlers.RequestMessage);
            Assert.IsTrue(messageContext.RequestMessages.Count > 0);
            Assert.IsNotNull(messageHandlers.CurrentMessageContext);
            Assert.AreEqual("olPjZjsXuQPJoV0HlruZkNzKc91E",messageHandlers.CurrentMessageContext.UserName);

            messageHandlers.WeixinContext.ExpireMinutes = 0;//马上过期
            messageHandlers.Execute();
            messageContext = messageHandlers.WeixinContext.GetMessageContext(messageHandlers.RequestMessage);
            Assert.AreEqual(0, messageContext.RequestMessages.Count);
        }

        private class TestContext
        {
            public static int FinishCount = 0;
            public static string RequestXmlFormat = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[{0}]]></FromUserName>
    <CreateTime>1357986928</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[TNT2]]></Content>
    <MsgId>5832509444155992350</MsgId>
</xml>
";
            public void Run()
            {
                for (int i = 0; i < 10; i++)
                {
                    //模拟10个不同用户名
                    var userName = Thread.CurrentThread.Name + "_" + i;
                    var xml = string.Format(RequestXmlFormat, userName);

                    for (int j = 0; j < 2; j++)
                    {
                        //每个用户请求2次
                        var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xml));
                        messageHandlers.Execute();
                    }
                    Thread.Sleep(5);
                }
                TestContext.FinishCount++;
            }
        }

        [TestMethod]
        public void MutipleThreadsTest()
        {
            var weixinContext = MessageHandler<MessageContext>.GlobalWeixinContext;//全局共享的WeixinContext上下文对象
            weixinContext.Restore();

            //多线程并发写入测试
            List<Thread> threadList = new List<Thread>();
            for (int i = 0; i < 200; i++)
            {
                var testContext = new TestContext();
                var thread = new Thread(testContext.Run);
                thread.Name = i.ToString();
                threadList.Add(thread);
            }

            threadList.ForEach(z => z.Start()); //开始所有线程

            while (TestContext.FinishCount < 200)
            {
            }

            Assert.AreEqual(200 * 10, weixinContext.MessageCollection.Count); //用户数量

            //判断消息上下是否自动移到底部
            {
                var userName = "3_4";

                var xml = string.Format(TestContext.RequestXmlFormat, userName);
                var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xml));
                messageHandlers.Execute();
                var lastQueueMessage = weixinContext.MessageQueue.Last();
                Assert.AreEqual(userName, lastQueueMessage.UserName);
            }

            //判断超时信息是否被及时删除
            {
                weixinContext.ExpireMinutes = 0.001; //设置过期时间（0.06秒）
                Thread.Sleep(100);
                weixinContext.GetLastRequestMessage("new"); //触发过期判断
                Assert.AreEqual(1, weixinContext.MessageCollection.Count); //只删除剩下当前这一个
            }
        }
    }
}
