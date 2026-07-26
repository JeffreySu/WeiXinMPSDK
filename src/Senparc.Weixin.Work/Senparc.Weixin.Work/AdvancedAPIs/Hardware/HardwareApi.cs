/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：HardwareApi.cs
    文件功能描述：企业微信硬件设备特征接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐硬件设备特征查询接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs.Hardware
{
    /// <summary>
    /// 企业微信硬件设备特征接口。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class HardwareApi
    {
        private const string GetDeviceFeatureUrl =
            "/cgi-bin/hardware/get_device_feature?provider_access_token={0}";

        /// <summary>
        /// 获取指定硬件设备反馈的自定义特征串。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/92739"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备厂商自定义的特征串。</returns>
        public static HardwareGetDeviceFeatureResult GetDeviceFeature(string providerAccessToken,
            HardwareGetDeviceFeatureRequest data, int timeOut = Config.TIME_OUT)
            => CommonJsonSend.Send<HardwareGetDeviceFeatureResult>(providerAccessToken,
                Config.ApiWorkHost + GetDeviceFeatureUrl, data, CommonJsonSendType.POST,
                timeOut: timeOut);

        /// <summary>
        /// 异步获取指定硬件设备反馈的自定义特征串。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/92739"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备厂商自定义的特征串。</returns>
        public static Task<HardwareGetDeviceFeatureResult> GetDeviceFeatureAsync(
            string providerAccessToken, HardwareGetDeviceFeatureRequest data,
            int timeOut = Config.TIME_OUT)
            => CommonJsonSend.SendAsync<HardwareGetDeviceFeatureResult>(providerAccessToken,
                Config.ApiWorkHost + GetDeviceFeatureUrl, data, CommonJsonSendType.POST,
                timeOut: timeOut);
    }
}
