using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 获取盘容量信息返回参数
    /// </summary>
    public class MngCapacityJsonResult:WorkJsonResult
    {
        /// <summary>
        /// 	全员容量总数,单位是B
        /// </summary>
        public int total_capacity_for_all { get; set; }
        /// <summary>
        /// 专业容量总数,单位是B
        /// </summary>
        public int total_capacity_for_vip { get; set; }
        /// <summary>
        /// 	全员容量可用总数,单位是B（第三方不返回该字段）
        /// </summary>
        public int rest_capacity_for_all { get; set; }
        /// <summary>
        /// 专业容量可用总数,单位是B（第三方不返回该字段）
        /// </summary>
        public int rest_capacity_for_vip { get; set; }
    }
}
