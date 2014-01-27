using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    //[TestClass]
    public class GroupTest : CommonApiTest
    {
        [TestMethod]
        public void GetTest()
        {
            LoadToken();

            var result = Groups.Get(base.tokenResult.access_token);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.groups.Count >= 3);
        }

        [TestMethod]
        public void GetIdTest()
        {
            LoadToken();

            var result = Groups.GetId(base.tokenResult.access_token, _testOpenId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.groupid >= 0);
        }
    }
}
