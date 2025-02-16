#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
 
    文件名：SecurityHelper.cs
    文件功能描述：安全帮助类，提供加密解密方法及微信支付要求的安全方法
    
    创建标识：Senparc - 20210822

    修改标识：Senparc - 20241020
    修改描述：v1.6.5 修改 SM 证书判断逻辑，向下兼容未升级 appsettings.json 的系统 #3084 感谢 @WXJDLM

----------------------------------------------------------------*/


using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
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
using System.Xml;

namespace Senparc.Weixin.TenPayV3.Helpers
{
    /// <summary>
    /// 安全帮助类
    /// </summary>
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
        /// 加密敏感信息，传入明文和从微信支付获取到的敏感信息加密公钥，事先使用OpenSSL转换cert.pem文件输出为der文件
        /// </summary>
        /// <param name="text"></param>
        /// <param name="publicKey"></param>
        /// <param name="encryptionType"></param>
        /// <param name="isWeixinPubKey">是否是微信支付公钥</param>
        /// <returns></returns>
        public static string Encrypt(string text, string publicKey, CertType encryptionType, bool isWeixinPubKey = false)
        {
            #region 基于微信支付公钥
            if (isWeixinPubKey)
            {
                var publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
                var publicKeyXml = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                    Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                    Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));

                var rsa = new RSACryptoServiceProvider();
                RSAFromXmlString(rsa, publicKeyXml);
                var buff = rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA1);
                return Convert.ToBase64String(buff);
            }
            #endregion

            if (encryptionType == CertType.SM)
            {
                ECPublicKeyParameters eCPublicKeyParameters = SMPemHelper.LoadPublicKeyToParameters(Encoding.UTF8.GetBytes(publicKey));
                return GmHelper.Sm2Encrypt(eCPublicKeyParameters, text);
            }
            else
            {
                var x509 = new X509Certificate2(Encoding.UTF8.GetBytes(publicKey));
                var rsa = x509.GetRSAPublicKey();
                var buff = rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA1);
                return Convert.ToBase64String(buff);
            }
        }

        /// <summary>
        /// 字段加密
        /// </summary>
        /// <param name="request"></param>
        /// <param name="publicKey"></param>
        /// <param name="encryptionType"></param>
        /// <param name="isWeixinPubKey">是否是微信支付公钥</param>
        public static void FieldEncrypt(object request, string publicKey, CertType encryptionType, bool isWeixinPubKey = false)
        {
            var pis = request.GetType().GetProperties();
            foreach (var pi in pis)
            {
                var value = pi.GetValue(request);
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    continue;

                if (!(pi.PropertyType.IsValueType || pi.PropertyType.Name.StartsWith("String") || typeof(IEnumerable).IsAssignableFrom(pi.PropertyType)))
                {
                    FieldEncrypt(value, publicKey, encryptionType, isWeixinPubKey);
                    continue;
                }

                if ((pi.GetCustomAttributes(typeof(FieldEncryptAttribute), true)?.Count() ?? 0) <= 0)
                    continue;

                var encryptValue = Encrypt(value.ToString(), publicKey, encryptionType, isWeixinPubKey);
                pi.SetValue(request, encryptValue);
            }
        }

        /// <summary>
        /// 字段加密
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiV3Key"></param>
        /// <param name="encryptionType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Obsolete("放弃使用", true)]
        public static async Task FieldEncryptAsync(object request, string apiV3Key, CertType encryptionType)
        {
            // name 敏感信息加密
            var basePayApis = new BasePayApis();
            var certificate = await basePayApis.CertificatesAsync(encryptionType);
            FieldEncrypt(request, certificate, apiV3Key, encryptionType);
        }

        /// <summary>
        /// 字段加密
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiV3Key"></param>
        /// <param name="encryptionType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Obsolete("放弃使用", true)]
        public static void FieldEncrypt(object request, CertificatesResultJson certificate, string apiV3Key, CertType encryptionType)
        {
            if (!(certificate?.ResultCode?.Success ?? false))
                throw new Exception(certificate?.ResultCode?.ErrorMessage ?? "basePayApis.CertificatesAsync 获取证书失败");

            var publicKey = GetPublicKey(certificate.data?.FirstOrDefault()?.encrypt_certificate, apiV3Key, encryptionType);
            if (!string.IsNullOrWhiteSpace(publicKey))
            {
                FieldEncrypt(request, publicKey, encryptionType);
            }
        }

        /// <summary>
        /// 敏感信息加密需要从https://api.mch.weixin.qq.com/risk/getcertficates此接口下载加密证书进行下一步加密，
        /// 该接口下载到的是密文，使用此AESGCM.Decrypt()方法解密得到证书明文
        /// </summary>
        /// <param name="encryptCertificate"></param>
        /// <param name="apiV3Key"></param>
        /// <param name="encryptionType"></param>
        /// <returns></returns>
        public static string GetPublicKey(Encrypt_Certificate encryptCertificate, string apiV3Key, CertType encryptionType)
        {
            if (encryptionType == CertType.SM)
            {
                return GmHelper.Sm4DecryptGCM(apiV3Key, encryptCertificate.nonce, "certificate", encryptCertificate.ciphertext);
            }
            else
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
        }

        public static void RSAFromXmlString(RSA rsa, string xmlString)
        {
            var parameters = new RSAParameters();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus":
                            parameters.Modulus = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "Exponent":
                            parameters.Exponent = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "P":
                            parameters.P = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "Q":
                            parameters.Q = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "DP":
                            parameters.DP = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "DQ":
                            parameters.DQ = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "InverseQ":
                            parameters.InverseQ = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                        case "D":
                            parameters.D = (string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText));
                            break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }
    }
}
