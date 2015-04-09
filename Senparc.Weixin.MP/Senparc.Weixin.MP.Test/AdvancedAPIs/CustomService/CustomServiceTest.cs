﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.CustomService;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class CustomServiceTest : CommonApiTest
    {
        protected string _custonPassWord = MD5UtilHelper.GetMD5("123123", null);

        [TestMethod]
        public void GetRecordTest()
        {
            var openId = "o3IHxjkke04__4n1kFeXpfMjjRBc";
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.GetRecord(accessToken, DateTime.Today, DateTime.Now, null, 10, 1);
            Assert.IsTrue(result.recordlist.Count > 0);
        }

        [TestMethod]
        public void GetCustomBasicInfoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.GetCustomBasicInfo(accessToken);
            Assert.IsTrue(result.kf_list.Count > 0);
        }

        [TestMethod]
        public void GetCustomOnlineInfoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.GetCustomOnlineInfo(accessToken);
            Assert.IsTrue(result.kf_online_list.Count > 0);
        }

        [TestMethod]
        public void AddCustomTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.AddCustom(accessToken, "zcc@SenparcRobot", "zcc", _custonPassWord);
            Assert.AreEqual(result.errcode,ReturnCode.请求成功);
        }

        [TestMethod]
        public void UpdateCustomTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.UpdateCustom(accessToken, "zcc@SenparcRobot", "zcc", _custonPassWord);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UploadCustomHeadimgCustom()
        {
            string file = "C:\\Users\\czhang\\Desktop\\logo.png";

            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.UploadCustomHeadimg(accessToken, "zcc@SenparcRobot", file);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void DeleteCustomTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.DeleteCustom(accessToken, "zcc@SenparcRobot");
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void CreateSessionTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.CreateSession(accessToken, _testOpenId, "zcc@SenparcRobot");
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void CloseSessionTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.CloseSession(accessToken, _testOpenId, "zcc@SenparcRobot");
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetSessionStateTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.GetSessionState(accessToken, _testOpenId);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetSessionListTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.GetSessionList(accessToken, _testOpenId);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void GetWaitCaseTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CustomServiceApi.GetWaitCase(accessToken);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
