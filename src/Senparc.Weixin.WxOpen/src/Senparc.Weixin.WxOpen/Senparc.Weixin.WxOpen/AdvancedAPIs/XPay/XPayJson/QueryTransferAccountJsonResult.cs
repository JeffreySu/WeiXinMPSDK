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
    public class QueryTransferAccountJsonResult : WxJsonResult
    {
        /// <summary>
        /// 可提现余额
        /// </summary>
        public List<QueryTransferAccountItem> acct_list { get; set; }
    }

    /// <summary>
    /// 发布的道具列表
    /// </summary>
    public class QueryTransferAccountItem
    {
        /// <summary>
        /// 充值账户名称
        /// </summary>
        public string transfer_account_name { get; set; }

        /// <summary>
        /// 充值账户 uid
        /// </summary>
        public long transfer_account_uid { get; set; }

        /// <summary>
        /// 充值账户服务商账号 id
        /// </summary>
        public long transfer_account_agency_id { get; set; }

        /// <summary>
        /// 充值账户服务商账号名称
        /// </summary>
        public string transfer_account_agency_name { get; set; }
    }
}
