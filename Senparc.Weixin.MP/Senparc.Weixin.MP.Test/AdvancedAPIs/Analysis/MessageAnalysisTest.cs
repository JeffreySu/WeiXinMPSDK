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
    public class MessageAnalysisTest : CommonApiTest
    {
        protected string beginData = "2014-12-02";
        protected string endData = "2014-12-02";

        [TestMethod]
        public void GetUpStreamMsgTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = MessageAnalysis.GetUpStreamMsg(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgHourTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = MessageAnalysis.GetUpStreamMsgHour(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgWeekTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = MessageAnalysis.GetUpStreamMsgWeek(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgMonthTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = MessageAnalysis.GetUpStreamMsgMonth(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = MessageAnalysis.GetUpStreamMsgDist(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistWeekTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = MessageAnalysis.GetUpStreamMsgDistWeek(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetUpStreamMsgDistMonthTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = MessageAnalysis.GetUpStreamMsgDistMonth(accessToken, beginData, endData);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
