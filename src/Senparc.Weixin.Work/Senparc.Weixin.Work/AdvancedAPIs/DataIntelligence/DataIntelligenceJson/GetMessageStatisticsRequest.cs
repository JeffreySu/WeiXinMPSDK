/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：GetMessageStatisticsRequest.cs
    文件功能描述：获取消息统计请求参数
    
    
    创建标识：Senparc - 20241128
    
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>
    /// 获取消息统计请求参数
    /// </summary>
    public class GetMessageStatisticsRequest
    {
        /// <summary>
        /// 统计开始时间，Unix时间戳
        /// </summary>
        public long starttime { get; set; }

        /// <summary>
        /// 统计结束时间，Unix时间戳
        /// </summary>
        public long endtime { get; set; }

        /// <summary>
        /// 统计类型：day(按天), week(按周), month(按月)
        /// </summary>
        public string type { get; set; } = "day";

        /// <summary>
        /// 应用ID，为空时统计全部应用
        /// </summary>
        public string agentid { get; set; }

        /// <summary>
        /// 用户ID列表，为空时统计全部用户
        /// </summary>
        public string[] userids { get; set; }
    }
}