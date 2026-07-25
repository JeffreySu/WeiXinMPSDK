#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_RetailRefundNotify.cs
    文件功能描述：RequestMessageEvent_RetailRefundNotify 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// B2B 门店助手退款结果通知。
    /// </summary>
    /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_refundorder"/>。</remarks>
    public class RequestMessageEvent_RetailRefundNotify : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型，固定为 <see cref="WxOpen.Event.retail_refund_notify"/>。
        /// </summary>
        public override Event Event => Event.retail_refund_notify;

        /// <summary>商户申请的小程序 AppId。</summary>
        public string appid { get; set; }

        /// <summary>微信商户号。</summary>
        public string mchid { get; set; }

        /// <summary>商户退款单号。</summary>
        public string out_refund_no { get; set; }

        /// <summary>可选的 B2B 支付退款单号。</summary>
        public string refund_id { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>B2B 支付订单号。</summary>
        public string order_id { get; set; }

        /// <summary>退款金额，单位为分。</summary>
        public long refund_amount { get; set; }

        /// <summary>订单总金额，单位为分。</summary>
        public long order_amount { get; set; }

        /// <summary>退款来源：1 人工客服退款，2 用户自行退款，3 其他。官方通知协议定义为字符串。</summary>
        public string refund_from { get; set; }

        /// <summary>可选的退款原因：0 暂无描述，1 产品问题，2 售后问题，3 意愿问题，4 价格问题，5 其他原因。</summary>
        public string refund_reason { get; set; }

        /// <summary>退款受理时间，格式为 yyyy-MM-dd HH:mm:ss。</summary>
        public string create_time { get; set; }

        /// <summary>退款成功时间，格式为 yyyy-MM-dd HH:mm:ss；退款成功时返回。</summary>
        public string refund_time { get; set; }

        /// <summary>退款状态：REFUND_SUCC 或 REFUND_FAIL。</summary>
        public string refund_status { get; set; }

        /// <summary>可选的微信支付退款单号。</summary>
        public string wxpay_refund_id { get; set; }

        /// <summary>订单环境：0 正式环境，1 沙箱环境。</summary>
        public int env { get; set; }

        /// <summary>交易渠道类型：0 微信支付，1 银行转账。</summary>
        public int pay_channel { get; set; }

        /// <summary>可选的退款结果说明，失败时可能包含具体原因。</summary>
        public string refund_desc { get; set; }
    }
}
