﻿using System;
using System.IO;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.Controllers;
using Senparc.Weixin.MP.Sample.Tests.Mock;

namespace Senparc.Weixin.MP.Sample.Tests.Controllers
{
    //已通过测试
    [TestClass]
    public class WeixinControllerTest : BaseTest
    {
        WeixinController target;
        Stream inputStream;

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


        private string xmlEvent_ClickFormat = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>{{0}}</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[CLICK]]></Event>
  <EventKey><![CDATA[{0}]]></EventKey>
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


        /// <summary>
        /// 初始化控制器及相关请求参数
        /// </summary>
        /// <param name="xmlFormat"></param>
        private void Init(string xmlFormat)
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
        private void PostTest(string xml)
        {
            Init(xml);//初始化

            var timestamp = "itsafaketimestamp";
            var nonce = "whateveryouwant";
            var signature = Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, WeixinController.Token);

            DateTime st = DateTime.Now;
            //这里使用MiniPost，绕过日志记录
            var actual = target.MiniPost(signature, timestamp, nonce, "echostr") as WeixinResult;
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
        public void MessageAgent_TextTest()
        {
            //文字测试
            var xml = string.Format(string.Format(xmlTextFormat, "托管"), DateTimeHelper.GetWeixinDateTime(DateTime.Now));
            Init(xml);//初始化

            var timestamp = "itsafaketimestamp";
            var nonce = "whateveryouwant";
            var signature = Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, WeixinController.Token);
            var actual = target.MiniPost(signature, timestamp, nonce, "echostr") as WeixinResult;
            Assert.IsNotNull(actual);
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
            var actual = target.MiniPost(signature, timestamp, nonce, "echostr") as WeixinResult;
            Assert.IsNotNull(actual);
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
            var actual = target.MiniPost(signature, timestamp, nonce, "echostr") as WeixinResult;
            Assert.IsNotNull(actual);
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
                var actual = target.MiniPost(signature, timestamp, nonce, "echostr") as WeixinResult;
                Assert.IsNotNull(actual);
            }
            Assert.AreEqual(1, MessageHandler<MessageContext>.GlobalWeixinContext.MessageQueue.Count);

            var weixinContext = MessageHandler<MessageContext>.GlobalWeixinContext.MessageQueue[0];
            var recordCount = MessageHandler<MessageContext>.GlobalWeixinContext.MaxRecordCount;
            Assert.AreEqual(recordCount, weixinContext.RequestMessages.Count);
            Assert.AreEqual(recordCount, weixinContext.ResponseMessages.Count);
        }
    }
}
