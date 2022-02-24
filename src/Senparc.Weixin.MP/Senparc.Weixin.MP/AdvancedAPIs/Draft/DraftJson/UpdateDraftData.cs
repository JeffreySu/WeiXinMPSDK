/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
    
    文件名：UpdateDraftData.cs
    文件功能描述：修改草稿需要的数据
    
    
    创建标识：zhongmeng - 2022224
 
   
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft
{
    /// <summary>
    /// 修改草稿需要post的数据
    /// </summary>
    public class UpdateDraftData
    {
        /// <summary>
        /// 要修改的图文消息的id
        /// </summary>
        public long media_id { get; set; }
        /// <summary>
        /// 要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0
        /// </summary>
        public int? index { get; set; }
        /// <summary>
        /// 草稿
        /// 若新增的是多图文素材，则此处应还有几段articles结构
        /// </summary>
        public List<NewsModel> articles { get; set; }
    }
}
