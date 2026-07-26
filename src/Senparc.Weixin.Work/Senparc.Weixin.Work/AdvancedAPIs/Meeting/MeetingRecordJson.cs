/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingRecordJson.cs
    文件功能描述：企业微信会议录制请求与结果强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议录制列表、统计、共享和删除模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 获取会议录制列表请求。
    /// </summary>
    public class GetMeetingRecordListRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议号。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置会议主持人的企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置查询起始 Unix 时间戳。</summary>
        public long? start_time { get; set; }

        /// <summary>获取或设置查询结束 Unix 时间戳。</summary>
        public long? end_time { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 会议录制文件信息。
    /// </summary>
    public class MeetingRecordFile
    {
        /// <summary>获取或设置录制文件 ID。</summary>
        public string record_file_id { get; set; }

        /// <summary>获取或设置开始录制的 Unix 时间戳。</summary>
        public long record_start_time { get; set; }

        /// <summary>获取或设置结束录制的 Unix 时间戳。</summary>
        public long record_end_time { get; set; }

        /// <summary>获取或设置录制文件大小，单位为字节。</summary>
        public long record_size { get; set; }

        /// <summary>获取或设置录制文件共享状态。</summary>
        public int sharing_state { get; set; }

        /// <summary>获取或设置录制文件共享链接。</summary>
        public string sharing_url { get; set; }

        /// <summary>获取或设置是否仅同企业成员可查看。</summary>
        public bool? required_same_corp { get; set; }

        /// <summary>获取或设置是否仅参会成员可查看。</summary>
        public bool? required_attendee { get; set; }

        /// <summary>获取或设置共享访问密码。</summary>
        public string password { get; set; }

        /// <summary>获取或设置共享链接过期 Unix 时间戳。</summary>
        public long? sharing_expire { get; set; }

        /// <summary>获取或设置是否允许下载录制文件。</summary>
        public bool? allow_download { get; set; }
    }

    /// <summary>
    /// 单次会议录制信息。
    /// </summary>
    public class MeetingRecordInfo
    {
        /// <summary>获取或设置会议录制 ID。</summary>
        public string meeting_record_id { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议号。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置会议主持人的企业成员 UserId。</summary>
        public string host_user_id { get; set; }

        /// <summary>获取或设置会议主题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置会议开始 Unix 时间戳。</summary>
        public long meeting_start_time { get; set; }

        /// <summary>获取或设置录制状态。</summary>
        public int state { get; set; }

        /// <summary>获取或设置会议录制文件列表。</summary>
        public IList<MeetingRecordFile> record_files { get; set; }
    }

    /// <summary>
    /// 获取会议录制列表结果。
    /// </summary>
    public class GetMeetingRecordListResult : WorkJsonResult
    {
        /// <summary>获取或设置会议录制列表。</summary>
        public IList<MeetingRecordInfo> record_meetings { get; set; }

        /// <summary>获取或设置是否还有更多录制。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取会议录制统计请求。
    /// </summary>
    public class GetMeetingRecordStatisticsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议录制 ID。</summary>
        public string meeting_record_id { get; set; }

        /// <summary>获取或设置查询起始 Unix 时间戳。</summary>
        public long? start_time { get; set; }

        /// <summary>获取或设置查询结束 Unix 时间戳。</summary>
        public long? end_time { get; set; }
    }

    /// <summary>
    /// 会议录制单日统计。
    /// </summary>
    public class MeetingRecordStatisticsSummary
    {
        /// <summary>获取或设置统计日期，格式为 yyyy-MM-dd。</summary>
        public string date { get; set; }

        /// <summary>获取或设置观看次数。</summary>
        public int view_count { get; set; }

        /// <summary>获取或设置下载次数。</summary>
        public int download_count { get; set; }
    }

    /// <summary>
    /// 获取会议录制统计结果。
    /// </summary>
    public class GetMeetingRecordStatisticsResult : WorkJsonResult
    {
        /// <summary>获取或设置按日期汇总的录制统计列表。</summary>
        public IList<MeetingRecordStatisticsSummary> summaries { get; set; }
    }

    /// <summary>
    /// 会议录制共享配置。
    /// </summary>
    public class MeetingRecordSharingConfig
    {
        /// <summary>获取或设置是否开启录制共享。</summary>
        public bool enable_sharing { get; set; }

        /// <summary>获取或设置共享权限类型。</summary>
        public int? sharing_auth_type { get; set; }

        /// <summary>获取或设置是否启用共享访问密码。</summary>
        public bool? enable_password { get; set; }

        /// <summary>获取或设置共享访问密码。</summary>
        public string password { get; set; }

        /// <summary>获取或设置是否启用共享有效期。</summary>
        public bool? enable_sharing_expire { get; set; }

        /// <summary>获取或设置共享链接过期 Unix 时间戳。</summary>
        public long? sharing_expire { get; set; }

        /// <summary>获取或设置是否允许下载录制文件。</summary>
        public bool? allow_download { get; set; }
    }

    /// <summary>
    /// 更新会议录制共享配置请求。
    /// </summary>
    public class UpdateMeetingRecordSharingConfigRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议录制 ID。</summary>
        public string meeting_record_id { get; set; }

        /// <summary>获取或设置录制共享配置。</summary>
        public MeetingRecordSharingConfig sharing_config { get; set; }
    }

    /// <summary>
    /// 更新会议录制共享配置结果。
    /// </summary>
    public class UpdateMeetingRecordSharingConfigResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 删除会议录制请求。
    /// </summary>
    public class DeleteMeetingRecordRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议录制 ID。</summary>
        public string meeting_record_id { get; set; }
    }

    /// <summary>
    /// 删除会议录制结果。
    /// </summary>
    public class DeleteMeetingRecordResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 删除会议录制文件请求。
    /// </summary>
    public class DeleteMeetingRecordFileRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议录制文件 ID。</summary>
        public string record_file_id { get; set; }
    }

    /// <summary>
    /// 删除会议录制文件结果。
    /// </summary>
    public class DeleteMeetingRecordFileResult : WorkJsonResult
    {
    }
}
