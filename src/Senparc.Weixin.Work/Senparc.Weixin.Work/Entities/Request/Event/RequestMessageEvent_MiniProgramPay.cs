/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_MiniProgramPay.cs
    文件功能描述：企业微信小程序对外收款通知模型及资源解密


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加小程序对外收款通知模型及资源解密

----------------------------------------------------------------*/

using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>小程序对外收款通知中的加密资源。</summary>
    public class MiniProgramPayNotificationResource
    {
        /// <summary>加密算法，当前为 AEAD_AES_256_GCM。</summary>
        public string algorithm { get; set; }

        /// <summary>Base64 编码的密文和认证标签。</summary>
        public string ciphertext { get; set; }

        /// <summary>附加认证数据。</summary>
        public string associated_data { get; set; }

        /// <summary>加密随机串。</summary>
        public string nonce { get; set; }
    }

    /// <summary>小程序对外收款通知基类。</summary>
    public class RequestMessageEvent_MiniProgramPayBase : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>通知唯一 ID。</summary>
        public string id { get; set; }

        /// <summary>通知创建时间，RFC 3339 格式。</summary>
        public string create_time { get; set; }

        /// <summary>支付通知事件类型。</summary>
        public string event_type { get; set; }

        /// <summary>资源类型。</summary>
        public string resource_type { get; set; }

        /// <summary>加密业务资源。</summary>
        public MiniProgramPayNotificationResource resource { get; set; }

        /// <summary>通知摘要。</summary>
        public string summary { get; set; }

        /// <summary>
        /// 使用“对外收款”应用回调配置中的 EncodingAESKey 解密业务资源。
        /// .NET Framework 与 netstandard2.0 不提供平台 AES-GCM 实现，将抛出
        /// <see cref="PlatformNotSupportedException"/>；这些目标仍可接收完整加密通知模型。
        /// </summary>
        public string DecryptResource(string encodingAesKey)
        {
#if NET462 || NETSTANDARD2_0
            throw new PlatformNotSupportedException(
                "当前目标框架不提供 AES-GCM。请在 netstandard2.1、netcoreapp3.1 或更新目标中解密通知资源。");
#else
            if (string.IsNullOrEmpty(encodingAesKey) || encodingAesKey.Length != 43)
            {
                throw new ArgumentException("EncodingAESKey 必须为 43 个字符。", nameof(encodingAesKey));
            }

            if (resource == null)
            {
                throw new InvalidOperationException("通知中不包含 resource 节点。");
            }

            if (!string.Equals(resource.algorithm, "AEAD_AES_256_GCM", StringComparison.OrdinalIgnoreCase))
            {
                throw new NotSupportedException($"不支持的通知资源加密算法：{resource.algorithm}");
            }

            var key = Convert.FromBase64String(encodingAesKey + "=");
            var encrypted = Convert.FromBase64String(resource.ciphertext);
            const int tagSize = 16;
            if (encrypted.Length <= tagSize)
            {
                throw new CryptographicException("通知资源密文长度无效。");
            }

            var ciphertext = new byte[encrypted.Length - tagSize];
            var tag = new byte[tagSize];
            var plaintext = new byte[ciphertext.Length];
            Buffer.BlockCopy(encrypted, 0, ciphertext, 0, ciphertext.Length);
            Buffer.BlockCopy(encrypted, ciphertext.Length, tag, 0, tag.Length);

            try
            {
                var nonce = Encoding.UTF8.GetBytes(resource.nonce);
                var associatedData = Encoding.UTF8.GetBytes(resource.associated_data ?? string.Empty);
#if NET8_0_OR_GREATER
                using (var aesGcm = new AesGcm(key, tagSize))
#else
                using (var aesGcm = new AesGcm(key))
#endif
                {
                    aesGcm.Decrypt(nonce, ciphertext, tag, plaintext, associatedData);
                }

                return Encoding.UTF8.GetString(plaintext);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(key);
                CryptographicOperations.ZeroMemory(encrypted);
                CryptographicOperations.ZeroMemory(ciphertext);
                CryptographicOperations.ZeroMemory(tag);
                CryptographicOperations.ZeroMemory(plaintext);
            }
#endif
        }

        /// <summary>解密并反序列化业务资源。</summary>
        public T DecryptResource<T>(string encodingAesKey) where T : class
            => JsonConvert.DeserializeObject<T>(DecryptResource(encodingAesKey));
    }

    /// <summary>小程序支付成功通知。</summary>
    public class RequestMessageEvent_MiniProgramPay_Transaction : RequestMessageEvent_MiniProgramPayBase
    {
        /// <summary>SDK 内部事件类型。</summary>
        public override Event Event => Event.pay_transaction;
    }

    /// <summary>小程序退款状态通知。</summary>
    public class RequestMessageEvent_MiniProgramPay_Refund : RequestMessageEvent_MiniProgramPayBase
    {
        /// <summary>SDK 内部事件类型。</summary>
        public override Event Event => Event.pay_refund;
    }
}
