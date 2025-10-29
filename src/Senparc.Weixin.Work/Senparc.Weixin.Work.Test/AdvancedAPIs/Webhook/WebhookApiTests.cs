using Senparc.Weixin.Work.AdvancedAPIs.Webhook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;
using Senparc.WeixinTests;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Webhook.Tests
{
    [TestClass()]
    public class WebhookApiTests : BaseTest
    {

        string key = "<Your Key>";

        [TestMethod()]
        public async Task SendTextAsyncTest()
        {
            try
            {
                var result = await Work.AdvancedAPIs.Webhook.WebhookApi.SendTextAsync(key, "测试消息");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task Markdown2Test()
        {
            var result = await Work.AdvancedAPIs.Webhook.WebhookApi.SendMarkdownV2Async(key, "## MarkdownV2 接口测试消息 \n#### 1. 这是一条测试消息 \n > 这是一条测试消息");
            Console.WriteLine(result);
        }
    }
}
