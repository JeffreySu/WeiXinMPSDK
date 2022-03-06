using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.TenPayHttpClient.Verifier
{
    public interface IVerifier
    {
        bool Verify(string wechatpayTimestamp, string wechatpayNonce, string wechatpaySignatureBase64, string content, string pubKey); // 验证签名
    }
}
