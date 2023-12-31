/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：Schedule.cs
    文件功能描述：日程信息
    
    
    创建标识：lishewen - 20191226
    
    修改标识：Senparc - 20230226
    修改描述：v3.15.16 添加 RepeatDayOfWeek 枚举、完善 Schedule 属性

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
    public enum RepeatDayOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
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
        public List<Attendee> attendees { get; set; }
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
        /// 日程所属日历ID
        /// 第三方应用必须指定cal_id
        /// </summary>
        public string cal_id { get; set; }
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
        /// <summary>
        /// 重复结束时刻，Unix时间戳。不填或填0表示一直重复
        /// </summary>
        public int repeat_unit { get; set; }
        /// <summary>
        /// 是否自定义重复。0-否；1-是
        /// </summary>
        public ushort is_custom_repeat { get; set; }
        /// <summary>
        /// 重复间隔
        /// 仅当指定为自定义重复时有效
        /// 该字段随repeat_type不同而含义不同
        /// 例如：
        /// repeat_interval指定为3，repeat_type指定为每周重复，那么每3周重复一次；
        /// repeat_interval指定为3，repeat_type指定为每月重复，那么每3个月重复一次
        /// </summary>
        public int repeat_interval { get; set; }
        /// <summary>
        /// 每周周几重复
        /// 仅当指定为自定义重复且重复类型为每周时有效
        /// 取值范围：1 ~ 7，分别表示周一至周日
        /// </summary>
        public RepeatDayOfWeek[] repeat_day_of_week { get; set; }
        /// <summary>
        /// 每月哪几天重复
        /// 仅当指定为自定义重复且重复类型为每月时有效
        /// 取值范围：1 ~ 31，分别表示1~31号
        /// </summary>
        public int[] repeat_day_of_month { get; set; }
        /// <summary>
        /// 时区。UTC偏移量表示(即偏离零时区的小时数)，东区为正数，西区为负数。
        /// 例如：+8 表示北京时间东八区
        /// 默认为北京时间东八区
        /// 取值范围：-12 ~ +12
        /// </summary>
        public int timezone { get; set; } = 8;
    }

    public class Attendee
    {
        /// <summary>
        /// 日程参与者ID
        /// </summary>
        public string userid { get; set; }
    }
    public class ScheduleAdd
    {
        /// <summary>
        /// 日程信息
        /// </summary>
        public Schedule schedule { get; set; }
        /// <summary>
        /// 授权方安装的应用agentid
        /// </summary>
        public int agentid { get; set; }
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
