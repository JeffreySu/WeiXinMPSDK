/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MsgAuditFinanceNativeApi.cs
    文件功能描述：企业微信会话内容存档 Finance 原生 SDK 动态加载适配器


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐 Windows、Linux Finance 原生函数绑定和资源释放

----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Senparc.Weixin.Work.AdvancedAPIs.MsgAudit
{
    /// <summary>
    /// Finance 原生 SDK 的可测试调用边界；每个 New 操作必须与对应的 Free 或 Destroy 操作配对。
    /// </summary>
    internal interface IMsgAuditFinanceNativeApi : IDisposable
    {
        /// <summary>创建 Finance SDK 实例。</summary>
        IntPtr NewSdk();

        /// <summary>初始化 Finance SDK 实例。</summary>
        int Init(IntPtr sdk, string corpId, string secret);

        /// <summary>销毁 Finance SDK 实例。</summary>
        void DestroySdk(IntPtr sdk);

        /// <summary>创建原生字符串缓冲区。</summary>
        IntPtr NewSlice();

        /// <summary>释放原生字符串缓冲区。</summary>
        void FreeSlice(IntPtr slice);

        /// <summary>拉取加密会话记录。</summary>
        int GetChatData(IntPtr sdk, ulong sequence, uint limit, string proxy,
            string proxyPassword, int timeoutSeconds, IntPtr slice);

        /// <summary>解密会话记录。</summary>
        int DecryptData(string decryptedRandomKey, string encryptedMessage, IntPtr slice);

        /// <summary>读取原生字符串缓冲区中的 UTF-8 内容。</summary>
        string GetSliceContent(IntPtr slice);

        /// <summary>创建原生媒体缓冲区。</summary>
        IntPtr NewMediaData();

        /// <summary>释放原生媒体缓冲区。</summary>
        void FreeMediaData(IntPtr mediaData);

        /// <summary>拉取一个媒体文件分片。</summary>
        int GetMediaData(IntPtr sdk, string indexBuffer, string sdkFileId, string proxy,
            string proxyPassword, int timeoutSeconds, IntPtr mediaData);

        /// <summary>读取下一次媒体分片请求使用的索引缓冲区。</summary>
        string GetMediaIndexBuffer(IntPtr mediaData);

        /// <summary>读取当前媒体分片的二进制数据。</summary>
        byte[] GetMediaBytes(IntPtr mediaData);

        /// <summary>读取媒体文件是否已经下载完成。</summary>
        bool IsMediaDataFinished(IntPtr mediaData);
    }

    /// <summary>
    /// 使用官方 C ABI 动态加载 Finance 原生库的实现。
    /// </summary>
    internal sealed class MsgAuditFinanceNativeApi : IMsgAuditFinanceNativeApi
    {
        private const string WindowsLibraryName = "WeWorkFinanceSdk_C.dll";
        private const string LinuxLibraryName = "libWeWorkFinanceSdk_C.so";

        private readonly FinanceLibraryHandle _library;
        private readonly NewSdkDelegate _newSdk;
        private readonly InitDelegate _init;
        private readonly DestroySdkDelegate _destroySdk;
        private readonly NewSliceDelegate _newSlice;
        private readonly FreeSliceDelegate _freeSlice;
        private readonly GetChatDataDelegate _getChatData;
        private readonly DecryptDataDelegate _decryptData;
        private readonly GetContentFromSliceDelegate _getContentFromSlice;
        private readonly GetSliceLenDelegate _getSliceLen;
        private readonly NewMediaDataDelegate _newMediaData;
        private readonly FreeMediaDataDelegate _freeMediaData;
        private readonly GetMediaDataDelegate _getMediaData;
        private readonly GetOutIndexBufDelegate _getOutIndexBuf;
        private readonly GetDataDelegate _getData;
        private readonly GetIndexLenDelegate _getIndexLen;
        private readonly GetDataLenDelegate _getDataLen;
        private readonly IsMediaDataFinishDelegate _isMediaDataFinish;
        private bool _disposed;

        /// <summary>
        /// 加载指定的官方 Finance 原生库并解析全部必需导出函数。
        /// </summary>
        /// <param name="libraryPath">原生库路径；为空时使用当前平台的官方文件名。</param>
        public MsgAuditFinanceNativeApi(string libraryPath)
        {
            var effectivePath = string.IsNullOrWhiteSpace(libraryPath)
                ? GetDefaultLibraryName()
                : libraryPath;

            _library = FinanceLibraryHandle.Load(effectivePath);
            try
            {
                _newSdk = _library.GetDelegate<NewSdkDelegate>("NewSdk");
                _init = _library.GetDelegate<InitDelegate>("Init");
                _destroySdk = _library.GetDelegate<DestroySdkDelegate>("DestroySdk");
                _newSlice = _library.GetDelegate<NewSliceDelegate>("NewSlice");
                _freeSlice = _library.GetDelegate<FreeSliceDelegate>("FreeSlice");
                _getChatData = _library.GetDelegate<GetChatDataDelegate>("GetChatData");
                _decryptData = _library.GetDelegate<DecryptDataDelegate>("DecryptData");
                _getContentFromSlice = _library.GetDelegate<GetContentFromSliceDelegate>("GetContentFromSlice");
                _getSliceLen = _library.GetDelegate<GetSliceLenDelegate>("GetSliceLen");
                _newMediaData = _library.GetDelegate<NewMediaDataDelegate>("NewMediaData");
                _freeMediaData = _library.GetDelegate<FreeMediaDataDelegate>("FreeMediaData");
                _getMediaData = _library.GetDelegate<GetMediaDataDelegate>("GetMediaData");
                _getOutIndexBuf = _library.GetDelegate<GetOutIndexBufDelegate>("GetOutIndexBuf");
                _getData = _library.GetDelegate<GetDataDelegate>("GetData");
                _getIndexLen = _library.GetDelegate<GetIndexLenDelegate>("GetIndexLen");
                _getDataLen = _library.GetDelegate<GetDataLenDelegate>("GetDataLen");
                _isMediaDataFinish = _library.GetDelegate<IsMediaDataFinishDelegate>("IsMediaDataFinish");
            }
            catch
            {
                _library.Dispose();
                throw;
            }
        }

        /// <inheritdoc />
        public IntPtr NewSdk() => _newSdk();

        /// <inheritdoc />
        public int Init(IntPtr sdk, string corpId, string secret)
        {
            using var corpIdValue = new Utf8NativeString(corpId);
            using var secretValue = new Utf8NativeString(secret);
            return _init(sdk, corpIdValue.Pointer, secretValue.Pointer);
        }

        /// <inheritdoc />
        public void DestroySdk(IntPtr sdk) => _destroySdk(sdk);

        /// <inheritdoc />
        public IntPtr NewSlice() => _newSlice();

        /// <inheritdoc />
        public void FreeSlice(IntPtr slice) => _freeSlice(slice);

        /// <inheritdoc />
        public int GetChatData(IntPtr sdk, ulong sequence, uint limit, string proxy,
            string proxyPassword, int timeoutSeconds, IntPtr slice)
        {
            using var proxyValue = new Utf8NativeString(proxy ?? string.Empty);
            using var passwordValue = new Utf8NativeString(proxyPassword ?? string.Empty);
            return _getChatData(sdk, sequence, limit, proxyValue.Pointer, passwordValue.Pointer,
                timeoutSeconds, slice);
        }

        /// <inheritdoc />
        public int DecryptData(string decryptedRandomKey, string encryptedMessage, IntPtr slice)
        {
            using var keyValue = new Utf8NativeString(decryptedRandomKey);
            using var messageValue = new Utf8NativeString(encryptedMessage);
            return _decryptData(keyValue.Pointer, messageValue.Pointer, slice);
        }

        /// <inheritdoc />
        public string GetSliceContent(IntPtr slice)
            => ReadUtf8(_getContentFromSlice(slice), _getSliceLen(slice), "Slice");

        /// <inheritdoc />
        public IntPtr NewMediaData() => _newMediaData();

        /// <inheritdoc />
        public void FreeMediaData(IntPtr mediaData) => _freeMediaData(mediaData);

        /// <inheritdoc />
        public int GetMediaData(IntPtr sdk, string indexBuffer, string sdkFileId, string proxy,
            string proxyPassword, int timeoutSeconds, IntPtr mediaData)
        {
            using var indexValue = new Utf8NativeString(indexBuffer ?? string.Empty);
            using var fileIdValue = new Utf8NativeString(sdkFileId);
            using var proxyValue = new Utf8NativeString(proxy ?? string.Empty);
            using var passwordValue = new Utf8NativeString(proxyPassword ?? string.Empty);
            return _getMediaData(sdk, indexValue.Pointer, fileIdValue.Pointer, proxyValue.Pointer,
                passwordValue.Pointer, timeoutSeconds, mediaData);
        }

        /// <inheritdoc />
        public string GetMediaIndexBuffer(IntPtr mediaData)
            => ReadUtf8(_getOutIndexBuf(mediaData), _getIndexLen(mediaData), "MediaData.outindexbuf");

        /// <inheritdoc />
        public byte[] GetMediaBytes(IntPtr mediaData)
            => ReadBytes(_getData(mediaData), _getDataLen(mediaData), "MediaData.data");

        /// <inheritdoc />
        public bool IsMediaDataFinished(IntPtr mediaData) => _isMediaDataFinish(mediaData) == 1;

        /// <summary>
        /// 释放动态加载的原生库。必须在所有 SDK、Slice 和 MediaData 对象释放后调用。
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _library.Dispose();
        }

        private static string GetDefaultLibraryName()
        {
            if (FinanceRuntimePlatform.IsWindows)
            {
                return WindowsLibraryName;
            }

            if (FinanceRuntimePlatform.IsLinux)
            {
                return LinuxLibraryName;
            }

            throw new PlatformNotSupportedException(
                "企业微信官方未提供当前操作系统可用的 Finance 会话内容存档原生库。" +
                "请在 Windows 或 Linux 上运行，并在 LibraryPath 中指定对应的官方库。");
        }

        private static string ReadUtf8(IntPtr pointer, int length, string bufferName)
        {
            var bytes = ReadBytes(pointer, length, bufferName);
            return bytes.Length == 0 ? string.Empty : Encoding.UTF8.GetString(bytes);
        }

        private static byte[] ReadBytes(IntPtr pointer, int length, string bufferName)
        {
            if (length < 0)
            {
                throw new InvalidOperationException($"Finance SDK 返回了无效的 {bufferName} 长度：{length}。");
            }

            if (length == 0)
            {
                return Array.Empty<byte>();
            }

            if (pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Finance SDK 返回的 {bufferName} 指针为空，但长度为 {length}。");
            }

            var bytes = new byte[length];
            Marshal.Copy(pointer, bytes, 0, length);
            return bytes;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr NewSdkDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int InitDelegate(IntPtr sdk, IntPtr corpId, IntPtr secret);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void DestroySdkDelegate(IntPtr sdk);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr NewSliceDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FreeSliceDelegate(IntPtr slice);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GetChatDataDelegate(IntPtr sdk, ulong sequence, uint limit,
            IntPtr proxy, IntPtr proxyPassword, int timeoutSeconds, IntPtr slice);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int DecryptDataDelegate(IntPtr decryptedRandomKey, IntPtr encryptedMessage,
            IntPtr slice);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr GetContentFromSliceDelegate(IntPtr slice);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GetSliceLenDelegate(IntPtr slice);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr NewMediaDataDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FreeMediaDataDelegate(IntPtr mediaData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GetMediaDataDelegate(IntPtr sdk, IntPtr indexBuffer, IntPtr sdkFileId,
            IntPtr proxy, IntPtr proxyPassword, int timeoutSeconds, IntPtr mediaData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr GetOutIndexBufDelegate(IntPtr mediaData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr GetDataDelegate(IntPtr mediaData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GetIndexLenDelegate(IntPtr mediaData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GetDataLenDelegate(IntPtr mediaData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int IsMediaDataFinishDelegate(IntPtr mediaData);
    }

    /// <summary>
    /// 跨目标框架的 Windows/Linux 动态库句柄。
    /// </summary>
    internal sealed class FinanceLibraryHandle : IDisposable
    {
        private const int RtldNow = 2;
        private IntPtr _handle;
        private readonly bool _isWindows;
        private readonly bool _useLegacyLibDl;

        private FinanceLibraryHandle(IntPtr handle, bool isWindows, bool useLegacyLibDl)
        {
            _handle = handle;
            _isWindows = isWindows;
            _useLegacyLibDl = useLegacyLibDl;
        }

        /// <summary>
        /// 加载动态库并返回受控句柄。
        /// </summary>
        /// <param name="path">动态库路径或文件名。</param>
        /// <returns>已经加载的动态库句柄。</returns>
        public static FinanceLibraryHandle Load(string path)
        {
            if (FinanceRuntimePlatform.IsWindows)
            {
                var handle = WindowsNative.LoadLibrary(path);
                if (handle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(),
                        $"无法加载企业微信 Finance 原生库：{path}");
                }

                return new FinanceLibraryHandle(handle, true, false);
            }

            if (!FinanceRuntimePlatform.IsLinux)
            {
                throw new PlatformNotSupportedException(
                    "企业微信 Finance 会话内容存档原生库仅支持 Windows 和 Linux。");
            }

            IntPtr linuxHandle;
            var useLegacy = false;
            try
            {
                linuxHandle = LinuxNative.DlOpen(path, RtldNow);
            }
            catch (DllNotFoundException)
            {
                useLegacy = true;
                linuxHandle = LinuxLegacyNative.DlOpen(path, RtldNow);
            }

            if (linuxHandle == IntPtr.Zero)
            {
                var detail = useLegacy ? LinuxLegacyNative.GetError() : LinuxNative.GetError();
                throw new DllNotFoundException(
                    $"无法加载企业微信 Finance 原生库：{path}。{detail}");
            }

            return new FinanceLibraryHandle(linuxHandle, false, useLegacy);
        }

        /// <summary>
        /// 获取动态库导出函数并转换为指定的 Cdecl 委托。
        /// </summary>
        /// <typeparam name="TDelegate">带有 <see cref="UnmanagedFunctionPointerAttribute"/> 的委托类型。</typeparam>
        /// <param name="exportName">官方 C ABI 导出函数名称。</param>
        /// <returns>绑定到导出函数的托管委托。</returns>
        public TDelegate GetDelegate<TDelegate>(string exportName) where TDelegate : Delegate
        {
            ThrowIfDisposed();
            IntPtr symbol;
            if (_isWindows)
            {
                symbol = WindowsNative.GetProcAddress(_handle, exportName);
            }
            else
            {
                symbol = _useLegacyLibDl
                    ? LinuxLegacyNative.DlSym(_handle, exportName)
                    : LinuxNative.DlSym(_handle, exportName);
            }

            if (symbol == IntPtr.Zero)
            {
                throw new EntryPointNotFoundException(
                    $"企业微信 Finance 原生库缺少导出函数：{exportName}。");
            }

            return Marshal.GetDelegateForFunctionPointer<TDelegate>(symbol);
        }

        /// <summary>
        /// 释放动态库句柄。
        /// </summary>
        public void Dispose()
        {
            var handle = _handle;
            if (handle == IntPtr.Zero)
            {
                return;
            }

            _handle = IntPtr.Zero;
            if (_isWindows)
            {
                WindowsNative.FreeLibrary(handle);
            }
            else if (_useLegacyLibDl)
            {
                LinuxLegacyNative.DlClose(handle);
            }
            else
            {
                LinuxNative.DlClose(handle);
            }
        }

        private void ThrowIfDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(FinanceLibraryHandle));
            }
        }

        private static class WindowsNative
        {
            [DllImport("kernel32", EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode,
                SetLastError = true)]
            internal static extern IntPtr LoadLibrary(string fileName);

            [DllImport("kernel32", EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi,
                SetLastError = true)]
            internal static extern IntPtr GetProcAddress(IntPtr module, string procedureName);

            [DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FreeLibrary(IntPtr module);
        }

        private static class LinuxNative
        {
            [DllImport("libdl.so.2", EntryPoint = "dlopen")]
            internal static extern IntPtr DlOpen(string fileName, int flags);

            [DllImport("libdl.so.2", EntryPoint = "dlsym")]
            internal static extern IntPtr DlSym(IntPtr handle, string symbol);

            [DllImport("libdl.so.2", EntryPoint = "dlclose")]
            internal static extern int DlClose(IntPtr handle);

            [DllImport("libdl.so.2", EntryPoint = "dlerror")]
            private static extern IntPtr DlError();

            internal static string GetError()
            {
                var pointer = DlError();
                return pointer == IntPtr.Zero ? string.Empty : Marshal.PtrToStringAnsi(pointer);
            }
        }

        private static class LinuxLegacyNative
        {
            [DllImport("libdl.so", EntryPoint = "dlopen")]
            internal static extern IntPtr DlOpen(string fileName, int flags);

            [DllImport("libdl.so", EntryPoint = "dlsym")]
            internal static extern IntPtr DlSym(IntPtr handle, string symbol);

            [DllImport("libdl.so", EntryPoint = "dlclose")]
            internal static extern int DlClose(IntPtr handle);

            [DllImport("libdl.so", EntryPoint = "dlerror")]
            private static extern IntPtr DlError();

            internal static string GetError()
            {
                var pointer = DlError();
                return pointer == IntPtr.Zero ? string.Empty : Marshal.PtrToStringAnsi(pointer);
            }
        }
    }

    /// <summary>
    /// 为 .NET Framework 与现代 .NET 提供一致的运行平台判断。
    /// </summary>
    internal static class FinanceRuntimePlatform
    {
        /// <summary>
        /// 获取当前进程是否运行在 Windows。
        /// </summary>
        public static bool IsWindows
        {
            get
            {
#if NET462
                var platform = Environment.OSVersion.Platform;
                return platform == PlatformID.Win32NT ||
                       platform == PlatformID.Win32Windows ||
                       platform == PlatformID.Win32S ||
                       platform == PlatformID.WinCE;
#else
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#endif
            }
        }

        /// <summary>
        /// 获取当前进程是否运行在 Linux。
        /// </summary>
        public static bool IsLinux
        {
            get
            {
#if NET462
                return Environment.OSVersion.Platform == PlatformID.Unix;
#else
                return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
#endif
            }
        }
    }

    /// <summary>
    /// 将托管字符串转换为以空字符结尾的 UTF-8 原生缓冲区，并在释放时清零。
    /// </summary>
    internal sealed class Utf8NativeString : IDisposable
    {
        private readonly int _length;

        /// <summary>
        /// 创建 UTF-8 原生字符串。
        /// </summary>
        /// <param name="value">待转换的托管字符串。</param>
        public Utf8NativeString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var bytes = Encoding.UTF8.GetBytes(value);
            _length = bytes.Length + 1;
            Pointer = Marshal.AllocHGlobal(_length);
            try
            {
                if (bytes.Length > 0)
                {
                    Marshal.Copy(bytes, 0, Pointer, bytes.Length);
                }

                Marshal.WriteByte(Pointer, bytes.Length, 0);
            }
            catch
            {
                Marshal.FreeHGlobal(Pointer);
                Pointer = IntPtr.Zero;
                throw;
            }
            finally
            {
                Array.Clear(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        /// 获取 UTF-8 原生缓冲区地址。
        /// </summary>
        public IntPtr Pointer { get; private set; }

        /// <summary>
        /// 清零并释放 UTF-8 原生缓冲区。
        /// </summary>
        public void Dispose()
        {
            var pointer = Pointer;
            if (pointer == IntPtr.Zero)
            {
                return;
            }

            Pointer = IntPtr.Zero;
            var zeros = new byte[_length];
            Marshal.Copy(zeros, 0, pointer, zeros.Length);
            Marshal.FreeHGlobal(pointer);
        }
    }
}
