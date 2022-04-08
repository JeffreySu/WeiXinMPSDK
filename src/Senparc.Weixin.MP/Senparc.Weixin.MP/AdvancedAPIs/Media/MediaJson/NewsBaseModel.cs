using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs.Media.MediaJson
{
    /// <summary>
    /// 获取素材总数返回结果
    /// </summary>
    public class BaseMediaListResultJson : WxJsonResult
    {
        /// <summary>
        /// 该类型的素材的总数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 本次调用获取的素材的数量
        /// </summary>
        public int item_count { get; set; }
    }

    /// <summary>
    /// 草稿图文素材
    /// </summary>
    public class DraftList_NewsResult : BaseMediaListResultJson
    {
        public List<MediaList_News_Item> item { get; set; }
    }

    public class MediaList_News_Item
    {
        public string media_id { get; set; }
        public Media_News_Content content { get; set; }
        /// <summary>
        /// 这个素材的创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 这个素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
    }

    public class Media_News_Content
    {
        public List<NewsBaseModel> news_item { get; set; }
    }


    /// <summary>
    /// 发布图文素材
    /// </summary>
    public class PublishList_NewsResult : BaseMediaListResultJson
    {
        public List<PublishNews_Item> item { get; set; }
    }
    public class PublishNews_Item
    {
        public string article_id { get; set; }
        public PublishNews_Content content { get; set; }
        /// <summary>
        /// 这个素材的创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 这个素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
    }
    public class PublishNews_Content
    {
        public List<PublishNews> news_item { get; set; }
    }

    public class PublishNews: NewsBaseModel
    {
        /// <summary>
        /// 该图文是否被删除
        /// </summary>
        public bool is_deleted { get; set; }

        /// <summary>
        /// 图文消息的URL
        /// </summary>
        public new string url { get; set; }
    }
    /// <summary>
    /// 图文消息模型
    /// </summary>
    public class NewsBaseModel
    {
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string digest { get; set; }

        /// <summary>
        /// 图文消息页面的内容，支持HTML标签
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        public string content_source_url { get; set; }

        /// <summary>
        /// 图文消息的封面图片素材id（一定是永久MediaID）
        /// </summary>
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 是否显示封面，1为显示，0为不显示
        /// </summary>
        public string show_cover_pic { get; set; }

        /// <summary>
        /// 封面图片的url
        /// </summary>
        public string thumb_url { get; set; }
        /// <summary>
        /// 是否打开评论，0不打开，1打开
        /// </summary>
        public int need_open_comment { get; set; }
        /// <summary>
        /// 是否粉丝才可评论，0所有人可评论，1粉丝才可评论
        /// </summary>
        public int only_fans_can_comment { get; set; }
        /// <summary>
        /// 草稿的临时链接
        /// </summary>
        public string url { get; set; }

    }
}
