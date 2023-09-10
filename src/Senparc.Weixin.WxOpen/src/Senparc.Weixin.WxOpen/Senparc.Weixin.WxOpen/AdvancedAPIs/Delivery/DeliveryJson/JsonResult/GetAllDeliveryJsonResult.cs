using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    /// <summary>
    /// 获取支持的快递公司列表
    /// </summary>
    public class GetAllDeliveryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 快递公司数量
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 快递公司信息列表
        /// </summary>
        public List<GetAllDeliveryData> data { get; set; }
    }
    /// <summary>
    /// 快递公司信息
    /// </summary>
    public class GetAllDeliveryData
    {
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string delivery_name { get; set; }
        /// <summary>
        /// 是否支持散单, 1表示支持
        /// </summary>
        public int can_use_cash { get; set; }
        /// <summary>
        /// 是否支持查询面单余额, 1表示支持
        /// </summary>
        public int can_get_quota { get; set; }
        /// <summary>
        /// 支持的服务类型列表
        /// </summary>
        public List<ServiceType> service_type { get; set; }
        /// <summary>
        /// 散单对应的bizid，当can_use_cash=1时有效
        /// </summary>
        public string cash_biz_id { get; set; }
    }
    /// <summary>
    /// 支持的服务类型
    /// </summary>
    public class ServiceType
    {
        /// <summary>
        /// 服务类型编号
        /// </summary>
        public int service_type { get; set; }
        /// <summary>
        /// 服务类型mingc
        /// </summary>
        public string service_name { get; set; }
    }
}
