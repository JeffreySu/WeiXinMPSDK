using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Open.Entities.Request;
using Senparc.Weixin.Open.MessageHandlers;

namespace Senparc.Weixin.Open.Test.ThirdPartyMessageHandlers
{
    public class CustomMessageHandler : ThirdPartyMessageHandler
    {
        public CustomMessageHandler(XDocument ecryptRequestDocument, PostModel postModel = null)
            : base(ecryptRequestDocument, postModel)
        {
        }
    }

    [TestClass]
    public class ThirdPartyMessageHandlerTest
    {
        //TODO:以下XML不正确
        private string requestXML = @"<xml>
    <AppId><![CDATA[wxbbd3f07e2945cf2a]]></AppId>
    <Encrypt><![CDATA[7d7R2gPF37VXWzQoveo4Ts2xG4iuOS3gwJ3kbc56PBL40BY6uHNjvlBrvaw3LqS4Th3Z93DIOjs7Yhlv8YW72oh1ARDuR0aXUTySTXQ8CWJWVz7VNWruFT25hfGBOnPlNW1GJyV5l8LLoIIasaiWJbteqEfgCW7ehwL8F7kTNClFaiFrS7AzlK6DMR4gMBdL0b0mfpfp8bfEkPmEcy/ARQiQBTwXz/NhxBvHTFGtKIv3VYR+ZztzoV1wVc/nXBg/FhvW7fzQRsqbCBZ2gegRwfz5j10HPB5rfjaZjO4fvEsx8V2/rTICtcUxkMV1GO5cgbasw/FEIDCH2NAUl/ht0w==]]></Encrypt>
</xml>
";

        string sToken = "senparc";
        string sAppID = "wxbbd3f07e2945cf2a";
        string sEncodingAESKey = "0123456789012345678901234567890123456789012";

        string sReqMsgSig = "346f1ed0ed84e80342072f2b88e2f4f018b8c79d";
        string sReqTimeStamp = "1436876197";
        string sReqNonce = "1879066412";
        private string sReqSig = "6cd59ed9ca88d3993475bd4960052c5e5204391e";

        [TestMethod]
        public void UnAuthMessageHandlerTest()
        {
            var postModel = new PostModel()
            {
                AppId = sAppID,
                Msg_Signature = sReqMsgSig,
                Signature = sReqSig,
                Timestamp = sReqTimeStamp,
                Nonce = sReqNonce,

                Token = sToken,
                EncodingAESKey = sEncodingAESKey
            };
            var messageHandler = new CustomMessageHandler(XDocument.Parse(requestXML), postModel);
            messageHandler.Execute();

            //TestMessageHandlers中没有处理坐标信息的重写方法，将返回默认消息


            Assert.IsInstanceOfType(messageHandler.ResponseMessageText, typeof(String));
            Assert.IsInstanceOfType(messageHandler.RequestMessage, typeof(RequestMessageUnauthorized));
            Assert.AreEqual("success", messageHandler.ResponseMessageText);
            Console.WriteLine(messageHandler.RequestDocument.ToString());
        }

        [TestMethod]
        public void TicketMessageHandlerTest()
        {
            string xml = @"<xml>
    <AppId><![CDATA[wxbbd3f07e2945cf2a]]></AppId>
    <Encrypt><![CDATA[/RWeOUN4J839ynYH960Duj2k0WwvSfwq2dBFQ1NG78v9CZKbiCk5F8Fq/RGV0oFALoy+2L7KDe+1EbitZK7T4KtHMxntcEeQDxhWGdbeyTYJStcl7pudKA/ltfPP2nDbMrvmdl6JAX/XY4El0XEaYMrxegU3B1aUMZJ1GDjz9pIFv5+DPWj2mf7mhwIKcJSfeMctifS5UyJHKmIM+gnPQjtcKsVdnjaLckEScUsKKTygeG9IOtWFfE720W6g5UfOg3yxPFCUrqjWKRTue7g0yT0vQyb0L5cODKh1bCjIRcgikJsRVo3tAX1QO/CrHzKEEDqaD3Rx1hRkvRl/2KQlFd/DkhOzWp1LmpsCZQiNj0Fdc41aVleaFrwUQ8svg9Wt2iJLDhdqlz1Us2Pb6Ayx3dJLhXtb4ynJGsPFq0N8RrCuLgU391BpAiga5JBcjzS50pfvBBEEPQDyAE7znPphrA==]]></Encrypt>
</xml>";
            var postModel = new PostModel()
            {
                AppId = sAppID,
                Msg_Signature = "066523677e42a15c64a725ba0058cd7651174624",
                Signature = "60d1ddec039f51cc8c7ee4ea9ae9a479dd7c7d01",
                Timestamp = "1436881222",
                Nonce = "296318614",

                Token = sToken,
                EncodingAESKey = sEncodingAESKey
            };
            var messageHandler = new CustomMessageHandler(XDocument.Parse(xml), postModel);
            messageHandler.Execute();

            //TestMessageHandlers中没有处理坐标信息的重写方法，将返回默认消息


            Assert.IsInstanceOfType(messageHandler.ResponseMessageText, typeof(String));
            Assert.AreEqual("success", messageHandler.ResponseMessageText);
            Console.WriteLine(messageHandler.RequestDocument.ToString());
            Assert.IsInstanceOfType(messageHandler.RequestMessage, typeof(RequestMessageComponentVerifyTicket));
        }
    }
}
