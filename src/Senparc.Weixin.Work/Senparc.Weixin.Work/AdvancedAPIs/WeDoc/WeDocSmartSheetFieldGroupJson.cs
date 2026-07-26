/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocSmartSheetFieldGroupJson.cs
    文件功能描述：企业微信智能表格字段分组强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能表格字段分组增删改查强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>
    /// 字段分组中的字段引用。
    /// </summary>
    public class WeDocSmartSheetFieldGroupChild
    {
        /// <summary>
        /// 获取或设置字段 ID。
        /// </summary>
        public string field_id { get; set; }
    }

    /// <summary>
    /// 智能表格字段分组。
    /// </summary>
    public class WeDocSmartSheetFieldGroup
    {
        /// <summary>
        /// 获取或设置字段分组 ID。
        /// </summary>
        public string field_group_id { get; set; }

        /// <summary>
        /// 获取或设置字段分组名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 获取或设置分组内的字段列表。
        /// </summary>
        public IList<WeDocSmartSheetFieldGroupChild> children { get; set; }
    }

    /// <summary>
    /// 获取智能表格字段分组请求。
    /// </summary>
    public class WeDocSmartSheetGetFieldGroupsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置分页起始偏移量。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 获取或设置单页返回数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 获取智能表格字段分组结果。
    /// </summary>
    public class WeDocSmartSheetGetFieldGroupsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置字段分组列表。
        /// </summary>
        public IList<WeDocSmartSheetFieldGroup> field_groups { get; set; }

        /// <summary>
        /// 获取或设置字段分组总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 获取或设置下一页的起始偏移量。
        /// </summary>
        public int? next { get; set; }
    }

    /// <summary>
    /// 新增智能表格字段分组请求。
    /// </summary>
    public class WeDocSmartSheetAddFieldGroupRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置字段分组名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 获取或设置分组内的字段列表。
        /// </summary>
        public IList<WeDocSmartSheetFieldGroupChild> children { get; set; }
    }

    /// <summary>
    /// 新增智能表格字段分组结果。
    /// </summary>
    public class WeDocSmartSheetAddFieldGroupResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置新增后的字段分组。
        /// </summary>
        public WeDocSmartSheetFieldGroup field_group { get; set; }
    }

    /// <summary>
    /// 更新智能表格字段分组请求。
    /// </summary>
    public class WeDocSmartSheetUpdateFieldGroupRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置待更新的字段分组 ID。
        /// </summary>
        public string field_group_id { get; set; }

        /// <summary>
        /// 获取或设置新的字段分组名称；不修改时可不传。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 获取或设置新的分组字段列表；不修改时可不传。
        /// </summary>
        public IList<WeDocSmartSheetFieldGroupChild> children { get; set; }
    }

    /// <summary>
    /// 更新智能表格字段分组结果。
    /// </summary>
    public class WeDocSmartSheetUpdateFieldGroupResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置更新后的字段分组。
        /// </summary>
        public WeDocSmartSheetFieldGroup field_group { get; set; }
    }

    /// <summary>
    /// 批量删除智能表格字段分组请求。
    /// </summary>
    public class WeDocSmartSheetDeleteFieldGroupsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置待删除的字段分组 ID 列表。
        /// </summary>
        public IList<string> field_group_ids { get; set; }
    }
}
