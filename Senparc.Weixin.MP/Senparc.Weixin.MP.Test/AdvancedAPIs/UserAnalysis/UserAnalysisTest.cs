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
    public class UserAnalysisTest : CommonApiTest
    {
        protected string beginData = "2014-12-02";
        protected string endData = "2014-12-07";

        [TestMethod]
        public void GetUserSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = UserAnalysis.GetUserSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.list[0].ref_date != null);
        }

        [TestMethod]
        public void GetUserCumulateTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = UserAnalysis.GetUserCumulate(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.list[0].ref_date != null);
        }
    }
}
