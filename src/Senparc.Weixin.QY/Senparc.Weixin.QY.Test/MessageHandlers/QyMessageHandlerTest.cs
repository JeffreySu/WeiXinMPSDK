using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Context;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.MessageHandlers;

namespace Senparc.Weixin.QY.Test.MessageHandlers
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class QyMessageHandlersTest
    {
        public class CustomerMessageHandlers : QyMessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
        {
            public CustomerMessageHandlers(XDocument requestDoc, PostModel postModel, int maxRecordCount = 0)
                : base(requestDoc, postModel, maxRecordCount)
            {
            }

            public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
            {
                var responseMessage =
                   ResponseMessageBase.CreateFromRequestMessage(RequestMessage, ResponseMsgType.Text) as
                   ResponseMessageText;
                responseMessage.Content = "文字信息";
                return responseMessage;
            }


            /// <summary>
            /// 默认消息
            /// </summary>
            /// <param name="requestMessage"></param>
            /// <returns></returns>
            public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
            {
                var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = "这是一条默认消息。";
                return responseMessage;
            }
        }

        private string testXml = @"<xml><ToUserName><![CDATA[wx7618c0a6d9358622]]></ToUserName>
<Encrypt><![CDATA[h3z+AK9zKP4dYs8j1FmthAILbJghEmdo2Y1U9Pdghzann6H2KJOpepaDT1zcp09/1/e/6ta48aUXebkHlu0rhzk4GW+cvVUHzbEiQVFlIvD+q4T/NLIm8E8BM+gO+DHslM7aXmYjvgMw6AYiBx80D+nZKNyJD3I8lRT3aHCq/hez0c+HTAnZyuCi5TfUAw0c6jWSfAq61VesRw4lhV925vJUOBXT/zOw760CEsYXSr2IAr/n4aPfDgRs2Ww2h/HPiVOQ2Ms1f/BOtFiKVWMqZCxbmJ7cyPHH7+uOSAS6DtXiQAdwpEZwHz+A5QTsmK6V0C6Ifgr7zrStb7ygM7kmcrAJctPhCfG7WlfrWrFNLdtx9Q2F7d6/soinswdoYF8g56s8UWguOVkM7UFGr8H2QqrUJm5S5iFP/XNcBwvPWYA=]]></Encrypt>
<AgentID><![CDATA[2]]></AgentID>
</xml>";



        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void TextTest()
        {
            var postModel = new PostModel()
            {
                Msg_Signature = "845997ceb6e4fd73edd9a377be227848ce20d34f",
                Timestamp = "1412587525",
                Nonce = "1501543730",

                Token = "fzBsmSaI8XE1OwBh",
                EncodingAESKey = "9J8CQ7iF9mLtQDZrUM1loOVQ6oNDxVtBi1DBU2oaewl",
                CorpId = "wx7618c0a6d9358622"
            };
            var messageHandler = new CustomerMessageHandlers(XDocument.Parse(testXml), postModel, 10);
            Assert.IsNotNull(messageHandler.RequestDocument);
            Assert.IsNotNull(messageHandler.RequestMessage);
            Assert.IsNotNull(messageHandler.EncryptPostData);
            Assert.IsTrue(messageHandler.AgentId == 2);

            messageHandler.Execute();

            Assert.IsNotNull(messageHandler.ResponseDocument);
            Assert.IsNotNull(messageHandler.ResponseMessage);


            Console.WriteLine(messageHandler.RequestDocument);
        }
    }
}
