/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Schedule.cs
    文件功能描述：企业微信日历与日程回调强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐日历、日程变更与回执回调事件模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>日历事件公共字段。</summary>
    public abstract class RequestMessageEvent_Calendar_Base : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>日历 ID。</summary>
        public string CalId { get; set; }
    }

    /// <summary>删除日历事件。</summary>
    public class RequestMessageEvent_Delete_Calendar : RequestMessageEvent_Calendar_Base
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.delete_calendar;
    }

    /// <summary>修改日历事件。</summary>
    public class RequestMessageEvent_Modify_Calendar : RequestMessageEvent_Calendar_Base
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.modify_calendar;
    }

    /// <summary>日程事件公共字段。</summary>
    public abstract class RequestMessageEvent_Schedule_Base : RequestMessageEvent_Calendar_Base
    {
        /// <summary>日程 ID。</summary>
        public string ScheduleId { get; set; }
    }

    /// <summary>修改日程事件。</summary>
    public class RequestMessageEvent_Modify_Schedule : RequestMessageEvent_Schedule_Base
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.modify_schedule;
    }

    /// <summary>删除日程事件。</summary>
    public class RequestMessageEvent_Delete_Schedule : RequestMessageEvent_Schedule_Base
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.delete_schedule;
    }

    /// <summary>日程参与人回执事件。</summary>
    public class RequestMessageEvent_Respond_Schedule : RequestMessageEvent_Schedule_Base
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.respond_schedule;
    }
}
