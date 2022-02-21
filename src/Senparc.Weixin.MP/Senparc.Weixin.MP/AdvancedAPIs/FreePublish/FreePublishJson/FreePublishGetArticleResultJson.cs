using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.FreePublish.FreePublishJson
{
    public class FreePublishGetArticleResultJson : WxJsonResult
    {
        public List<FreePublishGetArticleItemJson> news_item { get; set; }
    }

    public class FreePublishGetArticleItemJson : Draft_Content_Item
    {
        /// <summary>
        /// 该图文是否被删除
        /// </summary>
        public bool is_deleted { get; set; }
    }
}