/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：GetMessageStatisticsResult.cs
    文件功能描述：获取消息统计返回结果
    
    
    创建标识：Senparc - 20241128
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>
    /// 获取消息统计返回结果
    /// </summary>
    public class GetMessageStatisticsResult : WorkJsonResult
    {
        /// <summary>
        /// 统计数据列表
        /// </summary>
        public MessageStatistics[] statistics { get; set; }
    }

    /// <summary>
    /// 消息统计数据
    /// </summary>
    public class MessageStatistics
    {
        /// <summary>
        /// 统计日期，Unix时间戳
        /// </summary>
        public long date { get; set; }

        /// <summary>
        /// 发送消息总数
        /// </summary>
        public int total_send { get; set; }

        /// <summary>
        /// 接收消息总数
        /// </summary>
        public int total_receive { get; set; }

        /// <summary>
        /// 文本消息数量
        /// </summary>
        public int text_count { get; set; }

        /// <summary>
        /// 图片消息数量
        /// </summary>
        public int image_count { get; set; }

        /// <summary>
        /// 语音消息数量
        /// </summary>
        public int voice_count { get; set; }

        /// <summary>
        /// 视频消息数量
        /// </summary>
        public int video_count { get; set; }

        /// <summary>
        /// 文件消息数量
        /// </summary>
        public int file_count { get; set; }

        /// <summary>
        /// 链接消息数量
        /// </summary>
        public int link_count { get; set; }

        /// <summary>
        /// 位置消息数量
        /// </summary>
        public int location_count { get; set; }

        /// <summary>
        /// 活跃用户数
        /// </summary>
        public int active_users { get; set; }

        /// <summary>
        /// 活跃群聊数
        /// </summary>
        public int active_groups { get; set; }
    }
}
