#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using System.IO;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.Controllers;
using Senparc.Weixin.MP.Sample.Tests.Mock;
using Senparc.CO2NET.Helpers;

namespace Senparc.Weixin.MP.Sample.Tests.Controllers
{
    //已通过测试
    [TestClass]
    public class WeixinControllerTest : BaseTest
    {
        protected WeixinController target;
        protected Stream inputStream;

        string xmlTextFormat = @"<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>{{0}}</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[{0}]]></Content>
    <MsgId>5832509444155992350</MsgId>
</xml>
";

        string xmlLocationFormat = @"<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>{0}</CreateTime>
  <MsgType><![CDATA[location]]></MsgType>
  <Location_X>31.285774</Location_X>
  <Location_Y>120.597610</Location_Y>
  <Scale>19</Scale>
  <Label><![CDATA[中国江苏省苏州市沧浪区桐泾南路251号-309号]]></Label>
  <MsgId>5832828233808572154</MsgId>
</xml>";


        protected string xmlEvent_ClickFormat = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E_{{2}}]]></FromUserName>
  <CreateTime>{{0}}</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[CLICK]]></Event>
  <EventKey><![CDATA[{0}]]></EventKey>
  <MsgId>{{1}}</MsgId>
</xml>
";

        private string xmlVideoFormat = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>{0}</CreateTime>
  <MsgType><![CDATA[video]]></MsgType>
  <Video>
    <MediaId><![CDATA[mediaId]]></MediaId>
    <ThumbMediaId><![CDATA[thumbMediaId]]></ThumbMediaId>
  </Video> 
