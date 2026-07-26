/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChatDataCallbackHandler.cs
    文件功能描述：企业微信数据与智能专区程序 JSON 回调强类型分派


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐专区程序当前事件通知分派

----------------------------------------------------------------*/

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>
    /// 数据与智能专区程序 JSON 回调处理器。
    /// <para>企业微信专区 SDK 会将事件 JSON 字符串传入注册回调函数的 data 参数。</para>
    /// </summary>
    public static class ChatDataCallbackHandler
    {
        /// <summary>
        /// 根据 event_type 将专区程序事件 JSON 分派为强类型消息。
        /// </summary>
        /// <param name="json">专区 SDK 回调函数收到的完整 data JSON 字符串。</param>
        /// <returns>已识别的强类型事件；未知事件保留原始 JSON。</returns>
        /// <exception cref="ArgumentException">JSON 字符串为空时抛出。</exception>
        /// <exception cref="JsonReaderException">输入不是合法 JSON 时抛出。</exception>
        public static ChatDataCallbackEventBase Parse(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("专区程序事件 JSON 不能为空。", nameof(json));
            }

            var root = JObject.Parse(json);
            var eventType = (string)root["event_type"];
            switch (eventType)
            {
                case ChatDataCallbackTypes.AuditApprovedSingle:
                case ChatDataCallbackTypes.AuditApprovedRoom:
                    return Deserialize<ChatDataAuditApprovedCallback>(json);
                case ChatDataCallbackTypes.ConversationNewMessage:
                    return Deserialize<ChatDataConversationNewMessageCallback>(json);
                case ChatDataCallbackTypes.HitKeyword:
                    return Deserialize<ChatDataHitKeywordCallback>(json);
                case ChatDataCallbackTypes.AuthorizeKnowledgeBase:
                case ChatDataCallbackTypes.UnauthorizeKnowledgeBase:
                case ChatDataCallbackTypes.DeleteKnowledgeBase:
                case ChatDataCallbackTypes.KnowledgeBaseLearnDone:
                    return Deserialize<ChatDataKnowledgeBaseCallback>(json);
                case ChatDataCallbackTypes.ChatArchiveExportFinished:
                    return Deserialize<ChatDataExportFinishedCallback>(json);
                default:
                    return new ChatDataUnknownCallback
                    {
                        event_type = eventType,
                        timestamp = (long?)root["timestamp"] ?? 0L,
                        raw_json = json
                    };
            }
        }

        private static TCallback Deserialize<TCallback>(string json)
            where TCallback : ChatDataCallbackEventBase
            => JsonConvert.DeserializeObject<TCallback>(json);
    }
}
