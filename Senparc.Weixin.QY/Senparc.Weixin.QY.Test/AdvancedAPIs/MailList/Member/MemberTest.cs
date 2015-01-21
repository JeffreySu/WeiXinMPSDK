﻿using System;
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
        protected string _corpId = "wx7618c0a6d9358622"; //换成你的信息
        protected string _corpSecret = "PKrd-r76fDCNjbUY5-9I1vhOkMqBly038Sc8zcODscmu202dqCtUWkxK7nrCGUaas"; //换成你的信息

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
            var result = Member.CreateMember(accessToken, userId, "ceshi", new[] { 2 }, null, "18913536683", null, null, null, 0, extattr);
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
        public void BatchDeleteMemberTest(string[] userIds)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Member.BatchDeleteMember(accessToken, userIds);
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

        //[TestMethod]
        public void GetDepartmentMemberInfoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Member.GetDepartmentMemberInfo(accessToken, 2, 0, 0);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        [TestMethod]
        public void MemberTestAllSet()
        {
            string userId = "003";

            
            //var userIds = new string[] { "ceshi1", "ceshi2" };
            // BatchDeleteMemberTest(userIds);

            CreateMemberTest(userId);
            UpdateMemberTest(userId);
            GetMember(userId);
            GetDepartmentMemberTest();
            GetDepartmentMemberInfoTest();
            DeleteMemberTest(userId);
        }
    }
}
