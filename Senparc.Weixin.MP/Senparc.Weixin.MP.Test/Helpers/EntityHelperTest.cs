using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.MP.Test
{
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Helpers;

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
            responseMusic.Music.MusicUrl = "http://weixin.senparc.com/Content/music1.mp3";
            responseMusic.Music.HQMusicUrl = "http://weixin.senparc.com/Content/music2.mp3";

            var responseDoc = EntityHelper.ConvertEntityToXml(responseMusic);
            Console.WriteLine(responseDoc.ToString());
            Assert.AreEqual(responseMusic.Music.Title, responseDoc.Root.Element("Music").Element("Title").Value);
            Assert.AreEqual(responseMusic.Music.Description, responseDoc.Root.Element("Music").Element("Description").Value);
            Assert.AreEqual(responseMusic.Music.MusicUrl, responseDoc.Root.Element("Music").Element("MusicUrl").Value);
            Assert.AreEqual(responseMusic.Music.HQMusicUrl, responseDoc.Root.Element("Music").Element("HQMusicUrl").Value);
        }
    }
}
