using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{

    //已测试通过
    [TestClass]
    public class JsApiTicketContainerTest : CommonApiTest
    {
        //v13.3.0之后，JsApiTicketContainer已经合并入AccessTokenContainer
        [TestMethod]
        public void ContainerTest()
        {
            //注册
            AccessTokenContainer.Register(base._appId, base._appSecret);

            //获取Ticket完整结果（包括当前过期秒数）
            var ticketResult = JsApiTicketContainer.GetTicket(base._appId);
            Assert.IsNotNull(ticketResult);

            //只获取Ticket字符串
            var ticket = JsApiTicketContainer.GetTicket(base._appId);
            Assert.AreEqual(ticketResult, ticket);
            Console.WriteLine(ticket);

            //getNewTicket
            {
                ticket = JsApiTicketContainer.TryGetTicket(base._appId, base._appSecret);
                Assert.AreEqual(ticketResult, ticket);

                ticket = JsApiTicketContainer.TryGetTicket(base._appId, base._appSecret);
                //Assert.AreNotEqual(ticketResult.ticket, ticket);//如果微信服务器缓存，此处会相同

                Console.WriteLine(ticket);
            }

        }
    }
}
