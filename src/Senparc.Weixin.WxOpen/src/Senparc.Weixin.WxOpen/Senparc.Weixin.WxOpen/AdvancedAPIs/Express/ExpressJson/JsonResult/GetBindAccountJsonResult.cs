using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 拉取已绑定账号
    /// </summary>
    public class GetBindAccountJsonResult : ExpressJsonResult
    {
        /// <summary>
        /// 绑定的商家签约账号列表
        /// </summary>
        public List<GetBindAccountJsonResult_Shop_List> shop_list { get; set; }
    }

    public class GetBindAccountJsonResult_Shop_List
    {
        /// <summary>
        /// 配送公司Id
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string audit_result { get; set; }
    }
}
