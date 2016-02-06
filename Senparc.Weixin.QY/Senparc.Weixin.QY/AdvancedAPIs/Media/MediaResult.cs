/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：UploadResultJson.cs
    文件功能描述：素材管理接口返回结果
    
    
    创建标识：Senparc - 20150708
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.QY.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs.Media
{
    /// <summary>
    /// 获取永久图文素材返回结果
    /// </summary>
    public class GetForeverMpNewsResult : QyJsonResult
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        public GetForeverMpNewsResult_MpNews mpnews { get; set; }
    }

    public class GetForeverMpNewsResult_MpNews
    {
        public List<MpNewsArticle> articles { get; set; }
    }

    /// <summary>
    /// 获取素材总数返回结果
    /// </summary>
    public class GetCountResult : QyJsonResult
    {
        /// <summary>
        /// 应用素材总数目
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 图片素材总数目
        /// </summary>
        public int image_count { get; set; }
        /// <summary>
        /// 音频素材总数目
        /// </summary>
        public int voice_count { get; set; }
        /// <summary>
        /// 视频素材总数目
        /// </summary>
        public int video_count { get; set; }
        /// <summary>
        /// 文件素材总数目
        /// </summary>
        public int file_count { get; set; }
        /// <summary>
        /// 图文素材总数目
        /// </summary>
        public int mpnews_count { get; set; }
    }

    /// <summary>
    /// 获取素材列表返回结果
    /// </summary>
    public class BatchGetMaterialResult : QyJsonResult
    {
        /// <summary>
        /// 素材类型，可以为图文(mpnews)、图片（image）、音频（voice）、视频（video）、文件（file）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 应用该类型素材总数目
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 返回该类型素材数目
        /// </summary>
        public int item_count { get; set; }
        /// <summary>
        /// 素材列表
        /// </summary>
        public List<BatchGetMaterial_Item> itemlist { get; set; }
    }

    public class BatchGetMaterial_Item
    {
        /// <summary>
        /// 素材的媒体id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public long update_time { get; set; }
        /// <summary>
        /// 图文消息，一个图文消息支持1到10个图文
        /// </summary>
        public List<MpNewsArticle> articles { get; set; }
    }
}
