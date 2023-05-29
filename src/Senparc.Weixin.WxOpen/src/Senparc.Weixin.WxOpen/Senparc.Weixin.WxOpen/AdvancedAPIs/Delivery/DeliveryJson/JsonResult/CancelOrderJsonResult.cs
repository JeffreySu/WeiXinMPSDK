using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    /// <summary>
    /// 取消订单返回参数
    /// </summary>
    public class CancelOrderJsonResult:WxJsonResult
    {
        /// <summary>
        /// 运力返回的错误码
        /// </summary>
        public int delivery_resultcode { get; set; }
        /// <summary>
        /// 运力返回的错误信息
        /// </summary>
        public string delivery_resultmsg { get; set; }
    }
}
