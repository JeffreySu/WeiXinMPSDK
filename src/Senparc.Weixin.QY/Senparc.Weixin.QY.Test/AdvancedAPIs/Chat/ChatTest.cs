using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Test.CommonApis;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
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
            var accessToken = AccessTokenContainer.GetToken(_corpId);

            var result = ChatApi.CreateChat(accessToken, "1", "测试", "005", new[] { "002", "005", "007" });

            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        [TestMethod]
        public void SendChatMessageTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);

            var result = ChatApi.SendChatMessage(accessToken, "005", Chat_Type.@single, ChatMsgType.image, "002", "1-GdQZtyp-8G8i-UwC0qh0yDQch5wqso5MTus37CLlu72PIyHEYUFvo9oHYRifvKo4hYgAc6GEA4qxP8tlJd2rA");
            ChatApi.SendChatMessage(accessToken, "005", Chat_Type.@group, ChatMsgType.text, "1", "111");

            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        [TestMethod]
        public void QuitChatTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);

            var result = ChatApi.QuitChat(accessToken, "1", "007");
            
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }
    }
}
