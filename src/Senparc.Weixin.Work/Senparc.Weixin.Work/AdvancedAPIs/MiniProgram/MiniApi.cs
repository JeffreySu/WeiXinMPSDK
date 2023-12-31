/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：MiniApi.cs
    文件功能描述：小程序接口
    
    
    创建标识：Senparc - 20181009
    
	修改标识：mojinxun - 20230226
    修改描述：添加“获取下级/下游企业小程序session”接口

----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#13473
 */

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp;
using Senparc.Weixin.Work.AdvancedAPIs.External;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 企业微信小程序接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
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
        public static LoginCheckResultJson ThirdLoginCheck(string suiteAccessToken, string code, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/miniprogram/jscode2session?suite_access_token={0}&js_code={1}&grant_type=authorization_code", suiteAccessToken.AsUrlData(), code);

                return CommonJsonSend.Send<LoginCheckResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, suiteAccessToken);

        }

        /// <summary>
        /// 获取下级/下游企业小程序session
        /// 上级/上游企业通过该接口转换为下级/下游企业的小程序session
        /// https://developer.work.weixin.qq.com/document/path/95817
        /// </summary>
        /// <param name="accessToken">调用接口凭证。下级/下游企业的 access_token</param>
        /// <param name="userid">通过code2Session接口获取到的加密的userid 不多于64字节</param>
        /// <param name="session_key">通过code2Session接口获取到的属于上级/上游企业的会话密钥-不多于64字节</param>
        /// <param name="timeOut">请求参数</param>
        /// <returns></returns>
        public static TransferSessionResultJson TransferSession(string accessToken, string userid, string session_key, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/miniprogram/transfer_session?access_token={0}";
            var data = new
            {
                userid,
                session_key
            };
            return CommonJsonSend.Send<TransferSessionResultJson>(accessToken, urlFormat, data, CommonJsonSendType.POST);
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
        public static async Task<LoginCheckResultJson> ThirdLoginCheckAsync(string suiteAccessToken, string code, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/miniprogram/jscode2session?suite_access_token={0}&js_code={1}&grant_type=authorization_code", suiteAccessToken.AsUrlData(), code);

                return await CommonJsonSend.SendAsync<LoginCheckResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, suiteAccessToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取下级/下游企业小程序session
        /// 上级/上游企业通过该接口转换为下级/下游企业的小程序session
        /// https://developer.work.weixin.qq.com/document/path/95817
        /// </summary>
        /// <param name="accessToken">调用接口凭证。下级/下游企业的 access_token</param>
        /// <param name="userid">通过code2Session接口获取到的加密的userid 不多于64字节</param>
        /// <param name="session_key">通过code2Session接口获取到的属于上级/上游企业的会话密钥-不多于64字节</param>
        /// <param name="timeOut">请求参数</param>
        /// <returns></returns>
        public static async Task<TransferSessionResultJson> TransferSessionAsync(string accessToken, string userid, string session_key, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/miniprogram/transfer_session?access_token={0}";
            var data = new
            {
                userid,
                session_key
            };
            return await CommonJsonSend.SendAsync<TransferSessionResultJson>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }
        #endregion
    }
}
