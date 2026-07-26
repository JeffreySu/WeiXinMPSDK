#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ImmediateDeliveryJson.cs
    文件功能描述：ImmediateDeliveryJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.ImmediateDelivery
{
    /// <summary>
    /// 即时配送接口通用返回结果。
    /// </summary>
    /// <remarks>
    /// 配送公司业务错误通过 <see cref="resultcode"/> 和 <see cref="resultmsg"/> 返回；
    /// 微信平台错误仍通过基类的 errcode 和 errmsg 返回。
    /// </remarks>
    public class ImmediateDeliveryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 配送公司返回的错误码，0 表示成功。
        /// </summary>
        public int resultcode { get; set; }

        /// <summary>
        /// 配送公司返回的错误描述。
        /// </summary>
        public string resultmsg { get; set; }
    }

    /// <summary>
    /// 即时配送下单、预下单和重新下单的公共请求字段。
    /// </summary>
    public abstract class ImmediateDeliveryOrderRequestBase
    {
        /// <summary>
        /// 商家 ID，由配送公司分配，通常为配送公司开放平台的 AppKey。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户生成的订单唯一标识，最长 128 字节。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 配送公司 ID。
        /// </summary>
        public string delivery_id { get; set; }

        /// <summary>
        /// 下单用户的 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 发件人信息。部分配送公司在已填写门店编号时允许省略。
        /// </summary>
        public ImmediateDeliveryContact sender { get; set; }

        /// <summary>
        /// 收件人信息。
        /// </summary>
        public ImmediateDeliveryContact receiver { get; set; }

        /// <summary>
        /// 货物信息。
        /// </summary>
        public ImmediateDeliveryCargo cargo { get; set; }

        /// <summary>
        /// 配送订单信息。
        /// </summary>
        public ImmediateDeliveryOrderInfo order_info { get; set; }

        /// <summary>
        /// 展示在物流通知中的商品信息。
        /// </summary>
        public ImmediateDeliveryShop shop { get; set; }

        /// <summary>
        /// 使用配送公司 AppSecret 计算的 SHA1 校验串。
        /// </summary>
        public string delivery_sign { get; set; }

        /// <summary>
        /// 商家门店编号；是否必填取决于配送公司和门店配置。
        /// </summary>
        public string shop_no { get; set; }

        /// <summary>
        /// 子商户 ID，用于区分小程序内部的多个子商户。
        /// </summary>
        public string sub_biz_id { get; set; }
    }

    /// <summary>
    /// 预下即时配送单请求。
    /// </summary>
    public class ImmediateDeliveryPreAddOrderRequest : ImmediateDeliveryOrderRequestBase
    {
    }

    /// <summary>
    /// 添加或重新添加即时配送单请求。
    /// </summary>
    public class ImmediateDeliveryAddOrderRequest : ImmediateDeliveryOrderRequestBase
    {
        /// <summary>
        /// 预下单接口返回的配送令牌，用于在有效期内锁定运费。
        /// </summary>
        public string delivery_token { get; set; }
    }

    /// <summary>
    /// 即时配送联系人及地址信息。
    /// </summary>
    public class ImmediateDeliveryContact
    {
        /// <summary>
        /// 联系人姓名，最长 256 个字符。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 城市名称，例如“广州市”。
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 用于定位的街道、小区或大厦地址。
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 楼号、单元号、层号等地址详情。
        /// </summary>
        public string address_detail { get; set; }

        /// <summary>
        /// 电话或手机号码，最长 64 个字符。
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 经度，最多保留小数点后 6 位。
        /// </summary>
        public decimal lng { get; set; }

        /// <summary>
        /// 纬度，最多保留小数点后 6 位。
        /// </summary>
        public decimal lat { get; set; }

        /// <summary>
        /// 坐标类型：0 火星坐标，1 百度坐标；不填写时由接口按默认值处理。
        /// </summary>
        public int? coordinate_type { get; set; }
    }

    /// <summary>
    /// 即时配送货物信息。
    /// </summary>
    public class ImmediateDeliveryCargo
    {
        /// <summary>
        /// 货物价格，单位为元，范围为 (0, 5000]。
        /// </summary>
        public decimal goods_value { get; set; }

        /// <summary>
        /// 货物高度，单位为厘米，范围为 (0, 45]。
        /// </summary>
        public decimal? goods_height { get; set; }

        /// <summary>
        /// 货物宽度，单位为厘米，范围为 (0, 50]。
        /// </summary>
        public decimal? goods_width { get; set; }

        /// <summary>
        /// 货物长度，单位为厘米，范围为 (0, 65]。
        /// </summary>
        public decimal? goods_length { get; set; }

        /// <summary>
        /// 货物重量，单位为千克，范围为 (0, 50]。
        /// </summary>
        public decimal goods_weight { get; set; }

        /// <summary>
        /// 货物明细。
        /// </summary>
        public ImmediateDeliveryGoodsDetail goods_detail { get; set; }

        /// <summary>
        /// 骑手到店取货时使用的取货说明，最长 100 个字符。
        /// </summary>
        public string goods_pickup_info { get; set; }

        /// <summary>
        /// 货物一级品类，例如“美食夜宵”“日用百货”。
        /// </summary>
        public string cargo_first_class { get; set; }

        /// <summary>
        /// 货物二级品类。
        /// </summary>
        public string cargo_second_class { get; set; }
    }

    /// <summary>
    /// 即时配送货物明细。
    /// </summary>
    public class ImmediateDeliveryGoodsDetail
    {
        /// <summary>
        /// 货物列表。
        /// </summary>
        public IList<ImmediateDeliveryGoodsItem> goods { get; set; }
    }

    /// <summary>
    /// 即时配送单项货物。
    /// </summary>
    public class ImmediateDeliveryGoodsItem
    {
        /// <summary>
        /// 货物数量。
        /// </summary>
        public int good_count { get; set; }

        /// <summary>
        /// 货物名称。
        /// </summary>
        public string good_name { get; set; }

        /// <summary>
        /// 货物单价，单位为元，最多保留两位小数。
        /// </summary>
        public decimal? good_price { get; set; }

        /// <summary>
        /// 货物单位，最长 20 个字符。
        /// </summary>
        public string good_unit { get; set; }
    }

    /// <summary>
    /// 即时配送订单配置。
    /// </summary>
    public class ImmediateDeliveryOrderInfo
    {
        /// <summary>
        /// 配送公司定义的配送服务代码。
        /// </summary>
        public string delivery_service_code { get; set; }

        /// <summary>
        /// 期望派单时间，Unix 秒级时间戳。
        /// </summary>
        public long? expected_delivery_time { get; set; }

        /// <summary>
        /// 门店订单流水号，最长 32 个字符。
        /// </summary>
        public string poi_seq { get; set; }

        /// <summary>
        /// 订单备注，最长 200 个字符。
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// 用户下单付款时间，Unix 秒级时间戳。
        /// </summary>
        public long? order_time { get; set; }

        /// <summary>
        /// 是否保价：0 不保价，1 保价。
        /// </summary>
        public int? is_insured { get; set; }

        /// <summary>
        /// 保价金额，单位为元。
        /// </summary>
        public decimal? declared_value { get; set; }

        /// <summary>
        /// 小费，单位为元。
        /// </summary>
        public decimal? tips { get; set; }

        /// <summary>
        /// 是否直拿直送：0 不需要，1 需要。
        /// </summary>
        public int? is_direct_delivery { get; set; }

        /// <summary>
        /// 骑手应付金额，单位为元。
        /// </summary>
        public decimal? cash_on_delivery { get; set; }

        /// <summary>
        /// 骑手应收金额，单位为元。
        /// </summary>
        public decimal? cash_on_pickup { get; set; }

        /// <summary>
        /// 物流流向：1 门店到用户，2 用户到门店。
        /// </summary>
        public int? rider_pick_method { get; set; }

        /// <summary>
        /// 是否需要收货码：0 不需要，1 需要。
        /// </summary>
        public int? is_finish_code_needed { get; set; }

        /// <summary>
        /// 是否需要取货码：0 不需要，1 需要。
        /// </summary>
        public int? is_pickup_code_needed { get; set; }

        /// <summary>
        /// 期望送达时间，Unix 秒级时间戳。
        /// </summary>
        public long? expected_finish_time { get; set; }

        /// <summary>
        /// 期望取件时间，Unix 秒级时间戳。
        /// </summary>
        public long? expected_pick_time { get; set; }

        /// <summary>
        /// 订单类型：0 即时单，1 预约单。
        /// </summary>
        public int? order_type { get; set; }
    }

    /// <summary>
    /// 即时配送通知中展示的商品信息。
    /// </summary>
    public class ImmediateDeliveryShop
    {
        /// <summary>
        /// 商家小程序路径，建议指向订单页面。
        /// </summary>
        public string wxa_path { get; set; }

        /// <summary>
        /// 商品缩略图 URL。
        /// </summary>
        public string img_url { get; set; }

        /// <summary>
        /// 商品名称，最长 128 字节。
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 商品数量。
        /// </summary>
        public int? goods_count { get; set; }

        /// <summary>
        /// 授权商户小程序 AppId；第三方统一结算模式下用于让配送通知回流授权小程序。
        /// </summary>
        public string wxa_appid { get; set; }

        /// <summary>
        /// 商品明细扩展。官方参数说明引用此字段，但当前参数表未公布明细结构，调用方可按配送公司约定传入对象数组。
        /// </summary>
        public IList<object> detail_list { get; set; }
    }

    /// <summary>
    /// 绑定即时配送公司账号请求。
    /// </summary>
    public class ImmediateDeliveryBindAccountRequest
    {
        /// <summary>
        /// 配送公司 ID。
        /// </summary>
        public string delivery_id { get; set; }
    }

    /// <summary>
    /// 预取消即时配送单请求。
    /// </summary>
    public class ImmediateDeliveryPreCancelOrderRequest
    {
        /// <summary>
        /// 商家 ID。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户订单 ID。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 配送公司 ID。
        /// </summary>
        public string delivery_id { get; set; }

        /// <summary>
        /// 配送单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 取消原因 ID。
        /// </summary>
        public int? cancel_reason_id { get; set; }

        /// <summary>
        /// 取消原因说明。
        /// </summary>
        public string cancel_reason { get; set; }

        /// <summary>
        /// 商家门店编号。
        /// </summary>
        public string shop_no { get; set; }

        /// <summary>
        /// 配送公司签名。
        /// </summary>
        public string delivery_sign { get; set; }
    }

    /// <summary>
    /// 真实测试环境模拟更新配送单状态请求。
    /// </summary>
    public class ImmediateDeliveryRealMockUpdateOrderRequest
    {
        /// <summary>
        /// 商家 ID。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户订单 ID。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 配送状态，例如 101 等待分配骑手、302 配送成功。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 状态变更时间，Unix 秒级时间戳。
        /// </summary>
        public long action_time { get; set; }

        /// <summary>
        /// 状态附加信息。
        /// </summary>
        public string action_msg { get; set; }

        /// <summary>
        /// 配送公司签名。
        /// </summary>
        public string delivery_sign { get; set; }
    }

    /// <summary>
    /// 沙盒环境模拟配送公司更新状态请求。
    /// </summary>
    public class ImmediateDeliveryMockUpdateOrderRequest
    {
        /// <summary>
        /// 测试商家 ID，官方要求固定为 test_shop_id。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户订单 ID。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 配送单 ID。官方请求示例包含此字段，但当前参数表未单独列出。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 配送状态。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 状态变更时间，Unix 秒级时间戳。
        /// </summary>
        public long action_time { get; set; }

        /// <summary>
        /// 状态附加信息。
        /// </summary>
        public string action_msg { get; set; }
    }

    /// <summary>
    /// 拉取即时配送单请求。
    /// </summary>
    public class ImmediateDeliveryGetOrderRequest
    {
        /// <summary>
        /// 商家 ID。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户订单 ID。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 商家门店编号；仅有一个门店时官方说明允许不填。
        /// </summary>
        public string shop_no { get; set; }

        /// <summary>
        /// 配送公司签名。
        /// </summary>
        public string delivery_sign { get; set; }
    }

    /// <summary>
    /// 确认异常件已退回商家请求。
    /// </summary>
    public class ImmediateDeliveryConfirmReturnRequest
    {
        /// <summary>
        /// 商家 ID。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户订单 ID。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 配送单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 配送公司签名。
        /// </summary>
        public string delivery_sign { get; set; }

        /// <summary>
        /// 商家门店编号。
        /// </summary>
        public string shop_no { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public string remark { get; set; }
    }

    /// <summary>
    /// 取消即时配送单请求。
    /// </summary>
    public class ImmediateDeliveryCancelOrderRequest
    {
        /// <summary>
        /// 商家 ID。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户订单 ID。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 配送公司 ID。
        /// </summary>
        public string delivery_id { get; set; }

        /// <summary>
        /// 配送单 ID，顺丰同城必填。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 取消原因 ID：1 暂不需要、2 价格不合适、3 信息有误、4 取货不及时、5 配送不及时、6 其他。
        /// </summary>
        public int cancel_reason_id { get; set; }

        /// <summary>
        /// 取消原因；原因 ID 为 6 时必须填写。
        /// </summary>
        public string cancel_reason { get; set; }

        /// <summary>
        /// 商家门店编号。
        /// </summary>
        public string shop_no { get; set; }

        /// <summary>
        /// 配送公司签名。
        /// </summary>
        public string delivery_sign { get; set; }
    }

    /// <summary>
    /// 为即时配送单添加小费请求。
    /// </summary>
    public class ImmediateDeliveryAddTipsRequest
    {
        /// <summary>
        /// 商家 ID。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户订单 ID。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 配送单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 小费金额，单位为元。
        /// </summary>
        public decimal tips { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 配送公司签名。
        /// </summary>
        public string delivery_sign { get; set; }

        /// <summary>
        /// 商家门店编号。
        /// </summary>
        public string shop_no { get; set; }
    }

    /// <summary>
    /// 运力方更新即时配送单状态请求。
    /// </summary>
    public class ImmediateDeliveryProviderUpdateOrderRequest
    {
        /// <summary>
        /// 下单事件中微信推送的 Token。
        /// </summary>
        public string wx_token { get; set; }

        /// <summary>
        /// 订单状态，例如 101 等待分配骑手、102 分配骑手成功、302 配送成功。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 配送单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 状态附加信息。
        /// </summary>
        public string action_msg { get; set; }

        /// <summary>
        /// 状态变更时间，Unix 秒级时间戳。
        /// </summary>
        public long action_time { get; set; }

        /// <summary>
        /// 骑手信息；分配骑手成功时需要填写。
        /// </summary>
        public ImmediateDeliveryAgent agent { get; set; }

        /// <summary>
        /// 商家 ID，可为配送公司分配的 DevId 或 AppKey。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 商户订单 ID。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 商家门店编号。
        /// </summary>
        public string shop_no { get; set; }

        /// <summary>
        /// 配送公司小程序跳转路径，用于用户从服务通知进入配送页面。
        /// </summary>
        public string wxa_path { get; set; }

        /// <summary>
        /// 预计送达时间，Unix 秒级时间戳；骑手接单时填写。
        /// </summary>
        public long? expected_delivery_time { get; set; }
    }

    /// <summary>
    /// 即时配送骑手信息。
    /// </summary>
    public class ImmediateDeliveryAgent
    {
        /// <summary>
        /// 骑手姓名。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 骑手电话。
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 电话是否加密：0 不加密，1 加密。
        /// </summary>
        public int? is_phone_encrypted { get; set; }

        /// <summary>
        /// 骑手经度。官方请求示例包含该字段，当前参数表未单独列出。
        /// </summary>
        public decimal? lng { get; set; }

        /// <summary>
        /// 骑手纬度。官方请求示例包含该字段，当前参数表未单独列出。
        /// </summary>
        public decimal? lat { get; set; }
    }

    /// <summary>
    /// 已支持的即时配送公司。
    /// </summary>
    public class ImmediateDeliveryCompany
    {
        /// <summary>
        /// 配送公司 ID。
        /// </summary>
        public string delivery_id { get; set; }

        /// <summary>
        /// 配送公司名称。
        /// </summary>
        public string delivery_name { get; set; }
    }

    /// <summary>
    /// 获取即时配送公司列表结果。
    /// </summary>
    public class ImmediateDeliveryCompanyListJsonResult : ImmediateDeliveryJsonResult
    {
        /// <summary>
        /// 配送公司列表。
        /// </summary>
        public IList<ImmediateDeliveryCompany> list { get; set; }
    }

    /// <summary>
    /// 已绑定的即时配送账号。
    /// </summary>
    public class ImmediateDeliveryBoundAccount
    {
        /// <summary>
        /// 商家 ID。
        /// </summary>
        public string shopid { get; set; }

        /// <summary>
        /// 配送公司 ID。
        /// </summary>
        public string delivery_id { get; set; }

        /// <summary>
        /// 审核状态：0 已通过，1 审核中，2 未通过。
        /// </summary>
        public int audit_result { get; set; }
    }

    /// <summary>
    /// 获取已绑定即时配送账号结果。
    /// </summary>
    public class ImmediateDeliveryBoundAccountListJsonResult : ImmediateDeliveryJsonResult
    {
        /// <summary>
        /// 已绑定商家账号列表。
        /// </summary>
        public IList<ImmediateDeliveryBoundAccount> shop_list { get; set; }
    }

    /// <summary>
    /// 预下即时配送单结果。
    /// </summary>
    public class ImmediateDeliveryPreAddOrderJsonResult : ImmediateDeliveryJsonResult
    {
        /// <summary>
        /// 实际运费，单位为元，等于运费减去优惠券金额。
        /// </summary>
        public decimal fee { get; set; }

        /// <summary>
        /// 运费，单位为元。
        /// </summary>
        public decimal deliverfee { get; set; }

        /// <summary>
        /// 优惠券金额，单位为元。官方参数表写作 couponFee，返回示例写作 couponfee；JSON 反序列化按名称大小写兼容。
        /// </summary>
        public decimal couponfee { get; set; }

        /// <summary>
        /// 小费，单位为元。
        /// </summary>
        public decimal tips { get; set; }

        /// <summary>
        /// 保价费，单位为元，对应官方参数表中的 insurancefee。
        /// </summary>
        [JsonProperty("insurancefee")]
        public decimal? insurancefee { get; set; }

        /// <summary>
        /// 官方当前返回示例将 insurancefee 拼写为 insurancfee；保留该别名以兼容实际返回。
        /// </summary>
        [JsonProperty("insurancfee")]
        public decimal? insurancfee { get; set; }

        /// <summary>
        /// 配送距离，单位为米。
        /// </summary>
        public int distance { get; set; }

        /// <summary>
        /// 预下单配送令牌。
        /// </summary>
        public string delivery_token { get; set; }

        /// <summary>
        /// 预计骑手接单时间，单位为秒；无法预计时为 0。
        /// </summary>
        public int dispatch_duration { get; set; }
    }

    /// <summary>
    /// 添加或重新添加即时配送单结果。
    /// </summary>
    public class ImmediateDeliveryAddOrderJsonResult : ImmediateDeliveryPreAddOrderJsonResult
    {
        /// <summary>
        /// 配送单号。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 配送状态。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 收货码。
        /// </summary>
        public int? finish_code { get; set; }

        /// <summary>
        /// 取货码。
        /// </summary>
        public int? pickup_code { get; set; }
    }

    /// <summary>
    /// 预取消或取消即时配送单结果。
    /// </summary>
    public class ImmediateDeliveryCancelOrderJsonResult : ImmediateDeliveryJsonResult
    {
        /// <summary>
        /// 预计或实际扣除的违约金，单位为元。
        /// </summary>
        public decimal deduct_fee { get; set; }

        /// <summary>
        /// 取消结果说明。
        /// </summary>
        public string desc { get; set; }
    }

    /// <summary>
    /// 拉取即时配送单结果。
    /// </summary>
    public class ImmediateDeliveryGetOrderJsonResult : ImmediateDeliveryJsonResult
    {
        /// <summary>
        /// 配送状态。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 配送单号。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 骑手姓名。
        /// </summary>
        public string rider_name { get; set; }

        /// <summary>
        /// 骑手电话。
        /// </summary>
        public string rider_phone { get; set; }

        /// <summary>
        /// 骑手当前位置经度，配送中时返回。
        /// </summary>
        public decimal? rider_lng { get; set; }

        /// <summary>
        /// 骑手当前位置纬度，配送中时返回。
        /// </summary>
        public decimal? rider_lat { get; set; }

        /// <summary>
        /// 预计剩余送达时间，单位为秒。
        /// </summary>
        public int? reach_time { get; set; }
    }
}
