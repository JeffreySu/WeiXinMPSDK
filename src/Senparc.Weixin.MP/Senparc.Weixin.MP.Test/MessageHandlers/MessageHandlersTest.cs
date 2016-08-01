using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public class CustomerMessageHandlers : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public CustomerMessageHandlers(XDocument requestDoc, PostModel postModel = null, int maxRecordCount = 0)
            : base(requestDoc, postModel, maxRecordCount)
        {
        }

        public CustomerMessageHandlers(RequestMessageBase requestMessage, PostModel postModel = null, int maxRecordCount = 0)
            : base(requestMessage, postModel, maxRecordCount)
        {
        }


        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage =
               ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(RequestMessage);
            responseMessage.Content = "文字信息";
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            var responeMessage = this.CreateResponseMessage<ResponseMessageText>();
            responeMessage.Content = "OnEvent_LocationSelectRequest";
            return responeMessage;
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
            Assert.IsFalse(messageHandlers.UsingEcryptMessage);//没有使用加密模式
            Assert.IsFalse(messageHandlers.UsingCompatibilityModelEcryptMessage);//没有加密模式，所以也没有兼容模式

            Console.Write(messageHandlers.ResponseDocument.ToString());

            Assert.AreEqual("gh_a96a4a619366", messageHandlers.ResponseMessage.FromUserName);

            var responseMessage = messageHandlers.ResponseMessage as ResponseMessageText;
            Assert.IsNotNull(responseMessage);
            Assert.AreEqual("文字信息", responseMessage.Content);
        }

        [TestMethod]
        public void Event_LocationSelectTest()
        {
            var requestXML = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
<ToUserName>ToUserName</ToUserName>
<FromUserName>FromUserName</FromUserName>
<CreateTime>1444293582</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[location_select]]></Event>
<EventKey><![CDATA[ZBZXC]]></EventKey>
<SendLocationInfo><Location_X><![CDATA[31]]></Location_X>
<Location_Y><![CDATA[121]]></Location_Y>
<Scale><![CDATA[15]]></Scale>
<Label><![CDATA[嘉兴市南湖区政府东栅街道办事处(中环南路南)]]></Label>
<Poiname><![CDATA[南湖区富润路/中环南路(路口)旁]]></Poiname>
</SendLocationInfo>
</xml>
";
            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(requestXML));
            Assert.IsNotNull(messageHandlers.RequestDocument);
            Assert.IsInstanceOfType(messageHandlers.RequestMessage,typeof(RequestMessageEvent_Location_Select));
            Assert.AreEqual("ZBZXC",((RequestMessageEvent_Location_Select)messageHandlers.RequestMessage).EventKey);

            messageHandlers.Execute();
            Assert.IsNotNull(messageHandlers.ResponseMessage);
            Assert.IsNotNull(messageHandlers.ResponseDocument);
            Assert.IsFalse(messageHandlers.UsingEcryptMessage);//没有使用加密模式
            Assert.IsFalse(messageHandlers.UsingCompatibilityModelEcryptMessage);//没有加密模式，所以也没有兼容模式

            Console.WriteLine(messageHandlers.ResponseDocument.ToString());
            Assert.AreEqual("ToUserName", messageHandlers.ResponseMessage.FromUserName);
            Assert.IsInstanceOfType(messageHandlers.ResponseMessage,typeof(ResponseMessageText));
            Assert.AreEqual("OnEvent_LocationSelectRequest",((ResponseMessageText)messageHandlers.ResponseMessage).Content);
        }

        [TestMethod]
        public void EcryptMessageRequestTest()
        {
            //兼容模式测试
            var ecryptXml = @"<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>1414387151</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[你好]]></Content>
    <MsgId>6074746557628292943</MsgId>
    <Encrypt><![CDATA[2gUBUpAeuPFKBS+gkcvrR1cBq1VjTOQluB7+FQF00VnybRpYR3xko4S4wh0qD+64cWmJfF93ZNLm+HLZBexjHLAdJBs5RBG2rP1AJnU0/1vQU/Ac9Q1Nq7vfC4l3ciF8YwhQW0o/GE4MYWWakgdwnp0hQ7aVVwqMLd67A5bsURQHJiFY/cH0fVlsKe6J3aazGhRXFCxceOq2VTJ2Eulc8aBDVSM5/lAIUA/JPq5Z2RzomM0+aoa5XIfGyAtAdlBXD0ADTemxgfYAKI5EMfKtH5za3dKV2UWbGAlJQZ0fwrwPx6Rs8MsoEtyxeQ52gO94gafA+/kIVjamKTVLSgudLLz5rAdGneKkBVhXyfyfousm1DoDRjQdAdqMWpwbeG5hanoJyJiH+humW/1q8PAAiaEfA+BOuvBk/a5xL0Q2l2k=]]></Encrypt>
</xml>";
            //signature=e3203b6433eb554dd2fcba78fa48cb948fcb4801&timestamp=1414387151&nonce=917222494&encrypt_type=aes&msg_signature=ae70d4e343d946fc0477a5c760b95be0947fddbb
            var postModel = new PostModel()
            {
                Msg_Signature = "ae70d4e343d946fc0477a5c760b95be0947fddbb",
                Timestamp = "1414387151",
                Nonce = "917222494",

                Token = "weixin",
                EncodingAESKey = "mNnY5GekpChwqhy2c4NBH90g3hND6GeI4gii2YCvKLY",
                AppId = "wx669ef95216eef885"
            };
            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(ecryptXml), postModel);
            Assert.IsNotNull(messageHandlers.RequestDocument);
            Assert.IsNotNull(messageHandlers.RequestMessage);
            Assert.IsNotNull(messageHandlers.RequestMessage.Encrypt);
            Assert.IsNotNull(messageHandlers.RequestMessage.FromUserName);
            Assert.IsNotNull(messageHandlers.EcryptRequestDocument);
            Assert.IsTrue(messageHandlers.UsingEcryptMessage);
            Assert.IsTrue(messageHandlers.UsingCompatibilityModelEcryptMessage);


            //安全模式测试
            ecryptXml = @"<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <Encrypt><![CDATA[2gUBUpAeuPFKBS+gkcvrR1cBq1VjTOQluB7+FQF00VnybRpYR3xko4S4wh0qD+64cWmJfF93ZNLm+HLZBexjHLAdJBs5RBG2rP1AJnU0/1vQU/Ac9Q1Nq7vfC4l3ciF8YwhQW0o/GE4MYWWakgdwnp0hQ7aVVwqMLd67A5bsURQHJiFY/cH0fVlsKe6J3aazGhRXFCxceOq2VTJ2Eulc8aBDVSM5/lAIUA/JPq5Z2RzomM0+aoa5XIfGyAtAdlBXD0ADTemxgfYAKI5EMfKtH5za3dKV2UWbGAlJQZ0fwrwPx6Rs8MsoEtyxeQ52gO94gafA+/kIVjamKTVLSgudLLz5rAdGneKkBVhXyfyfousm1DoDRjQdAdqMWpwbeG5hanoJyJiH+humW/1q8PAAiaEfA+BOuvBk/a5xL0Q2l2k=]]></Encrypt>
