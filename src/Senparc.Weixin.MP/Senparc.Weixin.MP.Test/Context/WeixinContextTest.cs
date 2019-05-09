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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Test.MessageHandlers;

namespace Senparc.Weixin.MP.Test.Context
{
    [TestClass]
    public class WeixinContextTest
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
        public void LimitRecordCountTest()
        {
            var doc = XDocument.Parse(xmlText);
            var maxRecordCount = 1;
            for (int i = 0; i < 100; i++)
            {
                var messageHandler = new CustomMessageHandlers(doc,null,maxRecordCount);
                messageHandler.Execute();
            }
            var weixinContext = MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>.GlobalWeixinContext.MessageQueue.FirstOrDefault();
            var recordCount = MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>.GlobalWeixinContext.MaxRecordCount;

            Assert.IsNotNull(weixinContext);
            Assert.AreEqual(recordCount, weixinContext.RequestMessages.Count);
            Assert.AreEqual(recordCount, weixinContext.ResponseMessages.Count);
        }
    }
}
