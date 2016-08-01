/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_BaikeResult.cs
    文件功能描述：语意理解接口百科服务（baike）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
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
