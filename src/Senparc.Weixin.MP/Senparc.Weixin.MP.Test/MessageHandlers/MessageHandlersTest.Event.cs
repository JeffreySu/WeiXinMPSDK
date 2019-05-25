using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public partial class MessageHandlersTest
    {

        /// <summary>
        /// 事件测试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private CustomMessageHandlers VerifyEventTest<T>(string xml, Event eventType)
            where T : RequestMessageEventBase
        {
            var messageHandlers = new CustomMessageHandlers(XDocument.Parse(xml));
            Assert.IsNotNull(messageHandlers.RequestDocument);
            messageHandlers.Execute();
            Assert.IsNotNull(messageHandlers.TextResponseMessage);

            var requestMessage = messageHandlers.RequestMessage as T;

            Console.WriteLine(messageHandlers.RequestMessage.GetType());

            Assert.IsNotNull(requestMessage);
            Assert.AreEqual(eventType, requestMessage.Event);

            return messageHandlers;
        }


        #region 微信认证事件推送



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
            var messageHandler = VerifyEventTest<RequestMessageEvent_QualificationVerifySuccess>(xml, Event.qualification_verify_success);
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

        #region 微信


        [TestMethod]
        public void ViewMiniProgramTest()
        {
            var xml = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[view_miniprogram]]></Event>
<EventKey><![CDATA[pages/index/index]]></EventKey>
<MenuId>MENUID</MenuId>
</xml>
";
            var messageHandler = VerifyEventTest<RequestMessageEvent_View_Miniprogram>(xml, Event.view_miniprogram);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_View_Miniprogram;
            Assert.IsNotNull(requestMessage);
            Assert.IsInstanceOfType(messageHandler.ResponseMessage, typeof(ResponseMessageText));
            Assert.AreEqual("小程序被访问：MENUID - pages/index/index", (messageHandler.ResponseMessage as ResponseMessageText).Content);
        }


        #endregion

        #region 卡券回调


        [TestMethod]
        public void GiftCard_Pay_DoneTest()
        {
            var xml = @"<xml>
<ToUserName><![CDATA[gh_3fcea188bf78]]></ToUserName>
<FromUserName><![CDATA[obLatjgoYejavUtHsWwrX-2GtFJE]]></FromUserName>
<CreateTime>1472631550</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[giftcard_pay_done]]></Event>
<PageId><![CDATA[OQK0R3MaFnCm74Phw5hwFJlz5sn+jy1zzM2amDidDbU=]]></PageId>
<OrderId><![CDATA[Z2y2rY74ksZX1ceuGA]]></OrderId>
</xml>";
            var messageHandler = VerifyEventTest<RequestMessageEvent_GiftCard_Pay_Done>(xml, Event.giftcard_pay_done);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_GiftCard_Pay_Done;

            Assert.IsInstanceOfType(requestMessage, typeof(RequestMessageEvent_GiftCard_Pay_Done));
            Assert.IsInstanceOfType(messageHandler.ResponseMessage, typeof(ResponseMessageText));

            Assert.AreEqual("这里是 OnEvent_GiftCard_Pay_DoneRequest", (messageHandler.ResponseMessage as ResponseMessageText).Content);
        }

        [TestMethod]
        public void GiftCard_Send_To_FriendTest()
        {
            var xml = @"<xml>
<ToUserName><![CDATA[gh_3fcea188bf78]]></ToUserName>
<FromUserName><![CDATA[obLatjgoYejavUtHsWwrX-2GtFJE]]></FromUserName>
<CreateTime>1472631550</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[giftcard_send_to_friend]]></Event>
<PageId><![CDATA[OQK0R3MaFnCm74Phw5hwFJlz5sn+jy1zzM2amDidDbU=]]></PageId>
<OrderId><![CDATA[Z2y2rY74ksZX1ceuGA]]></OrderId>
<IsChatRoom>true</IsChatRoom>
<IsReturnBack><![CDATA[true]]></IsReturnBack>
</xml>";
            var messageHandler = VerifyEventTest<RequestMessageEvent_GiftCard_Send_To_Friend>(xml, Event.giftcard_send_to_friend);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_GiftCard_Send_To_Friend;

            Assert.IsInstanceOfType(requestMessage, typeof(RequestMessageEvent_GiftCard_Send_To_Friend));
            Assert.IsInstanceOfType(messageHandler.ResponseMessage, typeof(ResponseMessageText));

            Assert.AreEqual("这里是 OnEvent_GiftCard_Send_To_FriendRequest", (messageHandler.ResponseMessage as ResponseMessageText).Content);
        }

        [TestMethod]
        public void GiftCard_User_AcceptdTest()
        {
            var xml = @"<xml>
<ToUserName><![CDATA[gh_3fcea188bf78]]></ToUserName>
<FromUserName><![CDATA[obLatjgoYejavUtHsWwrX-2GtFJE]]></FromUserName>
<CreateTime>1472631800</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[giftcard_user_accept]]></Event>
<PageId><![CDATA[OQK0R3MaFnCm74Phw5hwFJlz5sn+jy1zzM2amDidDbU=]]></PageId>
<OrderId><![CDATA[Z2y2rY74ksZX1ceuGA]]></OrderId>
<IsChatRoom>true</IsChatRoom>
</xml>";
            var messageHandler = VerifyEventTest<RequestMessageEvent_GiftCard_User_Accept>(xml, Event.giftcard_user_accept);
            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_GiftCard_User_Accept;

            Assert.IsInstanceOfType(requestMessage, typeof(RequestMessageEvent_GiftCard_User_Accept));
            Assert.IsInstanceOfType(messageHandler.ResponseMessage, typeof(ResponseMessageText));

            Assert.AreEqual("这里是 OnEvent_GiftCard_User_AcceptRequest", (messageHandler.ResponseMessage as ResponseMessageText).Content);
        }

        #endregion

    }
}
