using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Attributes;
using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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

        /// <summary>
        /// 敏感信息加密需要从https://api.mch.weixin.qq.com/risk/getcertficates此接口下载加密证书进行下一步加密，
        /// 该接口下载到的是密文，使用此AESGCM.Decrypt()方法解密得到证书明文
        /// </summary>
        /// <param name="encryptCertificate"></param>
        /// <param name="apiV3Key"></param>
        /// <returns></returns>
        public static string GetPublicKey(Encrypt_Certificate encryptCertificate, string apiV3Key)
        {
            var buff = Convert.FromBase64String(encryptCertificate.ciphertext);
            var secret = Encoding.UTF8.GetBytes(apiV3Key);
            var nonce = Encoding.UTF8.GetBytes(encryptCertificate.nonce);
            var associatedData = Encoding.UTF8.GetBytes("certificate");

            // 算法 AEAD_AES_256_GCM，C# 环境使用 BouncyCastle.Crypto.dll 类库实现
            var cipher = new GcmBlockCipher(new AesEngine());
            var aead = new AeadParameters(new KeyParameter(secret), 128, nonce, associatedData);
            cipher.Init(false, aead);

            var data = new byte[cipher.GetOutputSize(buff.Length)];
            var num = cipher.ProcessBytes(buff, 0, buff.Length, data, 0);
            cipher.DoFinal(data, num);
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// 加密敏感信息，传入明文和从微信支付获取到的敏感信息加密公钥，事先使用OpenSSL转换cert.pem文件输出为der文件
        /// </summary>
        /// <param name="text"></param>
        /// <param name="publicKey"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static string Encrypt(string text, string publicKey, RSAEncryptionPadding padding)
        {
            var x509 = new X509Certificate2(Encoding.UTF8.GetBytes(publicKey));
            var rsa = x509.GetRSAPublicKey();
            var buff = rsa.Encrypt(Encoding.UTF8.GetBytes(text), padding);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 字段加密
        /// </summary>
        /// <param name="request"></param>
        /// <param name="publicKey"></param>
        /// <param name="padding"></param>
        public static void FieldEncrypt(object request, string publicKey, RSAEncryptionPadding padding)
        {
            var pis = request.GetType().GetProperties();
            foreach (var pi in pis)
            {
                var value = pi.GetValue(request);
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    continue;

                if (!(pi.PropertyType.IsValueType || pi.PropertyType.Name.StartsWith("String") || typeof(IEnumerable).IsAssignableFrom(pi.PropertyType)))
                {
                    FieldEncrypt(value, publicKey, padding);
                    continue;
                }

                if ((pi.GetCustomAttributes(typeof(FieldEncryptAttribute), true)?.Count() ?? 0) <= 0)
                    continue;

                var encryptValue = Encrypt(value.ToString(), publicKey, padding);
                pi.SetValue(request, encryptValue);
            }
        }

        /// <summary>
        /// 字段加密
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiV3Key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void FieldEncrypt(object request, CertificatesResultJson certificate, string apiV3Key)
        {
            if (!(certificate?.ResultCode?.Success ?? false))
                throw new Exception(certificate?.ResultCode?.ErrorMessage ?? "basePayApis.CertificatesAsync 获取证书失败");

            var publicKey = GetPublicKey(certificate.data?.FirstOrDefault()?.encrypt_certificate, apiV3Key);
            if (!string.IsNullOrWhiteSpace(publicKey))
                FieldEncrypt(request, publicKey, RSAEncryptionPadding.OaepSHA1);
        }

        /// <summary>
        /// 字段加密
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiV3Key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task FieldEncryptAsync(object request, string apiV3Key)
        {
            // name 敏感信息加密
            var basePayApis = new BasePayApis();
            var certificate = await basePayApis.CertificatesAsync();
            FieldEncrypt(request, certificate, apiV3Key);
        }
    }
}
