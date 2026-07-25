#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MedicalInsuranceApis.cs
    文件功能描述：微信支付 V3 医保自费混合支付接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.MedicalInsurance;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付 V3 医保自费混合支付接口。
    /// <para>普通商户、服务商和间连模式复用官方相同的请求路径，通过请求体及查询参数中的 sub_* 字段区分模式。</para>
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4016824672</para>
    /// </summary>
    public class MedicalInsuranceApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 创建医保支付接口实例。
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">微信支付 V3 配置；为空时使用全局默认配置。</param>
        public MedicalInsuranceApis(
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ??
                                  Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 医保自费混合收款下单。
        /// </summary>
        /// <param name="data">医保自费混合订单数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>医保自费混合订单结果。</returns>
        public async Task<MedicalInsuranceOrderResultJson> CreateOrderAsync(
            MedicalInsuranceOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/med-ins/orders");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<MedicalInsuranceOrderResultJson>(url, data, timeOut)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 使用医保自费混合订单号查询下单结果。
        /// </summary>
        /// <param name="mixTradeNo">医保自费混合订单号。</param>
        /// <param name="subMchId">服务商或间连模式下的子商户号；普通商户留空。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>医保自费混合订单结果。</returns>
        public async Task<MedicalInsuranceOrderResultJson> QueryOrderByMixTradeNoAsync(
            string mixTradeNo, string subMchId = null, int timeOut = Config.TIME_OUT)
        {
            var path = $"v3/med-ins/orders/mix-trade-no/{Escape(mixTradeNo)}" +
                       BuildSubMchIdQuery(subMchId);
            return await QueryOrderAsync(path, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 使用商户、服务商或从业机构订单号查询下单结果。
        /// </summary>
        /// <param name="outTradeNo">请求方订单号。</param>
        /// <param name="subMchId">服务商或间连模式下的子商户号；普通商户留空。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>医保自费混合订单结果。</returns>
        public async Task<MedicalInsuranceOrderResultJson> QueryOrderByOutTradeNoAsync(
            string outTradeNo, string subMchId = null, int timeOut = Config.TIME_OUT)
        {
            var path = $"v3/med-ins/orders/out-trade-no/{Escape(outTradeNo)}" +
                       BuildSubMchIdQuery(subMchId);
            return await QueryOrderAsync(path, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 向微信医保通知医保订单退款成功。
        /// </summary>
        /// <param name="mixTradeNo">医保自费混合订单号。</param>
        /// <param name="data">医保退款金额和退款单信息。</param>
        /// <param name="subMchId">服务商或间连模式下的子商户号；普通商户留空。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果；成功时官方返回 HTTP 204。</returns>
        public async Task<ReturnJsonBase> NotifyRefundSuccessAsync(string mixTradeNo,
            MedicalInsuranceRefundNotifyRequestData data, string subMchId = null,
            int timeOut = Config.TIME_OUT)
        {
            var query = $"?mix_trade_no={Escape(mixTradeNo)}";
            if (!string.IsNullOrWhiteSpace(subMchId))
            {
                query += $"&sub_mchid={Escape(subMchId)}";
            }

            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/med-ins/refunds/notify{query}");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<ReturnJsonBase>(url, data, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 生成小程序调用 wx.requestMedicalInsurancePay 所需参数。
        /// </summary>
        /// <param name="mixTradeNo">医保自费混合订单号。</param>
        /// <param name="prepayId">自费预下单 ID；纯医保支付时留空。</param>
        /// <param name="appId">用于自费支付签名的 AppId；为空时优先使用配置中的子商户 AppId。</param>
        /// <returns>小程序调起医保自费混合支付参数。</returns>
        public MedicalInsurancePayPackage CreateMiniProgramPayPackage(string mixTradeNo,
            string prepayId = null, string appId = null)
        {
            return CreatePayPackage(mixTradeNo, prepayId, appId, false);
        }

        /// <summary>
        /// 生成 JSAPI 调用 requestMedicalInsurancePay 所需参数。
        /// </summary>
        /// <param name="mixTradeNo">医保自费混合订单号。</param>
        /// <param name="prepayId">自费预下单 ID；纯医保支付时留空。</param>
        /// <param name="appId">JSAPI 使用及自费支付签名的 AppId；为空时优先使用配置中的子商户 AppId。</param>
        /// <returns>JSAPI 调起医保自费混合支付参数。</returns>
        public MedicalInsurancePayPackage CreateJsApiPayPackage(string mixTradeNo,
            string prepayId = null, string appId = null)
        {
            return CreatePayPackage(mixTradeNo, prepayId, appId, true);
        }

        private async Task<MedicalInsuranceOrderResultJson> QueryOrderAsync(string path, int timeOut)
        {
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<MedicalInsuranceOrderResultJson>(url, null, timeOut,
                ApiRequestMethod.GET).ConfigureAwait(false);
        }

        private MedicalInsurancePayPackage CreatePayPackage(string mixTradeNo, string prepayId,
            string appId, bool includeAppId)
        {
            var effectiveAppId = appId;
            if (string.IsNullOrWhiteSpace(effectiveAppId))
            {
                effectiveAppId = !string.IsNullOrWhiteSpace(_tenpayV3Setting.TenPayV3_SubAppId)
                    ? _tenpayV3Setting.TenPayV3_SubAppId
                    : _tenpayV3Setting.TenPayV3_AppId;
            }

            var result = new MedicalInsurancePayPackage
            {
                mixTradeNo = mixTradeNo,
                appid = includeAppId ? effectiveAppId : null
            };

            if (string.IsNullOrWhiteSpace(prepayId))
            {
                return result;
            }

            if (!_tenpayV3Setting.EncryptionType.HasValue)
            {
                throw new Senparc.Weixin.Exceptions.WeixinException(
                    "没有设置证书加密类型（EncryptionType）");
            }

            result.timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            result.nonceStr = Guid.NewGuid().ToString("N");
            result.package = $"prepay_id={prepayId}";
            result.signType = "RSA";
            result.paySign = TenPaySignHelper.CreatePaySign(result.timeStamp, result.nonceStr,
                result.package, effectiveAppId, _tenpayV3Setting.TenPayV3_PrivateKey,
                _tenpayV3Setting.EncryptionType.Value);
            return result;
        }

        private static string BuildSubMchIdQuery(string subMchId)
        {
            return string.IsNullOrWhiteSpace(subMchId)
                ? string.Empty
                : $"?sub_mchid={Escape(subMchId)}";
        }

        private static string Escape(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
