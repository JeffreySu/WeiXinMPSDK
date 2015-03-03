using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 资讯服务（news）
    /// </summary>
    public class Semantic_NewsResult : BaseSemanticResultJson
    {
        public Semantic_News semantic { get; set; }
    }

    public class Semantic_News : BaseSemanticIntent
    {
        public Semantic_Details_News details { get; set; }
    }

    public class Semantic_Details_News
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 新闻类别
        /// </summary>
        public string category { get; set; }
    }
}
