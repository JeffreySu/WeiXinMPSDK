/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDriveApi.cs
    文件功能描述：企业微信微盘空间接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐微盘空间管理、权限与安全设置接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive
{
    /// <summary>
    /// 企业微信微盘接口。
    /// <para>调用前需要为自建应用配置微盘接口权限，并使用该应用 Secret 获取的 access_token。</para>
    /// </summary>
    public static partial class WeDriveApi
    {
        private const string SpaceCreatePath = "/cgi-bin/wedrive/space_create";
        private const string SpaceRenamePath = "/cgi-bin/wedrive/space_rename";
        private const string SpaceDismissPath = "/cgi-bin/wedrive/space_dismiss";
        private const string SpaceInfoPath = "/cgi-bin/wedrive/space_info";
        private const string NewSpaceInfoPath = "/cgi-bin/wedrive/new_space_info";
        private const string SpaceAclAddPath = "/cgi-bin/wedrive/space_acl_add";
        private const string SpaceAclDeletePath = "/cgi-bin/wedrive/space_acl_del";
        private const string SpaceSettingPath = "/cgi-bin/wedrive/space_setting";
        private const string SpaceSharePath = "/cgi-bin/wedrive/space_share";

        /// <summary>
        /// 在微盘中新建空间，创建者为当前应用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间标题、成员权限及空间类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新建空间的 ID。</returns>
        public static WeDriveCreateSpaceResult CreateSpace(string accessTokenOrAppKey,
            WeDriveCreateSpaceRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveCreateSpaceResult>(accessTokenOrAppKey, SpaceCreatePath, request, timeOut);

        /// <summary>
        /// 异步在微盘中新建空间，创建者为当前应用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间标题、成员权限及空间类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新建空间的 ID。</returns>
        public static Task<WeDriveCreateSpaceResult> CreateSpaceAsync(string accessTokenOrAppKey,
            WeDriveCreateSpaceRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveCreateSpaceResult>(accessTokenOrAppKey, SpaceCreatePath, request, timeOut);

        /// <summary>
        /// 重命名由当前应用管理的微盘空间。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID 和新标题。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult RenameSpace(string accessTokenOrAppKey, WeDriveRenameSpaceRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SpaceRenamePath, request, timeOut);

        /// <summary>
        /// 异步重命名由当前应用管理的微盘空间。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID 和新标题。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> RenameSpaceAsync(string accessTokenOrAppKey,
            WeDriveRenameSpaceRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SpaceRenamePath, request, timeOut);

        /// <summary>
        /// 解散由当前应用管理的微盘空间。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult DismissSpace(string accessTokenOrAppKey, WeDriveSpaceIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SpaceDismissPath, request, timeOut);

        /// <summary>
        /// 异步解散由当前应用管理的微盘空间。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> DismissSpaceAsync(string accessTokenOrAppKey,
            WeDriveSpaceIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SpaceDismissPath, request, timeOut);

        /// <summary>
        /// 获取微盘空间成员、权限和安全设置等新版完整信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>空间成员、权限和安全设置。</returns>
        public static WeDriveSpaceInfoResult GetSpaceInfo(string accessTokenOrAppKey,
            WeDriveSpaceIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveSpaceInfoResult>(accessTokenOrAppKey, NewSpaceInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取微盘空间成员、权限和安全设置等新版完整信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>空间成员、权限和安全设置。</returns>
        public static Task<WeDriveSpaceInfoResult> GetSpaceInfoAsync(string accessTokenOrAppKey,
            WeDriveSpaceIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveSpaceInfoResult>(accessTokenOrAppKey, NewSpaceInfoPath, request, timeOut);

        /// <summary>
        /// 使用旧版空间信息接口获取微盘空间成员与权限。
        /// <para>新应用优先使用 <see cref="GetSpaceInfo"/> 获取包含安全设置的完整结果。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>旧版空间成员与权限信息。</returns>
        public static WeDriveSpaceInfoResult GetLegacySpaceInfo(string accessTokenOrAppKey,
            WeDriveSpaceIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveSpaceInfoResult>(accessTokenOrAppKey, SpaceInfoPath, request, timeOut);

        /// <summary>
        /// 异步使用旧版空间信息接口获取微盘空间成员与权限。
        /// <para>新应用优先使用 <see cref="GetSpaceInfoAsync"/> 获取包含安全设置的完整结果。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>旧版空间成员与权限信息。</returns>
        public static Task<WeDriveSpaceInfoResult> GetLegacySpaceInfoAsync(string accessTokenOrAppKey,
            WeDriveSpaceIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveSpaceInfoResult>(accessTokenOrAppKey, SpaceInfoPath, request, timeOut);

        /// <summary>
        /// 为微盘空间批量添加成员或部门。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID 及待添加的授权对象。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult AddSpaceMembers(string accessTokenOrAppKey, WeDriveSpaceAclRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SpaceAclAddPath, request, timeOut);

        /// <summary>
        /// 异步为微盘空间批量添加成员或部门。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID 及待添加的授权对象。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> AddSpaceMembersAsync(string accessTokenOrAppKey,
            WeDriveSpaceAclRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SpaceAclAddPath, request, timeOut);

        /// <summary>
        /// 从微盘空间批量移除成员或部门。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID 及待移除的授权对象。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult RemoveSpaceMembers(string accessTokenOrAppKey,
            WeDriveSpaceAclRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SpaceAclDeletePath, request, timeOut);

        /// <summary>
        /// 异步从微盘空间批量移除成员或部门。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID 及待移除的授权对象。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> RemoveSpaceMembersAsync(string accessTokenOrAppKey,
            WeDriveSpaceAclRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SpaceAclDeletePath, request, timeOut);

        /// <summary>
        /// 修改微盘空间的水印、保密、邀请链接及外部分享设置。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID 和需要修改的安全设置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult UpdateSpaceSetting(string accessTokenOrAppKey,
            WeDriveSpaceSettingRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SpaceSettingPath, request, timeOut);

        /// <summary>
        /// 异步修改微盘空间的水印、保密、邀请链接及外部分享设置。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID 和需要修改的安全设置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> UpdateSpaceSettingAsync(string accessTokenOrAppKey,
            WeDriveSpaceSettingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SpaceSettingPath, request, timeOut);

        /// <summary>
        /// 获取微盘空间邀请链接。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>空间邀请链接。</returns>
        public static WeDriveSpaceShareResult GetSpaceShareLink(string accessTokenOrAppKey,
            WeDriveSpaceIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveSpaceShareResult>(accessTokenOrAppKey, SpaceSharePath, request, timeOut);

        /// <summary>
        /// 异步获取微盘空间邀请链接。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>空间邀请链接。</returns>
        public static Task<WeDriveSpaceShareResult> GetSpaceShareLinkAsync(string accessTokenOrAppKey,
            WeDriveSpaceIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveSpaceShareResult>(accessTokenOrAppKey, SpaceSharePath, request, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);
    }
}
