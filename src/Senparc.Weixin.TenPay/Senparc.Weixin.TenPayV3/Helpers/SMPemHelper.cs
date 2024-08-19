using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Senparc.Weixin.TenPayV3.Helpers
{
    public class SMPemHelper
    {
        /// <summary>
        /// 加载椭圆私钥参数
        /// </summary>
        /// <param name="privateKeyBytes">PKCS#8 私钥字节数组。</param>
        /// <returns></returns>
        public static ECPrivateKeyParameters LoadPrivateKeyToParameters(byte[] privateKeyBytes)
        {
            return (ECPrivateKeyParameters)PrivateKeyFactory.CreateKey(privateKeyBytes);
        }


        /// <summary>
        /// 加载椭圆公钥参数
        /// </summary>
        /// <param name="publicKey">PKCS#8 公钥字节数组。</param>
        /// <returns></returns>
        public static ECPublicKeyParameters LoadPublicKeyToParameters(byte[] publicKeyBytes)
        {
            //使用 X509Certificate2 证书
            var x509 = new X509Certificate2(publicKeyBytes);
            var hex = x509.GetPublicKeyString();
            // 假设hex字符串是不带前缀的未压缩公钥（65字节：1字节0x04 + 32字节X坐标 + 32字节Y坐标）  
            if (hex.Length != 130) // 04 + 2 * 32 * 2 (hex字符)  
            {
                throw new ArgumentException("Invalid hex length for uncompressed EC public key");
            }

            // 去除可能的"04"前缀（如果是未压缩格式）  
            if (hex.StartsWith("04", StringComparison.OrdinalIgnoreCase))
            {
                hex = hex.Substring(2);
            }

            // 将十六进制字符串转换为字节数组  
            byte[] keyBytes = Hex.Decode(hex);

            // 分离X和Y坐标  
            byte[] xBytes = new byte[32];
            byte[] yBytes = new byte[32];
            Array.Copy(keyBytes, 0, xBytes, 0, 32);
            Array.Copy(keyBytes, 32, yBytes, 0, 32);

            // 获取椭圆曲线参数  
            X9ECParameters curveParams = ECNamedCurveTable.GetByName("sm2p256v1");
            ECDomainParameters domainParams = new ECDomainParameters(curveParams.Curve, curveParams.G, curveParams.N);

            // 创建公钥参数  
            BigInteger x = new BigInteger(1, xBytes);
            BigInteger y = new BigInteger(1, yBytes);
            ECPoint point = curveParams.Curve.CreatePoint(x, y);

            return new ECPublicKeyParameters(point, domainParams);
        }
    }
}
