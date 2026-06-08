/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：GetCalendarJsonResult.cs
    文件功能描述：获取日历接口返回参数
    
    
    创建标识：Copilot - 20260608
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Calendar.CalendarJson
{
    /// <summary>
    /// 获取日历接口返回参数
    /// </summary>
    public class GetCalendarJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 日历列表
        /// </summary>
        public List<CalendarResult> calendar_list { get; set; }
    }

    /// <summary>
    /// 日历结果信息
    /// </summary>
    public class CalendarResult : CalendarUpdate
    {
    }
}
