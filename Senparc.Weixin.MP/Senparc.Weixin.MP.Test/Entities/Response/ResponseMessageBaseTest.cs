using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.Entities.Response
{
    [TestClass]
    public class ResponseMessageBaseTest
    {
        RequestMessageText requestMessage = new RequestMessageText()
                                            {
                                                MsgId = 1,
                                                CreateTime = DateTime.Now,
                                                FromUserName = "TNT2",
                                                ToUserName = "Senparc",
                                                //MsgType = RequestMsgType.Text,
                                                Content = "This is a text message."
                                            };

        [TestMethod]
        public void CreateFromRequestMessageTest()
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            Assert.IsNotNull(responseMessage);
            Assert.AreEqual(ResponseMsgType.Text, responseMessage.MsgType);
            Assert.AreEqual("Senparc", responseMessage.FromUserName);
            Assert.AreEqual("TNT2", responseMessage.ToUserName);
        }

        [TestMethod]
        public void CreateFromRequestMessageGenericTest()
        {
            {
                var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
                Assert.IsNotNull(responseMessage);
                Assert.AreEqual(ResponseMsgType.Text, responseMessage.MsgType);
                Assert.AreEqual("Senparc", responseMessage.FromUserName);
                Assert.AreEqual("TNT2", responseMessage.ToUserName);
            }

            {
                var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
                Assert.IsNotNull(responseMessage);
                Assert.AreEqual(ResponseMsgType.News, responseMessage.MsgType);
            }

            {
                var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestMessage);
                Assert.IsNotNull(responseMessage);
                Assert.AreEqual(ResponseMsgType.Music, responseMessage.MsgType);
            }

            {
                try
                {
                    var responseMessage =
                        ResponseMessageBase.CreateFromRequestMessage<ResponseMessageBase>(requestMessage);
                    Assert.Fail();//上一步应该抛出异常，因为没有对应的ResponseMsgType
                }
                catch (WeixinException ex)
                {

                }
            }
        }

        [TestMethod]
        public void CreateFromResponseXml()
        {
            #region Text
            {
                var responseMessageText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>1384322280</CreateTime>
  <MsgType>text</MsgType>
  <Content><![CDATA[您点击了底部按钮。]]></Content>
  <FuncFlag>0</FuncFlag>
</xml>";
                var responseMessage = ResponseMessageBase.CreateFromResponseXml(responseMessageText);
                Assert.IsInstanceOfType(responseMessage, typeof(ResponseMessageText));
                var strongResponseMessage = responseMessage as ResponseMessageText;
                Assert.AreEqual("olPjZjsXuQPJoV0HlruZkNzKc91E", strongResponseMessage.ToUserName);
                Assert.AreEqual("gh_a96a4a619366", strongResponseMessage.FromUserName);
                Assert.AreEqual("您点击了底部按钮。", strongResponseMessage.Content);
                //Assert.AreEqual(false, strongResponseMessage.FuncFlag);
            }
            #endregion

            #region Image
            {
                var responseMessageImage = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>1384322770</CreateTime>
  <MsgType><![CDATA[image]]></MsgType>
  <Image>
    <MediaId><![CDATA[media_id]]></MediaId>
  </Image>
</xml>";
                var responseMessage = ResponseMessageBase.CreateFromResponseXml(responseMessageImage);
                Assert.IsInstanceOfType(responseMessage, typeof(ResponseMessageImage));
                var strongResponseMessage = responseMessage as ResponseMessageImage;
                Assert.AreEqual("olPjZjsXuQPJoV0HlruZkNzKc91E", strongResponseMessage.ToUserName);
                Assert.AreEqual("gh_a96a4a619366", strongResponseMessage.FromUserName);
                Assert.AreEqual("media_id", strongResponseMessage.Image.MediaId);
            }
            #endregion

            #region Voice
            {
                var responseMessageImage = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>1384322770</CreateTime>
  <MsgType><![CDATA[voice]]></MsgType>
  <Voice>
    <MediaId><![CDATA[media_id]]></MediaId>
  </Voice>
</xml>";
                var responseMessage = ResponseMessageBase.CreateFromResponseXml(responseMessageImage);
                Assert.IsInstanceOfType(responseMessage, typeof(ResponseMessageVoice));
                var strongResponseMessage = responseMessage as ResponseMessageVoice;
                Assert.AreEqual("olPjZjsXuQPJoV0HlruZkNzKc91E", strongResponseMessage.ToUserName);
                Assert.AreEqual("gh_a96a4a619366", strongResponseMessage.FromUserName);
                Assert.AreEqual("media_id", strongResponseMessage.Voice.MediaId);
            }
            #endregion

            #region Video
            {
                var responseMessageImage = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>1384322770</CreateTime>
  <MsgType><![CDATA[video]]></MsgType>
  <Video>
    <MediaId><![CDATA[media_id]]></MediaId>
    <Title><![CDATA[title]]></Title>
    <Description><![CDATA[description]]></Description>
  </Video> 
</xml>";
                var responseMessage = ResponseMessageBase.CreateFromResponseXml(responseMessageImage);
                Assert.IsInstanceOfType(responseMessage, typeof(ResponseMessageVideo));
                var strongResponseMessage = responseMessage as ResponseMessageVideo;
                Assert.AreEqual("olPjZjsXuQPJoV0HlruZkNzKc91E", strongResponseMessage.ToUserName);
                Assert.AreEqual("gh_a96a4a619366", strongResponseMessage.FromUserName);
                Assert.AreEqual("media_id", strongResponseMessage.Video.MediaId);
                Assert.AreEqual("title", strongResponseMessage.Video.Title);
                Assert.AreEqual("description", strongResponseMessage.Video.Description);
            }
            #endregion

            #region Music
            {
                var responseMessageMusic = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>1384322770</CreateTime>
  <MsgType>music</MsgType>
  <Music>
    <Title><![CDATA[标题]]></Title>
    <Description><![CDATA[描述]]></Description>
    <MusicUrl><![CDATA[http://weixin.senparc.com/Content/music1.mp3]]></MusicUrl>
    <HQMusicUrl><![CDATA[]]></HQMusicUrl>
  </Music>
  <FuncFlag>0</FuncFlag>
</xml>";
                var responseMessage = ResponseMessageBase.CreateFromResponseXml(responseMessageMusic);
                Assert.IsInstanceOfType(responseMessage, typeof(ResponseMessageMusic));
                var strongResponseMessage = responseMessage as ResponseMessageMusic;
                Assert.AreEqual("olPjZjsXuQPJoV0HlruZkNzKc91E", strongResponseMessage.ToUserName);
                Assert.AreEqual("gh_a96a4a619366", strongResponseMessage.FromUserName);
                Assert.AreEqual("标题", strongResponseMessage.Music.Title);
                Assert.AreEqual("描述", strongResponseMessage.Music.Description);
                Assert.AreEqual("http://weixin.senparc.com/Content/music1.mp3", strongResponseMessage.Music.MusicUrl);
                Assert.AreEqual("", strongResponseMessage.Music.HQMusicUrl);
            }
            #endregion

            #region News
            {
                var responseMessageNews = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>1384322646</CreateTime>
  <MsgType>news</MsgType>
  <ArticleCount>1</ArticleCount>
  <Articles>
    <item>
      <Title><![CDATA[您点击了子菜单图文按钮]]></Title>
      <Description><![CDATA[您点击了子菜单图文按钮，这是一条图文信息。]]></Description>
      <PicUrl><![CDATA[http://weixin.senparc.com/Images/qrcode.jpg]]></PicUrl>
      <Url><![CDATA[http://weixin.senparc.com]]></Url>
    </item>
  </Articles>
  <FuncFlag>0</FuncFlag>
</xml>";
                var responseMessage = ResponseMessageBase.CreateFromResponseXml(responseMessageNews);
                Assert.IsInstanceOfType(responseMessage, typeof(ResponseMessageNews));
                var strongResponseMessage = responseMessage as ResponseMessageNews;
                Assert.AreEqual("olPjZjsXuQPJoV0HlruZkNzKc91E", strongResponseMessage.ToUserName);
                Assert.AreEqual("gh_a96a4a619366", strongResponseMessage.FromUserName);
                Assert.AreEqual(1, strongResponseMessage.ArticleCount);
                Assert.AreEqual("您点击了子菜单图文按钮", strongResponseMessage.Articles[0].Title);
            }
            #endregion

        }
    }
}
