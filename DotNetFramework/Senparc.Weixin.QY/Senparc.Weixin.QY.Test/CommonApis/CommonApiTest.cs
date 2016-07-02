using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.Test.CommonApis
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class CommonApiTest
    {
        protected string _corpId = "wx7618c0a6d9358622"; //换成你的信息
        protected string _corpSecret = "PKrd-r76fDCNjbUY5-9I1vhOkMqBly038Sc8zcODscmu202dqCtUWkxK7nrCGUR3"; //换成你的信息

        public CommonApiTest()
        {
            //全局只需注册一次
            AccessTokenContainer.Register(_corpId, _corpSecret);
        }

        [TestMethod]
        public void GetTokenTest()
        {
            var tokenResult = CommonApi.GetToken(_corpId, _corpSecret);
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.access_token.Length > 0);
        }

        [TestMethod]
        public void GetTokenFailTest()
        {
            try
            {
                var result = CommonApi.GetToken("appid", "secret");
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode_QY.不合法的corpid);
            }
        }

        [TestMethod]
        public void GetCallBackIpTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);

            var result = CommonApi.GetCallBackIp(accessToken);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }
    }
}
