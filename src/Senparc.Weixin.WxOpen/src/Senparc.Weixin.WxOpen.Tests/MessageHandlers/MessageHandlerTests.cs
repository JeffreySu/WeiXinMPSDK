using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Tests.MessageHandlers.TestEntities;
using Senparc.WeixinTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.WxOpen.Tests.MessageHandlers
{
    [TestClass]
    public class MessageHandlerTests : WxOpenBaseTest
    {
        string xmlMediaCheck = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_6e306981e699]]></ToUserName>
  <FromUserName><![CDATA[oeaTy0Ej7xM_LexOCWGeJtL4ABoE]]></FromUserName>
  <CreateTime>1659967849</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[wxa_media_check]]></Event>
  <appid><![CDATA[wx12b4f63276b14d4c]]></appid>
  <trace_id><![CDATA[62f11967-2b24d27a-34715985]]></trace_id>
  <version>2</version>
  <detail>
    <strategy><![CDATA[content_model]]></strategy>
    <errcode>0</errcode>
    <suggest><![CDATA[pass]]></suggest>
    <label>100</label>
    <prob>90</prob>
  </detail>
  <errcode>0</errcode>
  <errmsg><![CDATA[ok]]></errmsg>
  <result>
    <suggest><![CDATA[pass]]></suggest>
    <label>100</label>
  </result>
</xml>
";

        [TestMethod]
        public void MediaCheckTest()
        {
            var messageHandlers = new CustomWxOpenMessageHandler(XDocument.Parse(xmlMediaCheck));
            //启用同步方法做替补
            messageHandlers.DefaultMessageHandlerAsyncEvent = NeuChar.MessageHandlers.DefaultMessageHandlerAsyncEvent.SelfSynicMethod;
            Assert.IsNotNull(messageHandlers.RequestDocument);
            Assert.IsInstanceOfType(messageHandlers.RequestMessage, typeof(RequestMessageEvent_MediaCheck));

            messageHandlers.ExecuteAsync(new System.Threading.CancellationToken()).GetAwaiter().GetResult();

            Assert.IsNotNull(messageHandlers.ResponseMessage);
            Assert.IsNotNull(messageHandlers.ResponseDocument);
            Assert.IsFalse(messageHandlers.UsingEncryptMessage);//没有使用加密模式
            Assert.IsFalse(messageHandlers.UsingCompatibilityModelEncryptMessage);//没有加密模式，所以也没有兼容模式

            Console.Write(messageHandlers.ResponseDocument.ToString());

            Assert.AreEqual("oeaTy0Ej7xM_LexOCWGeJtL4ABoE", messageHandlers.RequestMessage.FromUserName);

            var responseMessage = messageHandlers.ResponseMessage as SuccessResponseMessage;
            Assert.IsNotNull(responseMessage);
        }
    }
}
