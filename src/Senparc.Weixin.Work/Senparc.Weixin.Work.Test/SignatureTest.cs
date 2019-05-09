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
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.Work.Test
{
    [TestClass]
    public class SignatureTest
    {
        [TestMethod]
        public void GenarateSinatureTest()
        {
            var token = "Senparc";
            var timeStamp = "Weixin";
            var nonce = "QY";
            var msgEncrypt = "盛派网络";
            var result = Signature.GenarateSinature(token, timeStamp, nonce, msgEncrypt);

            Assert.IsNotNull(result);
            Assert.AreEqual("7f374c198863ee83d28532eb764dc0d6ecf03c66", result);
        }

        [TestMethod]
        public void VerifyURLTest()
        {
            //msg_signature=fee7db32f1010e0d8792f93465623f339181d84a&timestamp=1411122938&nonce=2059956920&echostr=rQxsOFvlBYlWwpdHb03s8mrQuN%2FCmp9h1paC0UsytfwfDoKOQlVnbeyl3O5WGYqTpsoeq8D08ASRcbzGUTYaOQ%3D%3D

            var token = "fzBsmSaI8XE1OwBh";
            var encodingAESKey = "9J8CQ7iF9mLtQDZrUM1loOVQ6oNDxVtBi1DBU2oaewl";
            var corpId = "wx7618c0a6d9358622";
            var msgSignature = "fee7db32f1010e0d8792f93465623f339181d84a";
            var timeStamp = "1411122938";
            var nonce = "2059956920";
            var echostr = "rQxsOFvlBYlWwpdHb03s8mrQuN/Cmp9h1paC0UsytfwfDoKOQlVnbeyl3O5WGYqTpsoeq8D08ASRcbzGUTYaOQ==";

            var replyEchoStr = Signature.VerifyURL(token, encodingAESKey, corpId, msgSignature, timeStamp, nonce, echostr);
            Assert.IsNotNull(replyEchoStr);
            Assert.AreEqual("2009497178386364159",replyEchoStr);
        }
    }
}

