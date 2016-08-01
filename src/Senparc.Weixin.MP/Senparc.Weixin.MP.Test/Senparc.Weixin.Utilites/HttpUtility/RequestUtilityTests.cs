using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs.Media;

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass]
    public class RequestUtilityTests
    {
        [TestMethod]
        public void HttpGetTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";
            var exceptResult = @"{""errcode"":40013,""errmsg"":""invalid appid""}";
            var actualResult = RequestUtility.HttpGet(url,null);
            Assert.AreEqual(exceptResult, actualResult);
        }

        [TestMethod]
        public void HttpPostTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。

            /*
             * 说明：在测试之前请确保url可用
             * 当前默认URL为Sample项目，可以使用Ctrl+F5打开Sample项目，确保可以访问
             */

            //随便找一个存在的测试图片
            var file = "..\\..\\..\\..\\Senparc.Weixin.MP.Sample\\Senparc.Weixin.MP.Sample\\Images\\qrcode.jpg";

            var url = "http://localhost:18666/TestUploadMediaFile/?token={0}&type={1}&contentLength={2}";

            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                url = string.Format(url, "TOKEN", UploadMediaFileType.image.ToString(), fs.Length);
            }

            //获取字符串结果
            var actualResult = RequestUtility.HttpPost(url, new CookieContainer(), formData: null, encoding: null);
            Assert.IsNotNull(actualResult);
            Console.WriteLine(actualResult);

            //比较强类型示例的结果
            UploadTemporaryMediaResult resultEntity = Post.GetResult<UploadTemporaryMediaResult>(actualResult);
            Assert.IsNotNull(resultEntity);
            Assert.AreEqual(UploadMediaFileType.image, resultEntity.type);
            Assert.AreEqual("MEDIA_ID", resultEntity.media_id);
            Assert.AreEqual(123456789, resultEntity.created_at);
        }
    }
}
