using Org.BouncyCastle.Crypto.Parameters;
using Senparc.Weixin.TenPayV3.Helpers;
using System;

namespace Senparc.Weixin.TenPayV3.TenPayHttpClient.Verifier
{
    public class SM3WithSM2Verifier : IVerifier
    {

        public bool Verify(string wechatpayTimestamp, string wechatpayNonce, string wechatpaySignatureBase64, string content, string pubKey)
        {
            //验签名串
            string contentForSign = $"{wechatpayTimestamp}\n{wechatpayNonce}\n{content}\n";

            byte[] pubKeyBytes = Convert.FromBase64String(pubKey);
            ECPublicKeyParameters eCPublicKeyParameters = SMPemHelper.LoadPublicKeyToParameters(pubKeyBytes);

            return GmHelper.VerifySm3WithSm2(eCPublicKeyParameters, contentForSign, wechatpaySignatureBase64);
        }
    }
}
