#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：TenPayNotifyHandler.cs
    文件功能描述：微信支付V3 回调请求handler

    创建标识：Senparc - 20210819

    修改标识：Senparc - 20210819
    修改描述：重构使用TenPaySignHelper类验证签名

    修改标识：Senparc - 20240802
    修改描述：v1.4.2 完善 SM 相关方法

    修改标识：Senparc - 20241020
    修改描述：v1.6.5 修改 SM 证书判断逻辑，向下兼容未升级 appsettings.json 的系统 #3084 感谢 @WXJDLM

    修改标识：mojinxun - 20250618
    修改描述：v2.1.0 兼容微信平台证书和微信支付公钥 / PR #3144

    修改标识：Senparc - 20251126
    修改描述：v2.4.0 修复 TenPayNotifyHandler Body 读取 bug，启用 EnableBuffering() 允许重复读取请求体 / Issue #3220

    修改标识：Senparc - 20260523
    修改描述：补充更新日志，完善文件头修改记录

    修改标识：Senparc - 20260718
    修改描述：v2.5.0 新增限长、可取消的异步通知正文读取

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 公开通知 ID 和原始正文，便于调用方实现幂等处理

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 新增品牌 API 专用密钥解密及微信支付公钥验签入口

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 按官方处理顺序先验签再解密品牌回调

----------------------------------------------------------------*/

