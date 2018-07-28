#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.Containers
{

    //已测试通过
    [TestClass]
    public class JsApiTicketContainerTest : CommonApiTest
    {
        //v13.3.0之后，JsApiTicketContainer已经合并入AccessTokenContainer
        //v13.6.0之后，JsApiTicketContainer重新从AccessTokenContainer分离
        //v14.2.0之后，所有Container命名空间调整到Senparc.Weixin.MP.Containers下
        [TestMethod]
        public void ContainerTest()
        {
            //注册
            AccessTokenContainer.Register(base._appId, base._appSecret);

            //获取Ticket完整结果（包括当前过期秒数）
            var ticketResult = JsApiTicketContainer.GetJsApiTicketResult(base._appId);
            Assert.IsNotNull(ticketResult);

            //只获取Ticket字符串
            var ticket = JsApiTicketContainer.GetJsApiTicket(base._appId);
            Assert.AreEqual(ticketResult.ticket, ticket);
            Console.WriteLine(ticket);

            //getNewTicket
            {
                ticket = JsApiTicketContainer.TryGetJsApiTicket(base._appId, base._appSecret, false);
                Assert.AreEqual(ticketResult.ticket, ticket);

                ticket = JsApiTicketContainer.TryGetJsApiTicket(base._appId, base._appSecret, true);
                //Assert.AreNotEqual(ticketResult.ticket, ticket);//如果微信服务器缓存，此处会相同

                Console.WriteLine(ticket);
            }

        }

        [TestMethod]
        public void RegisterToWeixinSettingTest()
        {
            var appId = Guid.NewGuid().ToString("n");
            var appSecret = Guid.NewGuid().ToString("n");
            var name = "公众号 JsApiTicketContainer 单元测试";
            JsApiTicketContainer.Register(appId, appSecret, name);

            Assert.AreEqual(appId, Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppId);
            Assert.AreEqual(appSecret, Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppSecret);
        }
    }
}
