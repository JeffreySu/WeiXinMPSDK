#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test
{
    [TestClass]
    public class RequestMessageFactoryTest
    {
        [TestMethod]
        public void GetEncryptPostDataTest()
        {
            var xml = @"<xml>
<ToUserName><![CDATA[wx7618c0a6d9358622]]></ToUserName>
<Encrypt><![CDATA[P/xe9JLq4RJd3AFnjwRsttpyBrTVAGZ49JspjVI65EL7ra73u5TNn3EXHngx1TQ1gnfuFpbRovJdVZ8WrqZ4y0PI9ncA9GR95TboQWK/RGbDpW/Rkq0il1lpw+c/NTk1abwH1C/2siKATSJTbGQ4mWSyhOME7vINBHeW7EjmGEZSaPxC60z1qcLYgMYAiEL/xrU484V6X6BG/jV2uF76+C7HWGMLVmu4DOHVW+UfqQo9SnpAqZx0KRcvT/8XxGUsGwgNWhYyuzUHxu1VuZK16IiHS494tjWrXs08dKQzcpwyID7dthqQDTdIVe0tiOwPAlXvv7jQ5iMtYoQlT32HOjNTn5o/hz9wFZNnC6TFi2Y0ocEWxEMNwDHsyK85ytryTZzL+OmZ7heB72ABNhx9uGhrLoA5M68/ZXwlmfJVx8M=]]></Encrypt>
<AgentID><![CDATA[2]]></AgentID>
</xml>";
            var encryptPostData = RequestMessageFactory.GetEncryptPostData(xml);
            Assert.IsNotNull(encryptPostData);
            Assert.AreEqual("wx7618c0a6d9358622", encryptPostData.ToUserName);
            Assert.AreEqual("P/xe9JLq4RJd3AFnjwRsttpyBrTVAGZ49JspjVI65EL7ra73u5TNn3EXHngx1TQ1gnfuFpbRovJdVZ8WrqZ4y0PI9ncA9GR95TboQWK/RGbDpW/Rkq0il1lpw+c/NTk1abwH1C/2siKATSJTbGQ4mWSyhOME7vINBHeW7EjmGEZSaPxC60z1qcLYgMYAiEL/xrU484V6X6BG/jV2uF76+C7HWGMLVmu4DOHVW+UfqQo9SnpAqZx0KRcvT/8XxGUsGwgNWhYyuzUHxu1VuZK16IiHS494tjWrXs08dKQzcpwyID7dthqQDTdIVe0tiOwPAlXvv7jQ5iMtYoQlT32HOjNTn5o/hz9wFZNnC6TFi2Y0ocEWxEMNwDHsyK85ytryTZzL+OmZ7heB72ABNhx9uGhrLoA5M68/ZXwlmfJVx8M=", encryptPostData.Encrypt);
            Assert.AreEqual(2, encryptPostData.AgentID);
        }

        private string xml_Text = @"<xml>
   <ToUserName><![CDATA[toUser]]></ToUserName>
   <FromUserName><![CDATA[fromUser]]></FromUserName> 
   <CreateTime>1348831860</CreateTime>
   <MsgType><![CDATA[text]]></MsgType>
   <Content><![CDATA[this is a test]]></Content>
   <MsgId>1234567890123456</MsgId>
   <AgentID>1</AgentID>
</xml>";

        private string xml_Image = @"<xml>
   <ToUserName><![CDATA[toUser]]></ToUserName>
   <FromUserName><![CDATA[fromUser]]></FromUserName>
   <CreateTime>1348831860</CreateTime>
   <MsgType><![CDATA[image]]></MsgType>
   <PicUrl><![CDATA[this is a url]]></PicUrl>
   <MediaId><![CDATA[media_id]]></MediaId>
   <MsgId>1234567890123456</MsgId>
   <AgentID>1</AgentID>
</xml>";

        private string xml_Voice = @"<xml>
   <ToUserName><![CDATA[toUser]]></ToUserName>
   <FromUserName><![CDATA[fromUser]]></FromUserName>
   <CreateTime>1357290913</CreateTime>
   <MsgType><![CDATA[voice]]></MsgType>
   <MediaId><![CDATA[media_id]]></MediaId>
   <Format><![CDATA[Format]]></Format>
   <MsgId>1234567890123456</MsgId>
   <AgentID>1</AgentID>
</xml>";

        private string xml_Video = @"<xml>
   <ToUserName><![CDATA[toUser]]></ToUserName>
   <FromUserName><![CDATA[fromUser]]></FromUserName>
   <CreateTime>1357290913</CreateTime>
   <MsgType><![CDATA[video]]></MsgType>
   <MediaId><![CDATA[media_id]]></MediaId>
   <ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
   <MsgId>1234567890123456</MsgId>
   <AgentID>1</AgentID>
</xml>";

        private string xml_Location = @"<xml>
   <ToUserName><![CDATA[toUser]]></ToUserName>
   <FromUserName><![CDATA[fromUser]]></FromUserName>
   <CreateTime>1351776360</CreateTime>
   <MsgType><![CDATA[location]]></MsgType>
   <Location_X>23.134521</Location_X>
   <Location_Y>113.358803</Location_Y>
   <Scale>20</Scale>
   <Label><![CDATA[位置信息]]></Label>
   <MsgId>1234567890123456</MsgId>
   <AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Location = @"<xml>
   <ToUserName><![CDATA[toUser]]></ToUserName>
   <FromUserName><![CDATA[FromUser]]></FromUserName>
   <CreateTime>123456789</CreateTime>
   <MsgType><![CDATA[event]]></MsgType>
   <Event><![CDATA[LOCATION]]></Event>
   <Latitude>23.104105</Latitude>
   <Longitude>113.320107</Longitude>
   <Precision>65.000000</Precision>
   <AgentID>1</AgentID>
</xml>";

        private string xmlEvent_ShortVideo = @"<xml>
   <ToUserName><![CDATA[toUser]]></ToUserName>
   <FromUserName><![CDATA[fromUser]]></FromUserName>
   <CreateTime>1357290913</CreateTime>
   <MsgType><![CDATA[shortvideo]]></MsgType>
   <MediaId><![CDATA[media_id]]></MediaId>
   <ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
   <MsgId>1234567890123456</MsgId>
   <AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Click = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[CLICK]]></Event>
<EventKey><![CDATA[EVENTKEY]]></EventKey>
<AgentID>1</AgentID>
</xml>";

        private string xmlEvent_View = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[VIEW]]></Event>
