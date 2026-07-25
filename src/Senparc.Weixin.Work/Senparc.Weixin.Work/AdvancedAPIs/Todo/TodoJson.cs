/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TodoJson.cs
    文件功能描述：企业微信待办强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐待办详情与状态强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Todo
{
    /// <summary>
    /// 获取待办详情请求。
    /// </summary>
    public class GetTodoRequest
    {
        /// <summary>
        /// 待办 ID。
        /// </summary>
        public string todo_id { get; set; }
    }

    /// <summary>
    /// 更新待办请求。
    /// </summary>
    public class UpdateTodoRequest
    {
        /// <summary>
        /// 待办 ID。
        /// </summary>
        public string todo_id { get; set; }

        /// <summary>
        /// 待办整体状态：0 表示已完成，1 表示进行中；不修改时不传。
        /// </summary>
        public int? status { get; set; }

        /// <summary>
        /// 待办参与人及其状态，最多 20 个；不修改时不传或传空数组。
        /// </summary>
        public IList<TodoAttendee> attendees { get; set; }
    }

    /// <summary>
    /// 待办参与人及其状态。
    /// </summary>
    public class TodoAttendee
    {
        /// <summary>
        /// 待办参与人 ID。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 参与人的待办状态：0 表示已完成，1 表示进行中。
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 待办提醒信息。
    /// </summary>
    public class TodoReminder
    {
        /// <summary>
        /// 提醒时间戳。
        /// </summary>
        public long remind_time { get; set; }
    }

    /// <summary>
    /// 获取待办详情结果。
    /// </summary>
    public class GetTodoResult : WorkJsonResult
    {
        /// <summary>
        /// 待办内容。
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 待办创建人 ID。
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 待办整体状态：0 表示已完成，1 表示进行中。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 待办创建时间戳。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 待办参与人及其状态。
        /// </summary>
        public IList<TodoAttendee> attendees { get; set; }

        /// <summary>
        /// 待办截止时间戳；未设置截止时间时可能不返回。
        /// </summary>
        public long? end_time { get; set; }

        /// <summary>
        /// 待办提醒列表。
        /// </summary>
        public IList<TodoReminder> reminders { get; set; }
    }
}
