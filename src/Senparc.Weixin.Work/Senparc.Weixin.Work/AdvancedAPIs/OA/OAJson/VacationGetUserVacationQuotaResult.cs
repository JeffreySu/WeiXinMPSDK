

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 
    /// </summary>
    public class VacationGetUserVacationQuotaResult : WorkJsonResult
    {
        /// <summary>
        /// 假期列表
        /// </summary>
        public List<VacationGetUserVacationQuotaResult_Lists> lists { get; set; }
    }

    public class VacationGetUserVacationQuotaResult_Lists
    {
        /// <summary>
        /// 假期id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 发放时长，单位为秒
        /// </summary>
        public int assignduration { get; set; }

        /// <summary>
        /// 使用时长，单位为秒
        /// </summary>
        public int usedduration { get; set; }

        /// <summary>
        /// 剩余时长，单位为秒
        /// </summary>
        public int leftduration { get; set; }

        /// <summary>
        /// 假期名称
        /// </summary>
        public string vacationname { get; set; }

        /// <summary>
        /// 假期的实际发放时长，通常在设置了按照实际工作时间发放假期后进行计算
        /// </summary>
        public int real_assignduration { get; set; }
    }
}
