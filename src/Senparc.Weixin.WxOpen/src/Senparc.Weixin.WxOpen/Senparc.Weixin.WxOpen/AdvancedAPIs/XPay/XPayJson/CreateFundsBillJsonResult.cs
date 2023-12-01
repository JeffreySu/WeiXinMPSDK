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
    public class CreateFundsBillJsonResult : WxJsonResult
    {
        /// <summary>
        /// 充值单 id
        /// </summary>
        public string bill_id { get; set; }
    }
}
