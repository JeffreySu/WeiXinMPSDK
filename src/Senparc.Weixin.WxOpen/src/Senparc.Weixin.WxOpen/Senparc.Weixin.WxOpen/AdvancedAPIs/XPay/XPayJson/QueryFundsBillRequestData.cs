using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询广告金充值记录
    /// </summary>
    public class QueryFundsBillRequestData
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
        public QueryAdverFundsFilter filter { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }

    /// <summary>
    /// 查询过滤条件
    /// </summary>
    public class QueryFundsBillFilter
    {
        /// <summary>
        /// 查询充值开始时间，unix秒级时间戳
        /// </summary>
        public long oper_time_begin { get; set; }

        /// <summary>
        /// 查询充值结束时间，unix秒级时间戳
        /// </summary>
        public long oper_time_end { get; set; }

        /// <summary>
        /// (可选)广告金充值单 ID
        /// </summary>
        public string bill_id { get; set; }

    }
}
