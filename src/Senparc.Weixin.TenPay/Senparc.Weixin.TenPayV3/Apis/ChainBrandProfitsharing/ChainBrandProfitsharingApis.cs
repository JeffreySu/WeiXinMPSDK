#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChainBrandProfitsharingApis.cs
    文件功能描述：微信支付连锁品牌分账现行接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐连锁品牌分账 11 项请求与账单下载接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.ChainBrandProfitsharing;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付连锁品牌分账现行接口。
    /// <para>用于普通服务商为品牌主、门店或其他接收方完成分账、回退、接收方和账单管理。</para>
    /// </summary>
    public class ChainBrandProfitsharingApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _setting;

        /// <summary>
        /// 创建连锁品牌分账接口实例。
        /// </summary>
        /// <param name="setting">微信支付 V3 服务商配置；为空时使用全局配置。</param>
        public ChainBrandProfitsharingApis(
            ISenparcWeixinSettingForTenpayV3 setting = null)
        {
            _setting = setting ??
                Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 请求连锁品牌分账。
        /// <para>接收方姓名由 SDK 使用微信支付公钥或平台证书自动加密。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012692975</para>
        /// </summary>
        /// <param name="data">品牌主、出资商户、订单和最多 50 个分账接收方。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账单和各接收方执行结果。</returns>
        public async Task<ChainBrandProfitsharingOrderResultJson>
            CreateOrderAsync(
                ChainBrandProfitsharingCreateOrderRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));
            var receivers = data.receivers ??
                Array.Empty<ChainBrandProfitsharingReceiverRequestData>();
            foreach (var receiver in receivers)
            {
                if (receiver?.type == "MERCHANT_ID" &&
                    string.IsNullOrWhiteSpace(receiver.name))
                {
                    throw new TenpayApiRequestException(
                        "接收方类型为 MERCHANT_ID 时，name 必填。");
                }
            }

            var request = await CreateSensitiveRequestAsync(receivers
                .Where(receiver => receiver != null &&
                    !string.IsNullOrWhiteSpace(receiver.name))
                .Cast<object>()).ConfigureAwait(false);
            const string path = "v3/brand/profitsharing/orders";
            return await request.RequestAsync<
                ChainBrandProfitsharingOrderResultJson>(GetUrl(path), data,
                timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询连锁品牌分账结果。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012467002</para>
        /// </summary>
        /// <param name="data">出资商户号、微信订单号和商户分账单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账单状态和各接收方执行结果。</returns>
        public Task<ChainBrandProfitsharingOrderResultJson> QueryOrderAsync(
            ChainBrandProfitsharingOrderQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery("v3/brand/profitsharing/orders",
                "sub_mchid", data?.sub_mchid,
                "transaction_id", data?.transaction_id,
                "out_order_no", data?.out_order_no);
            return GetAsync<ChainBrandProfitsharingOrderResultJson>(path,
                timeOut);
        }

        /// <summary>
        /// 请求连锁品牌分账回退。
        /// <para>微信分账单号和商户分账单号二选一填写。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012467097</para>
        /// </summary>
        /// <param name="data">原分账单、回退单号、接收商户和回退金额。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账回退处理结果。</returns>
        public Task<ChainBrandProfitsharingReturnOrderResultJson>
            CreateReturnOrderAsync(
                ChainBrandProfitsharingReturnOrderRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/profitsharing/returnorders";
            return PostAsync<ChainBrandProfitsharingReturnOrderResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 查询连锁品牌分账回退结果。
        /// <para>微信分账单号和商户分账单号二选一填写。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012467011</para>
        /// </summary>
        /// <param name="data">出资商户号、回退单号和原分账单标识。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账回退最终状态。</returns>
        public Task<ChainBrandProfitsharingReturnOrderResultJson>
            QueryReturnOrderAsync(
                ChainBrandProfitsharingReturnOrderQueryRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery("v3/brand/profitsharing/returnorders",
                "sub_mchid", data?.sub_mchid,
                "out_return_no", data?.out_return_no,
                "order_id", data?.order_id,
                "out_order_no", data?.out_order_no);
            return GetAsync<ChainBrandProfitsharingReturnOrderResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 解冻订单剩余资金并完结连锁品牌分账。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012467016</para>
        /// </summary>
        /// <param name="data">出资商户、微信订单号、商户分账单号和完结原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已完结的微信分账单。</returns>
        public Task<ChainBrandProfitsharingFinishOrderResultJson>
            FinishOrderAsync(
                ChainBrandProfitsharingFinishOrderRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/profitsharing/finish-order";
            return PostAsync<ChainBrandProfitsharingFinishOrderResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 查询订单剩余待分金额。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012467021</para>
        /// </summary>
        /// <param name="transactionId">微信支付订单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>订单剩余待分金额，单位为分。</returns>
        public Task<ChainBrandProfitsharingAmountsResultJson>
            QueryAmountsAsync(string transactionId,
                int timeOut = Config.TIME_OUT)
        {
            var path = "v3/brand/profitsharing/orders/" +
                       $"{Escape(transactionId)}/amounts";
            return GetAsync<ChainBrandProfitsharingAmountsResultJson>(path,
                timeOut);
        }

        /// <summary>
        /// 查询品牌主允许服务商分账的最大比例。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012467022</para>
        /// </summary>
        /// <param name="brandMchid">品牌主商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>品牌主最大分账比例，单位为万分比。</returns>
        public Task<ChainBrandProfitsharingBrandConfigResultJson>
            QueryBrandConfigAsync(string brandMchid,
                int timeOut = Config.TIME_OUT)
        {
            var path = "v3/brand/profitsharing/brand-configs/" +
                       Escape(brandMchid);
            return GetAsync<ChainBrandProfitsharingBrandConfigResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 添加连锁品牌分账接收方。
        /// <para>接收方姓名由 SDK 使用微信支付公钥或平台证书自动加密。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012467100</para>
        /// </summary>
        /// <param name="data">品牌主、应用、接收方和关系信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已建立的分账接收方关系。</returns>
        public async Task<ChainBrandProfitsharingReceiverResultJson>
            AddReceiverAsync(
                ChainBrandProfitsharingAddReceiverRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));
            if (data.type == "MERCHANT_ID" &&
                string.IsNullOrWhiteSpace(data.name))
            {
                throw new TenpayApiRequestException(
                    "接收方类型为 MERCHANT_ID 时，name 必填。");
            }

            var request = await CreateSensitiveRequestAsync(new object[]
            {
                data
            }).ConfigureAwait(false);
            const string path =
                "v3/brand/profitsharing/receivers/add";
            return await request.RequestAsync<
                ChainBrandProfitsharingReceiverResultJson>(GetUrl(path),
                data, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除连锁品牌分账接收方。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012467103</para>
        /// </summary>
        /// <param name="data">品牌主、应用和待删除接收方信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已删除的分账接收方关系。</returns>
        public Task<ChainBrandProfitsharingReceiverResultJson>
            DeleteReceiverAsync(
                ChainBrandProfitsharingDeleteReceiverRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path =
                "v3/brand/profitsharing/receivers/delete";
            return PostAsync<ChainBrandProfitsharingReceiverResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 申请连锁品牌分账账单。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012715572</para>
        /// </summary>
        /// <param name="data">可选子商户、账单日期和压缩类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>摘要信息和 30 秒内有效的下载地址。</returns>
        public Task<ChainBrandProfitsharingBillResultJson> ApplyBillAsync(
            ChainBrandProfitsharingBillRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildQuery("v3/profitsharing/bills",
                "sub_mchid", data?.sub_mchid,
                "bill_date", data?.bill_date,
                "tar_type", data?.tar_type);
            return GetAsync<ChainBrandProfitsharingBillResultJson>(path,
                timeOut);
        }

        /// <summary>
        /// 下载并校验连锁品牌分账账单。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012076073</para>
        /// </summary>
        /// <param name="bill">申请账单接口返回的下载地址和摘要信息。</param>
        /// <param name="destination">接收账单文件的可写流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载及摘要校验均成功时为 <see langword="true"/>。</returns>
        public Task<bool> DownloadBillAsync(
            ChainBrandProfitsharingBillResultJson bill, Stream destination,
            int timeOut = Config.TIME_OUT)
        {
            return DownloadBillAsync(bill, destination,
                CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 异步下载并校验连锁品牌分账账单，并支持取消。
        /// </summary>
        /// <param name="bill">申请账单接口返回的下载地址和摘要信息。</param>
        /// <param name="destination">接收账单文件的可写流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载及摘要校验均成功时为 <see langword="true"/>。</returns>
        public Task<bool> DownloadBillAsync(
            ChainBrandProfitsharingBillResultJson bill, Stream destination,
            CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT)
        {
            _ = bill ?? throw new ArgumentNullException(nameof(bill));
            var request = new TenPayApiRequest(_setting);
            return TenPayDownloadHelper.DownloadAndVerifyAsync(request,
                bill.download_url, destination, bill.hash_type,
                bill.hash_value, timeOut, cancellationToken);
        }

        private Task<T> PostAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_setting);
            return request.RequestAsync<T>(GetUrl(path), data, timeOut);
        }

        private Task<T> GetAsync<T>(string path, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_setting);
            return request.RequestAsync<T>(GetUrl(path), null, timeOut,
                ApiRequestMethod.GET);
        }

        private async Task<TenPayApiRequest> CreateSensitiveRequestAsync(
            IEnumerable<object> targets)
        {
            var targetList = targets?
                .Where(target => target != null)
                .ToArray() ?? Array.Empty<object>();
            if (targetList.Length == 0)
            {
                return new TenPayApiRequest(_setting);
            }

            var publicKey = GetConfiguredPaymentPublicKey();
            if (string.IsNullOrWhiteSpace(publicKey.Key))
            {
                var publicKeys = await new BasePayApis(_setting)
                    .GetPublicKeysAsync().ConfigureAwait(false);
                publicKey = SelectPublicKey(publicKeys);
            }

            if (string.IsNullOrWhiteSpace(publicKey.Key) ||
                string.IsNullOrWhiteSpace(publicKey.Value))
            {
                throw new TenpayApiRequestException(
                    "未获取到用于加密分账接收方姓名的微信支付公钥或平台证书。");
            }

            foreach (var target in targetList)
            {
                SecurityHelper.FieldEncrypt(target, publicKey.Value,
                    _setting.EncryptionType.Value,
                    _setting.TenPayV3_TenPayPubKeyEnable);
            }

            return new TenPayApiRequest(_setting, httpClient =>
            {
                httpClient.DefaultRequestHeaders.Add("Wechatpay-Serial",
                    publicKey.Key);
            });
        }

        private KeyValuePair<string, string>
            GetConfiguredPaymentPublicKey()
        {
            if (!_setting.TenPayV3_TenPayPubKeyEnable)
            {
                return default;
            }

            return new KeyValuePair<string, string>(
                _setting.TenPayV3_TenPayPubKeyID,
                SecurityHelper.GetUnwrapCertKey(
                    _setting.TenPayV3_TenPayPubKey));
        }

        private KeyValuePair<string, string> SelectPublicKey(
            IReadOnlyDictionary<string, string> publicKeys)
        {
            if (publicKeys == null)
            {
                return default;
            }

            var publicKeyId = _setting.TenPayV3_TenPayPubKeyID;
            if (!string.IsNullOrWhiteSpace(publicKeyId) &&
                publicKeys.TryGetValue(publicKeyId,
                    out var configuredPublicKey))
            {
                return new KeyValuePair<string, string>(publicKeyId,
                    configuredPublicKey);
            }

            return publicKeys.FirstOrDefault();
        }

        private static string GetUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string BuildQuery(string path,
            params string[] query)
        {
            var parts = new List<string>();
            for (var index = 0; index + 1 < query.Length; index += 2)
            {
                if (string.IsNullOrEmpty(query[index + 1]))
                {
                    continue;
                }

                parts.Add($"{Escape(query[index])}=" +
                          Escape(query[index + 1]));
            }

            return parts.Count == 0
                ? path
                : $"{path}?{string.Join("&", parts)}";
        }

        private static string Escape(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
