using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 根据订单ID获取订单详情返回结果
    /// </summary>
    public class GetByIdOrderResult : WxJsonResult
    {
        public Order order { get; set; }//订单详情
    }

    public class Order
    {
        public string order_id { get; set; }//订单ID
        public int order_status { get; set; }//订单状态
        public int order_total_price { get; set; }//订单总价格(单位 : 分)
        public string order_create_time { get; set; }//订单创建时间
        public int order_express_price { get; set; }//订单运费价格(单位 : 分)
        public string buyer_openid { get; set; }//买家微信OPENID
        public string buyer_nick { get; set; }//买家微信昵称
        public string receiver_name { get; set; }//收货人姓名
        public string receiver_province { get; set; }//收货地址省份
        public string receiver_city { get; set; }//收货地址城市
        public string receiver_address { get; set; }//收货详细地址
        public string receiver_mobile { get; set; }//收货人移动电话
        public string receiver_phone { get; set; }//收货人固定电话
        public string product_id { get; set; }//商品ID
        public string product_name { get; set; }//商品名称
        public int product_price { get; set; }//商品价格(单位 : 分)
        public string product_sku { get; set; }//商品SKU
        public int product_count { get; set; }//商品个数
        public string product_img { get; set; }//商品图片
        public string delivery_id { get; set; }//运单ID
        public string delivery_company { get; set; }//物流公司编码
        public string trans_id { get; set; }//交易ID
    }

    /// <summary>
    /// 根据订单状态/创建时间获取订单详情返回结果
    /// </summary>
    public class GetByFilterResult : WxJsonResult
    {
        public List<Order> order_list { get; set; } 
    }
}

