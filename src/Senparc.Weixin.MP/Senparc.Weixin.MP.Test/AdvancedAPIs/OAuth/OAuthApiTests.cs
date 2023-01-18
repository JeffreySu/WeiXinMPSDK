using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Tests
{
    [TestClass()]
    public class OAuthApiTests
    {
        [TestMethod()]
        public void GetAuthorizeUrlTest()
        {
            //常规情况
            {
                var appId = "APPID";
                var redirectUrl = "REDIRECT_URI";
                var scope = OAuthScope.snsapi_userinfo;
                var code = "CODE";
                var state = "STATE";
                var forcePopup = (bool?)null;
                var addConnectRedirect = false;

                var url = OAuthApi.GetAuthorizeUrl(appId, redirectUrl, state, scope, code, addConnectRedirect, forcePopup);

                Assert.AreEqual("https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=CODE&scope=snsapi_userinfo&state=STATE#wechat_redirect", url);
            }

            //存在 forcePopup
            {
                var appId = "APPID";
                var redirectUrl = "REDIRECT_URI";
                var scope = OAuthScope.snsapi_userinfo;
                var code = "CODE";
                var state = "STATE";
                var forcePopup = true;
                var addConnectRedirect = false;

                var url = OAuthApi.GetAuthorizeUrl(appId, redirectUrl, state, scope, code, addConnectRedirect, forcePopup);

                Assert.AreEqual("https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=CODE&scope=snsapi_userinfo&state=STATE&forcePopup=true#wechat_redirect", url);

                forcePopup = false;
                url = OAuthApi.GetAuthorizeUrl(appId, redirectUrl, state, scope, code, addConnectRedirect, forcePopup);

                Assert.AreEqual("https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=CODE&scope=snsapi_userinfo&state=STATE&forcePopup=false#wechat_redirect", url);
            }

            //存在 forcePopup 和 addConnectRedirect
            {
                var appId = "APPID";
                var redirectUrl = "REDIRECT_URI";
                var scope = OAuthScope.snsapi_userinfo;
                var code = "CODE";
                var state = "STATE";
                var forcePopup = (bool?)null;
                var addConnectRedirect = true;

                var url = OAuthApi.GetAuthorizeUrl(appId, redirectUrl, state, scope, code, addConnectRedirect, forcePopup);

                Assert.AreEqual("https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=CODE&scope=snsapi_userinfo&state=STATE&connect_redirect=1#wechat_redirect", url);
            }

            //同时存在 addConnectRedirect
            {
                var appId = "APPID";
                var redirectUrl = "REDIRECT_URI";
                var scope = OAuthScope.snsapi_userinfo;
                var code = "CODE";
                var state = "STATE";
                var forcePopup = true;
                var addConnectRedirect = true;

                var url = OAuthApi.GetAuthorizeUrl(appId, redirectUrl, state, scope, code, addConnectRedirect, forcePopup);

                Assert.AreEqual("https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=CODE&scope=snsapi_userinfo&state=STATE&forcePopup=true&connect_redirect=1#wechat_redirect", url);
            }
        }
    }
}