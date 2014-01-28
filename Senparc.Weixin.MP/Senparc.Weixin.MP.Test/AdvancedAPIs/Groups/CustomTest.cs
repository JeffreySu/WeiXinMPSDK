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
        public void CreateTest()
        {
            LoadToken();

            var result = Groups.Create(base.tokenResult.access_token, "测试组");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.group.id >= 100);
        }

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

        [TestMethod]
        public void UpdateTest()
        {
            LoadToken();

            var result = Groups.Update(base.tokenResult.access_token, 100, "测试组更新");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        [TestMethod]
        public void MemberUpdateTest()
        {
            LoadToken();

            var idArr = new[] { 0, 1, 2, 100,0 };
            foreach (var id in idArr)
            {
                var result = Groups.MemberUpdate(base.tokenResult.access_token, _testOpenId, id);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.errcode == ReturnCode.请求成功);
                var newGroupIdResult = Groups.GetId(base.tokenResult.access_token, _testOpenId);
                Assert.AreEqual(id, newGroupIdResult.groupid);
            }
        }
    }
}
