/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：KfWorkApi.cs
    文件功能描述：KfWorkApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.CustomService
{
    /// <summary>
    /// GetKfWorkBound 接口返回结果。
    /// </summary>
    public class GetKfWorkBoundJsonResult : WxJsonResult
    {
        public string entityName { get; set; }
        public string corpid { get; set; }
        public long bindTime { get; set; }
    }

    /// <summary>
    /// 小程序绑定微信客服
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class KfWorkApi
    {
        /// <summary>
        /// 查询已绑定的企业微信客服。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetKfWorkBoundJsonResult GetBound(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetKfWorkBoundJsonResult>(accessToken,
                    Config.ApiMpHost + "/customservice/work/get?access_token={0}", null, CommonJsonSendType.GET, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 绑定企业微信客服。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="corpId">企业微信 CorpId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult Bind(string accessTokenOrAppId, string corpId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send(accessToken, Config.ApiMpHost + "/customservice/work/bind?access_token={0}",
                    new { corpid = corpId }, timeOut: timeOut), accessTokenOrAppId);
        }

        /// <summary>
        /// 解绑企业微信客服。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="corpId">企业微信 CorpId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult Unbind(string accessTokenOrAppId, string corpId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send(accessToken, Config.ApiMpHost + "/customservice/work/unbind?access_token={0}",
                    new { corpid = corpId }, timeOut: timeOut), accessTokenOrAppId);
        }

        /// <summary>
        /// 异步查询已绑定的企业微信客服。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetKfWorkBoundJsonResult> GetBoundAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetKfWorkBoundJsonResult>(accessToken,
                    Config.ApiMpHost + "/customservice/work/get?access_token={0}", null, CommonJsonSendType.GET, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步绑定企业微信客服。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="corpId">企业微信 CorpId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> BindAsync(string accessTokenOrAppId, string corpId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync(accessToken, Config.ApiMpHost + "/customservice/work/bind?access_token={0}",
                    new { corpid = corpId }, timeOut: timeOut), accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步解绑企业微信客服。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="corpId">企业微信 CorpId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> UnbindAsync(string accessTokenOrAppId, string corpId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync(accessToken, Config.ApiMpHost + "/customservice/work/unbind?access_token={0}",
                    new { corpid = corpId }, timeOut: timeOut), accessTokenOrAppId).ConfigureAwait(false);
        }
    }
}
