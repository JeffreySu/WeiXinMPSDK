using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询广告金回收记录
    /// </summary>
    public class QueryRecoverBillRequestData
    {
        /// <summary>
        /// 查询页码，不小于 1
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页记录数量
        /// </summary>
        public int page_size { get; set; }

        /// <summary>
        /// 查询过滤条件
        /// </summary>
        public QueryRecoverBillFilter filter { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }

    /// <summary>
    /// 查询过滤条件
    /// </summary>
    public class QueryRecoverBillFilter
    {
        /// <summary>
        /// 查询回收开始时间，unix秒级时间戳
        /// </summary>
        public long recover_time_begin { get; set; }

        /// <summary>
        /// 查询回收结束时间，unix秒级时间戳
        /// </summary>
        public long recover_time_end { get; set; }

        /// <summary>
        /// (可选)广告金充值单 ID
        /// </summary>
        public string bill_id { get; set; }

    }
}
