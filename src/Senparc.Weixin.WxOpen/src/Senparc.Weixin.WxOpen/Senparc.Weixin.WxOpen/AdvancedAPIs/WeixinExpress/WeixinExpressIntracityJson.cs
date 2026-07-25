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

    文件名：WeixinExpressIntracityJson.cs
    文件功能描述：WeixinExpressIntracityJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 创建同城配送门店请求。
    /// </summary>
    public class WeixinExpressIntracityCreateStoreRequest
    {
        /// <summary>
        /// 商家自定义门店编号。
        /// </summary>
        public string out_store_id { get; set; }

        /// <summary>
        /// 门店名称。
        /// </summary>
        public string store_name { get; set; }

        /// <summary>
        /// 运力偏好：1 价格优先，2 运力优先；不填写时默认为价格优先。
        /// </summary>
        public int? order_pattern { get; set; }

        /// <summary>
        /// 优先使用的运力 ID；<see cref="order_pattern"/> 为 2 时必填，当前支持 DADA、SFTC。
        /// </summary>
        public string service_trans_prefer { get; set; }

        /// <summary>
        /// 门店发货地址。
        /// </summary>
        public WeixinExpressIntracityAddressInfo address_info { get; set; }
    }

    /// <summary>
    /// 同城配送门店地址。
    /// </summary>
    public class WeixinExpressIntracityAddressInfo
    {
        /// <summary>
        /// 省、自治区或直辖市名称。
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 地级市名称。
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 县、县级市或区名称。
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 街道名称。
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// 具体门牌号或详细地址。
        /// </summary>
        public string house { get; set; }

        /// <summary>
        /// 门店纬度。
        /// </summary>
        public decimal lat { get; set; }

        /// <summary>
        /// 门店经度。
        /// </summary>
        public decimal lng { get; set; }

        /// <summary>
        /// 门店联系电话，可填写 11 位手机号或带区号的固话。
        /// </summary>
        public string phone { get; set; }
    }

    /// <summary>
    /// 创建同城配送门店结果。
    /// </summary>
    public class WeixinExpressIntracityCreateStoreJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 门店所属小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商家自定义门店编号。
        /// </summary>
        public string out_store_id { get; set; }
    }

    /// <summary>
    /// 查询同城配送门店请求；不填写门店编号时返回全部门店。
    /// </summary>
    public class WeixinExpressIntracityQueryStoreRequest
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 商家自定义门店编号。
        /// </summary>
        public string out_store_id { get; set; }
    }

    /// <summary>
    /// 同城配送门店信息。
    /// </summary>
    public class WeixinExpressIntracityStoreInfo
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 商家自定义门店编号。
        /// </summary>
        public string out_store_id { get; set; }

        /// <summary>
        /// 门店名称；当前官方返回参数表未列出该字段，保留以兼容实际返回。
        /// </summary>
        public string store_name { get; set; }

        /// <summary>
        /// 门店所在城市行政区划代码。官方参数表标为字符串，示例返回数字，使用字符串兼容两种表示。
        /// </summary>
        public string city_id { get; set; }

        /// <summary>
        /// 运力偏好：1 价格优先，2 运力优先。
        /// </summary>
        public int order_pattern { get; set; }

        /// <summary>
        /// 优先使用的运力 ID。
        /// </summary>
        public string service_trans_prefer { get; set; }

        /// <summary>
        /// 门店地址。
        /// </summary>
        public WeixinExpressIntracityAddressInfo address_info { get; set; }
    }

    /// <summary>
    /// 查询同城配送门店结果。
    /// </summary>
    public class WeixinExpressIntracityQueryStoreJsonResult : WxJsonResult
    {
        /// <summary>
        /// 门店列表。
        /// </summary>
        public IList<WeixinExpressIntracityStoreInfo> store_list { get; set; }

        /// <summary>
        /// 符合条件的门店总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 门店所属小程序 AppId。
        /// </summary>
        public string appid { get; set; }
    }

    /// <summary>
    /// 更新同城配送门店时使用的门店定位条件。
    /// </summary>
    public class WeixinExpressIntracityStoreKey
    {
        /// <summary>
        /// 微信门店编号，与 <see cref="out_store_id"/> 二选一。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 商家自定义门店编号，与 <see cref="wx_store_id"/> 二选一。
        /// </summary>
        public string out_store_id { get; set; }
    }

    /// <summary>
    /// 同城配送门店更新内容。
    /// </summary>
    public class WeixinExpressIntracityStoreUpdateContent
    {
        /// <summary>
        /// 新门店名称。
        /// </summary>
        public string store_name { get; set; }

        /// <summary>
        /// 运力偏好：1 价格优先，2 运力优先。官方参数表写为 string，示例实际发送数字。
        /// </summary>
        public int? order_pattern { get; set; }

        /// <summary>
        /// 优先使用的运力 ID；<see cref="order_pattern"/> 为 2 时必填。
        /// </summary>
        public string service_trans_prefer { get; set; }

        /// <summary>
        /// 新门店地址。
        /// </summary>
        public WeixinExpressIntracityAddressInfo address_info { get; set; }
    }

    /// <summary>
    /// 更新同城配送门店请求。
    /// </summary>
    public class WeixinExpressIntracityUpdateStoreRequest
    {
        /// <summary>
        /// 要更新的门店定位条件。
        /// </summary>
        public WeixinExpressIntracityStoreKey keys { get; set; }

        /// <summary>
        /// 门店更新内容。
        /// </summary>
        public WeixinExpressIntracityStoreUpdateContent content { get; set; }
    }

    /// <summary>
    /// 同城配送门店充值请求。
    /// </summary>
    public class WeixinExpressIntracityStoreChargeRequest
    {
        /// <summary>
        /// 微信门店编号；门店扣费或未指定 <see cref="pay_mode"/> 时必填。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 运力 ID，当前支持 DADA、SFTC。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 充值金额，单位为分，最低 5000 分。
        /// </summary>
        public long amount { get; set; }

        /// <summary>
        /// 充值主体：PAY_MODE_STORE、PAY_MODE_APP 或 PAY_MODE_COMPONENT；不填时默认为门店。
        /// </summary>
        public string pay_mode { get; set; }
    }

    /// <summary>
    /// 同城配送门店充值结果。
    /// </summary>
    public class WeixinExpressIntracityStoreChargeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 充值页面地址。
        /// </summary>
        public string payurl { get; set; }

        /// <summary>
        /// 门店所属小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }
    }

    /// <summary>
    /// 同城配送门店余额退款请求。
    /// </summary>
    public class WeixinExpressIntracityStoreRefundRequest
    {
        /// <summary>
        /// 微信门店编号；门店扣费或未指定 <see cref="pay_mode"/> 时必填。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 充值或扣费主体：PAY_MODE_STORE、PAY_MODE_APP 或 PAY_MODE_COMPONENT。
        /// </summary>
        public string pay_mode { get; set; }

        /// <summary>
        /// 运力 ID。
        /// </summary>
        public string service_trans_id { get; set; }
    }

    /// <summary>
    /// 同城配送门店余额退款结果。
    /// </summary>
    public class WeixinExpressIntracityStoreRefundJsonResult : WxJsonResult
    {
        /// <summary>
        /// 门店所属小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 退款金额，单位为分。
        /// </summary>
        public long refund_amount { get; set; }
    }

    /// <summary>
    /// 查询同城配送资金流水请求。
    /// </summary>
    public class WeixinExpressIntracityQueryFlowRequest
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 流水类型：1 充值，2 消费，3 退款。
        /// </summary>
        public int flow_type { get; set; }

        /// <summary>
        /// 运力 ID；不填写时查询全部运力。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 查询开始时间，Unix 秒级时间戳；不填写时默认查询最近 90 天。
        /// </summary>
        public long? begin_time { get; set; }

        /// <summary>
        /// 查询结束时间，Unix 秒级时间戳；不填写时默认查询最近 90 天。
        /// </summary>
        public long? end_time { get; set; }

        /// <summary>
        /// 扣费主体：PAY_MODE_STORE、PAY_MODE_APP 或 PAY_MODE_COMPONENT。
        /// </summary>
        public string pay_mode { get; set; }
    }

    /// <summary>
    /// 同城配送资金流水。
    /// </summary>
    public class WeixinExpressIntracityFlow
    {
        /// <summary>
        /// 流水类型：1 充值，2 消费，3 退款。
        /// </summary>
        public int flow_type { get; set; }

        /// <summary>
        /// 小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 充值订单号。使用字符串保存，避免 64 位数值在部分调用方发生精度损失。
        /// </summary>
        public string pay_order_id { get; set; }

        /// <summary>
        /// 运力 ID。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 支付金额，单位为分。
        /// </summary>
        public long pay_amount { get; set; }

        /// <summary>
        /// 支付时间，Unix 秒级时间戳。
        /// </summary>
        public long pay_time { get; set; }

        /// <summary>
        /// 支付状态，例如 FAIL、SUCCESS。
        /// </summary>
        public string pay_status { get; set; }

        /// <summary>
        /// 订单创建时间，Unix 秒级时间戳。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 余额有效截止时间，Unix 秒级时间戳。
        /// </summary>
        public long consume_deadline { get; set; }

        /// <summary>
        /// 退款时间，Unix 秒级时间戳。
        /// </summary>
        public long refund_time { get; set; }

        /// <summary>
        /// 退款金额，单位为分。官方说明中的“时间戳类型”为明显笔误。
        /// </summary>
        public long refund_amount { get; set; }

        /// <summary>
        /// 下单用户 OpenId，消费流水返回。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 配送单状态，消费流水返回。
        /// </summary>
        public int delivery_status { get; set; }

        /// <summary>
        /// 退款状态，例如 PROCESSING、SUCCESS。
        /// </summary>
        public string refund_status { get; set; }

        /// <summary>
        /// 取消配送产生的违约金，单位为分。
        /// </summary>
        public long deduct_amount { get; set; }

        /// <summary>
        /// 运力公司的配送单 ID。
        /// </summary>
        public string bill_id { get; set; }

        /// <summary>
        /// 配送完成时间，Unix 秒级时间戳。
        /// </summary>
        public long delivery_finished_time { get; set; }
    }

    /// <summary>
    /// 查询同城配送资金流水结果。
    /// </summary>
    public class WeixinExpressIntracityQueryFlowJsonResult : WxJsonResult
    {
        /// <summary>
        /// 流水总数。当前官方参数表漏列该字段，但返回示例包含。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 流水列表。
        /// </summary>
        public IList<WeixinExpressIntracityFlow> flow_list { get; set; }

        /// <summary>
        /// 总支付金额，单位为分。
        /// </summary>
        public long total_pay_amt { get; set; }

        /// <summary>
        /// 总退款金额，单位为分。
        /// </summary>
        public long total_refund_amt { get; set; }

        /// <summary>
        /// 总违约金，单位为分，消费流水返回。
        /// </summary>
        public long total_deduct_amt { get; set; }
    }

    /// <summary>
    /// 查询同城配送余额请求。
    /// </summary>
    public class WeixinExpressIntracityBalanceQueryRequest
    {
        /// <summary>
        /// 微信门店编号；门店扣费或未指定 <see cref="pay_mode"/> 时必填。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 运力 ID；不填写时查询全部运力。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 充值或扣费主体：PAY_MODE_STORE、PAY_MODE_APP 或 PAY_MODE_COMPONENT。
        /// </summary>
        public string pay_mode { get; set; }
    }

    /// <summary>
    /// 同城配送余额明细。
    /// </summary>
    public class WeixinExpressIntracityBalanceDetail
    {
        /// <summary>
        /// 当前余额，单位为分。
        /// </summary>
        public long balance { get; set; }

        /// <summary>
        /// 运力 ID。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 运力名称。
        /// </summary>
        public string service_trans_name { get; set; }

        /// <summary>
        /// 当前生效且尚未完全消费的充值订单。官方参数表写为 object，实际示例返回数组。
        /// </summary>
        public IList<WeixinExpressIntracityBalanceOrder> order_list { get; set; }
    }

    /// <summary>
    /// 同城配送余额对应的充值订单。
    /// </summary>
    public class WeixinExpressIntracityBalanceOrder
    {
        /// <summary>
        /// 充值订单号，使用字符串避免 64 位整数精度损失。
        /// </summary>
        public string payorder_id { get; set; }

        /// <summary>
        /// 充值金额，单位为分。
        /// </summary>
        public long charge_amt { get; set; }

        /// <summary>
        /// 未使用余额，单位为分。
        /// </summary>
        public long unused_amt { get; set; }

        /// <summary>
        /// 充值生效时间，Unix 秒级时间戳。
        /// </summary>
        public long begin_time { get; set; }

        /// <summary>
        /// 失效时间，Unix 秒级时间戳；到期未使用余额将退款。
        /// </summary>
        public long end_time { get; set; }
    }

    /// <summary>
    /// 查询同城配送余额结果。
    /// </summary>
    public class WeixinExpressIntracityBalanceQueryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 总余额，单位为分。
        /// </summary>
        public long all_balance { get; set; }

        /// <summary>
        /// 分运力余额明细。官方参数表写为 object，实际示例返回数组。
        /// </summary>
        public IList<WeixinExpressIntracityBalanceDetail> balance_detail { get; set; }
    }

    /// <summary>
    /// 同城配送商品信息。
    /// </summary>
    public class WeixinExpressIntracityCargo
    {
        /// <summary>
        /// 商品名称。
        /// </summary>
        public string cargo_name { get; set; }

        /// <summary>
        /// 商品总重量，单位为克。
        /// </summary>
        public decimal cargo_weight { get; set; }

        /// <summary>
        /// 商品总价格，单位为分。
        /// </summary>
        public long cargo_price { get; set; }

        /// <summary>
        /// 商品类型，取值参见微信同城配送物品类型列表。
        /// </summary>
        public int cargo_type { get; set; }

        /// <summary>
        /// 商品数量。
        /// </summary>
        public int cargo_num { get; set; }

        /// <summary>
        /// 商品明细列表。官方参数表写为 object，实际示例发送数组。
        /// </summary>
        public IList<WeixinExpressIntracityCargoItem> item_list { get; set; }
    }

    /// <summary>
    /// 同城配送单项商品。
    /// </summary>
    public class WeixinExpressIntracityCargoItem
    {
        /// <summary>
        /// 商品名称。
        /// </summary>
        public string item_name { get; set; }

        /// <summary>
        /// 商品图片 URL。
        /// </summary>
        public string item_pic_url { get; set; }

        /// <summary>
        /// 商品数量。
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 同城配送预下单询价请求。
    /// </summary>
    public class WeixinExpressIntracityPreAddOrderRequest
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 收件人姓名。
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 收件人手机号。
        /// </summary>
        public string user_phone { get; set; }

        /// <summary>
        /// 收件地址经度。
        /// </summary>
        public decimal user_lng { get; set; }

        /// <summary>
        /// 收件地址纬度。
        /// </summary>
        public decimal user_lat { get; set; }

        /// <summary>
        /// 收件人详细地址。
        /// </summary>
        public string user_address { get; set; }

        /// <summary>
        /// 商品名称；官方请求体同时要求在 <see cref="cargo"/> 中填写商品名称。
        /// </summary>
        public string cargo_name { get; set; }

        /// <summary>
        /// 商品信息。
        /// </summary>
        public WeixinExpressIntracityCargo cargo { get; set; }

        /// <summary>
        /// 是否使用沙箱环境，1 表示使用沙箱。
        /// </summary>
        public int use_sandbox { get; set; }
    }

    /// <summary>
    /// 同城配送预下单询价结果。
    /// </summary>
    /// <remarks>
    /// 当前官方返回参数表标注为“无”，但接口说明明确承诺返回实时运费和配送距离，因此保留这些业务字段。
    /// </remarks>
    public class WeixinExpressIntracityPreAddOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 预下单选中的运力 ID。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 预估配送距离，单位为米。
        /// </summary>
        public decimal distance { get; set; }

        /// <summary>
        /// 预估配送费，单位为分；最终金额以下单接口为准。
        /// </summary>
        public long fee { get; set; }
    }

    /// <summary>
    /// 创建同城配送订单请求。
    /// </summary>
    public class WeixinExpressIntracityAddOrderRequest
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 商家门店订单编号；同一门店内必须唯一。
        /// </summary>
        public string store_order_id { get; set; }

        /// <summary>
        /// 收件用户 OpenId。
        /// </summary>
        public string user_openid { get; set; }

        /// <summary>
        /// 收件地址经度。
        /// </summary>
        public decimal user_lng { get; set; }

        /// <summary>
        /// 收件地址纬度。
        /// </summary>
        public decimal user_lat { get; set; }

        /// <summary>
        /// 收件人详细地址。
        /// </summary>
        public string user_address { get; set; }

        /// <summary>
        /// 收件人姓名。
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 收件人电话，可填写 11 位手机号或带区号的固话。
        /// </summary>
        public string user_phone { get; set; }

        /// <summary>
        /// 订单序号，用于配送员快速匹配商品。
        /// </summary>
        public string order_seq { get; set; }

        /// <summary>
        /// 验证码类型：0 不生成，1 取货码，2 收货码，3 两者都生成。
        /// </summary>
        public string verify_code_type { get; set; }

        /// <summary>
        /// 商家小程序订单详情页路径。
        /// </summary>
        public string order_detail_path { get; set; }

        /// <summary>
        /// 订单状态回调地址。
        /// </summary>
        public string callback_url { get; set; }

        /// <summary>
        /// 是否使用沙箱环境，1 表示使用沙箱。
        /// </summary>
        public int? use_sandbox { get; set; }

        /// <summary>
        /// 商品信息。
        /// </summary>
        public WeixinExpressIntracityCargo cargo { get; set; }
    }

    /// <summary>
    /// 创建同城配送订单结果。
    /// </summary>
    public class WeixinExpressIntracityAddOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 微信配送订单编号。
        /// </summary>
        public string wx_order_id { get; set; }

        /// <summary>
        /// 商家门店订单编号。
        /// </summary>
        public string store_order_id { get; set; }

        /// <summary>
        /// 配送运力 ID。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 配送距离，单位为米。
        /// </summary>
        public decimal distance { get; set; }

        /// <summary>
        /// 运力订单号。
        /// </summary>
        public string trans_order_id { get; set; }

        /// <summary>
        /// 运力配送单号，是否返回取决于运力。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 配送费，单位为分。
        /// </summary>
        public long fee { get; set; }

        /// <summary>
        /// 取货码。
        /// </summary>
        public string fetch_code { get; set; }

        /// <summary>
        /// 取货序号。
        /// </summary>
        public string order_seq { get; set; }
    }

    /// <summary>
    /// 查询同城配送订单请求。
    /// </summary>
    public class WeixinExpressIntracityQueryOrderRequest
    {
        /// <summary>
        /// 微信门店编号；使用门店订单号查询时必须与 <see cref="store_order_id"/> 成对填写。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 商家门店订单号；必须与 <see cref="wx_store_id"/> 成对填写。
        /// </summary>
        public string store_order_id { get; set; }

        /// <summary>
        /// 微信配送订单号，可单独用于查询。
        /// </summary>
        public string wx_order_id { get; set; }
    }

    /// <summary>
    /// 同城配送配送员信息。
    /// </summary>
    public class WeixinExpressIntracityTransporterInfo
    {
        /// <summary>
        /// 配送员姓名。
        /// </summary>
        public string transporter_name { get; set; }

        /// <summary>
        /// 配送员电话。
        /// </summary>
        public string transporter_phone { get; set; }
    }

    /// <summary>
    /// 同城配送订单中的门店快照。
    /// </summary>
    public class WeixinExpressIntracityStoreSnapshot
    {
        /// <summary>
        /// 门店名称。
        /// </summary>
        public string store_name { get; set; }

        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 门店详细地址。
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 门店经度。
        /// </summary>
        public decimal lng { get; set; }

        /// <summary>
        /// 门店纬度。
        /// </summary>
        public decimal lat { get; set; }

        /// <summary>
        /// 门店电话。
        /// </summary>
        public string phone_num { get; set; }
    }

    /// <summary>
    /// 同城配送订单中的收件人信息。
    /// </summary>
    public class WeixinExpressIntracityReceiverInfo
    {
        /// <summary>
        /// 收件人姓名。
        /// </summary>
        public string receiver_name { get; set; }

        /// <summary>
        /// 收件人详细地址。
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 收件人电话。
        /// </summary>
        public string phone_num { get; set; }

        /// <summary>
        /// 收件地址经度。
        /// </summary>
        public decimal lng { get; set; }

        /// <summary>
        /// 收件地址纬度。
        /// </summary>
        public decimal lat { get; set; }
    }

    /// <summary>
    /// 同城配送订单返回的商品信息。
    /// </summary>
    public class WeixinExpressIntracityCargoInfo
    {
        /// <summary>
        /// 商品名称。
        /// </summary>
        public string cargo_name { get; set; }

        /// <summary>
        /// 商品总重量，单位为克。
        /// </summary>
        public decimal cargo_weight { get; set; }

        /// <summary>
        /// 商品总价格，单位为分。
        /// </summary>
        public long cargo_price { get; set; }

        /// <summary>
        /// 商品类型。
        /// </summary>
        public int cargo_type { get; set; }

        /// <summary>
        /// 商品数量。
        /// </summary>
        public int cargo_num { get; set; }

        /// <summary>
        /// 商品明细。官方参数表写为 object，实际返回示例为数组。
        /// </summary>
        public IList<WeixinExpressIntracityCargoInfoItem> item_list { get; set; }
    }

    /// <summary>
    /// 同城配送订单返回的单项商品。
    /// </summary>
    public class WeixinExpressIntracityCargoInfoItem
    {
        /// <summary>
        /// 商品名称。
        /// </summary>
        public string item_name { get; set; }

        /// <summary>
        /// 商品图片 URL。
        /// </summary>
        public string item_pic_url { get; set; }

        /// <summary>
        /// 商品数量。官方参数表字段名为 num；返回示例中的 item_num 也会映射到本属性。
        /// </summary>
        public int num { get; set; }

        [JsonProperty("item_num")]
        private int ItemNum
        {
            set => num = value;
        }
    }

    /// <summary>
    /// 查询同城配送订单结果。
    /// </summary>
    public class WeixinExpressIntracityQueryOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信配送订单号。
        /// </summary>
        public string wx_order_id { get; set; }

        /// <summary>
        /// 商家门店订单号。
        /// </summary>
        public string store_order_id { get; set; }

        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 订单状态，例如 10000 创建成功、70000 配送完成。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 下单小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 收件用户 OpenId。
        /// </summary>
        public string user_openid { get; set; }

        /// <summary>
        /// 运力 ID。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 运力订单号。
        /// </summary>
        public string delivery_no { get; set; }

        /// <summary>
        /// 配送距离，单位为米。
        /// </summary>
        public decimal distance { get; set; }

        /// <summary>
        /// 实际支付费用，单位为分。
        /// </summary>
        public long actualfee { get; set; }

        /// <summary>
        /// 违约金，单位为分。
        /// </summary>
        public long deductfee { get; set; }

        /// <summary>
        /// 发单时间，Unix 秒级时间戳。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 配送员接单时间，Unix 秒级时间戳。
        /// </summary>
        public long accept_time { get; set; }

        /// <summary>
        /// 配送完成时间，Unix 秒级时间戳。
        /// </summary>
        public long finish_time { get; set; }

        /// <summary>
        /// 配送员取货时间，Unix 秒级时间戳。
        /// </summary>
        public long fetch_time { get; set; }

        /// <summary>
        /// 取消时间，Unix 秒级时间戳。
        /// </summary>
        public long cancel_time { get; set; }

        /// <summary>
        /// 预计送达时间，Unix 秒级时间戳。
        /// </summary>
        public long expected_finish_time { get; set; }

        /// <summary>
        /// 取货码，供商家验证。
        /// </summary>
        public string fetch_code { get; set; }

        /// <summary>
        /// 收货码，供收件人验证。
        /// </summary>
        public string recv_code { get; set; }

        /// <summary>
        /// 订单序号。
        /// </summary>
        public string order_seq { get; set; }

        /// <summary>
        /// 配送员信息。
        /// </summary>
        public WeixinExpressIntracityTransporterInfo transporter_info { get; set; }

        /// <summary>
        /// 门店信息。
        /// </summary>
        public WeixinExpressIntracityStoreSnapshot store_info { get; set; }

        /// <summary>
        /// 收件人信息。
        /// </summary>
        public WeixinExpressIntracityReceiverInfo receiver_info { get; set; }

        /// <summary>
        /// 商品信息。
        /// </summary>
        public WeixinExpressIntracityCargoInfo cargo_info { get; set; }
    }

    /// <summary>
    /// 取消同城配送订单请求。
    /// </summary>
    public class WeixinExpressIntracityCancelOrderRequest : WeixinExpressIntracityQueryOrderRequest
    {
        /// <summary>
        /// 取消原因：1 不需要了，2 信息填错，3 无人接单，99 其他。
        /// </summary>
        public int cancel_reason_id { get; set; }

        /// <summary>
        /// 取消原因描述。
        /// </summary>
        public string cancel_reason { get; set; }
    }

    /// <summary>
    /// 取消同城配送订单结果。
    /// </summary>
    public class WeixinExpressIntracityCancelOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 微信配送订单号。
        /// </summary>
        public string wx_order_id { get; set; }

        /// <summary>
        /// 商家门店订单号。
        /// </summary>
        public string store_order_id { get; set; }

        /// <summary>
        /// 取消后的订单状态。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 下单小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 取消配送产生的违约金，单位为分。
        /// </summary>
        public long deductfee { get; set; }
    }

    /// <summary>
    /// 设置同城配送扣费主体请求。
    /// </summary>
    public class WeixinExpressIntracitySetPayModeRequest
    {
        /// <summary>
        /// 小程序 AppId，必须与 AccessToken 匹配。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 扣费主体：PAY_MODE_STORE、PAY_MODE_APP 或 PAY_MODE_COMPONENT。
        /// </summary>
        public string pay_mode { get; set; }
    }

    /// <summary>
    /// 查询同城配送扣费主体请求。
    /// </summary>
    public class WeixinExpressIntracityGetPayModeRequest
    {
        /// <summary>
        /// 小程序 AppId，必须与 AccessToken 匹配。
        /// </summary>
        public string appid { get; set; }
    }

    /// <summary>
    /// 查询同城配送扣费主体结果。
    /// </summary>
    public class WeixinExpressIntracityGetPayModeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 扣费主体：PAY_MODE_STORE、PAY_MODE_APP 或 PAY_MODE_COMPONENT。
        /// </summary>
        public string pay_mode { get; set; }

        /// <summary>
        /// 扣费小程序 AppId，仅扣费主体为小程序时返回。
        /// </summary>
        public string pay_appid { get; set; }

        /// <summary>
        /// 扣费第三方平台 AppId，仅扣费主体为服务商时返回。
        /// </summary>
        public string pay_component_appid { get; set; }
    }

    /// <summary>
    /// 查询支持同城配送城市请求。
    /// </summary>
    public class WeixinExpressIntracityGetCityRequest
    {
        /// <summary>
        /// 指定运力 ID；不填写时返回全部运力支持的城市。
        /// </summary>
        public string service_trans_id { get; set; }
    }

    /// <summary>
    /// 同城配送运力支持的城市信息。
    /// </summary>
    public class WeixinExpressIntracityCitySupport
    {
        /// <summary>
        /// 运力 ID。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 支持的城市列表。
        /// </summary>
        public IList<WeixinExpressIntracityCity> city_list { get; set; }
    }

    /// <summary>
    /// 同城配送支持城市。
    /// </summary>
    public class WeixinExpressIntracityCity
    {
        /// <summary>
        /// 城市行政区域编码。
        /// </summary>
        public int city_code { get; set; }

        /// <summary>
        /// 城市名称。
        /// </summary>
        public string city_name { get; set; }
    }

    /// <summary>
    /// 查询支持同城配送城市结果。
    /// </summary>
    public class WeixinExpressIntracityGetCityJsonResult : WxJsonResult
    {
        /// <summary>
        /// 各运力支持的城市列表。官方参数表写为 object，实际示例返回数组。
        /// </summary>
        public IList<WeixinExpressIntracityCitySupport> support_list { get; set; }
    }

    /// <summary>
    /// 模拟同城配送订单状态回调请求。
    /// </summary>
    public class WeixinExpressIntracityMockNotifyRequest
    {
        /// <summary>
        /// 微信配送订单号，可单独用于定位订单。
        /// </summary>
        public string wx_order_id { get; set; }

        /// <summary>
        /// 微信门店编号；使用门店订单号时必须与 <see cref="store_order_id"/> 成对填写。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 商家门店订单号；必须与 <see cref="wx_store_id"/> 成对填写。
        /// </summary>
        public string store_order_id { get; set; }

        /// <summary>
        /// 要模拟的订单状态，例如 30000 配送员接单、70000 配送完成。
        /// </summary>
        public int order_status { get; set; }
    }

    /// <summary>
    /// 同城配送订单状态回调报文。
    /// </summary>
    public class WeixinExpressIntracityOrderStatusNotify
    {
        /// <summary>
        /// 下单小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信门店编号。
        /// </summary>
        public string wx_store_id { get; set; }

        /// <summary>
        /// 微信配送订单号。
        /// </summary>
        public string wx_order_id { get; set; }

        /// <summary>
        /// 商家门店订单号。
        /// </summary>
        public string store_order_id { get; set; }

        /// <summary>
        /// 订单状态。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 订单状态变更时间，Unix 秒级时间戳。
        /// </summary>
        public long status_change_time { get; set; }

        /// <summary>
        /// 消息推送时间，Unix 秒级时间戳。
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 运力 ID。
        /// </summary>
        public string service_trans_id { get; set; }

        /// <summary>
        /// 回调签名值，校验方式参见微信官方同城配送回调文档。
        /// </summary>
        public string sign { get; set; }
    }

    /// <summary>
    /// 商家接收同城配送状态回调后返回的应答报文。
    /// </summary>
    public class WeixinExpressIntracityNotifyResponse
    {
        /// <summary>
        /// 应答码，0 表示成功。
        /// </summary>
        public int return_code { get; set; }

        /// <summary>
        /// 应答说明，成功时通常为 OK。
        /// </summary>
        public string return_msg { get; set; }
    }
}
