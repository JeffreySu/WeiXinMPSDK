using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.FreePublish.FreePublishJson
{
    public class FreePublishBatchGetResultJson : BaseFreePublishListResultJson
    {
        public List<FreePublishList_Item> item { get; set; }
    }

    public class BaseFreePublishListResultJson : WxJsonResult
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


    public class FreePublishList_Item
    {
        /// <summary>
        /// 成功发布的图文消息id
        /// </summary>
        public string article_id { get; set; }
        /// <summary>
        /// 图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS。
        /// </summary>
        public FreePublishGetArticleResultJson content { get; set; }
        /// <summary>
        /// 这篇图文消息素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
    }
}