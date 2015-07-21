﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.Test.HttpUtility
{
    [TestClass]
    public class PostTest
    {
        [TestMethod]
        public void PostGetJsonTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url = "http://api.weixin.qq.com/cgi-bin/media/upload?access_token=TOKEN&type=image";
            try
            {
                //这里因为参数错误，系统会返回错误信息
                UploadTemporaryMediaResult resultFail = Post.PostGetJson<UploadTemporaryMediaResult>(url, cookieContainer: null, formData: null, encoding: null);
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.获取access_token时AppSecret错误或者access_token无效);
            }
        }

        [TestMethod]
        public void PostGetJsonByFormDataTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url = "http://localhost:12222/P2P/GetPassport";
            try
            {
                //这里因为参数错误，系统会返回错误信息
                var formData = new Dictionary<string, string>();
                formData["appKey"] = "test";
                formData["secret"] = "test2";
                var resultFail = Post.PostGetJson<object>(url, formData: formData);
            }
            catch (ErrorJsonResultException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DownLoadTest()
        {

        }
    }
}
