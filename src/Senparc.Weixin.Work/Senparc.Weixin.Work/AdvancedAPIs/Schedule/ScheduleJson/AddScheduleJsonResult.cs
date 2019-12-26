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
