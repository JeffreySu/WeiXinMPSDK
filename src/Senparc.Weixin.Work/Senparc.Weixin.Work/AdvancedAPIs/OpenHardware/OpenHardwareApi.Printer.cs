/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareApi.Printer.cs
    文件功能描述：企业微信智慧硬件打印扫描接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智慧硬件打印任务、扫描文件和转码接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>
    /// 企业微信智慧硬件打印和扫描设备接口。
    /// </summary>
    public static partial class OpenHardwareApi
    {
        private const string GetPrinterJobListPath = "/cgi-bin/openhw/device/get_printer_job_list";
        private const string GetPrinterJobDownloadUrlPath = "/cgi-bin/openhw/device/get_printer_job_download_url";
        private const string ReportPrinterJobStatusPath = "/cgi-bin/openhw/device/report_printer_job_status";
        private const string PushScanFilePath = "/cgi-bin/openhw/device/push_scan_file";
        private const string SetPrinterJobTransResultPath = "/cgi-bin/openhw/device/set_printer_job_trans_result";

        /// <summary>
        /// 按成员、状态、时间或任务 ID 分页获取打印任务。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96407"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">任务筛选和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>打印任务和下一页游标。</returns>
        public static OpenHardwareGetPrinterJobListResult GetPrinterJobList(
            string deviceAccessToken, OpenHardwareGetPrinterJobListRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<OpenHardwareGetPrinterJobListResult>(
                deviceAccessToken, GetPrinterJobListPath, data, timeOut);

        /// <summary>
        /// 异步按成员、状态、时间或任务 ID 分页获取打印任务。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96407"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">任务筛选和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>打印任务和下一页游标。</returns>
        public static Task<OpenHardwareGetPrinterJobListResult>
            GetPrinterJobListAsync(string deviceAccessToken,
                OpenHardwareGetPrinterJobListRequest data,
                int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<OpenHardwareGetPrinterJobListResult>(
                deviceAccessToken, GetPrinterJobListPath, data, timeOut);

        /// <summary>
        /// 获取指定打印任务的加密文件下载地址和解密密钥。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96408"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">打印任务 ID。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>文件下载地址和 AES 解密密钥。</returns>
        public static OpenHardwarePrinterJobDownloadResult
            GetPrinterJobDownloadUrl(string deviceAccessToken,
                OpenHardwarePrinterJobRequest data,
                int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<OpenHardwarePrinterJobDownloadResult>(
                deviceAccessToken, GetPrinterJobDownloadUrlPath, data, timeOut);

        /// <summary>
        /// 异步获取指定打印任务的加密文件下载地址和解密密钥。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96408"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">打印任务 ID。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>文件下载地址和 AES 解密密钥。</returns>
        public static Task<OpenHardwarePrinterJobDownloadResult>
            GetPrinterJobDownloadUrlAsync(string deviceAccessToken,
                OpenHardwarePrinterJobRequest data,
                int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<OpenHardwarePrinterJobDownloadResult>(
                deviceAccessToken, GetPrinterJobDownloadUrlPath, data, timeOut);

        /// <summary>
        /// 上报指定打印任务的成功或失败状态。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96409"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">任务状态及可选错误信息。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult ReportPrinterJobStatus(
            string deviceAccessToken, OpenHardwareReportPrinterJobStatusRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<WorkJsonResult>(deviceAccessToken,
                ReportPrinterJobStatusPath, data, timeOut);

        /// <summary>
        /// 异步上报指定打印任务的成功或失败状态。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96409"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">任务状态及可选错误信息。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> ReportPrinterJobStatusAsync(
            string deviceAccessToken, OpenHardwareReportPrinterJobStatusRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<WorkJsonResult>(deviceAccessToken,
                ReportPrinterJobStatusPath, data, timeOut);

        /// <summary>
        /// 将打印机扫描文件推送给指定企业成员。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96410"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">成员、文件和扫描授权码。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult PushScanFile(string deviceAccessToken,
            OpenHardwarePushScanFileRequest data, int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<WorkJsonResult>(deviceAccessToken,
                PushScanFilePath, data, timeOut);

        /// <summary>
        /// 异步将打印机扫描文件推送给指定企业成员。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96410"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">成员、文件和扫描授权码。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> PushScanFileAsync(string deviceAccessToken,
            OpenHardwarePushScanFileRequest data, int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<WorkJsonResult>(deviceAccessToken,
                PushScanFilePath, data, timeOut);

        /// <summary>
        /// 返回企业微信下发打印转码任务的文件或错误结果。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96412"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">任务、设置版本、文件及转码结果。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult SetPrinterJobTransResult(
            string deviceAccessToken,
            OpenHardwarePrinterJobTransResultRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<WorkJsonResult>(deviceAccessToken,
                SetPrinterJobTransResultPath, data, timeOut);

        /// <summary>
        /// 异步返回企业微信下发打印转码任务的文件或错误结果。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96412"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">任务、设置版本、文件及转码结果。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> SetPrinterJobTransResultAsync(
            string deviceAccessToken,
            OpenHardwarePrinterJobTransResultRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<WorkJsonResult>(deviceAccessToken,
                SetPrinterJobTransResultPath, data, timeOut);
    }
}
