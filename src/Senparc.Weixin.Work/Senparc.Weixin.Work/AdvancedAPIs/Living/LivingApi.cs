/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：LivingApi.cs
    文件功能描述：群直播接口
    
    
    创建标识：WangDrama - 20210630

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.Living.LivingJson;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Living
{
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class LivingApi
    {
        #region 同步

        /// <summary>
        /// 获取指定成员的所有直播ID
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetUserLivingResponse GetUserAllLivingid(string accessTokenOrAppKey, GetUserLivingRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/living/get_user_all_livingid?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetUserLivingResponse>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取直播详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetUserLivingInfoResponse GetLivingInfo(string accessTokenOrAppKey, string livingid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/living/get_living_info?access_token={0}&livingid={1}", accessToken, livingid);

                return CommonJsonSend.Send<GetUserLivingInfoResponse>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 获取所有观看直播的人员统计
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="livingid"></param>
        /// <param name="next_key">否	上一次调用时返回的next_key，初次调用可以填”0”</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetUserLivingWatchStateResponse GetLivingWatchState(string accessTokenOrAppKey, string livingid, string next_key, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var para = new
                {
                    livingid = livingid,
                    next_key = next_key
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/living/get_watch_stat?access_token={0}", accessToken, livingid);
                return CommonJsonSend.Send<GetUserLivingWatchStateResponse>(null, url, para, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion

        #region 异步
        /// <summary>
        /// 获取指定成员的所有直播ID
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetUserLivingResponse> GetUserAllLivingidAsync(string accessTokenOrAppKey, GetUserLivingRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/living/get_user_all_livingid?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetUserLivingResponse>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取直播详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetUserLivingInfoResponse> GetLivingInfoAsync(string accessTokenOrAppKey, string livingid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/living/get_living_info?access_token={0}&livingid={1}", accessToken, livingid);
                return await CommonJsonSend.SendAsync<GetUserLivingInfoResponse>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取所有观看直播的人员统计
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="livingid"></param>
        /// <param name="next_key">否	上一次调用时返回的next_key，初次调用可以填”0”</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetUserLivingWatchStateResponse> GetLivingWatchStateAsync(string accessTokenOrAppKey, string livingid, string next_key, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var para = new
                {
                    livingid = livingid,
                    next_key = next_key
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/living/get_watch_stat?access_token={0}", accessToken, livingid);
                return await CommonJsonSend.SendAsync<GetUserLivingWatchStateResponse>(null, url, para, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion
    }
}
