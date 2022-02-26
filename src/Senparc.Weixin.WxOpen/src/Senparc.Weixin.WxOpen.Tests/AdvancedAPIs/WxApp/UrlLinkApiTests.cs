using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.WxOpen.Tests;
using System;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Tests
{
    [TestClass()]
    public class UrlLinkApiTests : WxOpenBaseTest
    {
        [TestMethod()]
        public void GenerateTest()
        {
            var result = UrlLinkApi.Generate(base._wxOpenAppId);
            Console.WriteLine(result.ToJson(true));
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
            Assert.IsTrue(result.url_link != null);
            Assert.IsTrue(result.url_link.StartsWith("http"));
        }
    }
}