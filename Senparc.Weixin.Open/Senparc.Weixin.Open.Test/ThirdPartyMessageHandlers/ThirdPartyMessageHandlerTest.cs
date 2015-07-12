using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Open.Entities.Request;
using Senparc.Weixin.Open.MessageHandlers;

namespace Senparc.Weixin.Open.Test.ThirdPartyMessageHandlers
{
    public class CustomerMessageHandler : ThirdPartyMessageHandler
    {
        public CustomerMessageHandler(XDocument ecryptRequestDocument, PostModel postModel = null)
            : base(ecryptRequestDocument, postModel)
        {
        }
    }

    [TestClass]
    public class ThirdPartyMessageHandlerTest
    {
        private string requestXML = @"<xml>
	<ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName>
	<Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt>
</xml>";

        string sToken = "QDG6eK";
        string sAppID = "wx5823bf96d3bd56c7";
        string sEncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";

        string sReqMsgSig = "477715d11cdb4164915debcba66cb864d751f3e6";
        string sReqTimeStamp = "1409659813";
        string sReqNonce = "1372623149";

        [TestMethod]
        public void CustomerMessageHandlerTest()
        {
            //string sReqData = "<xml><ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName><Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt></xml>";

            var postModel = new PostModel()
            {
                AppId = sAppID,
                Msg_Signature = sReqMsgSig,
                //Signature = sReqMsgSig,
                Timestamp = sReqTimeStamp,
                Nonce = sReqNonce,

                Token = sToken,
                EncodingAESKey = sEncodingAESKey
            };
            var messageHandler = new CustomerMessageHandler(XDocument.Parse(requestXML), postModel);
            messageHandler.Execute();

            //TestMessageHandlers中没有处理坐标信息的重写方法，将返回默认消息


            Assert.IsInstanceOfType(messageHandler.ResponseMessageText, typeof(String));
            Assert.AreEqual("success。", messageHandler.ResponseMessageText);

        }
    }
}
