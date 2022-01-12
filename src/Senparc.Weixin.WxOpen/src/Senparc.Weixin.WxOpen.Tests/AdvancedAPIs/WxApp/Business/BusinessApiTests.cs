using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp;
using Senparc.Weixin.WxOpen.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Tests
{
    [TestClass()]
    public class BusinessApiTests : WxOpenBaseTest
    {
        [TestMethod()]
        public void GetUserPhoneNumberTest()
        {
            var code = "";
            var result = BusinessApi.GetUserPhoneNumber(base._wxOpenAppId, code);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
            Console.WriteLine(result.phone_info.ToJson(true));
        }
    }
}