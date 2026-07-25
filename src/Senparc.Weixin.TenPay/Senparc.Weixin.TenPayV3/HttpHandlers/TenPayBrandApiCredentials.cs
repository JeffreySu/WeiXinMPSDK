#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TenPayBrandApiCredentials.cs
    文件功能描述：微信支付品牌 API 鉴权凭据


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 支持微信支付品牌 API 专用 RSA 鉴权与响应验签

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付品牌 API 的 RSA 鉴权凭据。
    /// <para>品牌 API 使用独立的品牌 ID、品牌 API 证书和微信支付公钥，不能与普通商户 API 鉴权参数混用。</para>
    /// </summary>
    public sealed class TenPayBrandApiCredentials
    {
        /// <summary>
        /// 品牌 API 的 Authorization 认证类型。
        /// </summary>
        public const string AuthorizationType =
            "WECHATPAY-BRAND-SHA256-RSA2048";

        /// <summary>
        /// 创建微信支付品牌 API 鉴权凭据。
        /// </summary>
        /// <param name="brandId">微信支付分配的品牌 ID。</param>
        /// <param name="brandSerialNumber">品牌 API 证书序列号。</param>
        /// <param name="brandPrivateKey">品牌 API 证书对应的 RSA 私钥；支持 PKCS#8 PEM 文本或去除 PEM 头尾后的 Base64 DER。</param>
        /// <param name="wechatpayPublicKeyId">品牌 ID 关联的微信支付公钥 ID，同时用于 Wechatpay-Serial 请求头和响应公钥匹配。</param>
        /// <param name="wechatpayPublicKey">微信支付公钥；支持 PEM 文本或去除 PEM 头尾后的 Base64 DER，用于校验品牌 API 响应签名。</param>
        public TenPayBrandApiCredentials(string brandId,
            string brandSerialNumber, string brandPrivateKey,
            string wechatpayPublicKeyId, string wechatpayPublicKey)
        {
            BrandId = RequireValue(brandId, nameof(brandId));
            BrandSerialNumber = RequireValue(brandSerialNumber,
                nameof(brandSerialNumber));
            BrandPrivateKey = NormalizeKey(brandPrivateKey,
                nameof(brandPrivateKey));
            WechatpayPublicKeyId = RequireValue(wechatpayPublicKeyId,
                nameof(wechatpayPublicKeyId));
            WechatpayPublicKey = NormalizeKey(wechatpayPublicKey,
                nameof(wechatpayPublicKey));
        }

        /// <summary>
        /// 微信支付分配的品牌 ID。
        /// </summary>
        public string BrandId { get; }

        /// <summary>
        /// 品牌 API 证书序列号。
        /// </summary>
        public string BrandSerialNumber { get; }

        /// <summary>
        /// 品牌 API 证书对应的 PKCS#8 RSA 私钥 Base64 DER。
        /// </summary>
        public string BrandPrivateKey { get; }

        /// <summary>
        /// 品牌 ID 关联的微信支付公钥 ID。
        /// </summary>
        public string WechatpayPublicKeyId { get; }

        /// <summary>
        /// 用于校验品牌 API 响应签名的微信支付公钥 Base64 DER。
        /// </summary>
        public string WechatpayPublicKey { get; }

        private static string RequireValue(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("品牌 API 鉴权参数不能为空。",
                    parameterName);
            }

            return value;
        }

        private static string NormalizeKey(string value, string parameterName)
        {
            var requiredValue = RequireValue(value, parameterName);
            var builder = new StringBuilder();
            using (var reader = new StringReader(requiredValue))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var trimmedLine = line.Trim();
                    if (trimmedLine.StartsWith("-----",
                        StringComparison.Ordinal))
                    {
                        continue;
                    }

                    builder.Append(trimmedLine);
                }
            }

            var normalized = builder.ToString();
            try
            {
                Convert.FromBase64String(normalized);
            }
            catch (FormatException exception)
            {
                throw new ArgumentException(
                    "品牌 API 密钥必须是有效的 PEM 或 Base64 DER。",
                    parameterName, exception);
            }

            return normalized;
        }
    }
}
