/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalCatalogJson.cs
    文件功能描述：客户群转换、商品图册与聊天敏感词强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增客户群转换、商品图册与聊天敏感词模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>客户群 OpenGID 转换请求。</summary>
    public class OpenGidToChatIdRequest
    {
        public string opengid { get; set; }
    }

    /// <summary>客户群 OpenGID 转换结果。</summary>
    public class OpenGidToChatIdResult : WorkJsonResult
    {
        public string chat_id { get; set; }
    }

    /// <summary>商品图册图片。</summary>
    public class ProductAlbumImage
    {
        public string media_id { get; set; }
    }

    /// <summary>商品图册附件。</summary>
    public class ProductAlbumAttachment
    {
        public string type { get; set; }
        public ProductAlbumImage image { get; set; }
    }

    /// <summary>创建商品图册请求。</summary>
    public class ProductAlbumCreateRequest
    {
        public string description { get; set; }
        public long price { get; set; }
        public string product_sn { get; set; }
        public IList<ProductAlbumAttachment> attachments { get; set; }
    }

    /// <summary>创建商品图册结果。</summary>
    public class ProductAlbumCreateResult : WorkJsonResult
    {
        public string product_id { get; set; }
    }

    /// <summary>指定商品图册请求。</summary>
    public class ProductAlbumIdRequest
    {
        public string product_id { get; set; }
    }

    /// <summary>商品图册详情。</summary>
    public class ProductAlbumInfo
    {
        public string product_id { get; set; }
        public string description { get; set; }
        public long price { get; set; }
        public long create_time { get; set; }
        public string product_sn { get; set; }
        public IList<ProductAlbumAttachment> attachments { get; set; }
    }

    /// <summary>商品图册详情结果。</summary>
    public class ProductAlbumResult : WorkJsonResult
    {
        public ProductAlbumInfo product { get; set; }
    }

    /// <summary>商品图册列表请求。</summary>
    public class ProductAlbumListRequest
    {
        public int? limit { get; set; }
        public string cursor { get; set; }
    }

    /// <summary>商品图册列表结果。</summary>
    public class ProductAlbumListResult : WorkJsonResult
    {
        public string next_cursor { get; set; }
        public IList<ProductAlbumInfo> product_list { get; set; }
    }

    /// <summary>更新商品图册请求。</summary>
    public class ProductAlbumUpdateRequest : ProductAlbumIdRequest
    {
        public string description { get; set; }
        public long? price { get; set; }
        public string product_sn { get; set; }
        public IList<ProductAlbumAttachment> attachments { get; set; }
    }

    /// <summary>聊天敏感词适用范围。</summary>
    public class InterceptRuleRange
    {
        public IList<string> user_list { get; set; }
        public IList<long> department_list { get; set; }
    }

    /// <summary>额外的聊天敏感语义规则。</summary>
    public class InterceptExtraRule
    {
        public IList<int> semantics_list { get; set; }
    }

    /// <summary>创建聊天敏感词规则请求。</summary>
    public class InterceptRuleCreateRequest
    {
        public string rule_name { get; set; }
        public IList<string> word_list { get; set; }
        public IList<int> semantics_list { get; set; }
        public int intercept_type { get; set; }
        public InterceptRuleRange applicable_range { get; set; }
    }

    /// <summary>创建聊天敏感词规则结果。</summary>
    public class InterceptRuleCreateResult : WorkJsonResult
    {
        public string rule_id { get; set; }
    }

    /// <summary>聊天敏感词规则摘要。</summary>
    public class InterceptRuleSummary
    {
        public string rule_id { get; set; }
        public string rule_name { get; set; }
        public long create_time { get; set; }
    }

    /// <summary>聊天敏感词规则列表结果。</summary>
    public class InterceptRuleListResult : WorkJsonResult
    {
        public IList<InterceptRuleSummary> rule_list { get; set; }
    }

    /// <summary>指定聊天敏感词规则请求。</summary>
    public class InterceptRuleIdRequest
    {
        public string rule_id { get; set; }
    }

    /// <summary>聊天敏感词规则详情。</summary>
    public class InterceptRuleInfo : InterceptRuleSummary
    {
        public IList<string> word_list { get; set; }

        /// <summary>兼容官方详情示例中的顶层语义列表。</summary>
        public IList<int> semantics_list { get; set; }

        /// <summary>兼容官方参数表中的额外规则结构。</summary>
        public InterceptExtraRule extra_rule { get; set; }

        public int intercept_type { get; set; }
        public InterceptRuleRange applicable_range { get; set; }
    }

    /// <summary>聊天敏感词规则详情结果。</summary>
    public class InterceptRuleResult : WorkJsonResult
    {
        public InterceptRuleInfo rule { get; set; }
    }

    /// <summary>更新聊天敏感词规则请求。</summary>
    public class InterceptRuleUpdateRequest : InterceptRuleIdRequest
    {
        public string rule_name { get; set; }
        public IList<string> word_list { get; set; }
        public InterceptExtraRule extra_rule { get; set; }
        public int? intercept_type { get; set; }
        public InterceptRuleRange add_applicable_range { get; set; }
        public InterceptRuleRange remove_applicable_range { get; set; }
    }
}
