/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：PatrolReportJson.cs
    文件功能描述：企业微信政民沟通巡查上报强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐政民沟通巡查上报查询和统计模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    /// <summary>
    /// 政民沟通巡查网格信息。
    /// </summary>
    public class PatrolReportGrid
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
        /// 获取或设置网格管理员 UserId 列表。
        /// </summary>
        public IList<string> grid_admin { get; set; }
    }

    /// <summary>
    /// 获取政民沟通巡查网格结果。
    /// </summary>
    public class PatrolReportGridInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置网格列表。
        /// </summary>
        public IList<PatrolReportGrid> grid_list { get; set; }
    }

    /// <summary>
    /// 获取企业巡查事件数据请求。
    /// </summary>
    public class PatrolReportCorpStatusRequest
    {
        /// <summary>
        /// 获取或设置网格 ID。
        /// </summary>
        public string grid_id { get; set; }
    }

    /// <summary>
    /// 企业巡查事件数据结果。
    /// </summary>
    public class PatrolReportCorpStatusResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置办理中数量。
        /// </summary>
        public long processing { get; set; }

        /// <summary>
        /// 获取或设置今日上报数量。
        /// </summary>
        public long added_today { get; set; }

        /// <summary>
        /// 获取或设置今日办结数量。
        /// </summary>
        public long solved_today { get; set; }

        /// <summary>
        /// 获取或设置累计上报数量。
        /// </summary>
        public long total_case { get; set; }

        /// <summary>
        /// 获取或设置待分配数量。
        /// </summary>
        public long to_be_assigned { get; set; }

        /// <summary>
        /// 获取或设置累计办结数量。
        /// </summary>
        public long total_solved { get; set; }
    }

    /// <summary>
    /// 获取成员巡查事件数据请求。
    /// </summary>
    public class PatrolReportUserStatusRequest
    {
        /// <summary>
        /// 获取或设置成员 UserId。
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 成员巡查事件数据结果。
    /// </summary>
    public class PatrolReportUserStatusResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置办理中数量。
        /// </summary>
        public long processing { get; set; }

        /// <summary>
        /// 获取或设置今日上报数量。
        /// </summary>
        public long added_today { get; set; }

        /// <summary>
        /// 获取或设置今日办结数量。
        /// </summary>
        public long solved_today { get; set; }
    }

    /// <summary>
    /// 获取巡查事件分类统计请求。
    /// </summary>
    public class PatrolReportCategoryStatisticsRequest
    {
        /// <summary>
        /// 获取或设置分类 ID。
        /// </summary>
        public string category_id { get; set; }
    }

    /// <summary>
    /// 巡查事件分类统计项。
    /// </summary>
    public class PatrolReportCategoryStatisticsItem
    {
        /// <summary>
        /// 获取或设置分类 ID。
        /// </summary>
        public string category_id { get; set; }

        /// <summary>
        /// 获取或设置分类名称。
        /// </summary>
        public string category_name { get; set; }

        /// <summary>
        /// 获取或设置分类层级。
        /// </summary>
        public int category_level { get; set; }

        /// <summary>
        /// 获取或设置分类类型。
        /// </summary>
        public int category_type { get; set; }

        /// <summary>
        /// 获取或设置累计上报数量。
        /// </summary>
        public long total_case { get; set; }

        /// <summary>
        /// 获取或设置累计办结数量。
        /// </summary>
        public long total_solved { get; set; }
    }

    /// <summary>
    /// 巡查事件分类统计结果。
    /// </summary>
    public class PatrolReportCategoryStatisticsResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置分类统计列表。
        /// </summary>
        public IList<PatrolReportCategoryStatisticsItem> dashboard_list { get; set; }
    }

    /// <summary>
    /// 获取巡查事件工单列表请求。
    /// </summary>
    public class PatrolReportOrderListRequest
    {
        /// <summary>
        /// 获取或设置只返回此 Unix 时间戳后创建的工单。
        /// </summary>
        public long? begin_create_time { get; set; }

        /// <summary>
        /// 获取或设置只返回此 Unix 时间戳后修改的工单。
        /// </summary>
        public long? begin_modify_time { get; set; }

        /// <summary>
        /// 获取或设置分页游标。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 获取或设置单页返回数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 巡查事件工单位置。
    /// </summary>
    public class PatrolReportLocation
    {
        /// <summary>
        /// 获取或设置位置名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 获取或设置详细地址。
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 获取或设置经度。
        /// </summary>
        public decimal longitude { get; set; }

        /// <summary>
        /// 获取或设置纬度。
        /// </summary>
        public decimal latitude { get; set; }
    }

    /// <summary>
    /// 巡查事件工单处理记录。
    /// </summary>
    public class PatrolReportProcessRecord
    {
        /// <summary>
        /// 获取或设置流程类型。
        /// </summary>
        public int process_type { get; set; }

        /// <summary>
        /// 获取或设置办结人 UserId。
        /// </summary>
        public string solve_userid { get; set; }

        /// <summary>
        /// 获取或设置流程描述。
        /// </summary>
        public string process_desc { get; set; }

        /// <summary>
        /// 获取或设置流程状态。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 获取或设置办结时间的 Unix 时间戳。
        /// </summary>
        public long? solved_time { get; set; }

        /// <summary>
        /// 获取或设置处理图片 URL 列表。
        /// </summary>
        public IList<string> image_urls { get; set; }

        /// <summary>
        /// 获取或设置处理视频 MediaId 列表。
        /// </summary>
        public IList<string> video_media_ids { get; set; }
    }

    /// <summary>
    /// 巡查事件工单。
    /// </summary>
    public class PatrolReportOrder
    {
        /// <summary>
        /// 获取或设置工单 ID。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 获取或设置事件描述。
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 获取或设置紧急类型。
        /// </summary>
        public int urge_type { get; set; }

        /// <summary>
        /// 获取或设置事件分类名称。
        /// </summary>
        public string case_name { get; set; }

        /// <summary>
        /// 获取或设置网格名称。
        /// </summary>
        public string grid_name { get; set; }

        /// <summary>
        /// 获取或设置网格 ID。
        /// </summary>
        public string grid_id { get; set; }

        /// <summary>
        /// 获取或设置工单图片 URL 列表。
        /// </summary>
        public IList<string> image_urls { get; set; }

        /// <summary>
        /// 获取或设置工单视频 MediaId 列表。
        /// </summary>
        public IList<string> video_media_ids { get; set; }

        /// <summary>
        /// 获取或设置工单创建时间的 Unix 时间戳。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 获取或设置事件位置。
        /// </summary>
        public PatrolReportLocation location { get; set; }

        /// <summary>
        /// 获取或设置处理人 UserId 列表。
        /// </summary>
        public IList<string> processor_userids { get; set; }

        /// <summary>
        /// 获取或设置处理记录列表。
        /// </summary>
        public IList<PatrolReportProcessRecord> process_list { get; set; }
    }

    /// <summary>
    /// 获取巡查事件工单列表结果。
    /// </summary>
    public class PatrolReportOrderListResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置工单列表。
        /// </summary>
        public IList<PatrolReportOrder> order_list { get; set; }

        /// <summary>
        /// 获取或设置下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取巡查事件工单详情请求。
    /// </summary>
    public class PatrolReportOrderInfoRequest
    {
        /// <summary>
        /// 获取或设置工单 ID。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 获取巡查事件工单详情结果。
    /// </summary>
    public class PatrolReportOrderInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置工单详情。
        /// </summary>
        public PatrolReportOrder order_info { get; set; }
    }
}
