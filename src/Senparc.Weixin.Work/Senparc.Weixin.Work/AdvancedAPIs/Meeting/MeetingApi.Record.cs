/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Record.cs
    文件功能描述：企业微信会议录制管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议录制查询、下载、转写、统计、共享配置和删除接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string GetMeetingRecordListPath = "/cgi-bin/meeting/record/list";
        private const string GetMeetingRecordStatisticsPath = "/cgi-bin/meeting/record/get_statistics";
        private const string UpdateMeetingRecordSharingConfigPath = "/cgi-bin/meeting/record/update_sharing_config";
        private const string DeleteMeetingRecordPath = "/cgi-bin/meeting/record/delete";
        private const string DeleteMeetingRecordFilePath = "/cgi-bin/meeting/record/delete_file";
        private const string GetMeetingRecordFilePath = "/cgi-bin/meeting/record/get_file";
        private const string GetMeetingRecordFileListPath = "/cgi-bin/meeting/record/get_file_list";
        private const string GetMeetingRecordTranscriptParagraphListPath =
            "/cgi-bin/meeting/record/transcript/get_paragraph_list";
        private const string GetMeetingRecordTranscriptDetailPath =
            "/cgi-bin/meeting/record/transcript/get_detail";
        private const string SearchMeetingRecordTranscriptPath =
            "/cgi-bin/meeting/record/transcript/search";

        /// <summary>
        /// 分页获取企业微信会议录制列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98192"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、主持人、时间范围和分页筛选条件。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议录制、录制文件、共享配置和分页信息。</returns>
        public static GetMeetingRecordListResult GetMeetingRecordList(string accessTokenOrAppKey,
            GetMeetingRecordListRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRecordListResult>(accessTokenOrAppKey,
                GetMeetingRecordListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取企业微信会议录制列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98192"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、主持人、时间范围和分页筛选条件。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议录制、录制文件、共享配置和分页信息。</returns>
        public static Task<GetMeetingRecordListResult> GetMeetingRecordListAsync(
            string accessTokenOrAppKey, GetMeetingRecordListRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRecordListResult>(accessTokenOrAppKey,
                GetMeetingRecordListPath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议录制观看和下载统计。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98209"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、录制 ID 和可选时间范围。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>按日期汇总的观看次数和下载次数。</returns>
        public static GetMeetingRecordStatisticsResult GetMeetingRecordStatistics(
            string accessTokenOrAppKey, GetMeetingRecordStatisticsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRecordStatisticsResult>(accessTokenOrAppKey,
                GetMeetingRecordStatisticsPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议录制观看和下载统计。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98209"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、录制 ID 和可选时间范围。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>按日期汇总的观看次数和下载次数。</returns>
        public static Task<GetMeetingRecordStatisticsResult> GetMeetingRecordStatisticsAsync(
            string accessTokenOrAppKey, GetMeetingRecordStatisticsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRecordStatisticsResult>(accessTokenOrAppKey,
                GetMeetingRecordStatisticsPath, request, timeOut);

        /// <summary>
        /// 更新企业微信会议录制共享配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98208"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、录制 ID 和共享权限、密码、有效期及下载配置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议录制共享配置更新结果。</returns>
        public static UpdateMeetingRecordSharingConfigResult UpdateMeetingRecordSharingConfig(
            string accessTokenOrAppKey, UpdateMeetingRecordSharingConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<UpdateMeetingRecordSharingConfigResult>(accessTokenOrAppKey,
                UpdateMeetingRecordSharingConfigPath, request, timeOut);

        /// <summary>
        /// 异步更新企业微信会议录制共享配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98208"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、录制 ID 和共享权限、密码、有效期及下载配置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议录制共享配置更新结果。</returns>
        public static Task<UpdateMeetingRecordSharingConfigResult> UpdateMeetingRecordSharingConfigAsync(
            string accessTokenOrAppKey, UpdateMeetingRecordSharingConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<UpdateMeetingRecordSharingConfigResult>(accessTokenOrAppKey,
                UpdateMeetingRecordSharingConfigPath, request, timeOut);

        /// <summary>
        /// 删除企业微信会议录制。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98206"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和会议录制 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议录制删除结果。</returns>
        public static DeleteMeetingRecordResult DeleteMeetingRecord(string accessTokenOrAppKey,
            DeleteMeetingRecordRequest request, int timeOut = Config.TIME_OUT)
            => Post<DeleteMeetingRecordResult>(accessTokenOrAppKey,
                DeleteMeetingRecordPath, request, timeOut);

        /// <summary>
        /// 异步删除企业微信会议录制。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98206"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和会议录制 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议录制删除结果。</returns>
        public static Task<DeleteMeetingRecordResult> DeleteMeetingRecordAsync(
            string accessTokenOrAppKey, DeleteMeetingRecordRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeleteMeetingRecordResult>(accessTokenOrAppKey,
                DeleteMeetingRecordPath, request, timeOut);

        /// <summary>
        /// 删除企业微信会议中的指定录制文件。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98207"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和录制文件 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>录制文件删除结果。</returns>
        public static DeleteMeetingRecordFileResult DeleteMeetingRecordFile(
            string accessTokenOrAppKey, DeleteMeetingRecordFileRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<DeleteMeetingRecordFileResult>(accessTokenOrAppKey,
                DeleteMeetingRecordFilePath, request, timeOut);

        /// <summary>
        /// 异步删除企业微信会议中的指定录制文件。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98207"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和录制文件 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>录制文件删除结果。</returns>
        public static Task<DeleteMeetingRecordFileResult> DeleteMeetingRecordFileAsync(
            string accessTokenOrAppKey, DeleteMeetingRecordFileRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeleteMeetingRecordFileResult>(accessTokenOrAppKey,
                DeleteMeetingRecordFilePath, request, timeOut);

        /// <summary>
        /// 获取单个企业微信会议录制文件的播放、下载、音频和会议纪要信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98205"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和录制文件 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>录制文件地址、文件类型、会议纪要、智能转写文件和录制时间。</returns>
        public static GetMeetingRecordFileResult GetMeetingRecordFile(
            string accessTokenOrAppKey, GetMeetingRecordFileRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRecordFileResult>(accessTokenOrAppKey,
                GetMeetingRecordFilePath, request, timeOut);

        /// <summary>
        /// 异步获取单个企业微信会议录制文件的播放、下载、音频和会议纪要信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98205"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和录制文件 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>录制文件地址、文件类型、会议纪要、智能转写文件和录制时间。</returns>
        public static Task<GetMeetingRecordFileResult> GetMeetingRecordFileAsync(
            string accessTokenOrAppKey, GetMeetingRecordFileRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRecordFileResult>(accessTokenOrAppKey,
                GetMeetingRecordFilePath, request, timeOut);

        /// <summary>
        /// 获取指定会议录制下的全部录制文件播放和下载地址。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98196"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和会议录制 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议信息及录制文件播放、视频、音频和会议纪要地址列表。</returns>
        public static GetMeetingRecordFileListResult GetMeetingRecordFileList(
            string accessTokenOrAppKey, GetMeetingRecordFileListRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRecordFileListResult>(accessTokenOrAppKey,
                GetMeetingRecordFileListPath, request, timeOut);

        /// <summary>
        /// 异步获取指定会议录制下的全部录制文件播放和下载地址。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98196"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和会议录制 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议信息及录制文件播放、视频、音频和会议纪要地址列表。</returns>
        public static Task<GetMeetingRecordFileListResult> GetMeetingRecordFileListAsync(
            string accessTokenOrAppKey, GetMeetingRecordFileListRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRecordFileListResult>(accessTokenOrAppKey,
                GetMeetingRecordFileListPath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议录制转写的段落 ID 和时间范围。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98212"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和录制文件 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>声纹识别状态及录制转写段落列表。</returns>
        public static GetMeetingRecordTranscriptParagraphListResult
            GetMeetingRecordTranscriptParagraphList(string accessTokenOrAppKey,
                GetMeetingRecordTranscriptParagraphListRequest request,
                int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRecordTranscriptParagraphListResult>(accessTokenOrAppKey,
                GetMeetingRecordTranscriptParagraphListPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议录制转写的段落 ID 和时间范围。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98212"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和录制文件 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>声纹识别状态及录制转写段落列表。</returns>
        public static Task<GetMeetingRecordTranscriptParagraphListResult>
            GetMeetingRecordTranscriptParagraphListAsync(string accessTokenOrAppKey,
                GetMeetingRecordTranscriptParagraphListRequest request,
                int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRecordTranscriptParagraphListResult>(accessTokenOrAppKey,
                GetMeetingRecordTranscriptParagraphListPath, request, timeOut);

        /// <summary>
        /// 分段获取企业微信会议录制转写详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98211"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、录制文件、起始段落及查询数量。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>段落、发言人、句子、词、关键词、声纹状态和分页标记。</returns>
        public static GetMeetingRecordTranscriptDetailResult GetMeetingRecordTranscriptDetail(
            string accessTokenOrAppKey, GetMeetingRecordTranscriptDetailRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingRecordTranscriptDetailResult>(accessTokenOrAppKey,
                GetMeetingRecordTranscriptDetailPath, request, timeOut);

        /// <summary>
        /// 异步分段获取企业微信会议录制转写详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98211"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、录制文件、起始段落及查询数量。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>段落、发言人、句子、词、关键词、声纹状态和分页标记。</returns>
        public static Task<GetMeetingRecordTranscriptDetailResult>
            GetMeetingRecordTranscriptDetailAsync(string accessTokenOrAppKey,
                GetMeetingRecordTranscriptDetailRequest request,
                int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingRecordTranscriptDetailResult>(accessTokenOrAppKey,
                GetMeetingRecordTranscriptDetailPath, request, timeOut);

        /// <summary>
        /// 按指定文本搜索企业微信会议录制转写。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98213"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、录制文件 ID 和搜索文本。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>搜索命中位置和用于时间轴预览的时间点。</returns>
        public static SearchMeetingRecordTranscriptResult SearchMeetingRecordTranscript(
            string accessTokenOrAppKey, SearchMeetingRecordTranscriptRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SearchMeetingRecordTranscriptResult>(accessTokenOrAppKey,
                SearchMeetingRecordTranscriptPath, request, timeOut);

        /// <summary>
        /// 异步按指定文本搜索企业微信会议录制转写。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98213"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、录制文件 ID 和搜索文本。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>搜索命中位置和用于时间轴预览的时间点。</returns>
        public static Task<SearchMeetingRecordTranscriptResult>
            SearchMeetingRecordTranscriptAsync(string accessTokenOrAppKey,
                SearchMeetingRecordTranscriptRequest request,
                int timeOut = Config.TIME_OUT)
            => PostAsync<SearchMeetingRecordTranscriptResult>(accessTokenOrAppKey,
                SearchMeetingRecordTranscriptPath, request, timeOut);
    }
}
