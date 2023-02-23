using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.OpenAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.WeixinTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.OpenAPIs.Tests
{
    [TestClass()]
    public class OpenApiTests : CommonApiTest
    {
        [TestMethod()]
        public void QuotaGetTest()
        {
            var appId = base._appId;

            var result1 = OpenApi.QuotaGet(appId, "/cgi-bin/message/custom/send");

            Assert.IsNotNull(result1);
            Console.WriteLine(result1.ToJson(true));

            var openId = "oxRg0uLsnpHjb8o93uVnwMK_WAVw";
            var sendResult = AdvancedAPIs.CustomApi.SendText(appId, openId, "测试客服接口，增加接口调用次数");

            var result2 = OpenApi.QuotaGet(appId, "/cgi-bin/message/custom/send");
            Assert.IsNotNull(result2);
            Console.WriteLine(result2.ToJson(true));

            Assert.AreEqual(result2.quota.used, result1.quota.used + 1);

        }

        [TestMethod()]
        public void RidGetTest()
        {
            var appId = base._appId;
            var urlPath = "/cgi-bin/message/custom/send/worongApi";
            try
            {
                var result = OpenApi.QuotaGet(appId, urlPath);
                Console.WriteLine(result.ToJson(true));
            }
            catch (Senparc.Weixin.Exceptions.ErrorJsonResultException ex)
            {
                Console.WriteLine(ex.JsonResult.ToJson(true));

                Thread.Sleep(1000);//时间太快rid还没有被记录

                var rid = ex.GetRid();
                Console.WriteLine("rid:" + rid);

                var result = OpenApi.RidGet(appId, "62e56973-0c7be0b1-368b3439");
                Assert.IsNotNull(result);
                Console.WriteLine(result.ToJson(true));

                Assert.IsTrue(result.request.request_body.Contains(urlPath));
                Assert.IsTrue(result.request.response_body.Contains("cgi_path not found"));
            }

        }
    }
}