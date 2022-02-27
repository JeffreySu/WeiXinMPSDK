using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 草稿列表的Item
    /// </summary>
    public class DraftList_Result : BaseDraftListResultJson
    {
        public List<DraftList_Item> item { get; set; }
    }

    /// <summary>
    /// 获取素材总数返回结果
    /// </summary>
    public class BaseDraftListResultJson : WxJsonResult
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

    public class DraftList_Item
    {
        /// <summary>
        /// 图文消息的id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS。
        /// </summary>
        public Draft_Content content { get; set; }
        /// <summary>
        /// 这篇图文消息素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
    }

    public class Draft_Content
    {
        public List<Draft_Content_Item> news_item { get; set; } 
    }

    public class Draft_Content_Item : DraftModel
    {
        public string url { get; set; }
    }
}
