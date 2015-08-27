using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class StockData
    {
        public string product_id { get; set; }//商品ID
        public string sku_info { get; set; }//sku信息,格式"id1:vid1;id2:vid2",如商品为统一规格，则此处赋值为空字符串即可
        public int quantity { get; set; }//增加的库存数量
    }

    /// <summary>
    /// 增加库存
    /// </summary>
    public class AddStockData : StockData
    {
    }

    /// <summary>
    /// 减少库存
    /// </summary>
    public class ReduceStockData : StockData
    {
    }
}
