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
    public class QueryUserBalanceJsonResult : WxJsonResult
    {
        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public int balance { get; set; }

        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public int present_balance { get; set; }

        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public int sum_save { get; set; }

        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public int sum_present { get; set; }

        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public int sum_balance { get; set; }

        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public int sum_cost { get; set; }

        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public bool first_save_flag { get; set; }
    }
}
