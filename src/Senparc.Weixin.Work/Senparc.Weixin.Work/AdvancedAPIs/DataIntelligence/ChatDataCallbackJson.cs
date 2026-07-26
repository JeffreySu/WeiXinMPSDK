/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChatDataCallbackJson.cs
    文件功能描述：企业微信数据与智能专区程序 JSON 回调强类型模型


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐专区程序当前事件通知模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>
    /// 数据与智能专区程序事件类型常量。
    /// </summary>
    public static class ChatDataCallbackTypes
    {
        /// <summary>客户在单聊中同意进行聊天内容存档。</summary>
        public const string AuditApprovedSingle = "chat_archive_audit_approved_single";

        /// <summary>客户在群聊中同意进行聊天内容存档。</summary>
        public const string AuditApprovedRoom = "chat_archive_audit_approved_room";

        /// <summary>产生新的会话消息。</summary>
        public const string ConversationNewMessage = "conversation_new_message";

        /// <summary>会话命中关键词规则。</summary>
        public const string HitKeyword = "hit_keyword";

        /// <summary>企业授权知识集。</summary>
        public const string AuthorizeKnowledgeBase = "auth_knowledge_base";

        /// <summary>企业取消授权知识集。</summary>
        public const string UnauthorizeKnowledgeBase = "unauth_knowledge_base";

        /// <summary>企业删除已授权的知识集。</summary>
        public const string DeleteKnowledgeBase = "delete_knowledge_base";

        /// <summary>知识集内容学习完成。</summary>
        public const string KnowledgeBaseLearnDone = "knowledge_base_learn_done";

        /// <summary>会话内容导出任务完成。</summary>
        public const string ChatArchiveExportFinished = "chat_archive_export_finished";
    }

    /// <summary>
    /// 数据与智能专区程序 JSON 回调基类。
    /// </summary>
    public abstract class ChatDataCallbackEventBase
    {
        /// <summary>获取或设置事件类型。</summary>
        public string event_type { get; set; }

        /// <summary>获取或设置事件发生的 Unix 时间戳（秒）。</summary>
        public long timestamp { get; set; }
    }

    /// <summary>
    /// 客户同意进行聊天内容存档事件。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/99993</para>
    /// </summary>
    public class ChatDataAuditApprovedCallback : ChatDataCallbackEventBase
    {
        /// <summary>获取或设置客户同意存档的会话信息。</summary>
        public ChatDataAuditApprovedInfo chat_archive_audit_approved { get; set; }
    }

    /// <summary>
    /// 客户同意进行聊天内容存档的会话信息。
    /// </summary>
    public class ChatDataAuditApprovedInfo
    {
        /// <summary>获取或设置单聊中的企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置单聊中的外部联系人 ExternalUserId。</summary>
        public string external_userid { get; set; }

        /// <summary>获取或设置群聊中的 ChatId。</summary>
        public string chatid { get; set; }
    }

    /// <summary>
    /// 产生新会话消息事件。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/99994</para>
    /// </summary>
    public class ChatDataConversationNewMessageCallback : ChatDataCallbackEventBase
    {
        /// <summary>获取或设置新消息查询令牌。</summary>
        public ChatDataTokenInfo conversation_new_message { get; set; }
    }

    /// <summary>
    /// 命中关键词规则事件。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/99995</para>
    /// </summary>
    public class ChatDataHitKeywordCallback : ChatDataCallbackEventBase
    {
        /// <summary>获取或设置命中会话查询令牌。</summary>
        public ChatDataTokenInfo hit_keyword { get; set; }
    }

    /// <summary>
    /// 数据与智能专区程序事件使用的短期查询令牌。
    /// </summary>
    public class ChatDataTokenInfo
    {
        /// <summary>获取或设置十分钟内有效的查询令牌。</summary>
        public string token { get; set; }
    }

    /// <summary>
    /// 知识集授权、取消授权、删除或内容学习完成事件。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/99996</para>
    /// </summary>
    public class ChatDataKnowledgeBaseCallback : ChatDataCallbackEventBase
    {
        /// <summary>获取或设置授权知识集信息。</summary>
        public ChatDataKnowledgeBaseInfo auth_knowledge_base { get; set; }

        /// <summary>获取或设置取消授权知识集信息。</summary>
        public ChatDataKnowledgeBaseInfo unauth_knowledge_base { get; set; }

        /// <summary>获取或设置删除知识集信息。</summary>
        public ChatDataKnowledgeBaseInfo delete_knowledge_base { get; set; }

        /// <summary>获取或设置知识集内容学习结果。</summary>
        public ChatDataKnowledgeBaseInfo knowledge_base_learn_done { get; set; }
    }

    /// <summary>
    /// 知识集事件信息。
    /// </summary>
    public class ChatDataKnowledgeBaseInfo
    {
        /// <summary>获取或设置知识集 ID。</summary>
        public string knowledge_base_id { get; set; }

        /// <summary>获取或设置知识集名称。</summary>
        public string knowledge_base_name { get; set; }

        /// <summary>获取或设置内容 ID；仅内容学习完成事件返回。</summary>
        public long? doc_id { get; set; }

        /// <summary>获取或设置学习状态；0 表示成功，1 表示失败。</summary>
        public int? learn_status { get; set; }
    }

    /// <summary>
    /// 会话内容导出任务完成事件。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/99997</para>
    /// </summary>
    public class ChatDataExportFinishedCallback : ChatDataCallbackEventBase
    {
        /// <summary>获取或设置导出任务信息。</summary>
        public ChatDataExportFinishedInfo chat_archive_export_finished { get; set; }
    }

    /// <summary>
    /// 会话内容导出任务信息。
    /// </summary>
    public class ChatDataExportFinishedInfo
    {
        /// <summary>获取或设置 24 小时内有效的导出任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// SDK 尚未识别的数据与智能专区程序事件。
    /// </summary>
    public class ChatDataUnknownCallback : ChatDataCallbackEventBase
    {
        /// <summary>获取或设置未丢失字段的原始 JSON。</summary>
        public string raw_json { get; set; }
    }
}
