using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class MemberTest
    {
        protected string _corpId = "wxccd01c4e6bf59232"; //换成你的信息
        protected string _corpSecret = "ejXcV7rb9OtakBucpMji1kUtPmnKy4hNCskW_bUKLx8lRxO_aVrcc0gVTMEv13G1"; //换成你的信息

        public MemberTest()
        {
            //全局只需注册一次
            AccessTokenContainer.Register(_corpId, _corpSecret);
        }

        [TestMethod]
        public void CreateMemberTest(string userId)
        {
            Extattr extattr = new Extattr()
            {
                attrs = new List<Attr>()
                        {
                            new Attr(){ name = "员工角色",value = "123"}
                        }
            };
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Member.CreateMember(accessToken, userId, "ceshi", new[]{2}, null, "18913536683",null,null,null,0,extattr);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void UpdateMemberTest(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Member.UpdateMember(accessToken, userId, null, new[] { 2 }, null, "18913536683");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void DeleteMemberTest(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Member.DeleteMember(accessToken, userId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void GetMember(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Member.GetMember(accessToken, userId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void GetDepartmentMemberTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Member.GetDepartmentMember(accessToken, 2, 0, 0);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        [TestMethod]
        public void MemberTestAllSet()
        {
            string userId = "003";
            CreateMemberTest(userId);
            UpdateMemberTest(userId);
            GetMember(userId);
            GetDepartmentMemberTest();
            DeleteMemberTest(userId);
        }
    }
}
