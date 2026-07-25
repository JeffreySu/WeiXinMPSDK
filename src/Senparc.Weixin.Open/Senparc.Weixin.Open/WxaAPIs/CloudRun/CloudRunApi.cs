#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CloudRunApi.cs
    文件功能描述：CloudRunApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.CloudRun;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 第三方平台微信云托管接口。
    /// </summary>
    /// <remarks>
    /// <para>本类接口均使用第三方平台的 <c>component_access_token</c>。</para>
    /// <para><see href="https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/cloudbase-batch/cloudrun/getShareCloudbaseEnv.html">微信官方文档</see></para>
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class CloudRunApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 环境共享

        /// <summary>
        /// 查询指定小程序已共享的微信云托管环境（GetShareCloudbaseEnv）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appIds">需要查询的小程序 AppID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>AppID 与云托管环境的共享关系。</returns>
        public static CloudRunGetShareEnvJsonResult GetShareCloudbaseEnv(string componentAccessToken,
            IEnumerable<string> appIds, int timeOut = Config.TIME_OUT) =>
            Send<CloudRunGetShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchgetenvid",
                CreateGetShareRequest(appIds), timeOut);

        /// <summary>
        /// 异步查询指定小程序已共享的微信云托管环境（GetShareCloudbaseEnv）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appIds">需要查询的小程序 AppID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>AppID 与云托管环境的共享关系。</returns>
        public static Task<CloudRunGetShareEnvJsonResult> GetShareCloudbaseEnvAsync(string componentAccessToken,
            IEnumerable<string> appIds, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunGetShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchgetenvid",
                CreateGetShareRequest(appIds), timeOut);

        /// <summary>
        /// 将微信云托管环境共享给指定小程序（ShareCloudbaseEnv）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境和目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>共享结果以及需要管理员确认的链接。</returns>
        public static CloudRunShareEnvJsonResult ShareCloudbaseEnv(string componentAccessToken,
            IEnumerable<CloudRunEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            Send<CloudRunShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchshareenv",
                CreateShareRequest(data), timeOut);

        /// <summary>
        /// 异步将微信云托管环境共享给指定小程序（ShareCloudbaseEnv）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境和目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>共享结果以及需要管理员确认的链接。</returns>
        public static Task<CloudRunShareEnvJsonResult> ShareCloudbaseEnvAsync(string componentAccessToken,
            IEnumerable<CloudRunEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchshareenv",
                CreateShareRequest(data), timeOut);

        /// <summary>
        /// 解除微信云托管环境与指定小程序的共享关系（UnshareCloudbaseEnv）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境和目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>解除共享的处理结果。</returns>
        public static CloudRunShareEnvJsonResult UnshareCloudbaseEnv(string componentAccessToken,
            IEnumerable<CloudRunEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            Send<CloudRunShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchunshareenv",
                CreateShareRequest(data), timeOut);

        /// <summary>
        /// 异步解除微信云托管环境与指定小程序的共享关系（UnshareCloudbaseEnv）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境和目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>解除共享的处理结果。</returns>
        public static Task<CloudRunShareEnvJsonResult> UnshareCloudbaseEnvAsync(string componentAccessToken,
            IEnumerable<CloudRunEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchunshareenv",
                CreateShareRequest(data), timeOut);

        #endregion

        #region 环境与服务

        /// <summary>
        /// 获取第三方平台创建的微信云托管环境列表（GetWxCloudBaseRunEnvs）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信云托管环境列表。</returns>
        public static CloudRunEnvListJsonResult GetWxCloudBaseRunEnvs(string componentAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudRunEnvListJsonResult>(componentAccessToken, "/componenttcb/describeenvs", new { }, timeOut);

        /// <summary>
        /// 异步获取第三方平台创建的微信云托管环境列表（GetWxCloudBaseRunEnvs）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信云托管环境列表。</returns>
        public static Task<CloudRunEnvListJsonResult> GetWxCloudBaseRunEnvsAsync(string componentAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunEnvListJsonResult>(componentAccessToken, "/componenttcb/describeenvs", new { }, timeOut);

        /// <summary>
        /// 创建微信云托管环境（CreateCloudbaseEnv）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境别名及可选网络参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新环境 ID 和后付费订单号。</returns>
        public static CloudRunCreateEnvJsonResult CreateCloudbaseEnv(string componentAccessToken,
            CloudRunCreateEnvRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudRunCreateEnvJsonResult>(componentAccessToken, "/componenttcb/createcloudbaserunenv",
                request, timeOut);

        /// <summary>
        /// 异步创建微信云托管环境（CreateCloudbaseEnv）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境别名及可选网络参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新环境 ID 和后付费订单号。</returns>
        public static Task<CloudRunCreateEnvJsonResult> CreateCloudbaseEnvAsync(string componentAccessToken,
            CloudRunCreateEnvRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunCreateEnvJsonResult>(componentAccessToken, "/componenttcb/createcloudbaserunenv",
                request, timeOut);

        /// <summary>
        /// 在微信云托管环境中创建服务（CreateCloudbaseService）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、服务名称、镜像仓库及网络配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult CreateCloudbaseService(string componentAccessToken,
            CloudRunCreateServiceRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(componentAccessToken, "/componenttcb/establishcloudbaserunserver", request, timeOut);

        /// <summary>
        /// 异步在微信云托管环境中创建服务（CreateCloudbaseService）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、服务名称、镜像仓库及网络配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> CreateCloudbaseServiceAsync(string componentAccessToken,
            CloudRunCreateServiceRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(componentAccessToken, "/componenttcb/establishcloudbaserunserver", request, timeOut);

        #endregion

        #region 服务版本

        /// <summary>
        /// 创建微信云托管服务版本（CreateCloudbaseServiceVersion）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">服务版本、资源规格、镜像或代码包及扩缩容配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>版本名称和操作记录 ID。</returns>
        public static CloudRunServiceVersionJsonResult CreateCloudbaseServiceVersion(string componentAccessToken,
            CloudRunCreateServiceVersionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudRunServiceVersionJsonResult>(componentAccessToken,
                "/componenttcb/createcloudbaserunserverversion", request, timeOut);

        /// <summary>
        /// 异步创建微信云托管服务版本（CreateCloudbaseServiceVersion）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">服务版本、资源规格、镜像或代码包及扩缩容配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>版本名称和操作记录 ID。</returns>
        public static Task<CloudRunServiceVersionJsonResult> CreateCloudbaseServiceVersionAsync(
            string componentAccessToken, CloudRunCreateServiceVersionRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunServiceVersionJsonResult>(componentAccessToken,
                "/componenttcb/createcloudbaserunserverversion", request, timeOut);

        /// <summary>
        /// 滚动更新微信云托管服务版本（UpdateCloudbaseServiceVersion）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">目标版本、资源规格、镜像或代码包及扩缩容配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果和操作记录 ID。</returns>
        public static CloudRunServiceVersionJsonResult UpdateCloudbaseServiceVersion(string componentAccessToken,
            CloudRunUpdateServiceVersionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudRunServiceVersionJsonResult>(componentAccessToken,
                "/componenttcb/rollupdatecloudbaserunserverversion", request, timeOut);

        /// <summary>
        /// 异步滚动更新微信云托管服务版本（UpdateCloudbaseServiceVersion）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">目标版本、资源规格、镜像或代码包及扩缩容配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果和操作记录 ID。</returns>
        public static Task<CloudRunServiceVersionJsonResult> UpdateCloudbaseServiceVersionAsync(
            string componentAccessToken, CloudRunUpdateServiceVersionRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunServiceVersionJsonResult>(componentAccessToken,
                "/componenttcb/rollupdatecloudbaserunserverversion", request, timeOut);

        /// <summary>
        /// 删除微信云托管服务版本（DeleteCloudbaseServiceVersion）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、服务和待删除版本参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        public static CloudRunReleaseJsonResult DeleteCloudbaseServiceVersion(string componentAccessToken,
            CloudRunDeleteServiceVersionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudRunReleaseJsonResult>(componentAccessToken,
                "/componenttcb/deletecloudbaserunserverversion", request, timeOut);

        /// <summary>
        /// 异步删除微信云托管服务版本（DeleteCloudbaseServiceVersion）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、服务和待删除版本参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        public static Task<CloudRunReleaseJsonResult> DeleteCloudbaseServiceVersionAsync(
            string componentAccessToken, CloudRunDeleteServiceVersionRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunReleaseJsonResult>(componentAccessToken,
                "/componenttcb/deletecloudbaserunserverversion", request, timeOut);

        /// <summary>
        /// 发布微信云托管服务版本（ReleaseCloudbaseServiceVersion）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、服务和待发布版本参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>发布结果和发布单 ID。</returns>
        public static CloudRunReleaseJsonResult ReleaseCloudbaseServiceVersion(string componentAccessToken,
            CloudRunReleaseServiceVersionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudRunReleaseJsonResult>(componentAccessToken, "/componenttcb/releasecloudbaserunversion",
                request, timeOut);

        /// <summary>
        /// 异步发布微信云托管服务版本（ReleaseCloudbaseServiceVersion）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、服务和待发布版本参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>发布结果和发布单 ID。</returns>
        public static Task<CloudRunReleaseJsonResult> ReleaseCloudbaseServiceVersionAsync(
            string componentAccessToken, CloudRunReleaseServiceVersionRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudRunReleaseJsonResult>(componentAccessToken, "/componenttcb/releasecloudbaserunversion",
                request, timeOut);

        #endregion

        /// <summary>
        /// 创建查询共享环境的请求体，并固定来源类型为微信云托管。
        /// </summary>
        private static object CreateGetShareRequest(IEnumerable<string> appIds)
        {
            return new { appids = appIds, source_type = 1 };
        }

        /// <summary>
        /// 创建共享或解除共享环境的请求体，并固定来源类型为微信云托管。
        /// </summary>
        private static object CreateShareRequest(IEnumerable<CloudRunEnvShareItem> data)
        {
            return new { data, source_type = 1 };
        }

        /// <summary>
        /// 构造微信云托管接口地址。
        /// </summary>
        private static string BuildUrl(string componentAccessToken, string path)
        {
            return $"{Config.ApiMpHost}{path}?access_token={componentAccessToken.AsUrlData()}";
        }

        /// <summary>
        /// 同步发送微信云托管请求。
        /// </summary>
        private static T Send<T>(string componentAccessToken, string path, object request, int timeOut)
        {
            return CommonJsonSend.Send<T>(null, BuildUrl(componentAccessToken, path), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>
        /// 异步发送微信云托管请求。
        /// </summary>
        private static Task<T> SendAsync<T>(string componentAccessToken, string path, object request, int timeOut)
        {
            return CommonJsonSend.SendAsync<T>(null, BuildUrl(componentAccessToken, path), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }
    }
}
