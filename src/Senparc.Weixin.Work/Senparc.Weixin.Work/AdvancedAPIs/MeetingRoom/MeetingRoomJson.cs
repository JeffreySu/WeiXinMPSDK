/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingRoomJson.cs
    文件功能描述：企业微信会议室管理与预定强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 补齐会议室管理与预定强类型模型；补齐会议 ID 查询请求及预定排期字段

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MeetingRoom
{
    /// <summary>会议室经纬度坐标。</summary>
    public class MeetingRoomCoordinate
    {
        /// <summary>纬度字符串。</summary>
        public string latitude { get; set; }

        /// <summary>经度字符串。</summary>
        public string longitude { get; set; }
    }

    /// <summary>会议室可见及可预定范围。</summary>
    public class MeetingRoomRange
    {
        /// <summary>成员 UserId 列表，最多 1000 个。</summary>
        public IList<string> user_list { get; set; }

        /// <summary>部门 ID 列表，最多 1000 个。</summary>
        public IList<long> department_list { get; set; }
    }

    /// <summary>添加会议室请求。</summary>
    public class AddMeetingRoomRequest
    {
        /// <summary>会议室名称，最长 30 个字符。</summary>
        public string name { get; set; }

        /// <summary>会议室容量。</summary>
        public int capacity { get; set; }

        /// <summary>所在城市；使用位置时须与楼宇、楼层按官方层级规则填写。</summary>
        public string city { get; set; }

        /// <summary>所在楼宇。</summary>
        public string building { get; set; }

        /// <summary>所在楼层。</summary>
        public string floor { get; set; }

        /// <summary>设备 ID 列表：1 电视、2 电话、3 投影仪、4 白板、5 视频设备。</summary>
        public IList<int> equipment { get; set; }

        /// <summary>会议室坐标。</summary>
        public MeetingRoomCoordinate coordinate { get; set; }

        /// <summary>会议室使用范围。</summary>
        public MeetingRoomRange range { get; set; }
    }

    /// <summary>添加会议室结果。</summary>
    public class AddMeetingRoomResult : WorkJsonResult
    {
        /// <summary>新会议室 ID。</summary>
        public long meetingroom_id { get; set; }
    }

    /// <summary>查询会议室列表请求。</summary>
    public class GetMeetingRoomListRequest
    {
        /// <summary>按城市筛选。</summary>
        public string city { get; set; }

        /// <summary>按楼宇筛选，填写时须同时填写城市。</summary>
        public string building { get; set; }

        /// <summary>按楼层筛选，填写时须同时填写城市和楼宇。</summary>
        public string floor { get; set; }

        /// <summary>按设备 ID 列表筛选。</summary>
        public IList<int> equipment { get; set; }
    }

    /// <summary>会议室详情。</summary>
    public class MeetingRoomInfo
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>会议室名称。</summary>
        public string name { get; set; }

        /// <summary>会议室容量。</summary>
        public int capacity { get; set; }

        /// <summary>所在城市。</summary>
        public string city { get; set; }

        /// <summary>所在楼宇。</summary>
        public string building { get; set; }

        /// <summary>所在楼层。</summary>
        public string floor { get; set; }

        /// <summary>设备 ID 列表。</summary>
        public IList<int> equipment { get; set; }

        /// <summary>会议室坐标。</summary>
        public MeetingRoomCoordinate coordinate { get; set; }

        /// <summary>是否需要审批，0 表示不需要，1 表示需要。</summary>
        public int need_approval { get; set; }

        /// <summary>会议室使用范围。</summary>
        public MeetingRoomRange range { get; set; }
    }

    /// <summary>查询会议室列表结果。</summary>
    public class GetMeetingRoomListResult : WorkJsonResult
    {
        /// <summary>会议室列表。</summary>
        public IList<MeetingRoomInfo> meetingroom_list { get; set; }
    }

    /// <summary>更新会议室请求。</summary>
    public class UpdateMeetingRoomRequest
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>会议室名称。</summary>
        public string name { get; set; }

        /// <summary>会议室容量；不填写时不更新。</summary>
        public int? capacity { get; set; }

        /// <summary>所在城市。</summary>
        public string city { get; set; }

        /// <summary>所在楼宇。</summary>
        public string building { get; set; }

        /// <summary>所在楼层。</summary>
        public string floor { get; set; }

        /// <summary>设备 ID 列表。</summary>
        public IList<int> equipment { get; set; }

        /// <summary>会议室坐标。</summary>
        public MeetingRoomCoordinate coordinate { get; set; }

        /// <summary>替换后的会议室使用范围。</summary>
        public MeetingRoomRange range { get; set; }
    }

    /// <summary>删除会议室请求。</summary>
    public class DeleteMeetingRoomRequest
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }
    }

    /// <summary>查询会议室预定信息请求。</summary>
    public class GetMeetingRoomBookingInfoRequest
    {
        /// <summary>可选的会议室 ID。</summary>
        public long? meetingroom_id { get; set; }

        /// <summary>查询起始 Unix 时间戳，默认为当前时间。</summary>
        public long? start_time { get; set; }

        /// <summary>查询结束 Unix 时间戳，默认为次日零时；官方不支持跨天查询。</summary>
        public long? end_time { get; set; }

        /// <summary>按城市筛选。</summary>
        public string city { get; set; }

        /// <summary>按楼宇筛选，填写时须同时填写城市。</summary>
        public string building { get; set; }

        /// <summary>按楼层筛选，填写时须同时填写城市和楼宇。</summary>
        public string floor { get; set; }
    }

    /// <summary>单条会议室预定日程。</summary>
    public class MeetingRoomBookingSchedule
    {
        /// <summary>会议室预定 ID。</summary>
        public string booking_id { get; set; }

        /// <summary>周期性预定的主预定 ID。</summary>
        public string master_booking_id { get; set; }

        /// <summary>关联日程 ID。</summary>
        public string schedule_id { get; set; }

        /// <summary>关联会议 ID；通过会议预定或按会议 ID 查询时返回。</summary>
        public string meeting_id { get; set; }

        /// <summary>预定开始 Unix 时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>预定结束 Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>预定人 UserId。</summary>
        public string booker { get; set; }

        /// <summary>预定状态：0 已预定、1 已取消、2 申请中、3 审批中。</summary>
        public int status { get; set; }
    }

    /// <summary>一个会议室及其预定日程。</summary>
    public class MeetingRoomBookingInfo
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>查询时间段内的预定日程。</summary>
        public IList<MeetingRoomBookingSchedule> schedule { get; set; }
    }

    /// <summary>查询会议室预定信息结果。</summary>
    public class GetMeetingRoomBookingInfoResult : WorkJsonResult
    {
        /// <summary>会议室预定信息列表。</summary>
        public IList<MeetingRoomBookingInfo> booking_list { get; set; }
    }

    /// <summary>根据会议 ID 查询会议室预定信息请求。</summary>
    public class GetMeetingRoomBookingInfoByMeetingIdRequest
    {
        /// <summary>需要查询的会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>同一应用创建的会议 ID。</summary>
        public string meeting_id { get; set; }
    }

    /// <summary>根据会议 ID 查询会议室预定信息结果。</summary>
    public class GetMeetingRoomBookingInfoByMeetingIdResult : WorkJsonResult
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>与指定会议关联的会议室预定排期。</summary>
        public MeetingRoomBookingSchedule schedule { get; set; }
    }

    /// <summary>直接预定会议室请求。</summary>
    public class BookMeetingRoomRequest
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>会议主题。</summary>
        public string subject { get; set; }

        /// <summary>预定开始 Unix 时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>预定结束 Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>预定人 UserId。</summary>
        public string booker { get; set; }

        /// <summary>参与人 UserId 列表。</summary>
        public IList<string> attendees { get; set; }
    }

    /// <summary>直接预定会议室结果。</summary>
    public class BookMeetingRoomResult : WorkJsonResult
    {
        /// <summary>会议室预定 ID。</summary>
        public string booking_id { get; set; }

        /// <summary>关联日程 ID。</summary>
        public string schedule_id { get; set; }
    }

    /// <summary>通过日程预定会议室请求。</summary>
    public class BookMeetingRoomByScheduleRequest
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>同一应用创建的日程 ID。</summary>
        public string schedule_id { get; set; }

        /// <summary>预定人 UserId。</summary>
        public string booker { get; set; }
    }

    /// <summary>通过会议预定会议室请求。</summary>
    public class BookMeetingRoomByMeetingRequest
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>同一应用创建的会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>预定人 UserId。</summary>
        public string booker { get; set; }
    }

    /// <summary>通过重复日程或会议预定会议室结果。</summary>
    public class BookMeetingRoomForRecurringResult : WorkJsonResult
    {
        /// <summary>会议室预定 ID。</summary>
        public string booking_id { get; set; }

        /// <summary>冲突日期列表，每项为当天零时的 Unix 时间戳。</summary>
        public IList<long> conflict_date { get; set; }
    }

    /// <summary>取消会议室预定请求。</summary>
    public class CancelMeetingRoomBookingRequest
    {
        /// <summary>会议室预定 ID。</summary>
        public string booking_id { get; set; }

        /// <summary>是否保留日程：0 同步删除，1 保留；仅对非重复日程有效。</summary>
        public int? keep_schedule { get; set; }

        /// <summary>重复预定中需要取消的日期零点 Unix 时间戳；不填写表示取消全部重复预定。</summary>
        public long? cancel_date { get; set; }
    }

    /// <summary>根据预定 ID 查询预定详情请求。</summary>
    public class GetMeetingRoomBookingDetailRequest
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>会议室预定 ID。</summary>
        public string booking_id { get; set; }
    }

    /// <summary>根据预定 ID 查询预定详情结果。</summary>
    public class GetMeetingRoomBookingDetailResult : WorkJsonResult
    {
        /// <summary>会议室 ID。</summary>
        public long meetingroom_id { get; set; }

        /// <summary>会议室预定日程。</summary>
        public MeetingRoomBookingSchedule schedule { get; set; }
    }
}
