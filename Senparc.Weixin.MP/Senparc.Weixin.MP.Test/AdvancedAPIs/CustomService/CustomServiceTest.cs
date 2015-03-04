using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.CustomService;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class CustomServiceTest : CommonApiTest
    {
        [TestMethod]
        public void GetRecordTest()
        {
            var openId = "o3IHxjkke04__4n1kFeXpfMjjRBc";
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.GetRecord(accessToken, DateTime.Today, DateTime.Now, null, 10, 1);
            Assert.IsTrue(result.recordlist.Count > 0);
        }
    }
}
