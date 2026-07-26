/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LicenseApi.cs
    文件功能描述：企业微信服务商接口调用许可公共请求实现


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐接口调用许可 API

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.License
{
    /// <summary>
    /// 企业微信服务商接口调用许可 API。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static partial class LicenseApi
    {
        private static readonly JsonSetting LicenseIgnoreNullJsonSetting =
            new JsonSetting(true);

        private static T Post<T>(string providerAccessToken, string path, object data,
            int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.Send<T>(providerAccessToken,
                Config.ApiWorkHost + path + "?provider_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: LicenseIgnoreNullJsonSetting);

        private static Task<T> PostAsync<T>(string providerAccessToken, string path,
            object data, int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.SendAsync<T>(providerAccessToken,
                Config.ApiWorkHost + path + "?provider_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: LicenseIgnoreNullJsonSetting);

        private static T Get<T>(string providerAccessToken, string path, int timeOut)
            where T : WorkJsonResult, new()
            => CommonJsonSend.Send<T>(providerAccessToken,
                Config.ApiWorkHost + path + "?provider_access_token={0}", null,
                CommonJsonSendType.GET, timeOut: timeOut);

        private static Task<T> GetAsync<T>(string providerAccessToken, string path,
            int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.SendAsync<T>(providerAccessToken,
                Config.ApiWorkHost + path + "?provider_access_token={0}", null,
                CommonJsonSendType.GET, timeOut: timeOut);
    }
}
