using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.Test.HttpUtility
{
    [TestClass]
    public class RequestUtilityTest
    {
        [TestMethod]
        public void HttpGetTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";
            var exceptResult = @"{""errcode"":40013,""errmsg"":""invalid appid""}";
            var actualResult = RequestUtility.HttpGet(url);
            Assert.AreEqual(exceptResult, actualResult);
        }

        [TestMethod]
        public void HttpPostTest()
        {
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

            var actualResult = RequestUtility.HttpPost(url, new CookieContainer(), file);
            Assert.AreEqual("OK", actualResult);
        }
    }
}
