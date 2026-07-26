#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：MiniDramaApi.cs
    文件功能描述：MiniDramaApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama
{
    /// <summary>小程序短剧媒资管理与播放器服务端接口。</summary>
    /// <remarks>
    /// 官方目录共列出 41 项，其中“查询被授权信息”在剧目授权和账号授权目录重复出现，实际对应 40 个唯一请求路径。
    /// 所有接口均支持第三方平台使用 <c>authorizer_access_token</c> 代小程序调用。
    /// </remarks>
    public static partial class MiniDramaApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 媒资上传

        /// <summary>上传单个短剧视频及可选封面文件，适用于小于 10 MB 的视频。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="mediaName">文件名，需采用“剧目名 - 对应剧集数”格式。</param>
        /// <param name="mediaType">视频格式，例如 MP4。</param>
        /// <param name="mediaFilePath">视频文件绝对路径。</param>
        /// <param name="coverType">可选封面格式，例如 JPG。</param>
        /// <param name="coverFilePath">可选封面文件绝对路径；为空时微信截取视频首帧。</param>
        /// <param name="sourceContext">可选来源上下文，上传完成事件会透传。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传后的媒资 ID。</returns>
        /// <remarks>使用官方规定的 multipart/form-data 字段。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_fileupload/api_singlefileupload.html"/>。</remarks>
        public static MiniDramaMediaIdJsonResult SingleFileUpload(string accessTokenOrAppId, string mediaName, string mediaType,
            string mediaFilePath, string coverType = null, string coverFilePath = null, string sourceContext = null,
            int timeOut = Config.TIME_OUT)
        {
            return SendMultipart<MiniDramaMediaIdJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/singlefileupload",
                CreateSingleFileUploadFiles(mediaFilePath, coverFilePath),
                CreateSingleFileUploadFields(mediaName, mediaType, coverType, sourceContext), timeOut);
        }

        /// <summary>异步上传单个短剧视频及可选封面文件。</summary>
        /// <inheritdoc cref="SingleFileUpload"/>
        public static Task<MiniDramaMediaIdJsonResult> SingleFileUploadAsync(string accessTokenOrAppId, string mediaName, string mediaType,
            string mediaFilePath, string coverType = null, string coverFilePath = null, string sourceContext = null,
            int timeOut = Config.TIME_OUT)
        {
            return SendMultipartAsync<MiniDramaMediaIdJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/singlefileupload",
                CreateSingleFileUploadFiles(mediaFilePath, coverFilePath),
                CreateSingleFileUploadFields(mediaName, mediaType, coverType, sourceContext), timeOut);
        }

        /// <summary>从网络 URL 拉取并上传短剧视频。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">文件名、视频 URL、可选封面 URL 和来源上下文。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>异步上传任务 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_fileupload/api_pullupload.html"/>。</remarks>
        public static MiniDramaPullUploadJsonResult PullUpload(string accessTokenOrAppId, MiniDramaPullUploadRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaPullUploadJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/pullupload", request, timeOut);
        }

        /// <summary>异步从网络 URL 拉取并上传短剧视频。</summary>
        /// <inheritdoc cref="PullUpload"/>
        public static Task<MiniDramaPullUploadJsonResult> PullUploadAsync(string accessTokenOrAppId, MiniDramaPullUploadRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaPullUploadJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/pullupload", request, timeOut);
        }

        /// <summary>查询短剧拉取上传任务状态。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>任务类型、状态、完成时间和媒资 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_fileupload/api_gettask.html"/>。</remarks>
        public static MiniDramaGetTaskJsonResult GetTask(string accessTokenOrAppId, MiniDramaGetTaskRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetTaskJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/gettask", request, timeOut);
        }

        /// <summary>异步查询短剧拉取上传任务状态。</summary>
        /// <inheritdoc cref="GetTask"/>
        public static Task<MiniDramaGetTaskJsonResult> GetTaskAsync(string accessTokenOrAppId, MiniDramaGetTaskRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetTaskJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/gettask", request, timeOut);
        }

        /// <summary>申请短剧大文件分片上传。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">视频名称、格式、可选封面格式和来源上下文。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分片上传唯一标识。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_fileupload/api_applyupload.html"/>。</remarks>
        public static MiniDramaApplyUploadJsonResult ApplyUpload(string accessTokenOrAppId, MiniDramaApplyUploadRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaApplyUploadJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/applyupload", request, timeOut);
        }

        /// <summary>异步申请短剧大文件分片上传。</summary>
        /// <inheritdoc cref="ApplyUpload"/>
        public static Task<MiniDramaApplyUploadJsonResult> ApplyUploadAsync(string accessTokenOrAppId, MiniDramaApplyUploadRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaApplyUploadJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/applyupload", request, timeOut);
        }

        /// <summary>上传一个短剧视频或封面分片。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="uploadId">分片上传唯一标识。</param>
        /// <param name="partNumber">分片编号，范围 1 至 100。</param>
        /// <param name="resourceType">资源类型：1 视频，2 封面图片。</param>
        /// <param name="filePath">当前分片文件的绝对路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分片内容 ETag。</returns>
        /// <remarks>每个分片通常为 5 MB，最后一个分片可小于 5 MB。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_fileupload/api_uploadpart.html"/>。</remarks>
        public static MiniDramaUploadPartJsonResult UploadPart(string accessTokenOrAppId, string uploadId, int partNumber,
            int resourceType, string filePath, int timeOut = Config.TIME_OUT)
        {
            return SendMultipart<MiniDramaUploadPartJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/uploadpart",
                new Dictionary<string, string> { ["data"] = filePath },
                CreateUploadPartFields(uploadId, partNumber, resourceType), timeOut);
        }

        /// <summary>异步上传一个短剧视频或封面分片。</summary>
        /// <inheritdoc cref="UploadPart"/>
        public static Task<MiniDramaUploadPartJsonResult> UploadPartAsync(string accessTokenOrAppId, string uploadId, int partNumber,
            int resourceType, string filePath, int timeOut = Config.TIME_OUT)
        {
            return SendMultipartAsync<MiniDramaUploadPartJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/uploadpart",
                new Dictionary<string, string> { ["data"] = filePath },
                CreateUploadPartFields(uploadId, partNumber, resourceType), timeOut);
        }

        /// <summary>确认并合并短剧分片上传。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">上传标识及视频、封面分片 ETag 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>合并后的媒资 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_fileupload/api_commitupload.html"/>。</remarks>
        public static MiniDramaMediaIdJsonResult CommitUpload(string accessTokenOrAppId, MiniDramaCommitUploadRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaMediaIdJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/commitupload", request, timeOut);
        }

        /// <summary>异步确认并合并短剧分片上传。</summary>
        /// <inheritdoc cref="CommitUpload"/>
        public static Task<MiniDramaMediaIdJsonResult> CommitUploadAsync(string accessTokenOrAppId, MiniDramaCommitUploadRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaMediaIdJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/commitupload", request, timeOut);
        }

        #endregion

        #region 媒资管理

        /// <summary>分页查询已上传的短剧媒资。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">剧目、名称、时间及分页筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>媒资信息列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_media/api_listmedia.html"/>。</remarks>
        public static MiniDramaListMediaJsonResult ListMedia(string accessTokenOrAppId, MiniDramaListMediaRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaListMediaJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/listmedia", request, timeOut);
        }

        /// <summary>异步分页查询已上传的短剧媒资。</summary>
        /// <inheritdoc cref="ListMedia"/>
        public static Task<MiniDramaListMediaJsonResult> ListMediaAsync(string accessTokenOrAppId, MiniDramaListMediaRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaListMediaJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/listmedia", request, timeOut);
        }

        /// <summary>查询指定短剧媒资详情。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">媒资 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>媒资文件及审核信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_media/api_getmedia.html"/>。</remarks>
        public static MiniDramaGetMediaJsonResult GetMedia(string accessTokenOrAppId, MiniDramaMediaIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetMediaJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getmedia", request, timeOut);
        }

        /// <summary>异步查询指定短剧媒资详情。</summary>
        /// <inheritdoc cref="GetMedia"/>
        public static Task<MiniDramaGetMediaJsonResult> GetMediaAsync(string accessTokenOrAppId, MiniDramaMediaIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetMediaJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getmedia", request, timeOut);
        }

        /// <summary>获取短剧媒资临时播放链接。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">媒资 ID、过期时间及可选播放限制。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>带鉴权参数的 MP4 和 HLS 播放链接。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_media/api_getmedialink.html"/>。</remarks>
        public static MiniDramaGetMediaLinkJsonResult GetMediaLink(string accessTokenOrAppId, MiniDramaGetMediaLinkRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetMediaLinkJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getmedialink", request, timeOut);
        }

        /// <summary>异步获取短剧媒资临时播放链接。</summary>
        /// <inheritdoc cref="GetMediaLink"/>
        public static Task<MiniDramaGetMediaLinkJsonResult> GetMediaLinkAsync(string accessTokenOrAppId, MiniDramaGetMediaLinkRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetMediaLinkJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getmedialink", request, timeOut);
        }

        /// <summary>删除指定短剧媒资。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">媒资 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/vod_media/api_deletemedia.html"/>。</remarks>
        public static WxJsonResult DeleteMedia(string accessTokenOrAppId, MiniDramaMediaIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/deletemedia", request, timeOut);
        }

        /// <summary>异步删除指定短剧媒资。</summary>
        /// <inheritdoc cref="DeleteMedia"/>
        public static Task<WxJsonResult> DeleteMediaAsync(string accessTokenOrAppId, MiniDramaMediaIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/deletemedia", request, timeOut);
        }

        #endregion

        private static T Send<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, BuildUrl(path), request ?? new { }, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting), accessTokenOrAppId);
        }

        private static Task<T> SendAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken, BuildUrl(path), request ?? new { }, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false), accessTokenOrAppId);
        }

        private static T SendMultipart<T>(string accessTokenOrAppId, string path, Dictionary<string, string> files,
            Dictionary<string, string> fields, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                Post.PostFileGetJson<T>(CommonDI.CommonSP, string.Format(BuildUrl(path), accessToken), null, files, fields,
                    timeOut: timeOut), accessTokenOrAppId);
        }

        private static Task<T> SendMultipartAsync<T>(string accessTokenOrAppId, string path, Dictionary<string, string> files,
            Dictionary<string, string> fields, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await Post.PostFileGetJsonAsync<T>(CommonDI.CommonSP, string.Format(BuildUrl(path), accessToken), null, files, fields,
                    timeOut: timeOut).ConfigureAwait(false), accessTokenOrAppId);
        }

        private static string BuildUrl(string path)
        {
            return Config.ApiMpHost + path + "?access_token={0}";
        }

        private static Dictionary<string, string> CreateSingleFileUploadFiles(string mediaFilePath, string coverFilePath)
        {
            var files = new Dictionary<string, string> { ["media_data"] = mediaFilePath };
            if (!string.IsNullOrEmpty(coverFilePath))
            {
                files["cover_data"] = coverFilePath;
            }

            return files;
        }

        private static Dictionary<string, string> CreateSingleFileUploadFields(string mediaName, string mediaType,
            string coverType, string sourceContext)
        {
            var fields = new Dictionary<string, string>
            {
                ["media_name"] = mediaName,
                ["media_type"] = mediaType
            };
            AddIfNotEmpty(fields, "cover_type", coverType);
            AddIfNotEmpty(fields, "source_context", sourceContext);
            return fields;
        }

        private static Dictionary<string, string> CreateUploadPartFields(string uploadId, int partNumber, int resourceType)
        {
            return new Dictionary<string, string>
            {
                ["upload_id"] = uploadId,
                ["part_number"] = partNumber.ToString(),
                ["resource_type"] = resourceType.ToString()
            };
        }

        private static void AddIfNotEmpty(IDictionary<string, string> dictionary, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                dictionary[key] = value;
            }
        }
    }
}
