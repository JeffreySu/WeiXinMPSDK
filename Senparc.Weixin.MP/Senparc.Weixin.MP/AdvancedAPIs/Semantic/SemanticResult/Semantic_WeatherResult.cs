using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 天气服务（weather）
    /// </summary>
    public class Semantic_WeatherResult : BaseSemanticResultJson
    {
        public Semantic_Weather semantic { get; set; }
    }

    public class Semantic_Weather : BaseSemanticIntent
    {
        public Semantic_Details_Weather details { get; set; }
    }

    public class Semantic_Details_Weather
    {
        /// <summary>
        /// 地点
        /// </summary>
        public Semantic_Location location { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public Semantic_DateTime datetime { get; set; }
    }
}