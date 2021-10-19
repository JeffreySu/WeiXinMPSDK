using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 获取已支持的配送公司列表接口
    /// </summary>
    public class GetAllImmeDeliveryJsonResult : ExpressJsonResult
    {
        /// <summary>
        /// 配送公司列表
        /// </summary>
        public List<GetAllImmeDeliveryJsonResult_List> list { get; set; }
    }

    public class GetAllImmeDeliveryJsonResult_List
    {
        /// <summary>
        /// 配送公司Id
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 配送公司名称
        /// </summary>
        public string delivery_name { get; set; }
    }
}
