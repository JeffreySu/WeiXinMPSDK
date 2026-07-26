/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MsgAuditFinanceModels.cs
    文件功能描述：企业微信会话内容存档 Finance 原生 SDK 模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐 Finance 原生 SDK 强类型模型和异常

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Senparc.Weixin.Work.AdvancedAPIs.MsgAudit
{
    /// <summary>
    /// 企业微信会话内容存档 Finance 原生 SDK 客户端配置。
    /// </summary>
    public sealed class MsgAuditFinanceOptions
    {
        /// <summary>
        /// 获取或设置企业 ID。
        /// </summary>
        public string CorpId { get; set; }

        /// <summary>
        /// 获取或设置会话内容存档应用的 Secret。
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 获取或设置官方 Finance 原生库路径。
        /// <para>不设置时，Windows 加载 <c>WeWorkFinanceSdk_C.dll</c>，Linux 加载 <c>libWeWorkFinanceSdk_C.so</c>。</para>
        /// <para>企业微信目前未提供 macOS 版本的会话内容存档原生库。</para>
        /// </summary>
        public string LibraryPath { get; set; }

        /// <summary>
        /// 获取或设置访问企业微信服务的 HTTP 代理地址；不使用代理时保持为空。
        /// </summary>
        public string Proxy { get; set; }

        /// <summary>
        /// 获取或设置 HTTP 代理密码；不使用代理认证时保持为空。
        /// </summary>
        public string ProxyPassword { get; set; }

        /// <summary>
        /// 获取或设置原生 SDK 网络请求超时时间（秒），默认值为 5 秒。
        /// </summary>
        public int TimeoutSeconds { get; set; } = 5;
    }

    /// <summary>
    /// 企业微信会话内容存档拉取结果。
    /// </summary>
    public sealed class MsgAuditFinanceChatDataResult
    {
        /// <summary>
        /// 获取或设置企业微信返回码；0 表示成功。
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 获取或设置企业微信返回信息。
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 获取或设置本次拉取到的加密会话记录。
        /// </summary>
        public IList<MsgAuditFinanceEncryptedMessage> chatdata { get; set; }

        /// <summary>
        /// 获取原生 SDK 返回的完整 JSON，便于兼容官方后续新增字段。
        /// </summary>
        [JsonIgnore]
        public string raw_json { get; internal set; }
    }

    /// <summary>
    /// 企业微信会话内容存档中的一条加密消息。
    /// </summary>
    public sealed class MsgAuditFinanceEncryptedMessage
    {
        /// <summary>
        /// 获取或设置消息序号；下一次拉取时应将已处理的最大序号作为起始序号。
        /// </summary>
        public ulong seq { get; set; }

        /// <summary>
        /// 获取或设置消息 ID。
        /// </summary>
        public string msgid { get; set; }

        /// <summary>
        /// 获取或设置用于解密随机密钥的企业公钥版本号。
        /// </summary>
        public uint publickey_ver { get; set; }

        /// <summary>
        /// 获取或设置使用企业公钥加密并经过 Base64 编码的随机密钥。
        /// </summary>
        public string encrypt_random_key { get; set; }

        /// <summary>
        /// 获取或设置待交给 Finance 原生 SDK 解密的会话消息密文。
        /// </summary>
        public string encrypt_chat_msg { get; set; }
    }

    /// <summary>
    /// 企业微信会话内容存档媒体文件的单个下载分片。
    /// </summary>
    public sealed class MsgAuditFinanceMediaChunk
    {
        internal MsgAuditFinanceMediaChunk(byte[] data, string nextIndexBuffer, bool isFinished)
        {
            this.data = data ?? Array.Empty<byte>();
            next_index_buffer = nextIndexBuffer ?? string.Empty;
            is_finished = isFinished;
        }

        /// <summary>
        /// 获取当前分片的二进制数据；官方单个分片最大为 512 KB。
        /// </summary>
        public byte[] data { get; }

        /// <summary>
        /// 获取下一次下载时需要传入的索引缓冲区；首次请求传空字符串。
        /// </summary>
        public string next_index_buffer { get; }

        /// <summary>
        /// 获取媒体文件是否已经下载完成。
        /// </summary>
        public bool is_finished { get; }
    }

    /// <summary>
    /// 企业微信会话内容存档 Finance 原生 SDK 调用异常。
    /// </summary>
    public sealed class MsgAuditFinanceException : Exception
    {
        /// <summary>
        /// 使用原生 SDK 返回码和操作名称创建异常。
        /// </summary>
        /// <param name="errorCode">原生 SDK 返回码。</param>
        /// <param name="operation">发生错误的原生操作名称。</param>
        public MsgAuditFinanceException(int errorCode, string operation)
            : base($"企业微信会话内容存档 Finance SDK 调用失败：{operation}，错误码：{errorCode}。")
        {
            ErrorCode = errorCode;
            Operation = operation;
        }

        /// <summary>
        /// 获取 Finance 原生 SDK 返回码。
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// 获取发生错误的原生操作名称。
        /// </summary>
        public string Operation { get; }
    }

    /// <summary>
    /// 会话内容存档 Finance JSON 的 Native AOT 源生成上下文。
    /// </summary>
    [JsonSourceGenerationOptions(
        GenerationMode = JsonSourceGenerationMode.Metadata,
        PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(MsgAuditFinanceChatDataResult))]
    internal partial class MsgAuditFinanceJsonSerializerContext : JsonSerializerContext
    {
    }
}
