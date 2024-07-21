using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Crypto.Parameters;
using Senparc.Weixin.TenPayV3.Helpers;
using System;

namespace Senparc.Weixin.TenPayV3.Test.net8.Helpers
{
    [TestClass()]
    public class GmHelperTests
    {
        private string message = "盛派微信SDK国密SignWithSM3签名字符串";
        private string privateKeyPem = "MIGTAgEAMBMGByqGSM49AgEGCCqBHM9VAYItBHkwdwIBAQQg/U9Saz5ceDGQhFBMf/cHIh2+q5Lzvhrb8L2SI4sPUR+gCgYIKoEcz1UBgi2hRANCAAR7zUf3XqGdBMCnK02nD1fdGz+TN8xd0SQYgbkTQxk5v2mlXva0ZxDH4XJFI9D2Rt1pTSSDl29uhQ1GY3j6iy9+";
        private string publicKeyPem = "MIICyTCCAmygAwIBAgIUVOf1ntjfRXDPsUeA6oFS1FpnXuYwDAYIKoEcz1UBg3UFADCBjTFHMEUGA1UEAww+U2hlbiBaaGVuIGlUcnVzQ2hpbmEgQ2xhc3MgRW50ZXJwcmlzZSBTdWJzY3JpYmVyIENBIFNNMiAtIFRlc3QxGDAWBgNVBAsMD+a1i+ivlemDqOivleeUqDEbMBkGA1UECgwS5aSp6K+a5a6J5L+h6K+V55SoMQswCQYDVQQGEwJDTjAeFw0yMjA4MDQxMjE0MTlaFw0yMzA5MDMxMjE0MTlaMGsxCzAJBgNVBAYTAkNOMRswGQYDVQQKDBLlvq7kv6HllYbmiLfns7vnu58xKjAoBgNVBAsMIea3seWcs+W4guS8n+iNo+enkeaKgOaciemZkOWFrOWPuDETMBEGA1UEAwwKMjQ4MzI4MTc2MTBZMBMGByqGSM49AgEGCCqBHM9VAYItA0IABHvNR/deoZ0EwKcrTacPV90bP5M3zF3RJBiBuRNDGTm/aaVe9rRnEMfhckUj0PZG3WlNJIOXb26FDUZjePqLL36jgcgwgcUwDAYDVR0TAQH/BAIwADAOBgNVHQ8BAf8EBAMCBsAwHwYDVR0jBBgwFoAUK0Y6T9GeLM7UH4bC1j2avycoZPcwHQYDVR0OBBYEFBmE5STz2WNsXVrgclXrxsl6SCihMGUGA1UdHwReMFwwWqBYoFaGVGh0dHA6Ly9ldmNhLml0cnVzLmNvbS5jbi9wdWJsaWMvaXRydXNjcmw/Q0E9NzMzNUExQUYzNzRBMUU4QjQwM0FCMUFDMkQwNjVDQUU3NUNBQjIzNjAMBggqgRzPVQGDdQUAA0kAMEYCIQDK0r6D8VyiUVMfRnAfz40ZtiG8DJEF6Rn41oZ3qPW1aQIhAKtn5sKME+thLQFeyV70VSsraZ7h9Fccal2WzI2oCdtY";
        private string sign = "MEUCIQDuOrfi+BeRBhKfL8pDjBEawou3zSuvD5Jgtc+IazIemgIgNfjOX3tYzmOBzJIKlKJwmtNMM8VQ3O3wcZIeRtpAG1c=";
        private string apiV3Key = "a7cde1ZJB1kG2e7VfTs3jQzaWizur8Gb";
        private string nonce = "uluk4a9R25RW";
        private string sm2PlainText = "盛派微信SDK国密SM2明文字符串";
        private string sm4PlainText = "盛派微信SDK国密SM4明文字符串";
        private string associatedData = "associatedData";
        private string sm2CipherText = "MIGQAiEAu/8W8yrdB39F8sFkyvvHPU7jKBWnBXl/Da7SoqXW91kCIBrSfdqagaGqESouei6iwNaRI+OUpFjaSmGW6ClhG+gFBCA3eKXasHLhXQh7JDxtx/jCvdsuFtlYysYi6OzKVt5llQQnQZbC2Z8rrkkbvT/wNEhkuyN8RtajeYeGfqxOU8iTO/koIeTVEbqc";
        private string sm4CipherText = "baDxuiPTwcqkwYBVzv8Lh9xdVCtwqHXX6VqxEChy5Xx8OncGEnKwvf1UoxWtYKQ2eyNeZU0JcQ==";

