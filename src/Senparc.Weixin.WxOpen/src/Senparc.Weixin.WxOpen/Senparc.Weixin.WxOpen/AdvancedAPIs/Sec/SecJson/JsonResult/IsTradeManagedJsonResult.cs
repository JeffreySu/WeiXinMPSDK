using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    /// <summary>
    /// 查询小程序是否已开通发货信息管理服务
    /// </summary>
    public class IsTradeManagedJsonResult: WxJsonResult
    {
        /// <summary>
        /// 是否已开通小程序发货信息管理服务
        /// </summary>
        public bool is_trade_managed { get; set; } 
    }
}
