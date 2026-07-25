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

    文件名：TenPayApiRequest.cs
    文件功能描述：微信支付V3接口请求


    创建标识：Senparc - 20210815

    修改标识：Senparc - 20210822
    修改描述：重构使用ISenparcWeixinSettingForTenpayV3初始化实例

    修改标识：Senparc - 20211225
    修改描述：v0.5.2 发布版本删除调试代码

    修改标识：mojinxun - 20250618
    修改描述：v2.1.0 兼容微信平台证书和微信支付公钥 / PR #3144

    修改标识：Senparc - 20260718
    修改描述：v2.4.1 复用 HttpClient 并按请求隔离超时与资源释放

    修改标识：Senparc - 20260718
    修改描述：v2.5.0 复用序列化设置并支持请求取消与响应头优先读取

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻、商户开户和商户注销接口并增强 HTTP 与通知处理

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 支持微信支付品牌 API 专用鉴权与响应验签

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 限制品牌会员图片大小并避免 multipart 文件缓冲的重复数组分配

----------------------------------------------------------------*/

using Org.BouncyCastle.Crypto.Parameters;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付 API 请求
    /// </summary>
    public class TenPayApiRequest
    {
        private static readonly Newtonsoft.Json.JsonSerializerSettings RequestJsonSerializerSettings = new()
        {
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        };
        private static readonly ConditionalWeakTable<ISenparcWeixinSettingForTenpayV3, Lazy<HttpClient>> HttpClients = new();
        private static readonly ConditionalWeakTable<Action<HttpClient>, ConditionalWeakTable<ISenparcWeixinSettingForTenpayV3, Lazy<HttpClient>>> CustomHttpClients = new();
        private static readonly ConditionalWeakTable<TenPayBrandApiCredentials, Lazy<HttpClient>> BrandHttpClients = new();

        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;
        private readonly TenPayBrandApiCredentials _brandApiCredentials;
        private readonly Action<HttpClient> _setHeaderAction;
        private readonly Lazy<HttpClient> _client;

        public TenPayApiRequest(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null, Action<HttpClient> setHeaderAction = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
            _setHeaderAction = setHeaderAction;
            _client = GetOrCreateHttpClient(_tenpayV3Setting, _setHeaderAction);
        }

        private TenPayApiRequest(TenPayBrandApiCredentials brandApiCredentials)
        {
            _brandApiCredentials = brandApiCredentials ??
                throw new ArgumentNullException(nameof(brandApiCredentials));
            _client = BrandHttpClients.GetValue(_brandApiCredentials,
                credentials => new Lazy<HttpClient>(
                    () => CreateBrandHttpClient(credentials),
                    LazyThreadSafetyMode.ExecutionAndPublication));
        }

        /// <summary>
        /// 创建使用微信支付品牌 API 专用鉴权的请求实例。
        /// </summary>
        /// <param name="brandApiCredentials">品牌 ID、品牌 API 证书和微信支付公钥凭据。</param>
        /// <returns>使用 <c>WECHATPAY-BRAND-SHA256-RSA2048</c> 认证类型的请求实例。</returns>
        public static TenPayApiRequest CreateForBrand(
            TenPayBrandApiCredentials brandApiCredentials)
        {
            return new TenPayApiRequest(brandApiCredentials);
        }

        private static Lazy<HttpClient> GetOrCreateHttpClient(ISenparcWeixinSettingForTenpayV3 setting, Action<HttpClient> setHeaderAction)
        {
            var clients = setHeaderAction == null
                ? HttpClients
                : CustomHttpClients.GetValue(setHeaderAction, _ => new ConditionalWeakTable<ISenparcWeixinSettingForTenpayV3, Lazy<HttpClient>>());

            return clients.GetValue(setting, key => new Lazy<HttpClient>(
                () => CreateHttpClient(key, setHeaderAction),
                LazyThreadSafetyMode.ExecutionAndPublication));
        }

        private static HttpClient CreateHttpClient(ISenparcWeixinSettingForTenpayV3 setting, Action<HttpClient> setHeaderAction)
        {
            var client = new HttpClient(new TenPayHttpHandler(setting))
            {
                // 共享 HttpClient 不修改全局 Timeout，由每次请求自己的 CancellationToken 控制超时。
                Timeout = Timeout.InfiniteTimeSpan
            };

            SetDefaultHeaders(client);
            setHeaderAction?.Invoke(client);
            return client;
        }

        private static HttpClient CreateBrandHttpClient(
            TenPayBrandApiCredentials brandApiCredentials)
        {
            var client = new HttpClient(
                new TenPayHttpHandler(brandApiCredentials))
            {
                Timeout = Timeout.InfiniteTimeSpan
            };

            SetDefaultHeaders(client);
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "Wechatpay-Serial",
                brandApiCredentials.WechatpayPublicKeyId);
            return client;
        }

        private static void SetDefaultHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            var userAgentValues = UserAgentValues.Instance;
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Senparc.Weixin.TenPayV3-C#", userAgentValues.TenPayV3Version));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"(Senparc.Weixin {userAgentValues.SenparcWeixinVersion})"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(".NET", userAgentValues.RuntimeVersion));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"({userAgentValues.OSVersion})"));
        }

        /// <summary>
        /// 设置 HTTP 请求头
        /// </summary>
        /// <param name="client"></param>
        public void SetHeader(HttpClient client)
        {
            SetDefaultHeaders(client);
            if (_brandApiCredentials != null)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    "Wechatpay-Serial",
                    _brandApiCredentials.WechatpayPublicKeyId);
            }
            _setHeaderAction?.Invoke(client);
        }

        /// <summary>
        /// 获取 HttpResponseMessage 对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data">如果为 GET 请求，此参数可为 null</param>
        /// <param name="timeOut"></param>
        /// <param name="requestMethod"></param>
        /// <param name="checkDataNotNull">非 GET 请求情况下，是否强制检查 data 参数不能为 null，默认为 true</param>
        /// <returns>响应对象由调用方负责释放。</returns>
        public Task<HttpResponseMessage> GetHttpResponseMessageAsync(string url, object data, int timeOut = Config.TIME_OUT, ApiRequestMethod requestMethod = ApiRequestMethod.POST, bool checkDataNotNull = true)
        {
            return GetHttpResponseMessageAsync(url, data, CancellationToken.None, HttpCompletionOption.ResponseContentRead, timeOut, requestMethod, checkDataNotNull);
        }

        /// <summary>
        /// 获取 HttpResponseMessage 对象，并支持调用方取消及流式响应。
        /// </summary>
        /// <returns>响应对象由调用方负责释放。</returns>
        public async Task<HttpResponseMessage> GetHttpResponseMessageAsync(
            string url,
            object data,
            CancellationToken cancellationToken,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            int timeOut = Config.TIME_OUT,
            ApiRequestMethod requestMethod = ApiRequestMethod.POST,
            bool checkDataNotNull = true)
        {
            if (timeOut <= 0 && timeOut != Timeout.Infinite)
            {
                throw new ArgumentOutOfRangeException(nameof(timeOut), "超时时间必须大于 0，或使用 Timeout.Infinite。 ");
            }

            using var request = new HttpRequestMessage(GetHttpMethod(requestMethod), url);

            switch (requestMethod)
            {
                case ApiRequestMethod.GET:
                case ApiRequestMethod.DELETE:
                    WeixinTrace.Log(url);
                    break;
                case ApiRequestMethod.POST:
                case ApiRequestMethod.PUT:
                case ApiRequestMethod.PATCH:
                    if (checkDataNotNull)
                    {
                        _ = data ?? throw new ArgumentNullException($"{nameof(data)} 不能为 null！");
                    }

                    string jsonString = data != null
                        ? data.ToJson(false, RequestJsonSerializerSettings)
                        : "";
                    WeixinTrace.SendApiPostDataLog(url, jsonString);
                    request.Content = new StringContent(jsonString, Encoding.UTF8, mediaType: "application/json");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestMethod));
            }

            using var timeoutCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            if (timeOut != Timeout.Infinite)
            {
                timeoutCancellationTokenSource.CancelAfter(timeOut);
            }

            return await _client.Value.SendAsync(request, completionOption, timeoutCancellationTokenSource.Token).ConfigureAwait(false);
        }

        private static HttpMethod GetHttpMethod(ApiRequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case ApiRequestMethod.GET:
                    return HttpMethod.Get;
                case ApiRequestMethod.POST:
                    return HttpMethod.Post;
                case ApiRequestMethod.PUT:
                    return HttpMethod.Put;
                case ApiRequestMethod.PATCH:
                    return HttpMethod.Patch;
                case ApiRequestMethod.DELETE:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestMethod));
            }
        }

        /// <summary>
        /// 发送微信支付 multipart/form-data 文件请求。签名正文按微信支付要求使用 meta JSON，
        /// 而实际 HTTP 正文包含 meta 和 file 两个表单字段。
        /// </summary>
        /// <param name="url">微信支付 API URL。</param>
        /// <param name="fileName">上传文件名。</param>
        /// <param name="fileStream">待上传文件流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <param name="checkSign">是否校验微信支付响应签名。</param>
        /// <param name="createDefaultInstance">返回空响应时创建默认结果的委托。</param>
        /// <returns>反序列化后的微信支付响应结果。</returns>
        public Task<T> RequestMultipartAsync<T>(string url, string fileName, Stream fileStream,
            int timeOut = Config.TIME_OUT, bool checkSign = true, Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            return RequestMultipartAsync(url, fileName, fileStream, CancellationToken.None,
                timeOut, checkSign, createDefaultInstance);
        }

        /// <summary>
        /// 发送支持取消的微信支付 multipart/form-data 文件请求。
        /// </summary>
        /// <param name="url">微信支付 API URL。</param>
        /// <param name="fileName">上传文件名。</param>
        /// <param name="fileStream">待上传文件流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <param name="checkSign">是否校验微信支付响应签名。</param>
        /// <param name="createDefaultInstance">返回空响应时创建默认结果的委托。</param>
        /// <returns>反序列化后的微信支付响应结果。</returns>
        public Task<T> RequestMultipartAsync<T>(string url, string fileName, Stream fileStream,
            CancellationToken cancellationToken, int timeOut = Config.TIME_OUT, bool checkSign = true,
            Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            return RequestMultipartCoreAsync(url, fileName, fileStream, cancellationToken,
                MultipartMetaFieldStyle.FilenameAndSha256, timeOut, checkSign,
                createDefaultInstance);
        }

        /// <summary>
        /// 发送使用 <c>filename</c> 与 <c>sha256</c> 元数据字段、并限制文件大小的
        /// multipart/form-data 文件请求。
        /// </summary>
        internal Task<T> RequestMultipartWithMaxSizeAsync<T>(string url,
            string fileName, Stream fileStream,
            CancellationToken cancellationToken, int maxFileBytes,
            int timeOut = Config.TIME_OUT, bool checkSign = true,
            Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            return RequestMultipartCoreAsync(url, fileName, fileStream,
                cancellationToken, MultipartMetaFieldStyle.FilenameAndSha256,
                timeOut, checkSign, createDefaultInstance, maxFileBytes);
        }

        /// <summary>
        /// 发送使用 <c>file_name</c> 与 <c>file_digest</c> 元数据字段的
        /// multipart/form-data 文件请求。
        /// </summary>
        /// <param name="url">微信支付 API URL。</param>
        /// <param name="fileName">上传文件名。</param>
        /// <param name="fileStream">待上传文件流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <param name="checkSign">是否校验微信支付响应签名。</param>
        /// <param name="createDefaultInstance">返回空响应时创建默认结果的委托。</param>
        /// <returns>反序列化后的微信支付响应结果。</returns>
        internal Task<T> RequestMultipartWithFileDigestAsync<T>(string url,
            string fileName, Stream fileStream, CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT, bool checkSign = true,
            Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            return RequestMultipartCoreAsync(url, fileName, fileStream, cancellationToken,
                MultipartMetaFieldStyle.FileNameAndFileDigest, timeOut,
                checkSign, createDefaultInstance);
        }

        /// <summary>
        /// 发送使用 <c>filename</c> 与 <c>file_digest</c> 元数据字段的
        /// multipart/form-data 文件请求。
        /// </summary>
        internal Task<T> RequestMultipartWithFilenameAndFileDigestAsync<T>(
            string url, string fileName, Stream fileStream,
            CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT, bool checkSign = true,
            Func<T> createDefaultInstance = null,
            int maxFileBytes = 2 * 1024 * 1024)
            where T : ReturnJsonBase, new()
        {
            return RequestMultipartCoreAsync(url, fileName, fileStream,
                cancellationToken,
                MultipartMetaFieldStyle.FilenameAndFileDigest, timeOut,
                checkSign, createDefaultInstance, maxFileBytes);
        }

        private async Task<T> RequestMultipartCoreAsync<T>(string url, string fileName,
            Stream fileStream, CancellationToken cancellationToken,
            MultipartMetaFieldStyle metaFieldStyle, int timeOut, bool checkSign,
            Func<T> createDefaultInstance, int? maxFileBytes = null)
            where T : ReturnJsonBase, new()
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("文件名不能为空。", nameof(fileName));
            }

            _ = fileStream ?? throw new ArgumentNullException(nameof(fileStream));
            if (timeOut <= 0 && timeOut != Timeout.Infinite)
            {
                throw new ArgumentOutOfRangeException(nameof(timeOut), "超时时间必须大于 0，或使用 Timeout.Infinite。 ");
            }
            if (maxFileBytes.HasValue && maxFileBytes.Value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxFileBytes),
                    "文件大小上限必须大于 0。");
            }
            if (maxFileBytes.HasValue && fileStream.CanSeek &&
                fileStream.Length - fileStream.Position > maxFileBytes.Value)
            {
                throw new InvalidDataException(
                    $"上传文件超过允许上限 {maxFileBytes.Value} 字节。");
            }

            T result = null;
            try
            {
                byte[] fileBytes;
                int fileLength;
                using (var memoryStream = new MemoryStream())
                {
                    var buffer = ArrayPool<byte>.Shared.Rent(81920);
                    try
                    {
                        int bytesRead;
                        while ((bytesRead = await fileStream.ReadAsync(buffer,
                            0, buffer.Length, cancellationToken)
                            .ConfigureAwait(false)) > 0)
                        {
                            if (maxFileBytes.HasValue &&
                                memoryStream.Length + bytesRead >
                                maxFileBytes.Value)
                            {
                                throw new InvalidDataException(
                                    $"上传文件超过允许上限 {maxFileBytes.Value} 字节。");
                            }

                            await memoryStream.WriteAsync(buffer, 0,
                                bytesRead, cancellationToken)
                                .ConfigureAwait(false);
                        }

                        fileBytes = memoryStream.GetBuffer();
                        fileLength = checked((int)memoryStream.Length);
                    }
                    finally
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                    }
                }
                string fileSha256;
                using (var sha256 = SHA256.Create())
                {
                    fileSha256 = BitConverter.ToString(sha256.ComputeHash(
                            fileBytes, 0, fileLength))
                        .Replace("-", "")
                        .ToLowerInvariant();
                }

                var metaJson = CreateMultipartMetaJson(fileName, fileSha256,
                    metaFieldStyle);

                using var request = new HttpRequestMessage(HttpMethod.Post, url);
                using var multipart = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(fileBytes, 0,
                    fileLength);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(
                    GetMultipartMediaType(fileName));
                multipart.Add(fileContent, "file", fileName);
                multipart.Add(new StringContent(metaJson, Encoding.UTF8, "application/json"), "meta");
                request.Content = multipart;

                var signatureBody = Convert.ToBase64String(Encoding.UTF8.GetBytes(metaJson));
                request.Headers.TryAddWithoutValidation(TenPayHttpHandler.SignatureBodyHeader, signatureBody);

                using var timeoutCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                if (timeOut != Timeout.Infinite)
                {
                    timeoutCancellationTokenSource.CancelAfter(timeOut);
                }

                using var responseMessage = await _client.Value.SendAsync(
                    request, HttpCompletionOption.ResponseContentRead, timeoutCancellationTokenSource.Token)
                    .ConfigureAwait(false);
                var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var resultCode = TenPayApiResultCode.TryGetCode(responseMessage.StatusCode, content);

                if (resultCode.Success)
                {
                    if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                    {
                        result = new T { VerifySignSuccess = true };
                    }
                    else
                    {
                        result = content.GetObject<T>();
                        if (checkSign)
                        {
                            var timestamp = responseMessage.Headers.GetValues("Wechatpay-Timestamp").First();
                            var nonce = responseMessage.Headers.GetValues("Wechatpay-Nonce").First();
                            var signature = responseMessage.Headers.GetValues("Wechatpay-Signature").First();
                            var serial = responseMessage.Headers.GetValues("Wechatpay-Serial").First();
                            if (_brandApiCredentials != null)
                            {
                                if (!string.Equals(serial,
                                    _brandApiCredentials.WechatpayPublicKeyId,
                                    StringComparison.Ordinal))
                                {
                                    throw new InvalidOperationException(
                                        "品牌 API 响应的微信支付公钥 ID 与配置不匹配。");
                                }

                                result.VerifySignSuccess =
                                    TenPaySignHelper.VerifyTenpaySign(
                                        CertType.RSA, timestamp, nonce,
                                        signature, content,
                                        _brandApiCredentials.WechatpayPublicKey,
                                        true);
                            }
                            else
                            {
                                var publicKey = await TenPayV3InfoCollection
                                    .GetAPIv3PublicKeyAsync(_tenpayV3Setting,
                                        serial, cancellationToken)
                                    .ConfigureAwait(false);

                                if (_tenpayV3Setting.EncryptionType == CertType.SM)
                                {
                                    var publicKeyBytes = Convert.FromBase64String(publicKey);
                                    var parameters = SMPemHelper.LoadPublicKeyToParameters(publicKeyBytes);
                                    result.VerifySignSuccess = GmHelper.VerifySm3WithSm2(parameters,
                                        $"{timestamp}\n{nonce}\n{content}\n", signature);
                                }
                                else
                                {
                                    result.VerifySignSuccess = TenPaySignHelper.VerifyTenpaySign(
                                        _tenpayV3Setting.EncryptionType.Value, timestamp, nonce, signature,
                                        content, publicKey, TenPaySignHelper.IsPublicKey(serial));
                                }
                            }
                        }
                    }
                }
                else
                {
                    result = createDefaultInstance?.Invoke() ?? GetInstance<T>(true);
                    resultCode.Additional = content;
                }

                result.ResultCode = resultCode;
                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                result = createDefaultInstance?.Invoke() ?? GetInstance<T>(false);
                if (result != null)
                {
                    result.ResultCode = new TenPayApiResultCode { ErrorMessage = ex.Message };
                }

                return result;
            }
        }

        private enum MultipartMetaFieldStyle
        {
            FilenameAndSha256,
            FileNameAndFileDigest,
            FilenameAndFileDigest
        }

        private static string CreateMultipartMetaJson(string fileName,
            string fileSha256, MultipartMetaFieldStyle metaFieldStyle)
        {
            switch (metaFieldStyle)
            {
                case MultipartMetaFieldStyle.FileNameAndFileDigest:
                    return new
                    {
                        file_name = fileName,
                        file_digest = fileSha256
                    }.ToJson(false, RequestJsonSerializerSettings);
                case MultipartMetaFieldStyle.FilenameAndFileDigest:
                    return new
                    {
                        filename = fileName,
                        file_digest = fileSha256
                    }.ToJson(false, RequestJsonSerializerSettings);
                default:
                    return new
                    {
                        filename = fileName,
                        sha256 = fileSha256
                    }.ToJson(false, RequestJsonSerializerSettings);
            }
        }

        private static string GetMultipartMediaType(string fileName)
        {
            switch (Path.GetExtension(fileName)?.ToLowerInvariant())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".png":
                    return "image/png";
                case ".pdf":
                    return "application/pdf";
                case ".avi":
                    return "video/x-msvideo";
                case ".wmv":
                    return "video/x-ms-wmv";
                case ".mpeg":
                case ".mpg":
                    return "video/mpeg";
                case ".mp4":
                case ".m4v":
                    return "video/mp4";
                case ".mov":
                    return "video/quicktime";
                case ".mkv":
                    return "video/x-matroska";
                case ".flv":
                    return "video/x-flv";
                case ".f4v":
                    return "video/x-f4v";
                case ".rmvb":
                    return "application/vnd.rn-realmedia-vbr";
                default:
                    return "application/octet-stream";
            }
        }

        /// <summary>
        /// 请求参数，获取结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data">如果为 GET 请求，此参数可为 null</param>
        /// <param name="timeOut"></param>
        /// <param name="requestMethod"></param>
        /// <param name="checkSign"></param>
        /// <param name="createDefaultInstance"></param>
        /// <returns></returns>
        public Task<T> RequestAsync<T>(string url, object data, int timeOut = Config.TIME_OUT, ApiRequestMethod requestMethod = ApiRequestMethod.POST, bool checkSign = true, Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            return RequestAsyncCore(url, data, CancellationToken.None, timeOut, requestMethod,
                checkSign, createDefaultInstance, true);
        }

        /// <summary>
        /// 请求参数，获取结果，并支持调用方取消。
        /// </summary>
        public Task<T> RequestAsync<T>(string url, object data, CancellationToken cancellationToken, int timeOut = Config.TIME_OUT, ApiRequestMethod requestMethod = ApiRequestMethod.POST, bool checkSign = true, Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            return RequestAsyncCore(url, data, cancellationToken, timeOut, requestMethod,
                checkSign, createDefaultInstance, true);
        }

        /// <summary>
        /// 发送不包含请求正文的微信支付请求。
        /// </summary>
        /// <typeparam name="T">微信支付返回结果类型。</typeparam>
        /// <param name="url">微信支付 API URL。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <param name="requestMethod">HTTP 请求方法，通常为 POST 或 DELETE。</param>
        /// <param name="checkSign">是否校验微信支付响应签名。</param>
        /// <param name="createDefaultInstance">返回空响应或失败时创建结果实例的委托。</param>
        /// <returns>反序列化后的微信支付响应结果。</returns>
        public Task<T> RequestWithoutBodyAsync<T>(string url, int timeOut = Config.TIME_OUT,
            ApiRequestMethod requestMethod = ApiRequestMethod.POST, bool checkSign = true,
            Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            return RequestWithoutBodyAsync(url, CancellationToken.None, timeOut, requestMethod,
                checkSign, createDefaultInstance);
        }

        /// <summary>
        /// 发送支持取消且不包含请求正文的微信支付请求。
        /// </summary>
        /// <typeparam name="T">微信支付返回结果类型。</typeparam>
        /// <param name="url">微信支付 API URL。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <param name="requestMethod">HTTP 请求方法，通常为 POST 或 DELETE。</param>
        /// <param name="checkSign">是否校验微信支付响应签名。</param>
        /// <param name="createDefaultInstance">返回空响应或失败时创建结果实例的委托。</param>
        /// <returns>反序列化后的微信支付响应结果。</returns>
        public Task<T> RequestWithoutBodyAsync<T>(string url,
            CancellationToken cancellationToken, int timeOut = Config.TIME_OUT,
            ApiRequestMethod requestMethod = ApiRequestMethod.POST, bool checkSign = true,
            Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            return RequestAsyncCore(url, null, cancellationToken, timeOut, requestMethod,
                checkSign, createDefaultInstance, false);
        }

        private async Task<T> RequestAsyncCore<T>(string url, object data,
            CancellationToken cancellationToken, int timeOut, ApiRequestMethod requestMethod,
            bool checkSign, Func<T> createDefaultInstance, bool checkDataNotNull)
            where T : ReturnJsonBase, new()
        {
            T result = null;

            try
            {
                using HttpResponseMessage responseMessage = await GetHttpResponseMessageAsync(
                    url, data, cancellationToken, HttpCompletionOption.ResponseContentRead, timeOut,
                    requestMethod, checkDataNotNull).ConfigureAwait(false);

                //获取响应结果
                string content = await responseMessage.Content.ReadAsStringAsync();//TODO:如果不正确也要返回详情

#if DEBUG
                Console.WriteLine("Content:" + content + ",,Headers:" + responseMessage.Headers.ToString());
#endif

                //检查响应代码
                TenPayApiResultCode resultCode = TenPayApiResultCode.TryGetCode(responseMessage.StatusCode, content);

                if (resultCode.Success)
                {
                    if (resultCode.StateCode == ((int)HttpStatusCode.NoContent).ToString())
                    {
                        result = new T();
                        result.VerifySignSuccess = true;
                    }
                    else
                    {
                        result = content.GetObject<T>();

                        if (checkSign)
                        {
                            try
                            {
                                var wechatpayTimestamp = responseMessage.Headers.GetValues("Wechatpay-Timestamp").First();
                                var wechatpayNonce = responseMessage.Headers.GetValues("Wechatpay-Nonce").First();
                                var wechatpaySignatureBase64 = responseMessage.Headers.GetValues("Wechatpay-Signature").First();
                                var wechatpaySerial = responseMessage.Headers.GetValues("Wechatpay-Serial").First();
                                if (_brandApiCredentials != null)
                                {
                                    if (!string.Equals(wechatpaySerial,
                                            _brandApiCredentials.WechatpayPublicKeyId,
                                            StringComparison.Ordinal))
                                    {
                                        throw new InvalidOperationException(
                                            "品牌 API 响应的微信支付公钥 ID 与配置不匹配。");
                                    }

                                    result.VerifySignSuccess =
                                        TenPaySignHelper.VerifyTenpaySign(
                                            CertType.RSA, wechatpayTimestamp,
                                            wechatpayNonce,
                                            wechatpaySignatureBase64, content,
                                            _brandApiCredentials.WechatpayPublicKey,
                                            true);
                                }
                                else
                                {
                                    var pubKey = await TenPayV3InfoCollection
                                        .GetAPIv3PublicKeyAsync(
                                            _tenpayV3Setting, wechatpaySerial,
                                            cancellationToken)
                                        .ConfigureAwait(false);
                                    if (_tenpayV3Setting.EncryptionType ==
                                        CertType.SM)
                                    {
                                        byte[] pubKeyBytes =
                                            Convert.FromBase64String(pubKey);
                                        ECPublicKeyParameters
                                            eCPublicKeyParameters =
                                                SMPemHelper
                                                    .LoadPublicKeyToParameters(
                                                        pubKeyBytes);

                                        string contentForSign =
                                            $"{wechatpayTimestamp}\n{wechatpayNonce}\n{content}\n";

                                        result.VerifySignSuccess =
                                            GmHelper.VerifySm3WithSm2(
                                                eCPublicKeyParameters,
                                                contentForSign,
                                                wechatpaySignatureBase64);
                                    }
                                    else
                                    {
                                        var isTenpayPubKey =
                                            TenPaySignHelper.IsPublicKey(
                                                wechatpaySerial);
                                        result.VerifySignSuccess =
                                            TenPaySignHelper
                                                .VerifyTenpaySign(
                                                    _tenpayV3Setting
                                                        .EncryptionType.Value,
                                                    wechatpayTimestamp,
                                                    wechatpayNonce,
                                                    wechatpaySignatureBase64,
                                                    content, pubKey,
                                                    isTenpayPubKey);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new TenpayApiRequestException("RequestAsync 签名验证失败：" + ex.Message, ex);
                            }
                        }
                    }
                }
                else
                {
                    result = createDefaultInstance?.Invoke() ?? GetInstance<T>(true);
                    resultCode.Additional = content;
                }
                //T result = resultCode.Success ? (await responseMessage.Content.ReadAsStringAsync()).GetObject<T>() : new T();
                result.ResultCode = resultCode;

                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                result = createDefaultInstance?.Invoke() ?? GetInstance<T>(false);
                if (result != null)
                {
                    result.ResultCode = new() { ErrorMessage = ex.Message };
                }

                return result;
            }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="throwIfFaild"></param>
        /// <returns></returns>
        private T GetInstance<T>(bool throwIfFaild)
            where T : ReturnJsonBase
        {
            if (typeof(T).IsClass)
            {
                return Senparc.CO2NET.Helpers.ReflectionHelper.CreateInstance<T>(typeof(T).FullName, typeof(T).Assembly.GetName().Name);
            }
            else if (throwIfFaild)
            {
                throw new TenpayApiRequestException("GetInstance 失败，此类型无法自动生成：" + typeof(T).FullName);
            }
            return null;
        }
    }
}
