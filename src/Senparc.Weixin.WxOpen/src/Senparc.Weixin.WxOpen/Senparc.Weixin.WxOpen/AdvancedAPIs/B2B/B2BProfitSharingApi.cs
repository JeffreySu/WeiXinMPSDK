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

    文件名：B2BProfitSharingApi.cs
    文件功能描述：B2BProfitSharingApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.B2B
{
    /// <summary>
    /// 小程序 B2B 门店助手分账接口。
    /// </summary>
    public static partial class B2BApi
    {
        #region 分账接收方

        /// <summary>
        /// 添加 B2B 分账接收方。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">接收方关系、类型、标识和可选名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>添加结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_addprofitsharingaccount"/>。</remarks>
        public static WxJsonResult AddProfitSharingAccount(string accessTokenOrAppId, string paySig, B2BAddProfitSharingAccountRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/addprofitsharingaccount", request, timeOut, paySig);
        }

        /// <summary>异步添加 B2B 分账接收方。</summary>
        /// <inheritdoc cref="AddProfitSharingAccount"/>
        public static Task<WxJsonResult> AddProfitSharingAccountAsync(string accessTokenOrAppId, string paySig, B2BAddProfitSharingAccountRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/addprofitsharingaccount", request, timeOut, paySig);
        }

        /// <summary>
        /// 删除 B2B 分账接收方。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">接收方类型及 OpenId 或商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_delprofitsharingaccount"/>。</remarks>
        public static WxJsonResult DeleteProfitSharingAccount(string accessTokenOrAppId, string paySig, B2BDeleteProfitSharingAccountRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/delprofitsharingaccount", request, timeOut, paySig);
        }

        /// <summary>异步删除 B2B 分账接收方。</summary>
        /// <inheritdoc cref="DeleteProfitSharingAccount"/>
        public static Task<WxJsonResult> DeleteProfitSharingAccountAsync(string accessTokenOrAppId, string paySig, B2BDeleteProfitSharingAccountRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/delprofitsharingaccount", request, timeOut, paySig);
        }

        /// <summary>
        /// 分页查询 B2B 分账接收方。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">可选的偏移量和最大返回数量。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账接收方列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_queryprofitsharingaccount"/>。</remarks>
        public static B2BQueryProfitSharingAccountJsonResult QueryProfitSharingAccount(string accessTokenOrAppId, string paySig, B2BQueryProfitSharingAccountRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BQueryProfitSharingAccountJsonResult>(accessTokenOrAppId, "/retail/B2b/queryprofitsharingaccount", request, timeOut, paySig);
        }

        /// <summary>异步分页查询 B2B 分账接收方。</summary>
        /// <inheritdoc cref="QueryProfitSharingAccount"/>
        public static Task<B2BQueryProfitSharingAccountJsonResult> QueryProfitSharingAccountAsync(string accessTokenOrAppId, string paySig, B2BQueryProfitSharingAccountRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BQueryProfitSharingAccountJsonResult>(accessTokenOrAppId, "/retail/B2b/queryprofitsharingaccount", request, timeOut, paySig);
        }

        #endregion

        #region 分账订单

        /// <summary>
        /// 请求对 B2B 支付订单进行分账。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号、支付单号、金额和接收方。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账请求结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_createprofitsharingorder"/>。</remarks>
        public static WxJsonResult CreateProfitSharingOrder(string accessTokenOrAppId, string paySig, B2BCreateProfitSharingOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/createprofitsharingorder", request, timeOut, paySig);
        }

        /// <summary>异步请求对 B2B 支付订单进行分账。</summary>
        /// <inheritdoc cref="CreateProfitSharingOrder"/>
        public static Task<WxJsonResult> CreateProfitSharingOrderAsync(string accessTokenOrAppId, string paySig, B2BCreateProfitSharingOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/createprofitsharingorder", request, timeOut, paySig);
        }

        /// <summary>
        /// 查询 B2B 订单分账结果。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">支付单号、接收方和商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账状态及错误说明。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_queryprofitsharingorder"/>。</remarks>
        public static B2BQueryProfitSharingOrderJsonResult QueryProfitSharingOrder(string accessTokenOrAppId, string paySig, B2BQueryProfitSharingOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BQueryProfitSharingOrderJsonResult>(accessTokenOrAppId, "/retail/B2b/queryprofitsharingorder", request, timeOut, paySig);
        }

        /// <summary>异步查询 B2B 订单分账结果。</summary>
        /// <inheritdoc cref="QueryProfitSharingOrder"/>
        public static Task<B2BQueryProfitSharingOrderJsonResult> QueryProfitSharingOrderAsync(string accessTokenOrAppId, string paySig, B2BQueryProfitSharingOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BQueryProfitSharingOrderJsonResult>(accessTokenOrAppId, "/retail/B2b/queryprofitsharingorder", request, timeOut, paySig);
        }

        /// <summary>
        /// 查询 B2B 支付订单剩余可分账金额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号和原支付单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>剩余冻结金额，单位为分。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_queryprofitsharingremainamt"/>。</remarks>
        public static B2BQueryProfitSharingRemainingAmountJsonResult QueryProfitSharingRemainingAmount(string accessTokenOrAppId, string paySig, B2BProfitSharingOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BQueryProfitSharingRemainingAmountJsonResult>(accessTokenOrAppId, "/retail/B2b/queryprofitsharingremainamt", request, timeOut, paySig);
        }

        /// <summary>异步查询 B2B 支付订单剩余可分账金额。</summary>
        /// <inheritdoc cref="QueryProfitSharingRemainingAmount"/>
        public static Task<B2BQueryProfitSharingRemainingAmountJsonResult> QueryProfitSharingRemainingAmountAsync(string accessTokenOrAppId, string paySig, B2BProfitSharingOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BQueryProfitSharingRemainingAmountJsonResult>(accessTokenOrAppId, "/retail/B2b/queryprofitsharingremainamt", request, timeOut, paySig);
        }

        /// <summary>
        /// 完结 B2B 订单分账并解冻剩余资金。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">商户号和原支付单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>完结结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_finishprofitsharingorder"/>。</remarks>
        public static WxJsonResult FinishProfitSharingOrder(string accessTokenOrAppId, string paySig, B2BProfitSharingOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/finishprofitsharingorder", request, timeOut, paySig);
        }

        /// <summary>异步完结 B2B 订单分账并解冻剩余资金。</summary>
        /// <inheritdoc cref="FinishProfitSharingOrder"/>
        public static Task<WxJsonResult> FinishProfitSharingOrderAsync(string accessTokenOrAppId, string paySig, B2BProfitSharingOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/finishprofitsharingorder", request, timeOut, paySig);
        }

        /// <summary>
        /// 请求 B2B 分账回退。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">支付单、退款单、商户、接收方和回退金额。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账回退请求结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_refundprofitsharing"/>。</remarks>
        public static WxJsonResult RefundProfitSharing(string accessTokenOrAppId, string paySig, B2BRefundProfitSharingRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/refundprofitsharing", request, timeOut, paySig);
        }

        /// <summary>异步请求 B2B 分账回退。</summary>
        /// <inheritdoc cref="RefundProfitSharing"/>
        public static Task<WxJsonResult> RefundProfitSharingAsync(string accessTokenOrAppId, string paySig, B2BRefundProfitSharingRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/refundprofitsharing", request, timeOut, paySig);
        }

        /// <summary>
        /// 查询 B2B 分账回退结果。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="paySig">B2B 支付签名。</param>
        /// <param name="request">支付单、退款单、商户号和接收方。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分账回退状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_queryrefundprofitsharingorder"/>。</remarks>
        public static B2BQueryRefundProfitSharingJsonResult QueryRefundProfitSharingOrder(string accessTokenOrAppId, string paySig, B2BQueryRefundProfitSharingRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BQueryRefundProfitSharingJsonResult>(accessTokenOrAppId, "/retail/B2b/queryrefundprofitsharingorder", request, timeOut, paySig);
        }

        /// <summary>异步查询 B2B 分账回退结果。</summary>
        /// <inheritdoc cref="QueryRefundProfitSharingOrder"/>
        public static Task<B2BQueryRefundProfitSharingJsonResult> QueryRefundProfitSharingOrderAsync(string accessTokenOrAppId, string paySig, B2BQueryRefundProfitSharingRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BQueryRefundProfitSharingJsonResult>(accessTokenOrAppId, "/retail/B2b/queryrefundprofitsharingorder", request, timeOut, paySig);
        }

        #endregion
    }
}
