using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.Test.HttpUtility
{
    [TestClass]
    public class GetTest
    {
        [TestMethod]
        public void GetJsonTest()
        {
            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";
            try
            {
                //这里因为参数错误，系统会返回错误信息
                AccessTokenResult resultFail = Get.GetJson<AccessTokenResult>(url);
                Assert.IsNull(resultFail);

                //期望的信息（错误信息）
                WxJsonResult resultSuuces = Get.GetJson<WxJsonResult>(url);
                Assert.AreEqual(resultSuuces.errorcode, ReturnCode.不合法的APPID);
            }
            catch (Exception)
            {
               
            }
        }
    }
}
