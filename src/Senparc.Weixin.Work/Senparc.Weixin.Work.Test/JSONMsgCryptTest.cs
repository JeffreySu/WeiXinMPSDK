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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.Tencent;
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Helpers;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Extensions;


//本测试代码用于测试企业微信的加解密算法，同时用于生成一个加密的请求消息示例，以方便其他开发人员使用。
namespace Senparc.Weixin.Work.Test
{
    [TestClass]
    public class JSONMsgCryptTest
    {
        public string sToken = "QDG6eK";
        public string sEncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";

        string sReqTimeStamp = "1409659813";
        string sReqNonce = "1372623149";

        public BotRequestMessageText botRequestMessageText = new BotRequestMessageText();

        [TestMethod]
        public void TestMethod()
        {
            ////加解密库要求传 receiveid 参数，企业自建智能机器人的使用场景里，receiveid直接传空字符串即可
            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(sToken, sEncodingAESKey, "");

            //模拟一个请求消息
            botRequestMessageText.text = new BotRequestMessageText.Text();
            botRequestMessageText.text.content = "test";
            botRequestMessageText.from = new BotRequestMessageText.From();
            botRequestMessageText.from.userid = "test";
            botRequestMessageText.aibotid = "12345";
            botRequestMessageText.msgid = "1234567890123456";
            botRequestMessageText.chatid = "1234567890123456";

            var json = botRequestMessageText.ToJson(true);
            

            //加密出来的密文
            string sEncryptMsg = "";

            var encryptMsg = wxcpt.EncryptJsonMsg(json, sReqTimeStamp, sReqNonce, ref sEncryptMsg);

            System.Console.WriteLine(sEncryptMsg);

            /*
            这是加密过后的得到的密文，是请求消息不是回复消息，接下来测试请求消息的解密
             {"encrypt":"6RS0Ko1VsJN/WaFfxqhNGUtb8r2UxTNONDi2rnT8wc4ArAKL3DATSe52RWiPtCpMyIFp9wpXdjhNCPcc6TD8n31lGWsLSZeLt0qq8f+XoRDvVkp6Ma+SZjZsFrq8Tays8ugQ0O1XYJXBdHm4RIkKu2uYgXd+rYwwqi6KiXSh+YX2UJRESFyP9mflQf7HeKLK7eRjIyYUNDASVD3c6JKzGXtzC3G9/Gxjiez9r39XvlaJJzQmsqjSraC9o7Io1ny9E6G8pY5udbdiWyB4MFbrPXJ5tEEiHaIK2WYn7ZqxvRLvuvK/pMoLfDw6O1E2+NZCbkfyzEvWLM4mZnwhzj5Hhg==","msgsignature":"f490df6179b86bcaf6fbed0bf32166a477ca6c86","timestamp":"1409659813","nonce":"1372623149"}
            */

            string sig = "f490df6179b86bcaf6fbed0bf32166a477ca6c86";

            //解密出来的原文
            string sMsg = "";

            string encrypt = """
            {"encrypt":"6RS0Ko1VsJN/WaFfxqhNGUtb8r2UxTNONDi2rnT8wc4ArAKL3DATSe52RWiPtCpMyIFp9wpXdjhNCPcc6TD8n31lGWsLSZeLt0qq8f+XoRDvVkp6Ma+SZjZsFrq8Tays8ugQ0O1XYJXBdHm4RIkKu2uYgXd+rYwwqi6KiXSh+YX2UJRESFyP9mflQf7HeKLK7eRjIyYUNDASVD3c6JKzGXtzC3G9/Gxjiez9r39XvlaJJzQmsqjSraC9o7Io1ny9E6G8pY5udbdiWyB4MFbrPXJ5tEEiHaIK2WYn7ZqxvRLvuvK/pMoLfDw6O1E2+NZCbkfyzEvWLM4mZnwhzj5Hhg=="}
            """;

            //解密
            var decryptMsg = wxcpt.DecryptJsonMsg(sig, sReqTimeStamp, sReqNonce, encrypt, ref sMsg);
            System.Console.WriteLine(sMsg);

            //原文
            string originalMsg = """
        {
          "msgtype": "text",
          "text": {
            "content": "test"
          },
          "msgid": "1234567890123456",
          "aibotid": "12345",
          "chatid": "1234567890123456",
          "chattype": null,
          "from": {
            "userid": "test"
          }
        }
        """;
            Assert.AreEqual(originalMsg, sMsg);
        }
    }
}