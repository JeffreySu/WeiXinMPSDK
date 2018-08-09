#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Senparc.Weixin.WxOpen.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.CO2NET.Helpers;

namespace Senparc.Weixin.WxOpen.Helpers.Tests
{
    [TestClass()]
    public class EncryptHelperTests
    {
        [TestMethod()]
        public void GetSignatureTest()
        {
            {
                //官方提供的案例
                var rawData =
                    "{\"nickName\":\"Band\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Guangzhou\",\"province\":\"Guangdong\",\"country\":\"CN\",\"avatarUrl\":\"http://wx.qlogo.cn/mmopen/vi_32/1vZvI39NWFQ9XM4LtQpFrQJ1xlgZxx3w7bQxKARol6503Iuswjjn6nIGBiaycAjAtpujxyzYsrztuuICqIM5ibXQ/0\"}";

                var sessionKey = "HyVFkGl5F5OQWJZZaNzBBg==";
                var compareSignature = "75e81ceda165f4ffa64f4068af58c64b8f54b88c";

                var signature = EncryptHelper.GetSignature(rawData, sessionKey);
                Assert.AreEqual(compareSignature, signature);

            }

            {
                //自定义
                var rawData =
        "{\"nickName\":\"苏震巍\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Suzhou\",\"province\":\"Jiangsu\",\"country\":\"CN\",\"avatarUrl\":\"http://wx.qlogo.cn/mmopen/vi_32/PiajxSqBRaEKXyjX4N6I5Vx1aeiaBeJ2iaTLy15n0HgvjNbWEpKA3ZbdgXkOhWK7OH8iar3iaLxsZia5Ha4DnRPlMerw/0\"}";

                var sessionKey = "jCdFs2HMx+A9Dr9lhMJKxA==";
                var compareSignature = "2d65ebea7c7f500bfb874b71569a591047452d38";

                var signature = EncryptHelper.GetSignature(rawData, sessionKey);
                Assert.AreEqual(compareSignature, signature);
            }
        }

        [TestMethod]
        public void CheckSignatureTest()
        {
            //储存Session


            var sessionId = "7f3f7489cb904d20bd4b5e9443f1bcab";
            var rawData = "{\"nickName\":\"苏震巍\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Suzhou\",\"province\":\"Jiangsu\",\"country\":\"CN\",\"avatarUrl\":\"http://wx.qlogo.cn/mmopen/vi_32/PiajxSqBRaEKXyjX4N6I5Vx1aeiaBeJ2iaTLy15n0HgvjNbWEpKA3ZbdgXkOhWK7OH8iar3iaLxsZia5Ha4DnRPlMerw/0\"}";
            var sessionKey = "lEIWEBVlmAj/Ng0t54iahA==";
            var unionId = "";

            SessionContainer.UpdateSession(sessionId, "openId", sessionKey, unionId);

            var sessionBag = SessionContainer.GetSession(sessionId);
            Assert.IsNotNull(sessionBag);
            Assert.AreEqual(sessionKey, sessionBag.SessionKey);

            var compareSignature = "1149a88c75125de3146040c90d7bcc4b2a564a34";
            var result = EncryptHelper.CheckSignature(sessionId, rawData, compareSignature);
            Assert.IsTrue(result);

        }

