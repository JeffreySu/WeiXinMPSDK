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

using Senparc.Weixin.Work.Helpers;
using Senparc.Weixin.Work.Entities;
using Senparc.NeuChar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.Work.Test
{
    [TestClass]
    public class BotEntityHelperTest
    {
        [TestMethod]
        public void TextRequestEntityTest()
        {
            var json = """
{
    "msgid": "CAIQ16HMjQYY\/NGagIOAgAMgq4KM0AI=",
    "aibotid": "AIBOTID",
    "chatid": "CHATID",
    "chattype": "group",
    "from": {
        "userid": "USERID"
    },
    "msgtype": "text",
    "text": {
        "content": "@RobotA hello robot"
    }
}
""";
            var entity = BotEntityHelper.GetRequestEntity(json);
            Assert.IsNotNull(entity);
            Assert.AreEqual(RequestMsgType.Text, entity.MsgType);
            Assert.AreEqual("USERID",entity.FromUserName);
            Assert.AreEqual("AIBOTID",entity.ToUserName);
            var entityAsText = entity as RequestMessageText;
            Assert.AreEqual("@RobotA hello robot",entityAsText.Content);

        }

        [TestMethod]
        public void TextResponseEntityTest()
        {
            var entity = new ResponseMessageText();
            entity.Content = "this is a test";
            var jsonString = BotEntityHelper.GetResponseMsgString(entity);
            Assert.IsNotNull(jsonString);
            System.Console.WriteLine(jsonString);
            Assert.IsTrue(jsonString.Contains("this is a test"));
        }

        
    }
}