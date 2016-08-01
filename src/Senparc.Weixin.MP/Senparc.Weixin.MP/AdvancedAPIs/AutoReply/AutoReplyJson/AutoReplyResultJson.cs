/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：AutoReplyResultJson.cs
    文件功能描述：获取自动回复规则返回结果
    
    
    创建标识：Senparc - 20150907
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.AutoReply
{
    public class GetCurrentAutoreplyInfoResult : WxJsonResult
    {
        /// <summary>
        /// 关注后自动回复是否开启，0代表未开启，1代表开启
        /// </summary>
        public int is_add_friend_reply_open { get; set; }
        /// <summary>
        /// 消息自动回复是否开启，0代表未开启，1代表开启
        /// </summary>
        public int is_autoreply_open { get; set; }
        /// <summary>
        /// 关注后自动回复的信息
        /// </summary>
        public AddFriendAutoReplyInfo add_friend_autoreply_info { get; set; }
        /// <summary>
        /// 消息自动回复的信息
        /// </summary>
        public MessageDefaultAutoReplyInfo message_default_autoreply_info { get; set; }
        /// <summary>
        /// 关键词自动回复的信息
        /// </summary>
        public KeywordAutoReplyInfo keyword_autoreply_info { get; set; }
    }

    public class AddFriendAutoReplyInfo
    {
        /// <summary>
        /// 自动回复的类型。关注后自动回复和消息自动回复的类型仅支持文本（text）、图片（img）、语音（voice）、视频（video），关键词自动回复则还多了图文消息（news）
        /// </summary>
        public AutoReplyType type { get; set; }
        /// <summary>
        /// 对于文本类型，content是文本内容，对于图文、图片、语音、视频类型，content是mediaID
        /// </summary>
        public string content { get; set; }
    }

    public class MessageDefaultAutoReplyInfo
    {
        /// <summary>
        /// 自动回复的类型。关注后自动回复和消息自动回复的类型仅支持文本（text）、图片（img）、语音（voice）、视频（video），关键词自动回复则还多了图文消息（news）
        /// </summary>
        public AutoReplyType type { get; set; }
        /// <summary>
        /// 对于文本类型，content是文本内容，对于图文、图片、语音、视频类型，content是mediaID
        /// </summary>
        public string content { get; set; }
    }

    public class KeywordAutoReplyInfo
    {
        /// <summary>
        /// 关键词自动回复的信息列表
        /// </summary>
        public List<KeywordAutoReplyInfo_Item> list { get; set; }
    }

    public class KeywordAutoReplyInfo_Item
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string rule_name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 回复模式，reply_all代表全部回复，random_one代表随机回复其中一条
        /// </summary>
        public AutoReplyMode reply_mode { get; set; }
        /// <summary>
        /// 匹配的关键词列表
        /// </summary>
        public List<KeywordListInfoItem> keyword_list_info { get; set; }
    }

    public class KeywordListInfoItem
    {
        /// <summary>
        /// 自动回复的类型。关注后自动回复和消息自动回复的类型仅支持文本（text）、图片（img）、语音（voice）、视频（video），关键词自动回复则还多了图文消息（news）
        /// </summary>
        public AutoReplyType type { get; set; }
        /// <summary>
        /// 回复模式，reply_all代表全部回复，random_one代表随机回复其中一条
        /// </summary>
        public AutoReplyMatchMode match_mode { get; set; }
        /// <summary>
        /// 对于文本类型，content是文本内容，对于图文、图片、语音、视频类型，content是mediaID
        /// </summary>
        public string content { get; set; }
    }

    public class ReplyListInfoItem
    {
        /// <summary>
        /// 自动回复的类型。关注后自动回复和消息自动回复的类型仅支持文本（text）、图片（img）、语音（voice）、视频（video），关键词自动回复则还多了图文消息（news）
        /// </summary>
        public AutoReplyType type { get; set; }
        /// <summary>
        /// 图文消息的信息
        /// </summary>
        public NewsInfo news_info { get; set; }
        /// <summary>
        /// 对于文本类型，content是文本内容，对于图文、图片、语音、视频类型，content是mediaID
        /// </summary>
        public string content { get; set; }
    }

    public class NewsInfo
    {
        /// <summary>
        /// 图文消息的信息列表
        /// </summary>
        public List<NewsInfoItem> list { get; set; }
    }

    public class NewsInfoItem
    {
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 是否显示封面，0为不显示，1为显示
        /// </summary>
        public int show_cover { get; set; }
        /// <summary>
        /// 封面图片的URL
        /// </summary>
        public string cover_url { get; set; }
        /// <summary>
        /// 正文的URL
        /// </summary>
        public string content_url { get; set; }
        /// <summary>
        /// 原文的URL，若置空则无查看原文入口
        /// </summary>
        public string source_url { get; set; }
    }
}

