/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MediaApi.cs
    文件功能描述：多媒体文件接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    -----------------------------------
    
    修改标识：Senparc - 20170617
    修改描述：从QY移植，同步Work接口

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造

----------------------------------------------------------------*/

/*
    接口详见：http://work.weixin.qq.com/api/doc#10112
 */

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.Media;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 多媒体文件接口
    /// </summary>
    public static class MediaApi
    {
        #region 同步方法

        /// <summary>
        /// 上传临时媒体文件【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <param name="media">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static UploadTemporaryResultJson Upload(string accessTokenOrAppKey, UploadMediaFileType type, string media, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/media/upload?access_token={0}&type={1}", accessToken.AsUrlData(), type.ToString());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = media;
                return Post.PostFileGetJson<UploadTemporaryResultJson>(url, null, fileDictionary, null, null, null, false, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取临时媒体文件【QY移植修改】
        /// 权限说明：完全公开，media_id在同一企业内所有应用之间可以共享。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public static void Get(string accessTokenOrAppKey, string mediaId, Stream stream)
        {
            ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/media/get?access_token={0}&media_id={1}",
              accessToken.AsUrlData(), mediaId.AsUrlData());

                HttpUtility.Get.Download(url, stream);//todo 异常处理

                return new WorkJsonResult() { errcode = ReturnCode_Work.请求成功, errmsg = "ok" };
            }, accessTokenOrAppKey);



        }
        /// <summary>
        /// 获取临时媒体文件并保存到指定目录中
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="mediaId"></param>
        /// <param name="dir">保存目录</param>
        public static string Get(string accessTokenOrAppKey, string mediaId, string dir)
        {
            var result = ApiHandlerWapper.TryCommonApi(accessToken =>
           {
               var url = string.Format(Config.ApiWorkHost + "/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken.AsUrlData(), mediaId.AsUrlData());
               var fileName = HttpUtility.Get.Download(url, dir);
               return new WorkJsonResult() { errcode = ReturnCode_Work.请求成功, errmsg = fileName };
           }, accessTokenOrAppKey);
            return result.errmsg;
        }
        /// <summary>
        /// 上传永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <param name="timeOut"></param>
        /// <param name="mpNewsArticles"></param>
        /// <returns></returns>
        public static UploadForeverResultJson AddMpNews(string accessTokenOrAppKey, int agentId, int timeOut = Config.TIME_OUT, params MpNewsArticle[] mpNewsArticles)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/add_mpnews?access_token={0}",
                accessToken.AsUrlData());

                var data = new
                {
                    agentid = agentId,
                    mpnews = new
                    {
                        articles = mpNewsArticles
                    }
                };

                return CommonJsonSend.Send<UploadForeverResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 上传其他类型永久素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="type"></param>
        /// <param name="agentId"></param>
        /// <param name="media"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadForeverResultJson AddMaterial(string accessTokenOrAppKey, UploadMediaFileType type, int agentId, string media, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/add_material?agentid={1}&type={2}&access_token={0}", accessToken.AsUrlData(), agentId, type);
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = media;
                return Post.PostFileGetJson<UploadForeverResultJson>(url, null, fileDictionary, null, null, null, false, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static GetForeverMpNewsResult GetForeverMpNews(string accessTokenOrAppKey, int agentId, string mediaId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url =
                string.Format(
                    Config.ApiWorkHost + "/cgi-bin/material/get?access_token={0}&media_id={1}&agentid={2}",
                    accessToken.AsUrlData(), mediaId.AsUrlData(), agentId);

                return CommonJsonSend.Send<GetForeverMpNewsResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取临时媒体文件
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public static void GetForeverMaterial(string accessTokenOrAppKey, int agentId, string mediaId, Stream stream)
        {
            ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url =
                string.Format(
                    Config.ApiWorkHost + "/cgi-bin/material/get?access_token={0}&media_id={1}&agentid={2}",
                    accessToken.AsUrlData(), mediaId.AsUrlData(), agentId);

                HttpUtility.Get.Download(url, stream);//todo 异常处理
                return new WorkJsonResult() { errcode = ReturnCode_Work.请求成功, errmsg = "ok" };
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static WorkJsonResult DeleteForeverMaterial(string accessTokenOrAppKey, int agentId, string mediaId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url =
                string.Format(
                    Config.ApiWorkHost + "/cgi-bin/material/del?access_token={0}&agentid={1}&media_id={2}",
                    accessToken.AsUrlData(), agentId, mediaId.AsUrlData());

                return CommonJsonSend.Send<WorkJsonResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="mediaId"></param>
        /// <param name="agentId"></param>
        /// <param name="timeOut"></param>
        /// <param name="mpNewsArticles"></param>
        /// <returns></returns>
        public static UploadForeverResultJson UpdateMpNews(string accessTokenOrAppKey, string mediaId, int agentId, int timeOut = Config.TIME_OUT, params MpNewsArticle[] mpNewsArticles)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/update_mpnews?access_token={0}",
                accessToken.AsUrlData());

                var data = new
                {
                    agentid = agentId,
                    media_id = mediaId,
                    mpnews = new
                    {
                        articles = mpNewsArticles
                    }
                };

                return CommonJsonSend.Send<UploadForeverResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public static GetCountResult GetCount(string accessTokenOrAppKey, int agentId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/get_count?access_token={0}&agentid={1}",
                accessToken.AsUrlData(), agentId);

                return CommonJsonSend.Send<GetCountResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【Work中未定义】【疑似】获取素材列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="type"></param>
        /// <param name="agentId"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static BatchGetMaterialResult BatchGetMaterial(string accessTokenOrAppKey, UploadMediaFileType type, int agentId, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/batchget?access_token={0}",
                accessToken.AsUrlData());

                var data = new
                {
                    type = type.ToString(),
                    agentid = agentId,
                    offset = offset,
                    count = count,
                };

                return CommonJsonSend.Send<BatchGetMaterialResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }
        /// <summary>
        /// 上传图文消息内的图片
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="media"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadimgMediaResult UploadimgMedia(string accessTokenOrAppKey, string media, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/media/uploadimg?access_token={0}",
                accessToken.AsUrlData());

                var data = new
                {
                    media = media
                };

                return CommonJsonSend.Send<UploadimgMediaResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】上传临时媒体文件【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <param name="media">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<UploadTemporaryResultJson> UploadAsync(string accessTokenOrAppKey, UploadMediaFileType type, string media, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/media/upload?access_token={0}&type={1}", accessToken.AsUrlData(), type.ToString());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = media;
                return await Post.PostFileGetJsonAsync<UploadTemporaryResultJson>(url, null, fileDictionary, null, null, null, false, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】获取临时媒体文件【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public static async Task GetAsync(string accessTokenOrAppKey, string mediaId, Stream stream)
        {
            await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/media/get?access_token={0}&media_id={1}",
                accessToken.AsUrlData(), mediaId.AsUrlData());
                await HttpUtility.Get.DownloadAsync(url, stream);//todo 异常处理
                return new WorkJsonResult() { errcode = ReturnCode_Work.请求成功, errmsg = "ok" };
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】上传永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <param name="timeOut"></param>
        /// <param name="mpNewsArticles"></param>
        /// <returns></returns>
        public static async Task<UploadForeverResultJson> AddMpNewsAsync(string accessTokenOrAppKey, int agentId, int timeOut = Config.TIME_OUT, params MpNewsArticle[] mpNewsArticles)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/add_mpnews?access_token={0}",
                accessToken.AsUrlData());

                var data = new
                {
                    agentid = agentId,
                    mpnews = new
                    {
                        articles = mpNewsArticles
                    }
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UploadForeverResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】上传其他类型永久素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="type"></param>
        /// <param name="agentId"></param>
        /// <param name="media"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UploadForeverResultJson> AddMaterialAsync(string accessTokenOrAppKey, UploadMediaFileType type, int agentId, string media, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/add_material?agentid={1}&type={2}&access_token={0}", accessToken.AsUrlData(), agentId, type);
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = media;
                return await Post.PostFileGetJsonAsync<UploadForeverResultJson>(url, null, fileDictionary, null, null, null, false, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】获取永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static async Task<GetForeverMpNewsResult> GetForeverMpNewsAsync(string accessTokenOrAppKey, int agentId, string mediaId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url =
                string.Format(
                    Config.ApiWorkHost + "/cgi-bin/material/get?access_token={0}&media_id={1}&agentid={2}",
                    accessToken.AsUrlData(), mediaId.AsUrlData(), agentId);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetForeverMpNewsResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        ///【异步方法】获取临时媒体文件
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public static async Task GetForeverMaterialAsync(string accessTokenOrAppKey, int agentId, string mediaId, Stream stream)
        {
            await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url =
                string.Format(
                    Config.ApiWorkHost + "/cgi-bin/material/get?access_token={0}&media_id={1}&agentid={2}",
                    accessToken.AsUrlData(), mediaId.AsUrlData(), agentId);

                await HttpUtility.Get.DownloadAsync(url, stream);//todo 异常处理

                return new WorkJsonResult() { errcode = ReturnCode_Work.请求成功, errmsg = "ok" };
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】删除永久素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DeleteForeverMaterialAsync(string accessTokenOrAppKey, int agentId, string mediaId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url =
                string.Format(
                    Config.ApiWorkHost + "/cgi-bin/material/del?access_token={0}&agentid={1}&media_id={2}",
                    accessToken.AsUrlData(), agentId, mediaId.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】修改永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="mediaId"></param>
        /// <param name="agentId"></param>
        /// <param name="timeOut"></param>
        /// <param name="mpNewsArticles"></param>
        /// <returns></returns>
        public static async Task<UploadForeverResultJson> UpdateMpNewsAsync(string accessTokenOrAppKey, string mediaId, int agentId, int timeOut = Config.TIME_OUT, params MpNewsArticle[] mpNewsArticles)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/update_mpnews?access_token={0}",
                accessToken.AsUrlData());

                var data = new
                {
                    agentid = agentId,
                    media_id = mediaId,
                    mpnews = new
                    {
                        articles = mpNewsArticles
                    }
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UploadForeverResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】获取素材总数
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public static async Task<GetCountResult> GetCountAsync(string accessTokenOrAppKey, int agentId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/get_count?access_token={0}&agentid={1}",
                accessToken.AsUrlData(), agentId);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCountResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】【Work中未定义】【疑似】获取素材列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="type"></param>
        /// <param name="agentId"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<BatchGetMaterialResult> BatchGetMaterialAsync(string accessTokenOrAppKey, UploadMediaFileType type, int agentId, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/material/batchget?access_token={0}",
                accessToken.AsUrlData());

                var data = new
                {
                    type = type.ToString(),
                    agentid = agentId,
                    offset = offset,
                    count = count,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<BatchGetMaterialResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /*上传的图片限制
          大小不超过2MB，支持JPG,PNG格式
          每天上传的图片不能超过100张*/
        /// <summary>
        /// 【异步方法】上传图文消息内的图片
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="media"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>

        public static async Task<UploadimgMediaResult> UploadimgMediaAsync(string accessTokenOrAppKey, string media, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/media/uploadimg?access_token={0}",
                accessToken.AsUrlData());

                var data = new
                {
                    media = media
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UploadimgMediaResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }
        #endregion
#endif
    }
}
