using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.Custom
{
    [TestClass]
    public class CustomApiTests : WxOpenBaseTest
    {
        [TestMethod]
        public void SendLinkTest()
        {
            var openId = "";

            var result = Senparc.Weixin.WxOpen.AdvancedAPIs.CustomApi.SendLink(base._wxOpenAppId, openId, "测试小程序图文链接", "说明", "https://weixin.senparc.com", "https://weixin.senparc.com");

            //需要48小时内有交互
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }
    }
}
