/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_WeatherResult.cs
    文件功能描述：语意理解接口天气服务（weather）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
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