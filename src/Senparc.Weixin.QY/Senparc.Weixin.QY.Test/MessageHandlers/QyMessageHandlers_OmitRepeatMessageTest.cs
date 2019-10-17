﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
                                    Msg_Signature = "",
                                    Timestamp = "",
                                    Nonce = "",

                                    Token = "",
                                    EncodingAESKey = "",
                                    CorpId = ""
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