</xml>";

        private string xmlImageFormat = @"<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1425200601</CreateTime>
  <MsgType><![CDATA[image]]></MsgType>
  <PicUrl><![CDATA[http://mmbiz.qpic.cn/mmbiz/ZxBXNzgHyUq9W2782SegwYFwpf9mK9a6GGToC31ZjpJRH4pD4xnMXStxmx9vQbvZPwmJ1kcffz3KyNtGPVDJhw/0]]></PicUrl>
  <MsgId>6121189971737837469</MsgId>
  <MediaId><![CDATA[rS0qWb1wSLNpLjv3gD1QQnZV8WcL29CTqlf9uyC0jj1Nha2Sv4jJ_0LsT88qVe2a]]></MediaId>
</xml>
";

        /// <summary>
        /// 初始化控制器及相关请求参数
        /// </summary>
        /// <param name="xmlFormat"></param>
        protected void Init(string xmlFormat)
        {
            //target = StructureMap.ObjectFactory.GetInstance<WeixinController>();//使用IoC的在这里必须注入，不要直接实例化
            target = new WeixinController();

            inputStream = new MemoryStream();

            var xml = string.Format(xmlFormat, DateTimeHelper.GetWeixinDateTime(DateTime.Now));
            var bytes = System.Text.Encoding.UTF8.GetBytes(xml);

            inputStream.Write(bytes, 0, bytes.Length);
            inputStream.Flush();
            inputStream.Seek(0, SeekOrigin.Begin);

            target.SetFakeControllerContext(inputStream);
        }

        /// <summary>
        /// 测试不同类型的请求
        /// </summary>
        /// <param name="xml">微信发过来的xml原文</param>
        protected void PostTest(string xml)
        {
            Init(xml);//初始化

            var timestamp = "itsafaketimestamp";
            var nonce = "whateveryouwant";
            var signature = Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, WeixinController.Token);

            DateTime st = DateTime.Now;
            //这里使用MiniPost，绕过日志记录

            var postModel = new PostModel()
            {
                Signature = signature,
                Timestamp = timestamp,
                Nonce = nonce,
            };
            var actual = target.MiniPost(postModel) as FixWeixinBugWeixinResult;
            DateTime et = DateTime.Now;

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Content);

            Console.WriteLine(actual.Content);
            Console.WriteLine("页面用时（ms）：" + (et - st).TotalMilliseconds);
        }

        [TestMethod]
        public void TextPostTest()
        {
            PostTest(string.Format(xmlTextFormat, "TNT2"));
        }

        [TestMethod]
        public void LocationPostTest()
        {
            PostTest(xmlLocationFormat);
        }


        [TestMethod]
        public void VideoPostTest()
        {
            PostTest(xmlVideoFormat);
        }

        [TestMethod]
        public void ImagePostTest()
        {
            PostTest(xmlImageFormat);
        }


        [TestMethod]
        public void MessageAgent_TextTest()
        {
            //文字测试
            var xml = string.Format(string.Format(xmlTextFormat, "托管"), DateTimeHelper.GetWeixinDateTime(DateTime.Now));
            Init(xml);//初始化

            var timestamp = "itsafaketimestamp";
            var nonce = "whateveryouwant";
            var signature = Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, WeixinController.Token);
            var postModel = new PostModel()
            {
                Signature = signature,
                Timestamp = timestamp,
                Nonce = nonce
            };
            var actual = target.MiniPost(postModel) as FixWeixinBugWeixinResult; Assert.IsNotNull(actual);
            Console.WriteLine(actual.Content);
        }

        [TestMethod]
        public void MessageAgent_NewsTest()
        {
            //按钮测试-图文
            var xml = string.Format(string.Format(xmlEvent_ClickFormat, "SubClickRoot_Agent"), DateTimeHelper.GetWeixinDateTime(DateTime.Now));
            Init(xml);//初始化

            var timestamp = "itsafaketimestamp";
            var nonce = "whateveryouwant";
            var signature = Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, WeixinController.Token);
            var postModel = new PostModel()
            {
                Signature = signature,
                Timestamp = timestamp,
                Nonce = nonce
            };
            var actual = target.MiniPost(postModel) as FixWeixinBugWeixinResult; Assert.IsNotNull(actual);
            Console.WriteLine(actual.Content);
        }

        [TestMethod]
        public void MessageAgent_MemberTest()
        {
            //按钮测试-会员
            var xml = string.Format(string.Format(xmlEvent_ClickFormat, "Member"), DateTimeHelper.GetWeixinDateTime(DateTime.Now));
            Init(xml);//初始化

            var timestamp = "itsafaketimestamp";
            var nonce = "whateveryouwant";
            var signature = Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, WeixinController.Token);
            var postModel = new PostModel()
            {
                Signature = signature,
                Timestamp = timestamp,
                Nonce = nonce
            };
            var actual = target.MiniPost(postModel) as FixWeixinBugWeixinResult; Assert.IsNotNull(actual);
            Console.WriteLine(actual.Content);
        }

        [TestMethod]
        public void MessageContextRecordLimtTest()
        {
            //测试MessageContext的数量限制
            var xml = string.Format(string.Format(xmlTextFormat, "测试限制"), DateTimeHelper.GetWeixinDateTime(DateTime.Now));
            for (int i = 0; i < 100; i++)
            {
                Init(xml);//初始化

                var timestamp = "itsafaketimestamp";
                var nonce = "whateveryouwant";
                var signature = CheckSignature.GetSignature(timestamp, nonce, WeixinController.Token);
                var postModel = new PostModel()
                {
                    Signature = signature,
                    Timestamp = timestamp,
                    Nonce = nonce
                };
                var actual = target.MiniPost(postModel) as FixWeixinBugWeixinResult; Assert.IsNotNull(actual);
            }
            Assert.AreEqual(1, MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>.GlobalWeixinContext.MessageQueue.Count);

            var weixinContext = MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>.GlobalWeixinContext.MessageQueue[0];
            var recordCount = MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>.GlobalWeixinContext.MaxRecordCount;
            Assert.AreEqual(recordCount, weixinContext.RequestMessages.Count);
            Assert.AreEqual(recordCount, weixinContext.ResponseMessages.Count);
        }

        [TestMethod]
        public void TextMuteTest()
        {
            //测试无返回消息
            var xml = string.Format(string.Format(xmlEvent_ClickFormat, "mute"), DateTimeHelper.GetWeixinDateTime(DateTime.Now));
            Init(xml);//初始化

            PostTest(string.Format(xmlTextFormat, "mute"));

        }
    }
}
