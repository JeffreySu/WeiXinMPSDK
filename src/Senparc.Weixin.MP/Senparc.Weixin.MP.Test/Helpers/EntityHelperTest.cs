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
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.MP.Test
{
    using Senparc.NeuChar;
    using Senparc.NeuChar.Entities;
    using Senparc.Weixin.MP.Entities;
    using Senparc.NeuChar.Helpers;

    [TestClass]
    public class EntityHelperTest
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
        public void FillEntityWithXmlTest()
        {
            var doc = XDocument.Parse(xml);
            var entity = RequestMessageFactory.GetRequestEntity(doc);
            EntityHelper.FillEntityWithXml(entity as RequestMessageBase, doc);

            Assert.AreEqual("gh_a96a4a619366", entity.ToUserName);
            Assert.AreEqual(RequestMsgType.Text, entity.MsgType);
        }


        #region 可为空对象测试

        class NullableClass : ResponseMessageBase, IResponseMessageBase
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int? AgentId { get; set; }
            public int? MchId { get; set; }

            public string ToUserName { get; set; }
            public string FromUserName { get; set; }
            public DateTimeOffset CreateTime { get; set; }
            public ResponseMsgType MsgType { get; }
        }
        [TestMethod]
        public void ConvertEntityToXml_NullableTest()
        {


            var nullableTestXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <Id><![CDATA[10]]></Id>
  <Name><![CDATA[Jeffrey Su]]></Name>
  <AgentId></AgentId>
  <MchId>123</MchId>
</xml>";
            var doc = XDocument.Parse(nullableTestXml);
            var entity = new NullableClass();
            EntityHelper.FillEntityWithXml(entity as ResponseMessageBase, doc);

            Assert.AreEqual(10, entity.Id);
            Assert.AreEqual("Jeffrey Su", entity.Name);
            Assert.AreEqual(null, entity.AgentId);
            Assert.AreEqual(123, entity.MchId);
        }
        #endregion

        #region 多层嵌套Entity测试

        private string embedXml = @"<xml>
    <ToUserName><![CDATA[gh_4d00ed8d6399]]></ToUserName>
    <FromUserName><![CDATA[oV5CrjpxgaGXNHIQigzNlgLTnwic]]></FromUserName>
    <CreateTime>1481013459</CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[MASSSENDJOBFINISH]]></Event>
    <MsgID>1000001625</MsgID>
    <Status><![CDATA[err(30003)]]></Status>
    <TotalCount>0</TotalCount>
    <FilterCount>0</FilterCount>
    <SentCount>0</SentCount>
    <ErrorCount>0</ErrorCount>
    <CopyrightCheckResult>
    <Count>2</Count>
    <ResultList>
        <item>
        <ArticleIdx>1</ArticleIdx>
        <UserDeclareState>0</UserDeclareState>
        <AuditState>2</AuditState>
        <OriginalArticleUrl><![CDATA[Url_1]]></OriginalArticleUrl>
        <OriginalArticleType>1</OriginalArticleType>
        <CanReprint>1</CanReprint>
        <NeedReplaceContent>1</NeedReplaceContent>
        <NeedShowReprintSource>1</NeedShowReprintSource>
        </item>
        <item>
        <ArticleIdx>2</ArticleIdx>
        <UserDeclareState>0</UserDeclareState>
        <AuditState>2</AuditState>
        <OriginalArticleUrl><![CDATA[Url_2]]></OriginalArticleUrl>
        <OriginalArticleType>1</OriginalArticleType>
        <CanReprint>1</CanReprint>
        <NeedReplaceContent>1</NeedReplaceContent>
        <NeedShowReprintSource>1</NeedShowReprintSource>
        </item>
    </ResultList>
    <CheckState>2</CheckState>
    </CopyrightCheckResult>
