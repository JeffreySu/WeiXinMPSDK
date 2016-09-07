using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs.UserTag
{
    [TestClass]
    public class UserTagApiTests : CommonApiTest
    {
[TestMethod]
public void CreateTest()
{
    var result = Senparc.Weixin.MP.AdvancedAPIs.UserTagApi.Create(_appId, "新增标签");
    Assert.AreEqual(Senparc.Weixin.ReturnCode.请求成功,result.errcode);
}

[TestMethod]
public void GetTest()
{
    var result = Senparc.Weixin.MP.AdvancedAPIs.UserTagApi.Get(_appId, "获取标签");
    Assert.AreEqual(Senparc.Weixin.ReturnCode.请求成功, result.errcode);
}
    }

}
