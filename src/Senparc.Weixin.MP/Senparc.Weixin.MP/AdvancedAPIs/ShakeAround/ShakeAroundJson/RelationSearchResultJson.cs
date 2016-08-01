/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RelationSearchResultJson.cs
    文件功能描述：查询设备与页面的关联关系返回结果
    
    
    创建标识：Senparc - 20160216
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 查询设备与页面的关联关系返回结果
    /// </summary>
    public class RelationSearchResultJson : WxJsonResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public RelationSearchDate data { get; set; }
    }

    public class RelationSearchDate
    {
        public List<RelationItem> relations { get; set; }

        /// <summary>
        /// 设备或页面的关联关系总数
        /// </summary>
        public int total_count { get; set; }
    }

    /// <summary>
    /// 设备与页面的关联关系
    /// </summary>
    public class RelationItem : DeviceApply_Data_Device_Identifiers
    {
        /// <summary>
        /// 摇周边页面唯一ID
        /// </summary>
        public long page_id { get; set; }
    }
}