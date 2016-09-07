using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Context;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.MessageHandlers;

namespace Senparc.Weixin.QY.Test.MessageHandlers
{
    public class OmitRepeatMessageMessageHandlers : QyMessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public string RunStep { get; set; }

        public OmitRepeatMessageMessageHandlers(XDocument requestDoc)
            : base(requestDoc, new PostModel()
                                {
                                    Msg_Signature = "845997ceb6e4fd73edd9a377be227848ce20d34f",
                                    Timestamp = "1412587525",
                                    Nonce = "1501543730",

                                    Token = "fzBsmSaI8XE1OwBh",
                                    EncodingAESKey = "9J8CQ7iF9mLtQDZrUM1loOVQ6oNDxVtBi1DBU2oaewl",
                                    CorpId = "wx7618c0a6d9358622"
                                })
        {
        }

        public override void OnExecuting()
        {
            base.OnExecuting();
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = requestMessage.Content;
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
            responseMessage.Content = "您发送的消息类型暂未被识别。";
            return responseMessage;
        }
    }

    [TestClass]
    public class QyMessageHandlers_OmitRepeatMessageTest
    {
        private string xmlText = @"<xml><ToUserName><![CDATA[wx7618c0a6d9358622]]></ToUserName>
<Encrypt><![CDATA[h3z+AK9zKP4dYs8j1FmthAILbJghEmdo2Y1U9Pdghzann6H2KJOpepaDT1zcp09/1/e/6ta48aUXebkHlu0rhzk4GW+cvVUHzbEiQVFlIvD+q4T/NLIm8E8BM+gO+DHslM7aXmYjvgMw6AYiBx80D+nZKNyJD3I8lRT3aHCq/hez0c+HTAnZyuCi5TfUAw0c6jWSfAq61VesRw4lhV925vJUOBXT/zOw760CEsYXSr2IAr/n4aPfDgRs2Ww2h/HPiVOQ2Ms1f/BOtFiKVWMqZCxbmJ7cyPHH7+uOSAS6DtXiQAdwpEZwHz+A5QTsmK6V0C6Ifgr7zrStb7ygM7kmcrAJctPhCfG7WlfrWrFNLdtx9Q2F7d6/soinswdoYF8g56s8UWguOVkM7UFGr8H2QqrUJm5S5iFP/XNcBwvPWYA=]]></Encrypt>
<AgentID><![CDATA[2]]></AgentID>
</xml>";

        [TestMethod]
        public void OmitMessageTest()
        {
            {
                //发送两条响同MsgId的消息
                var messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(xmlText));
                messageHandler.OmitRepeatedMessage = true;
                messageHandler.Execute();
                Assert.IsNotNull(messageHandler.ResponseMessage);
                Assert.AreEqual("末", (messageHandler.ResponseMessage as ResponseMessageText).Content);

                messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(xmlText));
                messageHandler.OmitRepeatedMessage = true;
                messageHandler.Execute();
                Assert.IsNull(messageHandler.ResponseMessage);
            }

            {
                //发送两条相同MsgId的消息，但是不启用忽略
                var messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "1", "Jeffrey")));
                messageHandler.OmitRepeatedMessage = false;
                messageHandler.Execute();
                Assert.IsNotNull(messageHandler.ResponseMessage);
                Assert.AreEqual("末", (messageHandler.ResponseMessage as ResponseMessageText).Content);

                messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "1", "Su")));
                messageHandler.OmitRepeatedMessage = false;
                messageHandler.Execute();
                Assert.IsNotNull(messageHandler.ResponseMessage);
                Assert.AreEqual("末", (messageHandler.ResponseMessage as ResponseMessageText).Content);

            }
        }
    }
}
