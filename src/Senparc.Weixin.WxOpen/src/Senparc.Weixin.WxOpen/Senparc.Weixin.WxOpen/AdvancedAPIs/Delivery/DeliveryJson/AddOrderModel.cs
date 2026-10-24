using Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson
{
    /// <summary>
    /// 生成运单请求参数
    /// </summary>
    public class AddOrderModel
    {
        /// <summary>
        /// 订单ID，须保证全局唯一，不超过512字节
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 用户openid，当add_source=2时无需填写（不发送物流服务通知）
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 快递公司ID，参见getAllDelivery
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 快递客户编码或者现付编码
        /// </summary>
        public string biz_id { get; set; }
        /// <summary>
        /// 快递备注信息，比如"易碎物品"，不超过1024字节
        /// </summary>
        public string custom_remark { get; set; }
        /// <summary>
        /// 订单标签id，用于平台型小程序区分平台上的入驻方，tagid须与入驻方账号一一对应，非平台型小程序无需填写该字段
        /// </summary>
        public int tagid { get; set; }
        /// <summary>
        /// 订单来源，0为小程序订单，2为App或H5订单，填2则不发送物流服务通知
        /// </summary>
        public int add_source { get; set; }
        /// <summary>
        /// App或H5的appid，add_source=2时必填，需和开通了物流助手的小程序绑定同一open帐号
        /// </summary>
        public string wx_appid { get; set; }
        /// <summary>
        /// 发件人信息
        /// </summary>
        public SenderOrReceiverModel sender { get; set; }
        /// <summary>
        /// 收件人信息
        /// </summary>
        public SenderOrReceiverModel receiver { get; set; }
        /// <summary>
        /// 包裹信息，将传递给快递公司
        /// </summary>
        public CargoModel cargo { get; set; }
        /// <summary>
        /// 商品信息，会展示到物流服务通知和电子面单中
        /// </summary>
        public ShopModel shop { get; set; }
        /// <summary>
        /// 保价信息
        /// </summary>
        public InsuredModel insured { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public ServiceType service { get; set; }
        /// <summary>
        /// Unix 时间戳, 单位秒，顺丰必须传。 预期的上门揽件时间，0表示已事先约定取件时间；否则请传预期揽件时间戳，需大于当前时间，收件员会在预期时间附近上门。例如expect_time为“1557989929”，表示希望收件员将在2019年05月16日14:58:49-15:58:49内上门取货。说明：若选择 了预期揽件时间，请不要自己打单，由上门揽件的时候打印。如果是下顺丰散单，则必传此字段，否则不会有收件员上门揽件。
        /// </summary>
        public long expect_time { get; set; }
        /// <summary>
        /// 分单策略，【0：线下网点签约，1：总部签约结算】，不传默认线下网点签约。目前支持圆通。
        /// </summary>
        public int take_mode { get; set; }
    }
    /// <summary>
    /// 发件人(收件人）信息
    /// </summary>
    public class SenderOrReceiverModel
    {
        /// <summary>
        /// 发件人(收件人）姓名，不超过64字节
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 发件人(收件人）座机号码，若不填写则必须填写 mobile，不超过32字节
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 发件人(收件人）手机号码，若不填写则必须填写 tel，不超过32字节
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 发件人(收件人）公司名称，不超过64字节
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 发件人(收件人）邮编，不超过10字节
        /// </summary>
        public string post_code { get; set; }
        /// <summary>
        /// 发件人(收件人）国家，不超过64字节
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 发件人(收件人）省份，比如："广东省"，不超过64字节
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 发件人(收件人）市/地区，比如："广州市"，不超过64字节
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 发件人(收件人）区/县，比如："海珠区"，不超过64字节
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 发件人(收件人）详细地址，比如："XX路XX号XX大厦XX"，不超过512字节
        /// </summary>
        public string address { get; set; }
    }
    /// <summary>
    /// 包裹信息，将传递给快递公司
    /// </summary>
    public class CargoModel
    {
        /// <summary>
        /// 包裹数量, 默认为1
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 包裹总重量，单位是千克(kg)
        /// </summary>
        public decimal weight { get; set; }
        /// <summary>
        /// 包裹体积（长）
        /// </summary>
        public decimal space_x { get; set; }
        /// <summary>
        /// 包裹体积（宽）
        /// </summary>
        public decimal space_y { get; set;}
        /// <summary>
        /// 包裹体积（高）
        /// </summary>
        public decimal space_z { get; set; }
        /// <summary>
        /// 商品详情列表
        /// </summary>
        public List<DetailListModel> detail_list { get; set; }
    }
    /// <summary>
    /// 商品信息，会展示到物流服务通知和电子面单中
    /// </summary>
    public class ShopModel
    {
        /// <summary>
        /// 商家小程序的路径，建议为订单页面
        /// </summary>
        public string wxa_path { get; set; }
        /// <summary>
        /// 商品缩略图 url；shop.detail_list为空则必传，shop.detail_list非空可不传。
        /// </summary>
        public string img_url { get; set; }
        /// <summary>
        /// 商品名称, 不超过128字节；shop.detail_list为空则必传，shop.detail_list非空可不传。
        /// </summary>
        public string goods_name { get; set; }
        /// <summary>
        /// 商品数量；shop.detail_list为空则必传。shop.detail_list非空可不传，默认取shop.detail_list的size
        /// </summary>
        public int goods_count { get; set; }
        /// <summary>
        /// 商品详情列表，适配多商品场景，用以消息落地页展示。（新规范，新接入商家建议用此字段）
        /// </summary>
        ///public List<DetailListModel> detail_list { get; set; }
    }
    /// <summary>
    /// 保价信息
    /// </summary>
    public class InsuredModel
    {
        /// <summary>
        /// 是否保价，0 表示不保价，1 表示保价
        /// </summary>
        public int use_insured { get; set; }
        /// <summary>
        /// 保价金额，单位是分，比如: 10000 表示 100 元
        /// </summary>
        public int insured_value { get; set; }
    }
    /// <summary>
    /// 商品详情
    /// </summary>
    public class DetailListModel
    {
        /// <summary>
        /// 商品名，不超过128字节
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int count { get; set; }
    }
}
