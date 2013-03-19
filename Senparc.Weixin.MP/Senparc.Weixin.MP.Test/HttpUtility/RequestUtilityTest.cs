using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.Test.HttpUtility
{
    [TestClass]
    public class RequestUtilityTest
    {
        [TestMethod]
        public void DownloadStringTest()
        {
            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";
            var exceptResult = @"{""errcode"":40013,""errmsg"":""invalid appid""}";
            var actualResult = RequestUtility.DownloadString(url);
            Assert.AreEqual(exceptResult, actualResult);
        }
    }
}
