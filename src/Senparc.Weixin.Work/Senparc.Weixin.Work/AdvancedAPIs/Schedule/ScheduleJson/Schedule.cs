/*----------------------------------------------------------------
    Copyright (C) 2020 Senparc
    
    文件名：Schedule.cs
    文件功能描述：日程信息
    
    
    创建标识：lishewen - 20191226
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Schedule.ScheduleJson
{
    public enum Remind_Before_Event_Secs
    {
        事件开始时 = 0,
        五分钟 = 300,
        十五分钟 = 900,
        一小时 = 3600,
        一天 = 86400
    }
    public enum Repeat_Type
    {
        每日 = 0,
        每周 = 1,
        每月 = 2,
        每年 = 5,
        工作日 = 7
    }
    /// <summary>
    /// 日程信息
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// 组织者
        /// </summary>
        public string organizer { get; set; }
        /// <summary>
        /// 日程开始时间，Unix时间戳
        /// </summary>
        public int start_time { get; set; }
        /// <summary>
        /// 日程结束时间，Unix时间戳
        /// </summary>
        public int end_time { get; set; }
        /// <summary>
        /// 日程参与者列表。最多支持2000人
        /// </summary>
        public IEnumerable<Attendee> attendees { get; set; }
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
    }

    public class Reminders
    {
        /// <summary>
        /// 是否需要提醒。0-否；1-是
        /// </summary>
        public int is_remind { get; set; }
        /// <summary>
        /// 日程开始（start_time）前多少秒提醒，当is_remind为1时有效。例如： 300表示日程开始前5分钟提醒。
        /// </summary>
        public Remind_Before_Event_Secs remind_before_event_secs { get; set; }
        /// <summary>
        /// 是否重复日程。0-否；1-是
        /// </summary>
        public int is_repeat { get; set; }
        /// <summary>
        /// 重复类型，当is_repeat为1时有效。
        /// </summary>
        public Repeat_Type repeat_type { get; set; }
    }

    public class Attendee
    {
        /// <summary>
        /// 日程参与者ID
        /// </summary>
        public string userid { get; set; }
    }
    /// <summary>
    /// 注意，更新日程，不可变更组织者
    /// </summary>
    public class ScheduleUpdate : Schedule
    {
        /// <summary>
        /// 日程ID
        /// </summary>
        public string schedule_id { get; set; }
    }
}
