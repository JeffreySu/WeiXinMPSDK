using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class MerchantInfoResult : WxJsonResult
    {
        /// <summary>
        /// 商品类目列表
        /// </summary>
        public List<VerifiedCate> verified_cate_list { get; set; }

        /// <summary>
        /// 商品号段列表，包含该账户下有资质的条码号段
        /// </summary>
        public List<int> verified_firm_code_list { get; set; }

        /// <summary>
        /// 品牌标签列表，创建商品时传入，商户自定义生产成的品牌标识字段
        /// </summary>
        public List<string> brand_tag_list { get; set; }
    }

}
