using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class MemberTest
    {
        protected string _corpId = "wx7618c0a6d9358622"; //换成你的信息
        protected string _corpSecret = "PKrd-r76fDCNjbUY5-9I1vhOkMqBly038Sc8zcODscmu202dqCtUWkxK7nrCGURss"; //换成你的信息

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
            var result = MailListApi.CreateMember(accessToken, userId, "ceshi", new[] { 2 }, null, "18913536683", null, null, null, 0, extattr);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void UpdateMemberTest(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.UpdateMember(accessToken, userId, null, new[] { 2 }, null, "18913536683", email: "xxx@qq.com");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void DeleteMemberTest(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.DeleteMember(accessToken, userId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void BatchDeleteMemberTest(string[] userIds)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.BatchDeleteMember(accessToken, userIds);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void GetMember(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.GetMember(accessToken, userId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void GetDepartmentMemberTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.GetDepartmentMember(accessToken, 2, 0, 0);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void GetDepartmentMemberInfoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.GetDepartmentMemberInfo(accessToken, 2, 0, 0);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void InviteMemberTest(string userId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.InviteMember(accessToken, userId, "欢迎");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        [TestMethod]
        public void MemberTestAllSet()
        {
            string userId = "33";

            
            //var userIds = new string[] { "ceshi1", "ceshi2" };
            // BatchDeleteMemberTest(userIds);

            CreateMemberTest(userId);
            UpdateMemberTest(userId);
            InviteMemberTest(userId);
            GetMember(userId);
            GetDepartmentMemberTest();
            GetDepartmentMemberInfoTest();
            DeleteMemberTest(userId);
        }
    }
}
