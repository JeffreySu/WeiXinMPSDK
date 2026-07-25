/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：UpdatableMessageApi.cs
    文件功能描述：UpdatableMessageApi 微信接口封装


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Message
{
    /// <summary>
    /// CreateActivityId 接口返回结果。
    /// </summary>
    public class CreateActivityIdJsonResult : WxJsonResult
    {
        public string activity_id { get; set; }
        public long expiration_time { get; set; }
    }

    /// <summary>
    /// UpdatableMessageTemplate 信息。
    /// </summary>
    public class UpdatableMessageTemplateInfo
    {
        public IList<UpdatableMessageParameter> parameter_list { get; set; }
    }

    /// <summary>
    /// UpdatableMessageParameter 微信接口数据模型。
    /// </summary>
    public class UpdatableMessageParameter
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    /// <summary>
    /// ChatToolParticipator 信息。
    /// </summary>
    public class ChatToolParticipatorInfo
    {
        public string group_openid { get; set; }
        public int state { get; set; }
    }

    /// <summary>
    /// 小程序动态消息
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class UpdatableMessageApi
    {
        /// <summary>
        /// 创建动态消息活动 ID。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="unionId">用户 UnionId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static CreateActivityIdJsonResult CreateActivityId(string accessTokenOrAppId, string unionId = null,
            string openId = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + "/cgi-bin/message/wxopen/activityid/create?access_token=" + accessToken.AsUrlData();
                if (!string.IsNullOrEmpty(unionId)) url += "&unionid=" + unionId.AsUrlData();
                if (!string.IsNullOrEmpty(openId)) url += "&openid=" + openId.AsUrlData();
                return CommonJsonSend.Send<CreateActivityIdJsonResult>(accessToken, url, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新小程序动态消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="activityId">动态消息活动 ID。</param>
        /// <param name="targetState">动态消息目标状态。</param>
        /// <param name="templateInfo">模板消息内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SetUpdatableMessage(string accessTokenOrAppId, string activityId, int targetState,
            UpdatableMessageTemplateInfo templateInfo, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send(accessToken,
                    Config.ApiMpHost + "/cgi-bin/message/wxopen/updatablemsg/send?access_token={0}",
                    new { activity_id = activityId, target_state = targetState, template_info = templateInfo }, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 更新群聊工具消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="activityId">动态消息活动 ID。</param>
        /// <param name="targetState">动态消息目标状态。</param>
        /// <param name="templateId">消息或短信模板 ID。</param>
        /// <param name="versionType">小程序版本类型。</param>
        /// <param name="participatorInfoList">认证参与人信息列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SetChatToolMessage(string accessTokenOrAppId, string activityId, int targetState,
            string templateId, int versionType, IList<ChatToolParticipatorInfo> participatorInfoList = null,
            int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send(accessToken,
                    Config.ApiMpHost + "/cgi-bin/message/wxopen/chattoolmsg/send?access_token={0}",
                    new
                    {
                        activity_id = activityId,
                        target_state = targetState,
                        template_id = templateId,
                        version_type = versionType,
                        participator_info_list = participatorInfoList
                    }, timeOut: timeOut, jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 异步创建动态消息活动 ID。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="unionId">用户 UnionId。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<CreateActivityIdJsonResult> CreateActivityIdAsync(string accessTokenOrAppId, string unionId = null,
            string openId = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                var url = Config.ApiMpHost + "/cgi-bin/message/wxopen/activityid/create?access_token=" + accessToken.AsUrlData();
                if (!string.IsNullOrEmpty(unionId)) url += "&unionid=" + unionId.AsUrlData();
                if (!string.IsNullOrEmpty(openId)) url += "&openid=" + openId.AsUrlData();
                return CommonJsonSend.SendAsync<CreateActivityIdJsonResult>(accessToken, url, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步更新小程序动态消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="activityId">动态消息活动 ID。</param>
        /// <param name="targetState">动态消息目标状态。</param>
        /// <param name="templateInfo">模板消息内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> SetUpdatableMessageAsync(string accessTokenOrAppId, string activityId, int targetState,
            UpdatableMessageTemplateInfo templateInfo, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync(accessToken,
                    Config.ApiMpHost + "/cgi-bin/message/wxopen/updatablemsg/send?access_token={0}",
                    new { activity_id = activityId, target_state = targetState, template_info = templateInfo }, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步更新群聊工具消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="activityId">动态消息活动 ID。</param>
        /// <param name="targetState">动态消息目标状态。</param>
        /// <param name="templateId">消息或短信模板 ID。</param>
        /// <param name="versionType">小程序版本类型。</param>
        /// <param name="participatorInfoList">认证参与人信息列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> SetChatToolMessageAsync(string accessTokenOrAppId, string activityId, int targetState,
            string templateId, int versionType, IList<ChatToolParticipatorInfo> participatorInfoList = null,
            int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync(accessToken,
                    Config.ApiMpHost + "/cgi-bin/message/wxopen/chattoolmsg/send?access_token={0}",
                    new
                    {
                        activity_id = activityId,
                        target_state = targetState,
                        template_id = templateId,
                        version_type = versionType,
                        participator_info_list = participatorInfoList
                    }, timeOut: timeOut, jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }),
                accessTokenOrAppId).ConfigureAwait(false);
        }
    }
}
