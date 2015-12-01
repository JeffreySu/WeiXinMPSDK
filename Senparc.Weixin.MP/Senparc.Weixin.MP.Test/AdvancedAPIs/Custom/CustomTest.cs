using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
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
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = CustomApi.SendText(accessToken, openId, "来自平台的回复<>&\n换行了");
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void SendImageTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = CustomApi.SendImage(accessToken, openId, "10001037");
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void SendVoiceTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

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
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

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
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

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
    }
}
