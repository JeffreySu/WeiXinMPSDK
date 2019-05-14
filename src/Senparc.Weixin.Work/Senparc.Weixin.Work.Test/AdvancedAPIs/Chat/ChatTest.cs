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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Test.CommonApis;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    /// <summary>
    /// ChatTest
    /// </summary>
    [TestClass]
    public partial class ChatTest : CommonApiTest
    {
        [TestMethod]
        public void CreateChatTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var result = ChatApi.CreateChat(accessToken, "1", "测试", "005", new[] { "002", "005", "007" });

            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        [TestMethod]
        public void SendChatSimpleMessageTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var result = ChatApi.SendChatSimpleMessage(accessToken, "1", ChatMsgType.image, "1-GdQZtyp-8G8i-UwC0qh0yDQch5wqso5MTus37CLlu72PIyHEYUFvo9oHYRifvKo4hYgAc6GEA4qxP8tlJd2rA");
            ChatApi.SendChatSimpleMessage(accessToken, "1", ChatMsgType.text, "111");

            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        [TestMethod]
        public void QuitChatTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            var result = ChatApi.QuitChat(accessToken, "1", "007");
            
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }
    }
}
