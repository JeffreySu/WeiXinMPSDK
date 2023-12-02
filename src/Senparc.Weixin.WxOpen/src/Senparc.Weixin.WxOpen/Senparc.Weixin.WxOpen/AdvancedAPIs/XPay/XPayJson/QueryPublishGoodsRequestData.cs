using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询批量发布道具任务
    /// </summary>
    public class QueryPublishGoodsRequestData
    {
        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
