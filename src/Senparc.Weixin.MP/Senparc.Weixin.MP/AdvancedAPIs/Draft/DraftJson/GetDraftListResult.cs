/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
    
    文件名：GetDraftListResult.cs
    文件功能描述：获取草稿列表返回结果
    
    
    创建标识：zhongmeng - 2022224
 
   
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft
{
  

    /// <summary>
    /// 图文素材的Item
    /// </summary>
    public class GetDraftListResult : WxJsonResult
    {

        /// <summary>
        /// 草稿的总数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 本次调用获取的草稿的数量
        /// </summary>
        public int item_count { get; set; }
        public List<DraftList_Item> item { get; set; }
    }

    public class DraftList_Item
    {
        public string media_id { get; set; }
        public Draft_Content content { get; set; }
        /// <summary>
        /// 这个草稿的最后更新时间
        /// </summary>
        public long update_time { get; set; }
    }

    public class Draft_Content
    {
        public List<Draft_Content_Item> news_item { get; set; } 
    }

    public class Draft_Content_Item : NewsModel
    {
        public string url { get; set; }

        /// <summary>
        /// 封面图片的url
        /// </summary>
        public string thumb_url { get; set; }
    }

  

   
}
