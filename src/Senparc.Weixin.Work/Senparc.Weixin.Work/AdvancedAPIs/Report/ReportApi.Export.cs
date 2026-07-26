/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ReportApi.Export.cs
    文件功能描述：企业微信汇报导出接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐汇报导出及导出结果查询接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    /// <summary>
    /// 企业微信汇报导出接口。
    /// </summary>
    public static partial class ReportApi
    {
        private const string ExportDocumentPath = "/cgi-bin/oa/journal/export_doc";
        private const string GetExportDocumentResultPath = "/cgi-bin/oa/journal/get_export_doc_result";

        /// <summary>
        /// 将指定汇报记录导出到企业微信文档。
        /// <see href="https://developer.work.weixin.qq.com/document/path/96108"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">汇报记录单号和目标文档 ID。</param>
        /// <param name="timeOut">代理请求超时时间，单位为毫秒。</param>
        /// <returns>异步导出任务 ID。</returns>
        public static ExportReportDocumentResult ExportDocument(string accessTokenOrAppKey,
            ExportReportDocumentRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExportReportDocumentResult>(accessTokenOrAppKey, ExportDocumentPath, request, timeOut);

        /// <summary>
        /// 异步将指定汇报记录导出到企业微信文档。
        /// <see href="https://developer.work.weixin.qq.com/document/path/96108"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">汇报记录单号和目标文档 ID。</param>
        /// <param name="timeOut">代理请求超时时间，单位为毫秒。</param>
        /// <returns>异步导出任务 ID。</returns>
        public static Task<ExportReportDocumentResult> ExportDocumentAsync(string accessTokenOrAppKey,
            ExportReportDocumentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExportReportDocumentResult>(accessTokenOrAppKey, ExportDocumentPath, request, timeOut);

        /// <summary>
        /// 查询汇报导出任务结果。
        /// <see href="https://developer.work.weixin.qq.com/document/path/96108"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">包含导出任务 ID 的请求。</param>
        /// <param name="timeOut">代理请求超时时间，单位为毫秒。</param>
        /// <returns>任务状态及导出文档的临时下载地址。</returns>
        public static GetReportExportDocumentResult GetExportDocumentResult(string accessTokenOrAppKey,
            GetReportExportDocumentResultRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetReportExportDocumentResult>(accessTokenOrAppKey, GetExportDocumentResultPath, request,
                timeOut);

        /// <summary>
        /// 异步查询汇报导出任务结果。
        /// <see href="https://developer.work.weixin.qq.com/document/path/96108"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">包含导出任务 ID 的请求。</param>
        /// <param name="timeOut">代理请求超时时间，单位为毫秒。</param>
        /// <returns>任务状态及导出文档的临时下载地址。</returns>
        public static Task<GetReportExportDocumentResult> GetExportDocumentResultAsync(string accessTokenOrAppKey,
            GetReportExportDocumentResultRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetReportExportDocumentResult>(accessTokenOrAppKey, GetExportDocumentResultPath, request,
                timeOut);
    }
}
