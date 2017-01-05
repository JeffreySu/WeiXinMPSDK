using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Test.CommonApis;

namespace Senparc.Weixin.Work.Test.CommonAPIs
{
    //已测试通过
    [TestClass]
    public class JsApiTicketContainerTest : CommonApiTest
    {
        [TestMethod]
        public void ContainerTest()
        {
            //注册
            JsApiTicketContainer.Register(base._corpId, base._corpSecret);

            //获取Ticket完整结果（包括当前过期秒数）
            var ticketResult = JsApiTicketContainer.GetTicketResult(base._corpId,base._corpSecret);
            Assert.IsNotNull(ticketResult);
            Console.WriteLine(ticketResult.ticket);

            //只获取Ticket字符串
            var ticket = JsApiTicketContainer.GetTicket(base._corpId, base._corpSecret);
            Assert.AreEqual(ticketResult.ticket, ticket);

            //getNewTicket
            {
                ticket = JsApiTicketContainer.TryGetTicket(base._corpId, base._corpSecret, false);
                Assert.AreEqual(ticketResult.ticket, ticket);

                ticket = JsApiTicketContainer.TryGetTicket(base._corpId, base._corpSecret, true);
                Assert.AreEqual(ticketResult.ticket, ticket);//现在微信服务器有Ticket缓存，短时间内一致
                Console.WriteLine(ticketResult.ticket);
            }

        }
    }
}
