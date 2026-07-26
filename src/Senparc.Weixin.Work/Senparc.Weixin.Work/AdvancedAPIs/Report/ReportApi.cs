/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ReportApi.cs
    文件功能描述：企业微信汇报接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 补齐汇报记录、详情与统计接口；拆分政民沟通居民上报扩展接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    /// <summary>
    /// 企业微信汇报接口。
    /// </summary>
    public static partial class ReportApi
    {
        private const string GetRecordListPath = "/cgi-bin/oa/journal/get_record_list";
        private const string GetRecordDetailPath = "/cgi-bin/oa/journal/get_record_detail";
        private const string GetStatListPath = "/cgi-bin/oa/journal/get_stat_list";

        /// <summary>
        /// 批量获取汇报记录单号。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">汇报记录查询条件；时间跨度不能超过一个月，单页最多 100 条。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>汇报记录单号及分页信息。</returns>
        public static GetReportRecordListResult GetRecordList(string accessTokenOrAppKey,
            GetReportRecordListRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetReportRecordListResult>(accessTokenOrAppKey, GetRecordListPath, request, timeOut);

        /// <summary>
        /// 异步批量获取汇报记录单号。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">汇报记录查询条件；时间跨度不能超过一个月，单页最多 100 条。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>汇报记录单号及分页信息。</returns>
        public static Task<GetReportRecordListResult> GetRecordListAsync(string accessTokenOrAppKey,
            GetReportRecordListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetReportRecordListResult>(accessTokenOrAppKey, GetRecordListPath, request, timeOut);

        /// <summary>
        /// 获取汇报记录详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">包含汇报记录单号的请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>汇报详情、表单控件值和评论。</returns>
        public static GetReportRecordDetailResult GetRecordDetail(string accessTokenOrAppKey,
            GetReportRecordDetailRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetReportRecordDetailResult>(accessTokenOrAppKey, GetRecordDetailPath, request, timeOut);

        /// <summary>
        /// 异步获取汇报记录详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">包含汇报记录单号的请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>汇报详情、表单控件值和评论。</returns>
        public static Task<GetReportRecordDetailResult> GetRecordDetailAsync(string accessTokenOrAppKey,
            GetReportRecordDetailRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetReportRecordDetailResult>(accessTokenOrAppKey, GetRecordDetailPath, request, timeOut);

        /// <summary>
        /// 获取汇报统计数据。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">模板和统计时间范围；时间跨度不能超过一年。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>汇报模板的已汇报与未汇报统计。</returns>
        public static GetReportStatListResult GetStatList(string accessTokenOrAppKey,
            GetReportStatListRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetReportStatListResult>(accessTokenOrAppKey, GetStatListPath, request, timeOut);

        /// <summary>
        /// 异步获取汇报统计数据。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">模板和统计时间范围；时间跨度不能超过一年。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>汇报模板的已汇报与未汇报统计。</returns>
        public static Task<GetReportStatListResult> GetStatListAsync(string accessTokenOrAppKey,
            GetReportStatListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetReportStatListResult>(accessTokenOrAppKey, GetStatListPath, request, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);
    }
}
