#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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


using Senparc.Weixin.Work.MessageContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Work.Entities;
using Senparc.NeuChar.Exceptions;
using System;

namespace Senparc.Weixin.Work.Test
{
    [TestClass]
    public class WorkBotMessageContextTest
    {
        [TestMethod]
        public void GetRequestEntityMappingResultTest()
        {
            // 测试文本消息
            var messageContext = new WorkBotMessageContext();
            var requestMessageType1 = RequestMsgType.Text;
            var requestMessage = messageContext.GetRequestEntityMappingResult(requestMessageType1, null);
            Assert.IsNotNull(requestMessage);
            Assert.IsInstanceOfType(requestMessage, typeof(BotRequestMessageText));

            // 测试事件消息
            var requestMessageType2 = RequestMsgType.Event;
            var doc = XDocument.Parse(@"<xml>
  <msgid><![CDATA[CAIQ16HMjQYY/NGagIOAgAMgq4KM0AI=]]></msgid>
  <create_time>1700000000</create_time>
  <aibotid><![CDATA[AIBOTID]]></aibotid>
  <from>
    <corpid><![CDATA[wpxxxx]]></corpid>
    <userid><![CDATA[USERID]]></userid>
  </from>
  <msgtype><![CDATA[event]]></msgtype>
  <event>
    <eventtype><![CDATA[enter_chat]]></eventtype>
  </event>
</xml>");
            var requestMessage2 = messageContext.GetRequestEntityMappingResult(requestMessageType2, doc);
            Assert.IsNotNull(requestMessage2);
            Assert.IsInstanceOfType(requestMessage2, typeof(BotRequestMessageEvent_Enter));
        }

        [TestMethod]
        public void GetResponseEntityMappingResultTest()
        {
            // 测试文本消息
            var messageContext = new WorkBotMessageContext();
            var responseMessageType1 = ResponseMsgType.Text;
            var responseMessage = messageContext.GetResponseEntityMappingResult(responseMessageType1, null);
            Assert.IsNotNull(responseMessage);
            Assert.IsInstanceOfType(responseMessage, typeof(WorkBotResponseMessageText));

            // 测试模板卡片消息
            var responseMessageType2 = ResponseMsgType.Unknown;
            var doc = XDocument.Parse(@"<xml>
  <msgtype><![CDATA[template_card]]></msgtype>
  <template_card>
    <card_type><![CDATA[multiple_interaction]]></card_type>
    <source>
      <icon_url><![CDATA[https://wework.qpic.cn/wwpic/252813_jOfDHtcISzuodLa_1629280209/0]]></icon_url>
      <desc><![CDATA[企业微信]]></desc>
    </source>
    <main_title>
      <title><![CDATA[欢迎使用企业微信]]></title>
      <desc><![CDATA[您的好友正在邀请您加入企业微信]]></desc>
    </main_title>
    <select_list>
      <select_item>
        <question_key><![CDATA[question_key_one]]></question_key>
        <title><![CDATA[选择标签1]]></title>
        <disable>false</disable>
        <selected_id><![CDATA[id_one]]></selected_id>
        <option_list>
          <option>
            <id><![CDATA[id_one]]></id>
            <text><![CDATA[选择器选项1]]></text>
          </option>
          <option>
            <id><![CDATA[id_two]]></id>
            <text><![CDATA[选择器选项2]]></text>
          </option>
        </option_list>
      </select_item>
      <select_item>
        <question_key><![CDATA[question_key_two]]></question_key>
        <title><![CDATA[选择标签2]]></title>
        <selected_id><![CDATA[id_three]]></selected_id>
        <option_list>
          <option>
            <id><![CDATA[id_three]]></id>
            <text><![CDATA[选择器选项3]]></text>
          </option>
          <option>
            <id><![CDATA[id_four]]></id>
            <text><![CDATA[选择器选项4]]></text>
          </option>
        </option_list>
      </select_item>
    </select_list>
    <submit_button>
      <text><![CDATA[提交]]></text>
      <key><![CDATA[submit_key]]></key>
    </submit_button>
    <task_id><![CDATA[task_id]]></task_id>
  </template_card>
</xml>");
            var responseMessage2 = messageContext.GetResponseEntityMappingResult(responseMessageType2, doc);
            Assert.IsNotNull(responseMessage2);
            Assert.IsInstanceOfType(responseMessage2, typeof(WorkBotResponseMessageTemplateCard));
        }
    }
}