/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：AddCalendarJsonResult.cs
    文件功能描述：创建日历接口返回参数
    
    
    创建标识：Copilot - 20260608
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Calendar.CalendarJson
{
    /// <summary>
    /// 创建日历接口返回参数
    /// </summary>
    public class AddCalendarJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 日历ID
        /// </summary>
        public string cal_id { get; set; }
    }
}
