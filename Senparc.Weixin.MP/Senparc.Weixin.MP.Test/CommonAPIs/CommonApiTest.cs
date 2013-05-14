using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    [TestClass]
    public class CommonApiTest
    {
        [TestMethod]
        public void GetTokenTest()
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
                var result = CommonApi.GetUserInfo("token", "olPjZjsXuQPJoV0HlruZkNzKc91E");
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.验证失败);
            }
        }

        [TestMethod]
        public void UploadMediaFileTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            try
            {
                var result = CommonApi.UploadMediaFile("token", UploadMediaFileType.image, "C:\\NotExist.jpg");
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
