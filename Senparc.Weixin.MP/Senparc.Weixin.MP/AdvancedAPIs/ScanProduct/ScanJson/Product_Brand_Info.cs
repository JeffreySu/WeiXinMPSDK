using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class Product_Brand_Info
    {
        /// <summary>
        /// 商品基本信息
        /// </summary>
        public Product_Brand_Base_Info base_info { get; set; }

        /// <summary>
        /// 商品详细描述信息
        /// </summary>
        public Product_Brand_Detail_Info detail_info { get; set; }

        /// <summary>
        /// 商品推广服务区信息
        /// </summary>
        public Product_Brand_Action_Info action_info { get; set; }

        /// <summary>
        /// 商品组件信息
        /// </summary>
        public Product_Brand_Module_Info module_info { get; set; }
    }
}
