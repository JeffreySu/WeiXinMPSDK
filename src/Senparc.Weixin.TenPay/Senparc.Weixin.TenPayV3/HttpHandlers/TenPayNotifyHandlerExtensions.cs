#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TenPayNotifyHandlerExtensions.cs
    文件功能描述：TenPayNotifyHandlerExtensions 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities;
using Senparc.Weixin.TenPayV3.Apis.Complaint;
using Senparc.Weixin.TenPayV3.Apis.MedicalInsurance;
using Senparc.Weixin.TenPayV3.Apis.VehicleParking;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 常用支付通知的强类型验签、解密入口。
    /// </summary>
    public static class TenPayNotifyHandlerExtensions
    {
        /// <summary>
        /// 异步解密退款结果通知。
        /// </summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式解密。</param>
        /// <returns>验签并解密后的退款通知。</returns>
        public static Task<RefundNotifyJson> DecryptRefundNotifyAsync(this TenPayNotifyHandler handler,
            bool isPublicKey = false) => handler.DecryptGetObjectAsync<RefundNotifyJson>(isPublicKey);

        /// <summary>
        /// 异步解密消费者投诉通知。
        /// </summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式解密。</param>
        /// <returns>验签并解密后的消费者投诉通知。</returns>
        public static Task<ComplaintNotifyJson> DecryptComplaintNotifyAsync(this TenPayNotifyHandler handler,
            bool isPublicKey = false) => handler.DecryptGetObjectAsync<ComplaintNotifyJson>(isPublicKey);

        /// <summary>
        /// 异步解密停车入场状态通知。
        /// </summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式解密。</param>
        /// <returns>验签并解密后的停车入场状态通知。</returns>
        public static Task<ParkingStateNotifyJson> DecryptParkingStateNotifyAsync(this TenPayNotifyHandler handler,
            bool isPublicKey = false) => handler.DecryptGetObjectAsync<ParkingStateNotifyJson>(isPublicKey);

        /// <summary>
        /// 异步解密停车扣费通知。
        /// </summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式解密。</param>
        /// <returns>验签并解密后的停车扣费通知。</returns>
        public static Task<PayNotifyJson> DecryptParkingPayNotifyAsync(this TenPayNotifyHandler handler,
            bool isPublicKey = false) => handler.DecryptGetObjectAsync<PayNotifyJson>(isPublicKey);

        /// <summary>
        /// 异步解密停车退款通知。
        /// </summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式解密。</param>
        /// <returns>验签并解密后的停车退款通知。</returns>
        public static Task<ParkingRefundNotifyJson> DecryptParkingRefundNotifyAsync(this TenPayNotifyHandler handler,
            bool isPublicKey = false) => handler.DecryptGetObjectAsync<ParkingRefundNotifyJson>(isPublicKey);

        /// <summary>
        /// 异步解密医保自费混合收款成功通知。
        /// </summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式解密。</param>
        /// <returns>验签并解密后的医保自费混合订单结果。</returns>
        public static Task<MedicalInsuranceOrderResultJson> DecryptMedicalInsurancePayNotifyAsync(
            this TenPayNotifyHandler handler, bool isPublicKey = false) =>
            handler.DecryptGetObjectAsync<MedicalInsuranceOrderResultJson>(isPublicKey);
    }
}
