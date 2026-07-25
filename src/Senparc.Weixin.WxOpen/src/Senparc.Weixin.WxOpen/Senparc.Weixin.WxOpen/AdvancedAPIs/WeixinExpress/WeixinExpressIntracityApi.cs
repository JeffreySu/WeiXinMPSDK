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

    文件名：WeixinExpressIntracityApi.cs
    文件功能描述：WeixinExpressIntracityApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 微信物流服务同城配送接口。
    /// </summary>
    /// <remarks>本文件中的接口均支持使用 authorizer_access_token 由第三方平台代商家调用。</remarks>
    public static partial class WeixinExpressApi
    {
        #region 同城配送同步方法

        /// <summary>
        /// 申请开通微信物流同城配送能力。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_apply"/>。</remarks>
        public static WxJsonResult IntracityApply(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/apply", new { }, timeOut);
        }

        /// <summary>
        /// 创建同城配送门店。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店名称、运力偏好和发货地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信门店编号和商家自定义门店编号。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_createstore"/>。</remarks>
        public static WeixinExpressIntracityCreateStoreJsonResult IntracityCreateStore(string accessTokenOrAppId, WeixinExpressIntracityCreateStoreRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityCreateStoreJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/createstore", request, timeOut);
        }

        /// <summary>
        /// 查询同城配送门店；不指定门店编号时返回全部门店。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选的微信门店编号或商家自定义门店编号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>门店列表及总数。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_querystore"/>。</remarks>
        public static WeixinExpressIntracityQueryStoreJsonResult IntracityQueryStore(string accessTokenOrAppId, WeixinExpressIntracityQueryStoreRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityQueryStoreJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/querystore", request, timeOut);
        }

        /// <summary>
        /// 更新同城配送门店信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店定位条件和更新内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_updatestore"/>。</remarks>
        public static WxJsonResult IntracityUpdateStore(string accessTokenOrAppId, WeixinExpressIntracityUpdateStoreRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/updatestore", request, timeOut);
        }

        /// <summary>
        /// 为同城配送门店、小程序或服务商充值。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">充值主体、运力和金额。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>充值页面地址及门店信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_storecharge"/>。</remarks>
        public static WeixinExpressIntracityStoreChargeJsonResult IntracityStoreCharge(string accessTokenOrAppId, WeixinExpressIntracityStoreChargeRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityStoreChargeJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/storecharge", request, timeOut);
        }

        /// <summary>
        /// 退回同城配送未使用余额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">退款主体、门店和运力信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>实际退款金额。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_store_refund"/>。</remarks>
        public static WeixinExpressIntracityStoreRefundJsonResult IntracityStoreRefund(string accessTokenOrAppId, WeixinExpressIntracityStoreRefundRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityStoreRefundJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/storerefund", request, timeOut);
        }

        /// <summary>
        /// 查询同城配送充值、消费或退款流水。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店、流水类型、时间范围和扣费主体。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>资金流水及汇总金额。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_queryflow"/>。</remarks>
        public static WeixinExpressIntracityQueryFlowJsonResult IntracityQueryFlow(string accessTokenOrAppId, WeixinExpressIntracityQueryFlowRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityQueryFlowJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/queryflow", request, timeOut);
        }

        /// <summary>
        /// 查询同城配送门店、小程序或服务商余额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">余额主体、门店及可选的运力 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>总余额、分运力余额和充值订单。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_balancequery"/>。</remarks>
        public static WeixinExpressIntracityBalanceQueryJsonResult IntracityBalanceQuery(string accessTokenOrAppId, WeixinExpressIntracityBalanceQueryRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityBalanceQueryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/balancequery", request, timeOut);
        }

        /// <summary>
        /// 预下同城配送订单并查询实时运费和配送距离。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店、收件人、地址、商品和沙箱信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预估运力、配送距离和运费；最终费用以下单接口为准。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_preaddorder"/>。</remarks>
        public static WeixinExpressIntracityPreAddOrderJsonResult IntracityPreAddOrder(string accessTokenOrAppId, WeixinExpressIntracityPreAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityPreAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/preaddorder", request, timeOut);
        }

        /// <summary>
        /// 创建同城配送订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店订单、收件人、回调、验证码和商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信订单号、运力、运单号、距离和配送费。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_addorder"/>。</remarks>
        public static WeixinExpressIntracityAddOrderJsonResult IntracityAddOrder(string accessTokenOrAppId, WeixinExpressIntracityAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/addorder", request, timeOut);
        }

        /// <summary>
        /// 查询同城配送订单详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">微信订单号，或成对填写的微信门店编号和商家门店订单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>订单状态、费用、配送员、门店、收件人和商品信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_queryorder"/>。</remarks>
        public static WeixinExpressIntracityQueryOrderJsonResult IntracityQueryOrder(string accessTokenOrAppId, WeixinExpressIntracityQueryOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityQueryOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/queryorder", request, timeOut);
        }

        /// <summary>
        /// 取消同城配送订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">订单定位条件及取消原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>取消后的订单状态和违约金。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_cancelorder"/>。</remarks>
        public static WeixinExpressIntracityCancelOrderJsonResult IntracityCancelOrder(string accessTokenOrAppId, WeixinExpressIntracityCancelOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityCancelOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/cancelorder", request, timeOut);
        }

        /// <summary>
        /// 设置同城配送扣费主体。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">小程序 AppId 和扣费主体。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_setpaymode"/>。</remarks>
        public static WxJsonResult IntracitySetPayMode(string accessTokenOrAppId, WeixinExpressIntracitySetPayModeRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/setpaymode", request, timeOut);
        }

        /// <summary>
        /// 查询同城配送扣费主体。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">与 AccessToken 匹配的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>扣费模式及对应的扣费 AppId。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_getpaymode"/>。</remarks>
        public static WeixinExpressIntracityGetPayModeJsonResult IntracityGetPayMode(string accessTokenOrAppId, WeixinExpressIntracityGetPayModeRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityGetPayModeJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/getpaymode", request, timeOut);
        }

        /// <summary>
        /// 查询运力支持同城配送的城市。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选的运力 ID；不填写时返回全部运力。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运力及其支持的城市列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_getcity"/>。</remarks>
        public static WeixinExpressIntracityGetCityJsonResult IntracityGetCity(string accessTokenOrAppId, WeixinExpressIntracityGetCityRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressIntracityGetCityJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/getcity", request, timeOut);
        }

        /// <summary>
        /// 在测试场景模拟同城配送订单状态回调。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">订单定位条件和要模拟的状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>模拟回调触发结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_mocknotify"/>。</remarks>
        public static WxJsonResult IntracityMockNotify(string accessTokenOrAppId, WeixinExpressIntracityMockNotifyRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/mocknotify", request, timeOut);
        }

        #endregion

        #region 同城配送异步方法

        /// <summary>
        /// 异步申请开通微信物流同城配送能力。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_apply"/>。</remarks>
        public static Task<WxJsonResult> IntracityApplyAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/apply", new { }, timeOut);
        }

        /// <summary>
        /// 异步创建同城配送门店。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店名称、运力偏好和发货地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信门店编号和商家自定义门店编号。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_createstore"/>。</remarks>
        public static Task<WeixinExpressIntracityCreateStoreJsonResult> IntracityCreateStoreAsync(string accessTokenOrAppId, WeixinExpressIntracityCreateStoreRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityCreateStoreJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/createstore", request, timeOut);
        }

        /// <summary>
        /// 异步查询同城配送门店；不指定门店编号时返回全部门店。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选的微信门店编号或商家自定义门店编号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>门店列表及总数。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_querystore"/>。</remarks>
        public static Task<WeixinExpressIntracityQueryStoreJsonResult> IntracityQueryStoreAsync(string accessTokenOrAppId, WeixinExpressIntracityQueryStoreRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityQueryStoreJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/querystore", request, timeOut);
        }

        /// <summary>
        /// 异步更新同城配送门店信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店定位条件和更新内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_updatestore"/>。</remarks>
        public static Task<WxJsonResult> IntracityUpdateStoreAsync(string accessTokenOrAppId, WeixinExpressIntracityUpdateStoreRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/updatestore", request, timeOut);
        }

        /// <summary>
        /// 异步为同城配送门店、小程序或服务商充值。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">充值主体、运力和金额。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>充值页面地址及门店信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_storecharge"/>。</remarks>
        public static Task<WeixinExpressIntracityStoreChargeJsonResult> IntracityStoreChargeAsync(string accessTokenOrAppId, WeixinExpressIntracityStoreChargeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityStoreChargeJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/storecharge", request, timeOut);
        }

        /// <summary>
        /// 异步退回同城配送未使用余额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">退款主体、门店和运力信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>实际退款金额。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_store_refund"/>。</remarks>
        public static Task<WeixinExpressIntracityStoreRefundJsonResult> IntracityStoreRefundAsync(string accessTokenOrAppId, WeixinExpressIntracityStoreRefundRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityStoreRefundJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/storerefund", request, timeOut);
        }

        /// <summary>
        /// 异步查询同城配送充值、消费或退款流水。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店、流水类型、时间范围和扣费主体。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>资金流水及汇总金额。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_queryflow"/>。</remarks>
        public static Task<WeixinExpressIntracityQueryFlowJsonResult> IntracityQueryFlowAsync(string accessTokenOrAppId, WeixinExpressIntracityQueryFlowRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityQueryFlowJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/queryflow", request, timeOut);
        }

        /// <summary>
        /// 异步查询同城配送门店、小程序或服务商余额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">余额主体、门店及可选的运力 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>总余额、分运力余额和充值订单。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_balancequery"/>。</remarks>
        public static Task<WeixinExpressIntracityBalanceQueryJsonResult> IntracityBalanceQueryAsync(string accessTokenOrAppId, WeixinExpressIntracityBalanceQueryRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityBalanceQueryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/balancequery", request, timeOut);
        }

        /// <summary>
        /// 异步预下同城配送订单并查询实时运费和配送距离。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店、收件人、地址、商品和沙箱信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预估运力、配送距离和运费；最终费用以下单接口为准。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_preaddorder"/>。</remarks>
        public static Task<WeixinExpressIntracityPreAddOrderJsonResult> IntracityPreAddOrderAsync(string accessTokenOrAppId, WeixinExpressIntracityPreAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityPreAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/preaddorder", request, timeOut);
        }

        /// <summary>
        /// 异步创建同城配送订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">门店订单、收件人、回调、验证码和商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信订单号、运力、运单号、距离和配送费。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_addorder"/>。</remarks>
        public static Task<WeixinExpressIntracityAddOrderJsonResult> IntracityAddOrderAsync(string accessTokenOrAppId, WeixinExpressIntracityAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/addorder", request, timeOut);
        }

        /// <summary>
        /// 异步查询同城配送订单详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">微信订单号，或成对填写的微信门店编号和商家门店订单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>订单状态、费用、配送员、门店、收件人和商品信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_queryorder"/>。</remarks>
        public static Task<WeixinExpressIntracityQueryOrderJsonResult> IntracityQueryOrderAsync(string accessTokenOrAppId, WeixinExpressIntracityQueryOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityQueryOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/queryorder", request, timeOut);
        }

        /// <summary>
        /// 异步取消同城配送订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">订单定位条件及取消原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>取消后的订单状态和违约金。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_cancelorder"/>。</remarks>
        public static Task<WeixinExpressIntracityCancelOrderJsonResult> IntracityCancelOrderAsync(string accessTokenOrAppId, WeixinExpressIntracityCancelOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityCancelOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/cancelorder", request, timeOut);
        }

        /// <summary>
        /// 异步设置同城配送扣费主体。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">小程序 AppId 和扣费主体。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_setpaymode"/>。</remarks>
        public static Task<WxJsonResult> IntracitySetPayModeAsync(string accessTokenOrAppId, WeixinExpressIntracitySetPayModeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/setpaymode", request, timeOut);
        }

        /// <summary>
        /// 异步查询同城配送扣费主体。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">与 AccessToken 匹配的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>扣费模式及对应的扣费 AppId。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_getpaymode"/>。</remarks>
        public static Task<WeixinExpressIntracityGetPayModeJsonResult> IntracityGetPayModeAsync(string accessTokenOrAppId, WeixinExpressIntracityGetPayModeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityGetPayModeJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/getpaymode", request, timeOut);
        }

        /// <summary>
        /// 异步查询运力支持同城配送的城市。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选的运力 ID；不填写时返回全部运力。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运力及其支持的城市列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_getcity"/>。</remarks>
        public static Task<WeixinExpressIntracityGetCityJsonResult> IntracityGetCityAsync(string accessTokenOrAppId, WeixinExpressIntracityGetCityRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressIntracityGetCityJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/getcity", request, timeOut);
        }

        /// <summary>
        /// 异步在测试场景模拟同城配送订单状态回调。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">订单定位条件和要模拟的状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>模拟回调触发结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/same_city_distribution/api_intracity_mocknotify"/>。</remarks>
        public static Task<WxJsonResult> IntracityMockNotifyAsync(string accessTokenOrAppId, WeixinExpressIntracityMockNotifyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/intracity/mocknotify", request, timeOut);
        }

        #endregion
    }
}
