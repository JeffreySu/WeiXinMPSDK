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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.Media;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Test.CommonApis;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
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
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MediaApi.Upload(accessToken, UploadMediaFileType.video, _media);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        [TestMethod]
        public string UploadImageTest()
        {
            string _media = "E:\\Senparc项目\\WeiXinMPSDK\\src\\Senparc.Weixin.Work\\Senparc.Weixin.Work.Test\\AdvancedAPIs\\Media\\test.jpg";
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MediaApi.Upload(accessToken, UploadMediaFileType.image, _media);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);

            Console.WriteLine("mediaId:" + result.media_id);
            return result.media_id;
        }

        /// <summary>
        /// 上传并下载两次
        /// 相关问题：https://github.com/JeffreySu/WeiXinMPSDK/issues/1196
        /// </summary>
        [TestMethod]
        public void UploadAndGetImageTwiceTest()
        {
            GetImageTest();
            GetImageTest();
        }

        [TestMethod]
        public void GetImageTest()
        {
            string mediaId = UploadImageTest();
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

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
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MediaApi.BatchGetMaterial(accessToken, UploadMediaFileType.image, 0, 0, 50);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }
    }
}
