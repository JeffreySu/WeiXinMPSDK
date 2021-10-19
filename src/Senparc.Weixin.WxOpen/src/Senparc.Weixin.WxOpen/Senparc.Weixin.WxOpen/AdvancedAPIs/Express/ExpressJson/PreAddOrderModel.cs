using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class PreAddOrderModel
    {
        /// <summary>
        /// 商家id， 由配送公司分配的appkey
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 唯一标识订单的 ID，由商户生成, 不超过128字节
        /// </summary>
        public string shop_order_id { get; set; }
        /// <summary>
        /// 商家门店编号，在配送公司登记，美团、闪送必填
        /// </summary>
        public string shop_no { get; set; }
        /// <summary>
        /// 用配送公司提供的appSecret加密的校验串说明
        /// </summary>
        public string delivery_sign { get; set; }
        /// <summary>
        /// 配送公司ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 下单用户的openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 发件人信息，闪送、顺丰同城急送必须填写，美团配送、达达，若传了shop_no的值可不填该字段
        /// </summary>
        public PreAddOrderSender sender { get; set; }
        /// <summary>
        /// 收件人信息
        /// </summary>
        public PreAddOrderReceiver receiver { get; set; }
        /// <summary>
        /// 货物信息
        /// </summary>
        public PreAddOrderCargo cargo { get; set; }
        /// <summary>
        /// 订单信息
        /// </summary>
        public PreAddOrderOrderInfo order_info { get; set; }
        /// <summary>
        /// 商品信息，会展示到物流通知消息中
        /// </summary>
        public PreAddOrderShop shop { get; set; }
        /// <summary>
        /// 子商户id，区分小程序内部多个子商户
        /// </summary>
        public string sub_biz_id { get; set; }
    }

    public class PreAddOrderSender
    {
        /// <summary>
        /// 姓名，最长不超过256个字符
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 城市名称，如广州市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 地址(街道、小区、大厦等，用于定位)
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 地址详情(楼号、单元号、层号)
        /// </summary>
        public string address_detail { get; set; }
        /// <summary>
        /// 电话/手机号，最长不超过64个字符
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 经度（火星坐标或百度坐标，和 coordinate_type 字段配合使用，确到小数点后6位
        /// </summary>
        public decimal lng { get; set; }
        /// <summary>
        /// 纬度（火星坐标或百度坐标，和 coordinate_type 字段配合使用，精确到小数点后6位）
        /// </summary>
        public decimal lat { get; set; }
        /// <summary>
        /// 坐标类型，0：火星坐标（高德，腾讯地图均采用火星坐标） 1：百度坐标
        /// </summary>
        public int coordinate_type { get; set; }
    }

    public class PreAddOrderReceiver
    {
        /// <summary>
        /// 姓名，最长不超过256个字符
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 城市名称，如广州市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 地址(街道、小区、大厦等，用于定位)
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 地址详情(楼号、单元号、层号)
        /// </summary>
        public string address_detail { get; set; }
        /// <summary>
        /// 电话/手机号，最长不超过64个字符
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 经度（火星坐标或百度坐标，和 coordinate_type 字段配合使用，确到小数点后6位
        /// </summary>
        public decimal lng { get; set; }
        /// <summary>
        /// 纬度（火星坐标或百度坐标，和 coordinate_type 字段配合使用，精确到小数点后6位）
        /// </summary>
        public decimal lat { get; set; }
        /// <summary>
        /// 坐标类型，0：火星坐标（高德，腾讯地图均采用火星坐标） 1：百度坐标
        /// </summary>
        public int coordinate_type { get; set; }
    }

    public class PreAddOrderCargo
    {
        /// <summary>
        /// 货物价格，单位为元，精确到小数点后两位（如果小数点后位数多于两位，则四舍五入保留两位小数），范围为(0-5000]
        /// </summary>
        public string goods_value { get; set; }
        /// <summary>
        /// 货物高度，单位为cm，精确到小数点后两位（如果小数点后位数多于两位，则四舍五入保留两位小数），范围为(0-45]
        /// 非必填
        /// </summary>
        public string goods_height { get; set; }
        /// <summary>
        /// 货物长度，单位为cm，精确到小数点后两位（如果小数点后位数多于两位，则四舍五入保留两位小数），范围为(0-65]
        /// 非必填
        /// </summary>
        public string goods_length { get; set; }
        /// <summary>
        /// 货物宽度，单位为cm，精确到小数点后两位（如果小数点后位数多于两位，则四舍五入保留两位小数），范围为(0-50]
        /// 非必填
        /// </summary>
        public string goods_width { get; set; }
        /// <summary>
        /// 货物重量，单位为kg，精确到小数点后两位（如果小数点后位数多于两位，则四舍五入保留两位小数），范围为(0-50]
        /// </summary>
        public string goods_weight { get; set; }
        /// <summary>
        /// 货物详情，最长不超过10240个字符
        /// 非必填
        /// </summary>
        public PreAddOrderGoods goods_detail { get; set; }
        /// <summary>
        /// 货物取货信息，用于骑手到店取货，最长不超过100个字符
        /// 非必填
        /// </summary>
        public string goods_pickup_info { get; set; }
        /// <summary>
        /// 货物交付信息，最长不超过100个字符
        /// 非必填
        /// </summary>
        public string goods_delivery_info { get; set; }
        /// <summary>
        /// 品类一级类目, 详见品类表
        /// </summary>
        public string cargo_first_class { get; set; }
        /// <summary>
        /// 品类二级类目
        /// </summary>
        public string cargo_second_class { get; set; }
    }

    public class PreAddOrderGoods
    {
        /// <summary>
        /// 货物数量
        /// </summary>
        public int good_count { get; set; }
        /// <summary>
        /// 货品名称
        /// </summary>
        public string good_name { get; set; }
        /// <summary>
        /// 货品单价，精确到小数点后两位（如果小数点后位数多于两位，则四舍五入保留两位小数）
        /// 非必填
        /// </summary>
        public string good_price { get; set; }
        /// <summary>
        /// 货品单位，最长不超过20个字符
        /// 非必填
        /// </summary>
        public string good_unit { get; set; }
    }

    /// <summary>
    /// 均非必填
    /// </summary>
    public class PreAddOrderOrderInfo
    {
        /// <summary>
        /// 配送服务代码 不同配送公司自定义, 顺丰和达达不填
        /// 非必填
        /// </summary>
        public string delivery_service_code { get; set; }
        /// <summary>
        /// 订单类型, 0: 即时单 1 预约单，如预约单，需要设置expected_delivery_time或expected_finish_time或expected_pick_time
        /// 非必填
        /// </summary>
        public int order_type { get; set; } = 0;
        /// <summary>
        /// 期望派单时间(美团、达达支持，美团表示商家发单时间，达达表示系统调度时间, 到那个时间才会有状态更新的回调通知)，unix-timestamp, 比如1586342180
        /// 非必填
        /// </summary>
        public long expected_delivery_time { get; set; } = 0;
        /// <summary>
        /// 期望送达时间(顺丰同城急送支持），unix-timestamp, 比如1586342180
        /// 非必填
        /// </summary>
        public long expected_finish_time { get; set; } = 0;
        /// <summary>
        /// 期望取件时间（闪送、顺丰同城急送支持，闪送需要设置两个小时后的时间，顺丰同城急送只需传expected_finish_time或expected_pick_time其中之一即可，同时都传则以expected_finish_time为准），unix-timestamp, 比如1586342180
        /// </summary>
        public long expected_pick_time { get; set; } = 0;
        /// <summary>
        /// 门店订单流水号，建议提供，方便骑手门店取货，最长不超过32个字符
        /// </summary>
        public string poi_seq { get; set; }
        /// <summary>
        /// 备注，最长不超过200个字符
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 用户下单付款时间, 比如1555220757
        /// </summary>
        public long order_time { get; set; }
        /// <summary>
        /// 是否保价，0，非保价，1.保价
        /// </summary>
        public int is_insured { get; set; } = 0;
        /// <summary>
        /// 保价金额，单位为元，精确到分
        /// </summary>
        public decimal declared_value { get; set; }
        /// <summary>
        /// 小费，单位为元, 下单一般不加小费
        /// </summary>
        public decimal tips { get; set; }
        /// <summary>
        /// 是否选择直拿直送（0：不需要；1：需要。选择直拿直送后，同一时间骑手只能配送此订单至完成，配送费用也相应高一些，闪送必须选1，达达可选0或1，其余配送公司不支持直拿直送）
        /// </summary>
        public int is_direct_delivery { get; set; }
        /// <summary>
        /// 骑手应付金额，单位为元，精确到分
        /// </summary>
        public decimal cash_on_delivery { get; set; }
        /// <summary>
        /// 骑手应收金额，单位为元，精确到分
        /// </summary>
        public decimal cash_on_pickup { get; set; }
        /// <summary>
        /// 物流流向，1：从门店取件送至用户；2：从用户取件送至门店
        /// </summary>
        public int rider_pick_method { get; set; }
        /// <summary>
        /// 收货码（0：不需要；1：需要。收货码的作用是：骑手必须输入收货码才能完成订单妥投）
        /// 非必填
        /// </summary>
        public int is_finish_code_needed { get; set; }
        /// <summary>
        /// 取货码（0：不需要；1：需要。取货码的作用是：骑手必须输入取货码才能从商家取货）
        /// 非必填
        /// </summary>
        public int is_pickup_code_needed { get; set; }
    }

    public class PreAddOrderShop
    {
        /// <summary>
        /// 商家小程序的路径，建议为订单页面
        /// </summary>
        public string wxa_path { get; set; }
        /// <summary>
        /// 商品缩略图 url
        /// </summary>
        public string img_url { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_name { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int goods_count { get; set; }
        /// <summary>
        /// 若结算方式为：第三方向配送公司统一结算，商户后续和第三方结算，则该参数必填；在该结算模式下，第三方用自己的开发小程序替授权商户发起下单，并将授权小程序的appid给平台，后续配送通知中可回流授权商户小程序
        /// </summary>
        public string wxa_appid { get; set; }
    }

}
