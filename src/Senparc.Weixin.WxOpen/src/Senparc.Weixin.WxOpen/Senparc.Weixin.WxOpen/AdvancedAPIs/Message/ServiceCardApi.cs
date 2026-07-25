/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ServiceCardApi.cs
    文件功能描述：ServiceCardApi 微信接口封装


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Message
{
    /// <summary>
    /// GetUserNotify 接口返回结果。
    /// </summary>
    public class GetUserNotifyJsonResult : WxJsonResult
    {
        public UserNotifyInfo notify_info { get; set; }
    }

    /// <summary>
    /// UserNotify 信息。
    /// </summary>
    public class UserNotifyInfo
    {
        public int notify_type { get; set; }
        public string content_json { get; set; }
        public int code_state { get; set; }
        public long code_expire_time { get; set; }
    }

    /// <summary>
    /// 小程序服务卡片
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class ServiceCardApi
    {
        /// <summary>
        /// 设置服务卡片通知。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="notifyType">服务卡片通知类型。</param>
        /// <param name="notifyCode">服务卡片通知编码。</param>
        /// <param name="contentJson">消息内容 JSON。</param>
        /// <param name="checkJson">设备校验信息 JSON。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SetUserNotify(string accessTokenOrAppId, string openId, int notifyType,
            string notifyCode, string contentJson, string checkJson = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send(accessToken, Config.ApiMpHost + "/wxa/set_user_notify?access_token={0}",
                    new
                    {
                        openid = openId,
                        notify_type = notifyType,
                        notify_code = notifyCode,
                        content_json = contentJson,
                        check_json = checkJson
                    }, timeOut: timeOut, jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 设置服务卡片扩展通知。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="notifyType">服务卡片通知类型。</param>
        /// <param name="notifyCode">服务卡片通知编码。</param>
        /// <param name="extJson">扩展参数 JSON。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SetUserNotifyExt(string accessTokenOrAppId, string openId, int notifyType,
            string notifyCode, string extJson, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send(accessToken, Config.ApiMpHost + "/wxa/set_user_notifyext?access_token={0}",
                    new { openid = openId, notify_type = notifyType, notify_code = notifyCode, ext_json = extJson }, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 查询服务卡片通知设置。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="notifyType">服务卡片通知类型。</param>
        /// <param name="notifyCode">服务卡片通知编码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetUserNotifyJsonResult GetUserNotify(string accessTokenOrAppId, string openId, int notifyType,
            string notifyCode, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetUserNotifyJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxa/get_user_notify?access_token={0}",
                    new { openid = openId, notify_type = notifyType, notify_code = notifyCode }, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 异步设置服务卡片通知。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="notifyType">服务卡片通知类型。</param>
        /// <param name="notifyCode">服务卡片通知编码。</param>
        /// <param name="contentJson">消息内容 JSON。</param>
        /// <param name="checkJson">设备校验信息 JSON。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> SetUserNotifyAsync(string accessTokenOrAppId, string openId, int notifyType,
            string notifyCode, string contentJson, string checkJson = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync(accessToken, Config.ApiMpHost + "/wxa/set_user_notify?access_token={0}",
                    new
                    {
                        openid = openId,
                        notify_type = notifyType,
                        notify_code = notifyCode,
                        content_json = contentJson,
                        check_json = checkJson
                    }, timeOut: timeOut, jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步设置服务卡片扩展通知。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="notifyType">服务卡片通知类型。</param>
        /// <param name="notifyCode">服务卡片通知编码。</param>
        /// <param name="extJson">扩展参数 JSON。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> SetUserNotifyExtAsync(string accessTokenOrAppId, string openId, int notifyType,
            string notifyCode, string extJson, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync(accessToken, Config.ApiMpHost + "/wxa/set_user_notifyext?access_token={0}",
                    new { openid = openId, notify_type = notifyType, notify_code = notifyCode, ext_json = extJson }, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步查询服务卡片通知设置。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="notifyType">服务卡片通知类型。</param>
        /// <param name="notifyCode">服务卡片通知编码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetUserNotifyJsonResult> GetUserNotifyAsync(string accessTokenOrAppId, string openId, int notifyType,
            string notifyCode, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetUserNotifyJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxa/get_user_notify?access_token={0}",
                    new { openid = openId, notify_type = notifyType, notify_code = notifyCode }, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }
    }
}
