/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：QueryBizBalanceJsonResult.cs
    文件功能描述：QueryBizBalanceJsonResult 强类型数据模型


    创建标识：Senparc - 20231130

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询商家账户可提现余额结果。
    /// </summary>
    public class QueryBizBalanceJsonResult : WxJsonResult
    {
        /// <summary>
        /// 可提现余额
        /// </summary>
        public QueryBizBalanceAvailable balance_available { get; set; }
    }

    /// <summary>
    /// 可提现余额信息。
    /// </summary>
    public class QueryBizBalanceAvailable
    {
        /// <summary>
        /// 可提现余额，单位元
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// 币种（一般为CNY）
        /// </summary>
        public string currency_code { get; set; }
    }
}
