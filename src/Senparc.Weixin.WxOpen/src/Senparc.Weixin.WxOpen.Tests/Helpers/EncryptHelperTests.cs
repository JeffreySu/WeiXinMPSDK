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
            var rawData =
                "{\"nickName\":\"Band\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Guangzhou\",\"province\":\"Guangdong\",\"country\":\"CN\",\"avatarUrl\":\"http://wx.qlogo.cn/mmopen/vi_32/1vZvI39NWFQ9XM4LtQpFrQJ1xlgZxx3w7bQxKARol6503Iuswjjn6nIGBiaycAjAtpujxyzYsrztuuICqIM5ibXQ/0\"}";

            var sessionKey = "HyVFkGl5F5OQWJZZaNzBBg==";

            var signature = EncryptHelper.GetSignature(rawData, sessionKey);
            Assert.AreEqual("75e81ceda165f4ffa64f4068af58c64b8f54b88c",signature);
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
            Assert.AreEqual(sessionKey,sessionBag.SessionKey);

            var compareSignature = "1149a88c75125de3146040c90d7bcc4b2a564a34";
            var result = EncryptHelper.CheckSignature(sessionId, rawData, compareSignature);
            Assert.IsTrue(result);

        }
    }
}