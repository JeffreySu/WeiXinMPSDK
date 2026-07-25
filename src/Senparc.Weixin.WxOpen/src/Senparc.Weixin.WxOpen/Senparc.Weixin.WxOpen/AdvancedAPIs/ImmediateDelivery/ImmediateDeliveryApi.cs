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

    文件名：ImmediateDeliveryApi.cs
    文件功能描述：ImmediateDeliveryApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.ImmediateDelivery
{
    /// <summary>
    /// 小程序即时配送商户侧接口。
    /// </summary>
    /// <remarks>
    /// 官方商户侧接口支持第三方平台代商家调用；调用方可以传入小程序 AccessToken、
    /// authorizer_access_token，或已在 AccessToken 容器注册的小程序 AppId。
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class ImmediateDeliveryApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 同步方法

        /// <summary>
        /// 获取微信即时配送已支持的配送公司列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>配送公司列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_getallimmedelivery"/>。</remarks>
        public static ImmediateDeliveryCompanyListJsonResult GetAllDeliveryCompanies(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryCompanyListJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/delivery/getall", new { }, timeOut);
        }

        /// <summary>
        /// 预下即时配送单并获取运费、预计接单时间和配送令牌。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">发收件人、货物、订单及配送公司信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预下单价格和配送令牌。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_preaddorder"/>。</remarks>
        public static ImmediateDeliveryPreAddOrderJsonResult PreAddOrder(string accessTokenOrAppId, ImmediateDeliveryPreAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryPreAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/pre_add", request, timeOut);
        }

        /// <summary>
        /// 拉取小程序已绑定的配送公司账号。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已绑定账号及审核状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_getbindaccount"/>。</remarks>
        public static ImmediateDeliveryBoundAccountListJsonResult GetBoundAccounts(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryBoundAccountListJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/shop/get", new { }, timeOut);
        }

        /// <summary>
        /// 预取消配送单，查询预计违约金而不直接取消订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待取消订单、配送公司及原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预计违约金和说明。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_precancelorder"/>。</remarks>
        public static ImmediateDeliveryCancelOrderJsonResult PreCancelOrder(string accessTokenOrAppId, ImmediateDeliveryPreCancelOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryCancelOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/precancel", request, timeOut);
        }

        /// <summary>
        /// 由第三方平台代商户申请开通即时配送权限。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>开通申请结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_opendelivery"/>。</remarks>
        public static ImmediateDeliveryJsonResult OpenDelivery(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/open", new { }, timeOut);
        }

        /// <summary>
        /// 发起绑定配送公司账号的请求。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待绑定的配送公司。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>绑定请求结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_bindlocalaccount"/>。</remarks>
        public static ImmediateDeliveryJsonResult BindAccount(string accessTokenOrAppId, ImmediateDeliveryBindAccountRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/shop/add", request, timeOut);
        }

        /// <summary>
        /// 对已经取消、过期或投递异常的同一商户订单重新下单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">完整订单和预下单配送令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新的配送单信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_reorder"/>。</remarks>
        public static ImmediateDeliveryAddOrderJsonResult ReAddOrder(string accessTokenOrAppId, ImmediateDeliveryAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/readd", request, timeOut);
        }

        /// <summary>
        /// 在运力测试环境模拟配送公司更新真实测试订单状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商户订单、状态、时间及配送签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>模拟更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_realmockupdateorder"/>。</remarks>
        public static ImmediateDeliveryJsonResult RealMockUpdateOrder(string accessTokenOrAppId, ImmediateDeliveryRealMockUpdateOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/realmock_update_order", request, timeOut);
        }

        /// <summary>
        /// 在微信沙盒环境模拟配送公司更新配送单状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">测试商户订单和配送状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>沙盒模拟更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_mockupdateorder"/>。</remarks>
        public static ImmediateDeliveryJsonResult MockUpdateOrder(string accessTokenOrAppId, ImmediateDeliveryMockUpdateOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/test_update_order", request, timeOut);
        }

        /// <summary>
        /// 拉取指定即时配送单的状态及骑手信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商家、订单、门店及配送签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>配送状态、配送单号和骑手位置。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_getlocalorder"/>。</remarks>
        public static ImmediateDeliveryGetOrderJsonResult GetOrder(string accessTokenOrAppId, ImmediateDeliveryGetOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryGetOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/get", request, timeOut);
        }

        /// <summary>
        /// 确认异常配送件已退回商家。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商户订单、配送单、门店和签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>确认结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_abnormalconfirm"/>。</remarks>
        public static ImmediateDeliveryJsonResult ConfirmReturn(string accessTokenOrAppId, ImmediateDeliveryConfirmReturnRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/confirm_return", request, timeOut);
        }

        /// <summary>
        /// 取消即时配送单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待取消订单和取消原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>取消结果和实际违约金。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_cancellocalorder"/>。</remarks>
        public static ImmediateDeliveryCancelOrderJsonResult CancelOrder(string accessTokenOrAppId, ImmediateDeliveryCancelOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryCancelOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/cancel", request, timeOut);
        }

        /// <summary>
        /// 为待接单状态的即时配送单添加小费。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">配送单、小费、备注及签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>添加小费结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_addtips"/>。</remarks>
        public static ImmediateDeliveryJsonResult AddTips(string accessTokenOrAppId, ImmediateDeliveryAddTipsRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/addtips", request, timeOut);
        }

        /// <summary>
        /// 添加即时配送单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">完整订单及预下单配送令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>配送单号、费用、状态和取收货码。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_addlocalorder"/>。</remarks>
        public static ImmediateDeliveryAddOrderJsonResult AddOrder(string accessTokenOrAppId, ImmediateDeliveryAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<ImmediateDeliveryAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/add", request, timeOut);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 异步获取微信即时配送已支持的配送公司列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>配送公司列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_getallimmedelivery"/>。</remarks>
        public static Task<ImmediateDeliveryCompanyListJsonResult> GetAllDeliveryCompaniesAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryCompanyListJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/delivery/getall", new { }, timeOut);
        }

        /// <summary>
        /// 异步预下即时配送单并获取运费、预计接单时间和配送令牌。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">发收件人、货物、订单及配送公司信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预下单价格和配送令牌。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_preaddorder"/>。</remarks>
        public static Task<ImmediateDeliveryPreAddOrderJsonResult> PreAddOrderAsync(string accessTokenOrAppId, ImmediateDeliveryPreAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryPreAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/pre_add", request, timeOut);
        }

        /// <summary>
        /// 异步拉取小程序已绑定的配送公司账号。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已绑定账号及审核状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_getbindaccount"/>。</remarks>
        public static Task<ImmediateDeliveryBoundAccountListJsonResult> GetBoundAccountsAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryBoundAccountListJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/shop/get", new { }, timeOut);
        }

        /// <summary>
        /// 异步预取消配送单，查询预计违约金而不直接取消订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待取消订单、配送公司及原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>预计违约金和说明。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_precancelorder"/>。</remarks>
        public static Task<ImmediateDeliveryCancelOrderJsonResult> PreCancelOrderAsync(string accessTokenOrAppId, ImmediateDeliveryPreCancelOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryCancelOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/precancel", request, timeOut);
        }

        /// <summary>
        /// 异步由第三方平台代商户申请开通即时配送权限。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>开通申请结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_opendelivery"/>。</remarks>
        public static Task<ImmediateDeliveryJsonResult> OpenDeliveryAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/open", new { }, timeOut);
        }

        /// <summary>
        /// 异步发起绑定配送公司账号的请求。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待绑定的配送公司。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>绑定请求结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_bindlocalaccount"/>。</remarks>
        public static Task<ImmediateDeliveryJsonResult> BindAccountAsync(string accessTokenOrAppId, ImmediateDeliveryBindAccountRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/shop/add", request, timeOut);
        }

        /// <summary>
        /// 异步对已经取消、过期或投递异常的同一商户订单重新下单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">完整订单和预下单配送令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新的配送单信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_reorder"/>。</remarks>
        public static Task<ImmediateDeliveryAddOrderJsonResult> ReAddOrderAsync(string accessTokenOrAppId, ImmediateDeliveryAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/readd", request, timeOut);
        }

        /// <summary>
        /// 异步在运力测试环境模拟配送公司更新真实测试订单状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商户订单、状态、时间及配送签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>模拟更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_realmockupdateorder"/>。</remarks>
        public static Task<ImmediateDeliveryJsonResult> RealMockUpdateOrderAsync(string accessTokenOrAppId, ImmediateDeliveryRealMockUpdateOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/realmock_update_order", request, timeOut);
        }

        /// <summary>
        /// 异步在微信沙盒环境模拟配送公司更新配送单状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">测试商户订单和配送状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>沙盒模拟更新结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_mockupdateorder"/>。</remarks>
        public static Task<ImmediateDeliveryJsonResult> MockUpdateOrderAsync(string accessTokenOrAppId, ImmediateDeliveryMockUpdateOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/test_update_order", request, timeOut);
        }

        /// <summary>
        /// 异步拉取指定即时配送单的状态及骑手信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商家、订单、门店及配送签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>配送状态、配送单号和骑手位置。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_getlocalorder"/>。</remarks>
        public static Task<ImmediateDeliveryGetOrderJsonResult> GetOrderAsync(string accessTokenOrAppId, ImmediateDeliveryGetOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryGetOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/get", request, timeOut);
        }

        /// <summary>
        /// 异步确认异常配送件已退回商家。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商户订单、配送单、门店和签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>确认结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_abnormalconfirm"/>。</remarks>
        public static Task<ImmediateDeliveryJsonResult> ConfirmReturnAsync(string accessTokenOrAppId, ImmediateDeliveryConfirmReturnRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/confirm_return", request, timeOut);
        }

        /// <summary>
        /// 异步取消即时配送单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待取消订单和取消原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>取消结果和实际违约金。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_cancellocalorder"/>。</remarks>
        public static Task<ImmediateDeliveryCancelOrderJsonResult> CancelOrderAsync(string accessTokenOrAppId, ImmediateDeliveryCancelOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryCancelOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/cancel", request, timeOut);
        }

        /// <summary>
        /// 异步为待接单状态的即时配送单添加小费。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">配送单、小费、备注及签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>添加小费结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_addtips"/>。</remarks>
        public static Task<ImmediateDeliveryJsonResult> AddTipsAsync(string accessTokenOrAppId, ImmediateDeliveryAddTipsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/addtips", request, timeOut);
        }

        /// <summary>
        /// 异步添加即时配送单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">完整订单及预下单配送令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>配送单号、费用、状态和取收货码。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-business/api_addlocalorder"/>。</remarks>
        public static Task<ImmediateDeliveryAddOrderJsonResult> AddOrderAsync(string accessTokenOrAppId, ImmediateDeliveryAddOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<ImmediateDeliveryAddOrderJsonResult>(accessTokenOrAppId, "/cgi-bin/express/local/business/order/add", request, timeOut);
        }

        #endregion

        private static T Send<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return CommonJsonSend.Send<T>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return await CommonJsonSend.SendAsync<T>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }
    }
}
