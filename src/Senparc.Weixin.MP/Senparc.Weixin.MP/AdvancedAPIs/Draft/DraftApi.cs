using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft
{
    /// <summary>
    /// 开发者可新增常用的素材到草稿箱中进行使用。上传到草稿箱中的素材被群发或发布后，该素材将从草稿箱中移除。
    /// 新增草稿可在公众平台官网-草稿箱中查看和管理。
    /// 接口地址 https://developers.weixin.qq.com/doc/offiaccount/Draft_Box/Add_draft.html
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class DraftApi
    {
        #region 同步方法
        /// <summary>
        /// 新建草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="draft">草稿实体组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AddDraftResultJson AddDraft(string accessTokenOrAppId, int timeOut = Config.TIME_OUT, params DraftModel[] draft)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/add?access_token={0}";

                var data = new
                {
                    articles = draft
                };
                return CommonJsonSend.Send<AddDraftResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="mediaId">要获取的草稿的media_id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetDraftResultJson GetDraft(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/get?access_token={0}";

                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.Send<GetDraftResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取草稿的总数。此接口只统计数量，不返回草稿的具体内容
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetDraftCountResultJson GetDraftCount(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/count?access_token={0}";

                var data = new{};
                return CommonJsonSend.Send<GetDraftCountResultJson>(accessToken, url, data, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="mediaId">要删除的草稿的media_id</param>
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
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="mediaId">要更新的草稿的media_id</param>
        /// <param name="draft"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <returns></returns>
        public static WxJsonResult UpdateDraft(string accessTokenOrAppId, string mediaId,int index, DraftModel draft, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/update?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    index = index,
                    articles = draft
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取草稿列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="no_content">1 表示不返回 content 字段，0 表示正常返回，默认为 0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static DraftList_Result GetDraftList(string accessTokenOrAppId, int offset, int count,int no_content=0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/batchget?access_token={0}";

                var date = new
                {
                    offset = offset,
                    count = count,
                    no_content = no_content
                };

                return CommonJsonSend.Send<DraftList_Result>(accessToken, url, date, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        #endregion



        #region 异步方法
        /// <summary>
        /// 【异步方法】新建草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="draft">草稿实体组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<AddDraftResultJson> AddDraftAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT, params DraftModel[] draft)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/add?access_token={0}";

                var data = new
                {
                    articles = draft
                };
                return CommonJsonSend.SendAsync<AddDraftResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】 获取草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="mediaId">要获取的草稿的media_id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetDraftResultJson> GetDraftAsync(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/get?access_token={0}";

                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.SendAsync<GetDraftResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取草稿的总数。此接口只统计数量，不返回草稿的具体内容
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetDraftCountResultJson> GetDraftCountAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/count?access_token={0}";

                var data = new { };
                return CommonJsonSend.SendAsync<GetDraftCountResultJson>(accessToken, url, data, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】删除草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="mediaId">要删除的草稿的media_id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeleteDraftAsync(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/delete?access_token={0}";

                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】更新草稿
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="mediaId">要更新的草稿的media_id</param>
        /// <param name="draft"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UpdateDraftAsync(string accessTokenOrAppId, string mediaId, int index, DraftModel draft, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/update?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    index = index,
                    articles = draft
                };
                return CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取草稿列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="no_content">1 表示不返回 content 字段，0 表示正常返回，默认为 0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<DraftList_Result> GetDraftListAsync(string accessTokenOrAppId, int offset, int count, int no_content = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/draft/batchget?access_token={0}";

                var date = new
                {
                    offset = offset,
                    count = count,
                    no_content = no_content
                };

                return CommonJsonSend.SendAsync<DraftList_Result>(accessToken, url, date, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion
    }
}