using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
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
        protected string beginData = "2015-3-9";
        protected string endData = "2015-3-9";

        [TestMethod]
        public void ArticleSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = AnalysisApi.GetArticleSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void ArticleTotalTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetArticleTotal(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserReadTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUserRead(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserReadHourTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUserReadHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserShareTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUserShare(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserShareHourTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUserShareHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetInterfaceSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetInterfaceSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetInterfaceSummaryHourTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetInterfaceSummaryHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUpStreamMsg(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgHourTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUpStreamMsgHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgWeekTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUpStreamMsgWeek(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgMonthTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUpStreamMsgMonth(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUpStreamMsgDist(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistWeekTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUpStreamMsgDistWeek(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistMonthTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUpStreamMsgDistMonth(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUserSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUserSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.list[0].ref_date != null);
        }

        [TestMethod]
        public void GetUserCumulateTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = AnalysisApi.GetUserCumulate(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.list[0].ref_date != null);
        }
    }
}
