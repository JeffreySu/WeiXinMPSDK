using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class BaseProductData
    {
        /// <summary>
        /// 基本属性
        /// </summary>
        public Product_base product_base { get; set; }
        /// <summary>
        /// sku信息列表(可为多个)，每个sku信息串即为一个确定的商品，比如白色的37码的鞋子
        /// </summary>
        public List<Sku_list> sku_list { get; set; }
        /// <summary>
        /// 商品其他属性
        /// </summary>
        public Attrext attrext { get; set; }
        /// <summary>
        /// 运费信息
        /// </summary>
        public Delivery_info delivery_info { get; set; }
    }

    public class Product_base
    {
        /// <summary>
        /// 商品分类id
        /// </summary>
        public string[] category_id { get; set; }
        /// <summary>
        /// 商品属性列表
        /// </summary>
        public List<Property> property { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 商品sku定义
        /// </summary>
        public Sku_info sku_info { get; set; }
        /// <summary>
        /// 商品主图
        /// </summary>
        public string main_img { get; set; }
        /// <summary>
        /// 商品图片列表
        /// </summary>
        public string[] img { get; set; }
        /// <summary>
        /// 商品详情列表，显示在客户端的商品详情页内
        /// </summary>
        public List<Detail> detail { get; set; }
        /// <summary>
        /// 用户商品限购数量
        /// </summary>
        public int buy_limit { get; set; }
    }

    public class Property
    {
        /// <summary>
        /// 属性id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 属性值id
        /// </summary>
        public int vid { get; set; }
    }

    public class Sku_info
    {
        /// <summary>
        /// sku属性
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// sku值
        /// </summary>
        public int[] vid { get; set; }
    }

    public class Detail
    {
        /// <summary>
        /// 文字描述
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img { get; set; }
    }

    public class Sku_list
    {
        /// <summary>
        /// sku信息
        /// </summary>
        public string sku_id { get; set; }
        /// <summary>
        /// sku微信价(单位 : 分, 微信价必须比原价小, 否则添加商品失败)
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// sku iconurl(图片需调用图片上传接口获得图片Url)
        /// </summary>
        public string icon_url { get; set; }
        /// <summary>
        /// 商家商品编码
        /// </summary>
        public string product_code { get; set; }
        /// <summary>
        /// sku原价(单位 : 分)
        /// </summary>
        public int ori_price { get; set; }
        /// <summary>
        /// sku库存
        /// </summary>
        public int quantity { get; set; }

    }

    public class Attrext
    {
        /// <summary>
        /// 商品所在地地址
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 是否包邮(0-否, 1-是), 如果包邮delivery_info字段可省略
        /// </summary>
        public bool isPostFree { get; set; }
        /// <summary>
        /// 是否提供发票(0-否, 1-是)
        /// </summary>
        public bool isHasReceipt { get; set; }
        /// <summary>
        /// 是否保修(0-否, 1-是)
        /// </summary>
        public bool isUnderGuaranty { get; set; }
        /// <summary>
        /// 是否支持退换货(0-否, 1-是)
        /// </summary>
        public bool isSupportReplace { get; set; }
    }

    public class Location
    {
        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
    }

    /// 快递列表(Id   说明)
    /// 10000027	平邮
    /// 10000028	快递
    /// 10000029	EMS
    public class Delivery_info
    {
        /// <summary>
        /// 运费类型(0-使用下面express字段的默认模板, 1-使用template_id代表的邮费模板)
        /// </summary>
        public int delivery_type { get; set; }
        /// <summary>
        /// 邮费模板ID
        /// </summary>
        public int template_id { get; set; }
        /// <summary>
        /// 快递信息
        /// </summary>
        public List<Express> express { get; set; }
    }

    public class Express
    {
        /// <summary>
        /// 快递ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 运费(单位 : 分)
        /// </summary>
        public int price { get; set; }
    }
    
    /// <summary>
    /// 添加商品信息
    /// </summary>
    public class AddProductData : BaseProductData
    {
    }

    /// <summary>
    /// 修改商品信息
    /// </summary>
    public class UpdateProductData : BaseProductData
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string product_id { get; set; }
    }
}
