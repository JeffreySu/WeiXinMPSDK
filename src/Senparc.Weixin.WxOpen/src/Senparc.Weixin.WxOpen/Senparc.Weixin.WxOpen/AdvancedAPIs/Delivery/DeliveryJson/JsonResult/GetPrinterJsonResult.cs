using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    /// <summary>
    /// 获取打印员。若需要使用微信打单 PC 软件，才需要调用
    /// </summary>
    public class GetPrinterJsonResult:WxJsonResult
    {
        /// <summary>
        /// 打印员数量
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 打印员openid
        /// </summary>
        public List<string> openid { get; set; }
        /// <summary>
        /// tagid列表
        /// </summary>
        public List<string> tagid_list { get; set; }
    }
}
