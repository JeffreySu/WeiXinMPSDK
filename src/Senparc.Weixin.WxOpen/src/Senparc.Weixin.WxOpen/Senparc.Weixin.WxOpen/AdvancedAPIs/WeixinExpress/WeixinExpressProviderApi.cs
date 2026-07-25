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

    文件名：WeixinExpressProviderApi.cs
    文件功能描述：WeixinExpressProviderApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 微信物流服务运力方消息推送接口。
    /// </summary>
    /// <remarks>
    /// 官方明确本类接口不支持第三方平台代调用。
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class WeixinExpressProviderApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>
        /// 查询指定手机号是否已绑定微信物流消息服务。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">待查询的手机号码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用户手机号绑定状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/msgpush/api_deliveryuserquery"/>。</remarks>
        public static WeixinExpressUserBindingJsonResult QueryUserBinding(string accessTokenOrAppId, WeixinExpressUserQueryRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressUserBindingJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/userquery", request, timeOut);
        }

        /// <summary>
        /// 向已绑定用户推送物流轨迹节点。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">寄收件人、运单、创建时间和轨迹节点。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>推送结果及用户绑定状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/msgpush/api_deliverypathnotify"/>。</remarks>
        public static WeixinExpressUserBindingJsonResult NotifyPath(string accessTokenOrAppId, WeixinExpressPathNotifyRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WeixinExpressUserBindingJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/pathnotify", request, timeOut);
        }

        /// <summary>
        /// 异步查询指定手机号是否已绑定微信物流消息服务。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">待查询的手机号码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用户手机号绑定状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/msgpush/api_deliveryuserquery"/>。</remarks>
        public static Task<WeixinExpressUserBindingJsonResult> QueryUserBindingAsync(string accessTokenOrAppId, WeixinExpressUserQueryRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressUserBindingJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/userquery", request, timeOut);
        }

        /// <summary>
        /// 异步向已绑定用户推送物流轨迹节点。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">寄收件人、运单、创建时间和轨迹节点。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>推送结果及用户绑定状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/weixin-express/msgpush/api_deliverypathnotify"/>。</remarks>
        public static Task<WeixinExpressUserBindingJsonResult> NotifyPathAsync(string accessTokenOrAppId, WeixinExpressPathNotifyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WeixinExpressUserBindingJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/pathnotify", request, timeOut);
        }

        private static T Send<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : Weixin.Entities.WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return CommonJsonSend.Send<T>(accessToken, url, request, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : Weixin.Entities.WxJsonResult, new()
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
