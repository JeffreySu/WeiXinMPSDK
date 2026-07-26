/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.SmartSheet.Fields.cs
    文件功能描述：企业微信智能表格字段接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能表格字段增删改查接口及必要注释

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string GetSmartSheetFieldsPath = "/cgi-bin/wedoc/smartsheet/get_fields";
        private const string AddSmartSheetFieldsPath = "/cgi-bin/wedoc/smartsheet/add_fields";
        private const string DeleteSmartSheetFieldsPath = "/cgi-bin/wedoc/smartsheet/delete_fields";
        private const string UpdateSmartSheetFieldsPath = "/cgi-bin/wedoc/smartsheet/update_fields";

        /// <summary>
        /// 获取智能表格字段及字段类型配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99914"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、可选字段筛选和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>字段列表及分页信息。</returns>
        public static WeDocSmartSheetGetFieldsResult GetSmartSheetFields(string accessTokenOrAppKey,
            WeDocSmartSheetGetFieldsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetGetFieldsResult>(accessTokenOrAppKey, GetSmartSheetFieldsPath, request, timeOut);

        /// <summary>
        /// 异步获取智能表格字段及字段类型配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99914"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、可选字段筛选和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>字段列表及分页信息。</returns>
        public static Task<WeDocSmartSheetGetFieldsResult> GetSmartSheetFieldsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetGetFieldsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetGetFieldsResult>(accessTokenOrAppKey, GetSmartSheetFieldsPath, request, timeOut);

        /// <summary>
        /// 批量新增智能表格字段。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99904"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待新增字段列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的字段列表。</returns>
        public static WeDocSmartSheetAddFieldsResult AddSmartSheetFields(string accessTokenOrAppKey,
            WeDocSmartSheetAddFieldsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetAddFieldsResult>(accessTokenOrAppKey, AddSmartSheetFieldsPath, request, timeOut);

        /// <summary>
        /// 异步批量新增智能表格字段。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99904"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待新增字段列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的字段列表。</returns>
        public static Task<WeDocSmartSheetAddFieldsResult> AddSmartSheetFieldsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetAddFieldsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetAddFieldsResult>(accessTokenOrAppKey, AddSmartSheetFieldsPath, request, timeOut);

        /// <summary>
        /// 批量删除智能表格字段。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99906"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待删除字段 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteSmartSheetFields(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteFieldsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetFieldsPath, request, timeOut);

        /// <summary>
        /// 异步批量删除智能表格字段。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99906"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待删除字段 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteSmartSheetFieldsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteFieldsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetFieldsPath, request, timeOut);

        /// <summary>
        /// 批量更新智能表格字段标题、类型或字段配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99906"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待更新字段列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult UpdateSmartSheetFields(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateFieldsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartSheetFieldsPath, request, timeOut);

        /// <summary>
        /// 异步批量更新智能表格字段标题、类型或字段配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99906"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待更新字段列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> UpdateSmartSheetFieldsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateFieldsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartSheetFieldsPath, request, timeOut);
    }
}
