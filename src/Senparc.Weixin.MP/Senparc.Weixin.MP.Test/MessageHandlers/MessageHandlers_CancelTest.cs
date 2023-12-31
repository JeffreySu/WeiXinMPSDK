#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.WeixinTests;

namespace Senparc.Weixin.MP.Test.MessageHandlers
{
    public class CancelMessageHandlers : MessageHandler<MessageContexts.DefaultMpMessageContext>
    {
        public string RunStep { get; set; }

        public CancelMessageHandlers(XDocument requestDoc, PostModel postModel)
            : base(requestDoc, postModel)
        {
        }

        public override async Task OnExecutingAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("1");

            RunStep = "OnExecuting";
            CancelExecute = true;//取消执行
            await base.OnExecutingAsync(cancellationToken);
            Console.WriteLine("2");

        }

        public override async Task OnExecutedAsync(CancellationToken cancellationToken)
        {
            RunStep = "OnExecuted";
            await base.OnExecutedAsync(cancellationToken);
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            RunStep = "Execute";
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "文字信息";
            return responseMessage;
        }

        public override async Task<IResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
        {
            RunStep = "Execute";
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "文字信息-来自异步方法";
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
    public class MessageHandlers_CancelTest:BaseTest
    {
        string xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>{1}</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[TNT2]]></Content>
    <MsgId>{0}</MsgId>
</xml>
";

        [TestMethod]
        public async Task CancelTest()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var postModel = new PostModel() { AppId = "appId" };
            {
                //一开始就取消
                var messageHandler = new CancelMessageHandlers(XDocument.Parse(xmlText.FormatWith(SystemTime.NowTicks,DateTimeHelper.GetUnixDateTime(SystemTime.Now))), postModel);
                messageHandler.CancelExecute = true;

                //缺少异步方法重写的时候，使用同步方法
                messageHandler.DefaultMessageHandlerAsyncEvent = NeuChar.MessageHandlers.DefaultMessageHandlerAsyncEvent.SelfSynicMethod;
                await messageHandler.ExecuteAsync(cancellationToken);

                Assert.AreEqual(null, messageHandler.RunStep);
            }

            {
                //OnExecuting中途取消
                var messageHandler = new CancelMessageHandlers(XDocument.Parse(xmlText.FormatWith(SystemTime.NowTicks, DateTimeHelper.GetUnixDateTime(SystemTime.Now))), postModel);
                await messageHandler.ExecuteAsync(cancellationToken);

                Assert.AreEqual("OnExecuting", messageHandler.RunStep);
                Assert.IsNotNull(messageHandler.RequestDocument);
                Console.WriteLine(messageHandler.RequestDocument.ToString());
                Console.WriteLine(messageHandler.RequestMessage.ToJson(true));
                Assert.IsNull(messageHandler.ResponseMessage);
            }
        }
    }
}
