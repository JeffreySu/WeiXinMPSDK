/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：PayToolSignatureHelper.cs
    文件功能描述：企业微信收款工具请求签名辅助方法


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 实现收款工具递归参数签名和防重放字段补全

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Senparc.Weixin.Work.AdvancedAPIs.PayTool
{
    /// <summary>
    /// 企业微信收款工具 HMAC-SHA256 签名辅助方法。
    /// </summary>
    public static class PayToolSignatureHelper
    {
        /// <summary>
        /// 为收款工具请求补齐随机串、Unix 时间戳和数字签名。
        /// 已提供签名时不会重新签名，并要求调用方同时提供原签名对应的随机串和时间戳。
        /// </summary>
        /// <param name="request">需要签名的收款工具请求。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；请求未预签名时必填。</param>
        public static void PrepareRequest(PayToolSignedRequestBase request,
            string payToolApiSecret)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!string.IsNullOrEmpty(request.sig))
            {
                if (string.IsNullOrEmpty(request.nonce_str) || request.ts <= 0)
                {
                    throw new ArgumentException("预签名请求必须同时提供 nonce_str 和 ts。",
                        nameof(request));
                }

                return;
            }

            if (string.IsNullOrEmpty(payToolApiSecret))
            {
                throw new ArgumentException("请求未提供 sig 时必须提供收银台 API 调用密钥。",
                    nameof(payToolApiSecret));
            }

            if (string.IsNullOrEmpty(request.nonce_str))
            {
                request.nonce_str = Guid.NewGuid().ToString("N");
            }

            if (request.ts <= 0)
            {
                request.ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }

            request.sig = CreateSignature(request, payToolApiSecret);
        }

        /// <summary>
        /// 按企业微信规则递归展开非空叶子参数、按完整 key=value 字符串升序排列，
        /// 再使用收银台 API 调用密钥计算 HMAC-SHA256 并进行 Base64 编码。
        /// </summary>
        /// <param name="request">请求对象；任意层级名为 sig 的字段不参与签名。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥。</param>
        /// <returns>Base64 编码的数字签名。</returns>
        public static string CreateSignature(object request, string payToolApiSecret)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrEmpty(payToolApiSecret))
            {
                throw new ArgumentException("收银台 API 调用密钥不能为空。",
                    nameof(payToolApiSecret));
            }

            var pairs = new List<string>();
            CollectPairs(null, JToken.FromObject(request), pairs);
            pairs.Sort(StringComparer.Ordinal);
            var signingText = string.Join("&", pairs);

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(payToolApiSecret)))
            {
                return Convert.ToBase64String(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(signingText)));
            }
        }

        private static void CollectPairs(string key, JToken token, ICollection<string> pairs)
        {
            if (token == null || token.Type == JTokenType.Null ||
                token.Type == JTokenType.Undefined)
            {
                return;
            }

            if (token.Type == JTokenType.Object)
            {
                foreach (var property in ((JObject)token).Properties())
                {
                    if (!string.Equals(property.Name, "sig", StringComparison.Ordinal))
                    {
                        CollectPairs(property.Name, property.Value, pairs);
                    }
                }

                return;
            }

            if (token.Type == JTokenType.Array)
            {
                foreach (var item in (JArray)token)
                {
                    CollectPairs(key, item, pairs);
                }

                return;
            }

            var value = GetScalarValue((JValue)token);
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                pairs.Add(key + "=" + value);
            }
        }

        private static string GetScalarValue(JValue value)
        {
            if (value.Value == null)
            {
                return null;
            }

            switch (value.Type)
            {
                case JTokenType.Boolean:
                    return (bool)value.Value ? "true" : "false";
                case JTokenType.Bytes:
                    return Convert.ToBase64String((byte[])value.Value);
                case JTokenType.Date:
                    return Convert.ToDateTime(value.Value, CultureInfo.InvariantCulture)
                        .ToString("o", CultureInfo.InvariantCulture);
                default:
                    return Convert.ToString(value.Value, CultureInfo.InvariantCulture);
            }
        }
    }
}
