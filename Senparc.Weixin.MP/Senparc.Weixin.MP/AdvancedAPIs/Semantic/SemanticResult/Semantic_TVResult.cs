/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_TVResult.cs
    文件功能描述：语意理解接口电视节目预告（tv）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
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
