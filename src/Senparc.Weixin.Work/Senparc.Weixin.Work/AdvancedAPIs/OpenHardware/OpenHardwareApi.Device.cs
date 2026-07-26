/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareApi.Device.cs
    文件功能描述：企业微信智慧硬件设备接入接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智慧硬件凭证、设备和成员接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>
    /// 企业微信智慧硬件凭证、设备和成员接口。
    /// </summary>
    public static partial class OpenHardwareApi
    {
        private const string GetModelTokenPath = "/cgi-bin/openhw/get_model_token";
        private const string GetDeviceSecretPath = "/cgi-bin/openhw/get_device_secret";
        private const string GetDeviceTokenPath = "/cgi-bin/openhw/get_device_token";
        private const string RegisterDevicePath = "/cgi-bin/openhw/model/register_sn";
        private const string UnregisterDevicePath = "/cgi-bin/openhw/model/unregister_sn";
        private const string GetDeviceDetailPath = "/cgi-bin/openhw/device/get_device_detail";
        private const string ReportDeviceStatusPath = "/cgi-bin/openhw/device/report_device_status";
        private const string GetUserInfoByPagePath = "/cgi-bin/openhw/device/get_userinfo_by_page";
        private const string GetUserInfoByIdsPath = "/cgi-bin/openhw/device/get_userinfo_by_ids";
        private const string ReportFirmwareUpgradeResultPath = "/cgi-bin/openhw/device/report_firmware_upgrade_result";
        private const string GenerateLoginQrCodePath = "/cgi-bin/openhw/device/gen_login_qrcode";
        private const string GenerateIdQrCodePath = "/cgi-bin/openhw/device/gen_id_dynamic_qrcode";

        /// <summary>
        /// 使用型号标识、密钥和最新 Ticket 获取设备型号调用凭证。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95992"/></para>
        /// </summary>
        /// <param name="data">设备型号凭证请求。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备型号调用凭证及有效期。</returns>
        public static OpenHardwareGetModelTokenResult GetModelToken(
            OpenHardwareGetModelTokenRequest data, int timeOut = Config.TIME_OUT)
            => Post<OpenHardwareGetModelTokenResult>(GetModelTokenPath, data, timeOut);

        /// <summary>
        /// 异步使用型号标识、密钥和最新 Ticket 获取设备型号调用凭证。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95992"/></para>
        /// </summary>
        /// <param name="data">设备型号凭证请求。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备型号调用凭证及有效期。</returns>
        public static Task<OpenHardwareGetModelTokenResult> GetModelTokenAsync(
            OpenHardwareGetModelTokenRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<OpenHardwareGetModelTokenResult>(GetModelTokenPath, data,
                timeOut);

        /// <summary>
        /// 使用扫码授权码获取设备授权密钥和首次设备调用凭证。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95993"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">扫码授权码。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备授权密钥、设备调用凭证及有效期。</returns>
        public static OpenHardwareGetDeviceSecretResult GetDeviceSecret(
            string modelAccessToken, OpenHardwareGetDeviceSecretRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelToken<OpenHardwareGetDeviceSecretResult>(modelAccessToken,
                GetDeviceSecretPath, data, timeOut);

        /// <summary>
        /// 异步使用扫码授权码获取设备授权密钥和首次设备调用凭证。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95993"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">扫码授权码。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备授权密钥、设备调用凭证及有效期。</returns>
        public static Task<OpenHardwareGetDeviceSecretResult> GetDeviceSecretAsync(
            string modelAccessToken, OpenHardwareGetDeviceSecretRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelTokenAsync<OpenHardwareGetDeviceSecretResult>(
                modelAccessToken, GetDeviceSecretPath, data, timeOut);

        /// <summary>
        /// 使用设备序列号和授权密钥刷新设备调用凭证。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96022"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">设备序列号和设备授权密钥。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备调用凭证及有效期。</returns>
        public static OpenHardwareGetDeviceTokenResult GetDeviceToken(
            string modelAccessToken, OpenHardwareGetDeviceTokenRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelToken<OpenHardwareGetDeviceTokenResult>(modelAccessToken,
                GetDeviceTokenPath, data, timeOut);

        /// <summary>
        /// 异步使用设备序列号和授权密钥刷新设备调用凭证。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96022"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">设备序列号和设备授权密钥。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备调用凭证及有效期。</returns>
        public static Task<OpenHardwareGetDeviceTokenResult> GetDeviceTokenAsync(
            string modelAccessToken, OpenHardwareGetDeviceTokenRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelTokenAsync<OpenHardwareGetDeviceTokenResult>(
                modelAccessToken, GetDeviceTokenPath, data, timeOut);

        /// <summary>
        /// 在设备型号下录入一个设备序列号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95980"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">待录入设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备静态身份识别二维码内容。</returns>
        public static OpenHardwareRegisterDeviceResult RegisterDevice(
            string modelAccessToken, OpenHardwareDeviceSerialNumberRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelToken<OpenHardwareRegisterDeviceResult>(modelAccessToken,
                RegisterDevicePath, data, timeOut);

        /// <summary>
        /// 异步在设备型号下录入一个设备序列号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95980"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">待录入设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备静态身份识别二维码内容。</returns>
        public static Task<OpenHardwareRegisterDeviceResult> RegisterDeviceAsync(
            string modelAccessToken, OpenHardwareDeviceSerialNumberRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelTokenAsync<OpenHardwareRegisterDeviceResult>(
                modelAccessToken, RegisterDevicePath, data, timeOut);

        /// <summary>
        /// 从设备型号下删除一个尚可删除的设备序列号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95981"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">待删除设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult UnregisterDevice(string modelAccessToken,
            OpenHardwareDeviceSerialNumberRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelToken<WorkJsonResult>(modelAccessToken,
                UnregisterDevicePath, data, timeOut);

        /// <summary>
        /// 异步从设备型号下删除一个尚可删除的设备序列号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95981"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">待删除设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> UnregisterDeviceAsync(
            string modelAccessToken, OpenHardwareDeviceSerialNumberRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelTokenAsync<WorkJsonResult>(modelAccessToken,
                UnregisterDevicePath, data, timeOut);

        /// <summary>
        /// 获取设备名称、绑定状态和静态身份识别二维码等详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95982"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">待查询设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备详情。</returns>
        public static OpenHardwareGetDeviceDetailResult GetDeviceDetail(
            string modelAccessToken, OpenHardwareDeviceSerialNumberRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelToken<OpenHardwareGetDeviceDetailResult>(modelAccessToken,
                GetDeviceDetailPath, data, timeOut);

        /// <summary>
        /// 异步获取设备名称、绑定状态和静态身份识别二维码等详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95982"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">待查询设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备详情。</returns>
        public static Task<OpenHardwareGetDeviceDetailResult> GetDeviceDetailAsync(
            string modelAccessToken, OpenHardwareDeviceSerialNumberRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelTokenAsync<OpenHardwareGetDeviceDetailResult>(
                modelAccessToken, GetDeviceDetailPath, data, timeOut);

        /// <summary>
        /// 更新设备在线状态及固件版本信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95983"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">设备状态和固件版本。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult ReportDeviceStatus(string deviceAccessToken,
            OpenHardwareReportDeviceStatusRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<WorkJsonResult>(deviceAccessToken,
                ReportDeviceStatusPath, data, timeOut);

        /// <summary>
        /// 异步更新设备在线状态及固件版本信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95983"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">设备状态和固件版本。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> ReportDeviceStatusAsync(
            string deviceAccessToken, OpenHardwareReportDeviceStatusRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<WorkJsonResult>(deviceAccessToken,
                ReportDeviceStatusPath, data, timeOut);

        /// <summary>
        /// 分页全量获取设备可见的成员和门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95984"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">版本号和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>通讯录版本、成员及下一页游标。</returns>
        public static OpenHardwareGetUserInfoByPageResult GetUserInfoByPage(
            string deviceAccessToken, OpenHardwareGetUserInfoByPageRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<OpenHardwareGetUserInfoByPageResult>(
                deviceAccessToken, GetUserInfoByPagePath, data, timeOut);

        /// <summary>
        /// 异步分页全量获取设备可见的成员和门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95984"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">版本号和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>通讯录版本、成员及下一页游标。</returns>
        public static Task<OpenHardwareGetUserInfoByPageResult>
            GetUserInfoByPageAsync(string deviceAccessToken,
                OpenHardwareGetUserInfoByPageRequest data,
                int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<OpenHardwareGetUserInfoByPageResult>(
                deviceAccessToken, GetUserInfoByPagePath, data, timeOut);

        /// <summary>
        /// 获取设备中指定成员的信息和门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96037"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">待查询成员标识列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>指定成员信息。</returns>
        public static OpenHardwareGetUserInfoByIdsResult GetUserInfoByIds(
            string deviceAccessToken, OpenHardwareGetUserInfoByIdsRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<OpenHardwareGetUserInfoByIdsResult>(
                deviceAccessToken, GetUserInfoByIdsPath, data, timeOut);

        /// <summary>
        /// 异步获取设备中指定成员的信息和门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96037"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">待查询成员标识列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>指定成员信息。</returns>
        public static Task<OpenHardwareGetUserInfoByIdsResult> GetUserInfoByIdsAsync(
            string deviceAccessToken, OpenHardwareGetUserInfoByIdsRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<OpenHardwareGetUserInfoByIdsResult>(
                deviceAccessToken, GetUserInfoByIdsPath, data, timeOut);

        /// <summary>
        /// 上报企业微信下发的设备固件升级任务结果。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95999"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">操作 ID、执行结果和当前固件版本。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult ReportFirmwareUpgradeResult(
            string deviceAccessToken, OpenHardwareFirmwareUpgradeResultRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<WorkJsonResult>(deviceAccessToken,
                ReportFirmwareUpgradeResultPath, data, timeOut);

        /// <summary>
        /// 异步上报企业微信下发的设备固件升级任务结果。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95999"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">操作 ID、执行结果和当前固件版本。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> ReportFirmwareUpgradeResultAsync(
            string deviceAccessToken, OpenHardwareFirmwareUpgradeResultRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<WorkJsonResult>(deviceAccessToken,
                ReportFirmwareUpgradeResultPath, data, timeOut);

        /// <summary>
        /// 生成供企业用户进入设备管理菜单的静态或动态二维码内容。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98023"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">二维码类型和透传状态值。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>二维码内容和可选有效期。</returns>
        public static OpenHardwareQrCodeResult GenerateLoginQrCode(
            string deviceAccessToken, OpenHardwareGenerateLoginQrCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceToken<OpenHardwareQrCodeResult>(deviceAccessToken,
                GenerateLoginQrCodePath, data, timeOut);

        /// <summary>
        /// 异步生成供企业用户进入设备管理菜单的静态或动态二维码内容。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98023"/></para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证。</param>
        /// <param name="data">二维码类型和透传状态值。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>二维码内容和可选有效期。</returns>
        public static Task<OpenHardwareQrCodeResult> GenerateLoginQrCodeAsync(
            string deviceAccessToken, OpenHardwareGenerateLoginQrCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithDeviceTokenAsync<OpenHardwareQrCodeResult>(deviceAccessToken,
                GenerateLoginQrCodePath, data, timeOut);

        /// <summary>
        /// 为指定设备生成动态身份识别二维码内容。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98024"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">设备序列号和透传状态值。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>动态身份识别二维码内容及有效期。</returns>
        public static OpenHardwareQrCodeResult GenerateIdDynamicQrCode(
            string modelAccessToken, OpenHardwareGenerateIdQrCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelToken<OpenHardwareQrCodeResult>(modelAccessToken,
                GenerateIdQrCodePath, data, timeOut);

        /// <summary>
        /// 异步为指定设备生成动态身份识别二维码内容。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98024"/></para>
        /// </summary>
        /// <param name="modelAccessToken">设备型号调用凭证。</param>
        /// <param name="data">设备序列号和透传状态值。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>动态身份识别二维码内容及有效期。</returns>
        public static Task<OpenHardwareQrCodeResult> GenerateIdDynamicQrCodeAsync(
            string modelAccessToken, OpenHardwareGenerateIdQrCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => PostWithModelTokenAsync<OpenHardwareQrCodeResult>(modelAccessToken,
                GenerateIdQrCodePath, data, timeOut);
    }
}
