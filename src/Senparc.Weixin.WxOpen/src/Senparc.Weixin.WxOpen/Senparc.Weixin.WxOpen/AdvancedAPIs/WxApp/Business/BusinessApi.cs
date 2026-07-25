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

    文件名：BusinessApi.cs
    文件功能描述：wxa/business 接口


    创建标识：Senparc - 20220112

    修改标识：dodu2014 - 20260109
    修改描述：v3.25.0 feat: 添加“解绑用工关系”接口

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Business.JsonResult;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp
{
    /// <summary>
    /// wxa/business 接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class BusinessApi
    {
        #region 同步方法

        /// <summary>
        /// code换取用户手机号。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">每个code只能使用一次，code的有效期为5min</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetUserPhoneNumberJsonResult GetUserPhoneNumber(string accessTokenOrAppId, string code, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/business/getuserphonenumber?access_token={0}";
                string url = string.Format(urlFormat, accessToken);
                var data = new { code = code };
                return CommonJsonSend.Send<GetUserPhoneNumberJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 解绑用工关系
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openidList">被解绑用户的openid列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult UnBindUserAuthinfo(string accessTokenOrAppId, string[] openidList, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/business/unbinduserb2cauthinfo?access_token={0}";
                string url = string.Format(urlFormat, accessToken);
                var data = new { openid_list = openidList };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取插件用户的 OpenPID
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="code">微信登录或插件授权临时凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetPluginOpenPidJsonResult GetPluginOpenPid(string accessTokenOrAppId, string code, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/getpluginopenpid?access_token={0}";
                return CommonJsonSend.Send<GetPluginOpenPidJsonResult>(accessToken, urlFormat, new { code }, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 检查加密信息是否由最近访问过当前小程序的用户生成
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="encryptedMsgHash">加密数据的 SHA-256 摘要。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static CheckEncryptedDataJsonResult CheckEncryptedData(string accessTokenOrAppId, string encryptedMsgHash, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/business/checkencryptedmsg?access_token={0}";
                return CommonJsonSend.Send<CheckEncryptedDataJsonResult>(accessToken, urlFormat,
                    new { encrypted_msg_hash = encryptedMsgHash }, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取用户数据加密密钥
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="signature">用户数据签名。</param>
        /// <param name="sigMethod">签名算法，当前为 hmac_sha256。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetUserEncryptKeyJsonResult GetUserEncryptKey(string accessTokenOrAppId, string openId, string signature,
            string sigMethod = "hmac_sha256", int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/business/getuserencryptkey?access_token={0}&openid={1}&signature={2}&sig_method={3}",
                    accessToken.AsUrlData(), openId.AsUrlData(), signature.AsUrlData(), sigMethod.AsUrlData());
                return CommonJsonSend.Send<GetUserEncryptKeyJsonResult>(accessToken, url, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 重置用户 SessionKey
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="signature">用户数据签名。</param>
        /// <param name="sigMethod">签名算法，当前为 hmac_sha256。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ResetUserSessionKeyJsonResult ResetUserSessionKey(string accessTokenOrAppId, string openId, string signature,
            string sigMethod = "hmac_sha256", int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/resetusersessionkey?access_token={0}&openid={1}&signature={2}&sig_method={3}",
                    accessToken.AsUrlData(), openId.AsUrlData(), signature.AsUrlData(), sigMethod.AsUrlData());
                return CommonJsonSend.Send<ResetUserSessionKeyJsonResult>(accessToken, url, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】code换取用户手机号。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">每个code只能使用一次，code的有效期为5min</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetUserPhoneNumberJsonResult> GetUserPhoneNumberAsync(string accessTokenOrAppId, string code, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/business/getuserphonenumber?access_token={0}";
                string url = string.Format(urlFormat, accessToken);
                var data = new { code = code };
                return await CommonJsonSend.SendAsync<GetUserPhoneNumberJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】解绑用工关系
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openidList">被解绑用户的openid列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UnBindUserAuthinfoAsync(string accessTokenOrAppId, string[] openidList, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/business/unbinduserb2cauthinfo?access_token={0}";
                string url = string.Format(urlFormat, accessToken);
                var data = new { openid_list = openidList };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取插件用户的 OpenPID
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="code">微信登录或插件授权临时凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetPluginOpenPidJsonResult> GetPluginOpenPidAsync(string accessTokenOrAppId, string code, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/getpluginopenpid?access_token={0}";
                return await CommonJsonSend.SendAsync<GetPluginOpenPidJsonResult>(accessToken, urlFormat, new { code }, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】检查加密信息是否由最近访问过当前小程序的用户生成
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="encryptedMsgHash">加密数据的 SHA-256 摘要。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<CheckEncryptedDataJsonResult> CheckEncryptedDataAsync(string accessTokenOrAppId, string encryptedMsgHash, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/business/checkencryptedmsg?access_token={0}";
                return await CommonJsonSend.SendAsync<CheckEncryptedDataJsonResult>(accessToken, urlFormat,
                    new { encrypted_msg_hash = encryptedMsgHash }, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取用户数据加密密钥
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="signature">用户数据签名。</param>
        /// <param name="sigMethod">签名算法，当前为 hmac_sha256。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetUserEncryptKeyJsonResult> GetUserEncryptKeyAsync(string accessTokenOrAppId, string openId, string signature,
            string sigMethod = "hmac_sha256", int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/business/getuserencryptkey?access_token={0}&openid={1}&signature={2}&sig_method={3}",
                    accessToken.AsUrlData(), openId.AsUrlData(), signature.AsUrlData(), sigMethod.AsUrlData());
                return await CommonJsonSend.SendAsync<GetUserEncryptKeyJsonResult>(accessToken, url, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】重置用户 SessionKey
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="signature">用户数据签名。</param>
        /// <param name="sigMethod">签名算法，当前为 hmac_sha256。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<ResetUserSessionKeyJsonResult> ResetUserSessionKeyAsync(string accessTokenOrAppId, string openId, string signature,
            string sigMethod = "hmac_sha256", int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/resetusersessionkey?access_token={0}&openid={1}&signature={2}&sig_method={3}",
                    accessToken.AsUrlData(), openId.AsUrlData(), signature.AsUrlData(), sigMethod.AsUrlData());
                return await CommonJsonSend.SendAsync<ResetUserSessionKeyJsonResult>(accessToken, url, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        #endregion
    }
}
