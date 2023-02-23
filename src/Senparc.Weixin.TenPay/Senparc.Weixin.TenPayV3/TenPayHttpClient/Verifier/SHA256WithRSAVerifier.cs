using Senparc.Weixin.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Senparc.Weixin.TenPayV3.TenPayHttpClient.Verifier
{
    public class SHA256WithRSAVerifier : IVerifier
    {
        public bool Verify(string wechatpayTimestamp, string wechatpayNonce, string wechatpaySignatureBase64, string content, string pubKey)
        {
            //验签名串
            string contentForSign = $"{wechatpayTimestamp}\n{wechatpayNonce}\n{content}\n";

            //Base64 解码 pubKey（必须已经使用 ApiSecurityHelper.GetUnwrapCertKey() 方法进行 Unwrap）
            var bs = Convert.FromBase64String(pubKey);
            //使用 X509Certificate2 证书
            var x509 = new X509Certificate2(bs);
            //AsymmetricAlgorithm对象
            var key = x509.PublicKey.Key;

            //RSAPKCS1SignatureDeformatter 对象
            RSAPKCS1SignatureDeformatter df = new RSAPKCS1SignatureDeformatter(key);
            //指定 SHA256
            df.SetHashAlgorithm("SHA256");
            //SHA256Managed 方法已弃用，使用 SHA256.Create() 生成 SHA256 对象
            var sha256 = SHA256.Create();
            //应答签名
            byte[] signature = Convert.FromBase64String(wechatpaySignatureBase64);
            //对比签名
            byte[] compareByte = sha256.ComputeHash(Encoding.UTF8.GetBytes(contentForSign));
            //验证签名
            var result = df.VerifySignature(compareByte, signature);

            return result;
        }
    }
}
