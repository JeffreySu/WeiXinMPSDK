using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.MP.Test
{
    using Senparc.Weixin.MP;

    [TestClass]
    public class CheckSignatureTest
    {
        [TestMethod]
        public void GetSignatureTest()
        {
            //2013-01-12 09:12:00 118.244.133.118 GET /weixin signature=335b26997f05c82243aad6b10a2d1853637e71a8&echostr=5833258582299191661&timestamp=1357982061&nonce=1358161344 80 - 101.226.89.83 Mozilla/4.0 200 0 0 35
            {
                var signature = "335b26997f05c82243aad6b10a2d1853637e71a8";
                var echostr = "echostr";
                var timestamp = "1357982061";
                var nonce = "1358161344";
                var token = "weixin";

                var result = CheckSignature.GetSignature(timestamp, nonce, token);
                Assert.AreEqual(signature, result);
            }

            {
                var signature = "efa917e2445cc9f0aa4386ca92f96de5a242e24e";
                var echostr = "5837835896451489886";
                var timestamp = "1359208540";
                var nonce = "1359227084";
                var token = "huhujm";

                var result = CheckSignature.GetSignature(timestamp, nonce, token);
                Assert.AreEqual(signature, result);
            }
        }
    }
}