using Microsoft.AspNetCore.Http;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付通知消息处理器
    /// </summary>
    public class TenPayNotifyHandler
    {
        /// <summary>
        /// 默认允许的通知请求体上限（1 MiB）。
        /// </summary>
        public const int DefaultMaxBodyBytes = 1024 * 1024;

        readonly private NotifyRequest NotifyRequest;
        readonly private string Body;

        /// <summary>
        /// 微信支付通知信封。可读取官方通知 ID、事件类型和加密资源元数据。
        /// </summary>
        public NotifyRequest Notification => NotifyRequest;

        /// <summary>
        /// 微信支付通知的唯一 ID，可作为调用方幂等去重键。
        /// </summary>
        public string NotificationId => NotifyRequest?.id;

        /// <summary>
        /// 原始通知正文。验签、审计或延迟处理时可复用，不会再次读取请求流。
        /// </summary>
        public string RawBody => Body;

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting { get; }
        private HttpContext _httpContext;


        /// <summary>
        /// 构造函数
        /// 注意:.NetCore环境必须传入HttpContext实例，不能传Null，这个接口调试特别困难，千万别出错！
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public TenPayNotifyHandler(HttpContext httpContext, ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
            : this(
                httpContext,
                senparcWeixinSettingForTenpayV3,
                ReadBodyAsync(httpContext, null, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult())
        {
        }

        /// <summary>
        /// 使用自定义请求体上限的兼容构造函数。新代码优先使用 <see cref="CreateAsync"/>。
        /// </summary>
        public TenPayNotifyHandler(HttpContext httpContext, ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3, int maxBodyBytes)
            : this(
                httpContext,
                senparcWeixinSettingForTenpayV3,
                ReadBodyAsync(httpContext, maxBodyBytes, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult())
        {
        }

        private TenPayNotifyHandler(
            HttpContext httpContext,
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3,
            NotificationBody notificationBody)
        {
            _ = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _httpContext = httpContext;
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;

            if (!_tenpayV3Setting.EncryptionType.HasValue)
            {
                throw new Senparc.Weixin.Exceptions.WeixinException("没有设置证书加密类型（EncryptionType）");
            }

            Body = notificationBody.Body;
            NotifyRequest = notificationBody.NotifyRequest;
        }

        /// <summary>
        /// 异步创建通知处理器，支持请求取消并限制请求体大小。
        /// </summary>
        public static async Task<TenPayNotifyHandler> CreateAsync(
            HttpContext httpContext,
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null,
            int maxBodyBytes = DefaultMaxBodyBytes,
            CancellationToken cancellationToken = default)
        {
            var notificationBody = await ReadBodyAsync(httpContext, maxBodyBytes, cancellationToken).ConfigureAwait(false);
            return new TenPayNotifyHandler(httpContext, senparcWeixinSettingForTenpayV3, notificationBody);
        }

        private static async Task<NotificationBody> ReadBodyAsync(
            HttpContext httpContext,
            int? maxBodyBytes,
            CancellationToken cancellationToken)
        {
            _ = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            if (maxBodyBytes.HasValue && maxBodyBytes.Value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxBodyBytes), "请求体上限必须大于 0。");
            }

            var request = httpContext.Request;
            if (request.Method != "POST" && request.Method != "PUT" && request.Method != "PATCH")
            {
                return NotificationBody.Empty;
            }

            if (maxBodyBytes.HasValue && request.ContentLength > maxBodyBytes.Value)
            {
                throw new InvalidDataException($"微信支付通知请求体超过允许上限 {maxBodyBytes} 字节。");
            }

            if (maxBodyBytes.HasValue)
            {
                request.EnableBuffering(bufferThreshold: 30 * 1024, bufferLimit: maxBodyBytes.Value);
            }
            else
            {
                request.EnableBuffering(bufferThreshold: 30 * 1024);
            }
            request.Body.Position = 0;

            using (var linkedCancellation = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken,
                httpContext.RequestAborted))
            using (var bodyBuffer = new MemoryStream(request.ContentLength.HasValue
                ? (int)Math.Min(request.ContentLength.Value, maxBodyBytes ?? int.MaxValue)
                : 0))
            {
                var buffer = ArrayPool<byte>.Shared.Rent(Math.Min(81920, maxBodyBytes ?? 81920));
                try
                {
                    var totalBytes = 0;
                    int bytesRead;
                    while ((bytesRead = await request.Body.ReadAsync(
                        buffer, 0, buffer.Length, linkedCancellation.Token).ConfigureAwait(false)) > 0)
                    {
                        totalBytes += bytesRead;
                        if (maxBodyBytes.HasValue && totalBytes > maxBodyBytes.Value)
                        {
                            throw new InvalidDataException($"微信支付通知请求体超过允许上限 {maxBodyBytes} 字节。");
                        }

                        await bodyBuffer.WriteAsync(buffer, 0, bytesRead, linkedCancellation.Token).ConfigureAwait(false);
                    }

                    bodyBuffer.TryGetBuffer(out var bodySegment);
                    var body = Encoding.UTF8.GetString(bodySegment.Array, bodySegment.Offset, totalBytes);
                    return new NotificationBody(body, body.GetObject<NotifyRequest>());
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                    request.Body.Position = 0;
                }
            }
        }

        private sealed class NotificationBody
        {
            internal static readonly NotificationBody Empty = new NotificationBody(null, null);

            internal string Body { get; }
            internal NotifyRequest NotifyRequest { get; }

            internal NotificationBody(string body, NotifyRequest notifyRequest)
            {
                Body = body;
                NotifyRequest = notifyRequest;
            }
        }

        /// <summary>
        /// 将返回的结果中的ciphertext进行AEAD_AES_256_GCM解反序列化为实体
        /// 签名规则见微信官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_5.shtml
        /// </summary>
        /// <param name="aes_key">这里需要传入apiv3秘钥进行AEAD_AES_256_GCM解密 可空</param>
        /// <param name="nonce">加密的随机串 可空</param>
        /// <param name="associated_data">附加数据包 可空</param>
        /// <returns></returns>
        // TODO: 本方法持续测试
        [Obsolete($"请使用 {nameof(DecryptGetObjectAsync)} 方法", true)]
        public async Task<T> AesGcmDecryptGetObjectAsync<T>(string aes_key = null, string nonce = null, string associated_data = null, bool isTenPayPubKey = false) where T : ReturnJsonBase, new()
        {
            return await AesGcmDecryptGetObjectAsync<T>(nonce: nonce, associated_data: associated_data, isTenPayPubKey);
        }

        /// <summary>
        /// 将返回的结果中的ciphertext进行AEAD_AES_256_GCM解反序列化为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nonce">加密的随机串 可空</param>
        /// <param name="associated_data">附加数据包 可空</param>
        /// <param name="isPublicKey">是否为微信支付公钥</param>
        /// <returns></returns>
        private async Task<T> AesGcmDecryptGetObjectAsync<T>(string nonce = null, string associated_data = null, bool isTenPayPubKey = false) where T : ReturnJsonBase, new()
        {
            var aes_key = _tenpayV3Setting.TenPayV3_APIv3Key;
            nonce ??= NotifyRequest.resource.nonce;
            associated_data ??= NotifyRequest.resource.associated_data;

            var decrypted_string = SecurityHelper.AesGcmDecryptCiphertext(aes_key, nonce, associated_data, NotifyRequest.resource.ciphertext);

            T result = decrypted_string.GetObject<T>();

            //验证请求签名
            var wechatpayTimestamp = _httpContext.Request.Headers?["Wechatpay-Timestamp"];
            var wechatpayNonce = _httpContext.Request.Headers?["Wechatpay-Nonce"];
            var wechatpaySignature = _httpContext.Request.Headers?["Wechatpay-Signature"];
            var wechatpaySerial = _httpContext.Request.Headers?["Wechatpay-Serial"];

            result.VerifySignSuccess = await TenPaySignHelper.VerifyTenpaySign(wechatpayTimestamp, wechatpayNonce, wechatpaySignature, Body, wechatpaySerial, _tenpayV3Setting);
            result.ResultCode = new TenPayApiResultCode($"{_httpContext.Response.StatusCode} / {_httpContext.Request.Method}", "", "", "", result.VerifySignSuccess == true);

            return result;
        }

        /// <summary>
        /// 将返回的结果中的ciphertext进行SM4/GCM/NoPadding解反序列化为实体
        /// 签名规则见微信官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_5.shtml
        /// </summary>
        /// <param name="aes_key">这里需要传入apiv3秘钥进行SM4/GCM/NoPadding解密 可空</param>
        /// <param name="nonce">加密的随机串 可空</param>
        /// <param name="associated_data">附加数据包 可空</param>
        /// <param name="isPublicKey">是否为微信支付公钥</param>
        /// <returns></returns>
        // TODO: 本方法持续测试
        private async Task<T> Sm4GcmDecryptGetObjectAsync<T>(string nonce = null, string associated_data = null, bool isPublicKey = false) where T : ReturnJsonBase, new()
        {
            var aes_key = _tenpayV3Setting.TenPayV3_APIv3Key;
            nonce ??= NotifyRequest.resource.nonce;
            associated_data ??= NotifyRequest.resource.associated_data;

            var decrypted_string = GmHelper.Sm4DecryptGCM(aes_key, nonce, associated_data, NotifyRequest.resource.ciphertext);

            T result = decrypted_string.GetObject<T>();

            //验证请求签名
            var wechatpayTimestamp = _httpContext.Request.Headers?["Wechatpay-Timestamp"];
            var wechatpayNonce = _httpContext.Request.Headers?["Wechatpay-Nonce"];
            var wechatpaySignature = _httpContext.Request.Headers?["Wechatpay-Signature"];
            var wechatpaySerial = _httpContext.Request.Headers?["Wechatpay-Serial"];

            result.VerifySignSuccess = await TenPaySignHelper.VerifyTenpaySign(wechatpayTimestamp, wechatpayNonce, wechatpaySignature, Body, wechatpaySerial, this._tenpayV3Setting);
            result.ResultCode = new TenPayApiResultCode($"{_httpContext.Response.StatusCode} / {_httpContext.Request.Method}", "", "", "", result.VerifySignSuccess == true);

            return result;
        }

        /// <summary>
        /// 将返回的结果中的ciphertext进行解密反序列化为实体
        /// 签名规则见微信官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_5.shtml
        /// </summary>
        /// <param name="aes_key">这里需要传入apiv3秘钥进行解密 可空</param>
        /// <param name="nonce">加密的随机串 可空</param>
        /// <param name="associated_data">附加数据包 可空</param>
        /// <returns></returns>
        // TODO: 本方法持续测试
        public async Task<T> DecryptGetObjectAsync<T>(bool isPublicKey,/*string aes_key = null, */string nonce = null, string associated_data = null) where T : ReturnJsonBase, new()
        {
            if (_tenpayV3Setting.EncryptionType == CertType.SM)
            {
                return await Sm4GcmDecryptGetObjectAsync<T>(nonce, associated_data, isPublicKey);
            }
            else
            {
                return await AesGcmDecryptGetObjectAsync<T>(nonce, associated_data, isTenPayPubKey: isPublicKey);
            }
        }

        /// <summary>
        /// 使用品牌 API 专用密钥解密通知资源，并使用品牌关联的微信支付公钥验签。
        /// </summary>
        /// <typeparam name="T">解密后的强类型通知模型。</typeparam>
        /// <param name="brandApiKey">品牌 API 密钥。</param>
        /// <param name="brandApiCredentials">品牌 API 鉴权凭据，其中包含回调验签所需的微信支付公钥。</param>
        /// <param name="nonce">加密随机串；为空时读取通知资源中的值。</param>
        /// <param name="associatedData">附加数据；为空时读取通知资源中的值。</param>
        /// <returns>验签并解密后的品牌通知。</returns>
        public Task<T> DecryptBrandGetObjectAsync<T>(string brandApiKey,
            TenPayBrandApiCredentials brandApiCredentials,
            string nonce = null, string associatedData = null)
            where T : ReturnJsonBase, new()
        {
            if (string.IsNullOrWhiteSpace(brandApiKey))
            {
                throw new ArgumentException("品牌 API 密钥不能为空。",
                    nameof(brandApiKey));
            }

            _ = brandApiCredentials ?? throw new ArgumentNullException(
                nameof(brandApiCredentials));
            var resource = NotifyRequest?.resource ?? throw new InvalidDataException(
                "通知正文中缺少加密资源 resource。");

            var wechatpayTimestamp =
                _httpContext.Request.Headers?["Wechatpay-Timestamp"].ToString();
            var wechatpayNonce =
                _httpContext.Request.Headers?["Wechatpay-Nonce"].ToString();
            var wechatpaySignature =
                _httpContext.Request.Headers?["Wechatpay-Signature"].ToString();
            var wechatpaySerial =
                _httpContext.Request.Headers?["Wechatpay-Serial"].ToString();

            if (!string.Equals(wechatpaySerial,
                brandApiCredentials.WechatpayPublicKeyId,
                StringComparison.Ordinal))
            {
                throw new InvalidOperationException(
                    "品牌 API 通知的微信支付公钥 ID 与配置不匹配。");
            }

            var verifySignSuccess = TenPaySignHelper.VerifyTenpaySign(
                CertType.RSA, wechatpayTimestamp, wechatpayNonce,
                wechatpaySignature, Body,
                brandApiCredentials.WechatpayPublicKey, true);
            var decryptedString = SecurityHelper.AesGcmDecryptCiphertext(
                brandApiKey, nonce ?? resource.nonce,
                associatedData ?? resource.associated_data,
                resource.ciphertext);
            var result = decryptedString.GetObject<T>();
            result.VerifySignSuccess = verifySignSuccess;
            result.ResultCode = new TenPayApiResultCode(
                $"{_httpContext.Response.StatusCode} / {_httpContext.Request.Method}",
                "", "", "", result.VerifySignSuccess == true);

            return Task.FromResult(result);
        }
    }
}