        [TestMethod]
        public void SignSm3WithSm2Test()
        {

            byte[] keyData = Convert.FromBase64String(privateKeyPem);
            byte[] pubKeyBytes = Convert.FromBase64String(publicKeyPem);
            ECPrivateKeyParameters eCPrivateKeyParameters = SMPemHelper.LoadPrivateKeyToParameters(keyData);
            ECPublicKeyParameters eCPublicKeyParameters = SMPemHelper.LoadPublicKeyToParameters(pubKeyBytes);

            byte[] signBytes = GmHelper.SignSm3WithSm2(eCPrivateKeyParameters, message);
            string signatureResult = Convert.ToBase64String(signBytes);

            Assert.IsNotNull(signatureResult);

            Assert.IsTrue(GmHelper.VerifySm3WithSm2(eCPublicKeyParameters, message, signatureResult));
        }

        [TestMethod]
        public void VerifySm3WithSm2Test()
        {
            byte[] pubKeyBytes = Convert.FromBase64String(publicKeyPem);
            ECPublicKeyParameters eCPublicKeyParameters = SMPemHelper.LoadPublicKeyToParameters(pubKeyBytes);

            Assert.IsTrue(GmHelper.VerifySm3WithSm2(eCPublicKeyParameters, message, sign));
        }

        [TestMethod]
        public void Sm2EncryptTest()
        {
            byte[] keyData = Convert.FromBase64String(privateKeyPem);
            byte[] pubKeyBytes = Convert.FromBase64String(publicKeyPem);
            ECPrivateKeyParameters eCPrivateKeyParameters = SMPemHelper.LoadPrivateKeyToParameters(keyData);
            ECPublicKeyParameters eCPublicKeyParameters = SMPemHelper.LoadPublicKeyToParameters(pubKeyBytes);

            var encryptData = GmHelper.Sm2Encrypt(eCPublicKeyParameters, sm2PlainText);
            var decryptData = GmHelper.Sm2Decrypt(eCPrivateKeyParameters, encryptData);

            Assert.AreEqual(sm2PlainText, decryptData);
        }

        [TestMethod]
        public void Sm2DecryptTest()
        {
            byte[] keyData = Convert.FromBase64String(privateKeyPem);
            ECPrivateKeyParameters eCPrivateKeyParameters = SMPemHelper.LoadPrivateKeyToParameters(keyData);

            var decryptData = GmHelper.Sm2Decrypt(eCPrivateKeyParameters, sm2CipherText);

            Assert.AreEqual(sm2PlainText, decryptData);
        }

        [TestMethod]
        public void Sm4EncryptGCMTest()
        {
            var encryptData = GmHelper.Sm4EncryptGCM(apiV3Key, nonce, associatedData, sm4PlainText);
            var decryptData = GmHelper.Sm4DecryptGCM(apiV3Key, nonce, associatedData, encryptData);

            Assert.AreEqual(sm4PlainText, decryptData);
        }

        [TestMethod]
        public void Sm4DecryptGCMTest()
        {
            var decryptData = GmHelper.Sm4DecryptGCM(apiV3Key, nonce, associatedData, sm4CipherText);

            Assert.AreEqual(sm4PlainText, decryptData);
        }
    }
}
