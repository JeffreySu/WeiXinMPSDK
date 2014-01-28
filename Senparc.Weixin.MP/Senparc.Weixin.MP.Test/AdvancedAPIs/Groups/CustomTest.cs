using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    //[TestClass]
    public class GroupTest : CommonApiTest
    {
        [TestMethod]
        public void CreateTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = Groups.Create(accessToken, "测试组");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.group.id >= 100);
        }

        [TestMethod]
        public void GetTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = Groups.Get(accessToken);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.groups.Count >= 3);
        }

        [TestMethod]
        public void GetIdTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = Groups.GetId(accessToken, _testOpenId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.groupid >= 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = Groups.Update(accessToken, 100, "测试组更新");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        [TestMethod]
        public void MemberUpdateTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var idArr = new[] { 0, 1, 2, 100, 0 };
            foreach (var id in idArr)
            {
                var result = Groups.MemberUpdate(accessToken, _testOpenId, id);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.errcode == ReturnCode.请求成功);
                var newGroupIdResult = Groups.GetId(accessToken, _testOpenId);
                Assert.AreEqual(id, newGroupIdResult.groupid);
            }
        }
    }
}
