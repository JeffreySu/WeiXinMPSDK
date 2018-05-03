/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：SendChatMessageData.cs
    文件功能描述：会话接口返回结果
    
    
    创建标识：Senparc - 20150728

    修改标识：pekrr1e  - 20180503
    修改描述：v1.4.0 新增企业微信群聊会话功能支持

----------------------------------------------------------------*/

using Senparc.Weixin.Work.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Chat
{
    /// <summary>
    /// 发送消息基础数据
    /// </summary>
    public class BaseSendChatMessageData
    {
        public BaseSendChatMessageData(string chatid, int? safe = null)
        {
            this.chatid = chatid;
            this.safe = safe;
        }
        /// <summary>
        /// 群聊id
        /// </summary>
        public string chatid { get; set; }
        public int? safe { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; set; }
    }

    /// <summary>
    /// 发送文本消息数据
    /// </summary>
    public class SendTextMessageData : BaseSendChatMessageData
    {
        public SendTextMessageData(string chatid, int? safe = null) : base(chatid, safe)
        {
            msgtype = ChatMsgType.text.ToString();
        }
        public SendTextMessageData(string chatid, string content, int? safe = null) : this(chatid, safe)
        {
            text = new Chat_Content(content);
        }
        public Chat_Content text { get; set; }
    }

    public class Chat_Content
    {
        public Chat_Content(string content)
        {
            this.content = content;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 发送图片消息数据
    /// </summary>
    public class SendImageMessageData : BaseSendChatMessageData
    {
        public SendImageMessageData(string chatid, int? safe = null) : base(chatid, safe)
        {
            msgtype = ChatMsgType.image.ToString();
        }
        public SendImageMessageData(string chatid, string media_id, int? safe = null) : this(chatid, safe)
        {
            image = new Chat_Media(media_id);
        }
        public Chat_Media image { get; set; }
    }
    /// <summary>
    /// 发送语音消息数据
    /// </summary>
    public class SendVoiceMessageData : BaseSendChatMessageData
    {
        public SendVoiceMessageData(string chatid, int? safe = null) : base(chatid, safe)
        {
            msgtype = ChatMsgType.voice.ToString();
        }
        public SendVoiceMessageData(string chatid, string media_id, int? safe = null) : this(chatid, safe)
        {
            voice = new Chat_Media(media_id);
        }
        public Chat_Media voice { get; set; }
    }
    /// <summary>
    /// 发送文件消息数据
    /// </summary>
    public class SendFileMessageData : BaseSendChatMessageData
    {
        public SendFileMessageData(string chatid, int? safe = null) : base(chatid, safe)
        {
            msgtype = ChatMsgType.file.ToString();
        }
        public SendFileMessageData(string chatid, string media_id, int? safe = null) : this(chatid, safe)
        {
            file = new Chat_Media(media_id);
        }
        public Chat_Media file { get; set; }
    }

    public class Chat_Media
    {
        public Chat_Media(string media_id)
        {
            this.media_id = media_id;
        }
        /// <summary>
        /// 媒体或文件id，可以调用上传素材文件接口获取
        /// </summary>
        public string media_id { get; set; }
    }
    public class SendVideoMessageData : BaseSendChatMessageData
    {
        public SendVideoMessageData(string chatid, int? safe = null) : base(chatid, safe)
        {
            msgtype = ChatMsgType.video.ToString();
        }
        public SendVideoMessageData(string chatid, string media_id, string title = null, string description = null, int? safe = null) : this(chatid, safe)
        {
            video = new Chat_MediaVideo(media_id, title, description);
        }
        public SendVideoMessageData(string chatid, Chat_MediaVideo video, int? safe = null) : this(chatid, safe)
        {
            this.video = video;
        }
        public Chat_MediaVideo video { get; set; }
    }
    public class Chat_MediaVideo : Chat_Media
    {
        public Chat_MediaVideo(string media_id, string title = null, string description = null) : base(media_id)
        {
            this.title = title;
            this.description = description;
        }
        /// <summary>
        /// 视频消息的标题，不超过128个字节，超过会自动截断
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 视频消息的描述，不超过512个字节，超过会自动截断
        /// </summary>
        public string description { get; set; }
    }
    public class SendTextCardMessageData : BaseSendChatMessageData
    {
        public SendTextCardMessageData(string chatid, int? safe = null) : base(chatid, safe)
        {
            msgtype = ChatMsgType.textcard.ToString();
        }
        public SendTextCardMessageData(string chatid, string title, string description, string url, string btntxt = null, int? safe = null) : this(chatid, safe)
        {
            textcard = new Chat_TextCard(title, description, url, btntxt);
        }
        public SendTextCardMessageData(string chatid, Chat_TextCard textcard, int? safe = null) : this(chatid, safe)
        {
            this.textcard = textcard;
        }
        public Chat_TextCard textcard { get; set; }
    }
    public class Chat_TextCard
    {
        public Chat_TextCard(string title, string description, string url, string btntxt = null)
        {
            this.title = title;
            this.description = description;
            this.url = url;
            this.btntxt = btntxt;
        }
        /// <summary>
        /// 标题，不超过128个字节，超过会自动截断
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述，不超过512个字节，超过会自动截断
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 点击后跳转的链接。
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 按钮文字。 默认为“详情”， 不超过4个文字，超过自动截断。
        /// </summary>
        public string btntxt { get; set; }
    }
    public class SendNewsMessageData : BaseSendChatMessageData
    {
        public SendNewsMessageData(string chatid, int? safe = null) : base(chatid, safe)
        {
            msgtype = ChatMsgType.news.ToString();
        }
        public SendNewsMessageData(string chatid, List<Article> articles, string title, string url, string description = null, string picurl = null, string btntxt = null, int? safe = null) : this(chatid, safe)
        {
            news = new Chat_News(articles, title, url, description, picurl, btntxt);
        }
        public SendNewsMessageData(string chatid, Chat_News news, int? safe = null) : this(chatid, safe)
        {
            this.news = news;
        }

        public Chat_News news { get; set; }
    }
    public class Chat_NewsBase
    {
        public Chat_NewsBase(List<Article> articles, string title)
        {
            this.articles = articles;
            this.title = title;
        }
        /// <summary>
        /// 图文消息，一个图文消息支持1到8条图文
        /// </summary>
        public List<Article> articles { get; set; }
        /// <summary>
        /// 标题，不超过128个字节，超过会自动截断
        /// </summary>
        public string title { get; set; }
    }
    public class Chat_News : Chat_NewsBase
    {
        public Chat_News(List<Article> articles, string title, string url, string description = null, string picurl = null, string btntxt = null) : base(articles, title)
        {
            this.url = url;
            this.description = description;
            this.picurl = picurl;
            this.btntxt = btntxt;
        }
        /// <summary>
        /// 点击后跳转的链接。
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 描述，不超过512个字节，超过会自动截断
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640320，小图8080。
        /// </summary>
        public string picurl { get; set; }
        /// <summary>
        /// 按钮文字。 默认为“详情”， 不超过4个文字，超过自动截断。
        /// </summary>
        public string btntxt { get; set; }
    }
    public class SendMpNewsMessageData : BaseSendChatMessageData
    {
        public SendMpNewsMessageData(string chatid, int? safe = null) : base(chatid, safe)
        {
            msgtype = ChatMsgType.mpnews.ToString();
        }
        public SendMpNewsMessageData(string chatid, List<Article> articles, string title, string thumb_media_id, string content, string author = null, string content_source_url = null, string digest = null, int? safe = null) : this(chatid, safe)
        {
            mpnews = new Chat_MpNews(articles, title, thumb_media_id, content, author, content_source_url, digest);
        }
        public SendMpNewsMessageData(string chatid, Chat_MpNews mpnews, int? safe = null) : this(chatid, safe)
        {
            this.mpnews = mpnews;
        }
        public Chat_MpNews mpnews { get; set; }
    }
    public class Chat_MpNews : Chat_NewsBase
    {
        public Chat_MpNews(List<Article> articles, string title, string thumb_media_id, string content, string author = null, string content_source_url = null, string digest = null) : base(articles, title)
        {
            this.articles = articles;
            this.title = title;
            this.thumb_media_id = thumb_media_id;
            this.content = content;
            this.author = author;
            this.content_source_url = content_source_url;
            this.digest = digest;
        }
        /// <summary>
        /// 图文消息缩略图的media_id, 可以通过素材管理接口获得。此处thumb_media_id即上传接口返回的media_id
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 图文消息的作者，不超过64个字节
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 图文消息点击“阅读原文”之后的页面链接
        /// </summary>
        public string content_source_url { get; set; }
        /// <summary>
        /// 图文消息的内容，支持html标签，不超过666 K个字节
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文消息的描述，不超过512个字节，超过会自动截断
        /// </summary>
        public string digest { get; set; }
    }
}