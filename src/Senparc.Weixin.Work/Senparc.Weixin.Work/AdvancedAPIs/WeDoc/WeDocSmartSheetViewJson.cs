/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocSmartSheetViewJson.cs
    文件功能描述：企业微信智能表格视图强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能表格视图增删改查强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>
    /// 获取智能表格视图请求。
    /// </summary>
    public class WeDocSmartSheetGetViewsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置需要查询的视图 ID 列表；留空时按分页参数获取视图。
        /// </summary>
        public IList<string> view_ids { get; set; }

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
    /// 智能表格视图中的排序字段。
    /// </summary>
    public class WeDocSmartSheetSortItem
    {
        /// <summary>
        /// 获取或设置字段 ID。
        /// </summary>
        public string field_id { get; set; }

        /// <summary>
        /// 获取或设置是否按降序排列。
        /// </summary>
        public bool? desc { get; set; }
    }

    /// <summary>
    /// 智能表格视图排序配置。
    /// </summary>
    public class WeDocSmartSheetSortSpec
    {
        /// <summary>
        /// 获取或设置排序字段列表。
        /// </summary>
        public IList<WeDocSmartSheetSortItem> sort_infos { get; set; }
    }

    /// <summary>
    /// 智能表格视图中的分组字段。
    /// </summary>
    public class WeDocSmartSheetGroupItem
    {
        /// <summary>
        /// 获取或设置字段 ID。
        /// </summary>
        public string field_id { get; set; }

        /// <summary>
        /// 获取或设置是否按降序排列。
        /// </summary>
        public bool? desc { get; set; }
    }

    /// <summary>
    /// 智能表格视图分组配置。
    /// </summary>
    public class WeDocSmartSheetGroupSpec
    {
        /// <summary>
        /// 获取或设置分组字段列表。
        /// </summary>
        public IList<WeDocSmartSheetGroupItem> groups { get; set; }
    }

    /// <summary>
    /// 智能表格筛选条件的字符串值。
    /// </summary>
    public class WeDocSmartSheetStringFilterValue
    {
        /// <summary>
        /// 获取或设置字符串值列表。
        /// </summary>
        public IList<string> value { get; set; }
    }

    /// <summary>
    /// 智能表格筛选条件的数字值。
    /// </summary>
    public class WeDocSmartSheetNumberFilterValue
    {
        /// <summary>
        /// 获取或设置数字值。
        /// </summary>
        public decimal value { get; set; }
    }

    /// <summary>
    /// 智能表格筛选条件的布尔值。
    /// </summary>
    public class WeDocSmartSheetBoolFilterValue
    {
        /// <summary>
        /// 获取或设置布尔值。
        /// </summary>
        public bool value { get; set; }
    }

    /// <summary>
    /// 智能表格筛选条件的成员值。
    /// </summary>
    public class WeDocSmartSheetUserFilterValue
    {
        /// <summary>
        /// 获取或设置成员 UserID 列表。
        /// </summary>
        public IList<string> value { get; set; }
    }

    /// <summary>
    /// 智能表格日期时间筛选值内容。
    /// </summary>
    public class WeDocSmartSheetDateTimeFilterData
    {
        /// <summary>
        /// 获取或设置日期值类型。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 获取或设置日期时间值列表。
        /// </summary>
        public IList<string> value { get; set; }
    }

    /// <summary>
    /// 智能表格筛选条件的日期时间值。
    /// </summary>
    public class WeDocSmartSheetDateTimeFilterValue
    {
        /// <summary>
        /// 获取或设置日期时间筛选值内容。
        /// </summary>
        public WeDocSmartSheetDateTimeFilterData value { get; set; }
    }

    /// <summary>
    /// 智能表格视图或记录查询的筛选条件。
    /// </summary>
    public class WeDocSmartSheetFilterCondition
    {
        /// <summary>
        /// 获取或设置字段 ID。
        /// </summary>
        public string field_id { get; set; }

        /// <summary>
        /// 获取或设置字段类型；部分写入场景可省略。
        /// </summary>
        public string field_type { get; set; }

        /// <summary>
        /// 获取或设置条件操作符，例如 <c>OPERATOR_CONTAINS</c>。
        /// </summary>
        public string @operator { get; set; }

        /// <summary>
        /// 获取或设置字符串条件值。
        /// </summary>
        public WeDocSmartSheetStringFilterValue string_value { get; set; }

        /// <summary>
        /// 获取或设置数字条件值。
        /// </summary>
        public WeDocSmartSheetNumberFilterValue number_value { get; set; }

        /// <summary>
        /// 获取或设置布尔条件值。
        /// </summary>
        public WeDocSmartSheetBoolFilterValue bool_value { get; set; }

        /// <summary>
        /// 获取或设置成员条件值。
        /// </summary>
        public WeDocSmartSheetUserFilterValue user_value { get; set; }

        /// <summary>
        /// 获取或设置日期时间条件值。
        /// </summary>
        public WeDocSmartSheetDateTimeFilterValue date_time_value { get; set; }
    }

    /// <summary>
    /// 智能表格筛选配置。
    /// </summary>
    public class WeDocSmartSheetFilterSpec
    {
        /// <summary>
        /// 获取或设置条件连接方式，例如 <c>CONJUNCTION_AND</c>。
        /// </summary>
        public string conjunction { get; set; }

        /// <summary>
        /// 获取或设置筛选条件列表。
        /// </summary>
        public IList<WeDocSmartSheetFilterCondition> conditions { get; set; }
    }

    /// <summary>
    /// 智能表格视图的条件填色项。
    /// </summary>
    public class WeDocSmartSheetColorCondition
    {
        /// <summary>
        /// 获取或设置条件填色 ID；新增条件时可留空。
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 获取或设置条件填色类型。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 获取或设置颜色标识。
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 获取或设置触发填色的筛选条件。
        /// </summary>
        public WeDocSmartSheetFilterCondition condition { get; set; }
    }

    /// <summary>
    /// 智能表格视图条件填色配置。
    /// </summary>
    public class WeDocSmartSheetColorConfig
    {
        /// <summary>
        /// 获取或设置条件填色列表。
        /// </summary>
        public IList<WeDocSmartSheetColorCondition> conditions { get; set; }
    }

    /// <summary>
    /// 智能表格视图属性。
    /// </summary>
    public class WeDocSmartSheetViewProperty
    {
        /// <summary>
        /// 获取或设置数据变化后是否自动重新排序。
        /// </summary>
        public bool? auto_sort { get; set; }

        /// <summary>
        /// 获取或设置排序配置。
        /// </summary>
        public WeDocSmartSheetSortSpec sort_spec { get; set; }

        /// <summary>
        /// 获取或设置筛选配置。
        /// </summary>
        public WeDocSmartSheetFilterSpec filter_spec { get; set; }

        /// <summary>
        /// 获取或设置分组配置。
        /// </summary>
        public WeDocSmartSheetGroupSpec group_spec { get; set; }

        /// <summary>
        /// 获取或设置是否启用字段统计。
        /// </summary>
        public bool? is_field_stat_enabled { get; set; }

        /// <summary>
        /// 获取或设置字段可见性；键为字段 ID，值表示是否可见。
        /// </summary>
        public IDictionary<string, bool> field_visibility { get; set; }

        /// <summary>
        /// 获取或设置冻结字段数量。
        /// </summary>
        public int? frozen_field_count { get; set; }

        /// <summary>
        /// 获取或设置条件填色配置。
        /// </summary>
        public WeDocSmartSheetColorConfig color_config { get; set; }
    }

    /// <summary>
    /// 智能表格视图信息。
    /// </summary>
    public class WeDocSmartSheetView
    {
        /// <summary>
        /// 获取或设置视图 ID。
        /// </summary>
        public string view_id { get; set; }

        /// <summary>
        /// 获取或设置视图标题。
        /// </summary>
        public string view_title { get; set; }

        /// <summary>
        /// 获取或设置视图类型，例如 <c>VIEW_TYPE_GRID</c>。
        /// </summary>
        public string view_type { get; set; }

        /// <summary>
        /// 获取或设置视图属性。
        /// </summary>
        public WeDocSmartSheetViewProperty property { get; set; }
    }

    /// <summary>
    /// 获取智能表格视图结果。
    /// </summary>
    public class WeDocSmartSheetGetViewsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置视图列表。
        /// </summary>
        public IList<WeDocSmartSheetView> views { get; set; }

        /// <summary>
        /// 获取或设置符合条件的视图总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 获取或设置下一页起始偏移量。
        /// </summary>
        public int? next { get; set; }

        /// <summary>
        /// 获取或设置是否还有更多视图。
        /// </summary>
        public bool has_more { get; set; }
    }

    /// <summary>
    /// 甘特视图或日历视图的日期字段配置。
    /// </summary>
    public class WeDocSmartSheetDateRangeViewProperty
    {
        /// <summary>
        /// 获取或设置开始日期字段 ID。
        /// </summary>
        public string start_date_field_id { get; set; }

        /// <summary>
        /// 获取或设置结束日期字段 ID。
        /// </summary>
        public string end_date_field_id { get; set; }
    }

    /// <summary>
    /// 新增智能表格视图请求。
    /// </summary>
    public class WeDocSmartSheetAddViewRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置视图标题。
        /// </summary>
        public string view_title { get; set; }

        /// <summary>
        /// 获取或设置视图类型。
        /// </summary>
        public string view_type { get; set; }

        /// <summary>
        /// 获取或设置甘特视图日期字段配置。
        /// </summary>
        public WeDocSmartSheetDateRangeViewProperty property_gantt { get; set; }

        /// <summary>
        /// 获取或设置日历视图日期字段配置。
        /// </summary>
        public WeDocSmartSheetDateRangeViewProperty property_calendar { get; set; }
    }

    /// <summary>
    /// 新增智能表格视图结果。
    /// </summary>
    public class WeDocSmartSheetAddViewResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置已新增的视图。
        /// </summary>
        public WeDocSmartSheetView view { get; set; }
    }

    /// <summary>
    /// 更新智能表格视图请求。
    /// </summary>
    public class WeDocSmartSheetUpdateViewRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置视图 ID。
        /// </summary>
        public string view_id { get; set; }

        /// <summary>
        /// 获取或设置新的视图标题。
        /// </summary>
        public string view_title { get; set; }

        /// <summary>
        /// 获取或设置新的视图属性。
        /// </summary>
        public WeDocSmartSheetViewProperty property { get; set; }
    }

    /// <summary>
    /// 批量删除智能表格视图请求。
    /// </summary>
    public class WeDocSmartSheetDeleteViewsRequest : WeDocSmartSheetSheetRequest
    {
        /// <summary>
        /// 获取或设置待删除的视图 ID 列表。
        /// </summary>
        public IList<string> view_ids { get; set; }
    }
}
