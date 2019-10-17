/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetMediaListResultJson.cs
    文件功能描述：获取素材列表返回结果
    
    
    创建标识：Senparc - 20150324
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

namespace Senparc.Weixin.MP.AdvancedAPIs.Media
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
    /// 图文素材的Item
    /// </summary>
    public class MediaList_NewsResult : BaseMediaListResultJson
    {
        public List<MediaList_News_Item> item { get; set; }
    }

    public class MediaList_News_Item
    {
        public string media_id { get; set; }
        public Media_News_Content content { get; set; }
        /// <summary>
        /// 这个素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
    }

    public class Media_News_Content
    {
        public List<Media_News_Content_Item> news_item { get; set; } 
    }

    public class Media_News_Content_Item : NewsModel
    {
        public string url { get; set; }

        /// <summary>
        /// 封面图片的url
        /// </summary>
        public string thumb_url { get; set; }
    }

    /// <summary>
    /// 除图文之外的其他素材的Item
    /// </summary>
    public class MediaList_OthersResult : BaseMediaListResultJson
    {
        public List<MediaList_Others_Item> item { get; set; }
    }

    public class MediaList_Others_Item
    {
        public string media_id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 这个素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
        /// <summary>
        /// 图文页的URL，或者，当获取的列表是图片素材列表时，该字段是图片的URL
        /// </summary>
        public string url { get; set; }
    }
}
