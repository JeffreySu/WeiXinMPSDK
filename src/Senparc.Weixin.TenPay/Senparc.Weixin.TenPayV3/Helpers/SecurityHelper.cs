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
            //将解密所需数据转换为Bytes
            var keyBytes = Encoding.UTF8.GetBytes(aes_key);
            var nonceBytes = Encoding.UTF8.GetBytes(nonce);
            var associatedBytes = associated_data == null ? null : Encoding.UTF8.GetBytes(associated_data);

            //AEAD_AES_256_GCM 解密
            var encryptedBytes = Convert.FromBase64String(ciphertext);
            //tag size is 16 TODO: what is tag size?
            var cipherBytes = encryptedBytes[..^16];
            var tag = encryptedBytes[^16..];
            var decryptedData = new byte[cipherBytes.Length];
            using var cipher = new AesGcm(keyBytes);
            cipher.Decrypt(nonceBytes, cipherBytes, tag, decryptedData, associatedBytes);
            var decrypted_string = Encoding.UTF8.GetString(decryptedData);

            return decrypted_string;
        }

        /// <summary>   
        /// 获得纯净的证书内容
        /// </summary>
        /// <param name="orignalPublicKey">原始证书秘钥字符串</param>
        /// <returns></returns>
        public static string GetUnwrapCertKey(string originalPublicKey)
        {
            var unwrapKey = Regex.Replace(originalPublicKey, @"(\s|([\-]+[^\-]+[\-]+))+", "");
            return unwrapKey;
        }

        /// <summary>
        /// 获取文件的 HASH 值
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        public static string GetFileHash(string filePath, string type = "SHA1")
        {
            switch (type)
            {
                case "SHA1":
                    {
                        var hash = SHA1.Create();
                        var stream = new FileStream(filePath, FileMode.Open);
                        byte[] hashByte = hash.ComputeHash(stream);
                        stream.Close();
                        return BitConverter.ToString(hashByte).Replace("-", "");
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        /// <summary>
        /// 获取文件的 HASH 值
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        public static string GetFileHash(Stream stream, string type = "SHA1")
        {
            switch (type)
            {
                case "SHA1":
                    {
                        var hash = SHA1.Create();
                        stream.Seek(0, SeekOrigin.Begin);
                        byte[] hashByte = hash.ComputeHash(stream);
                        return BitConverter.ToString(hashByte).Replace("-", "");
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
