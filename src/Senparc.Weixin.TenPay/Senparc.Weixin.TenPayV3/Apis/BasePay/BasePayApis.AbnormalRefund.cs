#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BasePayApis.AbnormalRefund.cs
    文件功能描述：BasePayApis.AbnormalRefund 相关功能


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.BasePay;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付基础支付补充接口。
    /// </summary>
    public partial class BasePayApis
    {
        /// <summary>
        /// 发起异常退款。普通商户和服务商使用同一路径，服务商需在请求中填写 sub_mchid。
        /// </summary>
        /// <param name="refundId">微信支付退款单号。</param>
        /// <param name="data">接口业务数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public async Task<RefundReturnJson> ApplyAbnormalRefundAsync(string refundId,
            AbnormalRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            var escapedRefundId = Uri.EscapeDataString(refundId ?? "");
            var url = GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/refund/domestic/refunds/{escapedRefundId}/apply-abnormal-refund");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<RefundReturnJson>(url, data, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 合单支付订单发起异常退款。微信支付使用与普通支付一致的退款单处理接口。
        /// </summary>
        /// <param name="refundId">微信支付退款单号。</param>
        /// <param name="data">接口业务数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public Task<RefundReturnJson> ApplyCombineAbnormalRefundAsync(string refundId,
            AbnormalRefundRequestData data, int timeOut = Config.TIME_OUT)
        {
            return ApplyAbnormalRefundAsync(refundId, data, timeOut);
        }
    }
}
