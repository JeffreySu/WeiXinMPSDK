using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取草稿列表返回结果
    /// </summary>
    public class GetDraftResultJson : WxJsonResult
    {
        public List<DraftItem> news_item { get; set; }
    }  
    
    public class GetDraftCountResultJson : WxJsonResult
    {
        public int total_count { get; set; }
    }

    public class DraftItem : DraftModel
    {
        /// <summary>
        /// 草稿的临时链接
        /// </summary>
        public string url { get; set; }
    }
}
