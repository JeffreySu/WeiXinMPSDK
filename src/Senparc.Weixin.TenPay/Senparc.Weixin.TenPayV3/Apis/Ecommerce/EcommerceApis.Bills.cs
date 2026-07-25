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

    文件名：EcommerceApis.Bills.cs
    文件功能描述：微信支付 V3 电商收付通账单申请与下载接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐电商二级商户资金账单申请和加密文件下载能力

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 微信支付 V3 电商收付通账单申请与下载接口。
    /// </summary>
    public partial class EcommerceApis
    {
        /// <summary>
        /// 申请电商平台全部二级商户资金账单。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012760697</para>
        /// </summary>
        /// <param name="data">账单日期、账户类型、加密算法和压缩类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>账单分片数量、下载地址、加密密钥和随机字符串。</returns>
        public Task<EcommerceFundflowBillResultJson>
            ApplyAllSubMerchantFundflowBillAsync(
                EcommerceAllSubMerchantFundflowBillRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path = BuildEcommerceBillQuery(
                "v3/ecommerce/bill/fundflowbill",
                "bill_date", data?.bill_date,
                "account_type", data?.account_type,
                "tar_type", data?.tar_type,
                "algorithm", data?.algorithm);
            return GetEcommerceBillAsync<EcommerceFundflowBillResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 申请指定单个子商户资金账单。
        /// <para>该入口只返回加密账单元数据，不会在解密前错误校验原始账单摘要。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012760249</para>
        /// </summary>
        /// <param name="data">子商户号、账单日期、账户类型、加密算法和压缩类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>账单分片数量、下载地址、加密密钥和随机字符串。</returns>
        public Task<EcommerceFundflowBillResultJson>
            ApplySingleSubMerchantFundflowBillAsync(
                EcommerceSingleSubMerchantFundflowBillRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path = BuildEcommerceBillQuery(
                "v3/bill/sub-merchant-fundflowbill",
                "sub_mchid", data?.sub_mchid,
                "bill_date", data?.bill_date,
                "account_type", data?.account_type,
                "algorithm", data?.algorithm,
                "tar_type", data?.tar_type);
            return GetEcommerceBillAsync<EcommerceFundflowBillResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 使用申请账单接口返回的签名下载地址获取账单文件。
        /// </summary>
        /// <param name="downloadUrl">申请账单接口返回、有效期为五分钟的下载地址。</param>
        /// <param name="destination">接收账单文件的可写流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件下载成功时为 <see langword="true"/>。</returns>
        public Task<bool> DownloadEcommerceBillAsync(string downloadUrl,
            Stream destination, int timeOut = Config.TIME_OUT)
        {
            return DownloadEcommerceBillAsync(downloadUrl, destination,
                CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 使用申请账单接口返回的签名下载地址异步获取账单文件。
        /// <para>电商二级商户资金账单为加密文件，本方法只下载原始内容；调用方应先解密，再使用返回元数据中的哈希值校验明文账单。</para>
        /// </summary>
        /// <param name="downloadUrl">申请账单接口返回、有效期为五分钟的下载地址。</param>
        /// <param name="destination">接收账单文件的可写流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件下载成功时为 <see langword="true"/>。</returns>
        public Task<bool> DownloadEcommerceBillAsync(string downloadUrl,
            Stream destination, CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT)
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return TenPayDownloadHelper.DownloadAndVerifyAsync(request,
                downloadUrl, destination, null, null, timeOut,
                cancellationToken);
        }

        private Task<T> GetEcommerceBillAsync<T>(string path, int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetEcommerceBillUrl(path), null,
                timeOut, ApiRequestMethod.GET);
        }

        private static string GetEcommerceBillUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string BuildEcommerceBillQuery(string path,
            params string[] query)
        {
            var parts = new List<string>();
            for (var index = 0; index + 1 < query.Length; index += 2)
            {
                if (string.IsNullOrEmpty(query[index + 1]))
                {
                    continue;
                }

                parts.Add($"{EscapeEcommerceBillValue(query[index])}=" +
                          $"{EscapeEcommerceBillValue(query[index + 1])}");
            }

            return parts.Count == 0
                ? path
                : $"{path}?{string.Join("&", parts)}";
        }

        private static string EscapeEcommerceBillValue(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
