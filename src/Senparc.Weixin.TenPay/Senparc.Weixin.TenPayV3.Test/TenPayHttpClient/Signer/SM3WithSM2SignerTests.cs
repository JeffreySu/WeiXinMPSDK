using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.TenPayHttpClient.Signer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TenPayHttpClient.Signer.Tests
{
    [TestClass()]
    public class SM3WithSM2SignerTests
    {
        [TestMethod()]
        public void SignTest()
        {
            SM3WithSM2Signer sM3WithSM2Signer = new();
            var message = "Senparc";
            var privateKey = "";
            var sign = sM3WithSM2Signer.Sign(message, privateKey);
            var exceptResult = "1ab21d8355cfa17f8e61194831e81a8f22bec8c728fefb747ed035eb5082aa2b";

            Assert.AreEqual(exceptResult, sign);
        }
    }
}