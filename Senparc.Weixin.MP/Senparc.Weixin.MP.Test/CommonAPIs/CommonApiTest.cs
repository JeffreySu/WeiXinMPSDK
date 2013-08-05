using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    [TestClass]
    public partial class CommonApiTest
    {
        protected AccessTokenResult tokenResult = null;

        protected AccessTokenResult LoadToken()
        {
            if (tokenResult==null)
            {
                //正确数据，请填写微信公众账号后台的AppId及AppSecret
                tokenResult = CommonApi.GetToken("wx669ef95216eef885", "0a9f8daa099d06d488ff900eb0cf0f43");
            }
            return tokenResult;
        }

        [TestMethod]
        public void GetTokenTest()
        {
            LoadToken();
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.access_token.Length > 0);
            Assert.IsTrue(tokenResult.expires_in > 0);
        }

        public void GetTokenFailTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            try
            {
                var result = CommonApi.GetToken("appid", "secret");
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);
            }
        }

        [TestMethod]
        public void GetUserInfoTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。

            try
            {
                GetTokenTest();
                var result = CommonApi.GetUserInfo(tokenResult.access_token, "olPjZjsXuQPJoV0HlruZkNzKc91E");
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                //如果不参加内测，只是“服务号”，这类接口仍然不能使用，会抛出异常：错误代码：45009：api freq out of limit
            }
        }

        [TestMethod]
        public void UploadMediaFileTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            try
            {
                var file = "..\\..\\..\\..\\Senparc.Weixin.MP.Sample\\Senparc.Weixin.MP.Sample\\Images\\qrcode.jpg";

                var result = CommonApi.UploadMediaFile("token", UploadMediaFileType.image, file);
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.验证失败);
            }
        }
    }
}
