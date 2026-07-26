/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingStatisticsVipJson.cs
    文件功能描述：企业微信会议发起统计与高级账号请求及结果模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议发起统计和高级账号批量管理强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 获取企业微信会议发起统计请求。
    /// </summary>
    public class GetMeetingStartStatisticsRequest
    {
        /// <summary>获取或设置统计查询类型。</summary>
        public int type { get; set; }

        /// <summary>获取或设置查询起始 Unix 时间戳。</summary>
        public long begin_time { get; set; }

        /// <summary>获取或设置查询结束 Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 企业微信会议发起记录。
    /// </summary>
    public class MeetingStartStatisticsItem
    {
        /// <summary>获取或设置会议发起成员的 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置会议发起 Unix 时间戳。</summary>
        public long start_time { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议发起统计结果。
    /// </summary>
    public class GetMeetingStartStatisticsResult : WorkJsonResult
    {
        /// <summary>获取或设置会议发起记录列表。</summary>
        public IList<MeetingStartStatisticsItem> meeting_list { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 提交批量分配企业微信会议高级账号任务请求。
    /// </summary>
    public class SubmitMeetingVipBatchAddJobRequest
    {
        /// <summary>获取或设置需要分配高级账号的成员 UserId 列表。</summary>
        public IList<string> userid_list { get; set; }
    }

    /// <summary>
    /// 提交批量分配企业微信会议高级账号任务结果。
    /// </summary>
    public class SubmitMeetingVipBatchAddJobResult : WorkJsonResult
    {
        /// <summary>获取或设置异步任务 ID。</summary>
        public string jobid { get; set; }

        /// <summary>获取或设置无效成员 UserId 列表。</summary>
        public IList<string> invalid_userid_list { get; set; }
    }

    /// <summary>
    /// 查询批量分配企业微信会议高级账号任务结果请求。
    /// </summary>
    public class GetMeetingVipBatchAddJobResultRequest
    {
        /// <summary>获取或设置异步任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// 企业微信会议高级账号批量任务执行结果。
    /// </summary>
    public class MeetingVipBatchJobResult
    {
        /// <summary>获取或设置操作成功的成员 UserId 列表。</summary>
        public IList<string> succ_userid_list { get; set; }

        /// <summary>获取或设置操作失败的成员 UserId 列表。</summary>
        public IList<string> fail_userid_list { get; set; }
    }

    /// <summary>
    /// 查询批量分配企业微信会议高级账号任务结果。
    /// </summary>
    public class GetMeetingVipBatchAddJobResultResult : WorkJsonResult
    {
        /// <summary>获取或设置批量分配任务执行结果。</summary>
        public MeetingVipBatchJobResult job_result { get; set; }
    }

    /// <summary>
    /// 提交批量撤销企业微信会议高级账号任务请求。
    /// </summary>
    public class SubmitMeetingVipBatchDeleteJobRequest
    {
        /// <summary>获取或设置需要撤销高级账号的成员 UserId 列表。</summary>
        public IList<string> userid_list { get; set; }
    }

    /// <summary>
    /// 提交批量撤销企业微信会议高级账号任务结果。
    /// </summary>
    public class SubmitMeetingVipBatchDeleteJobResult : WorkJsonResult
    {
        /// <summary>获取或设置异步任务 ID。</summary>
        public string jobid { get; set; }

        /// <summary>获取或设置无效成员 UserId 列表。</summary>
        public IList<string> invalid_userid_list { get; set; }
    }

    /// <summary>
    /// 查询批量撤销企业微信会议高级账号任务结果请求。
    /// </summary>
    public class GetMeetingVipBatchDeleteJobResultRequest
    {
        /// <summary>获取或设置异步任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// 查询批量撤销企业微信会议高级账号任务结果。
    /// </summary>
    public class GetMeetingVipBatchDeleteJobResultResult : WorkJsonResult
    {
        /// <summary>获取或设置批量撤销任务执行结果。</summary>
        public MeetingVipBatchJobResult job_result { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议高级账号成员列表请求。
    /// </summary>
    public class GetMeetingVipListRequest
    {
        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议高级账号成员列表结果。
    /// </summary>
    public class GetMeetingVipListResult : WorkJsonResult
    {
        /// <summary>获取或设置已分配会议高级账号的成员 UserId 列表。</summary>
        public IList<string> userid_list { get; set; }

        /// <summary>获取或设置是否还有更多数据。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }
}
