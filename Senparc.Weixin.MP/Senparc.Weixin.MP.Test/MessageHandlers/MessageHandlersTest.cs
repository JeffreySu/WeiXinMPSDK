using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public class CustomerMessageHandlers : MessageHandler<MessageContext<IRequestMessageBase,IResponseMessageBase>>
    {
        public CustomerMessageHandlers(XDocument requestDoc, int maxRecordCount = 0)
            : base(requestDoc, maxRecordCount)
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

        #region v1.5之后，所有的OnXX方法均从抽象方法变为虚方法，并都有默认返回消息操作，不需要处理的消息类型无需重写。

        //public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

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

        string xmlLocation = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1358061152</CreateTime>
  <MsgType><![CDATA[location]]></MsgType>
  <Location_X>31.285774</Location_X>
  <Location_Y>120.597610</Location_Y>
  <Scale>19</Scale>
  <Label><![CDATA[中国江苏省苏州市沧浪区桐泾南路251号-309号]]></Label>
  <MsgId>5832828233808572154</MsgId>
</xml>";

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
            Assert.AreEqual("olPjZjsXuQPJoV0HlruZkNzKc91E", messageHandlers.CurrentMessageContext.UserName);

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
        public void RestoreTest()
        {
            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xmlText));
            messageHandlers.Execute();
            Assert.IsTrue(messageHandlers.WeixinContext.MessageCollection.Count > 0);
            messageHandlers.WeixinContext.Restore();
            Assert.AreEqual(0, messageHandlers.WeixinContext.MessageCollection.Count);
        }

        [TestMethod]
        public void MutipleThreadsTest()
        {
            //
            var weixinContext = MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>.GlobalWeixinContext;//全局共享的WeixinContext上下文对象
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

        [TestMethod]
        public void OnEvent_MassSendJobFinishRequestTest()
        {
            var xml = @"<xml>
<ToUserName><![CDATA[gh_3e8adccde292]]></ToUserName>
<FromUserName><![CDATA[oR5Gjjl_eiZoUpGozMo7dbBJ362A]]></FromUserName>
<CreateTime>1394524295</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[MASSSENDJOBFINISH]]></Event>
<MsgID>1988</MsgID>
<Status><![CDATA[sendsuccess]]></Status>
<TotalCount>100</TotalCount>
<FilterCount>80</FilterCount>
<SentCount>75</SentCount>
<ErrorCount>5</ErrorCount>
</xml>";

            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xml));
            messageHandlers.Execute();
            Assert.IsNotNull(messageHandlers.ResponseMessage);
            Console.Write(messageHandlers.ResponseDocument);
        }


        [TestMethod]
        public void DefaultResponseMessageTest()
        {
            var messageHandler = new CustomerMessageHandlers(XDocument.Parse(xmlLocation));
            messageHandler.Execute();

            //TestMessageHandlers中没有处理坐标信息的重写方法，将返回默认消息


            Assert.IsInstanceOfType(messageHandler.ResponseMessage, typeof(ResponseMessageText));
            Assert.AreEqual("您发送的消息类型暂未被识别。", ((ResponseMessageText)messageHandler.ResponseMessage).Content);
        }
    }
}
