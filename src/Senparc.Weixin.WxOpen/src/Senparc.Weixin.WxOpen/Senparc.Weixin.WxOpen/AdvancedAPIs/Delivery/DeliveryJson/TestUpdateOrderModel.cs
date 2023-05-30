using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson
{
    /// <summary>
    /// 模拟更新订单状态请求参数
    /// </summary>
    public class TestUpdateOrderModel
    {
        /// <summary>
        /// 商户id,需填test_biz_id
        /// </summary>
        public string biz_id {  get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 快递公司id,需填TEST
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string waybill_id { get; set; }
        /// <summary>
        /// 轨迹变化 Unix 时间戳
        /// </summary>
        public long action_time { get; set; }
        /// <summary>
        /// 轨迹变化类型
        /// </summary>
        public int action_type { get; set; }
        /// <summary>
        /// 轨迹变化具体信息说明,使用UTF-8编码
        /// </summary>
        public string action_msg { get; set; }
    }
}
