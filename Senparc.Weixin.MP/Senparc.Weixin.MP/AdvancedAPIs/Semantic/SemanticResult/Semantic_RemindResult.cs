using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
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
