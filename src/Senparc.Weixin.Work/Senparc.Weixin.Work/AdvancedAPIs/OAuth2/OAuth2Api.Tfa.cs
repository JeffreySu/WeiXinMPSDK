/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OAuth2Api.Tfa.cs
    文件功能描述：企业微信用户二次验证信息接口


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐获取用户二次验证信息接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.OAuth2;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 企业微信网页授权及用户二次验证接口。
    /// </summary>
    public static partial class OAuth2Api
    {
        private const string GetTfaInfoPath = "/cgi-bin/auth/get_tfa_info";

        /// <summary>
        /// 使用用户进入二次验证页面时获得的 Code 获取成员和二次验证授权码。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99499"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">五分钟内有效且只能使用一次的二次验证 Code。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>成员 UserId 和用于解锁企业微信终端的二次验证授权码。</returns>
        public static GetTfaInfoResult GetTfaInfo(string accessTokenOrAppKey,
            GetTfaInfoRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetTfaInfoResult>(accessToken,
                    Config.ApiWorkHost + GetTfaInfoPath + "?access_token={0}", request,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);

        /// <summary>
        /// 异步使用用户进入二次验证页面时获得的 Code 获取成员和二次验证授权码。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99499"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">五分钟内有效且只能使用一次的二次验证 Code。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>成员 UserId 和用于解锁企业微信终端的二次验证授权码。</returns>
        public static Task<GetTfaInfoResult> GetTfaInfoAsync(string accessTokenOrAppKey,
            GetTfaInfoRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetTfaInfoResult>(accessToken,
                    Config.ApiWorkHost + GetTfaInfoPath + "?access_token={0}", request,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
    }
}
