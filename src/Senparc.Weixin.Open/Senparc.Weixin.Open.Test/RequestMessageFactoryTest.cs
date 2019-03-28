using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.Open.Entities;
using Senparc.Weixin.Open.Helpers;

namespace Senparc.Weixin.Open.Test
{
    [TestClass]
    public class RequestMessageFactoryTest
    {
        string component_verify_ticketText = @"<xml>
<AppId>1</AppId>
<CreateTime>1413192605</CreateTime>
<InfoType>component_verify_ticket</InfoType>
<ComponentVerifyTicket>Senparc</ComponentVerifyTicket>
</xml>
";

        private string unauthorizedText = @"<xml>
<AppId>1</AppId>
<CreateTime>1413192605</CreateTime>
<InfoType>unauthorized</InfoType>
<AuthorizerAppid>211</AuthorizerAppid>
</xml>";

        [TestMethod]
        public void GetRequestEntityTest()
        {
            var dt = DateTimeHelper.BaseTime.AddTicks(((long)1413192605 + 8 * 60 * 60) * 10000000);
            {
                //component_verify_ticket
                var doc = XDocument.Parse(component_verify_ticketText);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsInstanceOfType(result, typeof(RequestMessageComponentVerifyTicket));
                Assert.AreEqual("1", result.AppId);
                Assert.AreEqual(dt, result.CreateTime);
                Assert.AreEqual("Senparc", (result as RequestMessageComponentVerifyTicket).ComponentVerifyTicket);
                Console.WriteLine(doc);
            }

            {
                //unauthorized
                var doc = XDocument.Parse(unauthorizedText);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsInstanceOfType(result, typeof(RequestMessageUnauthorized));
                Assert.AreEqual("1", result.AppId);
                Assert.AreEqual(dt, result.CreateTime);
                Assert.AreEqual("211", (result as RequestMessageUnauthorized).AuthorizerAppid);
                Console.WriteLine(doc);
            }
        }
    }
}
