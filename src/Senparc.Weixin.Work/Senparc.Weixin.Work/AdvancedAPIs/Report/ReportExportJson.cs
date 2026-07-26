/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ReportExportJson.cs
    文件功能描述：企业微信汇报导出请求与结果模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐汇报导出及任务结果强类型模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    /// <summary>
    /// 将汇报记录导出到企业微信文档的请求。
    /// </summary>
    public class ExportReportDocumentRequest
    {
        /// <summary>
        /// 汇报记录单号。
        /// </summary>
        public string journaluuid { get; set; }

        /// <summary>
        /// 用于接收导出内容的企业微信文档 ID。
        /// </summary>
        public string docid { get; set; }
    }

    /// <summary>
    /// 发起汇报导出任务的结果。
    /// </summary>
    public class ExportReportDocumentResult : WorkJsonResult
    {
        /// <summary>
        /// 导出任务 ID，用于查询任务处理结果。
        /// </summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// 查询汇报导出任务结果的请求。
    /// </summary>
    public class GetReportExportDocumentResultRequest
    {
        /// <summary>
        /// 发起导出任务时返回的任务 ID。
        /// </summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// 查询汇报导出任务的结果。
    /// </summary>
    public class GetReportExportDocumentResult : WorkJsonResult
    {
        /// <summary>
        /// 导出任务状态，具体状态值以企业微信官方文档为准。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 导出成功后返回的文档临时下载地址；任务尚未完成时可能为空。
        /// </summary>
        public string url { get; set; }
    }
}
