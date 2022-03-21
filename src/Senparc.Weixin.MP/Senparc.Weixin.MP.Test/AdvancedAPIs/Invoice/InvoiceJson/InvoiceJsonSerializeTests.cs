using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.WeixinTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.MP.Test.NetCore3.AdvancedAPIs.Invoice.InvoiceJson
{
    /// <summary>
    /// 序列化测试
    /// </summary>
    [TestClass]
    public class InvoiceJsonSerializeTests : BaseTest
    {
        [TestMethod]
        public void InvoiceJsonSerializeTest()
        {
            var data = new Senparc.Weixin.MP.AdvancedAPIs.GetAuthUrlData()
            {
                s_pappid = "appid",
                order_id = "1234",
                money = 11,
                timestamp = 1474875876,
                source = SourceType.web.ToString(),
                redirect_url = "https://mp.weixin.qq.com",
                type = AuthType.填写字段开票授权,
                ticket = "ticket"
            };

            var jsonStr = Senparc.CO2NET.Helpers.SerializerHelper.GetJsonString(data);
            Console.WriteLine(jsonStr);
            Assert.IsTrue(jsonStr.Contains("web"));
        }

    }
}
