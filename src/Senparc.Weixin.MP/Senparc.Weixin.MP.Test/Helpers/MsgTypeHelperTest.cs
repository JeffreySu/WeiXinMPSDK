using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Test
{
    [TestClass]
    public class MsgTypeHelperTest
    {
        string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
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
        public void GetMsgTypeTest()
        {
            var doc = XDocument.Parse(xml);
            var result = MsgTypeHelper.GetRequestMsgType(doc);
            Assert.AreEqual(RequestMsgType.Text, result);

            var result2 = MsgTypeHelper.GetRequestMsgType("image");
            Assert.AreEqual(RequestMsgType.Image, result2);
        }
    }
}
