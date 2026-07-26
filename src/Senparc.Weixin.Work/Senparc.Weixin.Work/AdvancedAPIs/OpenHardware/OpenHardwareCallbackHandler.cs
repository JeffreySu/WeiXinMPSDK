/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareCallbackHandler.cs
    文件功能描述：企业微信开放硬件加密 JSON 回调验签、解密和强类型分派


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐开放硬件事件、指令和被动响应处理

----------------------------------------------------------------*/

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.Work.Tencent;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>
    /// 企业微信开放硬件加密 JSON 回调处理器。
    /// <para>按官方协议完成验签、AES 解密、事件/指令分派和被动响应加密。</para>
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96062"/></para>
    /// </summary>
    public static class OpenHardwareCallbackHandler
    {
        /// <summary>
        /// 验签、解密并将开放硬件回调分派为强类型消息。
        /// </summary>
        /// <param name="token">开放硬件回调地址配置的 Token。</param>
        /// <param name="encodingAesKey">开放硬件回调地址配置的 EncodingAESKey。</param>
        /// <param name="receiveId">接收方标识；服务商通用地址传 CorpId，型号地址传 ModelId。</param>
        /// <param name="msgSignature">请求参数 msg_signature。</param>
        /// <param name="timestamp">请求参数 timestamp。</param>
        /// <param name="nonce">请求参数 nonce。</param>
        /// <param name="encryptedBody">包含 tousername 和 encrypt 的请求 JSON 正文。</param>
        /// <returns>解密后的原始 JSON 及强类型回调消息。</returns>
        /// <exception cref="ArgumentException">必需参数或加密正文为空时抛出。</exception>
        /// <exception cref="OpenHardwareCallbackCryptException">验签、解密或接收方校验失败时抛出。</exception>
        public static OpenHardwareCallbackParseResult DecryptAndParse(
            string token, string encodingAesKey, string receiveId,
            string msgSignature, string timestamp, string nonce,
            string encryptedBody)
        {
            EnsureNotEmpty(token, nameof(token));
            EnsureNotEmpty(encodingAesKey, nameof(encodingAesKey));
            EnsureNotEmpty(receiveId, nameof(receiveId));
            EnsureNotEmpty(msgSignature, nameof(msgSignature));
            EnsureNotEmpty(timestamp, nameof(timestamp));
            EnsureNotEmpty(nonce, nameof(nonce));
            EnsureNotEmpty(encryptedBody, nameof(encryptedBody));

            var envelope = JsonConvert
                .DeserializeObject<OpenHardwareEncryptedCallbackRequest>(encryptedBody);
            if (envelope == null || string.IsNullOrWhiteSpace(envelope.encrypt))
            {
                throw new ArgumentException(
                    "开放硬件回调正文必须包含非空 encrypt 字段。",
                    nameof(encryptedBody));
            }

            var plaintext = string.Empty;
            var crypt = new WXBizMsgCrypt(token, encodingAesKey, receiveId);
            var errorCode = crypt.DecryptJsonMsg(msgSignature, timestamp, nonce,
                envelope.encrypt, ref plaintext);
            if (errorCode != 0)
            {
                throw new OpenHardwareCallbackCryptException(errorCode);
            }

            return new OpenHardwareCallbackParseResult
            {
                tousername = envelope.tousername,
                plaintext = plaintext,
                message = ParsePlaintext(plaintext)
            };
        }

        /// <summary>
        /// 将已解密的开放硬件 JSON 按 event_type 或 command_type 分派为强类型消息。
        /// </summary>
        /// <param name="plaintext">验签解密后的完整 JSON 文本。</param>
        /// <returns>已识别的事件或指令；未识别类型会保留原始 JSON。</returns>
        /// <exception cref="ArgumentException">明文为空时抛出。</exception>
        /// <exception cref="JsonReaderException">明文不是合法 JSON 时抛出。</exception>
        public static OpenHardwareCallbackMessageBase ParsePlaintext(
            string plaintext)
        {
            EnsureNotEmpty(plaintext, nameof(plaintext));

            var root = JObject.Parse(plaintext);
            var messageType = (string)root["msg_type"];
            var callbackType = messageType == "event"
                ? (string)root["event"]?["event_type"]
                : messageType == "command"
                    ? (string)root["command"]?["command_type"]
                    : null;

            switch (messageType + ":" + callbackType)
            {
                case "event:" + OpenHardwareCallbackTypes.Bind:
                    return Deserialize<OpenHardwareEventCallback<OpenHardwareBindEvent>>(plaintext);
                case "event:" + OpenHardwareCallbackTypes.Unbind:
                    return Deserialize<OpenHardwareEventCallback<OpenHardwareUnbindEvent>>(plaintext);
                case "event:" + OpenHardwareCallbackTypes.ContactChange:
                    return Deserialize<OpenHardwareEventCallback<OpenHardwareContactChangeEvent>>(plaintext);
                case "event:" + OpenHardwareCallbackTypes.ModelTicket:
                    return Deserialize<OpenHardwareEventCallback<OpenHardwareModelTicketEvent>>(plaintext);
                case "event:" + OpenHardwareCallbackTypes.VerifyDevice:
                    return Deserialize<OpenHardwareEventCallback<OpenHardwareVerifyDeviceEvent>>(plaintext);
                case "command:" + OpenHardwareCallbackTypes.UpdateFirmware:
                    return Deserialize<OpenHardwareCommandCallback<OpenHardwareUpdateFirmwareCommand>>(plaintext);
                case "command:" + OpenHardwareCallbackTypes.FetchDeviceStatus:
                    return Deserialize<OpenHardwareCommandCallback<OpenHardwareFetchDeviceStatusCommand>>(plaintext);
                case "command:" + OpenHardwareCallbackTypes.UserScan:
                    return Deserialize<OpenHardwareCommandCallback<OpenHardwareUserScanCommand>>(plaintext);
                case "command:" + OpenHardwareCallbackTypes.EnterPage:
                case "command:" + OpenHardwareCallbackTypes.ExitPage:
                case "command:" + OpenHardwareCallbackTypes.DeleteBiometricInfo:
                    return Deserialize<OpenHardwareCommandCallback<OpenHardwareBiometricPageCommand>>(plaintext);
                case "command:" + OpenHardwareCallbackTypes.RemoteOpenDoor:
                    return Deserialize<OpenHardwareCommandCallback<OpenHardwareRemoteOpenDoorCommand>>(plaintext);
                case "command:" + OpenHardwareCallbackTypes.PrinterJobSubmit:
                    return Deserialize<OpenHardwareCommandCallback<OpenHardwarePrinterJobSubmitCommand>>(plaintext);
                case "command:" + OpenHardwareCallbackTypes.PrinterJobTranscode:
                    return Deserialize<OpenHardwareCommandCallback<OpenHardwarePrinterJobTranscodeCommand>>(plaintext);
                case "command:" + OpenHardwareCallbackTypes.PrinterJobDelete:
                    return Deserialize<OpenHardwareCommandCallback<OpenHardwarePrinterJobDeleteCommand>>(plaintext);
                default:
                    return new OpenHardwareUnknownCallbackMessage
                    {
                        msg_type = messageType,
                        base_info = root["base_info"]?
                            .ToObject<OpenHardwareCallbackBaseInfo>(),
                        callback_type = callbackType,
                        raw_json = plaintext
                    };
            }
        }

        /// <summary>
        /// 将强类型被动响应序列化、加密并生成企业微信要求的签名字段。
        /// </summary>
        /// <typeparam name="TResponse">开放硬件被动响应类型。</typeparam>
        /// <param name="token">开放硬件回调地址配置的 Token。</param>
        /// <param name="encodingAesKey">开放硬件回调地址配置的 EncodingAESKey。</param>
        /// <param name="receiveId">接收方标识；服务商通用地址传 CorpId，型号地址传 ModelId。</param>
        /// <param name="timestamp">生成签名使用的时间戳。</param>
        /// <param name="nonce">生成签名使用的随机字符串。</param>
        /// <param name="response">需要加密的强类型被动响应。</param>
        /// <returns>可直接序列化返回的加密响应结构。</returns>
        /// <exception cref="ArgumentNullException">被动响应对象为 null 时抛出。</exception>
        /// <exception cref="OpenHardwareCallbackCryptException">加密或生成签名失败时抛出。</exception>
        public static OpenHardwareEncryptedCallbackReply EncryptResponse<TResponse>(
            string token, string encodingAesKey, string receiveId,
            string timestamp, string nonce, TResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var plaintext = JsonConvert.SerializeObject(response,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            return EncryptResponse(token, encodingAesKey, receiveId, timestamp,
                nonce, plaintext);
        }

        /// <summary>
        /// 加密被动响应明文并生成企业微信要求的签名字段。
        /// </summary>
        /// <param name="token">开放硬件回调地址配置的 Token。</param>
        /// <param name="encodingAesKey">开放硬件回调地址配置的 EncodingAESKey。</param>
        /// <param name="receiveId">接收方标识；服务商通用地址传 CorpId，型号地址传 ModelId。</param>
        /// <param name="timestamp">生成签名使用的时间戳。</param>
        /// <param name="nonce">生成签名使用的随机字符串。</param>
        /// <param name="plaintext">需要加密的 JSON 明文。</param>
        /// <returns>可直接序列化返回的加密响应结构。</returns>
        /// <exception cref="ArgumentException">必需参数或响应明文为空时抛出。</exception>
        /// <exception cref="OpenHardwareCallbackCryptException">加密或生成签名失败时抛出。</exception>
        public static OpenHardwareEncryptedCallbackReply EncryptResponse(
            string token, string encodingAesKey, string receiveId,
            string timestamp, string nonce, string plaintext)
        {
            EnsureNotEmpty(token, nameof(token));
            EnsureNotEmpty(encodingAesKey, nameof(encodingAesKey));
            EnsureNotEmpty(receiveId, nameof(receiveId));
            EnsureNotEmpty(timestamp, nameof(timestamp));
            EnsureNotEmpty(nonce, nameof(nonce));
            EnsureNotEmpty(plaintext, nameof(plaintext));

            var crypt = new WXBizMsgCrypt(token, encodingAesKey, receiveId);
            BotEncryptedReply encryptedReply = null;
            var errorCode = crypt.EncryptJsonMsg(plaintext, timestamp, nonce,
                ref encryptedReply);
            if (errorCode != 0)
            {
                throw new OpenHardwareCallbackCryptException(errorCode);
            }

            return new OpenHardwareEncryptedCallbackReply
            {
                encrypt = encryptedReply.encrypt,
                msgsignature = encryptedReply.msgsignature,
                timestamp = timestamp,
                nonce = encryptedReply.nonce
            };
        }

        private static TMessage Deserialize<TMessage>(string plaintext)
            where TMessage : OpenHardwareCallbackMessageBase
            => JsonConvert.DeserializeObject<TMessage>(plaintext);

        private static void EnsureNotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("参数不能为空。", parameterName);
            }
        }
    }
}
