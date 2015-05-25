using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.ShakeAround;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class ShakeAroundTest : CommonApiTest
    {
        [TestMethod]
        public void DeviceApplyTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = ShakeAroundApi.DeviceApply(accessToken, 1,"测试","测试");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UploadImageTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            string file = @"E:\测试.jpg";

            var result = ShakeAroundApi.UploadImage(accessToken, file);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
