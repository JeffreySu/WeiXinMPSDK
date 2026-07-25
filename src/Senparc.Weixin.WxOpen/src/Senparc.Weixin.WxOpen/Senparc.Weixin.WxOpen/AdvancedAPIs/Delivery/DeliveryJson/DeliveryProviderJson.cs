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

    文件名：DeliveryProviderJson.cs
    文件功能描述：DeliveryProviderJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson
{
    /// <summary>
    /// 运力方更新商户审核结果请求。
    /// </summary>
    public class DeliveryProviderUpdateBusinessRequest
    {
        /// <summary>
        /// 商户小程序 AppId，即商户审核事件中的 ShopAppID。
        /// </summary>
        public string shop_app_id { get; set; }

        /// <summary>
        /// 商户在运力方的物流账号。
        /// </summary>
        public string biz_id { get; set; }

        /// <summary>
        /// 审核结果，0 表示通过，其他值表示失败。
        /// </summary>
        public int result_code { get; set; }

        /// <summary>
        /// 审核失败原因；仅 <see cref="result_code"/> 不等于 0 时需要填写。
        /// </summary>
        public string result_msg { get; set; }
    }

    /// <summary>
    /// 运力方更新运单轨迹请求。
    /// </summary>
    public class DeliveryProviderUpdatePathRequest
    {
        /// <summary>
        /// 商户侧下单事件中推送的 Token。
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 运单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 轨迹变化时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long action_time { get; set; }

        /// <summary>
        /// 轨迹变化类型，例如 100001 揽件成功、300003 签收成功、400001 订单取消。
        /// </summary>
        public int action_type { get; set; }

        /// <summary>
        /// 轨迹变化说明，使用 UTF-8 编码；包含手机号时直接填写 11 位号码。
        /// </summary>
        public string action_msg { get; set; }
    }

    /// <summary>
    /// 运力方面单模板预览请求。
    /// </summary>
    public class DeliveryProviderPreviewTemplateRequest
    {
        /// <summary>
        /// 运单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// Base64 编码后的面单 HTML 模板。
        /// </summary>
        public string waybill_template { get; set; }

        /// <summary>
        /// 运力方下单事件返回的自定义面单数据。
        /// </summary>
        public string waybill_data { get; set; }

        /// <summary>
        /// 商户生成运单时提交的原始下单数据。
        /// </summary>
        public AddOrderModel custom { get; set; }
    }

    /// <summary>
    /// 运力方面单模板预览结果。
    /// </summary>
    public class DeliveryProviderPreviewTemplateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 运单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// Base64 编码后的已渲染面单 HTML。
        /// </summary>
        public string rendered_waybill_template { get; set; }
    }

    /// <summary>
    /// 运力方获取面单联系人请求。
    /// </summary>
    public class DeliveryProviderGetContactRequest
    {
        /// <summary>
        /// 商户侧下单事件中推送的 Token。
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 运单 ID。
        /// </summary>
        public string waybill_id { get; set; }
    }

    /// <summary>
    /// 面单联系人信息。
    /// </summary>
    public class DeliveryProviderContact
    {
        /// <summary>
        /// 联系人姓名。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 座机号码。
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 手机号码。
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 已合并省、市、区信息的完整地址。
        /// </summary>
        public string address { get; set; }
    }

    /// <summary>
    /// 运力方获取面单联系人结果。
    /// </summary>
    public class DeliveryProviderGetContactJsonResult : WxJsonResult
    {
        /// <summary>
        /// 运单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 发件人信息。
        /// </summary>
        public DeliveryProviderContact sender { get; set; }

        /// <summary>
        /// 收件人信息。
        /// </summary>
        public DeliveryProviderContact receiver { get; set; }
    }

    /// <summary>
    /// 运力方取消订单请求。
    /// </summary>
    public class DeliveryProviderCancelOrderRequest
    {
        /// <summary>
        /// 商户侧下单事件中推送的 Token。
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 取消订单的原因。
        /// </summary>
        public string reason { get; set; }
    }

    /// <summary>
    /// 运力方更新待支付运费请求。
    /// </summary>
    public class DeliveryProviderUpdateOrderFeeRequest
    {
        /// <summary>
        /// 商户侧下单事件中推送的 Token。
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 运单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 是否需要用户在线支付：0 不需要，1 需要，2 需要且使用支付分。
        /// </summary>
        public int need_pay { get; set; }

        /// <summary>
        /// 用户最终需要支付的金额，单位为分。
        /// </summary>
        public long fee { get; set; }

        /// <summary>
        /// 原价，通常为运费、保价费和其他费用之和，单位为分。
        /// </summary>
        public long original_fee { get; set; }

        /// <summary>
        /// 基础运费，单位为分。
        /// </summary>
        public long base_fee { get; set; }

        /// <summary>
        /// 保价费，单位为分。
        /// </summary>
        public long? insured_fee { get; set; }

        /// <summary>
        /// 其他费用，单位为分。
        /// </summary>
        public long? other_fee { get; set; }

        /// <summary>
        /// 其他费用备注。官方当前参数表将类型标为 number，但字段语义为文本备注。
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 微信支付商品详情页显示的商品名称。
        /// </summary>
        public string pay_goods_name { get; set; }
    }

    /// <summary>
    /// 运力方退款请求。
    /// </summary>
    public class DeliveryProviderRefundOrderRequest
    {
        /// <summary>
        /// 商户侧下单事件中推送的 Token。
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 退款金额，单位为分；支持部分退款。
        /// </summary>
        public long fee { get; set; }
    }

    /// <summary>
    /// 运力方对账单下载请求。
    /// </summary>
    public class DeliveryProviderGetBillRequest
    {
        /// <summary>
        /// 对账日期，格式为 yyyyMMdd。
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 账单类型：ALL 全部、SUCCESS 支付成功、REFUND 退款。
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// 运力方对账单下载结果。
    /// </summary>
    public class DeliveryProviderGetBillJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信返回的原始内容。成功时为对账文件文本；失败时保留错误 JSON 文本。
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 运力方返回用户投诉处理结果请求。
    /// </summary>
    public class DeliveryProviderUpdateComplaintResultRequest
    {
        /// <summary>
        /// 商户侧下单事件中推送的 Token。
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 运单 ID。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 投诉处理结果。
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 投诉处理结果说明。
        /// </summary>
        public string desc { get; set; }
    }

    /// <summary>
    /// 运力方更新订单状态请求。
    /// </summary>
    public class DeliveryProviderUpdateOrderStatusRequest
    {
        /// <summary>
        /// 商户侧下单事件中推送的 Token。
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 运单 ID；部分下单阶段状态可不填。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 状态变化时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long action_time { get; set; }

        /// <summary>
        /// 状态类型，例如 90001 网点接单、100001 揽件成功、300003 签收成功、400001 订单取消。
        /// </summary>
        public int action_type { get; set; }

        /// <summary>
        /// 状态变化说明，使用 UTF-8 编码；包含手机号时直接填写 11 位号码。
        /// </summary>
        public string action_msg { get; set; }

        /// <summary>
        /// 取件员姓名。
        /// </summary>
        public string pickup_courier_name { get; set; }

        /// <summary>
        /// 取件员电话。
        /// </summary>
        public string pickup_courier_phone { get; set; }

        /// <summary>
        /// 派件员姓名。
        /// </summary>
        public string delivery_courier_name { get; set; }

        /// <summary>
        /// 派件员电话。
        /// </summary>
        public string delivery_courier_phone { get; set; }
    }
}
