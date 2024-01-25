using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 获取投诉列表
    /// </summary>
    public class GetComplaintListRequestData
    {
        /// <summary>
        /// 筛选偏移，从0开始
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 筛选最多返回条数
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 筛选开始时间，格式为yyyy-mm-dd,如“2023-01-01”
        /// </summary>
        public string begin_date { get; set; }

        /// <summary>
        /// 筛选结束时间，格式为yyyy-mm-dd,如“2023-01-01”
        /// </summary>
        public string end_date { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
