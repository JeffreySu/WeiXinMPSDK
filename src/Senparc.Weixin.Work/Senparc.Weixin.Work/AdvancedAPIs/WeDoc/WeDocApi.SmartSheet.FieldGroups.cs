/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.SmartSheet.FieldGroups.cs
    文件功能描述：企业微信智能表格字段分组接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能表格字段分组增删改查接口及必要注释

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string GetSmartSheetFieldGroupsPath = "/cgi-bin/wedoc/smartsheet/get_field_groups";
        private const string AddSmartSheetFieldGroupPath = "/cgi-bin/wedoc/smartsheet/add_field_group";
        private const string UpdateSmartSheetFieldGroupPath = "/cgi-bin/wedoc/smartsheet/update_field_group";
        private const string DeleteSmartSheetFieldGroupsPath = "/cgi-bin/wedoc/smartsheet/delete_field_groups";

        /// <summary>
        /// 分页获取智能表格字段分组。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101103"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>字段分组列表及分页信息。</returns>
        public static WeDocSmartSheetGetFieldGroupsResult GetSmartSheetFieldGroups(string accessTokenOrAppKey,
            WeDocSmartSheetGetFieldGroupsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetGetFieldGroupsResult>(accessTokenOrAppKey, GetSmartSheetFieldGroupsPath, request, timeOut);

        /// <summary>
        /// 异步分页获取智能表格字段分组。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101103"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>字段分组列表及分页信息。</returns>
        public static Task<WeDocSmartSheetGetFieldGroupsResult> GetSmartSheetFieldGroupsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetGetFieldGroupsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetGetFieldGroupsResult>(accessTokenOrAppKey, GetSmartSheetFieldGroupsPath, request, timeOut);

        /// <summary>
        /// 新增智能表格字段分组。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101100"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、分组名称及字段列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的字段分组。</returns>
        public static WeDocSmartSheetAddFieldGroupResult AddSmartSheetFieldGroup(string accessTokenOrAppKey,
            WeDocSmartSheetAddFieldGroupRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetAddFieldGroupResult>(accessTokenOrAppKey, AddSmartSheetFieldGroupPath, request, timeOut);

        /// <summary>
        /// 异步新增智能表格字段分组。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101100"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、分组名称及字段列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的字段分组。</returns>
        public static Task<WeDocSmartSheetAddFieldGroupResult> AddSmartSheetFieldGroupAsync(string accessTokenOrAppKey,
            WeDocSmartSheetAddFieldGroupRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetAddFieldGroupResult>(accessTokenOrAppKey, AddSmartSheetFieldGroupPath, request, timeOut);

        /// <summary>
        /// 更新智能表格字段分组名称或字段列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101101"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、字段分组 ID 及待更新内容。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新后的字段分组。</returns>
        public static WeDocSmartSheetUpdateFieldGroupResult UpdateSmartSheetFieldGroup(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateFieldGroupRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetUpdateFieldGroupResult>(accessTokenOrAppKey, UpdateSmartSheetFieldGroupPath, request, timeOut);

        /// <summary>
        /// 异步更新智能表格字段分组名称或字段列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101101"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、字段分组 ID 及待更新内容。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新后的字段分组。</returns>
        public static Task<WeDocSmartSheetUpdateFieldGroupResult> UpdateSmartSheetFieldGroupAsync(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateFieldGroupRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetUpdateFieldGroupResult>(accessTokenOrAppKey, UpdateSmartSheetFieldGroupPath, request, timeOut);

        /// <summary>
        /// 批量删除智能表格字段分组。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101102"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待删除的字段分组 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteSmartSheetFieldGroups(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteFieldGroupsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetFieldGroupsPath, request, timeOut);

        /// <summary>
        /// 异步批量删除智能表格字段分组。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101102"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待删除的字段分组 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteSmartSheetFieldGroupsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteFieldGroupsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetFieldGroupsPath, request, timeOut);
    }
}
