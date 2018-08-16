using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.WeixinTests.Entities
{
    [TestClass]
    public class SenparcWeixinSettingTests
    {
        [TestMethod]
        public void InitTest()
        {
            var senparcWeixinSetting = new SenparcWeixinSetting(true);
            Assert.AreEqual(true, senparcWeixinSetting.IsDebug);

            var defaultItem = senparcWeixinSetting.Items["Default"];
            Assert.IsNotNull(defaultItem);

            var appId = Guid.NewGuid().ToString("n");
            var secret = Guid.NewGuid().ToString("n");
            senparcWeixinSetting.WeixinAppId = appId;
            senparcWeixinSetting.WeixinAppSecret = secret;

            //defaultItem 和 SenparcWeixinSetting 的基类是同一个对象
            Assert.AreEqual(appId, defaultItem.WeixinAppId);
            Assert.AreEqual(secret, defaultItem.WeixinAppSecret);

            //添加新项目
            var newItem = senparcWeixinSetting.Items["New"];
            var newAppId = "Senparc" + Guid.NewGuid().ToString("n");
            newItem.WeixinAppId = newAppId;
            Assert.AreEqual(newAppId, senparcWeixinSetting.Items["New"].WeixinAppId);

        }
    }
}
