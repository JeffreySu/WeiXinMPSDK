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
            var sessionKey = "X9SEb3ICAtmCHw2ouHD9Gg==";
            var encryptedData =
            "B+qZ1aOcV+KVXad4k7/rofp01LkO1ns4xKvlmqY3qb/za/gKdFlZ4HPMBHZ/xTyi2nycCn3zrsM/e3rPOZPrwpogLKXEBEGICODD0JKjkFat6hkqrg3lt/SaFJCDOlpAO6ddJ+yB49hDRYkRihrYK2MtsaTkRcka08kDlrtWmPiJopldbf0hfxzkaDkc1XeYqPyo31r4Q10fy65CA4Y3odGGYro1KEiZN4QpTzIuQOFBw1tEL88KIUgqJ0qO5YzrAVNcJ05DG2ViOGRjv1qfqiVsNiZ8O/CUn8KqDdAkmBexYRASD8XsR5bCYlQEix2iJwgnfXrXiMhAC2K4RTkAOoXchwNxT64rMnhLWVEB7T2uYyGPsE20m4q6cOrrWEMLttrqOITMmtF7T8MZp1oFj4CvXBLn0RLktAQXPoa2rtvQpxHhhl8NLuFeBqaQAgLJGUDg3QD2GZMzWw0aVupE7ss+7/WOvqmn5KsWfcqf9VQ=";
            var iv = "IBAD3Gk1pFRAmUEJCNJ4Gw==";
            var result = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecodeEncryptedData(sessionKey, encryptedData, iv);
            Console.WriteLine(result);

            var result2 = Convert.FromBase64String(result);
            Console.WriteLine(Encoding.UTF8.GetString(result2));
        }
    }
}