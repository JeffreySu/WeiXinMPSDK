#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：FreePublishApi.cs
    文件功能描述：发布能力接口
    
    
    创建标识：dupeng0811 - 20220227

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.FreePublish.FreePublishJson;

namespace Senparc.Weixin.MP.AdvancedAPIs.FreePublish
{
    /// <summary>
    /// 发布能力接口
    /// 接口地址 https://developers.weixin.qq.com/doc/offiaccount/Publish/Publish.html
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class FreePublishApi
    {
        #region 同步方法

        /// <summary>
        /// 发布接口
        /// 开发者需要先将图文素材以草稿的形式保存（见“草稿箱/新建草稿”，如需从已保存的草稿中选择，见“草稿箱/获取草稿列表”），选择要发布的草稿 media_id 进行发布
        /// 请注意：正常情况下调用成功时，errcode将为0，此时只意味着发布任务提交成功，并不意味着此时发布已经完成，所以，仍有可能在后续的发布过程中出现异常情况导致发布失败，如原创声明失败、平台审核不通过等。
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SubmitFreePublishResultJson Submit(string accessTokenOrAppId,string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/submit?access_token={0}";

                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.Send<SubmitFreePublishResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 发布状态轮询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="publishId">发布任务id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetFreePublishResultJson Get(string accessTokenOrAppId,string publishId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/get?access_token={0}";

                var data = new
                {
                    publish_id = publishId
                };
                return CommonJsonSend.Send<GetFreePublishResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除发布
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="articleId">成功发布时返回的 article_id</param>
        /// <param name="index">要删除的文章在图文消息中的位置，第一篇编号为1，该字段不填或填0会删除全部文章</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult Delete(string accessTokenOrAppId,string articleId,int index =0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/delete?access_token={0}";

                var data = new
                {
                    article_id = articleId,
                    index = index
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 通过 article_id 获取已发布文章
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="articleId">要获取的草稿的article_id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static FreePublishGetArticleResultJson GetArticle(string accessTokenOrAppId,string articleId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/getarticle?access_token={0}";

                var data = new
                {
                    article_id = articleId
                };
                return CommonJsonSend.Send<FreePublishGetArticleResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取成功发布列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="no_content">1 表示不返回 content 字段，0 表示正常返回，默认为 0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static FreePublishBatchGetResultJson GetFreePublishList(string accessTokenOrAppId, int offset, int count, int no_content = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/batchget?access_token={0}";

                var date = new
                {
                    offset = offset,
                    count = count,
                    no_content = no_content
                };

                return CommonJsonSend.Send<FreePublishBatchGetResultJson>(accessToken, url, date, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】发布接口
        /// 开发者需要先将图文素材以草稿的形式保存（见“草稿箱/新建草稿”，如需从已保存的草稿中选择，见“草稿箱/获取草稿列表”），选择要发布的草稿 media_id 进行发布
        /// 请注意：正常情况下调用成功时，errcode将为0，此时只意味着发布任务提交成功，并不意味着此时发布已经完成，所以，仍有可能在后续的发布过程中出现异常情况导致发布失败，如原创声明失败、平台审核不通过等。
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<SubmitFreePublishResultJson> SubmitAsync(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/submit?access_token={0}";

                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.SendAsync<SubmitFreePublishResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】发布状态轮询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="publishId">发布任务id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetFreePublishResultJson> GetAsync(string accessTokenOrAppId, string publishId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/get?access_token={0}";

                var data = new
                {
                    publish_id = publishId
                };
                return CommonJsonSend.SendAsync<GetFreePublishResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】删除发布
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="articleId">成功发布时返回的 article_id</param>
        /// <param name="index">要删除的文章在图文消息中的位置，第一篇编号为1，该字段不填或填0会删除全部文章</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeleteAsync(string accessTokenOrAppId, string articleId, int index = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/delete?access_token={0}";

                var data = new
                {
                    article_id = articleId,
                    index = index
                };
                return CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】通过 article_id 获取已发布文章
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="articleId">要获取的草稿的article_id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<FreePublishGetArticleResultJson> GetArticleAsync(string accessTokenOrAppId, string articleId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/getarticle?access_token={0}";

                var data = new
                {
                    article_id = articleId
                };
                return CommonJsonSend.SendAsync<FreePublishGetArticleResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取成功发布列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="no_content">1 表示不返回 content 字段，0 表示正常返回，默认为 0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<FreePublishBatchGetResultJson> GetFreePublishListAsync(string accessTokenOrAppId, int offset, int count, int no_content = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/freepublish/batchget?access_token={0}";

                var date = new
                {
                    offset = offset,
                    count = count,
                    no_content = no_content
                };

                return CommonJsonSend.SendAsync<FreePublishBatchGetResultJson>(accessToken, url, date, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}