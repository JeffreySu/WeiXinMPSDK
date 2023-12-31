/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：Order.cs
    文件功能描述：订单
    
    
    创建标识：Yaofeng - 20231026

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.SecOrder
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 原支付交易对应的微信订单号。
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 支付下单商户的商户号，由微信支付生成并下发。
        /// </summary>
        public string merchant_id { get; set; }

        /// <summary>
        /// 二级商户号。
        /// </summary>
        public string sub_merchant_id { get; set; }

        /// <summary>
        /// 商户系统内部订单号，只能是数字、大小写字母`_-*`且在同一个商户号下唯一。
        /// </summary>
        public string merchant_trade_no { get; set; }

        /// <summary>
        /// 以分号连接的该支付单的所有商品描述，当超过120字时自动截断并以 “...” 结尾。
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 支付单实际支付金额，整型，单位：分钱。
        /// </summary>
        public int paid_amount { get; set; }

        /// <summary>
        /// 支付者openid。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 交易创建时间，时间戳形式。
        /// </summary>
        public long trade_create_time { get; set; }

        /// <summary>
        /// 支付时间，时间戳形式。
        /// </summary>
        public long pay_time { get; set; }

        /// <summary>
        /// 订单状态枚举：(1) 待发货；(2) 已发货；(3) 确认收货；(4) 交易完成；(5) 已退款。
        /// </summary>
        public int order_state { get; set; }

        /// <summary>
        /// 是否处在交易纠纷中。
        /// </summary>
        public bool in_complaint { get; set; }

        /// <summary>
        /// 发货信息。
        /// </summary>
        public Order_Shipping shipping { get; set; }
    }

    public class Order_Shipping
    {
        /// <summary>
        /// 发货模式，发货模式枚举值：1、UNIFIED_DELIVERY（统一发货）2、SPLIT_DELIVERY（分拆发货） 示例值: UNIFIED_DELIVERY
        /// </summary>
        public int delivery_mode { get; set; }

        /// <summary>
        /// 物流模式，发货方式枚举值：1、实体物流配送采用快递公司进行实体物流配送形式 2、同城配送 3、虚拟商品，虚拟商品，例如话费充值，点卡等，无实体配送形式 4、用户自提
        /// </summary>
        public int logistics_type { get; set; }

        /// <summary>
        /// 是否已完成全部发货。
        /// </summary>
        public bool finish_shipping { get; set; }

        /// <summary>
        /// 在小程序后台发货信息录入页录入的商品描述。
        /// </summary>
        public string goods_desc { get; set; }

        /// <summary>
        /// 已完成全部发货的次数，未完成时为 0，完成时为 1，重新发货并完成后为 2。
        /// </summary>
        public int finish_shipping_count { get; set; }

        /// <summary>
        /// 物流信息列表，发货物流单列表，支持统一发货（单个物流单）和分拆发货（多个物流单）两种模式。
        /// </summary>
        public List<Order_Shipping_List> shipping_list { get; set; }
    }

    /// <summary>
    /// 联系方式，当发货的物流公司为顺丰时，联系方式为必填，收件人或寄件人联系方式二选一
    /// </summary>
    public class Order_Shipping_List : Shipping
    {
        /// <summary>
        /// 使用上传物流信息 API 录入的该物流信息的商品描述。
        /// </summary>
        public string goods_desc { get; set; }

        /// <summary>
        /// 该物流信息的上传时间，时间戳形式。
        /// </summary>
        public long upload_time { get; set; }
    }
}