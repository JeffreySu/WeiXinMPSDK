using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryAdverFundsJsonResult : WxJsonResult
    {
        /// <summary>
        /// 广告金发放记录列表
        /// </summary>
        public List<QueryAdverFundsItem> adver_funds_list { get; set; }

        /// <summary>
        /// 查询命中总的页数
        /// </summary>
        public int total_page { get; set; }
    }

    /// <summary>
    /// 发布的道具列表
    /// </summary>
    public class QueryAdverFundsItem
    {
        /// <summary>
        /// 结算周期开始时间，unix秒级时间戳
        /// </summary>
        public long settle_begin { get; set; }

        /// <summary>
        /// 查算周期结束时间，unix秒级时间戳
        /// </summary>
        public long settle_end { get; set; }

        /// <summary>
        /// 发放广告金金额，单位分
        /// </summary>
        public int total_amount { get; set; }

        /// <summary>
        /// 剩余可用广告金金额，单位分
        /// </summary>
        public int remain_amount { get; set; }

        /// <summary>
        /// 广告金过期时间，unix秒级时间戳
        /// </summary>
        public long expire_time { get; set; }

        /// <summary>
        /// 广告金发放原因， 0:广告激励，1:通用赠送
        /// </summary>
        public int fund_type { get; set; }

        /// <summary>
        /// 广告金发放ID
        /// </summary>
        public string fund_id { get; set; }
    }
}
