using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

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
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var type = UploadMediaFileType.image;
            var file = @"E:\1.jpg";
            var result = MediaApi.UploadTemporaryMedia(accessToken, type, file);

            Assert.AreEqual(type, result.type);
            Assert.IsNotNull(result.media_id);
            mediaId = result.media_id;
        }

        [TestMethod]
        public void GetTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            UploadTemporaryMediaTest();//上传

            using (MemoryStream ms = new MemoryStream())
            {
                MediaApi.Get(accessToken, mediaId, ms);
                Assert.IsTrue(ms.Length > 0);

                //保存到文件
                var fileName = string.Format(@"E:\testpic_{0}.jpg", DateTime.Now.Ticks);
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
        public void UploadForeverMediaTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var type = UploadMediaFileType.image;
            var file = @"E:\1.jpg";
            var result = MediaApi.UploadForeverMedia(accessToken, type, file);

            Assert.AreEqual(type, result.type);
            Assert.IsNotNull(result.media_id);
            mediaId = result.media_id;
        }

        [TestMethod]
        public void UploadNewsTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var type = UploadMediaFileType.thumb;
            var file = @"E:\1.jpg";
            var result = MediaApi.UploadForeverMedia(accessToken, type, file);

            Assert.AreEqual(type, result.type);
            Assert.IsNotNull(result.thumb_media_id);

            var news = new NewsModel()
                {
                    author = "test",
                    content = "test",
                    content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                    digest = "test",
                    show_cover_pic = "1",
                    thumb_media_id = result.thumb_media_id,
                    title = "test"
                };

            var result1 = MediaApi.UploadNews(accessToken, 10000, news);

            Assert.IsNotNull(result1.media_id);
        }

        [TestMethod]
        public void GetMediaListTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = MediaApi.GetNewsMediaList(accessToken, 0, 5);

            Assert.IsNotNull(result.item_count);
        }
    }
}
