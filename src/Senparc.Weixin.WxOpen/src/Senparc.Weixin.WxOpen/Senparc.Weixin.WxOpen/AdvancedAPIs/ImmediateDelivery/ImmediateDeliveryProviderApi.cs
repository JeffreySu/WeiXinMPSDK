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

    文件名：ImmediateDeliveryProviderApi.cs
    文件功能描述：ImmediateDeliveryProviderApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.ImmediateDelivery
{
    /// <summary>
    /// 小程序即时配送运力方接口。
    /// </summary>
    /// <remarks>
    /// 官方运力方接口不支持第三方平台代调用；请传入运力方小程序 AccessToken 或已注册的 AppId。
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class ImmediateDeliveryProviderApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>
        /// 更新即时配送单的接单、取货、配送、退回或取消状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">微信下单 Token、订单状态、配送单、商户和骑手信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运力方状态更新结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-provider/api_updateorder"/>。
        /// 最终成功状态为 302；最终失败状态包括 103、203、204、205、401、501、502。
        /// </remarks>
        public static ImmediateDeliveryJsonResult UpdateOrderStatus(string accessTokenOrAppId, ImmediateDeliveryProviderUpdateOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + "/cgi-bin/express/local/delivery/update_order?access_token={0}";
                return CommonJsonSend.Send<ImmediateDeliveryJsonResult>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 异步更新即时配送单的接单、取货、配送、退回或取消状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">微信下单 Token、订单状态、配送单、商户和骑手信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运力方状态更新结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/immediate-delivery/deliver-by-provider/api_updateorder"/>。
        /// 最终成功状态为 302；最终失败状态包括 103、203、204、205、401、501、502。
        /// </remarks>
        public static Task<ImmediateDeliveryJsonResult> UpdateOrderStatusAsync(string accessTokenOrAppId, ImmediateDeliveryProviderUpdateOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiMpHost + "/cgi-bin/express/local/delivery/update_order?access_token={0}";
                return await CommonJsonSend.SendAsync<ImmediateDeliveryJsonResult>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }
    }
}
