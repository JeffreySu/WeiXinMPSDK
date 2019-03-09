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
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar.Agents;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Test.Agents
{
    [TestClass]
    public class MessageAgentTest
    {
        [TestMethod]
        public void RequestXmlTest()
        {
            var url = "https://sdk.weixin.senparc.com/weixin"; //可以换成你自己的地址
            var token = "weixin"; //替换成自己的Token

            var requestXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
<CreateTime>1384322309</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[CLICK]]></Event>
<EventKey><![CDATA[OneClick]]></EventKey>
</xml>";

            var xml = MessageAgent.RequestXml(null, url, token, requestXml);
            var responseMessage = ResponseMessageBase.CreateFromResponseXml(xml, MpMessageEntityEnlightener.Instance);
            Assert.IsNotNull(responseMessage);
            Assert.IsInstanceOfType(responseMessage, typeof(ResponseMessageText));
            var strongResponseMessage = responseMessage as ResponseMessageText;
            Assert.IsTrue(strongResponseMessage.Content.Contains("您点击了底部按钮。"));

            Console.Write(strongResponseMessage.Content);
        }

        [TestMethod]
        public void CheckUrlAndTokenTest()
        {
            var url = "https://sdk.weixin.senparc.com/weixin";
            var token = "weixin";
            var result = MessageAgent.CheckUrlAndToken(url, token);
            Assert.IsTrue(result);

            token = "wrong_token";
            result = MessageAgent.CheckUrlAndToken(url, token);
            Assert.IsFalse(false);

            url = "wrong_url";
            token = "weixin";
            result = MessageAgent.CheckUrlAndToken(url, token);
            Assert.IsFalse(false);
        }
    }
}
