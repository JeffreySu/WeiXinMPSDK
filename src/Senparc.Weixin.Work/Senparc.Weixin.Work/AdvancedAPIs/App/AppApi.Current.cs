/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AppApi.Current.cs
    文件功能描述：企业微信应用迁移、权限和管理员增量接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐应用迁移、权限及管理员查询接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.App;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    public static partial class AppApi
    {
        private const string MigrateToCustomizedAppPath = "/cgi-bin/agent/migrate_to_customized_app";
        private const string GetAppPermissionsPath = "/cgi-bin/agent/get_permissions";
        private const string GetAppAdminListPath = "/cgi-bin/agent/get_admin_list";

        /// <summary>
        /// 将代开发应用迁移为自建应用。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/96072">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">代开发应用模板调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移操作结果。</returns>
        public static WorkJsonResult MigrateToCustomizedApp(string accessTokenOrAppKey,
            MigrateToCustomizedAppRequest request, int timeOut = Config.TIME_OUT)
            => PostCurrent<WorkJsonResult>(accessTokenOrAppKey, MigrateToCustomizedAppPath, request, timeOut);

        /// <summary>
        /// 异步将代开发应用迁移为自建应用。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/96072">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">代开发应用模板调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移操作结果。</returns>
        public static Task<WorkJsonResult> MigrateToCustomizedAppAsync(string accessTokenOrAppKey,
            MigrateToCustomizedAppRequest request, int timeOut = Config.TIME_OUT)
            => PostCurrentAsync<WorkJsonResult>(accessTokenOrAppKey, MigrateToCustomizedAppPath, request,
                timeOut);

        /// <summary>
        /// 获取应用需要添加的权限列表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/99052">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>应用权限标识列表。</returns>
        public static GetAppPermissionsResult GetAppPermissions(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => PostCurrent<GetAppPermissionsResult>(accessTokenOrAppKey, GetAppPermissionsPath,
                new EmptyAppRequest(), timeOut);

        /// <summary>
        /// 异步获取应用需要添加的权限列表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/99052">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>应用权限标识列表。</returns>
        public static Task<GetAppPermissionsResult> GetAppPermissionsAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => PostCurrentAsync<GetAppPermissionsResult>(accessTokenOrAppKey, GetAppPermissionsPath,
                new EmptyAppRequest(), timeOut);

        /// <summary>
        /// 获取应用管理员列表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/100073">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>管理员及其管理权限列表。</returns>
        public static GetAppAdminListResult GetAppAdminList(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => PostCurrent<GetAppAdminListResult>(accessTokenOrAppKey, GetAppAdminListPath,
                new EmptyAppRequest(), timeOut);

        /// <summary>
        /// 异步获取应用管理员列表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/100073">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>管理员及其管理权限列表。</returns>
        public static Task<GetAppAdminListResult> GetAppAdminListAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => PostCurrentAsync<GetAppAdminListResult>(accessTokenOrAppKey, GetAppAdminListPath,
                new EmptyAppRequest(), timeOut);

        private sealed class EmptyAppRequest
        {
        }

        private static T PostCurrent<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut,
                jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        private static Task<T> PostCurrentAsync<T>(string accessTokenOrAppKey, string path, object request,
            int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut,
                jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);
    }
}
