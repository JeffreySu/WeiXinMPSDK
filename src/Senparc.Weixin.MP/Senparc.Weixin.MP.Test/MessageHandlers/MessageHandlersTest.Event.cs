using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public partial class MessageHandlersTest
    {
        #region 微信认证事件推送

        /// <summary>
        /// 微信认证事件测试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private CustomerMessageHandlers VerifyEventTest<T>(string xml, Event eventType)
            where T : RequestMessageEventBase
        {
            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xml));
            Assert.IsNotNull(messageHandlers.RequestDocument);
            messageHandlers.Execute();
            Assert.IsNotNull(messageHandlers.TextResponseMessage);

            var requestMessage = messageHandlers.RequestMessage as T;

            Assert.IsNotNull(requestMessage);
            Assert.AreEqual(eventType, requestMessage.Event);

            return messageHandlers;
        }


        [TestMethod]
        public void QualificationVerifySuccessTest()
        {
            var xml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1442401156</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[qualification_verify_success]]></Event>
<ExpiredTime>1442401156</ExpiredTime>
</xml> ";
            var messageHandler = VerifyEventTest<RequestMessageEvent_QualificationVerifySuccess>(xml,Event.qualification_verify_success);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_QualificationVerifySuccess;
            Assert.AreEqual("2015-09-16 18:59:16", requestMessage.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual("success", messageHandler.TextResponseMessage);
        }


        [TestMethod]
        public void QualificationVerifyFailTest()
        {
            var xml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1442401156</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[qualification_verify_fail]]></Event>
<FailTime>1442401156</FailTime>
<FailReason><![CDATA[by time]]></FailReason>
</xml>";
            var messageHandler = VerifyEventTest<RequestMessageEvent_QualificationVerifyFail>(xml, Event.qualification_verify_fail);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_QualificationVerifyFail;
            Assert.AreEqual("2015-09-16 18:59:16", requestMessage.FailTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual("by time", requestMessage.FailReason);
            Assert.AreEqual("success", messageHandler.TextResponseMessage);
        }

        [TestMethod]
        public void NamingVerifySuccessTest()
        {
            var xml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1442401093</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[naming_verify_success]]></Event>
<ExpiredTime>1442401156</ExpiredTime>
</xml> ";
            var messageHandler = VerifyEventTest<RequestMessageEvent_NamingVerifySuccess>(xml, Event.naming_verify_success);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_NamingVerifySuccess;
            Assert.AreEqual("2015-09-16 18:59:16", requestMessage.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual("success", messageHandler.TextResponseMessage);
        }

        [TestMethod]
        public void NamingVerifyFailTest()
        {
            var xml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1442401061</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[naming_verify_fail]]></Event>
<FailTime>1442401156</FailTime>
<FailReason><![CDATA[by time 2]]></FailReason>
</xml>";
            var messageHandler = VerifyEventTest<RequestMessageEvent_NamingVerifyFail>(xml, Event.naming_verify_fail);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_NamingVerifyFail;
            Assert.AreEqual("2015-09-16 18:59:16", requestMessage.FailTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual("by time 2", requestMessage.FailReason);
            Assert.AreEqual("success", messageHandler.TextResponseMessage);
        }

        [TestMethod]
        public void AnnualRenewTest()
        {
            var xml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1442401004</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[annual_renew]]></Event>
<ExpiredTime>1442401156</ExpiredTime>
</xml>";
            var messageHandler = VerifyEventTest<RequestMessageEvent_AnnualRenew>(xml, Event.annual_renew);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_AnnualRenew;
            Assert.AreEqual("2015-09-16 18:59:16", requestMessage.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual("success", messageHandler.TextResponseMessage);
        }


        [TestMethod]
        public void VerifyExpiredTest()
        {
            var xml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1442400900</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[verify_expired]]></Event>
<ExpiredTime>1442401156</ExpiredTime>
</xml>";
            var messageHandler = VerifyEventTest<RequestMessageEvent_VerifyExpired>(xml, Event.verify_expired);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_VerifyExpired;
            Assert.AreEqual("2015-09-16 18:59:16", requestMessage.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual("success", messageHandler.TextResponseMessage);
        }

        #endregion

    }
}
