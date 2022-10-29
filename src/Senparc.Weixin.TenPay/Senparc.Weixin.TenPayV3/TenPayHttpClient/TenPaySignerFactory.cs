using Client.TenPayHttpClient.Signer;
using Senparc.Weixin.TenPayV3.TenPayHttpClient.Verifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.TenPayHttpClient
{
    /// <summary>
    /// 微信支付证书工厂类
    /// </summary>
    public static class TenPayCertFactory
    {
        /// <summary>
        /// 获取签名对象
        /// </summary>
        /// <param name="signerType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static ISigner GetSigner(CertType certType)
        {
            switch (certType)
            {
                case CertType.RSA:
                    return new SHA256WithRSASigner();
                case CertType.SM:
                    return new SM3WithSM2Signer();
                default:
                    throw new ArgumentOutOfRangeException(nameof(certType));
            }
        }

        /// <summary>
        /// 获取验签对象
        /// </summary>
        /// <param name="signerType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IVerifier GetVerifier(CertType certType)
        {
            switch (certType)
            {
                case CertType.RSA:
                    return new SHA256WithRSAVerifier();
                case CertType.SM:
                    return new SM3WithSM2Verifier();
                default:
                    throw new ArgumentOutOfRangeException(nameof(certType));
            }
        }
    }
}
