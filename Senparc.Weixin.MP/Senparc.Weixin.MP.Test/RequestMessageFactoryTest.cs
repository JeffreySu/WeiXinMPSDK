using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

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

        private string xmlVoice = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1361430302</CreateTime>
  <MsgType><![CDATA[voice]]></MsgType>
  <MediaId><![CDATA[X1yfgB2XI-faU6R2jmKz0X1JZmPCxIvM-9ktt4K92BB9577SCi41S-qMl60q5DJo]]></MediaId>
  <Format><![CDATA[amr]]></Format>
  <MsgId>5847298622973403529</MsgId>
</xml>";

        private string xmlEvent_Enter = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>123456789</CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[ENTER]]></Event>
</xml>";

        private string xmlEvent_Location = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>123456789</CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[LOCATION]]></Event>
    <Latitude>23.137466</Latitude>
    <Longitude>113.352425</Longitude>
    <Precision>119.385040</Precision>
</xml>";

        private string xmlEvent_Subscribe = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1364447046</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[subscribe]]></Event>
  <EventKey><![CDATA[]]></EventKey>
</xml>";

        private string xmlEvent_Unsubscribe = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1364447020</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[unsubscribe]]></Event>
  <EventKey><![CDATA[]]></EventKey>
</xml>
";

        [TestMethod]
        public void GetRequestEntityTest()
        {
            var dt = DateTimeHelper.BaseTime.AddTicks(((long)1358061152 + 8 * 60 * 60) * 10000000);

            {
                //Text
                var doc = XDocument.Parse(xmlText);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsInstanceOfType(result, typeof(RequestMessageText));
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

            {
                //Voice
                var doc = XDocument.Parse(xmlVoice);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageVoice;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("X1yfgB2XI-faU6R2jmKz0X1JZmPCxIvM-9ktt4K92BB9577SCi41S-qMl60q5DJo", result.MediaId);
            }

            {
                //Event-ENTRY
                var doc = XDocument.Parse(xmlEvent_Enter);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Enter;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.ENTER, result.Event);
            }

            {
                //Event-Location
                var doc = XDocument.Parse(xmlEvent_Location);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Location;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.LOCATION, result.Event);
                Assert.AreEqual(23.137466, result.Latitude);
                Assert.AreEqual(113.352425, result.Longitude);
                Assert.AreEqual(119.385040, result.Precision);
            }

            {
                //Event-Subscribe
                var doc = XDocument.Parse(xmlEvent_Subscribe);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Subscribe;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.subscribe, result.Event);
            }

            {
                //Event-Unsubscribe
                var doc = XDocument.Parse(xmlEvent_Unsubscribe);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Unsubscribe;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.unsubscribe, result.Event);
                Assert.AreEqual(new DateTime(2013, 3, 28), result.CreateTime.Date);
            }
        }
    }
}
