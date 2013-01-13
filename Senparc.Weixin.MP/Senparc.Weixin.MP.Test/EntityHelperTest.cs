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
                    ResponseMessageBase.CreateFromRequestMessage(requestEntity, ResponseMsgType.Text) as
                    ResponseMessageText;
                Assert.IsNotNull(responseText);
                responseText.Content = "新内容";
                var responseDoc = EntityHelper.ConvertEntityToXml(responseText);
                Assert.AreEqual("新内容", responseDoc.Root.Element("Content").Value);
                Console.WriteLine(responseDoc.ToString());
            }
            {
                //News
                var responseNews =
                    ResponseMessageBase.CreateFromRequestMessage(requestEntity, ResponseMsgType.News) as
                    ResponseMessageNews;
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
            var imageXML = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1357996976</CreateTime>
  <MsgType><![CDATA[image]]></MsgType>
  <PicUrl><![CDATA[http://mmsns.qpic.cn/mmsns/ZxBXNzgHyUqazGkXUvujSOOHruk6XP5P9984HOCSATlW1orZDlpdCA/0]]></PicUrl>
  <MsgId>5832552599987382826</MsgId>
</xml>";
            var doc = XDocument.Parse(imageXML);
            var requestEntity = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageImage;
            Assert.IsNotNull(requestEntity);

            var responseNews =
                    ResponseMessageBase.CreateFromRequestMessage(requestEntity, ResponseMsgType.News) as
                    ResponseMessageNews;
            Assert.IsNotNull(responseNews);

            responseNews.Articles.Add(new Article()
            {
                Description = "测试说明",
                Title = "测试标题",
                Url = "http://www.senparc.com",
                PicUrl = requestEntity.PicUrl
            });
            Assert.AreEqual(1, responseNews.ArticleCount);

            var responseDoc = EntityHelper.ConvertEntityToXml(responseNews);
            Console.WriteLine(responseDoc.ToString());
            Assert.AreEqual(requestEntity.PicUrl, responseDoc.Root.Element("Articles").Elements("item").First().Element("PicUrl").Value);
        }
    }
}
