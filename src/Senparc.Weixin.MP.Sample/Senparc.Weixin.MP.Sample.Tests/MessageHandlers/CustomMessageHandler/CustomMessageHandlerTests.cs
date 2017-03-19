using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler.Tests
{
    [TestClass()]
    public class CustomMessageHandlerTests
    {
        [TestMethod()]
        public void MassageAgentTest()
        {

            var requestMessage = new RequestMessageText()
            {
                Content = "代理",
                CreateTime = DateTime.Now,
                FromUserName = "FromeUserName",
                ToUserName = "ToUserName",
                MsgId = 123,
            };

            var messageHandler = new CustomMessageHandler(requestMessage);
            messageHandler.Execute();

            Assert.IsInstanceOfType(messageHandler.ResponseMessage, typeof(ResponseMessageText));

            Console.WriteLine(((ResponseMessageText)messageHandler.ResponseMessage).Content);

            Assert.IsTrue(((ResponseMessageText)messageHandler.ResponseMessage).Content.Contains("代理"));
        }


    }
}