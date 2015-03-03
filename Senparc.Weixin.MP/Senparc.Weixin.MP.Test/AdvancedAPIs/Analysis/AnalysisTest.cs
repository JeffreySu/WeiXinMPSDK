using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.Analysis;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    [TestClass]
    public class AnalysisTest : CommonApiTest
    {
        protected string beginData = "2014-12-25";
        protected string endData = "2014-12-25";

        [TestMethod]
        public void ArticleSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = AnalysisAPI.GetArticleSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void ArticleTotalTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetArticleTotal(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserReadTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUserRead(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserReadHourTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUserReadHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserShareTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUserShare(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserShareHourTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUserShareHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetInterfaceSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetInterfaceSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetInterfaceSummaryHourTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetInterfaceSummaryHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUpStreamMsg(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgHourTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUpStreamMsgHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgWeekTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUpStreamMsgWeek(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgMonthTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUpStreamMsgMonth(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUpStreamMsgDist(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistWeekTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUpStreamMsgDistWeek(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistMonthTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUpStreamMsgDistMonth(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUserSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUserSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.list[0].ref_date != null);
        }

        [TestMethod]
        public void GetUserCumulateTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = AnalysisAPI.GetUserCumulate(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.list[0].ref_date != null);
        }
    }
}