<EventKey><![CDATA[www.qq.com]]></EventKey>
<AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Scancode_Push = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>1408090502</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_push]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[1]]></ScanResult>
</ScanCodeInfo>
<AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Scancode_Waitmsg = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>1408090606</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_waitmsg]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[2]]></ScanResult>
</ScanCodeInfo>
<AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Pic_Sysphoto = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>1408090651</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_sysphoto]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[1b5f7c23b5bf75682a53e7b6d163e185]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
<AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Pic_Photo_Or_Album = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_photo_or_album]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
<AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Pic_Weixin = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_weixin]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
<AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Location_Select = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
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
<AgentID>1</AgentID>
</xml>";

        private string xmlEvent_Enter_Agent = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>1408091189</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[enter_agent]]></Event>
<EventKey><![CDATA[]]></EventKey>
<AgentID>1</AgentID>
</xml>";

        private string xml_Suite_Ticket = @"<xml>
		<SuiteId><![CDATA[wxfc918a2d200c9a4c]]></SuiteId>
		<InfoType> <![CDATA[suite_ticket]]></InfoType>
		<TimeStamp>1403610513</TimeStamp>
		<SuiteTicket><![CDATA[asdfasfdasdfasdf]]></SuiteTicket>
	</xml>";

        private string xml_Change_Auth = @"<xml>
		<SuiteId><![CDATA[wxfc918a2d200c9a4c]]></SuiteId>
		<InfoType><![CDATA[change_auth]]></InfoType>
		<TimeStamp>1403610513</TimeStamp> 
		<AuthCorpId><![CDATA[wxf8b4f85f3a794e77]]></AuthCorpId>
	</xml>	";

        private string xml_Cancel_Auth = @"<xml>
		<SuiteId><![CDATA[wxfc918a2d200c9a4c]]></SuiteId>
		<InfoType><![CDATA[cancel_auth]]></InfoType>
		<TimeStamp>1403610513</TimeStamp>
		<AuthCorpId><![CDATA[wxf8b4f85f3a794e77]]></AuthCorpId>
	</xml>	";

        private string xml_Batch_Job_Result = @"<xml><ToUserName><![CDATA[wx28dbb14e37208abe]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>1425284517</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[batch_job_result]]></Event>
