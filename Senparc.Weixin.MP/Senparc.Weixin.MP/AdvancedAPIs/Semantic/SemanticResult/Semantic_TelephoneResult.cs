/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_TelephoneResult.cs
    文件功能描述：语意理解接口常用电话服务（telephone）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 常用电话服务（telephone）
    /// </summary>
    public class Semantic_TelephoneResult : BaseSemanticResultJson
    {
        public Semantic_Telephone semantic { get; set; }
    }

    public class Semantic_Telephone : BaseSemanticIntent
    {
        public Details_Telephone details { get; set; }
    }

    public class Details_Telephone
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string telephone { get; set; }
    }
}
