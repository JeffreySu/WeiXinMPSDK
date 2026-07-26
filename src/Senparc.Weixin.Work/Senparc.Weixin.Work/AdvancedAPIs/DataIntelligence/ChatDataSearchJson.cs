/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChatDataSearchJson.cs
    文件功能描述：数据与智能专区群聊和消息搜索模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐数据与智能专区群聊和消息搜索模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>搜索专区群聊请求。</summary>
    public class ChatDataSearchChatRequest
    {
        /// <summary>搜索词。</summary>
        public string query_word { get; set; }

        /// <summary>分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>本次返回的最大数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>搜索命中的专区群聊。</summary>
    public class ChatDataSearchChatItem
    {
        /// <summary>群聊 ID。</summary>
        public string chatid { get; set; }
    }

    /// <summary>专区群聊搜索结果。</summary>
    public class ChatDataSearchChatResult : WorkJsonResult
    {
        /// <summary>命中的群聊列表。</summary>
        public ChatDataSearchChatItem[] chat_list { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否仍有未返回的数据。</summary>
        public bool has_more { get; set; }
    }

    /// <summary>搜索消息时指定的成员或外部联系人。</summary>
    public class ChatDataSearchMessageUser
    {
        /// <summary>成员在数据与智能专区中的 OpenUserID。</summary>
        public string open_userid { get; set; }

        /// <summary>外部联系人 UserID。</summary>
        public string external_userid { get; set; }
    }

    /// <summary>搜索消息的会话范围。</summary>
    public class ChatDataSearchMessageChatInfo
    {
        /// <summary>会话类型。</summary>
        public int chat_type { get; set; }

        /// <summary>会话参与者列表。</summary>
        public ChatDataSearchMessageUser[] id_list { get; set; }
    }

    /// <summary>搜索专区消息请求。</summary>
    public class ChatDataSearchMessageRequest
    {
        /// <summary>搜索词。</summary>
        public string query_word { get; set; }

        /// <summary>搜索的会话范围。</summary>
        public ChatDataSearchMessageChatInfo chat_info { get; set; }

        /// <summary>搜索时间范围起点，Unix 时间戳。</summary>
        public long? start_time { get; set; }

        /// <summary>搜索时间范围终点，Unix 时间戳。</summary>
        public long? end_time { get; set; }

        /// <summary>分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>本次返回的最大数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>搜索命中的专区消息。</summary>
    public class ChatDataSearchMessageItem
    {
        /// <summary>消息 ID。</summary>
        public string msgid { get; set; }
    }

    /// <summary>专区消息搜索结果。</summary>
    public class ChatDataSearchMessageResult : WorkJsonResult
    {
        /// <summary>命中的消息列表。</summary>
        public ChatDataSearchMessageItem[] msg_list { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否仍有未返回的数据。</summary>
        public bool has_more { get; set; }
    }
}
