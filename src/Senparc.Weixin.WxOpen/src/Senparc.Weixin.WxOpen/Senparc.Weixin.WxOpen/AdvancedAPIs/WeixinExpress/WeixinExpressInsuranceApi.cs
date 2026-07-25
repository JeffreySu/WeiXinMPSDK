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

    文件名：WeixinExpressInsuranceApi.cs
    文件功能描述：WeixinExpressInsuranceApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 微信物流无忧退货及运费险接口。
    /// </summary>
    public static partial class WeixinExpressApi
    {
        /// <summary>
        /// 开通无忧退货能力。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>开通结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_open"/>；支持第三方平台代商家调用。</remarks>
        public static WxJsonResult OpenInsuranceFreight(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/open", new { }, timeOut);
        }

        /// <summary>
        /// 查询无忧退货开通状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>是否已开通。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_query_open"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressInsuranceOpenStatusJsonResult QueryInsuranceFreightOpenStatus(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressInsuranceOpenStatusJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/query_open", new { }, timeOut);
        }

        /// <summary>
        /// 发货时为订单投保无忧退货。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">买家、支付、运单、地址及商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>保单、止期、预估理赔金额及保费。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_createorder"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressInsuranceCreateOrderJsonResult CreateInsuranceFreightOrder(string accessTokenOrAppId, WeixinExpressInsuranceCreateOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressInsuranceCreateOrderJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/createorder", request, timeOut);
        }

        /// <summary>
        /// 收到用户退货后申请无忧退货理赔。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">买家、支付单、退货运单和快递公司。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>理赔报案号及上门取件标志。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_claim"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressInsuranceClaimJsonResult ClaimInsuranceFreight(string accessTokenOrAppId, WeixinExpressInsuranceClaimRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressInsuranceClaimJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/claim", request, timeOut);
        }

        /// <summary>
        /// 申请无忧退货保费充值订单号。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">充值金额，单位为分。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>充值订单 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_createchargeid"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressInsuranceCreateChargeJsonResult CreateInsuranceChargeId(string accessTokenOrAppId, WeixinExpressInsuranceCreateChargeRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressInsuranceCreateChargeJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/createchargeid", request, timeOut);
        }

        /// <summary>
        /// 为无忧退货充值订单申请支付链接。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">充值订单 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信服务市场充值链接。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_applypay"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressInsuranceApplyPayJsonResult ApplyInsurancePay(string accessTokenOrAppId, WeixinExpressInsuranceApplyPayRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressInsuranceApplyPayJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/applypay", request, timeOut);
        }

        /// <summary>
        /// 拉取无忧退货保费充值订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">订单状态和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>充值订单列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_getpayorderlist"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressInsurancePayOrderListJsonResult GetInsurancePayOrderList(string accessTokenOrAppId, WeixinExpressInsurancePayOrderListRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressInsurancePayOrderListJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/getpayorderlist", request, timeOut);
        }

        /// <summary>
        /// 发起无忧退货充值保费退款。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退款申请结果。</returns>
        /// <remarks>官方当前接口不接收请求体字段。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_refund"/>；支持第三方平台代商家调用。</remarks>
        public static WxJsonResult RefundInsurancePremium(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/refund", new { }, timeOut);
        }

        /// <summary>
        /// 拉取指定时间范围内的无忧退货理赔摘要。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">开始和结束时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>投保、理赔、保费和余额摘要。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_getsummary"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressInsuranceSummaryJsonResult GetInsuranceSummary(string accessTokenOrAppId, WeixinExpressInsuranceSummaryRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressInsuranceSummaryJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/getsummary", request, timeOut);
        }

        /// <summary>
        /// 按支付单、保单、报案、运单、时间或状态拉取无忧退货保单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">保单筛选、分页和排序条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>保单列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_getorderlist"/>；支持第三方平台代商家调用。</remarks>
        public static WeixinExpressInsuranceOrderListJsonResult GetInsuranceOrderList(string accessTokenOrAppId, WeixinExpressInsuranceOrderListRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressInsuranceOrderListJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/getorderlist", request, timeOut);
        }

        /// <summary>
        /// 设置无忧退货保费余额告警阈值。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">告警余额，单位为分；0 表示关闭通知。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_update_notify_funds"/>；支持第三方平台代商家调用。</remarks>
        public static WxJsonResult UpdateInsuranceNotifyFunds(string accessTokenOrAppId, WeixinExpressInsuranceNotifyFundsRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/update_notify_funds", request, timeOut);
        }

        /// <summary>
        /// 异步开通无忧退货能力。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>开通结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_open"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WxJsonResult> OpenInsuranceFreightAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/open", new { }, timeOut);
        }

        /// <summary>
        /// 异步查询无忧退货开通状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>是否已开通。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_query_open"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressInsuranceOpenStatusJsonResult> QueryInsuranceFreightOpenStatusAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressInsuranceOpenStatusJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/query_open", new { }, timeOut);
        }

        /// <summary>
        /// 异步在发货时为订单投保无忧退货。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">买家、支付、运单、地址及商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>保单、止期、预估理赔金额及保费。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_createorder"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressInsuranceCreateOrderJsonResult> CreateInsuranceFreightOrderAsync(string accessTokenOrAppId, WeixinExpressInsuranceCreateOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressInsuranceCreateOrderJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/createorder", request, timeOut);
        }

        /// <summary>
        /// 异步申请无忧退货理赔。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">买家、支付单、退货运单和快递公司。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>理赔报案号及上门取件标志。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_claim"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressInsuranceClaimJsonResult> ClaimInsuranceFreightAsync(string accessTokenOrAppId, WeixinExpressInsuranceClaimRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressInsuranceClaimJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/claim", request, timeOut);
        }

        /// <summary>
        /// 异步申请无忧退货保费充值订单号。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">充值金额，单位为分。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>充值订单 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_createchargeid"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressInsuranceCreateChargeJsonResult> CreateInsuranceChargeIdAsync(string accessTokenOrAppId, WeixinExpressInsuranceCreateChargeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressInsuranceCreateChargeJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/createchargeid", request, timeOut);
        }

        /// <summary>
        /// 异步为无忧退货充值订单申请支付链接。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">充值订单 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信服务市场充值链接。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_applypay"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressInsuranceApplyPayJsonResult> ApplyInsurancePayAsync(string accessTokenOrAppId, WeixinExpressInsuranceApplyPayRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressInsuranceApplyPayJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/applypay", request, timeOut);
        }

        /// <summary>
        /// 异步拉取无忧退货保费充值订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">订单状态和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>充值订单列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_getpayorderlist"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressInsurancePayOrderListJsonResult> GetInsurancePayOrderListAsync(string accessTokenOrAppId, WeixinExpressInsurancePayOrderListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressInsurancePayOrderListJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/getpayorderlist", request, timeOut);
        }

        /// <summary>
        /// 异步发起无忧退货充值保费退款。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退款申请结果。</returns>
        /// <remarks>官方当前接口不接收请求体字段。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_refund"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WxJsonResult> RefundInsurancePremiumAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/refund", new { }, timeOut);
        }

        /// <summary>
        /// 异步拉取指定时间范围内的无忧退货理赔摘要。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">开始和结束时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>投保、理赔、保费和余额摘要。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_getsummary"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressInsuranceSummaryJsonResult> GetInsuranceSummaryAsync(string accessTokenOrAppId, WeixinExpressInsuranceSummaryRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressInsuranceSummaryJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/getsummary", request, timeOut);
        }

        /// <summary>
        /// 异步拉取无忧退货保单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">保单筛选、分页和排序条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>保单列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_getorderlist"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WeixinExpressInsuranceOrderListJsonResult> GetInsuranceOrderListAsync(string accessTokenOrAppId, WeixinExpressInsuranceOrderListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressInsuranceOrderListJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/getorderlist", request, timeOut);
        }

        /// <summary>
        /// 异步设置无忧退货保费余额告警阈值。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">告警余额，单位为分；0 表示关闭通知。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/freight/api_insurance_freight_update_notify_funds"/>；支持第三方平台代商家调用。</remarks>
        public static Task<WxJsonResult> UpdateInsuranceNotifyFundsAsync(string accessTokenOrAppId, WeixinExpressInsuranceNotifyFundsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/business/insurance_freight/update_notify_funds", request, timeOut);
        }
    }
}
