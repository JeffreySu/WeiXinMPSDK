using Senparc.CO2NET.Helpers;
using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Helpers
{
    public class SecurityHelper
    {
        /// <summary>
        /// 解密微信支付接口 ciphertext 内容
        /// </summary>
        /// <returns></returns>
        public static string AesGcmDecryptCiphertext(string aes_key, string nonce, string associated_data, string ciphertext)
        {
            return EncryptHelper.AesGcmDecrypt(aes_key, nonce, associated_data, ciphertext);
        }

        /// <summary>   
        /// 获得纯净的证书内容
        /// </summary>
        /// <param name="originalPublicKey">原始证书秘钥字符串</param>
        /// <returns></returns>
        public static string GetUnwrapCertKey(string originalPublicKey)
        {
            var unwrapKey = Regex.Replace(originalPublicKey, @"(\s|([\-]+[^\-]+[\-]+))+", "");
            return unwrapKey;
        }
    }
}
