/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChatDataKeywordJson.cs
    文件功能描述：数据与智能专区敏感关键词规则模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐数据与智能专区敏感关键词规则模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>敏感关键词列表。</summary>
    public class ChatDataKeywordWords
    {
        /// <summary>关键词文本列表。</summary>
        public string[] word_list { get; set; }
    }

    /// <summary>敏感行为列表。</summary>
    public class ChatDataKeywordSemantics
    {
        /// <summary>敏感行为类型列表。</summary>
        public int[] semantics_list { get; set; }
    }

    /// <summary>关键词规则的对象或会话类型列表。</summary>
    public class ChatDataKeywordTypeList
    {
        /// <summary>类型列表。</summary>
        public int[] type_list { get; set; }
    }

    /// <summary>关键词规则的部门范围。</summary>
    public class ChatDataKeywordDepartmentList
    {
        /// <summary>部门 ID 列表。</summary>
        public long[] id_list { get; set; }
    }

    /// <summary>关键词规则的字符串 ID 范围。</summary>
    public class ChatDataKeywordIdList
    {
        /// <summary>成员、外部联系人或群聊 ID 列表。</summary>
        public string[] id_list { get; set; }
    }

    /// <summary>关键词规则的会话范围。</summary>
    public class ChatDataKeywordSessionTypeList
    {
        /// <summary>会话范围类型列表。</summary>
        public int[] session_type_list { get; set; }
    }

    /// <summary>关键词规则的手机号码白名单。</summary>
    public class ChatDataKeywordMobileList
    {
        /// <summary>手机号码列表。</summary>
        public string[] mobile_list { get; set; }
    }

    /// <summary>关键词规则的邮箱白名单。</summary>
    public class ChatDataKeywordEmailList
    {
        /// <summary>邮箱列表。</summary>
        public string[] email_list { get; set; }
    }

    /// <summary>关键词规则的银行卡号白名单。</summary>
    public class ChatDataKeywordBankCardList
    {
        /// <summary>银行卡号列表。</summary>
        public string[] bank_card_list { get; set; }
    }

    /// <summary>敏感关键词规则适用范围。</summary>
    public class ChatDataKeywordApplicableRange
    {
        /// <summary>适用对象类型。</summary>
        public ChatDataKeywordTypeList target_type { get; set; }

        /// <summary>适用会话类型。</summary>
        public ChatDataKeywordTypeList chat_type { get; set; }

        /// <summary>适用部门。</summary>
        public ChatDataKeywordDepartmentList department { get; set; }

        /// <summary>适用成员。</summary>
        public ChatDataKeywordIdList user { get; set; }

        /// <summary>适用外部联系人。</summary>
        public ChatDataKeywordIdList external_contact { get; set; }

        /// <summary>适用群聊。</summary>
        public ChatDataKeywordIdList chat { get; set; }

        /// <summary>适用会话范围。</summary>
        public ChatDataKeywordSessionTypeList session_type { get; set; }

        /// <summary>手机号码白名单。</summary>
        public ChatDataKeywordMobileList exclude_mobile { get; set; }

        /// <summary>邮箱白名单。</summary>
        public ChatDataKeywordEmailList exclude_email { get; set; }

        /// <summary>银行卡号白名单。</summary>
        public ChatDataKeywordBankCardList exclude_bank_card { get; set; }
    }

    /// <summary>敏感关键词规则内容。</summary>
    public class ChatDataKeywordRuleRequest
    {
        /// <summary>规则名称。</summary>
        public string name { get; set; }

        /// <summary>关键词列表。</summary>
        public ChatDataKeywordWords keyword { get; set; }

        /// <summary>敏感行为列表。</summary>
        public ChatDataKeywordSemantics semantics { get; set; }

        /// <summary>规则适用范围。</summary>
        public ChatDataKeywordApplicableRange applicable_range { get; set; }
    }

    /// <summary>创建敏感关键词规则请求。</summary>
    public class ChatDataKeywordRuleCreateRequest : ChatDataKeywordRuleRequest
    {
    }

    /// <summary>更新敏感关键词规则请求。</summary>
    public class ChatDataKeywordRuleUpdateRequest : ChatDataKeywordRuleRequest
    {
        /// <summary>规则 ID。</summary>
        public string rule_id { get; set; }
    }

    /// <summary>敏感关键词规则 ID 请求。</summary>
    public class ChatDataKeywordRuleIdRequest
    {
        /// <summary>规则 ID。</summary>
        public string rule_id { get; set; }
    }

    /// <summary>敏感关键词规则创建结果。</summary>
    public class ChatDataKeywordRuleCreatedResult : WorkJsonResult
    {
        /// <summary>规则 ID。</summary>
        public string rule_id { get; set; }
    }

    /// <summary>分页获取敏感关键词规则列表请求。</summary>
    public class ChatDataKeywordRuleListRequest
    {
        /// <summary>分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>本次返回的最大数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>敏感关键词规则摘要。</summary>
    public class ChatDataKeywordRuleSummary
    {
        /// <summary>规则 ID。</summary>
        public string rule_id { get; set; }

        /// <summary>规则名称。</summary>
        public string name { get; set; }

        /// <summary>规则创建时间，Unix 时间戳。</summary>
        public long create_time { get; set; }
    }

    /// <summary>敏感关键词规则列表结果。</summary>
    public class ChatDataKeywordRuleListResult : WorkJsonResult
    {
        /// <summary>规则摘要列表。</summary>
        public ChatDataKeywordRuleSummary[] rule_list { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否仍有未返回的数据。</summary>
        public bool has_more { get; set; }
    }

    /// <summary>敏感关键词规则详情结果。</summary>
    public class ChatDataKeywordRuleDetailResult : WorkJsonResult
    {
        /// <summary>规则名称。</summary>
        public string name { get; set; }

        /// <summary>关键词列表。</summary>
        public ChatDataKeywordWords keyword { get; set; }

        /// <summary>敏感行为列表。</summary>
        public ChatDataKeywordSemantics semantics { get; set; }

        /// <summary>规则适用范围。</summary>
        public ChatDataKeywordApplicableRange applicable_range { get; set; }
    }

    /// <summary>分页获取命中敏感关键词规则的消息请求。</summary>
    public class ChatDataKeywordHitMessageListRequest
    {
        /// <summary>首次查询使用的令牌。</summary>
        public string token { get; set; }

        /// <summary>分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>本次返回的最大数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>消息命中的敏感关键词规则。</summary>
    public class ChatDataKeywordHitRule
    {
        /// <summary>规则 ID。</summary>
        public string rule_id { get; set; }

        /// <summary>是否命中规则中的关键词。</summary>
        public bool has_hit_keyword { get; set; }

        /// <summary>命中的敏感行为类型列表。</summary>
        public int[] semantics_list { get; set; }
    }

    /// <summary>命中敏感关键词规则的消息。</summary>
    public class ChatDataKeywordHitMessage
    {
        /// <summary>消息 ID。</summary>
        public string msgid { get; set; }

        /// <summary>命中的规则列表。</summary>
        public ChatDataKeywordHitRule[] hit_rule_list { get; set; }
    }

    /// <summary>命中敏感关键词规则的消息列表结果。</summary>
    public class ChatDataKeywordHitMessageListResult : WorkJsonResult
    {
        /// <summary>命中规则的消息列表。</summary>
        public ChatDataKeywordHitMessage[] msg_list { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否仍有未返回的数据。</summary>
        public bool has_more { get; set; }
    }
}
