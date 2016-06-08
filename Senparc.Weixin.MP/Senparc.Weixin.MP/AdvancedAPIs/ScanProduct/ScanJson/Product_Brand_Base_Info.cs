using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class Product_Brand_Base_Info
    {
        /// <summary>
        /// 商品名称，不要超过15个字，超过会以省略号结束
        /// </summary>
        public string title { get; set; }
        
        /// <summary>
        /// 商品缩略图
        /// </summary>
        public string thumb_url { get; set; }

        /// <summary>
        /// 品牌字段
        /// </summary>
        public string brand_tag { get; set; }

        /// <summary>
        /// 商品类目ID
        /// </summary>
        public string category_id { get; set; }

        /// <summary>
        /// 是否展示有该商品的电商渠道
        /// </summary>
        public string store_mgr_type { get; set; }

        /// <summary>
        /// 电商渠道,如果 store_mgr_type 为 custom,则可从以下电商渠道进行选择:
        /// 2 为亚马逊,3 为当当网,4 为京东,9 为一号店,11为聚美优品,19 为酒仙网
        /// </summary>
        public List<int> store_vendorid_list { get; set; }

        /// <summary>
        /// 主页头部背景颜色，必须大写的例如FFFFFF，不要传入#
        /// </summary>
        public string color { get; set; }
    }
}
