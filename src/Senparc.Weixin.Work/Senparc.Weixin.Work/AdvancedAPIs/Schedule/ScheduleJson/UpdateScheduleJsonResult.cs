/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：UpdateScheduleJsonResult.cs
    文件功能描述：更新重复日程接口返回参数


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐重复日程分裂后的日程标识返回模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Schedule.ScheduleJson
{
    /// <summary>
    /// 更新日程结果。更新部分重复周期时，企业微信会返回分裂后新产生的日程 ID。
    /// </summary>
    public class UpdateScheduleJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 修改重复日程后新产生的日程 ID；修改全部周期时可能为空。
        /// </summary>
        public string schedule_id { get; set; }
    }
}
