using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test
{
    [TestClass]
    public class RequestMessageFactoryTest
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

        private string xmlImage = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1358061658</CreateTime>
  <MsgType><![CDATA[image]]></MsgType>
  <PicUrl><![CDATA[http://mmsns.qpic.cn/mmsns/ZxBXNzgHyUqazGkXUvujSPxexzynJAicf440qkyLibBd1OEO4saJiavLQ/0]]></PicUrl>
  <MsgId>5832830407062023950</MsgId>
</xml>";

        [TestMethod]
        public void GetRequestEntityTest()
        {
            {
                //Text
                var doc = XDocument.Parse(xmlText);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsInstanceOfType(result, typeof (RequestMessageText));
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("TNT2", (result as RequestMessageText).Content);
            }

            {
                //Location
                var doc = XDocument.Parse(xmlLocation);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageLocation;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(19, result.Scale);
            }

            {
                //Image
                var doc = XDocument.Parse(xmlImage);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageImage;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("http://mmsns.qpic.cn/mmsns/ZxBXNzgHyUqazGkXUvujSPxexzynJAicf440qkyLibBd1OEO4saJiavLQ/0", result.PicUrl);
            }
        }
    }
}
