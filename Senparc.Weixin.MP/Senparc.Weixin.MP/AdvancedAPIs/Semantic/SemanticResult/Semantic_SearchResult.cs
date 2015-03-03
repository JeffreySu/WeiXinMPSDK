using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 网页搜索（search））
    /// </summary>
    public class Semantic_SearchResult : BaseSemanticResultJson
    {
        public Semantic_Search semantic { get; set; }
    }

    public class Semantic_Search : BaseSemanticIntent
    {
        public Semantic_Details_Search details { get; set; }
    }

    public class Semantic_Details_Search
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 搜索引擎类型：google, baidu, sogou, 360, taobao,jingdong
        /// </summary>
        public string channel { get; set; }
    }
}