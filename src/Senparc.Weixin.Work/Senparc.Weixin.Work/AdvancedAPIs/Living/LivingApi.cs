/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：LivingApi.cs
    文件功能描述：群直播接口
    
    
    创建标识：WangDrama - 20210616

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐预约直播生命周期、微信观看凭证和商城观众信息接口

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
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

        /// <summary>创建预约直播。</summary>
        public static CreateLivingResult Create(string accessTokenOrAppKey, CreateLivingRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<CreateLivingResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/create?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
        }

        /// <summary>修改当前应用创建的预约直播。</summary>
        public static WorkJsonResult Modify(string accessTokenOrAppKey, ModifyLivingRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<WorkJsonResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/modify?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
        }

        /// <summary>取消当前应用创建的预约直播。</summary>
        public static WorkJsonResult Cancel(string accessTokenOrAppKey, LivingIdRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<WorkJsonResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/cancel?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
        }

        /// <summary>删除当前应用创建的直播回放数据。</summary>
        public static WorkJsonResult DeleteReplayData(string accessTokenOrAppKey, LivingIdRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<WorkJsonResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/delete_replay_data?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
        }

        /// <summary>获取微信观看直播或回放的临时凭证。</summary>
        public static GetLivingCodeResult GetLivingCode(string accessTokenOrAppKey, GetLivingCodeRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetLivingCodeResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/get_living_code?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
        }

        /// <summary>获取跳转小程序商城的直播观众和邀请人信息。</summary>
        public static GetLivingShareInfoResult GetLivingShareInfo(string accessTokenOrAppKey, GetLivingShareInfoRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetLivingShareInfoResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/get_living_share_info?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
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

        /// <summary>异步创建预约直播。</summary>
        public static async Task<CreateLivingResult> CreateAsync(string accessTokenOrAppKey, CreateLivingRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<CreateLivingResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/create?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut).ConfigureAwait(false), accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>异步修改当前应用创建的预约直播。</summary>
        public static async Task<WorkJsonResult> ModifyAsync(string accessTokenOrAppKey, ModifyLivingRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/modify?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut).ConfigureAwait(false), accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>异步取消当前应用创建的预约直播。</summary>
        public static async Task<WorkJsonResult> CancelAsync(string accessTokenOrAppKey, LivingIdRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/cancel?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut).ConfigureAwait(false), accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>异步删除当前应用创建的直播回放数据。</summary>
        public static async Task<WorkJsonResult> DeleteReplayDataAsync(string accessTokenOrAppKey, LivingIdRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/delete_replay_data?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut).ConfigureAwait(false), accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>异步获取微信观看直播或回放的临时凭证。</summary>
        public static async Task<GetLivingCodeResult> GetLivingCodeAsync(string accessTokenOrAppKey, GetLivingCodeRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<GetLivingCodeResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/get_living_code?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut).ConfigureAwait(false), accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>异步获取跳转小程序商城的直播观众和邀请人信息。</summary>
        public static async Task<GetLivingShareInfoResult> GetLivingShareInfoAsync(string accessTokenOrAppKey, GetLivingShareInfoRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<GetLivingShareInfoResult>(accessToken,
                    Config.ApiWorkHost + "/cgi-bin/living/get_living_share_info?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut).ConfigureAwait(false), accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion
    }
}
