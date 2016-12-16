using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    [TestClass]
    public class AutoReplyTest : CommonApiTest
    {
        [TestMethod]
        public void ArticleSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = AutoReplyApi.GetCurrentAutoreplyInfo(accessToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}