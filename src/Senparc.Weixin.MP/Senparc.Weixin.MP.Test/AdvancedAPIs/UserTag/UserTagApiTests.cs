using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs.UserTag
{
    [TestClass]
    public class UserTagApiTests : CommonApiTest
    {
        //private CreateTagResult CreateResult;

        [TestMethod]
        public void CreateAndGetTest()
        {
            var createResult = Senparc.Weixin.MP.AdvancedAPIs.UserTagApi.Create(_appId, "新增标签");
            Assert.AreEqual(Senparc.Weixin.ReturnCode.请求成功, createResult.errcode);

            var getResult = Senparc.Weixin.MP.AdvancedAPIs.UserTagApi.Get(_appId, createResult.tag.id);
            Assert.AreEqual(Senparc.Weixin.ReturnCode.请求成功, getResult.errcode);

        }

    }

}
