using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.MP.Test
{
    [TestClass]
    public class WeixinExceptionTest
    {
        string xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>1357986928</CreateTime>
    <MsgType><![CDATA[UNKNOWN]]></MsgType>
    <Content><![CDATA[TNT2]]></Content>
    <MsgId>5832509444155992350</MsgId>
</xml>
";
        [TestMethod]
        public void ThrowTest()
        {
            WeixinException unkonwnException = null;
            try
            {
                var response = MP.RequestMessageFactory.GetRequestEntity(xmlText);
            }
            catch (WeixinException ex)
            {
                unkonwnException = ex;
            }

            Assert.IsNotNull(unkonwnException);
        }
    }
}
