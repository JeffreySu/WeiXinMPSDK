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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.Helpers.Extensions;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已测试通过
    [TestClass]
    public class MediaTest : CommonApiTest
    {
        private string mediaId = null;

        [TestMethod]
        public void UploadTemporaryMediaTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var type = UploadMediaFileType.image;

#if NETCOREAPP2_0 || NETCOREAPP2_1
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\qr.jpg");
#else
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\qr.jpg");
#endif

            var result = MediaApi.UploadTemporaryMedia(accessToken, type, file);

            Assert.AreEqual(type, result.type);
            Assert.IsNotNull(result.media_id);
            mediaId = result.media_id;

        }

        [TestMethod]
        public void UploadTemporaryNewsTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var file = @"E:\1.jpg";
            var mediaId = MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.thumb, file).thumb_media_id;

            Assert.IsNotNull(mediaId);

            var new1 = new NewsModel()
            {
                author = "test",
                content = "test",
                content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                digest = "test",
                show_cover_pic = "1",
                thumb_media_id = mediaId,
                title = "test"
            };

            var new2 = new NewsModel()
            {
                author = "test",
                content = "test111",
                content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                digest = "test",
                show_cover_pic = "1",
                thumb_media_id = mediaId,
                title = "test"
            };

            var result = MediaApi.UploadTemporaryNews(accessToken, 10000, new1, new2);

            Assert.IsNotNull(result.media_id);
        }

        [TestMethod]
        public void GetStreamTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            UploadTemporaryMediaTest();//上传

            using (MemoryStream ms = new MemoryStream())
            {
                MediaApi.Get(accessToken, mediaId, ms);
                Assert.IsTrue(ms.Length > 0);

                //保存到文件

#if NETCOREAPP2_0 || NETCOREAPP2_1
                var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\testpic_{0}.core20.jpg".FormatWith(DateTime.Now.ToString("yyyyMMddHHmmss")));
#else
                var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\testpic_{0}.net45.jpg".FormatWith(DateTime.Now.ToString("yyyyMMddHHmmss")));
#endif

                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    ms.Position = 0;
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                    }
                    fs.Flush();
                }

                Assert.IsTrue(File.Exists(fileName));
            }
        }



        [TestMethod]
        public void GetDirTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            UploadTemporaryMediaTest();//上传


