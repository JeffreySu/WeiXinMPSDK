/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ReportGridJson.cs
    文件功能描述：企业微信政民沟通网格与事件分类管理强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐政民沟通网格与事件分类管理模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    /// <summary>
    /// 新增政民沟通网格请求。
    /// </summary>
    public class ReportGridAddRequest
    {
        /// <summary>
        /// 获取或设置网格名称。
        /// </summary>
        public string grid_name { get; set; }

        /// <summary>
        /// 获取或设置上级网格 ID；顶级网格可不填。
        /// </summary>
        public string grid_parent_id { get; set; }

        /// <summary>
        /// 获取或设置网格管理员 UserId 列表。
        /// </summary>
        public IList<string> grid_admin { get; set; }

        /// <summary>
        /// 获取或设置网格成员 UserId 列表。
        /// </summary>
        public IList<string> grid_member { get; set; }
    }

    /// <summary>
    /// 新增政民沟通网格结果。
    /// </summary>
    public class ReportGridAddResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置新增的网格 ID。
        /// </summary>
        public string grid_id { get; set; }

        /// <summary>
        /// 获取或设置无效的 UserId 列表。
        /// </summary>
        public IList<string> invalid_userids { get; set; }
    }

    /// <summary>
    /// 更新政民沟通网格请求。
    /// </summary>
    public class ReportGridUpdateRequest : ReportGridAddRequest
    {
        /// <summary>
        /// 获取或设置需要更新的网格 ID。
        /// </summary>
        public string grid_id { get; set; }
    }

    /// <summary>
    /// 更新政民沟通网格结果。
    /// </summary>
    public class ReportGridUpdateResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置无效的 UserId 列表。
        /// </summary>
        public IList<string> invalid_userids { get; set; }
    }

    /// <summary>
    /// 删除政民沟通网格请求。
    /// </summary>
    public class ReportGridDeleteRequest
    {
        /// <summary>
        /// 获取或设置需要删除的网格 ID。
        /// </summary>
        public string grid_id { get; set; }
    }

    /// <summary>
    /// 删除政民沟通网格结果。
    /// </summary>
    public class ReportGridDeleteResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 获取政民沟通网格列表请求。
    /// </summary>
    public class ReportGridListRequest
    {
        /// <summary>
        /// 获取或设置需要查询的上级网格 ID；不填时查询顶级网格。
        /// </summary>
        public string grid_id { get; set; }
    }

    /// <summary>
    /// 政民沟通网格详细信息。
    /// </summary>
    public class ReportGridInfo
    {
        /// <summary>
        /// 获取或设置网格 ID。
        /// </summary>
        public string grid_id { get; set; }

        /// <summary>
        /// 获取或设置网格名称。
        /// </summary>
        public string grid_name { get; set; }

        /// <summary>
        /// 获取或设置上级网格 ID。
        /// </summary>
        public string grid_parent_id { get; set; }

        /// <summary>
        /// 获取或设置网格管理员 UserId 列表。
        /// </summary>
        public IList<string> grid_admin { get; set; }

        /// <summary>
        /// 获取或设置网格成员 UserId 列表。
        /// </summary>
        public IList<string> grid_member { get; set; }
    }

    /// <summary>
    /// 获取政民沟通网格列表结果。
    /// </summary>
    public class ReportGridListResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置网格列表。
        /// </summary>
        public IList<ReportGridInfo> grid_list { get; set; }
    }

    /// <summary>
    /// 获取成员网格信息请求。
    /// </summary>
    public class ReportGridUserInfoRequest
    {
        /// <summary>
        /// 获取或设置成员 UserId。
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 成员所在的政民沟通网格摘要。
    /// </summary>
    public class ReportGridSummary
    {
        /// <summary>
        /// 获取或设置网格 ID。
        /// </summary>
        public string grid_id { get; set; }

        /// <summary>
        /// 获取或设置网格名称。
        /// </summary>
        public string grid_name { get; set; }
    }

    /// <summary>
    /// 获取成员网格信息结果。
    /// </summary>
    public class ReportGridUserInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置成员管理的网格列表。
        /// </summary>
        public IList<ReportGridSummary> manage_grids { get; set; }

        /// <summary>
        /// 获取或设置成员加入的网格列表。
        /// </summary>
        public IList<ReportGridSummary> joined_grids { get; set; }
    }

    /// <summary>
    /// 新增政民沟通事件分类请求。
    /// </summary>
    public class ReportGridCategoryAddRequest
    {
        /// <summary>
        /// 获取或设置分类名称。
        /// </summary>
        public string category_name { get; set; }

        /// <summary>
        /// 获取或设置分类层级。
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 获取或设置上级分类 ID。
        /// </summary>
        public string parent_category_id { get; set; }
    }

    /// <summary>
    /// 新增政民沟通事件分类结果。
    /// </summary>
    public class ReportGridCategoryAddResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置新增的分类 ID。
        /// </summary>
        public string category_id { get; set; }
    }

    /// <summary>
    /// 更新政民沟通事件分类请求。
    /// </summary>
    public class ReportGridCategoryUpdateRequest : ReportGridCategoryAddRequest
    {
        /// <summary>
        /// 获取或设置需要更新的分类 ID。
        /// </summary>
        public string category_id { get; set; }
    }

    /// <summary>
    /// 更新政民沟通事件分类结果。
    /// </summary>
    public class ReportGridCategoryUpdateResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 删除政民沟通事件分类请求。
    /// </summary>
    public class ReportGridCategoryDeleteRequest
    {
        /// <summary>
        /// 获取或设置需要删除的分类 ID。
        /// </summary>
        public string category_id { get; set; }
    }

    /// <summary>
    /// 删除政民沟通事件分类结果。
    /// </summary>
    public class ReportGridCategoryDeleteResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 获取政民沟通事件分类列表请求。
    /// </summary>
    public class ReportGridCategoryListRequest
    {
    }

    /// <summary>
    /// 政民沟通事件分类信息。
    /// </summary>
    public class ReportGridCategoryInfo
    {
        /// <summary>
        /// 获取或设置分类 ID；官方列表响应字段为 <c>cata_id</c>。
        /// </summary>
        public string cata_id { get; set; }

        /// <summary>
        /// 获取或设置分类名称；官方列表响应字段为 <c>cata_name</c>。
        /// </summary>
        public string cata_name { get; set; }

        /// <summary>
        /// 获取或设置分类层级。
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 获取或设置上级分类 ID；官方列表响应字段为 <c>parent_cata_id</c>。
        /// </summary>
        public string parent_cata_id { get; set; }
    }

    /// <summary>
    /// 获取政民沟通事件分类列表结果。
    /// </summary>
    public class ReportGridCategoryListResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置事件分类列表；官方响应字段为 <c>cata_list</c>。
        /// </summary>
        public IList<ReportGridCategoryInfo> cata_list { get; set; }
    }
}
