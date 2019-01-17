using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.Poi;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    public class StoreJsonResult : WxJsonResult
    {
        public BusinessData business { get; set; }
    }

    public class BusinessData
    {
        public StoreBaseData base_info { get; set; }
    }

    public class StoreBaseData : StoreBaseInfo
    {
        /// <summary>
        /// 1.审核通过
        /// 2.审核中
        /// 3.审核失败
        /// </summary>
        public int status { get; set; }
    }

    public class StoreListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 店铺列表
        /// </summary>
        public IEnumerable<StoreBaseData> business_list { get; set; }
        /// <summary>
        /// 店铺总数
        /// </summary>
        public int total_count { get; set; }
    }
}
