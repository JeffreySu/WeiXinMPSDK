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
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Test
{
    [TestClass]
    public class ResponseMessageFactoryTest
    {
        string xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>63497820384</CreateTime>
  <MsgType>text</MsgType>
  <Content><![CDATA[文字信息]]></Content>
  <FuncFlag>0</FuncFlag>
</xml>";

        string xmlNews = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>63497821905</CreateTime>
  <MsgType>news</MsgType>
  <Content><![CDATA[这里是正文内容，一共将发2条Article。]]></Content>
  <ArticleCount>2</ArticleCount>
  <Articles>
    <item>
      <Title><![CDATA[您刚才发送了图片信息]]></Title>
      <Description><![CDATA[您发送的图片将会显示在边上]]></Description>
      <PicUrl><![CDATA[http://mmsns.qpic.cn/mmsns/ZxBXNzgHyUrDziagRXm6xU54o2gzuaLvibZOMWvgcpSCuYsic2xicVcoCA/0]]></PicUrl>
      <Url><![CDATA[https://sdk.weixin.senparc.com]]></Url>
    </item>
    <item>
      <Title><![CDATA[第二条]]></Title>
      <Description><![CDATA[第二条带连接的内容]]></Description>
      <PicUrl><![CDATA[http://mmsns.qpic.cn/mmsns/ZxBXNzgHyUrDziagRXm6xU54o2gzuaLvibZOMWvgcpSCuYsic2xicVcoCA/0]]></PicUrl>
      <Url><![CDATA[https://sdk.weixin.senparc.com]]></Url>
    </item>
  </Articles>
  <FuncFlag>0</FuncFlag>
</xml>";

        string xmlMusic = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
  <CreateTime>63497823450</CreateTime>
  <MsgType>music</MsgType>
  <Music>
    <Title><![CDATA[标题]]></Title>
    <Description><![CDATA[说明]]></Description>
    <MusicUrl><![CDATA[https://sdk.weixin.senparc.com/Content/music1.mp3]]></MusicUrl>
    <HQMusicUrl><![CDATA[]]></HQMusicUrl>
  </Music>
 <FuncFlag>0</FuncFlag>
</xml>";

        [TestMethod]
        public void GetResponseEntityTest()
        {
            {
                //Text
                ResponseMessageText exceptResult = new ResponseMessageText()
                                              {
                                                  ToUserName = "olPjZjsXuQPJoV0HlruZkNzKc91E",
                                                  FromUserName = "gh_a96a4a619366",
                                                  CreateTime = DateTimeHelper.GetDateTimeFromXml(63497820384),
                                                  //MsgType = ResponseMsgType.Text,
                                                  Content = "文字信息",
                                                  //FuncFlag = false
                                              };
                var result = ResponseMessageFactory.GetResponseEntity(xmlText) as ResponseMessageText;
                Assert.AreEqual(exceptResult.ToUserName, result.ToUserName);
                Assert.AreEqual(exceptResult.CreateTime, result.CreateTime);
                Assert.AreEqual(exceptResult.Content, result.Content);
            }

            {
                //Image
                ResponseMessageNews exceptResult = new ResponseMessageNews()
                                                     {
                                                         //Articles = new List<Article>(),
                                                         CreateTime = DateTimeHelper.GetDateTimeFromXml(63497821905),
                                                         FromUserName = "gh_a96a4a619366",
                                                         ToUserName = "olPjZjsXuQPJoV0HlruZkNzKc91E",
                                                         //FuncFlag = false,
                                                         //MsgType = ResponseMsgType.News
                                                     };
                var result = ResponseMessageFactory.GetResponseEntity(xmlNews) as ResponseMessageNews;
                Assert.AreEqual(exceptResult.ToUserName, result.ToUserName);
                Assert.AreEqual(exceptResult.CreateTime, result.CreateTime);
                Assert.AreEqual(2, result.ArticleCount);
                Assert.AreEqual(result.Articles.Count, result.ArticleCount);
            }

            //TODO：测试语音和视频类型

            {
                //Music
                ResponseMessageMusic exceptResult = new ResponseMessageMusic()
                                                        {
                                                            Music = new Music()
                                                                        {
                                                                            Title = "标题",
                                                                            Description = "说明",
                                                                            MusicUrl = "https://sdk.weixin.senparc.com/Content/music1.mp3",
                                                                            HQMusicUrl = ""
                                                                        },
                                                            CreateTime = DateTimeHelper.GetDateTimeFromXml(63497823450),
                                                            FromUserName = "gh_a96a4a619366",
                                                            ToUserName = "olPjZjsXuQPJoV0HlruZkNzKc91E",
                                                            //FuncFlag = false,
                                                            //MsgType = ResponseMsgType.Music
                                                        };
                var result = ResponseMessageFactory.GetResponseEntity(xmlMusic) as ResponseMessageMusic;
                Assert.AreEqual(exceptResult.ToUserName, result.ToUserName);
                Assert.AreEqual(exceptResult.CreateTime, result.CreateTime);
                //Assert.AreEqual(exceptResult.Music, result.Music);
            }
        }
    }
}
