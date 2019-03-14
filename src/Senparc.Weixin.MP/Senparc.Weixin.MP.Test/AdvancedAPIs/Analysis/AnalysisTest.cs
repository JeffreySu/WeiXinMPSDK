#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Analysis;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
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
