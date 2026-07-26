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

    文件名：ServiceMarketApi.cs
    文件功能描述：ServiceMarketApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.ServiceMarket
{
    /// <summary>微信服务市场调用接口。</summary>
    public static class ServiceMarketApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>调用服务市场上架的同步或异步 API。</summary>
        /// <typeparam name="TData">服务提供方定义的 JSON 数据结构。</typeparam>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">服务 ID、接口名、业务数据和唯一消息 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>同步回包字符串，或异步请求 ID。</returns>
        /// <remarks>本接口支持第三方平台代调用，权限集 ID 为 66 或 67，并支持微信二次加密请求。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/wx-service-market/api_invokeservice"/>。</remarks>
        public static ServiceMarketInvokeJsonResult InvokeService<TData>(string accessTokenOrAppId, ServiceMarketInvokeRequest<TData> request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<ServiceMarketInvokeJsonResult>(accessTokenOrAppId, "/wxa/servicemarket", request, timeOut);
        }

        /// <summary>异步调用服务市场上架的同步或异步 API。</summary>
        /// <inheritdoc cref="InvokeService{TData}"/>
        public static Task<ServiceMarketInvokeJsonResult> InvokeServiceAsync<TData>(string accessTokenOrAppId, ServiceMarketInvokeRequest<TData> request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<ServiceMarketInvokeJsonResult>(accessTokenOrAppId, "/wxa/servicemarket", request, timeOut);
        }

        /// <summary>获取服务市场异步 API 的处理结果。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">异步调用返回的唯一 RequestId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>异步请求 ID 及处理结果 JSON 字符串。</returns>
        /// <remarks>本接口不支持云调用，但支持第三方平台代调用，权限集 ID 为 66 或 67。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/wx-service-market/api_servicemarketretrieve"/>。</remarks>
        public static ServiceMarketInvokeJsonResult RetrieveResult(string accessTokenOrAppId, ServiceMarketRetrieveRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<ServiceMarketInvokeJsonResult>(accessTokenOrAppId, "/wxa/servicemarketretrieve", request, timeOut);
        }

        /// <summary>异步获取服务市场异步 API 的处理结果。</summary>
        /// <inheritdoc cref="RetrieveResult"/>
        public static Task<ServiceMarketInvokeJsonResult> RetrieveResultAsync(string accessTokenOrAppId, ServiceMarketRetrieveRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<ServiceMarketInvokeJsonResult>(accessTokenOrAppId, "/wxa/servicemarketretrieve", request, timeOut);
        }

        private static T SendPost<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : Weixin.Entities.WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", request,
                    CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting), accessTokenOrAppId);
        }

        private static Task<T> SendPostAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : Weixin.Entities.WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", request,
                    CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false), accessTokenOrAppId);
        }
    }
}
