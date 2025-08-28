/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：GetConversationRecordsRequest.cs
    文件功能描述：获取会话记录请求参数
    
    
    创建标识：Senparc - 20241128
    
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>
    /// 获取会话记录请求参数
    /// </summary>
    public class GetConversationRecordsRequest
    {
        /// <summary>
        /// 会话ID
        /// </summary>
        public string chatid { get; set; }

        /// <summary>
        /// 开始时间，Unix时间戳
        /// </summary>
        public long starttime { get; set; }

        /// <summary>
        /// 结束时间，Unix时间戳
        /// </summary>
        public long endtime { get; set; }

        /// <summary>
        /// 分页拉取的cursor，初始传入为空
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 分页拉取的数量限制，最大为1000
        /// </summary>
        public int limit { get; set; } = 100;
    }
}