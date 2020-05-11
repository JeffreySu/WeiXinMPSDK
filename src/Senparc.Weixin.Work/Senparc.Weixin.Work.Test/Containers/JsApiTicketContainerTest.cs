#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

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

        [TestMethod]
        public void RegisterToWeixinSettingTest()
        {
            var corpId = Guid.NewGuid().ToString("n");
            var corpSecret = Guid.NewGuid().ToString("n");
            var name = "企业微信单元测试-JsApiTicketContainer";
            AccessTokenContainer.Register(corpId, corpSecret, name);

            Assert.AreEqual(corpId, Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinCorpId);
            Assert.AreEqual(corpSecret, Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinCorpSecret);
        }

    }
}
