using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 电视节目预告（tv）
    /// </summary>
    public class Semantic_TVResult : BaseSemanticResultJson
    {
        public Semantic_TV semantic { get; set; }
    }

    public class Semantic_TV : BaseSemanticIntent
    {
        public Semantic_Details_TV details { get; set; }
    }

    public class Semantic_Details_TV
    {
        /// <summary>
        /// 播放时间
        /// </summary>
        public Semantic_DateTime datetime { get; set; }
        /// <summary>
        /// 电视台名称
        /// </summary>
        public string tv_name { get; set; }
        /// <summary>
        /// 电视频道名称
        /// </summary>
        public string tv_channel { get; set; }
        /// <summary>
        /// 节目名称
        /// </summary>
        public string show_name { get; set; }
        /// <summary>
        /// 节目类型
        /// </summary>
        public string category { get; set; }
    }
}
