/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
    
    文件名：GetDraftResult.cs
    文件功能描述：获取草稿返回的结果
    
    
    创建标识：zhongmeng - 2022224
 
   
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft
{
    /// <summary>
    /// 获取草稿返回结果
    /// </summary>
    public class GetDraftResult : WxJsonResult
    {
        public List<DraftItem> news_item { get; set; }
    }

    public class DraftItem : NewsModel
    {
        /// <summary>
        /// 图文页的URL
        /// </summary>
        public string url { get; set; }
    }

}
