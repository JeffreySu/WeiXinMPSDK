/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Record.cs
    文件功能描述：企业微信会议录制管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议录制查询、统计、共享配置和删除接口

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
    }
}
