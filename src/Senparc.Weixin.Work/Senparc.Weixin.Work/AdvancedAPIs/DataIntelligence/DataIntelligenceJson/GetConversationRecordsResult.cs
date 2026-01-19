/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：GetConversationRecordsResult.cs
    文件功能描述：获取会话记录返回结果
    
    
    创建标识：Senparc - 20241128
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>
    /// 获取会话记录返回结果
    /// </summary>
    public class GetConversationRecordsResult : WorkJsonResult
    {
        /// <summary>
        /// 是否还有更多数据
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 下次拉取的cursor
        /// </summary>
        public string next_cursor { get; set; }

        /// <summary>
        /// 会话记录列表
        /// </summary>
        public ConversationRecord[] records { get; set; }
    }

    /// <summary>
    /// 会话记录
    /// </summary>
    public class ConversationRecord
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string msgid { get; set; }

        /// <summary>
        /// 消息类型，text/image/voice/video/file/location/link等
        /// </summary>
        public string msgtype { get; set; }

        /// <summary>
        /// 发送者用户ID
        /// </summary>
        public string from { get; set; }

        /// <summary>
        /// 接收者用户ID，群聊时为空
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// 群聊ID，单聊时为空
        /// </summary>
        public string roomid { get; set; }

        /// <summary>
        /// 消息发送时间，Unix时间戳
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public ConversationContent content { get; set; }
    }

    /// <summary>
    /// 消息内容
    /// </summary>
    public class ConversationContent
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 媒体文件ID
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long filesize { get; set; }

        /// <summary>
        /// 链接标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 链接描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 位置纬度
        /// </summary>
        public double latitude { get; set; }

        /// <summary>
        /// 位置经度
        /// </summary>
        public double longitude { get; set; }

        /// <summary>
        /// 位置名称
        /// </summary>
        public string location_name { get; set; }

        /// <summary>
        /// 位置地址
        /// </summary>
        public string address { get; set; }
    }
}
