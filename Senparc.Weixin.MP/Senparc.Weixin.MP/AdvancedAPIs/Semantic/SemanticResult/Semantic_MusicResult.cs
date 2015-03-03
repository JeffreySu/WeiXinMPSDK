using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 音乐服务（music）
    /// </summary>
    public class Semantic_MusicResult : BaseSemanticResultJson
    {
        public Semantic_Music semantic { get; set; }
    }

    public class Semantic_Music : BaseSemanticIntent
    {
        public Semantic_Details_Music details { get; set; }
    }

    public class Semantic_Details_Music
    {
        /// <summary>
        /// 歌曲名
        /// </summary>
        public string song { get; set; }
        /// <summary>
        /// 歌手
        /// </summary>
        public string singer { get; set; }
        /// <summary>
        /// 专辑
        /// </summary>
        public string album { get; set; }
        /// <summary>
        /// 歌曲类型
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 语言：中文，英文，韩文，日文，…
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 电影名
        /// </summary>
        public string movie { get; set; }
        /// <summary>
        /// 电视剧名
        /// </summary>
        public string tv { get; set; }
        /// <summary>
        /// 节目名
        /// </summary>
        public string show { get; set; }
        /// <summary>
        /// 排序类型：0排序无要求（默认），1时间升序，2时间降序，3热度高优先级
        /// </summary>
        public int sort { get; set; }
    }
}
