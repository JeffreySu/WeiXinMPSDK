using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    //已通过测试
    //[TestClass]
    public partial class CommonApiTest
    {
        private dynamic _appConfig;
        protected dynamic AppConfig
        {
            get
            {
                if (_appConfig == null)
                {
                    if (File.Exists("../../test.config"))
                    {
                        var doc = XDocument.Load("../../test.config");
                        _appConfig = new
                        {
                            AppId = doc.Root.Element("AppId").Value,
                            Secret = doc.Root.Element("Secret").Value,
                            MchId = doc.Root.Element("MchId").Value,
                            TenPayKey = doc.Root.Element("TenPayKey").Value,
                            TenPayCertPath = doc.Root.Element("TenPayCertPath").Value,
                        };
                    }
                    else
                    {
                        _appConfig = new
                        {
                            AppId = "YourAppId", //换成你的信息
                            Secret = "YourSecret",//换成你的信息
                            MchId = "YourMchId",//换成你的信息
                            TenPayKey = "YourTenPayKey",//换成你的信息
                            TenPayCertPath = "YourTenPayCertPath",//换成你的信息
                        };
                    }
                }
                return _appConfig;
            }
        }

        protected string _appId
        {
            get { return AppConfig.AppId; }
        }

        protected string _appSecret
        {
            get { return AppConfig.Secret; }
        }

        protected string _mchId
        {
            get { return AppConfig.MchId; }
        }

        protected string _tenPayKey
        {
            get { return AppConfig.TenPayKey; }
        }

        protected string _tenPayCertPath
        {
            get { return AppConfig.TenPayCertPath; }
        }


        /* 由于获取accessToken有次数限制，为了节约请求，
        * 可以到 http://weixin.senparc.com/Menu 获取Token之后填入下方，
        * 使用当前可用Token直接进行测试。
        */
        private string _access_token = null;

        protected string _testOpenId = "oIb08txj1En8hGXzHRvAjf-3X9Oc";//换成实际关注者的OpenId

        /// <summary>
        /// 自动获取Openid
        /// </summary>
        /// <param name="getNew">是否从服务器上强制获取一个</param>
        /// <returns></returns>
        protected string getTestOpenId(bool getNew)
        {
            if (getNew || string.IsNullOrEmpty(_testOpenId))
            {
                var accessToken = AccessTokenContainer.GetAccessToken(_appId);
                var openIdResult = UserApi.Get(accessToken, null);
                _testOpenId = openIdResult.data.openid.First();
            }
            return _testOpenId;
        }

        public CommonApiTest()
        {
            //全局只需注册一次
            AccessTokenContainer.Register(_appId, _appSecret);

            //v13.3.0之后，JsApiTicketContainer已经合并入AccessTokenContainer，已经不需要单独注册
            ////全局只需注册一次
            //JsApiTicketContainer.Register(_appId, _appSecret);
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
                var accessToken = AccessTokenContainer.GetAccessToken(_appId);
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
