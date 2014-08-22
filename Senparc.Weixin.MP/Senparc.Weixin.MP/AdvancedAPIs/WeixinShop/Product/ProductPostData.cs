using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class ProductData
    {
        public Product_base product_base { get; set; }//基本属性
        public List<Sku_list> sku_list { get; set; }//sku信息列表(可为多个)，每个sku信息串即为一个确定的商品，比如白色的37码的鞋子
        public Attrext attrext { get; set; }//商品其他属性
        public Delivery_info delivery_info { get; set; }//运费信息
    }

    public class Product_base
    {
        public string[] category_id { get; set; }//商品分类id
        public List<Property> property { get; set; }//商品属性列表
        public string name { get; set; }//商品名称
        public Sku_info sku_info { get; set; }//商品sku定义
        public string main_img { get; set; }//商品主图
        public string[] img { get; set; }//商品图片列表
        public List<Detail> detail { get; set; }//商品详情列表，显示在客户端的商品详情页内
        public int buy_limit { get; set; }//用户商品限购数量
    }

    public class Property
    {
        public int id { get; set; }//属性id
        public int vid { get; set; }//属性值id
    }

    public class Sku_info
    {
        public int id { get; set; }//sku属性
        public int[] vid { get; set; }//sku值
    }

    public class Detail
    {
        public string text { get; set; }//文字描述
        public string img { get; set; }//图片
    }

    public class Sku_list
    {
        public string sku_id { get; set; }//sku信息
        public int price { get; set; }//sku微信价(单位 : 分, 微信价必须比原价小, 否则添加商品失败)
        public string icon_url { get; set; }//sku iconurl(图片需调用图片上传接口获得图片Url)
        public string product_code { get; set; }//商家商品编码
        public int ori_price { get; set; }//sku原价(单位 : 分)
        public int quantity { get; set; }//sku库存

    }

    public class Attrext
    {
        public Location location { get; set; }//商品所在地地址
        public bool isPostFree { get; set; }//是否包邮(0-否, 1-是), 如果包邮delivery_info字段可省略
        public bool isHasReceipt { get; set; }//是否提供发票(0-否, 1-是)
        public bool isUnderGuaranty { get; set; }//是否保修(0-否, 1-是)
        public bool isSupportReplace { get; set; }//是否支持退换货(0-否, 1-是)
    }

    public class Location
    {
        public string country { get; set; }//国家
        public string province { get; set; }//省份
        public string city { get; set; }//城市
        public string address { get; set; }//地址
    }

    /// 快递列表(Id   说明)
    /// 10000027	平邮
    /// 10000028	快递
    /// 10000029	EMS
    public class Delivery_info
    {
        public int delivery_type { get; set; }//运费类型(0-使用下面express字段的默认模板, 1-使用template_id代表的邮费模板)
        public int template_id { get; set; }//邮费模板ID
        public List<Express> express { get; set; }//快递信息
    }

    public class Express
    {
        public int id { get; set; }//快递ID
        public int price { get; set; }//运费(单位 : 分)
    }
    
    /// <summary>
    /// 添加商品信息
    /// </summary>
    public class AddProductData : ProductData
    {
    }

    /// <summary>
    /// 修改商品信息
    /// </summary>
    public class UpdateProductData : ProductData
    {
        public string product_id { get; set; }
    }
}
