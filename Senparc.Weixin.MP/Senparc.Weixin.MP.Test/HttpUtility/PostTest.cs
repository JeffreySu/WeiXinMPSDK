using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.Test.HttpUtility
{
    [TestClass]
    public class PostTest
    {
        [TestMethod]
        public void GetJsonTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url = "http://api.weixin.qq.com/cgi-bin/media/upload?access_token=TOKEN&type=image";
            try
            {
                //这里因为参数错误，系统会返回错误信息
                UploadMediaFileResult resultFail = Post.GetJson<UploadMediaFileResult>(url);
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
