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
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public class OmitRepeatMessageMessageHandlers : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public string RunStep { get; set; }


        public OmitRepeatMessageMessageHandlers(XDocument requestDoc,PostModel postModel)
            : base(requestDoc, postModel)
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
    public class MessageHandlers_OmitRepeatMessage
    {
        string xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>1357986928</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[{1}]]></Content>
    <MsgId>{0}</MsgId>
</xml>
";

        private PostModel _postModel = new PostModel() { AppId = "appId" };


        [TestMethod]
        public void OmitMessageTest_DifferentMsgId()
        {
            //发送两条不同MsgId的消息
            var messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "11", "Jeffrey")), _postModel);
            messageHandler.OmitRepeatedMessage = true;
            messageHandler.Execute();
            Assert.IsNotNull(messageHandler.ResponseMessage);
            Assert.AreEqual("Jeffrey", (messageHandler.ResponseMessage as ResponseMessageText).Content);

            messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "12", "Su")), _postModel);
            messageHandler.OmitRepeatedMessage = true;
            messageHandler.Execute();
            Assert.IsNotNull(messageHandler.ResponseMessage);
            Assert.AreEqual("Su", (messageHandler.ResponseMessage as ResponseMessageText).Content);
        }

        [TestMethod]
        public void OmitMessageTest_SameMsgId()
        {
            //发送两条相同MsgId的消息
            var messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "21", "Jeffrey")), _postModel);
            messageHandler.OmitRepeatedMessage = true;
            messageHandler.Execute();
            Assert.IsNotNull(messageHandler.ResponseMessage);
            Assert.AreEqual("Jeffrey", (messageHandler.ResponseMessage as ResponseMessageText).Content);

            messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "21", "Su")), _postModel);
            messageHandler.OmitRepeatedMessage = true;
            messageHandler.Execute();
            Assert.IsNull(messageHandler.ResponseMessage);

        }

        //不使用去重
        [TestMethod]
        public void OmitMessageTest_NotOmit()
        {
            //发送两条相同MsgId的消息，但是不启用忽略
            var messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "31", "Jeffrey")), _postModel);
            messageHandler.OmitRepeatedMessage = false;
            messageHandler.Execute();
            Assert.IsNotNull(messageHandler.ResponseMessage);
            Assert.AreEqual("Jeffrey", (messageHandler.ResponseMessage as ResponseMessageText).Content);

            messageHandler = new OmitRepeatMessageMessageHandlers(XDocument.Parse(string.Format(xmlText, "31", "Su")), _postModel);
            messageHandler.OmitRepeatedMessage = false;
            messageHandler.Execute();
            Assert.IsNotNull(messageHandler.ResponseMessage);
            Assert.AreEqual("Su", (messageHandler.ResponseMessage as ResponseMessageText).Content);

        }
    }
}
