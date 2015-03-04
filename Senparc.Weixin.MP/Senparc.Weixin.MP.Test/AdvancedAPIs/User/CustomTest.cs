using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    //[TestClass]
    public class UserTest : CommonApiTest
    {

        [TestMethod]
        public void InfoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = UserApi.Info(accessToken, _testOpenId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = UserApi.Get(accessToken, _testOpenId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.total > 0);
            Assert.IsTrue(result.data == null || result.data.openid.Count > 0);
        }
    }
}
