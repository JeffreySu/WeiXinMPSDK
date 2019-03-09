using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public partial class MessageHandlersTest
    {

        [TestMethod]
        public void RenderResponseMessageNewsTest()
        {
            var xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
 <xml>
  <ToUserName><![CDATA[gh_0fe614101343]]></ToUserName>
  <FromUserName><![CDATA[oxRg0uLsnpHjb8o93uVnwMK_WAVw]]></FromUserName>
  <CreateTime>1539684529</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[CLICK]]></Event>
  <EventKey><![CDATA[NEUCHAR|43E8BCD9]]></EventKey>
</xml>";

            var messageHandler = VerifyEventTest<RequestMessageEvent_Click>(xmlText, Event.CLICK);
            messageHandler.Execute();

            Assert.IsNotNull(messageHandler.TextResponseMessage);
            Console.WriteLine(messageHandler.TextResponseMessage);
        }


        [TestMethod]
        public void SendMenuTest()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1552115230</CreateTime>
  <MsgType><![CDATA[text]]></MsgType>
  <Content><![CDATA[满意]]></Content>
  <MsgId>22220946756594166</MsgId>
  <bizmsgmenuid>101</bizmsgmenuid>
</xml>";
            var messageHandler = new CustomMessageHandlers(XDocument.Parse(xml));
            messageHandler.Execute();
            Assert.IsInstanceOfType(messageHandler.ResponseMessage, typeof(ResponseMessageText));
            Assert.AreEqual("选择菜单：101，文字：满意", ((ResponseMessageText)messageHandler.ResponseMessage).Content);
        }
    }
}
