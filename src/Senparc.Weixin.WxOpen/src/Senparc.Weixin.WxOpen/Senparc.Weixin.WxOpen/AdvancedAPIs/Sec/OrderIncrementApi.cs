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

    文件名：OrderIncrementApi.cs
    文件功能描述：OrderIncrementApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    /// <summary>
    /// 小程序交易管理服务增量接口。
    /// </summary>
    public static partial class Order
    {
        private static readonly JsonSetting IncrementIgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>
        /// 对未发货订单进行特殊发货报备。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">订单号、报备类型和可选的预计发货时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>报备结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/order_shipping/api_opspecialorder"/>。本接口支持第三方平台代商家调用。</remarks>
        public static WxJsonResult ReportSpecialOrder(string accessTokenOrAppId, SpecialOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendIncrement<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/order/opspecialorder", request, timeOut);
        }

        /// <summary>
        /// 异步对未发货订单进行特殊发货报备。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">订单号、报备类型和可选的预计发货时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>报备结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/order_shipping/api_opspecialorder"/>。本接口支持第三方平台代商家调用。</remarks>
        public static Task<WxJsonResult> ReportSpecialOrderAsync(string accessTokenOrAppId, SpecialOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendIncrementAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/order/opspecialorder", request, timeOut);
        }

        /// <summary>
        /// 提交小程序品牌申请。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">申请类型、品牌信息和证明材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/order_shipping/api_famousbrandapply"/>。本接口支持第三方平台代商家调用。</remarks>
        public static WxJsonResult ApplyFamousBrand(string accessTokenOrAppId, FamousBrandApplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendIncrement<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/famousbrand/apply", request, timeOut);
        }

        /// <summary>
        /// 异步提交小程序品牌申请。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">申请类型、品牌信息和证明材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/order_shipping/api_famousbrandapply"/>。本接口支持第三方平台代商家调用。</remarks>
        public static Task<WxJsonResult> ApplyFamousBrandAsync(string accessTokenOrAppId, FamousBrandApplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendIncrementAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/famousbrand/apply", request, timeOut);
        }

        /// <summary>
        /// 查询小程序品牌申请状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>整体进度、申请类型和审核原因。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/order_shipping/api_getfamousbrandapplystatus"/>。本接口支持第三方平台代商家调用。</remarks>
        public static FamousBrandStatusJsonResult GetFamousBrandApplyStatus(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendIncrement<FamousBrandStatusJsonResult>(accessTokenOrAppId, "/wxa/sec/famousbrand/get_status", new { }, timeOut);
        }

        /// <summary>
        /// 异步查询小程序品牌申请状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>整体进度、申请类型和审核原因。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/order_shipping/api_getfamousbrandapplystatus"/>。本接口支持第三方平台代商家调用。</remarks>
        public static Task<FamousBrandStatusJsonResult> GetFamousBrandApplyStatusAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendIncrementAsync<FamousBrandStatusJsonResult>(accessTokenOrAppId, "/wxa/sec/famousbrand/get_status", new { }, timeOut);
        }

        /// <summary>
        /// 申请变更小程序交易类型。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">目标交易类型、申请材料和理由。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/order_shipping/api_setwxatradetypecgi"/>。本接口支持第三方平台代商家调用。</remarks>
        public static WxJsonResult ApplyTradeTypeChange(string accessTokenOrAppId, TradeTypeChangeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendIncrement<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/order/setwxatradetypecgi", request, timeOut);
        }

        /// <summary>
        /// 异步申请变更小程序交易类型。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">目标交易类型、申请材料和理由。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/order_shipping/api_setwxatradetypecgi"/>。本接口支持第三方平台代商家调用。</remarks>
        public static Task<WxJsonResult> ApplyTradeTypeChangeAsync(string accessTokenOrAppId, TradeTypeChangeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendIncrementAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/order/setwxatradetypecgi", request, timeOut);
        }

        private static T SendIncrement<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return CommonJsonSend.Send<T>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IncrementIgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendIncrementAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return await CommonJsonSend.SendAsync<T>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IncrementIgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }
    }
}
