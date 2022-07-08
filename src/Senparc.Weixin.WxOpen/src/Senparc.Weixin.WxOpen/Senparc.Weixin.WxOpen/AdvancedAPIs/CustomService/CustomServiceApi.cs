using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.WxOpen.AdvancedAPIs.CustomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs
{
    /// <summary>
    /// 小程序客服管理
    /// 文档https://developers.weixin.qq.com/miniprogram/introduction/custom.html#获取客服基本信息
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public class CustomServiceApi
    {
        #region 同步方法
        /// <summary>
        /// 获取客服基本信息列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static GetKfListResultJson GetKfList(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/customservice/getkflist?access_token={0}", accessToken.AsUrlData());


                return CommonJsonSend.Send<GetKfListResultJson>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取在线客服列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static GetOnlineKfListResultJson GetOnlineKfList(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/customservice/getonlinekflist?access_token={0}", accessToken.AsUrlData());


                return CommonJsonSend.Send<GetOnlineKfListResultJson>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 添加客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kf_wx">客服微信号</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static WxJsonResult KfAccountAdd(string accessTokenOrAppId, string kf_wx, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/add?access_token={0}", accessToken.AsUrlData());

                var data = new 
                {
                    kf_wx
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kf_openid">客服微信的OPENID</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static WxJsonResult KfAccountDel(string accessTokenOrAppId, string kf_openid, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/del?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_openid
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置客服管理员
        /// </summary>
        /// <param name="accessTokenOrAppId">>AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kf_openid">客服微信的OPENID</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static WxJsonResult KfAccountSetAdmin(string accessTokenOrAppId, string kf_openid, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/setadmin?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_openid
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 取消客服管理员
        /// </summary>
        /// <param name="accessTokenOrAppId">>AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kf_openid">客服微信的OPENID</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static WxJsonResult KfAccountCancelAdmin(string accessTokenOrAppId, string kf_openid, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/canceladmin?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_openid
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法
        /// <summary>
        /// 获取客服基本信息列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<GetKfListResultJson> GetKfListAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/customservice/getkflist?access_token={0}", accessToken.AsUrlData());


                return await CommonJsonSend.SendAsync<GetKfListResultJson>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取在线客服列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<GetOnlineKfListResultJson> GetOnlineKfListAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/customservice/getonlinekflist?access_token={0}", accessToken.AsUrlData());


                return await CommonJsonSend.SendAsync<GetOnlineKfListResultJson>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 添加客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kf_wx">客服微信号</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> KfAccountAddAsync(string accessTokenOrAppId, string kf_wx, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/add?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_wx
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kf_openid">客服微信的OPENID</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> KfAccountDelAsync(string accessTokenOrAppId, string kf_openid, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/del?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_openid
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置客服管理员
        /// </summary>
        /// <param name="accessTokenOrAppId">>AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kf_openid">客服微信的OPENID</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> KfAccountSetAdminAsync(string accessTokenOrAppId, string kf_openid, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/setadmin?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_openid
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 取消客服管理员
        /// </summary>
        /// <param name="accessTokenOrAppId">>AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kf_openid">客服微信的OPENID</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> KfAccountCancelAdminAsync(string accessTokenOrAppId, string kf_openid, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/canceladmin?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_openid
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId);
        }

        #endregion

    }
}
