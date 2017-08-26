using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public partial class MessageHandlersTest
    {
        #region 微信认证事件推送


        #endregion

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

            var messageHandlers = new CustomerMessageHandlers(XDocument.Parse(xml));
            Assert.IsNotNull(messageHandlers.RequestDocument);
            messageHandlers.Execute();
            Assert.IsNotNull(messageHandlers.ResponseMessage);

            var requestMessage = messageHandlers.RequestMessage as RequestMessageEvent_QualificationVerifySuccess;
            Assert.IsNotNull(requestMessage);
            Assert.AreEqual("2015-09-16 18:59:16", requestMessage.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
