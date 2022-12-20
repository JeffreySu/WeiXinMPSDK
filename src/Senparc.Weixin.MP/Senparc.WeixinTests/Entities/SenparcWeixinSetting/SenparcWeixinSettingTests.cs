using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers.Tests;

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

        [TestMethod]
        public void PrivateKeyTest()
        {
            var senparcWeixinSetting = new SenparcWeixinSetting(true);
            var privateKey = "~/apiclient_key.pem";
            senparcWeixinSetting.TenPayV3_PrivateKey = privateKey;
            Console.WriteLine(senparcWeixinSetting.TenPayV3_PrivateKey);
            Assert.IsTrue(senparcWeixinSetting.TenPayV3_PrivateKey.Length > 100);

            var exceptResult = TenPayHelperTests.EXCEPT_RESULT;

            Assert.AreEqual(exceptResult, senparcWeixinSetting.TenPayV3_PrivateKey);

            //测试 TenPayV3_PrivateKey 对象没有变化
            var tenPayV3PrivateKeyHashCode = senparcWeixinSetting.TenPayV3_PrivateKey.GetHashCode();
            var readKey = senparcWeixinSetting.TenPayV3_PrivateKey;
            var newTenPayV3PrivateKeyHashCode = senparcWeixinSetting.TenPayV3_PrivateKey.GetHashCode();
            Assert.AreEqual(tenPayV3PrivateKeyHashCode, newTenPayV3PrivateKeyHashCode);

        }
    }
}
