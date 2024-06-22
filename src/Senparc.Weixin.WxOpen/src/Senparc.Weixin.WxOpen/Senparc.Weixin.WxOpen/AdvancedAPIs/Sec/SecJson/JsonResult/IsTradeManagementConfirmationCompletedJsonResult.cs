using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    /// <summary>
    /// 查询小程序是否已完成交易结算管理确认
    /// </summary>
    public class IsTradeManagementConfirmationCompletedJsonResult : WxJsonResult
    {
        /// <summary>
        /// 是否已完成交易结算管理确认
        /// </summary>
        public bool completed { get; set; }
    }
}
