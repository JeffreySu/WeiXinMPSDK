/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MediaApi.UrlUpload.cs
    文件功能描述：企业微信通过 URL 异步上传临时素材接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加 URL 上传任务提交与结果查询同步/异步入口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.Media;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 多媒体 URL 上传接口扩展。
    /// </summary>
    public static partial class MediaApi
    {
        private const string UploadByUrlPath = "/cgi-bin/media/upload_by_url";
        private const string GetUploadByUrlResultPath = "/cgi-bin/media/get_upload_by_url_result";

        /// <summary>
        /// 提交通过 URL 异步上传临时素材的任务。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/96219">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">URL、文件名、素材类型及校验信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>异步上传任务 ID。</returns>
        public static MediaUploadByUrlResult UploadByUrl(string accessTokenOrAppKey,
            MediaUploadByUrlRequest request, int timeOut = Config.TIME_OUT)
            => PostUrlUpload<MediaUploadByUrlResult>(accessTokenOrAppKey, UploadByUrlPath, request, timeOut);

        /// <summary>
        /// 异步提交通过 URL 上传临时素材的任务。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/96219">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">URL、文件名、素材类型及校验信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>异步上传任务 ID。</returns>
        public static Task<MediaUploadByUrlResult> UploadByUrlAsync(string accessTokenOrAppKey,
            MediaUploadByUrlRequest request, int timeOut = Config.TIME_OUT)
            => PostUrlUploadAsync<MediaUploadByUrlResult>(accessTokenOrAppKey, UploadByUrlPath, request, timeOut);

        /// <summary>
        /// 获取通过 URL 异步上传临时素材的任务结果。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/96219">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待查询的任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>任务状态及素材上传明细。</returns>
        public static MediaUploadByUrlTaskResult GetUploadByUrlResult(string accessTokenOrAppKey,
            MediaGetUploadByUrlResultRequest request, int timeOut = Config.TIME_OUT)
            => PostUrlUpload<MediaUploadByUrlTaskResult>(accessTokenOrAppKey, GetUploadByUrlResultPath,
                request, timeOut);

        /// <summary>
        /// 异步获取通过 URL 上传临时素材的任务结果。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/96219">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待查询的任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>任务状态及素材上传明细。</returns>
        public static Task<MediaUploadByUrlTaskResult> GetUploadByUrlResultAsync(string accessTokenOrAppKey,
            MediaGetUploadByUrlResultRequest request, int timeOut = Config.TIME_OUT)
            => PostUrlUploadAsync<MediaUploadByUrlTaskResult>(accessTokenOrAppKey, GetUploadByUrlResultPath,
                request, timeOut);

        private static T PostUrlUpload<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : Senparc.Weixin.Entities.WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut,
                jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        private static Task<T> PostUrlUploadAsync<T>(string accessTokenOrAppKey, string path, object request,
            int timeOut) where T : Senparc.Weixin.Entities.WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut,
                jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);
    }
}