<BatchJob><JobId><![CDATA[S0MrnndvRG5fadSlLwiBqiDDbM143UqTmKP3152FZk4]]></JobId>
<JobType><![CDATA[sync_user]]></JobType>
<ErrCode>0</ErrCode>
<ErrMsg><![CDATA[ok]]></ErrMsg>
</BatchJob>
</xml>";

        [TestMethod]
        public void GetRequestEntityTest()
        {
            {
                //Text
                var doc = XDocument.Parse(xml_Text);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageText;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("this is a test", result.Content);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Image
                var doc = XDocument.Parse(xml_Image);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageImage;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("this is a url", result.PicUrl);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Voice
                var doc = XDocument.Parse(xml_Voice);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageVoice;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("media_id", result.MediaId);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Video
                var doc = XDocument.Parse(xml_Video);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageVideo;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("media_id", result.MediaId);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Location
                var doc = XDocument.Parse(xml_Location);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageLocation;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(113.358803, result.Location_Y);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //ShortVideo
                var doc = XDocument.Parse(xmlEvent_ShortVideo);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageShortVideo;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("media_id", result.MediaId);
                Assert.AreEqual(1, result.AgentID);
            }
            
            {
                //Event_Location
                var doc = XDocument.Parse(xmlEvent_Location);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Location;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(113.320107, result.Longitude);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_Click
                var doc = XDocument.Parse(xmlEvent_Click);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Click;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("EVENTKEY", result.EventKey);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_View
                var doc = XDocument.Parse(xmlEvent_View);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_View;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("www.qq.com", result.EventKey);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_Scancode_Push
                var doc = XDocument.Parse(xmlEvent_Scancode_Push);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Scancode_Push;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("qrcode", result.ScanCodeInfo.ScanType);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_Scancode_Waitmsg
                var doc = XDocument.Parse(xmlEvent_Scancode_Waitmsg);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Scancode_Waitmsg;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("qrcode", result.ScanCodeInfo.ScanType);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_Pic_Sysphoto
                var doc = XDocument.Parse(xmlEvent_Pic_Sysphoto);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Sysphoto;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("1", result.SendPicsInfo.Count);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_Pic_Photo_Or_Album
                var doc = XDocument.Parse(xmlEvent_Pic_Photo_Or_Album);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Photo_Or_Album;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("5a75aaca956d97be686719218f275c6b", result.SendPicsInfo.PicList[0].item.PicMd5Sum);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_Pic_Weixin
                var doc = XDocument.Parse(xmlEvent_Pic_Weixin);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Weixin;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("5a75aaca956d97be686719218f275c6b", result.SendPicsInfo.PicList[0].item.PicMd5Sum);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_Location_Select
                var doc = XDocument.Parse(xmlEvent_Location_Select);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Location_Select;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(" 广州市海珠区客村艺苑路 106号", result.SendLocationInfo.Label);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Event_Enter_Agent
                var doc = XDocument.Parse(xmlEvent_Enter_Agent);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Enter_Agent;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(1, result.AgentID);
            }

            {
                //Suite_Ticket
                var doc = XDocument.Parse(xml_Suite_Ticket);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageInfo_Suite_Ticket;
                Assert.IsNotNull(result);
                Assert.AreEqual("wxfc918a2d200c9a4c", result.SuiteId);
                Assert.AreEqual("asdfasfdasdfasdf", result.SuiteTicket);
            }

            {
                //Change_Auth
                var doc = XDocument.Parse(xml_Change_Auth);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageInfo_Change_Auth;
                Assert.IsNotNull(result);
                Assert.AreEqual("wxfc918a2d200c9a4c", result.SuiteId);
                Assert.AreEqual("wxf8b4f85f3a794e77", result.AuthCorpId);
            }

            {
                //Cancel_Auth
                var doc = XDocument.Parse(xml_Cancel_Auth);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageInfo_Cancel_Auth;
                Assert.IsNotNull(result);
                Assert.AreEqual("wxfc918a2d200c9a4c", result.SuiteId);
                Assert.AreEqual("wxf8b4f85f3a794e77", result.AuthCorpId);
            }

            {
                //Batch_Job_Result
                var doc = XDocument.Parse(xml_Batch_Job_Result);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Batch_Job_Result;
                Assert.IsNotNull(result);
                Assert.AreEqual("wx28dbb14e37208abe", result.ToUserName);
                Assert.AreEqual("ok", result.BatchJob.ErrMsg);
                Assert.AreEqual(0, result.BatchJob.ErrCode);
            }
        }
    }
}
