using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Agent;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.Agents
{
    [TestClass]
    public class MessageAgentTest
    {
        [TestMethod]
        public void RequestXmlTest()
        {
            var url = "http://weixin.senparc.com/weixin"; //可以换成你自己的地址
            var token = "weixin"; //替换成自己的Token

            var requestXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
<CreateTime>1384322309</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[CLICK]]></Event>
<EventKey><![CDATA[OneClick]]></EventKey>
</xml>";

            var xml = MessageAgent.RequestXml(null, url, token, requestXml);
            var responseMessage = ResponseMessageBase.CreateFromResponseXml(xml);
            Assert.IsNotNull(responseMessage);
            Assert.IsInstanceOfType(responseMessage, typeof (ResponseMessageText));
            var strongResponseMessage = responseMessage as ResponseMessageText;
            Assert.IsTrue(strongResponseMessage.Content.Contains("您点击了底部按钮。"));

            Console.Write(strongResponseMessage.Content);
        }

        [TestMethod]
        public void CheckUrlAndTokenTest()
        {
            var url = "http://weixin.senparc.com/weixin";
            var token = "weixin";
            var result = MessageAgent.CheckUrlAndToken(url, token);
            Assert.IsTrue(result);

            token = "wrong_token";
            result = MessageAgent.CheckUrlAndToken(url, token);
            Assert.IsFalse(false);

            url = "wrong_url";
            token = "weixin";
            result = MessageAgent.CheckUrlAndToken(url, token);
            Assert.IsFalse(false);
        }
    }
}
