using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
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

        private string xmlVideo = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1361430302</CreateTime>
  <MsgType><![CDATA[video]]></MsgType>
  <MediaId><![CDATA[mediaId]]></MediaId>
  <ThumbMediaId><![CDATA[thumbMediaId]]></ThumbMediaId>
</xml>";

//        @"<?xml version=""1.0"" encoding=""utf-8""?>
//<xml>
//  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
//  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
//  <CreateTime>1361430302</CreateTime>
//  <MsgType><![CDATA[video]]></MsgType>
//  <Video>
//    <MediaId><![CDATA[mediaId]]></MediaId>
//    <ThumbMediaId><![CDATA[thumbMediaId]]></ThumbMediaId>
//  </Video> 
//</xml>";

        private string xmlLink = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
<ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
<CreateTime>1351776360</CreateTime>
<MsgType><![CDATA[link]]></MsgType>
<Title><![CDATA[公众平台官网链接]]></Title>
<Description><![CDATA[Senparc.Weixin.MP SDK公众平台官网链接]]></Description>
<Url><![CDATA[http://weixin.senparc.com]]></Url>
<MsgId>1234567890123456</MsgId>
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

        private string xmlEvent_Click = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1364447020</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[CLICK]]></Event>
  <EventKey><![CDATA[SubClickRoot_Agent]]></EventKey>
</xml>
";

        private string xmlEvent_Scan = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1364447020</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[scan]]></Event>
  <EventKey><![CDATA[SCENE_VALUE]]></EventKey>
  <Ticket><![CDATA[TICKET]]></Ticket>
</xml>
";

        private string xmlEvent_View = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1394805110</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[VIEW]]></Event>
  <EventKey><![CDATA[http://weixin.senparc.com]]></EventKey>
</xml>
";

        private string xmlEvent_Scancode_Push = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
<ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090502</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_push]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[1]]></ScanResult>
</ScanCodeInfo>
</xml>";

        private string xmlEvent_Scancode_Waitmsg = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090606</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_waitmsg]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[2]]></ScanResult>
</ScanCodeInfo>
</xml>";

        private string xmlEvent_Pic_Sysphoto = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090651</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_sysphoto]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[1b5f7c23b5bf75682a53e7b6d163e185]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";

        private string xmlEvent_Pic_Photo_Or_Album = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_photo_or_album]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
<item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";

        private string xmlEvent_Pic_Weixin = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_weixin]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";

        private string xmlEvent_Location_Select = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408091189</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[location_select]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendLocationInfo><Location_X><![CDATA[23]]></Location_X>
<Location_Y><![CDATA[113]]></Location_Y>
<Scale><![CDATA[15]]></Scale>
<Label><![CDATA[ 广州市海珠区客村艺苑路 106号]]></Label>
<Poiname><![CDATA[]]></Poiname>
</SendLocationInfo>
</xml>";

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
                //Video
                var doc = XDocument.Parse(xmlVideo);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageVideo;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("mediaId", result.MediaId);
                Assert.AreEqual("thumbMediaId", result.ThumbMediaId);
            }

            {
                //Link
                var doc = XDocument.Parse(xmlLink);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageLink;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("公众平台官网链接", result.Title);
                Assert.AreEqual("Senparc.Weixin.MP SDK公众平台官网链接", result.Description);
                Assert.AreEqual("http://weixin.senparc.com", result.Url);
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

            {
                //Event-CLICK
                var doc = XDocument.Parse(xmlEvent_Click);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Click;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.CLICK, result.Event);
                Assert.AreEqual(new DateTime(2013, 3, 28), result.CreateTime.Date);

                Assert.AreEqual("SubClickRoot_Agent", result.EventKey);
            }

            {
                //Event-scan
                var doc = XDocument.Parse(xmlEvent_Scan);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Scan;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.scan, result.Event);
                Assert.AreEqual(new DateTime(2013, 3, 28), result.CreateTime.Date);

                Assert.AreEqual("SCENE_VALUE", result.EventKey);
                Assert.AreEqual("TICKET", result.Ticket);
            }

            {
                //Event-VIEW
                var doc = XDocument.Parse(xmlEvent_View);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_View;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.VIEW, result.Event);
                Assert.AreEqual(new DateTime(2014, 3, 14), result.CreateTime.Date);
                Assert.AreEqual("http://weixin.senparc.com", result.EventKey);
            }
            
            {
                //Event-Scancode_Push
                var doc = XDocument.Parse(xmlEvent_Scancode_Push);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Scancode_Push;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.scancode_push, result.Event);
                Assert.IsNotNull(result.ScanCodeInfo.ScanResult);
            }

            {
                //Event-Scancode_Scancode_Waitmsg
                var doc = XDocument.Parse(xmlEvent_Scancode_Waitmsg);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Scancode_Waitmsg;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.scancode_waitmsg, result.Event);
                Assert.IsNotNull(result.ScanCodeInfo.ScanResult);
            }

            {
                //Event-Scancode_Pic_Sysphoto
                var doc = XDocument.Parse(xmlEvent_Pic_Sysphoto);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Sysphoto;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.pic_sysphoto, result.Event);
                Assert.IsNotNull(result.SendPicsInfo.PicList);
            }

            {
                //Event-Scancode_Pic_Photo_Or_Album
                var doc = XDocument.Parse(xmlEvent_Pic_Photo_Or_Album);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Photo_Or_Album;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.pic_photo_or_album, result.Event);
                Assert.IsNotNull(result.SendPicsInfo.PicList);
            }

            {
                //Event-Scancode_Pic_Weixin
                var doc = XDocument.Parse(xmlEvent_Pic_Weixin);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Weixin;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.pic_weixin, result.Event);
                Assert.IsNotNull(result.SendPicsInfo.PicList);
            }

            {
                //Event-Scancode_Location_Select
                var doc = XDocument.Parse(xmlEvent_Location_Select);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Location_Select;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.location_select, result.Event);
                Assert.IsNotNull(result.SendLocationInfo.Location_X);
            }
        }
    }
}
