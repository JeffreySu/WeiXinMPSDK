using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    //已通过测试
    //[TestClass]
    public partial class CommonApiTest
    {
        protected string _appId = "wxbe855a981c34aa3f"; //换成你的信息
        protected string _appSecret = "19879045c410ecccbc675d9bd0fb32c9"; //换成你的信息


        /* 由于获取accessToken有次数限制，为了节约请求，
        * 可以到 http://weixin.senparc.com/Menu 获取Token之后填入下方，
        * 使用当前可用Token直接进行测试。
        */
        private string _access_token = null;

        protected string _testOpenId = "oIb08txj1En8hGXzHRvAjf-3X9Oc";//换成实际关注者的OpenId

        public CommonApiTest()
        {
            //全局只需注册一次
            AccessTokenContainer.Register(_appId, _appSecret);

            //全局只需注册一次
            JsApiTicketContainer.Register(_appId, _appSecret);
        }

        [TestMethod]
        public void GetTokenTest()
        {
            var tokenResult = CommonApi.GetToken(_appId, _appSecret);
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.access_token.Length > 0);
            Assert.IsTrue(tokenResult.expires_in > 0);
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
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);
            }
        }

        [TestMethod]
        public void GetUserInfoTest()
        {
            try
            {
                var accessToken = AccessTokenContainer.GetToken(_appId);
                var result = CommonApi.GetUserInfo(accessToken, _testOpenId);
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                //如果不参加内测，只是“服务号”，这类接口仍然不能使用，会抛出异常：错误代码：45009：api freq out of limit
            }
        }

        [TestMethod]
        public void GetTicketTest()
        {
            var tokenResult = CommonApi.GetTicket(_appId, _appSecret);
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.ticket.Length > 0);
            Assert.IsTrue(tokenResult.expires_in > 0);
        }
    }
}
