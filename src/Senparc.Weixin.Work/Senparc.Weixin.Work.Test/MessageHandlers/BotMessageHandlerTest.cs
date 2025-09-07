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
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Test.CommonApis;
using Senparc.Weixin.Work.Test.net6.MessageHandlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Test.MessageHandlers
{
    [TestClass]
    public class BotMessageHandlerTest : CommonApiTest
    {
        public string testJSON = """
        {"encrypt":"6RS0Ko1VsJN/WaFfxqhNGUtb8r2UxTNONDi2rnT8wc4ArAKL3DATSe52RWiPtCpMyIFp9wpXdjhNCPcc6TD8n31lGWsLSZeLt0qq8f+XoRDvVkp6Ma+SZjZsFrq8Tays8ugQ0O1XYJXBdHm4RIkKu2uYgXd+rYwwqi6KiXSh+YX2UJRESFyP9mflQf7HeKLK7eRjIyYUNDASVD3c6JKzGXtzC3G9/Gxjiez9r39XvlaJJzQmsqjSraC9o7Io1ny9E6G8pY5udbdiWyB4MFbrPXJ5tEEiHaIK2WYn7ZqxvRLvuvK/pMoLfDw6O1E2+NZCbkfyzEvWLM4mZnwhzj5Hhg=="}
        """;
        [TestMethod]
        public async Task TestMethod()
        {
            var postModel = new PostModel()
            {
                Msg_Signature = "f490df6179b86bcaf6fbed0bf32166a477ca6c86",
                Timestamp = "1409659813",
                Nonce = "1372623149",

                Token = "QDG6eK",
                EncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C",
            };
            var messageHandler = new BotCustomMessageHandler(testJSON, postModel, 10);

            System.Console.WriteLine(messageHandler.RequestJsonStr);
            Assert.IsNotNull(messageHandler.RequestJsonStr);
            Assert.IsNotNull(messageHandler.RequestMessage);
            Assert.IsNotNull(messageHandler.MessageEntityEnlightener);
            Assert.IsNotNull(messageHandler.RequestMessage.GetRepeatedBusiness);
            System.Console.WriteLine(messageHandler.MessageEntityEnlightener.PlatformType.ToString());

            await messageHandler.ExecuteAsync(new CancellationToken());

            System.Console.WriteLine(messageHandler.ResponseJsonStr);
            Assert.IsNotNull(messageHandler.ResponseJsonStr);
            Assert.IsNotNull(messageHandler.ResponseMessage);

            System.Console.WriteLine(messageHandler.FinalResponseJsonStr);
            Assert.IsNotNull(messageHandler.FinalResponseJsonStr);
        }
    }
}