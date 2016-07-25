/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Semantic_RemindResult.cs
    文件功能描述：语意理解接口提醒服务（remind）返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 提醒服务（remind）
    /// </summary>
    public class Semantic_RemindResult : BaseSemanticResultJson
    {
        public Semantic_Remind semantic { get; set; }
    }

    public class Semantic_Remind : BaseSemanticIntent
    {
        public Semantic_Details_Remind details { get; set; }
    }

    public class Semantic_Details_Remind
    {
        /// <summary>
        /// 时间
        /// </summary>
        public Semantic_DateTime datetime { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        public string @event { get; set; }
        /// <summary>
        /// 类别：0提醒；1闹钟  注：提醒有具体事件，闹钟没有具体事件
        /// </summary>
        public int remind_type { get; set; }
    }
}
