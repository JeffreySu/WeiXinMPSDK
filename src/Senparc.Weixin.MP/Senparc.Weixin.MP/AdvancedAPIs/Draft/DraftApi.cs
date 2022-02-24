
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.Draft;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 草稿管理接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class DraftApi
    {
        #region 同步方法


        #region 草稿
        /*
         https://mp.weixin.qq.com/cgi-bin/announce?action=getannouncement&announce_id=11644831863qFQSh&version=&lang=zh_CN&token=
         */

        /// <summary>
        /// 新增草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="news">图文消息组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateDraftResult CreateDraft(string accessTokenOrAppId, int timeOut = Config.TIME_OUT, params NewsModel[] news)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/draft/add?access_token={0}";

                var data = new
                {
                    articles = news
                };
                return CommonJsonSend.Send<CreateDraftResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

      
        /// <summary>
        /// 获取草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetDraftResult GetDraft(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/get?access_token={0}";
                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.Send<GetDraftResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

     
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult DeleteDraft(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/delete?access_token={0}";
                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId">要修改的图文消息的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="news">草稿</param>
        /// <returns></returns>
        public static WxJsonResult UpdateDraft(string accessTokenOrAppId, string mediaId, int? index, NewsModel news, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/update?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    index = index,
                    articles = news
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取草稿总数
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static GetDraftCountResult GetDraftCount(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/draft/count?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetDraftCountResult>(null, url, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取草稿列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">从全部草稿的该偏移位置开始返回，0表示从第一个草稿 返回</param>
        /// <param name="count">返回草稿的数量，取值在1到20之间</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetDraftListResult GetDraftList(string accessTokenOrAppId, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/draft/batchget?access_token={0}", accessToken.AsUrlData());

                var date = new
                {
                    type = "news",
                    offset = offset,
                    count = count
                };

                return CommonJsonSend.Send<GetDraftListResult>(null, url, date, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);

        }

        #endregion

      

        #endregion

        #region 异步方法
        #region 草稿
        /// <summary>
        /// 【异步方法】新增草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="news">图文消息组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CreateDraftResult> CreateDraftAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT, params NewsModel[] news)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string urlFormat = Config.ApiMpHost + "/cgi-bin/draft/add?access_token={0}";

               var data = new
               {
                   articles = news
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CreateDraftResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

     
        /// <summary>
        /// 【异步方法】获取草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetDraftResult> GetDraftAsync(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = Config.ApiMpHost + "/cgi-bin/draft/get?access_token={0}";
               var data = new
               {
                   media_id = mediaId
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetDraftResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

   
        /// <summary>
        /// 【异步方法】删除草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeleteDraftAsync(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = Config.ApiMpHost + "/cgi-bin/draft/delete?access_token={0}";
               var data = new
               {
                   media_id = mediaId
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】修改草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId">要修改的图文消息的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="news">草稿</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UpdateDraftAsync(string accessTokenOrAppId, string mediaId, int? index, NewsModel news, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = Config.ApiMpHost + "/cgi-bin/draft/update?access_token={0}";

               var data = new
               {
                   media_id = mediaId,
                   index = index,
                   articles = news
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取草稿总数
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static async Task<GetDraftCountResult> GetDraftCountAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = string.Format(Config.ApiMpHost + "/cgi-bin/draft/count?access_token={0}", accessToken.AsUrlData());

               return await CommonJsonSend.SendAsync<GetDraftCountResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取草稿列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">从全部草稿的该偏移位置开始返回，0表示从第一个草稿 返回</param>
        /// <param name="count">返回草稿的数量，取值在1到20之间</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetDraftListResult> GetDraftListAsync(string accessTokenOrAppId, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = string.Format(Config.ApiMpHost + "/cgi-bin/draft/batchget?access_token={0}", accessToken.AsUrlData());

               var date = new
               {
                   type = "news",
                   offset = offset,
                   count = count
               };

               return await CommonJsonSend.SendAsync<GetDraftListResult>(null, url, date, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);

        }

   
        #endregion

        #endregion
    }
}
