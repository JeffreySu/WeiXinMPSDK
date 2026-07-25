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

    文件名：B2BOrderApi.cs
    文件功能描述：B2BOrderApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.B2B
{
    /// <summary>
    /// 小程序 B2B 门店助手订单、退款与资金接口。
    /// </summary>
    public static partial class B2BApi
    {
        #region 订单与退款

        /// <summary>
        /// 查询 B2B 支付订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号及商户订单号或 B2B 支付订单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>订单状态、金额、支付和结算信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_getorder"/>。</remarks>
        public static B2BGetOrderJsonResult GetOrder(string accessTokenOrAppId, string paySig, B2BOrderIdentityRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetOrderJsonResult>(accessTokenOrAppId, "/retail/B2b/getorder", request, timeOut, paySig);
        }

        /// <summary>异步查询 B2B 支付订单。</summary>
        /// <inheritdoc cref="GetOrder"/>
        public static Task<B2BGetOrderJsonResult> GetOrderAsync(string accessTokenOrAppId, string paySig, B2BOrderIdentityRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetOrderJsonResult>(accessTokenOrAppId, "/retail/B2b/getorder", request, timeOut, paySig);
        }

        /// <summary>
        /// 关闭尚未支付的 B2B 订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号及商户订单号或 B2B 支付订单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>关单结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_closeb2border"/>。</remarks>
        public static WxJsonResult CloseOrder(string accessTokenOrAppId, string paySig, B2BOrderIdentityRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/closeb2border", request, timeOut, paySig);
        }

        /// <summary>异步关闭尚未支付的 B2B 订单。</summary>
        /// <inheritdoc cref="CloseOrder"/>
        public static Task<WxJsonResult> CloseOrderAsync(string accessTokenOrAppId, string paySig, B2BOrderIdentityRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/closeb2border", request, timeOut, paySig);
        }

        /// <summary>
        /// 发起 B2B 订单退款。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">原订单标识、退款单号、金额和原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>B2B 退款单号和原订单标识。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_refundorder"/>。</remarks>
        public static B2BRefundJsonResult RefundOrder(string accessTokenOrAppId, string paySig, B2BRefundRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BRefundJsonResult>(accessTokenOrAppId, "/retail/B2b/refund", request, timeOut, paySig);
        }

        /// <summary>异步发起 B2B 订单退款。</summary>
        /// <inheritdoc cref="RefundOrder"/>
        public static Task<B2BRefundJsonResult> RefundOrderAsync(string accessTokenOrAppId, string paySig, B2BRefundRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BRefundJsonResult>(accessTokenOrAppId, "/retail/B2b/refund", request, timeOut, paySig);
        }

        /// <summary>
        /// 查询 B2B 退款状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号以及商户退款单号或 B2B 支付退款单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退款状态、金额、渠道及技术服务费回退信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_getrefund"/>。</remarks>
        public static B2BGetRefundJsonResult GetRefund(string accessTokenOrAppId, string paySig, B2BGetRefundRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetRefundJsonResult>(accessTokenOrAppId, "/retail/B2b/getrefund", request, timeOut, paySig);
        }

        /// <summary>异步查询 B2B 退款状态。</summary>
        /// <inheritdoc cref="GetRefund"/>
        public static Task<B2BGetRefundJsonResult> GetRefundAsync(string accessTokenOrAppId, string paySig, B2BGetRefundRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetRefundJsonResult>(accessTokenOrAppId, "/retail/B2b/getrefund", request, timeOut, paySig);
        }

        #endregion

        #region 密钥、账单与资金

        /// <summary>
        /// 获取 B2B 支付正式环境和沙箱环境 AppKey。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">微信商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>正式环境和沙箱环境 AppKey。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_getappkey"/>。该接口的官方 URL 不要求 <c>pay_sig</c>。</remarks>
        public static B2BGetAppKeyJsonResult GetAppKey(string accessTokenOrAppId, B2BMerchantIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetAppKeyJsonResult>(accessTokenOrAppId, "/retail/B2b/getappkey", request, timeOut);
        }

        /// <summary>异步获取 B2B 支付正式环境和沙箱环境 AppKey。</summary>
        /// <inheritdoc cref="GetAppKey"/>
        public static Task<B2BGetAppKeyJsonResult> GetAppKeyAsync(string accessTokenOrAppId, B2BMerchantIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetAppKeyJsonResult>(accessTokenOrAppId, "/retail/B2b/getappkey", request, timeOut);
        }

        /// <summary>
        /// 获取 B2B 交易账单与资金账单下载链接。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号和 yyyyMMdd 格式账单日期。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>交易、退款、资金、分账和银行转账账单链接。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_downloadbill"/>。</remarks>
        public static B2BDownloadBillJsonResult DownloadBill(string accessTokenOrAppId, string paySig, B2BDownloadBillRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BDownloadBillJsonResult>(accessTokenOrAppId, "/retail/B2b/downloadbill", request, timeOut, paySig);
        }

        /// <summary>异步获取 B2B 交易账单与资金账单下载链接。</summary>
        /// <inheritdoc cref="DownloadBill"/>
        public static Task<B2BDownloadBillJsonResult> DownloadBillAsync(string accessTokenOrAppId, string paySig, B2BDownloadBillRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BDownloadBillJsonResult>(accessTokenOrAppId, "/retail/B2b/downloadbill", request, timeOut, paySig);
        }

        /// <summary>
        /// 查询 B2B 商户账户余额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">微信商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可提现金额和待结算金额。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_getmchbalance"/>。</remarks>
        public static B2BGetMerchantBalanceJsonResult GetMerchantBalance(string accessTokenOrAppId, string paySig, B2BMerchantIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetMerchantBalanceJsonResult>(accessTokenOrAppId, "/retail/B2b/getmchbalance", request, timeOut, paySig);
        }

        /// <summary>异步查询 B2B 商户账户余额。</summary>
        /// <inheritdoc cref="GetMerchantBalance"/>
        public static Task<B2BGetMerchantBalanceJsonResult> GetMerchantBalanceAsync(string accessTokenOrAppId, string paySig, B2BMerchantIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetMerchantBalanceJsonResult>(accessTokenOrAppId, "/retail/B2b/getmchbalance", request, timeOut, paySig);
        }

        /// <summary>
        /// 发起 B2B 商户手动提现。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号、提现金额和外部提现单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提现申请结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_manualwithdraw"/>。</remarks>
        public static WxJsonResult Withdraw(string accessTokenOrAppId, string paySig, B2BWithdrawRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/withdraw", request, timeOut, paySig);
        }

        /// <summary>异步发起 B2B 商户手动提现。</summary>
        /// <inheritdoc cref="Withdraw"/>
        public static Task<WxJsonResult> WithdrawAsync(string accessTokenOrAppId, string paySig, B2BWithdrawRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/withdraw", request, timeOut, paySig);
        }

        /// <summary>
        /// 查询 B2B 商户提现状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号和外部提现单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提现金额、状态和失败原因。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_querywithdraw"/>。</remarks>
        public static B2BQueryWithdrawJsonResult QueryWithdraw(string accessTokenOrAppId, string paySig, B2BQueryWithdrawRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BQueryWithdrawJsonResult>(accessTokenOrAppId, "/retail/B2b/querywithdraw", request, timeOut, paySig);
        }

        /// <summary>异步查询 B2B 商户提现状态。</summary>
        /// <inheritdoc cref="QueryWithdraw"/>
        public static Task<B2BQueryWithdrawJsonResult> QueryWithdrawAsync(string accessTokenOrAppId, string paySig, B2BQueryWithdrawRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BQueryWithdrawJsonResult>(accessTokenOrAppId, "/retail/B2b/querywithdraw", request, timeOut, paySig);
        }

        /// <summary>
        /// 开启或关闭 B2B 微信支付自动提现，并设置留存额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号、自动提现状态和可选留存金额。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_setautowithdraw"/>。</remarks>
        public static WxJsonResult SetAutoWithdraw(string accessTokenOrAppId, string paySig, B2BSetAutoWithdrawRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/setautowithdraw", request, timeOut, paySig);
        }

        /// <summary>异步开启或关闭 B2B 微信支付自动提现，并设置留存额。</summary>
        /// <inheritdoc cref="SetAutoWithdraw"/>
        public static Task<WxJsonResult> SetAutoWithdrawAsync(string accessTokenOrAppId, string paySig, B2BSetAutoWithdrawRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/setautowithdraw", request, timeOut, paySig);
        }

        #endregion
    }
}
