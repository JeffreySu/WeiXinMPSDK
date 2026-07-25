#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ParkingReminderApis.cs
    文件功能描述：微信支付 V3 停车缴费服务接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.ParkingReminder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付 V3 停车缴费服务接口。
    /// <para>该产品用于服务商提交停车场进件、同步车辆和支付状态，并查询停车场及停车费用。</para>
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4019514964</para>
    /// </summary>
    public class ParkingReminderApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>创建停车缴费服务接口实例。</summary>
        /// <param name="senparcWeixinSettingForTenpayV3">微信支付 V3 服务商配置；为空时使用全局默认配置。</param>
        public ParkingReminderApis(
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ??
                                  Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>提交停车场进件申请。</summary>
        /// <param name="data">停车场及计费规则。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>停车场进件申请单号。</returns>
        public Task<ParkingLotApplicationSubmitResultJson> SubmitApplicationAsync(
            ParkingLotApplicationRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/application";
            return PostAsync<ParkingLotApplicationSubmitResultJson>(path, data, timeOut);
        }

        /// <summary>查询停车场进件申请单。</summary>
        /// <param name="parkingLotAuditNo">停车场进件申请单号。</param>
        /// <param name="outParkingLotId">服务商停车场 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>停车场进件申请详情。</returns>
        public Task<ParkingLotApplicationResultJson> QueryApplicationAsync(string parkingLotAuditNo,
            string outParkingLotId, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/application/query";
            var query = BuildQuery(new Dictionary<string, object>
            {
                ["parking_lot_audit_no"] = parkingLotAuditNo,
                ["out_parking_lot_id"] = outParkingLotId
            });
            return GetAsync<ParkingLotApplicationResultJson>(path + query, timeOut);
        }

        /// <summary>查询停车场进件申请单列表。</summary>
        /// <param name="outParkingLotId">服务商停车场 ID。</param>
        /// <param name="offset">分页偏移量。</param>
        /// <param name="limit">分页大小。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>停车场进件申请记录列表。</returns>
        public Task<ParkingLotApplicationListResultJson> QueryApplicationListAsync(
            string outParkingLotId, int? offset = null, int? limit = null,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/applications";
            var query = BuildQuery(new Dictionary<string, object>
            {
                ["out_parking_lot_id"] = outParkingLotId,
                ["offset"] = offset,
                ["limit"] = limit
            });
            return GetAsync<ParkingLotApplicationListResultJson>(path + query, timeOut);
        }

        /// <summary>撤回停车场进件申请。</summary>
        /// <param name="parkingLotAuditNo">停车场进件申请单号。</param>
        /// <param name="outParkingLotId">服务商停车场 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public Task<ReturnJsonBase> WithdrawApplicationAsync(string parkingLotAuditNo,
            string outParkingLotId, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/application/withdraw";
            return PostAsync<ReturnJsonBase>(path, new
            {
                parking_lot_audit_no = parkingLotAuditNo,
                out_parking_lot_id = outParkingLotId
            }, timeOut);
        }

        /// <summary>同步车辆入场通知。</summary>
        /// <param name="data">车辆入场信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付停车流水号。</returns>
        public Task<ParkingEntryResultJson> SyncEntryAsync(ParkingEntryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/entry";
            return PostAsync<ParkingEntryResultJson>(path, data, timeOut);
        }

        /// <summary>同步车辆离场通知。</summary>
        /// <param name="data">车辆离场及支付信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public Task<ReturnJsonBase> SyncExitAsync(ParkingExitRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/exit";
            return PostAsync<ReturnJsonBase>(path, data, timeOut);
        }

        /// <summary>同步停车支付结果通知。</summary>
        /// <param name="data">停车订单及支付结果。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public Task<ReturnJsonBase> SyncPaymentAsync(ParkingPaymentRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/payment";
            return PostAsync<ReturnJsonBase>(path, data, timeOut);
        }

        /// <summary>同步非临停扩展支付。</summary>
        /// <param name="data">非临停订单、费用明细和支付信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public Task<ReturnJsonBase> SyncExtensionPaymentAsync(ParkingExtensionPaymentRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/ext-payment";
            return PostAsync<ReturnJsonBase>(path, data, timeOut);
        }

        /// <summary>查询停车场信息。</summary>
        /// <param name="outParkingLotId">服务商停车场 ID，与 wxParkingLotId 至少填写一个。</param>
        /// <param name="wxParkingLotId">微信支付停车场 ID，与 outParkingLotId 至少填写一个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>停车场启用状态和小程序配置。</returns>
        public Task<ParkingLotInfoResultJson> QueryParkingLotAsync(string outParkingLotId = null,
            string wxParkingLotId = null, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/parking-lot";
            var query = BuildQuery(new Dictionary<string, object>
            {
                ["out_parking_lot_id"] = outParkingLotId,
                ["wx_parking_lot_id"] = wxParkingLotId
            });
            return GetAsync<ParkingLotInfoResultJson>(path + query, timeOut);
        }

        /// <summary>查询停车费用。</summary>
        /// <param name="data">停车流水号、车牌或服务商停车记录 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前应付金额、支付状态和允许离场时间。</returns>
        public Task<ParkingFeeResultJson> QueryParkingFeeAsync(ParkingFeeRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/parking/reminders/parking-fee";
            return PostAsync<ParkingFeeResultJson>(path, data, timeOut);
        }

        private Task<T> GetAsync<T>(string path, int timeOut) where T : ReturnJsonBase, new()
        {
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(url, null, timeOut, ApiRequestMethod.GET);
        }

        private Task<T> PostAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(url, data, timeOut);
        }

        private static string BuildQuery(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var values = new List<string>();
            foreach (var parameter in parameters)
            {
                if (parameter.Value == null || string.IsNullOrWhiteSpace(parameter.Value.ToString()))
                {
                    continue;
                }

                values.Add($"{Escape(parameter.Key)}={Escape(parameter.Value.ToString())}");
            }

            return values.Count == 0 ? string.Empty : "?" + string.Join("&", values);
        }

        private static string Escape(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
