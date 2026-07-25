/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.SmartSheet.cs
    文件功能描述：企业微信智能表格权限和工作表接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 按官方协议拆分智能表格权限和工作表接口模型

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string GetSmartSheetAuthPath = "/cgi-bin/wedoc/smartsheet/get_sheet_auth";
        private const string ModifySmartSheetAuthPath = "/cgi-bin/wedoc/smartsheet/mod_sheet_auth";
        private const string GetSmartSheetPath = "/cgi-bin/wedoc/smartsheet/get_sheet";
        private const string AddSmartSheetPath = "/cgi-bin/wedoc/smartsheet/add_sheet";
        private const string DeleteSmartSheetPath = "/cgi-bin/wedoc/smartsheet/delete_sheet";
        private const string UpdateSmartSheetPath = "/cgi-bin/wedoc/smartsheet/update_sheet";

        /// <summary>
        /// 获取智能表格字段、记录等内容权限。
        /// <para>官方接口：<c>POST /cgi-bin/wedoc/smartsheet/get_sheet_auth</c>。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档和可选工作表标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>智能表格内容权限。</returns>
        public static WeDocSmartSheetAuthResult GetSmartSheetAuth(string accessTokenOrAppKey,
            WeDocSmartSheetAuthRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetAuthResult>(accessTokenOrAppKey, GetSmartSheetAuthPath, request, timeOut);

        /// <summary>
        /// 异步获取智能表格字段、记录等内容权限。
        /// <para>官方接口：<c>POST /cgi-bin/wedoc/smartsheet/get_sheet_auth</c>。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档和可选工作表标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>智能表格内容权限。</returns>
        public static Task<WeDocSmartSheetAuthResult> GetSmartSheetAuthAsync(string accessTokenOrAppKey,
            WeDocSmartSheetAuthRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetAuthResult>(accessTokenOrAppKey, GetSmartSheetAuthPath, request, timeOut);

        /// <summary>
        /// 修改智能表格字段、记录等内容权限。
        /// <para>官方接口：<c>POST /cgi-bin/wedoc/smartsheet/mod_sheet_auth</c>。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档、工作表及权限载荷。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult ModifySmartSheetAuth(string accessTokenOrAppKey,
            WeDocSmartSheetModifyAuthRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ModifySmartSheetAuthPath, request, timeOut);

        /// <summary>
        /// 异步修改智能表格字段、记录等内容权限。
        /// <para>官方接口：<c>POST /cgi-bin/wedoc/smartsheet/mod_sheet_auth</c>。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档、工作表及权限载荷。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> ModifySmartSheetAuthAsync(string accessTokenOrAppKey,
            WeDocSmartSheetModifyAuthRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ModifySmartSheetAuthPath, request, timeOut);

        /// <summary>
        /// 获取智能表格工作表列表或指定工作表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99911"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档和可选工作表标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工作表列表。</returns>
        public static WeDocSmartSheetGetSheetsResult GetSmartSheets(string accessTokenOrAppKey,
            WeDocSmartSheetGetSheetsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetGetSheetsResult>(accessTokenOrAppKey, GetSmartSheetPath, request, timeOut);

        /// <summary>
        /// 异步获取智能表格工作表列表或指定工作表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99911"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档和可选工作表标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工作表列表。</returns>
        public static Task<WeDocSmartSheetGetSheetsResult> GetSmartSheetsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetGetSheetsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetGetSheetsResult>(accessTokenOrAppKey, GetSmartSheetPath, request, timeOut);

        /// <summary>
        /// 在智能表格文档中新增工作表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99896"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和新工作表属性。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的工作表属性。</returns>
        public static WeDocSmartSheetAddSheetResult AddSmartSheet(string accessTokenOrAppKey,
            WeDocSmartSheetAddSheetRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetAddSheetResult>(accessTokenOrAppKey, AddSmartSheetPath, request, timeOut);

        /// <summary>
        /// 异步在智能表格文档中新增工作表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99896"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和新工作表属性。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的工作表属性。</returns>
        public static Task<WeDocSmartSheetAddSheetResult> AddSmartSheetAsync(string accessTokenOrAppKey,
            WeDocSmartSheetAddSheetRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetAddSheetResult>(accessTokenOrAppKey, AddSmartSheetPath, request, timeOut);

        /// <summary>
        /// 删除智能表格工作表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99898"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档和工作表标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteSmartSheet(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteSheetRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetPath, request, timeOut);

        /// <summary>
        /// 异步删除智能表格工作表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99898"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档和工作表标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteSmartSheetAsync(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteSheetRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetPath, request, timeOut);

        /// <summary>
        /// 更新智能表格工作表标题。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99898"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识及待更新工作表属性。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult UpdateSmartSheet(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateSheetRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartSheetPath, request, timeOut);

        /// <summary>
        /// 异步更新智能表格工作表标题。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99898"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识及待更新工作表属性。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> UpdateSmartSheetAsync(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateSheetRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartSheetPath, request, timeOut);
    }
}
