/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_XPay_Refund_Notify.cs
    文件功能描述：小程序虚拟支付 - 退款推送
    
    
    创建标识：Senparc - 20231130

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    public class RequestMessageEvent_XPayRefundNotify : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.xpay_refund_notify; }
        }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string WxRefundId { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string MchRefundId { get; set; }

        /// <summary>
        /// 退款单对应支付单的微信单号
        /// </summary>
        public string WxOrderId { get; set; }

        /// <summary>
        /// 退款单对应支付单的商户单号
        /// </summary>
        public string MchOrderId { get; set; }

        /// <summary>
        /// 退款金额，单位分
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 退款结果，0为成功，非0为失败
        /// </summary>
        public int RetCode { get; set; }

        /// <summary>
        /// 退款结果详情，失败时为退款失败的原因
        /// </summary>
        public string RetMsg { get; set; }

        /// <summary>
        /// 开始退款时间，秒级时间戳
        /// </summary>
        public long RefundStartTimestamp { get; set; }

        /// <summary>
        /// 结束退款时间，秒级时间戳
        /// </summary>
        public long RefundSuccTimestamp { get; set; }

        /// <summary>
        /// 退款单的微信支付单号
        /// </summary>
        public string WxpayRefundTransactionId { get; set; }

        /// <summary>
        /// 重试次数，从0开始。重试间隔为2 4 8 16...最多15次
        /// </summary>
        public int RetryTimes { get; set; }
    }
}