#if NETCOREAPP2_0 || NETCOREAPP2_1
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\");
#else
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\");
#endif

            var fileName = MediaApi.Get(accessToken, mediaId, dir);

            Assert.IsTrue(File.Exists(fileName));

            Console.WriteLine("原始文件："+ fileName);
        }


        [TestMethod()]
        public void GetVoiceTest()
        {
            string serverId = "IT41QWoGSnkt5fj01mK2ByhgRACBgvRW6fGP3bt9QAjH8vwqsra9qYJkj8LCXzNS";
            var file = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".speex"; //Server.GetMapPath("~/../")

            using (MemoryStream ms = new MemoryStream())
            {
                MediaApi.Get(base._appId, serverId, ms);

                //保存到文件
                ms.Position = 0;
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                //判断是否上传成功
                byte[] topBuffer = new byte[1];
                ms.Read(topBuffer, 0, 1);
                if (topBuffer[0] == '{')
                {
                    //写入日志
                    ms.Position = 0;
                    byte[] logBuffer = new byte[1024];
                    ms.Read(logBuffer, 0, logBuffer.Length);
                    string str = System.Text.Encoding.Default.GetString(logBuffer);
                    Console.WriteLine(str);
                    Assert.Fail();
                }
                else
                {
                    ms.Position = 0;

                    //创建目录
                    using (FileStream fs = new FileStream(file, FileMode.Create))
                    {
                        while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            fs.Write(buffer, 0, bytesRead);
                        }
                        fs.Flush();
                    }
                }

            }

        }


        private string UploadForeverMediaTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var file = @"..\..\AdvancedAPIs\Media\test.jpg";

            var result = MediaApi.UploadForeverMedia(accessToken, file);

            Assert.IsNotNull(result.media_id);
            mediaId = result.media_id;
            return mediaId;
        }

        [TestMethod]
        public void UploadForeverVideoTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var file = @"E:\Test.mp4";
            var result = MediaApi.UploadForeverVideo(accessToken, file, "测试", "测试");

            Assert.IsNotNull(result.media_id);
            mediaId = result.media_id;
        }

        //[TestMethod]
        private string UploadAndUpdateNewsTest(string accessToken)
        {
            var file = @"E:\1.jpg";
            var result = MediaApi.UploadForeverMedia(accessToken, file);

            Assert.IsNotNull(result.media_id);

            var new1 = new NewsModel()
            {
                author = "test",
                content = "test",
                content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                digest = "test",
                show_cover_pic = "1",
                thumb_media_id = result.media_id,
                title = "test"
            };

            var new2 = new NewsModel()
            {
                author = "test",
                content = "test111",
                content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                digest = "test",
                show_cover_pic = "1",
                thumb_media_id = result.media_id,
                title = "test"
            };

            var result1 = MediaApi.UploadNews(accessToken, 10000, new1, new2);

            Assert.IsNotNull(result1.media_id);

            //var result2 = MediaApi.UpdateForeverNews(accessToken, result1.media_id, 0, 10000, new2);

            //Assert.AreEqual(result2.errcode, ReturnCode.请求成功);

            return result1.media_id;
        }

        [TestMethod]
        public void GetForeverMediaTest()
        {
            var mediaId = UploadForeverMediaTest();
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            using (MemoryStream stream = new MemoryStream())
            {
                MediaApi.GetForeverMedia(accessToken, mediaId, stream);
                Assert.IsTrue(stream.Length > 0);

                var fileName = @"..\..\AdvancedAPIs\Media\test.download." + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";
                using (var fs = new FileStream(fileName, FileMode.CreateNew))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        //[TestMethod]
        private void GetForeverNewsTest(string accessToken, string mediaId)
        {
            var result = MediaApi.GetForeverNews(accessToken, mediaId);

            Assert.IsTrue(result.news_item.Count > 0);
            Assert.AreEqual(result.news_item[0].content, "test");
        }

        //[TestMethod]
        private void DeleteForeverMediaTest(string accessToken, string mediaId)
        {
            var result = MediaApi.DeleteForeverMedia(accessToken, mediaId);

            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void ForeverNewsTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            string mediaId = UploadAndUpdateNewsTest(accessToken);
            GetForeverNewsTest(accessToken, mediaId);
            DeleteForeverMediaTest(accessToken, mediaId);
        }

        [TestMethod]
        public void GetMediaListTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = MediaApi.GetNewsMediaList(accessToken, 0, 5);

            Assert.IsNotNull(result.item_count);
        }

        [TestMethod]
        public void GetNewsMediaListTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = MediaApi.GetNewsMediaList(accessToken, 0, 3);

            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
            Assert.AreEqual(result.item_count, 3);
        }

        [TestMethod]
        public void GetOthersMediaListTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = MediaApi.GetOthersMediaList(accessToken, UploadMediaFileType.image, 0, 3);

            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
            Assert.AreEqual(result.item_count, 3);
        }

        [TestMethod]
        public void AfterDeleteImgTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var file = @"..\..\AdvancedAPIs\Media\test.jpg";

            var result = MediaApi.UploadForeverMedia(accessToken, file);

            Assert.IsNotNull(result.media_id);

            CustomApi.SendImage(accessToken, "o3IHxjrPzMVZIJOgYMH1PyoTW_Tg", result.media_id);

            MediaApi.DeleteForeverMedia(accessToken, result.media_id);
        }

        [TestMethod]
        public void AfterDeleteNewsTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var file = @"E:\1.jpg";
            var result = MediaApi.UploadForeverMedia(accessToken, file);

            Assert.IsNotNull(result.media_id);

            var new1 = new NewsModel()
            {
                author = "test",
                content = "test",
                content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                digest = "test",
                show_cover_pic = "1",
                thumb_media_id = result.media_id,
                title = "test"
            };

            var new2 = new NewsModel()
            {
                author = "test",
                content = "test111",
                content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                digest = "test",
                show_cover_pic = "1",
                thumb_media_id = result.media_id,
                title = "test"
            };

            var result1 = MediaApi.UploadNews(accessToken, 10000, new1, new2);

            Assert.IsNotNull(result1.media_id);

            var clientMsgId = DateTime.Now.Ticks.ToString();
            GroupMessageApi.SendGroupMessageByOpenId(accessToken, GroupMessageType.mpnews, result1.media_id, clientMsgId, 10000, "o3IHxjrPzMVZIJOgYMH1PyoTW_Tg", "o3IHxjrPzMVZIJOgYMH1PyoTW_Tg");
            //var result2 = MediaApi.UpdateForeverNews(accessToken, result1.media_id, 0, 10000, new2);

            MediaApi.DeleteForeverMedia(accessToken, result1.media_id);
            //Assert.AreEqual(result2.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void AfterDeleteVideoTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var file = @"E:\Test.mp4";
            var result = MediaApi.UploadForeverVideo(accessToken, file, "测试", "测试", 100000);

            Assert.IsNotNull(result.media_id);

            CustomApi.SendVideo(accessToken, "o3IHxjrPzMVZIJOgYMH1PyoTW_Tg", result.media_id, "测试", "测试");
            MediaApi.DeleteForeverMedia(accessToken, result.media_id);
        }


    }
}
