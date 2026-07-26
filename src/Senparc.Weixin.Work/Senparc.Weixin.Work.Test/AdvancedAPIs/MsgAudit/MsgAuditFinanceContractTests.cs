/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MsgAuditFinanceContractTests.cs
    文件功能描述：企业微信会话内容存档 Finance 原生 SDK 契约测试


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v1.0.3 验证 Finance 原生函数、资源释放和媒体分片续传

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.MsgAudit;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.MsgAudit
{
    [TestClass]
    public class MsgAuditFinanceContractTests
    {
        [TestMethod]
        public void ClientInitializesAndParsesChatDataWithUnsigned64BitSequence()
        {
            var native = new FakeFinanceNativeApi
            {
                ChatDataJson = "{\"errcode\":0,\"errmsg\":\"ok\",\"chatdata\":[{" +
                    "\"seq\":18446744073709551610,\"msgid\":\"msg-1\"," +
                    "\"publickey_ver\":7,\"encrypt_random_key\":\"key\"," +
                    "\"encrypt_chat_msg\":\"message\"}]}"
            };
            var options = CreateOptions();

            using var client = new MsgAuditFinanceClient(options, native);
            var result = client.GetChatData(18446744073709551000UL, 1000);

            Assert.AreEqual("ww-corp", native.InitializedCorpId);
            Assert.AreEqual("finance-secret", native.InitializedSecret);
            Assert.AreEqual(18446744073709551000UL, native.LastSequence);
            Assert.AreEqual(1000U, native.LastLimit);
            Assert.AreEqual("http://127.0.0.1:8080", native.LastProxy);
            Assert.AreEqual("proxy-secret", native.LastProxyPassword);
            Assert.AreEqual(9, native.LastTimeoutSeconds);
            Assert.AreEqual(18446744073709551610UL, result.chatdata[0].seq);
            Assert.AreEqual(7U, result.chatdata[0].publickey_ver);
            Assert.AreEqual(native.ChatDataJson, result.raw_json);
            Assert.AreEqual(1, native.FreeSliceCount);
        }

        [TestMethod]
        public void DecryptFailureThrowsTypedExceptionAndStillFreesSlice()
        {
            var native = new FakeFinanceNativeApi { DecryptResult = 10006 };
            using var client = new MsgAuditFinanceClient(CreateOptions(), native);

            var exception = Assert.ThrowsException<MsgAuditFinanceException>(
                () => client.DecryptData("plain-random-key", "encrypted-message"));

            Assert.AreEqual(10006, exception.ErrorCode);
            Assert.AreEqual("DecryptData", exception.Operation);
            Assert.AreEqual(1, native.FreeSliceCount);
        }

        [TestMethod]
        public void DownloadMediaWritesAllChunksAndUsesReturnedIndexBuffer()
        {
            var native = new FakeFinanceNativeApi();
            native.MediaChunks.Enqueue(new FakeMediaChunk(
                new byte[] { 1, 2, 3 }, "next-index", false));
            native.MediaChunks.Enqueue(new FakeMediaChunk(
                new byte[] { 4, 5 }, "final-index", true));

            using var client = new MsgAuditFinanceClient(CreateOptions(), native);
            using var destination = new MemoryStream();
            var length = client.DownloadMedia("sdk-file-id", destination);

            Assert.AreEqual(5L, length);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5 }, destination.ToArray());
            CollectionAssert.AreEqual(new[] { string.Empty, "next-index" },
                native.RequestedMediaIndexes.ToArray());
            Assert.AreEqual(2, native.FreeMediaDataCount);
        }

        [TestMethod]
        public void DisposeDestroysSdkAndNativeLibraryOnlyOnce()
        {
            var native = new FakeFinanceNativeApi();
            var client = new MsgAuditFinanceClient(CreateOptions(), native);

            client.Dispose();
            client.Dispose();

            Assert.AreEqual(1, native.DestroySdkCount);
            Assert.AreEqual(1, native.DisposeCount);
            Assert.ThrowsException<ObjectDisposedException>(() => client.GetChatData(0));
        }

        [TestMethod]
        public void ClientRejectsInvalidArgumentsBeforeCallingNativeOperations()
        {
            var native = new FakeFinanceNativeApi();
            using var client = new MsgAuditFinanceClient(CreateOptions(), native);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => client.GetChatData(0, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => client.GetChatData(0, 1001));
            Assert.ThrowsException<ArgumentException>(() => client.DecryptData("", "message"));
            Assert.ThrowsException<ArgumentException>(() => client.GetMediaData(""));
            Assert.AreEqual(0, native.NewSliceCount);
            Assert.AreEqual(0, native.NewMediaDataCount);
        }

        [TestMethod]
        public void NativeBindingContainsAllOfficialExportsCdeclAndXmlComments()
        {
            var root = FindRepositoryRoot();
            var nativeSource = File.ReadAllText(Path.Combine(root, "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "MsgAudit", "MsgAuditFinanceNativeApi.cs"));
            var clientSource = File.ReadAllText(Path.Combine(root, "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "MsgAudit", "MsgAuditFinanceClient.cs"));
            var modelSource = File.ReadAllText(Path.Combine(root, "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "MsgAudit", "MsgAuditFinanceModels.cs"));

            foreach (var exportName in new[]
            {
                "NewSdk", "Init", "GetChatData", "DecryptData", "GetMediaData", "DestroySdk",
                "NewSlice", "FreeSlice", "GetContentFromSlice", "GetSliceLen", "NewMediaData",
                "FreeMediaData", "GetOutIndexBuf", "GetData", "GetIndexLen", "GetDataLen",
                "IsMediaDataFinish"
            })
            {
                StringAssert.Contains(nativeSource, $"\"{exportName}\"");
            }

            StringAssert.Contains(nativeSource, "CallingConvention.Cdecl");
            StringAssert.Contains(nativeSource, "libWeWorkFinanceSdk_C.so");
            StringAssert.Contains(nativeSource, "WeWorkFinanceSdk_C.dll");
            StringAssert.Contains(clientSource, "/// <param name=\"decryptedRandomKey\">");
            StringAssert.Contains(clientSource, "/// <param name=\"sdkFileId\">");
            StringAssert.Contains(modelSource, "/// 获取当前分片的二进制数据");
            StringAssert.Contains(modelSource, "JsonSourceGenerationMode.Metadata");
        }

        private static MsgAuditFinanceOptions CreateOptions()
        {
            return new MsgAuditFinanceOptions
            {
                CorpId = "ww-corp",
                Secret = "finance-secret",
                Proxy = "http://127.0.0.1:8080",
                ProxyPassword = "proxy-secret",
                TimeoutSeconds = 9
            };
        }

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(),
                AppContext.BaseDirectory,
                Path.GetDirectoryName(sourceFilePath)
            })
            {
                var directory = string.IsNullOrEmpty(startPath) ? null : new DirectoryInfo(startPath);
                while (directory != null)
                {
                    if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                    {
                        return directory.FullName;
                    }

                    directory = directory.Parent;
                }
            }

            Assert.Fail("无法定位仓库根目录。");
            return null;
        }

        private sealed class FakeFinanceNativeApi : IMsgAuditFinanceNativeApi
        {
            private readonly Dictionary<IntPtr, string> _sliceContents =
                new Dictionary<IntPtr, string>();
            private readonly Dictionary<IntPtr, FakeMediaChunk> _mediaContents =
                new Dictionary<IntPtr, FakeMediaChunk>();
            private int _nextHandle = 10;

            public string ChatDataJson { get; set; } =
                "{\"errcode\":0,\"errmsg\":\"ok\",\"chatdata\":[]}";

            public int DecryptResult { get; set; }

            public Queue<FakeMediaChunk> MediaChunks { get; } = new Queue<FakeMediaChunk>();

            public List<string> RequestedMediaIndexes { get; } = new List<string>();

            public string InitializedCorpId { get; private set; }

            public string InitializedSecret { get; private set; }

            public ulong LastSequence { get; private set; }

            public uint LastLimit { get; private set; }

            public string LastProxy { get; private set; }

            public string LastProxyPassword { get; private set; }

            public int LastTimeoutSeconds { get; private set; }

            public int NewSliceCount { get; private set; }

            public int FreeSliceCount { get; private set; }

            public int NewMediaDataCount { get; private set; }

            public int FreeMediaDataCount { get; private set; }

            public int DestroySdkCount { get; private set; }

            public int DisposeCount { get; private set; }

            public IntPtr NewSdk() => new IntPtr(1);

            public int Init(IntPtr sdk, string corpId, string secret)
            {
                InitializedCorpId = corpId;
                InitializedSecret = secret;
                return 0;
            }

            public void DestroySdk(IntPtr sdk) => DestroySdkCount++;

            public IntPtr NewSlice()
            {
                NewSliceCount++;
                return new IntPtr(_nextHandle++);
            }

            public void FreeSlice(IntPtr slice)
            {
                FreeSliceCount++;
                _sliceContents.Remove(slice);
            }

            public int GetChatData(IntPtr sdk, ulong sequence, uint limit, string proxy,
                string proxyPassword, int timeoutSeconds, IntPtr slice)
            {
                LastSequence = sequence;
                LastLimit = limit;
                LastProxy = proxy;
                LastProxyPassword = proxyPassword;
                LastTimeoutSeconds = timeoutSeconds;
                _sliceContents[slice] = ChatDataJson;
                return 0;
            }

            public int DecryptData(string decryptedRandomKey, string encryptedMessage, IntPtr slice)
            {
                _sliceContents[slice] = "{\"msgtype\":\"text\"}";
                return DecryptResult;
            }

            public string GetSliceContent(IntPtr slice) => _sliceContents[slice];

            public IntPtr NewMediaData()
            {
                NewMediaDataCount++;
                return new IntPtr(_nextHandle++);
            }

            public void FreeMediaData(IntPtr mediaData)
            {
                FreeMediaDataCount++;
                _mediaContents.Remove(mediaData);
            }

            public int GetMediaData(IntPtr sdk, string indexBuffer, string sdkFileId, string proxy,
                string proxyPassword, int timeoutSeconds, IntPtr mediaData)
            {
                RequestedMediaIndexes.Add(indexBuffer);
                _mediaContents[mediaData] = MediaChunks.Dequeue();
                return 0;
            }

            public string GetMediaIndexBuffer(IntPtr mediaData)
                => _mediaContents[mediaData].NextIndexBuffer;

            public byte[] GetMediaBytes(IntPtr mediaData) => _mediaContents[mediaData].Data;

            public bool IsMediaDataFinished(IntPtr mediaData) => _mediaContents[mediaData].IsFinished;

            public void Dispose() => DisposeCount++;
        }

        private sealed class FakeMediaChunk
        {
            public FakeMediaChunk(byte[] data, string nextIndexBuffer, bool isFinished)
            {
                Data = data;
                NextIndexBuffer = nextIndexBuffer;
                IsFinished = isFinished;
            }

            public byte[] Data { get; }

            public string NextIndexBuffer { get; }

            public bool IsFinished { get; }
        }
    }
}
