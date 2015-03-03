using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 网址服务（website）
    /// </summary>
    public class Semantic_WebsiteResult : BaseSemanticResultJson
    {
        public Semantic_Website semantic { get; set; }
    }

    public class Semantic_Website : BaseSemanticIntent
    {
        public Semantic_Details_Website details { get; set; }
    }

    public class Semantic_Details_Website
    {
        /// <summary>
        /// 网址名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; }
    }
}