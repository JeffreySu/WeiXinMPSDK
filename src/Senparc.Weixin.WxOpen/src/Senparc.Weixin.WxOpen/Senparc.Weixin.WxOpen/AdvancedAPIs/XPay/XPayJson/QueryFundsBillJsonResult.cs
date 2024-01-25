using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryFundsBillJsonResult : WxJsonResult
    {
        /// <summary>
        /// 广告金充值记录列表
        /// </summary>
        public List<QueryFundsBillItem> bill_list { get; set; }

        /// <summary>
        /// 查询命中总的页数
        /// </summary>
        public int total_page { get; set; }
    }

    /// <summary>
    /// 广告金充值记录列表
    /// </summary>
    public class QueryFundsBillItem
    {
        /// <summary>
        /// 充值单 ID
        /// </summary>
        public string bill_id { get; set; }

        /// <summary>
        /// 充值时间，unix秒级时间戳
        /// </summary>
        public long oper_time { get; set; }

        /// <summary>
        /// 对应广告金结算周期开始时间，unix秒级时间戳
        /// </summary>
        public long settle_begin { get; set; }

        /// <summary>
        /// 对应广告金结算周期结束时间，unix秒级时间戳
        /// </summary>
        public long settle_end { get; set; }

        /// <summary>
        /// 对应广告金ID
        /// </summary>
        public string fund_id { get; set; }

        /// <summary>
        /// 充值账户
        /// </summary>
        public string transfer_account_name { get; set; }

        /// <summary>
        /// 充值账户UID
        /// </summary>
        public long transfer_account_uid { get; set; }

        /// <summary>
        /// 充值金额，单位：分
        /// </summary>
        public int transfer_amount { get; set; }

        /// <summary>
        /// 广告金充值状态：0-充值中，1-充值成功，2-充值失败
        /// </summary>
        public int status { get; set; }
    }
}
