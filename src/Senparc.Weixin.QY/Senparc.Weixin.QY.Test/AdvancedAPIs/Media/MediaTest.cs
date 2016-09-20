using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.Media;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Test.CommonApis;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class MediaTest : CommonApiTest
    {
        [TestMethod]
        public void UploadVideoTest()
        {
            string _media = "E:\\test2.mp4";
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MediaApi.Upload(accessToken, UploadMediaFileType.video, _media);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        [TestMethod]
        public string UploadImageTest()
        {
            string _media = "E:\\1.jpg";
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MediaApi.Upload(accessToken, UploadMediaFileType.image, _media);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
            return result.media_id;
        }

        [TestMethod]
        public void GetImageTest()
        {
            string mediaId = UploadImageTest();
            var accessToken = AccessTokenContainer.GetToken(_corpId);

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
        public void BatchGetMaterialTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MediaApi.BatchGetMaterial(accessToken, UploadMediaFileType.image, 0, 0, 50);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }
    }
}
