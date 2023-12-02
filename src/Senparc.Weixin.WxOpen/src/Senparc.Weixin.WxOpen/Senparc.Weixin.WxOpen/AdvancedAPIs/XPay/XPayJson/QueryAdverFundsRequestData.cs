using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询广告金发放记录
    /// </summary>
    public class QueryAdverFundsRequestData
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
    /// 发布的商品列表
    /// </summary>
    public class QueryAdverFundsFilter
    {
        /// <summary>
        /// 查询结算周期开始时间，unix秒级时间戳
        /// </summary>
        public long settle_begin { get; set; }

        /// <summary>
        /// 查询结算周期结束时间，unix秒级时间戳
        /// </summary>
        public long settle_end { get; set; }

        /// <summary>
        /// (可选)广告金发放原因， 0:广告激励，1:通用赠送
        /// </summary>
        public int fund_type { get; set; }

    }
}
