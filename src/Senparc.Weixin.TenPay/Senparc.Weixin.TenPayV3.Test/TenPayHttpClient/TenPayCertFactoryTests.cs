using Client.TenPayHttpClient.Signer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPayV3.TenPayHttpClient;
using Senparc.Weixin.TenPayV3.TenPayHttpClient.Verifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.TenPayHttpClient.Tests
{
    [TestClass()]
    public class TenPayCertFactoryTests
    {
        [TestMethod()]
        public void GetSignerTest()
        {
            {
                var certType = CertType.RSA;
                var result = TenPayCertFactory.GetSigner(certType);
                Assert.IsInstanceOfType(result, typeof(SHA256WithRSASigner));
                Assert.AreEqual("SHA256-RSA2048", result.GetAlgorithm());
            }

            {
                var certType = CertType.SM;
                var result = TenPayCertFactory.GetSigner(certType);
                Assert.IsInstanceOfType(result, typeof(SM3WithSM2Signer));
                Assert.AreEqual("SM3-SM2", result.GetAlgorithm());
            }
        }

        [TestMethod()]
        public void GetVerifierTest()
        {
            {
                var certType = CertType.RSA;
                var result = TenPayCertFactory.GetVerifier(certType);
                Assert.IsInstanceOfType(result, typeof(SHA256WithRSAVerifier));
                //TODO：测试 Verify() 方法
            }

            {
                var certType = CertType.SM;
                var result = TenPayCertFactory.GetVerifier(certType);
                Assert.IsInstanceOfType(result, typeof(SM3WithSM2Verifier));
                //TODO：测试 Verify() 方法
            }
        }
    }
}