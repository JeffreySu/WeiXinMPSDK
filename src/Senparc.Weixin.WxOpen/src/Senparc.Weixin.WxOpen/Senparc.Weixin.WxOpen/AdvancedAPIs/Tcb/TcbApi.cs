using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.WxOpen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    /// <summary>
    /// 云函数
    /// 注意: HTTP API 途径触发云函数不包含用户信息
    /// </summary>
    public static class TcbApi
    {
        #region 同步方法
        /// <summary>
        /// 触发云函数。注意：HTTP API 途径触发云函数不包含用户信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云开发环境ID</param>
        /// <param name="name">云函数名称</param>
        /// <param name="postBody">云函数的传入参数，具体结构由开发者定义。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.InvokeCloudFunction", true)]
        public static WxCloudFunctionJsonResult InvokeCloudFunction(string accessTokenOrAppId, string env, string name, object postBody, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/invokecloudfunction?access_token={0}&env=" + env + "&name=" + name;
                return CommonJsonSend.Send<WxCloudFunctionJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】触发云函数。注意：HTTP API 途径触发云函数不包含用户信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云开发环境ID</param>
        /// <param name="name">云函数名称</param>
        /// <param name="postBody">云函数的传入参数，具体结构由开发者定义。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.InvokeCloudFunctionAsync", true)]
        public static async Task<WxCloudFunctionJsonResult> SendTemplateMessageAsync(string accessTokenOrAppId, string env, string name, object postBody, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/invokecloudfunction?access_token={0}&env=" + env + "&name=" + name;
                return await CommonJsonSend.SendAsync<WxCloudFunctionJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