</xml>";
            messageHandlers = new CustomerMessageHandlers(XDocument.Parse(ecryptXml), postModel);
            Assert.IsNotNull(messageHandlers.RequestDocument);
            Assert.IsNotNull(messageHandlers.RequestMessage);
            Assert.IsNotNull(messageHandlers.RequestMessage.Encrypt);
            Assert.IsNotNull(messageHandlers.RequestMessage.FromUserName);
            Assert.IsNotNull(messageHandlers.EcryptRequestDocument);
            Assert.IsTrue(messageHandlers.UsingEcryptMessage);
            Assert.IsFalse(messageHandlers.UsingCompatibilityModelEcryptMessage);
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

        /// <summary>
        /// 专为测试用的构造函数测试
        /// </summary>
        [TestMethod]
        public void TestConstructorTest()
        {
            var requestMessage = new RequestMessageText()
            {
                Content = "Hi",
                CreateTime = DateTime.Now,
                FromUserName = "FromeUserName",
                ToUserName = "ToUserName",
                MsgId = 123,
            };
            var messageHandler = new CustomerMessageHandlers(requestMessage);
            messageHandler.Execute();

            //TestMessageHandlers中没有处理坐标信息的重写方法，将返回默认消息


            Assert.IsInstanceOfType(messageHandler.ResponseMessage, typeof(ResponseMessageText));
            Assert.AreEqual("文字信息", ((ResponseMessageText)messageHandler.ResponseMessage).Content);
        }
    }
}
