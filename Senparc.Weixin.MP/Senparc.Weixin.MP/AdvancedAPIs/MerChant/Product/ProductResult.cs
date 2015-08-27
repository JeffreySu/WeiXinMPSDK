using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 增加商品返回结果
    /// </summary>
    public class AddProductResult : WxJsonResult
    {
        public string product_id { get; set; }
    }

    /// <summary>
    /// 查询商品返回结果
    /// </summary>
    public class GetProductResult : WxJsonResult
    {
        //商品详细信息
        public ProductInfoData product_info { get; set; }
    }

    /// <summary>
    /// 修改商品信息
    /// </summary>
    public class ProductInfoData : BaseProductData
    {
        public string product_id { get; set; }
    }

    /// <summary>
    /// 获取指定状态的所有商品返回结果
    /// </summary>
    public class GetByStatusResult : WxJsonResult
    {
        public List<GetByStatusProductInfo> products_info { get; set; }
    }

    public class GetByStatusProductInfo : BaseProductData
    {
        public string product_id { get; set; }
        public int status { get; set; }
    }

    /// <summary>
    /// 获取指定分类的所有子分类返回结果
    /// </summary>
    public class GetSubResult : WxJsonResult
    {
        public List<CateItem> cate_list { get; set; }
    }

    public class CateItem
    {
        public int id { get; set; }//子分类ID
        public string name { get; set; }//子分类名称
    }

    /// <summary>
    /// 获取指定子分类的所有SKU返回结果
    /// </summary>
    public class GetSkuResult : WxJsonResult
    {
        public List<Sku> sku_table { get; set; }
    }

    public class Sku
    {
        public int id { get; set; }//sku id
        public string name { get; set; }//sku 名称
        public List<Value> value_list { get; set; }//sku vid列表
    }

    public class Value
    {
        public int id { get; set; }//vid
        public string name { get; set; }//vid名称
    }

    /// <summary>
    /// 获取指定分类的所有属性返回结果
    /// </summary>
    public class GetPropertyResult : WxJsonResult
    {
        public List<PropertyItem> properties { get; set; }
    }

    public class PropertyItem
    {
        public int id { get; set; }//属性id
        public string name { get; set; }//属性名称
        public List<PropertyValue> property_value { get; set; }//属性值
    }

    public class PropertyValue
    {
        public int id { get; set; }//属性值id
        public string name { get; set; }//属性值名称
    }
}
