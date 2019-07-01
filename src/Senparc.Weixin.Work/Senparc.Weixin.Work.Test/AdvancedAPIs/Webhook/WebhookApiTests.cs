using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.Work.Test.vs2017.AdvancedAPIs.Webhook
{
    [TestClass]
    public class WebhookApiTests
    {
        const string key = "[your key]";
        [TestMethod]
        public void SendTestTest()
        {
            try
            {
                Work.AdvancedAPIs.Webhook.WebhookApi.SendText(key, "测试消息");
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
