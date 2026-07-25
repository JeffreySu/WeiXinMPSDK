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

    文件名：EcommerceApis.CrossBorder.cs
    文件功能描述：微信支付 V3 电商收付通跨境付款接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐可出境余额、资金出境、结果查询及购付汇账单接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 微信支付 V3 电商收付通跨境付款接口。
    /// </summary>
    public partial class EcommerceApis
    {
        /// <summary>
        /// 查询微信支付订单剩余可出境金额。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476109</para>
        /// </summary>
        /// <param name="transactionId">微信支付订单号。</param>
        /// <param name="subMchId">申请资金出境的二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付订单号和剩余可出境金额。</returns>
        public Task<EcommerceFundsToOverseaAvailableAmountResultJson>
            QueryFundsToOverseaAvailableAmountAsync(string transactionId,
                string subMchId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/funds-to-oversea/transactions/{EscapeFundsToOverseaValue(transactionId)}/available_abroad_amounts";
            path = BuildFundsToOverseaQuery(path, "sub_mchid", subMchId);
            return GetFundsToOverseaAsync<EcommerceFundsToOverseaAvailableAmountResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 申请将微信支付订单资金出境至指定境外收款币种。
        /// <para>接口请求成功仅表示受理成功，应继续调用查询出境结果接口确认最终状态。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476113</para>
        /// </summary>
        /// <param name="data">出境单号、二级商户、金额、币种、商品、卖家、物流和收款人信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>资金出境受理状态、金额、汇率、手续费及失败原因。</returns>
        public Task<EcommerceFundsToOverseaOrderResultJson>
            ApplyFundsToOverseaAsync(EcommerceFundsToOverseaRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/funds-to-oversea/orders";
            return PostFundsToOverseaAsync<EcommerceFundsToOverseaOrderResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 按商户出境单号查询资金出境结果。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476127</para>
        /// </summary>
        /// <param name="outOrderId">申请资金出境时生成的商户出境单号。</param>
        /// <param name="subMchId">申请资金出境的二级商户号。</param>
        /// <param name="transactionId">资金出境对应的微信支付订单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>资金出境最终状态、金额、汇率、手续费及失败原因。</returns>
        public Task<EcommerceFundsToOverseaOrderResultJson>
            QueryFundsToOverseaOrderAsync(string outOrderId,
                string subMchId, string transactionId,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/funds-to-oversea/orders/{EscapeFundsToOverseaValue(outOrderId)}";
            path = BuildFundsToOverseaQuery(path,
                "sub_mchid", subMchId,
                "transaction_id", transactionId);
            return GetFundsToOverseaAsync<EcommerceFundsToOverseaOrderResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 获取指定日期的购付汇账单文件下载链接。
        /// <para>下载链接需使用微信支付 API v3 规则签名，可复用 <see cref="DownloadEcommerceBillAsync(string, System.IO.Stream, int)"/> 下载原始文件。</para>
        /// <para>若下载文件为 gzip，应先解压，再使用返回的摘要校验原始账单完整性。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476132</para>
        /// </summary>
        /// <param name="data">账单日期及可选的二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>账单文件摘要类型、摘要值和签名下载地址。</returns>
        public Task<EcommerceFundsToOverseaBillResultJson>
            QueryFundsToOverseaBillDownloadUrlAsync(
                EcommerceFundsToOverseaBillRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path = BuildFundsToOverseaQuery(
                "v3/funds-to-oversea/bill-download-url",
                "bill_date", data?.bill_date,
                "sub_mchid", data?.sub_mchid);
            return GetFundsToOverseaAsync<EcommerceFundsToOverseaBillResultJson>(
                path, timeOut);
        }

        private Task<T> PostFundsToOverseaAsync<T>(string path, object data,
            int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetFundsToOverseaUrl(path), data,
                timeOut);
        }

        private Task<T> GetFundsToOverseaAsync<T>(string path, int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetFundsToOverseaUrl(path), null,
                timeOut, ApiRequestMethod.GET);
        }

        private static string GetFundsToOverseaUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string BuildFundsToOverseaQuery(string path,
            params string[] query)
        {
            var parts = new List<string>();
            for (var index = 0; index + 1 < query.Length; index += 2)
            {
                if (string.IsNullOrEmpty(query[index + 1]))
                {
                    continue;
                }

                parts.Add($"{EscapeFundsToOverseaValue(query[index])}=" +
                          $"{EscapeFundsToOverseaValue(query[index + 1])}");
            }

            return parts.Count == 0
                ? path
                : $"{path}?{string.Join("&", parts)}";
        }

        private static string EscapeFundsToOverseaValue(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
