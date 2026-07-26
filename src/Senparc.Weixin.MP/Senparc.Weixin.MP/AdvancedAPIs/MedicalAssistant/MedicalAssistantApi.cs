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

    文件名：MedicalAssistantApi.cs
    文件功能描述：MedicalAssistantApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.MedicalAssistant
{
    /// <summary>
    /// 微信就医助手接口。
    /// </summary>
    /// <remarks>
    /// 仅适用于已开通微信就医助手能力的公立医院或卫健委公众号，并且必须使用同主体公众号的 AccessToken。
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class MedicalAssistantApi
    {
        /// <summary>
        /// 推送微信就医助手消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">同主体公众号 AccessToken 或 AppId。</param>
        /// <param name="request">就医助手消息请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 同一就医流程应保持 <see cref="SendChannelMessageRequest{TBusinessInfo}.order_id"/> 不变，
        /// 同一用户和订单下的 <see cref="SendChannelMessageRequest{TBusinessInfo}.msg_id"/> 必须唯一。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/medicalassistant/api_cityservice_sendchannelmsg"/>。
        /// </remarks>
        public static WxJsonResult SendChannelMessage(string accessTokenOrAppId, SendChannelMessageRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendChannelMessage<MedicalAssistantBusinessInfo>(accessTokenOrAppId, request, timeOut);
        }

        /// <summary>
        /// 使用自定义状态业务信息推送微信就医助手消息。
        /// </summary>
        /// <typeparam name="TBusinessInfo">当前消息状态对应的业务信息类型。</typeparam>
        /// <param name="accessTokenOrAppId">同主体公众号 AccessToken 或 AppId。</param>
        /// <param name="request">就医助手消息请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 当官方为特定 <c>status</c> 定义了不同的 <c>business_info</c> 结构时，可通过泛型请求模型完整传递该结构。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/medicalassistant/api_cityservice_sendchannelmsg"/>。
        /// </remarks>
        public static WxJsonResult SendChannelMessage<TBusinessInfo>(string accessTokenOrAppId, SendChannelMessageRequest<TBusinessInfo> request, int timeOut = Config.TIME_OUT)
            where TBusinessInfo : class
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cityservice/sendchannelmsg?access_token={0}", accessToken);
                return CommonJsonSend.Send<WxJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 异步推送微信就医助手消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">同主体公众号 AccessToken 或 AppId。</param>
        /// <param name="request">就医助手消息请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 同一就医流程应保持 <see cref="SendChannelMessageRequest{TBusinessInfo}.order_id"/> 不变，
        /// 同一用户和订单下的 <see cref="SendChannelMessageRequest{TBusinessInfo}.msg_id"/> 必须唯一。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/medicalassistant/api_cityservice_sendchannelmsg"/>。
        /// </remarks>
        public static Task<WxJsonResult> SendChannelMessageAsync(string accessTokenOrAppId, SendChannelMessageRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendChannelMessageAsync<MedicalAssistantBusinessInfo>(accessTokenOrAppId, request, timeOut);
        }

        /// <summary>
        /// 使用自定义状态业务信息异步推送微信就医助手消息。
        /// </summary>
        /// <typeparam name="TBusinessInfo">当前消息状态对应的业务信息类型。</typeparam>
        /// <param name="accessTokenOrAppId">同主体公众号 AccessToken 或 AppId。</param>
        /// <param name="request">就医助手消息请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 当官方为特定 <c>status</c> 定义了不同的 <c>business_info</c> 结构时，可通过泛型请求模型完整传递该结构。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/medicalassistant/api_cityservice_sendchannelmsg"/>。
        /// </remarks>
        public static async Task<WxJsonResult> SendChannelMessageAsync<TBusinessInfo>(string accessTokenOrAppId, SendChannelMessageRequest<TBusinessInfo> request, int timeOut = Config.TIME_OUT)
            where TBusinessInfo : class
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cityservice/sendchannelmsg?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
    }
}
