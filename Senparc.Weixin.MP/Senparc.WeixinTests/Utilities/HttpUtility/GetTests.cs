using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass]
    public class GetTest
    {
        [TestMethod]
        public void GetJsonTest()
        {
            return; //已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。

            {
                var url = "http://apistore.baidu.com/microservice/cityinfo?cityname=苏州";
                var result = Get.GetJson<dynamic>(url);
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result["errNum"]);
                Assert.AreEqual("苏州", result["retData"]["cityName"]);

                Console.WriteLine(result.GetType());
            }

            {
                var url =
             "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";
                try
                {
                    //这里因为参数错误，系统会返回错误信息
                    WxJsonResult resultFail = Get.GetJson<WxJsonResult>(url);
                    Assert.Fail(); //上一步就应该已经抛出异常
                }
                catch (ErrorJsonResultException ex)
                {
                    //实际返回的信息（错误信息）
                    Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);
                }

            }

        }
    }
}
