/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：AddScheduleJsonResult.cs
    文件功能描述：创建日程接口返回参数
    
    
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
    public class AddScheduleJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 日程ID
        /// </summary>
        public string schedule_id { get; set; }
    }
}
