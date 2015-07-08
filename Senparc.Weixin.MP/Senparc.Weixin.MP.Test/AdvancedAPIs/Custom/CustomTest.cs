using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs.Custom;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    [TestClass]
    public class CustomTest : CommonApiTest
    {
        private string openId = "od16Wjj1-jMmbemHKgFFQzd7I43Q";

        [TestMethod]
        public void SendTextTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = CustomApi.SendText(accessToken, openId, "来自平台的回复<>&\n换行了");
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void SendImageTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = CustomApi.SendImage(accessToken, openId, "10001037");
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void SendVoiceTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            try
            {
                var result = CustomApi.SendVoice(accessToken, openId, "1000018");
                Assert.Fail();//因为这里写测试代码的时候，微信账号还没有权限，所以会抛出异常（故意的），如果是已经开通的应该是“请求成功”
            }
            catch (ErrorJsonResultException ex)
            {
                Assert.AreEqual(ReturnCode.api功能未授权, ex.JsonResult.errcode);
            }
        }

        [TestMethod]
        public void SendVideoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            try
            {
                var result = CustomApi.SendVideo(accessToken, openId, "1000018", "1000012","[description]");
                Assert.Fail();//因为这里写测试代码的时候，微信账号还没有权限，所以会抛出异常（故意的），如果是已经开通的应该是“请求成功”
            }
            catch (ErrorJsonResultException ex)
            {
                Assert.AreEqual(ReturnCode.api功能未授权, ex.JsonResult.errcode);
            }
        }

        [TestMethod]
        public void SendNewsTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var articles = new List<Article>();
            articles.Add(new Article()
            {
                Title = "测试标题",
                Description = "测试描述",
                Url = "http://weixin.senparc.com",
                PicUrl = "http://weixin.senparc.com/Images/qrcode.jpg"
            });
            articles.Add(new Article()
            {
                Title = "测试更多标题",
                Description = "测试更多描述",
                Url = "http://weixin.senparc.com",
                PicUrl = "http://weixin.senparc.com/Images/qrcode.jpg"
            });

            var result = CustomApi.SendNews(accessToken, openId, articles);
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void Test()
        {
            var accessToken = AccessTokenContainer.TryGetToken("wx4cd1fb1b02e91111", "a0a5255206f3ecb2d42abf1e26ea53e5");

            var openIdList = UserApi.Get(accessToken, null).data.openid;

            string openId = null;

            foreach (var item in openIdList)
            {
                var userInfo = UserApi.Info(accessToken, item);
                if (userInfo.nickname == "TYS")
                {
                    openId = userInfo.openid;
                    break;
                }
            }

            Assert.IsTrue(openId != null);
        }

        [TestMethod]
        public void SendTestMessageTest()
        {
            var accessToken = AccessTokenContainer.TryGetToken("wx669ef95216eef885", "c27e1cd2e6a5d697d767baaf5b91132f");

            var articles = new List<Article>();
            articles.Add(new Article()
            {
                Title = "测试标题",
                Description = "测试描述",
                Url = "http://weixin.senparc.com",
                PicUrl = "http://weixin.senparc.com/Images/qrcode.jpg"
            });

            var result = CustomApi.SendNews(accessToken, "olPjZjnPxdjAFYhnFp7qYLsWcmjQ", articles);
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }
    }
}
