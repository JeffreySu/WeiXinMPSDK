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
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.WeixinTests;

namespace Senparc.Weixin.WxOpen.Helpers.Tests
{
    [TestClass()]
    public class EncryptHelperTests : BaseTest
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

            //测试 EncryptHelper.DecodeEncryptedData() 方法
            var userInfoStr = EncryptHelper.DecodeEncryptedData(sessionKey, encryptedData, iv);
            Console.WriteLine("userInfoStr:");
            Console.WriteLine(SerializerHelper.GetJsonString(userInfoStr));
        }

        [TestMethod()]
        public void DecodeUserInfoBySessionIdTest2()
        {
            //测试 issue：https://github.com/JeffreySu/WeiXinMPSDK/issues/1869
            //var sessionId = "ABCDEFGHIJK";
            var sessionKey = "0sVkQ4CtcaiYJtvoPLBecw==";
            var encryptedData =
                "GiW4s+17o7RSaPOwGX8Ir1+3c/RYbHKvRzBg8UFlmIIiArLtU0ctkzjq1LRR5MH5CSPs63Jt4qCoFScSlRKlQ4/RVXXJFQV+r/1L+qKv/PdHRvVDLb+8P6CvPTurEuHsxlLyXTnnlEIu6IFYFzZWBMIp6+SHEK85mEb1gw4BtMmEy9EitnMskNjsEnmpI3M9r8ItKyQ8hinJejuno0JPXn3trc+2gMheNt4+4NwMTM6mzzGVO6g40NP7NjK9Tl6+An2TjBe+GGVFdrkl5hpYDXE/YO2FsL909faX3Y08msSuCVk5AsMGMJiUwddiu44KODdxCYfwLxBaIgYJEY6xLygFmAMuDg/L2g4/wDabBrhA5BNsD6lrcRRbvrHK65Lu3xd1oTXyMGbfUGTD4GLlLSJUX2FhcG7ZmHwg1jQUuKFHJu/AMQgdoPa/JONAu5Hjp0hL7ahr5LC0ghwdTfTowg3X1Ko9IgRxxj755eGgXQK7AnsMwjXzt4X+4YpOYpCb2LVSrTV2t4QjVNPe+Rjmsg==";
            var iv = "4y2ftkwAM2mF6Qc89HydpA==";
            //var unionId = "";

            //SessionContainer.UpdateSession(sessionId, "OpenId", sessionKey, unionId);
            try
            {
                var userInfo = EncryptHelper.DecodeEncryptedData(sessionKey, encryptedData, iv);


                //var userInfo = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecodeUserInfoBySessionId(sessionId,
                //    encryptedData, iv);
                Assert.IsNotNull(userInfo);
                Console.WriteLine(userInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
           
            //Assert.AreEqual("wxfcb0a0031394a51c", userInfo.watermark.appid);

            //Console.WriteLine(SerializerHelper.GetJsonString(userInfo));
        }


        [TestMethod()]
        public void DecodeUserInfoBySessionIdTest3()
        {
            //测试 issue：https://github.com/JeffreySu/WeiXinMPSDK/issues/1825
            var sessionKey = "98195476102492321891401391061935624977242";
            var encryptedData =
                "deWfUALVVTxrux2cp0qeqLWotTHTIpRmIrpcuWoh3ngyr7vjCDYq1wh2Q0CE6Zj9P/V2ZVqtjkVAiGdBuBR8fSs9qpWhb9ieO5FoumuvgoM6HP5+7Eul6lm8njXJlbTZr+pODAIeMoBIwpQUPpCLwYtpSuKlQGKvsrmoVU5j5xgoKm4dyKmNwq3qcqE5Q+HUOV0r/c7GusFWZD0haaccduMjmKAyupCpbwdDu6kiVfEo1pVZdp5j4C5ihrZdE7gzeS9vOAFDaB+NXPB6Lz+H8js6BH8gVJ7tZ1KUAwqt+FIqHHBKsREKoyjePwREkRc1Sr/N+QR1vps2cFGpqp16NAoTyT/JFi2jNs8PgrrEYZkjVvyMUYFlDnq5BWNyyh5RX34JEq7EN62sc+wfAMB2Nrm/QEcBCtYLycP3xcQnCLasU2SQbpIr5GOUz7aiIu5rwMXMUDDg7jxCOA4+ORfSHUgS6OczRjY+QqrcfKmlA84=";
            var iv = "116115241129461711788323441202601974169239";
            var userInfo = EncryptHelper.DecodeEncryptedData(sessionKey, encryptedData, iv);
            Assert.IsNotNull(userInfo);
            Console.WriteLine(userInfo);
        }

        //[TestMethod]
        //public void AES_DecryptTest()
        //{
        //    JsCode2JsonResult result = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<JsCode2JsonResult>(@"{""openid"":""o5mT-4xYKKfTYu4qjrZ6lpnZE7KY"",""session_key"":""0sVkQ4CtcaiYJtvoPLBecw=="",""unionid"":null,""errcode"":0,""ErrorCodeValue"":0,""errmsg"":null,""P2PData"":null}");
        //    //使用SessionContainer管理登录信息（推荐）
        //    var unionId = result.unionid;
        //    var sessionBag = SessionContainer.UpdateSession(null, result.openid, result.session_key, unionId);
        //    var sessionId = sessionBag.Key;


        //    var model = SerializerHelper.GetObject<dynamic>(@"{""Code"":""071GaTvK0UyoL92mO8yK0WlMvK0GaTvj"",""State"":""Csproj_wxOpen_Bind"",""ReturnUrl"":"""",""EncryptedData"":""GiW4s+17o7RSaPOwGX8Ir1+3c/RYbHKvRzBg8UFlmIIiArLtU0ctkzjq1LRR5MH5CSPs63Jt4qCoFScSlRKlQ4/RVXXJFQV+r/1L+qKv/PdHRvVDLb+8P6CvPTurEuHsxlLyXTnnlEIu6IFYFzZWBMIp6+SHEK85mEb1gw4BtMmEy9EitnMskNjsEnmpI3M9r8ItKyQ8hinJejuno0JPXn3trc+2gMheNt4+4NwMTM6mzzGVO6g40NP7NjK9Tl6+An2TjBe+GGVFdrkl5hpYDXE/YO2FsL909faX3Y08msSuCVk5AsMGMJiUwddiu44KODdxCYfwLxBaIgYJEY6xLygFmAMuDg/L2g4/wDabBrhA5BNsD6lrcRRbvrHK65Lu3xd1oTXyMGbfUGTD4GLlLSJUX2FhcG7ZmHwg1jQUuKFHJu/AMQgdoPa/JONAu5Hjp0hL7ahr5LC0ghwdTfTowg3X1Ko9IgRxxj755eGgXQK7AnsMwjXzt4X+4YpOYpCb2LVSrTV2t4QjVNPe+Rjmsg=="",""Iv"":""4y2ftkwAM2mF6Qc89HydpA=="",""RawData"":""{\""nickName\"":\""*Rebecca\"",\""gender\"":2,\""language\"":\""zh_CN\"",\""city\"":\""Qingdao\"",\""province\"":\""Shandong\"",\""country\"":\""China\"",\""avatarUrl\"":\""https://wx.qlogo.cn/mmopen/vi_32/DYAIOgq83epNuU1WTDS98s7OS06OmnNktDJyHkrjRmEJrDjheXnnETPOTtMcsMPP6q7Bn3TIorV28iaETTvl3sA/132\""}"",""Signature"":""4a9a68b855b73f5a448d2b169a697ee14de5cf06""}");

        //    var checkSuccess = EncryptHelper.CheckSignature(sessionId, model.RawData, model.Signature);
        //    if (!checkSuccess)
        //    {
        //        //throw new UserFriendlyException("签名校验失败");
        //    }

        //    var userInfo = EncryptHelper.DecodeUserInfoBySessionId(sessionId, model.EncryptedData, model.Iv);

        //}

    }
}