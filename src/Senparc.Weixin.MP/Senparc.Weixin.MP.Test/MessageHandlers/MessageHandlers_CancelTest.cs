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
    public class CancelMessageHandlers : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public string RunStep { get; set; }

        public CancelMessageHandlers(XDocument requestDoc, PostModel postModel)
            : base(requestDoc, postModel)
        {
        }

        public override void OnExecuting()
        {
            RunStep = "OnExecuting";
            CancelExcute = true;//取消执行
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            RunStep = "OnExecuted";
            base.OnExecuted();
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            RunStep = "Execute";
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
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
            responseMessage.Content = "您发送的消息类型暂未被识别。";
            return responseMessage;
        }
    }

    [TestClass]
    public class MessageHandlers_CancelTest
    {
        string xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>1357986928</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[TNT2]]></Content>
    <MsgId>5832509444155992350</MsgId>
</xml>
";

        [TestMethod]
        public void CancelTest()
        {
            var postModel = new PostModel() { AppId = "appId" };
            {
                //一开始就取消
                var messageHandler = new CancelMessageHandlers(XDocument.Parse(xmlText), postModel);
                messageHandler.CancelExcute = true;
                messageHandler.Execute();

                Assert.AreEqual(null, messageHandler.RunStep);
            }

            {
                //OnExecuting中途取消
                var messageHandler = new CancelMessageHandlers(XDocument.Parse(xmlText), postModel);
                messageHandler.Execute();

                Assert.AreEqual("OnExecuting", messageHandler.RunStep);
            }
        }
    }
}
