using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 百科服务（baike）
    /// </summary>
    public class Semantic_BaikeResult : BaseSemanticResultJson
    {
        public Semantic_Baike semantic { get; set; }
    }

    public class Semantic_Baike : BaseSemanticIntent
    {
        public Semantic_Details_Baike details { get; set; }
    }

    public class Semantic_Details_Baike
    {
        /// <summary>
        /// 百科关键词
        /// </summary>
        public string keyword { get; set; }
    }
}
