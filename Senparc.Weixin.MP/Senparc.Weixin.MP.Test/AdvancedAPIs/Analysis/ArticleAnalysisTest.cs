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
    public class ArticleAnalysisTest : CommonApiTest
    {
        protected string beginData = "2014-12-25";
        protected string endData = "2014-12-25";

        [TestMethod]
        public void ArticleSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = ArticleAnalysis.GetArticleSummary(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void ArticleTotalTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = ArticleAnalysis.GetArticleTotal(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserReadTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = ArticleAnalysis.GetUserRead(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserReadHourTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = ArticleAnalysis.GetUserReadHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserShareTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = ArticleAnalysis.GetUserShare(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UserShareHourTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = ArticleAnalysis.GetUserShareHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
