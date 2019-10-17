using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs.Media
{
    /// <summary>
    /// 视频永久素材
    /// </summary>
    public class GetForeverMediaVideoResultJson : WxJsonResult
    {
        /// <summary>
        /// 视频素材的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 视频素材的说明
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 视频素材的下载链接
        /// </summary>
        public string down_url { get; set; }
    }

    //public class GetForeverMediaResultJson_NewsItem
    //{
    //    /// <summary>
    //    /// 图文消息的标题
    //    /// </summary>
    //    public string title { get; set; }
    //    /// <summary>
    //    /// 图文消息的封面图片素材id（必须是永久mediaID）
    //    /// </summary>
    //    public string thumb_media_id { get; set; }
    //    /// <summary>
    //    /// 是否显示封面，0为false，即不显示，1为true，即显示
    //    /// </summary>
    //    public int show_cover_pic { get; set; }
    //    /// <summary>
    //    /// 作者
    //    /// </summary>
    //    public string author { get; set; }
    //    /// <summary>
    //    /// 图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空
    //    /// </summary>
    //    public string digest { get; set; }
    //    /// <summary>
    //    /// 图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS
    //    /// </summary>
    //    public string content { get; set; }
    //    /// <summary>
    //    /// 图文页的URL
    //    /// </summary>
    //    public string url { get; set; }
    //    /// <summary>
    //    /// 图文消息的原文地址，即点击“阅读原文”后的URL
    //    /// </summary>
    //    public string content_source_url { get; set; }
    //}
}
