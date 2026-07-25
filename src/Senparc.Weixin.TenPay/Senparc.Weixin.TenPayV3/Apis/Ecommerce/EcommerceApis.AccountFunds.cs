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

    文件名：EcommerceApis.AccountFunds.cs
    文件功能描述：微信支付 V3 电商收付通账户资金管理接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐余额查询、商户提现、异常文件及通知模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 微信支付 V3 电商收付通账户资金管理接口。
    /// </summary>
    public partial class EcommerceApis
    {
        /// <summary>
        /// 查询二级商户账户实时余额。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476690</para>
        /// </summary>
        /// <param name="subMchId">二级商户号。</param>
        /// <param name="accountType">账户类型，可选 BASIC、FEES、OPERATION 或 DEPOSIT。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二级商户的可用余额、不可用余额和账户类型。</returns>
        public Task<EcommerceSubMerchantBalanceResultJson>
            QuerySubMerchantBalanceAsync(string subMchId,
                string accountType = null, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/fund/balance/{EscapeAccountFundsValue(subMchId)}";
            path = BuildAccountFundsQuery(path, "account_type", accountType);
            return GetAccountFundsAsync<EcommerceSubMerchantBalanceResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 查询二级商户指定日期的日终余额。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476693</para>
        /// </summary>
        /// <param name="subMchId">二级商户号。</param>
        /// <param name="date">查询日期，格式为 yyyy-MM-dd。</param>
        /// <param name="accountType">账户类型，可选 BASIC 或 DEPOSIT。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>指定日期的二级商户日终余额。</returns>
        public Task<EcommerceSubMerchantBalanceResultJson>
            QuerySubMerchantDayEndBalanceAsync(string subMchId, string date,
                string accountType = null, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/fund/enddaybalance/{EscapeAccountFundsValue(subMchId)}";
            path = BuildAccountFundsQuery(path, "date", date,
                "account_type", accountType);
            return GetAccountFundsAsync<EcommerceSubMerchantBalanceResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 查询平台账户实时余额。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476700</para>
        /// </summary>
        /// <param name="accountType">平台账户类型：BASIC、OPERATION 或 FEES。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>平台账户的可用余额和不可用余额。</returns>
        public Task<EcommercePlatformBalanceResultJson> QueryPlatformBalanceAsync(
            string accountType, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/merchant/fund/balance/{EscapeAccountFundsValue(accountType)}";
            return GetAccountFundsAsync<EcommercePlatformBalanceResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 查询平台账户指定日期的日终余额。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476702</para>
        /// </summary>
        /// <param name="accountType">平台账户类型：BASIC、OPERATION 或 FEES。</param>
        /// <param name="date">查询日期，格式为 yyyy-MM-dd；不传时按官方默认日期查询。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>平台账户的日终可用余额和不可用余额。</returns>
        public Task<EcommercePlatformBalanceResultJson>
            QueryPlatformDayEndBalanceAsync(string accountType,
                string date = null, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/merchant/fund/dayendbalance/{EscapeAccountFundsValue(accountType)}";
            path = BuildAccountFundsQuery(path, "date", date);
            return GetAccountFundsAsync<EcommercePlatformBalanceResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 为二级商户提交预约提现申请。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476652</para>
        /// </summary>
        /// <param name="data">二级商户号、提现单号、金额、出款账户及通知地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付预约提现单号、商户提现单号和二级商户号。</returns>
        public Task<EcommerceSubMerchantWithdrawalApplyResultJson>
            SubmitSubMerchantWithdrawalAsync(
                EcommerceSubMerchantWithdrawalRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/ecommerce/fund/withdraw";
            return PostAccountFundsAsync<EcommerceSubMerchantWithdrawalApplyResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 按商户预约提现单号查询二级商户提现状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476656</para>
        /// </summary>
        /// <param name="outRequestNo">商户自定义的预约提现单号。</param>
        /// <param name="subMchId">二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二级商户预约提现的状态和入账信息。</returns>
        public Task<EcommerceSubMerchantWithdrawalQueryResultJson>
            QuerySubMerchantWithdrawalByOutRequestNoAsync(
                string outRequestNo, string subMchId,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/fund/withdraw/out-request-no/{EscapeAccountFundsValue(outRequestNo)}";
            path = BuildAccountFundsQuery(path, "sub_mchid", subMchId);
            return GetAccountFundsAsync<EcommerceSubMerchantWithdrawalQueryResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 按微信支付预约提现单号查询二级商户提现状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476665</para>
        /// </summary>
        /// <param name="withdrawId">微信支付生成的预约提现单号。</param>
        /// <param name="subMchId">二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二级商户预约提现的状态和入账信息。</returns>
        public Task<EcommerceSubMerchantWithdrawalQueryResultJson>
            QuerySubMerchantWithdrawalByWithdrawIdAsync(string withdrawId,
                string subMchId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/fund/withdraw/{EscapeAccountFundsValue(withdrawId)}";
            path = BuildAccountFundsQuery(path, "sub_mchid", subMchId);
            return GetAccountFundsAsync<EcommerceSubMerchantWithdrawalQueryResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 提交平台账户预约提现申请。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476670</para>
        /// </summary>
        /// <param name="data">提现单号、金额、出款账户、备注及通知地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付预约提现单号和商户提现单号。</returns>
        public Task<EcommercePlatformWithdrawalApplyResultJson>
            SubmitPlatformWithdrawalAsync(
                EcommercePlatformWithdrawalRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/merchant/fund/withdraw";
            return PostAccountFundsAsync<EcommercePlatformWithdrawalApplyResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 按商户预约提现单号查询平台提现状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476672</para>
        /// </summary>
        /// <param name="outRequestNo">商户自定义的预约提现单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>平台预约提现的状态、失败处理方案及入账信息。</returns>
        public Task<EcommercePlatformWithdrawalQueryResultJson>
            QueryPlatformWithdrawalByOutRequestNoAsync(string outRequestNo,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/merchant/fund/withdraw/out-request-no/{EscapeAccountFundsValue(outRequestNo)}";
            return GetAccountFundsAsync<EcommercePlatformWithdrawalQueryResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 按微信支付预约提现单号查询平台提现状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476674</para>
        /// </summary>
        /// <param name="withdrawId">微信支付生成的预约提现单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>平台预约提现的状态、失败处理方案及入账信息。</returns>
        public Task<EcommercePlatformWithdrawalQueryResultJson>
            QueryPlatformWithdrawalByWithdrawIdAsync(string withdrawId,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/merchant/fund/withdraw/withdraw-id/{EscapeAccountFundsValue(withdrawId)}";
            return GetAccountFundsAsync<EcommercePlatformWithdrawalQueryResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 为二级商户按日终余额提交预约提现申请。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4013328143</para>
        /// </summary>
        /// <param name="data">二级商户号、计算方式、预留金额、备注及通知地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>日终余额提现单的完整受理状态。</returns>
        public Task<EcommerceSubMerchantDayEndWithdrawalResultJson>
            SubmitSubMerchantDayEndWithdrawalAsync(
                EcommerceSubMerchantDayEndWithdrawalRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path =
                "v3/platsolution/ecommerce/withdraw/day-end-balance-withdraw";
            return PostAccountFundsAsync<EcommerceSubMerchantDayEndWithdrawalResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 查询二级商户按日终余额预约提现状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4013328163</para>
        /// </summary>
        /// <param name="outRequestNo">商户自定义的预约提现单号。</param>
        /// <param name="subMchId">二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>日终余额提现的总额、成功额、失败额和退票额。</returns>
        public Task<EcommerceSubMerchantDayEndWithdrawalResultJson>
            QuerySubMerchantDayEndWithdrawalAsync(string outRequestNo,
                string subMchId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/platsolution/ecommerce/withdraw/day-end-balance-withdraw/out-request-no/{EscapeAccountFundsValue(outRequestNo)}";
            path = BuildAccountFundsQuery(path, "sub_mchid", subMchId);
            return GetAccountFundsAsync<EcommerceSubMerchantDayEndWithdrawalResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 获取指定日期的提现异常文件下载地址及摘要。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476678</para>
        /// </summary>
        /// <param name="billType">账单类型，当前取值为 NO_SUCC。</param>
        /// <param name="billDate">账单日期，格式为 yyyy-MM-dd。</param>
        /// <param name="tarType">压缩类型，可选 GZIP。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载地址、摘要算法和摘要值。</returns>
        public Task<EcommerceWithdrawalAbnormalBillResultJson>
            QueryWithdrawalAbnormalBillAsync(string billType, string billDate,
                string tarType = null, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/merchant/fund/withdraw/bill-type/{EscapeAccountFundsValue(billType)}";
            path = BuildAccountFundsQuery(path, "bill_date", billDate,
                "tar_type", tarType);
            return GetAccountFundsAsync<EcommerceWithdrawalAbnormalBillResultJson>(
                path, timeOut);
        }

        private Task<T> PostAccountFundsAsync<T>(string path, object data,
            int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetAccountFundsUrl(path), data,
                timeOut);
        }

        private Task<T> GetAccountFundsAsync<T>(string path, int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetAccountFundsUrl(path), null,
                timeOut, ApiRequestMethod.GET);
        }

        private static string GetAccountFundsUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string EscapeAccountFundsValue(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }

        private static string BuildAccountFundsQuery(string path,
            params string[] namesAndValues)
        {
            var query = new List<string>();
            for (var index = 0; index + 1 < namesAndValues.Length; index += 2)
            {
                var value = namesAndValues[index + 1];
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                query.Add(
                    $"{EscapeAccountFundsValue(namesAndValues[index])}={EscapeAccountFundsValue(value)}");
            }

            return query.Count == 0
                ? path
                : $"{path}?{string.Join("&", query)}";
        }
    }
}