</xml>";


        /// <summary>
        /// 测试多层复杂结构XML
        /// </summary>
        [TestMethod]
        public void FillEntityWithEmbedXmlTest()
        {
            var doc = XDocument.Parse(embedXml);
            var entity = RequestMessageFactory.GetRequestEntity(doc);
            EntityHelper.FillEntityWithXml(entity as RequestMessageBase, doc);

            Assert.AreEqual("gh_4d00ed8d6399", entity.ToUserName);
            Assert.AreEqual(RequestMsgType.Event, entity.MsgType);

            var strongEntity = entity as RequestMessageEvent_MassSendJobFinish;
            Assert.IsNotNull(strongEntity);
            Assert.AreEqual(2,strongEntity.CopyrightCheckResult.Count);
            Assert.AreEqual("Url_1", strongEntity.CopyrightCheckResult.ResultList[0].item.OriginalArticleUrl);
            Assert.AreEqual("Url_2", strongEntity.CopyrightCheckResult.ResultList[1].item.OriginalArticleUrl);
        }

        #endregion


        [TestMethod]
        public void ConvertEntityToXmlTest()
        {
            var doc = XDocument.Parse(xml);
            var requestEntity = RequestMessageFactory.GetRequestEntity(doc);

            {
                //Text
                var responseText =
                    ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestEntity);
                Assert.IsNotNull(responseText);
                responseText.Content = "新内容";
                var responseDoc = EntityHelper.ConvertEntityToXml(responseText);

                Console.WriteLine(responseDoc.ToString());

                Assert.AreEqual("新内容", responseDoc.Root.Element("Content").Value);
            }

            {
                //News
                var responseNews =
                    ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestEntity);
                Assert.IsNotNull(responseNews);

                responseNews.Articles.Add(new Article()
                {
                    Description = "测试说明",
                    Title = "测试标题",
                    Url = "http://www.senparc.com",
                    PicUrl = "http://img.senparc.com/images/v2/logo.jpg'"
                });
                Assert.AreEqual(1, responseNews.ArticleCount);
                var responseDoc = EntityHelper.ConvertEntityToXml(responseNews);
                Console.WriteLine(responseDoc.ToString());
            }
        }

        [TestMethod]
        public void ConvertEntityToXml_ImageTest()
        {
            {
                var imageRequestXML = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1357996976</CreateTime>
  <MsgType><![CDATA[image]]></MsgType>
  <PicUrl><![CDATA[http://mmsns.qpic.cn/mmsns/ZxBXNzgHyUqazGkXUvujSOOHruk6XP5P9984HOCSATlW1orZDlpdCA/0]]></PicUrl>
  <MsgId>5832552599987382826</MsgId>
  <MediaId><![CDATA[Mj0WUTZeeG9yuBKhGP7iR5n1xUJO9IpTjGNC4buMuswfEOmk6QSIRb_i98do5nwo]]></MediaId>
</xml>";
                var doc = XDocument.Parse(imageRequestXML);
                var requestEntity = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageImage;
                Assert.IsNotNull(requestEntity);

                //var responseNews =
                //        ResponseMessageBase.CreateFromRequestMessage(requestEntity, ResponseMsgType.News) as
                //        ResponseMessageNews;
                //Assert.IsNotNull(responseNews);

                //responseNews.Articles.Add(new Article()
                //{
                //    Description = "测试说明",
                //    Title = "测试标题",
                //    Url = "http://www.senparc.com",
                //    PicUrl = requestEntity.PicUrl
                //});
                //Assert.AreEqual(1, responseNews.ArticleCount);

                //var responseDoc = EntityHelper.ConvertEntityToXml(responseNews);
                //Console.WriteLine(responseDoc.ToString());
                //Assert.AreEqual(requestEntity.PicUrl, responseDoc.Root.Element("Articles").Elements("item").First().Element("PicUrl").Value);


                //返回图片信息
                var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageImage>(requestEntity);
                responseMessage.Image.MediaId = requestEntity.MediaId;
                var responseDoc = EntityHelper.ConvertEntityToXml(responseMessage);
                Assert.IsNotNull(responseDoc);

            }

            //            {
            //                var imageResponseXML = @"<?xml version=""1.0"" encoding=""utf-8""?>
            //<xml>
            //  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
            //<FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
            //<CreateTime>1392354263</CreateTime>
            //<MsgType><![CDATA[image]]></MsgType>
            //<PicUrl><![CDATA[http://mmbiz.qpic.cn/mmbiz/ZxBXNzgHyUqDOeR0nSWZ4ibeF49C2yBbUB9tltJaFLqvjvDOUkt1tgp3q2cr1KZMLRsHHA2380sAggSPRuRMjicQ/0]]></PicUrl>
            //<MsgId>5980116024231210973</MsgId>
            //<MediaId><![CDATA[Mj0WUTZeeG9yuBKhGP7iR5n1xUJO9IpTjGNC4buMuswfEOmk6QSIRb_i98do5nwo]]></MediaId>
            //</xml>";
            //            }
        }

        [TestMethod]
        public void ConvertEntityToXml_MusicTest()
        {
            var voiceTest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1361430302</CreateTime>
  <MsgType><![CDATA[voice]]></MsgType>
  <MediaId><![CDATA[X1yfgB2XI-faU6R2jmKz0X1JZmPCxIvM-9ktt4K92BB9577SCi41S-qMl60q5DJo]]></MediaId>
  <Format><![CDATA[amr]]></Format>
  <MsgId>5847298622973403529</MsgId>
</xml>";
            var doc = XDocument.Parse(voiceTest);
            var requestEntity = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageVoice;
            Assert.IsNotNull(requestEntity);

            var responseMusic =
                    ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestEntity);
            Assert.IsNotNull(responseMusic);

            responseMusic.Music.Title = "测试Music";
            responseMusic.Music.Description = "测试Music的说明";
            responseMusic.Music.MusicUrl = "https://sdk.weixin.senparc.com/Content/music1.mp3";
            responseMusic.Music.HQMusicUrl = "https://sdk.weixin.senparc.com/Content/music2.mp3";

            var responseDoc = EntityHelper.ConvertEntityToXml(responseMusic);
            Console.WriteLine(responseDoc.ToString());
            Assert.AreEqual(responseMusic.Music.Title, responseDoc.Root.Element("Music").Element("Title").Value);
            Assert.AreEqual(responseMusic.Music.Description, responseDoc.Root.Element("Music").Element("Description").Value);
            Assert.AreEqual(responseMusic.Music.MusicUrl, responseDoc.Root.Element("Music").Element("MusicUrl").Value);
            Assert.AreEqual(responseMusic.Music.HQMusicUrl, responseDoc.Root.Element("Music").Element("HQMusicUrl").Value);
        }
    }
}
