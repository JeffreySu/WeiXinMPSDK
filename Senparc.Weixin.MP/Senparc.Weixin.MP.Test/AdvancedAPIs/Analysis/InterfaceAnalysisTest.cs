using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    [TestClass]
    public class InterfaceAnalysisTest : CommonApiTest
    {
        protected string beginData = "2014-12-02";
        protected string endData = "2014-12-02";

        [TestMethod]
        public void GetInterfaceSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = InterfaceAnalysis.GetInterfaceSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetInterfaceSummaryHourTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = InterfaceAnalysis.GetInterfaceSummaryHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
