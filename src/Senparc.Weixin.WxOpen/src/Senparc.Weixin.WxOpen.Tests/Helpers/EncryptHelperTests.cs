using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.WxOpen.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.WxOpen.Containers;

namespace Senparc.Weixin.WxOpen.Helpers.Tests
{
    [TestClass()]
    public class EncryptHelperTests
    {
        [TestMethod()]
        public void GetSignatureTest()
        {
            //var rawData =
            //    "{\"nickName\":\"Band\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Guangzhou\",\"province\":\"Guangdong\",\"country\":\"CN\",\"avatarUrl\":\"http://wx.qlogo.cn/mmopen/vi_32/1vZvI39NWFQ9XM4LtQpFrQJ1xlgZxx3w7bQxKARol6503Iuswjjn6nIGBiaycAjAtpujxyzYsrztuuICqIM5ibXQ/0\"}";

            //var sessionKey = "HyVFkGl5F5OQWJZZaNzBBg==";
            //var compareSignature = "75e81ceda165f4ffa64f4068af58c64b8f54b88c";

            var rawData =
    "{\"nickName\":\"苏震巍\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Suzhou\",\"province\":\"Jiangsu\",\"country\":\"CN\",\"avatarUrl\":\"http://wx.qlogo.cn/mmopen/vi_32/PiajxSqBRaEKXyjX4N6I5Vx1aeiaBeJ2iaTLy15n0HgvjNbWEpKA3ZbdgXkOhWK7OH8iar3iaLxsZia5Ha4DnRPlMerw/0\"}";

            var sessionKey = "X9SEb3ICAtmCHw2ouHD9Gg==";
            var compareSignature = "4d2b94bb94a41eb6c9a33dc79445c468140ae852";

            var signature = EncryptHelper.GetSignature(rawData, sessionKey);
            Assert.AreEqual(compareSignature, signature);
        }

        [TestMethod]
        public void CheckSignatureTest()
        {
            //储存Session


            var sessionId = "7f3f7489cb904d20bd4b5e9443f1bcab";
            var rawData = "{\"nickName\":\"苏震巍\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Suzhou\",\"province\":\"Jiangsu\",\"country\":\"CN\",\"avatarUrl\":\"http://wx.qlogo.cn/mmopen/vi_32/PiajxSqBRaEKXyjX4N6I5Vx1aeiaBeJ2iaTLy15n0HgvjNbWEpKA3ZbdgXkOhWK7OH8iar3iaLxsZia5Ha4DnRPlMerw/0\"}";
            var sessionKey = "lEIWEBVlmAj/Ng0t54iahA==";

            SessionContainer.UpdateSession(sessionId, "openId", sessionKey);

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
            var sessionKey = "tiihtNczf5v6AKRyjwEUhQ==";
            var encryptedData =
                "CiyLU1Aw2KjvrjMdj8YKliAjtP4gsMZM" +
                "QmRzooG2xrDcvSnxIMXFufNstNGTyaGS" +
                "9uT5geRa0W4oTOb1WT7fJlAC+oNPdbB+" +
                "3hVbJSRgv+4lGOETKUQz6OYStslQ142d" +
                "NCuabNPGBzlooOmB231qMM85d2/fV6Ch" +
                "evvXvQP8Hkue1poOFtnEtpyxVLW1zAo6" +
                "/1Xx1COxFvrc2d7UL/lmHInNlxuacJXw" +
                "u0fjpXfz/YqYzBIBzD6WUfTIF9GRHpOn" +
                "/Hz7saL8xz+W//FRAUid1OksQaQx4CMs" +
                "8LOddcQhULW4ucetDf96JcR3g0gfRK4P" +
                "C7E/r7Z6xNrXd2UIeorGj5Ef7b1pJAYB" +
                "6Y5anaHqZ9J6nKEBvB4DnNLIVWSgARns" +
                "/8wR2SiRS7MNACwTyrGvt9ts8p12PKFd" +
                "lqYTopNHR1Vf7XjfhQlVsAJdNiKdYmYV" +
                "oKlaRv85IfVunYzO0IKXsyl7JCUjCpoG" +
                "20f0a04COwfneQAGGwd5oa+T8yO5hzuy" +
                "Db/XcxxmK01EpqOyuxINew==";
            var iv = "r7BXXKkLb8qrSNn05n0qiA==";
            var result = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecodeEncryptedData(sessionKey, encryptedData, iv);
            //Console.WriteLine(result);

            var result2 = Convert.FromBase64String(result);
            Console.WriteLine(Encoding.UTF8.GetString(result2));

            // 解密后的数据为
            //
            // data = {
            //   "nickName": "Band",
            //   "gender": 1,
            //   "language": "zh_CN",
            //   "city": "Guangzhou",
            //   "province": "Guangdong",
            //   "country": "CN",
            //   "avatarUrl": "http://wx.qlogo.cn/mmopen/vi_32/aSKcBBPpibyKNicHNTMM0qJVh8Kjgiak2AHWr8MHM4WgMEm7GFhsf8OYrySdbvAMvTsw3mo8ibKicsnfN5pRjl1p8HQ/0",
            //   "unionId": "ocMvos6NjeKLIBqg5Mr9QjxrP1FA",
            //   "watermark": {
            //     "timestamp": 1477314187,
            //     "appid": "wx4f4bc4dec97d474b"
            //   }
            // }

        }
    }
}