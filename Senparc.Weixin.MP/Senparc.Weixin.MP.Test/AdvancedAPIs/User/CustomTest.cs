using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    //[TestClass]
    public class UserTest : CommonApiTest
    {
        private string openId = "omOTCt0E8gm6J2Fg0ArAaPS3_os8";

        [TestMethod]
        public void InfoTest()
        {
            LoadToken();

            var result = User.Info(base.tokenResult.access_token, openId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTest()
        {
            LoadToken();

            var result = User.Get(base.tokenResult.access_token, "omOTCt38ZV0hy1TpizqV0-PKAuSI");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.total > 0);
            Assert.IsTrue(result.data == null || result.data.openid.Count > 0);
        }
    }
}
