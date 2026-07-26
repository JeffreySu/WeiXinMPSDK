/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：HealthApi.cs
    文件功能描述：企业微信健康上报接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐健康上报统计、任务和答案接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Health
{
    /// <summary>
    /// 企业微信健康上报接口。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class HealthApi
    {
        private const string GetStatisticsPath = "/cgi-bin/health/get_health_report_stat";
        private const string GetReportJobIdsPath = "/cgi-bin/health/get_report_jobids";
        private const string GetReportJobInfoPath = "/cgi-bin/health/get_report_job_info";
        private const string GetReportAnswerPath = "/cgi-bin/health/get_report_answer";

        /// <summary>
        /// 获取指定日期的健康上报使用统计。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93676"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">健康上报应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">统计日期。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>应用使用次数和使用人数。</returns>
        public static HealthGetReportStatisticsResult GetStatistics(string accessTokenOrAppKey,
            HealthGetReportStatisticsRequest data, int timeOut = Config.TIME_OUT)
            => Post<HealthGetReportStatisticsResult>(accessTokenOrAppKey, GetStatisticsPath, data, timeOut);

        /// <summary>
        /// 异步获取指定日期的健康上报使用统计。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93676"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">健康上报应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">统计日期。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>应用使用次数和使用人数。</returns>
        public static Task<HealthGetReportStatisticsResult> GetStatisticsAsync(
            string accessTokenOrAppKey, HealthGetReportStatisticsRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<HealthGetReportStatisticsResult>(accessTokenOrAppKey,
                GetStatisticsPath, data, timeOut);

        /// <summary>
        /// 分页获取健康上报任务 ID。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93677"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">健康上报应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>任务 ID 列表和分页结束标记。</returns>
        public static HealthGetReportJobIdsResult GetReportJobIds(string accessTokenOrAppKey,
            HealthGetReportJobIdsRequest data, int timeOut = Config.TIME_OUT)
            => Post<HealthGetReportJobIdsResult>(accessTokenOrAppKey, GetReportJobIdsPath, data, timeOut);

        /// <summary>
        /// 异步分页获取健康上报任务 ID。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93677"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">健康上报应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>任务 ID 列表和分页结束标记。</returns>
        public static Task<HealthGetReportJobIdsResult> GetReportJobIdsAsync(
            string accessTokenOrAppKey, HealthGetReportJobIdsRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<HealthGetReportJobIdsResult>(accessTokenOrAppKey,
                GetReportJobIdsPath, data, timeOut);

        /// <summary>
        /// 获取指定日期的健康上报任务配置。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93678"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">健康上报应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">任务 ID 和日期。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>任务范围、汇报对象和问题模板。</returns>
        public static HealthGetReportJobInfoResult GetReportJobInfo(string accessTokenOrAppKey,
            HealthGetReportJobInfoRequest data, int timeOut = Config.TIME_OUT)
            => Post<HealthGetReportJobInfoResult>(accessTokenOrAppKey,
                GetReportJobInfoPath, data, timeOut);

        /// <summary>
        /// 异步获取指定日期的健康上报任务配置。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93678"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">健康上报应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">任务 ID 和日期。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>任务范围、汇报对象和问题模板。</returns>
        public static Task<HealthGetReportJobInfoResult> GetReportJobInfoAsync(
            string accessTokenOrAppKey, HealthGetReportJobInfoRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<HealthGetReportJobInfoResult>(accessTokenOrAppKey,
                GetReportJobInfoPath, data, timeOut);

        /// <summary>
        /// 分页获取指定日期的健康上报答案。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93679"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">健康上报应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">任务、日期和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>成员、学生或家长提交的答案。</returns>
        public static HealthGetReportAnswerResult GetReportAnswer(string accessTokenOrAppKey,
            HealthGetReportAnswerRequest data, int timeOut = Config.TIME_OUT)
            => Post<HealthGetReportAnswerResult>(accessTokenOrAppKey,
                GetReportAnswerPath, data, timeOut);

        /// <summary>
        /// 异步分页获取指定日期的健康上报答案。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93679"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">健康上报应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">任务、日期和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>成员、学生或家长提交的答案。</returns>
        public static Task<HealthGetReportAnswerResult> GetReportAnswerAsync(
            string accessTokenOrAppKey, HealthGetReportAnswerRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<HealthGetReportAnswerResult>(accessTokenOrAppKey,
                GetReportAnswerPath, data, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object data, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object data,
            int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppKey);
    }
}
