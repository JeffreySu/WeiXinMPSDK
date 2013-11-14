using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    //[TestClass]
    public class CustomTest : CommonApiTest
    {
        private string openId = "omOTCt0E8gm6J2Fg0ArAaPS3_os8";

        [TestMethod]
        public void SendTextTest()
        {
            LoadToken();

            var result = Custom.SendText(base.tokenResult.access_token, openId, "来自平台的回复");
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void SendImageTest()
        {
            LoadToken();

            var result = Custom.SendImage(base.tokenResult.access_token, openId, "10001037");
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void SendVoiceTest()
        {
            LoadToken();

            try
            {
                var result = Custom.SendVoice(base.tokenResult.access_token, openId, "1000018");
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
            LoadToken();

            try
            {
                var result = Custom.SendVideo(base.tokenResult.access_token, openId, "1000018", "1000012");
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
            LoadToken();

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

            var result = Custom.SendNews(base.tokenResult.access_token, openId, articles);
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }
    }
}