        [TestMethod()]
        public void DecodeEncryptedDataTest()
        {
            var sessionKey = "/mGmINZAe+7k6kNz32wxSw==";
            var encryptedData =
                "CFcsIXmH2r0v9ehjEhS+uUpJkr8qGQyt+Za3YkhjVNNA+xGj2WB2QFxDXdKVSzc10LukeB2maCxZCqpPQrWQx6CKF/VkEx96hXpPuBMpWBnnLzupoJpkRW9gJGRz7dcXDnqzstf2etRumDeAFDyjEKZ6bqs+KTE7qHauMsctxg4TXPbzzvWQm783j9PoWsCm/0A+aGNWCfZSFuJgi5G+LjTVqcGqP+mlAnLIFmgGLTo3vWrekz0//2vCMhgcgwKjPMR+VZTB7UItvnWfF4h4oOajcMuEiwTifaFkyn7l4NtLroMYjOfId16B6XCTK0BvPhTw9GI3wPMDopwWF2q3Op8M2fYWJuVGFKbrAZvVY/ILeIxYLaHuwHAOYULLre5Mg1kQpURlQ6I6e6GjraJUoL1BqsM38DayY5xRRFJsehZgrWkOySWICuN20Bte7+2N8D6PvhsaNyQz+4Lp4XY/Nn+clNGoM1v6aKTCv7PY2wo=";
            var iv = "ASJ0whjRyLK1tvgb7bAVSw==";

            //var sessionKey = "tiihtNczf5v6AKRyjwEUhQ==";
            //var encryptedData =
            //    "CiyLU1Aw2KjvrjMdj8YKliAjtP4gsMZMQmRzooG2xrDcvSnxIMXFufNstNGTyaGS9uT5geRa0W4oTOb1WT7fJlAC+oNPdbB+3hVbJSRgv+4lGOETKUQz6OYStslQ142dNCuabNPGBzlooOmB231qMM85d2/fV6ChevvXvQP8Hkue1poOFtnEtpyxVLW1zAo6/1Xx1COxFvrc2d7UL/lmHInNlxuacJXwu0fjpXfz/YqYzBIBzD6WUfTIF9GRHpOn/Hz7saL8xz+W//FRAUid1OksQaQx4CMs8LOddcQhULW4ucetDf96JcR3g0gfRK4PC7E/r7Z6xNrXd2UIeorGj5Ef7b1pJAYB6Y5anaHqZ9J6nKEBvB4DnNLIVWSgARns/8wR2SiRS7MNACwTyrGvt9ts8p12PKFdlqYTopNHR1Vf7XjfhQlVsAJdNiKdYmYVoKlaRv85IfVunYzO0IKXsyl7JCUjCpoG20f0a04COwfneQAGGwd5oa+T8yO5hzuyDb/XcxxmK01EpqOyuxINew==";
            //var iv = "r7BXXKkLb8qrSNn05n0qiA==";
            var result = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecodeEncryptedData(sessionKey, encryptedData, iv);
            Console.WriteLine(result);
            Assert.IsNotNull(result);

            //输出：{ "openId":"onh7q0DGM1dctSDbdByIHvX4imxA","nickName":"苏震巍","gender":1,"language":"zh_CN","city":"Suzhou","province":"Jiangsu","country":"CN","avatarUrl":"http://wx.qlogo.cn/mmopen/vi_32/PiajxSqBRaEKXyjX4N6I5Vx1aeiaBeJ2iaTLy15n0HgvjNbWEpKA3ZbdgXkOhWK7OH8iar3iaLxsZia5Ha4DnRPlMerw/0","watermark":{ "timestamp":1485785979,"appid":"wxfcb0a0031394a51c"} }

            //Assert.AreEqual("wxfcb0a0031394a51c",userInfo.);
        }

        [TestMethod()]
        public void DecodeUserInfoBySessionIdTest()
        {
            var sessionId = "ABCDEFG";
            var sessionKey = "/mGmINZAe+7k6kNz32wxSw==";
            var encryptedData =
                "CFcsIXmH2r0v9ehjEhS+uUpJkr8qGQyt+Za3YkhjVNNA+xGj2WB2QFxDXdKVSzc10LukeB2maCxZCqpPQrWQx6CKF/VkEx96hXpPuBMpWBnnLzupoJpkRW9gJGRz7dcXDnqzstf2etRumDeAFDyjEKZ6bqs+KTE7qHauMsctxg4TXPbzzvWQm783j9PoWsCm/0A+aGNWCfZSFuJgi5G+LjTVqcGqP+mlAnLIFmgGLTo3vWrekz0//2vCMhgcgwKjPMR+VZTB7UItvnWfF4h4oOajcMuEiwTifaFkyn7l4NtLroMYjOfId16B6XCTK0BvPhTw9GI3wPMDopwWF2q3Op8M2fYWJuVGFKbrAZvVY/ILeIxYLaHuwHAOYULLre5Mg1kQpURlQ6I6e6GjraJUoL1BqsM38DayY5xRRFJsehZgrWkOySWICuN20Bte7+2N8D6PvhsaNyQz+4Lp4XY/Nn+clNGoM1v6aKTCv7PY2wo=";
            var iv = "ASJ0whjRyLK1tvgb7bAVSw==";
            var unionId = "";

            SessionContainer.UpdateSession(sessionId, "OpenId", sessionKey, unionId);

            var userInfo = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecodeUserInfoBySessionId(sessionId,
                encryptedData, iv);
            Assert.IsNotNull(userInfo);
            Assert.AreEqual("wxfcb0a0031394a51c", userInfo.watermark.appid);

            Console.WriteLine(SerializerHelper.GetJsonString(userInfo));
        }
    }
}