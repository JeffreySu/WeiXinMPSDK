/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：HardwareDeviceApi.cs
    文件功能描述：HardwareDeviceApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.HardwareDevice
{
    /// <summary>
    /// GetSnTicket 接口返回结果。
    /// </summary>
    public class GetSnTicketJsonResult : WxJsonResult
    {
        public string sn_ticket { get; set; }
    }

    /// <summary>
    /// CreateIotGroup 接口返回结果。
    /// </summary>
    public class CreateIotGroupJsonResult : WxJsonResult
    {
        public string group_id { get; set; }
    }

    /// <summary>
    /// IotDevice 微信接口数据模型。
    /// </summary>
    public class IotDevice
    {
        public string model_id { get; set; }
        public string sn { get; set; }
    }

    /// <summary>
    /// IotDeviceOperation 接口返回结果。
    /// </summary>
    public class IotDeviceOperationResult : IotDevice
    {
        public int errcode { get; set; }
        public long expire_time { get; set; }
    }

    /// <summary>
    /// GetIotGroupInfo 接口返回结果。
    /// </summary>
    public class GetIotGroupInfoJsonResult : WxJsonResult
    {
        public string group_name { get; set; }
        public List<IotDevice> device_list { get; set; }
        public string model_id { get; set; }
        public string model_type { get; set; }
    }

    /// <summary>
    /// IotDeviceOperation 接口返回结果。
    /// </summary>
    public class IotDeviceOperationJsonResult : WxJsonResult
    {
        public List<IotDeviceOperationResult> device_list { get; set; }
    }

    /// <summary>
    /// LicenseDevice 微信接口数据模型。
    /// </summary>
    public class LicenseDevice : IotDevice
    {
        public uint active_number { get; set; }
    }

    /// <summary>
    /// LicensePackage 微信接口数据模型。
    /// </summary>
    public class LicensePackage
    {
        public string pkg_id { get; set; }
        public int pkg_type { get; set; }
        public long start_time { get; set; }
        public long end_time { get; set; }
        public int pkg_status { get; set; }
        public long used { get; set; }
        public long all { get; set; }
    }

    /// <summary>
    /// GetLicensePackageList 接口返回结果。
    /// </summary>
    public class GetLicensePackageListJsonResult : WxJsonResult
    {
        public List<LicensePackage> pkg_list { get; set; }
        public long max_active_number { get; set; }
    }

    /// <summary>
    /// 小程序硬件设备接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class HardwareDeviceApi
    {
        private static T Post<T>(string accessTokenOrAppId, string path, object data, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppId);
        }

        private static async Task<T> PostAsync<T>(string accessTokenOrAppId, string path, object data, int timeOut)
            where T : WxJsonResult, new()
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送 IoT 设备消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="templateId">消息或短信模板 ID。</param>
        /// <param name="sn">IoT 设备序列号。</param>
        /// <param name="modelId">IoT 设备型号 ID。</param>
        /// <param name="page">页码。</param>
        /// <param name="toOpenIdList">消息接收用户 OpenId 列表。</param>
        /// <param name="data">接口业务数据。</param>
        /// <param name="miniprogramState">小程序版本状态。</param>
        /// <param name="lang">小程序语言版本。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SendDeviceMessage(string accessTokenOrAppId, string templateId, string sn,
            string modelId, string page, IList<string> toOpenIdList, object data, string miniprogramState = "formal",
            string lang = "zh_CN", int timeOut = Config.TIME_OUT)
        {
            return Post<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/message/device/subscribe/send", new
            {
                template_id = templateId,
                sn,
                modelId,
                page,
                to_openid_list = toOpenIdList,
                miniprogram_state = miniprogramState,
                data,
                lang
            }, timeOut);
        }

        /// <summary>
        /// 获取 IoT 设备序列号票据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="sn">IoT 设备序列号。</param>
        /// <param name="modelId">IoT 设备型号 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetSnTicketJsonResult GetSnTicket(string accessTokenOrAppId, string sn, string modelId,
            int timeOut = Config.TIME_OUT)
        {
            return Post<GetSnTicketJsonResult>(accessTokenOrAppId, "/wxa/getsnticket",
                new { sn, model_id = modelId }, timeOut);
        }

        /// <summary>
        /// 创建 IoT 设备组。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="modelId">IoT 设备型号 ID。</param>
        /// <param name="groupName">IoT 设备组名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static CreateIotGroupJsonResult CreateIotGroup(string accessTokenOrAppId, string modelId,
            string groupName, int timeOut = Config.TIME_OUT)
        {
            return Post<CreateIotGroupJsonResult>(accessTokenOrAppId, "/wxa/business/group/createid",
                new { model_id = modelId, group_name = groupName }, timeOut);
        }

        /// <summary>
        /// 获取 IoT 设备组信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="groupId">群聊、设备组或音视频房间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetIotGroupInfoJsonResult GetIotGroupInfo(string accessTokenOrAppId, string groupId,
            int timeOut = Config.TIME_OUT)
        {
            return Post<GetIotGroupInfoJsonResult>(accessTokenOrAppId, "/wxa/business/group/getinfo",
                new { group_id = groupId }, timeOut);
        }

        /// <summary>
        /// 向 IoT 设备组添加设备。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="groupId">群聊、设备组或音视频房间 ID。</param>
        /// <param name="devices">IoT 设备列表。</param>
        /// <param name="forceAdd">是否强制将设备加入当前设备组。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static IotDeviceOperationJsonResult AddIotGroupDevices(string accessTokenOrAppId, string groupId,
            IList<IotDevice> devices, bool forceAdd = false, int timeOut = Config.TIME_OUT)
        {
            return Post<IotDeviceOperationJsonResult>(accessTokenOrAppId, "/wxa/business/group/adddevice",
                new { group_id = groupId, device_list = devices, force_add = forceAdd }, timeOut);
        }

        /// <summary>
        /// 从 IoT 设备组移除设备。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="groupId">群聊、设备组或音视频房间 ID。</param>
        /// <param name="devices">IoT 设备列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static IotDeviceOperationJsonResult RemoveIotGroupDevices(string accessTokenOrAppId, string groupId,
            IList<IotDevice> devices, int timeOut = Config.TIME_OUT)
        {
            return Post<IotDeviceOperationJsonResult>(accessTokenOrAppId, "/wxa/business/group/removedevice",
                new { group_id = groupId, device_list = devices }, timeOut);
        }

        /// <summary>
        /// 获取 IoT 设备 License 套餐列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="packageType">IoT License 套餐类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetLicensePackageListJsonResult GetLicensePackageList(string accessTokenOrAppId, int packageType,
            int timeOut = Config.TIME_OUT)
        {
            return Post<GetLicensePackageListJsonResult>(accessTokenOrAppId, "/wxa/business/license/getpkglist",
                new { pkg_type = packageType }, timeOut);
        }

        /// <summary>
        /// 激活 IoT 设备 License。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="packageType">IoT License 套餐类型。</param>
        /// <param name="devices">IoT 设备列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static IotDeviceOperationJsonResult ActivateLicenseDevices(string accessTokenOrAppId, int packageType,
            IList<LicenseDevice> devices, int timeOut = Config.TIME_OUT)
        {
            return Post<IotDeviceOperationJsonResult>(accessTokenOrAppId, "/wxa/business/license/activedevice",
                new { pkg_type = packageType, device_list = devices }, timeOut);
        }

        /// <summary>
        /// 查询 IoT 设备 License 信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="devices">IoT 设备列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static IotDeviceOperationJsonResult GetLicenseDeviceInfo(string accessTokenOrAppId,
            IList<IotDevice> devices, int timeOut = Config.TIME_OUT)
        {
            return Post<IotDeviceOperationJsonResult>(accessTokenOrAppId, "/wxa/business/license/getdeviceinfo",
                new { device_list = devices }, timeOut);
        }

        /// <summary>
        /// 异步发送 IoT 设备消息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="templateId">消息或短信模板 ID。</param>
        /// <param name="sn">IoT 设备序列号。</param>
        /// <param name="modelId">IoT 设备型号 ID。</param>
        /// <param name="page">页码。</param>
        /// <param name="toOpenIdList">消息接收用户 OpenId 列表。</param>
        /// <param name="data">接口业务数据。</param>
        /// <param name="miniprogramState">小程序版本状态。</param>
        /// <param name="lang">小程序语言版本。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> SendDeviceMessageAsync(string accessTokenOrAppId, string templateId, string sn,
            string modelId, string page, IList<string> toOpenIdList, object data, string miniprogramState = "formal",
            string lang = "zh_CN", int timeOut = Config.TIME_OUT)
        {
            return PostAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/message/device/subscribe/send", new
            {
                template_id = templateId,
                sn,
                modelId,
                page,
                to_openid_list = toOpenIdList,
                miniprogram_state = miniprogramState,
                data,
                lang
            }, timeOut);
        }

        /// <summary>
        /// 异步获取 IoT 设备序列号票据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="sn">IoT 设备序列号。</param>
        /// <param name="modelId">IoT 设备型号 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetSnTicketJsonResult> GetSnTicketAsync(string accessTokenOrAppId, string sn, string modelId,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetSnTicketJsonResult>(accessTokenOrAppId, "/wxa/getsnticket", new { sn, model_id = modelId }, timeOut);

        /// <summary>
        /// 异步创建 IoT 设备组。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="modelId">IoT 设备型号 ID。</param>
        /// <param name="groupName">IoT 设备组名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<CreateIotGroupJsonResult> CreateIotGroupAsync(string accessTokenOrAppId, string modelId,
            string groupName, int timeOut = Config.TIME_OUT)
            => PostAsync<CreateIotGroupJsonResult>(accessTokenOrAppId, "/wxa/business/group/createid",
                new { model_id = modelId, group_name = groupName }, timeOut);

        /// <summary>
        /// 异步获取 IoT 设备组信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="groupId">群聊、设备组或音视频房间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetIotGroupInfoJsonResult> GetIotGroupInfoAsync(string accessTokenOrAppId, string groupId,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetIotGroupInfoJsonResult>(accessTokenOrAppId, "/wxa/business/group/getinfo",
                new { group_id = groupId }, timeOut);

        /// <summary>
        /// 异步向 IoT 设备组添加设备。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="groupId">群聊、设备组或音视频房间 ID。</param>
        /// <param name="devices">IoT 设备列表。</param>
        /// <param name="forceAdd">是否强制将设备加入当前设备组。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<IotDeviceOperationJsonResult> AddIotGroupDevicesAsync(string accessTokenOrAppId, string groupId,
            IList<IotDevice> devices, bool forceAdd = false, int timeOut = Config.TIME_OUT)
            => PostAsync<IotDeviceOperationJsonResult>(accessTokenOrAppId, "/wxa/business/group/adddevice",
                new { group_id = groupId, device_list = devices, force_add = forceAdd }, timeOut);

        /// <summary>
        /// 异步从 IoT 设备组移除设备。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="groupId">群聊、设备组或音视频房间 ID。</param>
        /// <param name="devices">IoT 设备列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<IotDeviceOperationJsonResult> RemoveIotGroupDevicesAsync(string accessTokenOrAppId, string groupId,
            IList<IotDevice> devices, int timeOut = Config.TIME_OUT)
            => PostAsync<IotDeviceOperationJsonResult>(accessTokenOrAppId, "/wxa/business/group/removedevice",
                new { group_id = groupId, device_list = devices }, timeOut);

        /// <summary>
        /// 异步获取 IoT 设备 License 套餐列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="packageType">IoT License 套餐类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetLicensePackageListJsonResult> GetLicensePackageListAsync(string accessTokenOrAppId,
            int packageType, int timeOut = Config.TIME_OUT)
            => PostAsync<GetLicensePackageListJsonResult>(accessTokenOrAppId, "/wxa/business/license/getpkglist",
                new { pkg_type = packageType }, timeOut);

        /// <summary>
        /// 异步激活 IoT 设备 License。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="packageType">IoT License 套餐类型。</param>
        /// <param name="devices">IoT 设备列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<IotDeviceOperationJsonResult> ActivateLicenseDevicesAsync(string accessTokenOrAppId,
            int packageType, IList<LicenseDevice> devices, int timeOut = Config.TIME_OUT)
            => PostAsync<IotDeviceOperationJsonResult>(accessTokenOrAppId, "/wxa/business/license/activedevice",
                new { pkg_type = packageType, device_list = devices }, timeOut);

        /// <summary>
        /// 异步查询 IoT 设备 License 信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="devices">IoT 设备列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<IotDeviceOperationJsonResult> GetLicenseDeviceInfoAsync(string accessTokenOrAppId,
            IList<IotDevice> devices, int timeOut = Config.TIME_OUT)
            => PostAsync<IotDeviceOperationJsonResult>(accessTokenOrAppId, "/wxa/business/license/getdeviceinfo",
                new { device_list = devices }, timeOut);
    }
}
