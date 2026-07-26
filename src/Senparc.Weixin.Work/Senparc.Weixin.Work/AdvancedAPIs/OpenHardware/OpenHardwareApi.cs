/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareApi.cs
    文件功能描述：企业微信智慧硬件云端接入公共请求实现


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智慧硬件云端接入 API

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>
    /// 企业微信智慧硬件云端接入 API。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static partial class OpenHardwareApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting =
            new JsonSetting(true);

        private static T Post<T>(string path, object data, int timeOut)
            where T : WorkJsonResult, new()
            => CommonJsonSend.Send<T>(null, Config.ApiWorkHost + path, data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);

        private static Task<T> PostAsync<T>(string path, object data, int timeOut)
            where T : WorkJsonResult, new()
            => CommonJsonSend.SendAsync<T>(null, Config.ApiWorkHost + path, data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);

        private static T PostWithModelToken<T>(string modelAccessToken, string path,
            object data, int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.Send<T>(modelAccessToken,
                Config.ApiWorkHost + path + "?model_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);

        private static Task<T> PostWithModelTokenAsync<T>(string modelAccessToken,
            string path, object data, int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.SendAsync<T>(modelAccessToken,
                Config.ApiWorkHost + path + "?model_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);

        private static T PostWithDeviceToken<T>(string deviceAccessToken, string path,
            object data, int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.Send<T>(deviceAccessToken,
                Config.ApiWorkHost + path + "?device_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);

        private static Task<T> PostWithDeviceTokenAsync<T>(string deviceAccessToken,
            string path, object data, int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.SendAsync<T>(deviceAccessToken,
                Config.ApiWorkHost + path + "?device_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);
    }
}
