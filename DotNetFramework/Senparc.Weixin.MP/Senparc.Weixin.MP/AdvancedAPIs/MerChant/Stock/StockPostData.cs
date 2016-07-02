namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    public class BaseStockData
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// sku信息,格式"id1:vid1;id2:vid2",如商品为统一规格，则此处赋值为空字符串即可
        /// </summary>
        public string sku_info { get; set; }
        /// <summary>
        /// 增加的库存数量
        /// </summary>
        public int quantity { get; set; }
    }

    /// <summary>
    /// 增加库存
    /// </summary>
    public class AddStockData : BaseStockData
    {
    }

    /// <summary>
    /// 减少库存
    /// </summary>
    public class ReduceStockData : BaseStockData
    {
    }
}
