/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocContentJson.cs
    文件功能描述：企业微信在线文档内容强类型边界模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐在线文档内容强类型边界模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text.Json;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>获取在线文档内容请求。</summary>
    public class WeDocContentRequest
    {
        /// <summary>文档 DocID。</summary>
        public string docid { get; set; }

        /// <summary>起始位置。</summary>
        public int? start { get; set; }

        /// <summary>本次返回内容数量上限。</summary>
        public int? limit { get; set; }

    }

    /// <summary>在线文档内容结果。</summary>
    public class WeDocContentResult : WorkJsonResult
    {
        /// <summary>文档 DocID。</summary>
        public string docid { get; set; }

        /// <summary>文档块级内容；结构由企业微信文档块类型决定。</summary>
        public JsonElement content { get; set; }

        /// <summary>兼容部分协议版本使用的文档内容字段。</summary>
        public JsonElement doc_content { get; set; }

        /// <summary>是否还有更多内容。</summary>
        public bool? has_more { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>编辑在线文档内容请求。</summary>
    public class WeDocContentModifyRequest
    {
        /// <summary>文档 DocID。</summary>
        public string docid { get; set; }

        /// <summary>文档编辑操作列表；每项保留企业微信块级操作的原始 JSON 结构。</summary>
        public IList<JsonElement> requests { get; set; }

        /// <summary>客户端幂等标识。</summary>
        public string client_token { get; set; }
    }
}
