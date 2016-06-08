using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class GetProductResult : WxJsonResult
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public Product_Brand_Info brand_info { get; set; }
    }

}
