/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MsgAuditFinanceClient.cs
    文件功能描述：企业微信会话内容存档 Finance 原生 SDK 客户端


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会话拉取、消息解密和媒体下载能力

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Senparc.Weixin.Work.AdvancedAPIs.MsgAudit
{
    /// <summary>
    /// 企业微信会话内容存档 Finance 原生 SDK 客户端。
    /// <para>每个客户端独占一个官方 SDK 实例，内部串行化原生调用；使用结束后必须调用 <see cref="Dispose"/>。</para>
    /// <para>官方原生库由企业微信单独提供，不随 Senparc.Weixin.Work NuGet 包分发。</para>
    /// </summary>
    public sealed class MsgAuditFinanceClient : IDisposable
    {
        private readonly object _syncRoot = new object();
        private readonly string _proxy;
        private readonly string _proxyPassword;
        private readonly int _timeoutSeconds;
        private IMsgAuditFinanceNativeApi _nativeApi;
        private IntPtr _sdk;
        private bool _disposed;

        /// <summary>
        /// 创建并初始化企业微信会话内容存档 Finance 客户端。
        /// </summary>
        /// <param name="options">企业 ID、会话内容存档 Secret、原生库路径及网络选项。</param>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> 为空。</exception>
        /// <exception cref="ArgumentException">企业 ID、Secret 或超时配置无效。</exception>
        /// <exception cref="PlatformNotSupportedException">当前操作系统没有企业微信官方 Finance 原生库。</exception>
        /// <exception cref="MsgAuditFinanceException">官方 Finance SDK 初始化失败。</exception>
        public MsgAuditFinanceClient(MsgAuditFinanceOptions options)
            : this(options, CreateNativeApi(options))
        {
        }

        internal MsgAuditFinanceClient(MsgAuditFinanceOptions options,
            IMsgAuditFinanceNativeApi nativeApi)
        {
            ValidateOptions(options);
            _nativeApi = nativeApi ?? throw new ArgumentNullException(nameof(nativeApi));
            _proxy = options.Proxy ?? string.Empty;
            _proxyPassword = options.ProxyPassword ?? string.Empty;
            _timeoutSeconds = options.TimeoutSeconds;

            try
            {
                _sdk = _nativeApi.NewSdk();
                EnsureNativeHandle(_sdk, "NewSdk");
                ThrowIfNativeError(_nativeApi.Init(_sdk, options.CorpId, options.Secret), "Init");
            }
            catch
            {
                ReleaseResources(false);
                throw;
            }
        }

        /// <summary>
        /// 拉取加密会话记录。
        /// </summary>
        /// <param name="sequence">起始消息序号；返回 sequence 之后的消息，首次调用传 0。</param>
        /// <param name="limit">单次拉取条数，必须介于 1 和 1000 之间。</param>
        /// <returns>包含加密随机密钥、消息密文和完整原始 JSON 的强类型结果。</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="limit"/> 不在官方允许范围内。</exception>
        /// <exception cref="MsgAuditFinanceException">原生 SDK 返回非零错误码。</exception>
        public MsgAuditFinanceChatDataResult GetChatData(ulong sequence, uint limit = 1000)
        {
            if (limit == 0 || limit > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(limit), "单次拉取条数必须介于 1 和 1000 之间。");
            }

            lock (_syncRoot)
            {
                ThrowIfDisposed();
                var slice = _nativeApi.NewSlice();
                EnsureNativeHandle(slice, "NewSlice");
                try
                {
                    var errorCode = _nativeApi.GetChatData(_sdk, sequence, limit, _proxy,
                        _proxyPassword, _timeoutSeconds, slice);
                    ThrowIfNativeError(errorCode, "GetChatData");

                    var json = _nativeApi.GetSliceContent(slice);
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        throw new InvalidDataException("Finance SDK 返回的会话内容为空。");
                    }

                    var result = JsonSerializer.Deserialize(json,
                        MsgAuditFinanceJsonSerializerContext.Default.MsgAuditFinanceChatDataResult);
                    if (result == null)
                    {
                        throw new InvalidDataException("无法解析 Finance SDK 返回的会话内容 JSON。");
                    }

                    result.raw_json = json;
                    return result;
                }
                finally
                {
                    _nativeApi.FreeSlice(slice);
                }
            }
        }

        /// <summary>
        /// 使用官方 Finance SDK 解密一条会话消息。
        /// </summary>
        /// <param name="decryptedRandomKey">
        /// 对 <c>encrypt_random_key</c> 完成 Base64 解码并使用企业 RSA 私钥按 PKCS#1 解密后得到的随机密钥。
        /// </param>
        /// <param name="encryptedMessage">拉取结果中的 <c>encrypt_chat_msg</c> 消息密文。</param>
        /// <returns>解密后的完整会话消息 JSON；消息类型字段由企业微信协议定义。</returns>
        /// <exception cref="ArgumentException">随机密钥或消息密文为空。</exception>
        /// <exception cref="MsgAuditFinanceException">原生 SDK 返回非零错误码。</exception>
        public string DecryptData(string decryptedRandomKey, string encryptedMessage)
        {
            ValidateRequiredText(decryptedRandomKey, nameof(decryptedRandomKey));
            ValidateRequiredText(encryptedMessage, nameof(encryptedMessage));

            lock (_syncRoot)
            {
                ThrowIfDisposed();
                var slice = _nativeApi.NewSlice();
                EnsureNativeHandle(slice, "NewSlice");
                try
                {
                    var errorCode = _nativeApi.DecryptData(decryptedRandomKey, encryptedMessage, slice);
                    ThrowIfNativeError(errorCode, "DecryptData");
                    return _nativeApi.GetSliceContent(slice);
                }
                finally
                {
                    _nativeApi.FreeSlice(slice);
                }
            }
        }

        /// <summary>
        /// 下载一个会话媒体文件分片。
        /// </summary>
        /// <param name="sdkFileId">解密后消息 JSON 中的 <c>sdkfileid</c>。</param>
        /// <param name="indexBuffer">上一次响应返回的索引缓冲区；首次请求传空字符串或 <see langword="null"/>。</param>
        /// <returns>当前二进制分片、下一次索引缓冲区及完成状态。</returns>
        /// <exception cref="ArgumentException"><paramref name="sdkFileId"/> 为空。</exception>
        /// <exception cref="MsgAuditFinanceException">原生 SDK 返回非零错误码。</exception>
        public MsgAuditFinanceMediaChunk GetMediaData(string sdkFileId, string indexBuffer = null)
        {
            ValidateRequiredText(sdkFileId, nameof(sdkFileId));

            lock (_syncRoot)
            {
                ThrowIfDisposed();
                var mediaData = _nativeApi.NewMediaData();
                EnsureNativeHandle(mediaData, "NewMediaData");
                try
                {
                    var errorCode = _nativeApi.GetMediaData(_sdk, indexBuffer ?? string.Empty,
                        sdkFileId, _proxy, _proxyPassword, _timeoutSeconds, mediaData);
                    ThrowIfNativeError(errorCode, "GetMediaData");
                    return new MsgAuditFinanceMediaChunk(
                        _nativeApi.GetMediaBytes(mediaData),
                        _nativeApi.GetMediaIndexBuffer(mediaData),
                        _nativeApi.IsMediaDataFinished(mediaData));
                }
                finally
                {
                    _nativeApi.FreeMediaData(mediaData);
                }
            }
        }

        /// <summary>
        /// 连续下载会话媒体文件的全部分片并写入目标流。
        /// </summary>
        /// <param name="sdkFileId">解密后消息 JSON 中的 <c>sdkfileid</c>。</param>
        /// <param name="destination">可写的目标流；本方法不会关闭该流。</param>
        /// <param name="cancellationToken">在每个原生分片请求前检查的取消令牌。</param>
        /// <returns>写入目标流的总字节数。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="destination"/> 为空。</exception>
        /// <exception cref="ArgumentException"><paramref name="sdkFileId"/> 为空。</exception>
        /// <exception cref="ArgumentException"><paramref name="destination"/> 不可写。</exception>
        /// <exception cref="InvalidDataException">原生 SDK 未完成下载但没有返回可继续使用的新索引。</exception>
        public long DownloadMedia(string sdkFileId, Stream destination,
            CancellationToken cancellationToken = default)
        {
            ValidateRequiredText(sdkFileId, nameof(sdkFileId));
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (!destination.CanWrite)
            {
                throw new ArgumentException("目标流必须可写。", nameof(destination));
            }

            var indexBuffer = string.Empty;
            long totalBytes = 0;
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var chunk = GetMediaData(sdkFileId, indexBuffer);
                if (chunk.data.Length > 0)
                {
                    destination.Write(chunk.data, 0, chunk.data.Length);
                    totalBytes = checked(totalBytes + chunk.data.LongLength);
                }

                if (chunk.is_finished)
                {
                    return totalBytes;
                }

                if (string.IsNullOrEmpty(chunk.next_index_buffer) ||
                    string.Equals(chunk.next_index_buffer, indexBuffer, StringComparison.Ordinal))
                {
                    throw new InvalidDataException(
                        "Finance SDK 尚未完成媒体下载，但没有返回可继续使用的新索引缓冲区。");
                }

                indexBuffer = chunk.next_index_buffer;
            }
        }

        /// <summary>
        /// 销毁官方 SDK 实例并释放动态库句柄。
        /// </summary>
        public void Dispose()
        {
            lock (_syncRoot)
            {
                if (_disposed)
                {
                    return;
                }

                ReleaseResources(true);
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 在调用方未显式释放时尽力回收原生资源。
        /// </summary>
        ~MsgAuditFinanceClient()
        {
            ReleaseResources(false);
        }

        private static IMsgAuditFinanceNativeApi CreateNativeApi(MsgAuditFinanceOptions options)
        {
            ValidateOptions(options);
            return new MsgAuditFinanceNativeApi(options.LibraryPath);
        }

        private static void ValidateOptions(MsgAuditFinanceOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ValidateRequiredText(options.CorpId, nameof(options.CorpId));
            ValidateRequiredText(options.Secret, nameof(options.Secret));
            if (options.TimeoutSeconds <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(options.TimeoutSeconds),
                    "原生 SDK 网络请求超时时间必须大于 0 秒。");
            }
        }

        private static void ValidateRequiredText(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("参数不能为空。", parameterName);
            }
        }

        private static void EnsureNativeHandle(IntPtr handle, string operation)
        {
            if (handle == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Finance SDK 的 {operation} 操作返回了空指针。");
            }
        }

        private static void ThrowIfNativeError(int errorCode, string operation)
        {
            if (errorCode != 0)
            {
                throw new MsgAuditFinanceException(errorCode, operation);
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MsgAuditFinanceClient));
            }
        }

        private void ReleaseResources(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            var nativeApi = _nativeApi;
            _nativeApi = null;
            if (nativeApi == null)
            {
                return;
            }

            try
            {
                if (_sdk != IntPtr.Zero)
                {
                    nativeApi.DestroySdk(_sdk);
                    _sdk = IntPtr.Zero;
                }
            }
            catch when (!disposing)
            {
                // 终结器路径不能让原生释放异常终止进程。
            }
            finally
            {
                if (disposing)
                {
                    nativeApi.Dispose();
                }
                else
                {
                    try
                    {
                        nativeApi.Dispose();
                    }
                    catch
                    {
                        // 终结器路径只能尽力释放动态库。
                    }
                }
            }
        }
    }
}
