/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareApi.Attendance.cs
    文件功能描述：企业微信智慧硬件考勤门禁接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智慧硬件考勤门禁数据与结果上报接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>
    /// 企业微信智慧硬件考勤和门禁设备接口。
    /// </summary>
    public static partial class OpenHardwareApi
    {
        private const string ReportCheckinDataPath = "/cgi-bin/openhw/device/report_checkin_data";
        private const string ReportTemperatureDataPath = "/cgi-bin/openhw/device/report_temperature_data";
        private const string ReportAccessDataPath = "/cgi-bin/openhw/device/report_access_data";
        private const string ReportBiometricInfoResultPath = "/cgi-bin/openhw/device/report_bio_info_result";
        private const string ReportRemoteOpenResultPath = "/cgi-bin/openhw/device/report_remote_open_result";

        /// <summary>
        /// 批量上报设备产生的考勤打卡记录。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95985"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">待上报的考勤记录。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>上报失败的记录列表。</returns>
        public static OpenHardwareReportCheckinDataResult ReportCheckinData(
            string deviceAccessToken, OpenHardwareReportCheckinDataRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<OpenHardwareReportCheckinDataResult>(
                deviceAccessToken, ReportCheckinDataPath, data, timeOut);

        /// <summary>
        /// 异步批量上报设备产生的考勤打卡记录。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95985"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">待上报的考勤记录。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>上报失败的记录列表。</returns>
        public static Task<OpenHardwareReportCheckinDataResult>
            ReportCheckinDataAsync(string deviceAccessToken,
                OpenHardwareReportCheckinDataRequest data,
                int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<OpenHardwareReportCheckinDataResult>(
                deviceAccessToken, ReportCheckinDataPath, data, timeOut);

        /// <summary>
        /// 批量上报设备产生的体温检测记录。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95986"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">待上报的体温记录。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>上报失败的记录列表。</returns>
        public static OpenHardwareReportTemperatureDataResult ReportTemperatureData(
            string deviceAccessToken, OpenHardwareReportTemperatureDataRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<OpenHardwareReportTemperatureDataResult>(
                deviceAccessToken, ReportTemperatureDataPath, data, timeOut);

        /// <summary>
        /// 异步批量上报设备产生的体温检测记录。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95986"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">待上报的体温记录。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>上报失败的记录列表。</returns>
        public static Task<OpenHardwareReportTemperatureDataResult>
            ReportTemperatureDataAsync(string deviceAccessToken,
                OpenHardwareReportTemperatureDataRequest data,
                int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<OpenHardwareReportTemperatureDataResult>(
                deviceAccessToken, ReportTemperatureDataPath, data, timeOut);

        /// <summary>
        /// 批量上报设备产生的门禁通行记录。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95997"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">待上报的门禁记录。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>上报失败的记录列表。</returns>
        public static OpenHardwareReportAccessDataResult ReportAccessData(
            string deviceAccessToken, OpenHardwareReportAccessDataRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<OpenHardwareReportAccessDataResult>(
                deviceAccessToken, ReportAccessDataPath, data, timeOut);

        /// <summary>
        /// 异步批量上报设备产生的门禁通行记录。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95997"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">待上报的门禁记录。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>上报失败的记录列表。</returns>
        public static Task<OpenHardwareReportAccessDataResult> ReportAccessDataAsync(
            string deviceAccessToken, OpenHardwareReportAccessDataRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<OpenHardwareReportAccessDataResult>(
                deviceAccessToken, ReportAccessDataPath, data, timeOut);

        /// <summary>
        /// 上报成员识别信息录入或变化指令的执行结果。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96000"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">操作 ID 和执行结果。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult ReportBiometricInfoResult(
            string deviceAccessToken, OpenHardwareBiometricInfoResultRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<WorkJsonResult>(deviceAccessToken,
                ReportBiometricInfoResultPath, data, timeOut);

        /// <summary>
        /// 异步上报成员识别信息录入或变化指令的执行结果。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96000"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">操作 ID 和执行结果。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> ReportBiometricInfoResultAsync(
            string deviceAccessToken, OpenHardwareBiometricInfoResultRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<WorkJsonResult>(deviceAccessToken,
                ReportBiometricInfoResultPath, data, timeOut);

        /// <summary>
        /// 上报远程开门指令的执行结果。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96048"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">操作 ID、类型和执行结果。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult ReportRemoteOpenResult(
            string deviceAccessToken, OpenHardwareRemoteOpenResultRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<WorkJsonResult>(deviceAccessToken,
                ReportRemoteOpenResultPath, data, timeOut);

        /// <summary>
        /// 异步上报远程开门指令的执行结果。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96048"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">操作 ID、类型和执行结果。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> ReportRemoteOpenResultAsync(
            string deviceAccessToken, OpenHardwareRemoteOpenResultRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<WorkJsonResult>(deviceAccessToken,
                ReportRemoteOpenResultPath, data, timeOut);
    }
}
