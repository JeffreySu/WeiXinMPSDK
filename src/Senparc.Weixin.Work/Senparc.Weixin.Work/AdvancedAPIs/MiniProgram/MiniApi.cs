/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MiniApi.cs
    文件功能描述：小程序接口
    
    
    创建标识：Senparc - 20181009
    
    
----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#13473
 */

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.External;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 企业微信小程序接口
    /// </summary>
    public static class MiniApi
    {
        #region 同步方法


        /// <summary>
        /// 登录凭证校验
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "MiniApi.LoginCheck", true)]
        public static LoginCheckResultJson LoginCheck(string accessTokenOrAppKey, string code, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/miniprogram/jscode2session?access_token={0}&js_code={1}&grant_type=authorization_code", accessToken, code);

                return CommonJsonSend.Send<LoginCheckResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 第三方登录凭证校验
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="code"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "MiniApi.ThirdLoginCheck", true)]
        public static LoginCheckResultJson ThirdLoginCheck(string suiteAccessToken, string code, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/miniprogram/jscode2session?suite_access_token={0}&js_code={1}&grant_type=authorization_code", suiteAccessToken.AsUrlData(), code);

                return CommonJsonSend.Send<LoginCheckResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, suiteAccessToken);

        }

        #endregion


        #region 异步方法



        /// <summary>
        /// 【异步方法】登录凭证校验
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "MiniApi.LoginCheckAsync", true)]
        public static async Task<LoginCheckResultJson> LoginCheckAsync(string accessTokenOrAppKey, string code, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/miniprogram/jscode2session?access_token={0}&js_code={1}&grant_type=authorization_code", accessToken, code);

                return await CommonJsonSend.SendAsync<LoginCheckResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】第三方登录凭证校验
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "MiniApi.ThirdLoginCheckAsync", true)]
        public static async Task<LoginCheckResultJson> ThirdLoginCheckAsync(string suiteAccessToken, string code, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/miniprogram/jscode2session?suite_access_token={0}&js_code={1}&grant_type=authorization_code", suiteAccessToken.AsUrlData(), code);

                return await CommonJsonSend.SendAsync<LoginCheckResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, suiteAccessToken).ConfigureAwait(false);

        }

        #endregion
    }
}
