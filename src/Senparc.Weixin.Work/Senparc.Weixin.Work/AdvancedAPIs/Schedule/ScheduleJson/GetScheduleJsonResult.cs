/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetScheduleJsonResult.cs
    文件功能描述：获取日程接口返回参数
    
    
    创建标识：lishewen - 20191226
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Schedule.ScheduleJson
{
    public enum Response_Status
    {
        未处理 = 0,
        待定 = 1,
        全部接受 = 2,
        仅接受一次 = 3,
        拒绝 = 4
    }
    /// <summary>
    /// 注意，被取消的日程也可以拉取详情，调用者需要检查status
    /// </summary>
    public class GetScheduleJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 日程列表
        /// </summary>
        public List<Schedule_Item> schedule_list { get; set; }
    }

    public class Schedule_Item
    {
        /// <summary>
        /// 日程ID
        /// </summary>
        public string schedule_id { get; set; }
        /// <summary>
        /// 组织者
        /// </summary>
        public string organizer { get; set; }
        /// <summary>
        /// 日程参与者列表。最多支持2000人
        /// </summary>
        public List<AttendeeResult> attendees { get; set; }
        /// <summary>
        /// 日程标题。0 ~ 128 字符。不填会默认显示为“新建事件”
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 日程描述。0 ~ 512 字符
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 提醒相关信息
        /// </summary>
        public Reminders reminders { get; set; }
        /// <summary>
        /// 日程地址。0 ~ 128 字符
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// 日程开始时间，Unix时间戳
        /// </summary>
        public int start_time { get; set; }
        /// <summary>
        /// 日程结束时间，Unix时间戳
        /// </summary>
        public int end_time { get; set; }
        /// <summary>
        /// 日程状态。0-正常；1-已取消
        /// </summary>
        public int status { get; set; }
    }

    public class AttendeeResult : Attendee
    {
        public Response_Status response_status { get; set; }
    }

}
