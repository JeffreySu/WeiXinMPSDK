#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CloudBaseBatchApi.cs
    文件功能描述：第三方平台批量云开发接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.CloudBaseBatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 第三方平台批量云开发接口。
    /// </summary>
    /// <remarks>
    /// <para>本类接口均使用第三方平台的 <c>component_access_token</c>，用于批量管理授权小程序的云开发资源。</para>
    /// <para><see href="https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/cloudbase-batch/">微信官方文档</see></para>
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class CloudBaseBatchApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 环境管理

        /// <summary>更换授权小程序关联的云开发环境（ChangeTcbEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="env">需要关联的云开发环境 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ChangeTcbEnv(string componentAccessToken, string env,
            int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(componentAccessToken, "/tcb/modifyenv", new { env }, timeOut);

        /// <summary>异步更换授权小程序关联的云开发环境（ChangeTcbEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="env">需要关联的云开发环境 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ChangeTcbEnvAsync(string componentAccessToken, string env,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(componentAccessToken, "/tcb/modifyenv", new { env }, timeOut);

        /// <summary>查询或设置云开发 access_token 调用权限（SetCloudAccessToken）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">权限查询或设置参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前权限开关、接口白名单和配置版本。</returns>
        public static CloudBaseBatchAccessTokenJsonResult SetCloudAccessToken(string componentAccessToken,
            CloudBaseBatchAccessTokenRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchAccessTokenJsonResult>(componentAccessToken, "/tcb/usecloudaccesstoken", request,
                timeOut);

        /// <summary>异步查询或设置云开发 access_token 调用权限（SetCloudAccessToken）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">权限查询或设置参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前权限开关、接口白名单和配置版本。</returns>
        public static Task<CloudBaseBatchAccessTokenJsonResult> SetCloudAccessTokenAsync(
            string componentAccessToken, CloudBaseBatchAccessTokenRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchAccessTokenJsonResult>(componentAccessToken, "/tcb/usecloudaccesstoken", request,
                timeOut);

        /// <summary>查询指定小程序已共享的云开发环境（GetShareCloudbaseEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appIds">需要查询的小程序 AppID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>AppID 与云开发环境的共享关系。</returns>
        public static CloudBaseBatchGetShareEnvJsonResult GetShareCloudbaseEnv(string componentAccessToken,
            IEnumerable<string> appIds, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchGetShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchgetenvid",
                CreateGetShareRequest(appIds), timeOut);

        /// <summary>异步查询指定小程序已共享的云开发环境（GetShareCloudbaseEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appIds">需要查询的小程序 AppID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>AppID 与云开发环境的共享关系。</returns>
        public static Task<CloudBaseBatchGetShareEnvJsonResult> GetShareCloudbaseEnvAsync(
            string componentAccessToken, IEnumerable<string> appIds, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchGetShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchgetenvid",
                CreateGetShareRequest(appIds), timeOut);

        /// <summary>获取第三方平台创建的云开发环境列表（GetTcbEnvList）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云开发环境列表。</returns>
        public static CloudBaseBatchEnvListJsonResult GetTcbEnvList(string componentAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchEnvListJsonResult>(componentAccessToken, "/componenttcb/describeenvs", new { },
                timeOut);

        /// <summary>异步获取第三方平台创建的云开发环境列表（GetTcbEnvList）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云开发环境列表。</returns>
        public static Task<CloudBaseBatchEnvListJsonResult> GetTcbEnvListAsync(string componentAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchEnvListJsonResult>(componentAccessToken, "/componenttcb/describeenvs", new { },
                timeOut);

        /// <summary>创建云开发环境（CreateTcbEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="alias">云开发环境别名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>环境 ID 和创建任务 ID。</returns>
        public static CloudBaseBatchCreateEnvJsonResult CreateTcbEnv(string componentAccessToken, string alias,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchCreateEnvJsonResult>(componentAccessToken, "/componenttcb/createenv",
                CreateEnvRequest(alias), timeOut);

        /// <summary>异步创建云开发环境（CreateTcbEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="alias">云开发环境别名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>环境 ID 和创建任务 ID。</returns>
        public static Task<CloudBaseBatchCreateEnvJsonResult> CreateTcbEnvAsync(string componentAccessToken,
            string alias, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchCreateEnvJsonResult>(componentAccessToken, "/componenttcb/createenv",
                CreateEnvRequest(alias), timeOut);

        /// <summary>将云开发环境共享给指定小程序（ShareCloudbaseEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境和目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>共享结果以及需要管理员确认的链接。</returns>
        public static CloudBaseBatchShareEnvJsonResult ShareCloudbaseEnv(string componentAccessToken,
            IEnumerable<CloudBaseBatchEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchshareenv",
                CreateShareRequest(data), timeOut);

        /// <summary>异步将云开发环境共享给指定小程序（ShareCloudbaseEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境和目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>共享结果以及需要管理员确认的链接。</returns>
        public static Task<CloudBaseBatchShareEnvJsonResult> ShareCloudbaseEnvAsync(string componentAccessToken,
            IEnumerable<CloudBaseBatchEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchshareenv",
                CreateShareRequest(data), timeOut);

        /// <summary>解除云开发环境与指定小程序的共享关系（UnshareCloudbaseEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境和目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>解除共享结果。</returns>
        public static CloudBaseBatchShareEnvJsonResult UnshareCloudbaseEnv(string componentAccessToken,
            IEnumerable<CloudBaseBatchEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchunshareenv",
                CreateShareRequest(data), timeOut);

        /// <summary>异步解除云开发环境与指定小程序的共享关系（UnshareCloudbaseEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境和目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>解除共享结果。</returns>
        public static Task<CloudBaseBatchShareEnvJsonResult> UnshareCloudbaseEnvAsync(string componentAccessToken,
            IEnumerable<CloudBaseBatchEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchunshareenv",
                CreateShareRequest(data), timeOut);

        #endregion

        #region 云函数管理

        /// <summary>批量创建云函数（BatchUploadCloudFunction）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">云函数名称、环境列表和 ZIP 代码包。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建失败的环境列表。</returns>
        public static CloudBaseBatchFunctionFailJsonResult BatchUploadCloudFunction(string componentAccessToken,
            CloudBaseBatchUploadFunctionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchFunctionFailJsonResult>(componentAccessToken, "/componenttcb/batchuploadscf", request,
                timeOut);

        /// <summary>异步批量创建云函数（BatchUploadCloudFunction）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">云函数名称、环境列表和 ZIP 代码包。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建失败的环境列表。</returns>
        public static Task<CloudBaseBatchFunctionFailJsonResult> BatchUploadCloudFunctionAsync(
            string componentAccessToken, CloudBaseBatchUploadFunctionRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchFunctionFailJsonResult>(componentAccessToken, "/componenttcb/batchuploadscf",
                request, timeOut);

        /// <summary>更新云函数配置（UploadCloudFunctionConfig）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">云函数运行、网络和环境变量配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult UploadCloudFunctionConfig(string componentAccessToken,
            CloudBaseBatchFunctionConfigRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(componentAccessToken, "/componenttcb/updatescfconfig", request, timeOut);

        /// <summary>异步更新云函数配置（UploadCloudFunctionConfig）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">云函数运行、网络和环境变量配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> UploadCloudFunctionConfigAsync(string componentAccessToken,
            CloudBaseBatchFunctionConfigRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(componentAccessToken, "/componenttcb/updatescfconfig", request, timeOut);

        /// <summary>删除云函数（DeleteCloudFunction）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult DeleteCloudFunction(string componentAccessToken,
            CloudBaseBatchFunctionIdentityRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(componentAccessToken, "/componenttcb/deletescf", request, timeOut);

        /// <summary>异步删除云函数（DeleteCloudFunction）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> DeleteCloudFunctionAsync(string componentAccessToken,
            CloudBaseBatchFunctionIdentityRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(componentAccessToken, "/componenttcb/deletescf", request, timeOut);

        /// <summary>获取云函数列表（GetCloudFunctionList）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 及可选分页、搜索参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数列表。</returns>
        public static CloudBaseBatchFunctionListJsonResult GetCloudFunctionList(string componentAccessToken,
            CloudBaseBatchFunctionListRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchFunctionListJsonResult>(componentAccessToken, "/componenttcb/getscflist", request,
                timeOut);

        /// <summary>异步获取云函数列表（GetCloudFunctionList）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 及可选分页、搜索参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数列表。</returns>
        public static Task<CloudBaseBatchFunctionListJsonResult> GetCloudFunctionListAsync(
            string componentAccessToken, CloudBaseBatchFunctionListRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchFunctionListJsonResult>(componentAccessToken, "/componenttcb/getscflist",
                request, timeOut);

        /// <summary>获取云函数触发器（GetTriggers）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>触发器列表。</returns>
        public static CloudBaseBatchTriggerListJsonResult GetTriggers(string componentAccessToken,
            CloudBaseBatchTriggerQueryRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchTriggerListJsonResult>(componentAccessToken, "/componenttcb/gettriggers", request,
                timeOut);

        /// <summary>异步获取云函数触发器（GetTriggers）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>触发器列表。</returns>
        public static Task<CloudBaseBatchTriggerListJsonResult> GetTriggersAsync(string componentAccessToken,
            CloudBaseBatchTriggerQueryRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchTriggerListJsonResult>(componentAccessToken, "/componenttcb/gettriggers",
                request, timeOut);

        /// <summary>批量更新云函数触发器（UpdateTriggers）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">云函数名称、环境列表和触发器配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新失败的环境列表。</returns>
        public static CloudBaseBatchFunctionErrorJsonResult UpdateTriggers(string componentAccessToken,
            CloudBaseBatchUpdateTriggersRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchFunctionErrorJsonResult>(componentAccessToken, "/componenttcb/batchupdatetriggers",
                request, timeOut);

        /// <summary>异步批量更新云函数触发器（UpdateTriggers）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">云函数名称、环境列表和触发器配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新失败的环境列表。</returns>
        public static Task<CloudBaseBatchFunctionErrorJsonResult> UpdateTriggersAsync(
            string componentAccessToken, CloudBaseBatchUpdateTriggersRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchFunctionErrorJsonResult>(componentAccessToken,
                "/componenttcb/batchupdatetriggers", request, timeOut);

        /// <summary>调用云函数（InvokeCloudFunction）。</summary>
        /// <remarks>按照官方 HTTPS 示例将 <paramref name="env"/> 和 <paramref name="name"/> 放入查询字符串，请求体原样传给云函数。</remarks>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="name">云函数名称。</param>
        /// <param name="postBody">云函数自定义请求体。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数返回数据。</returns>
        public static CloudBaseBatchInvokeFunctionJsonResult InvokeCloudFunction(string componentAccessToken,
            string env, string name, object postBody, int timeOut = Config.TIME_OUT) =>
            SendInvoke<CloudBaseBatchInvokeFunctionJsonResult>(componentAccessToken, "/tcb/invokecloudfunction",
                env, name, postBody, timeOut);

        /// <summary>异步调用云函数（InvokeCloudFunction）。</summary>
        /// <remarks>按照官方 HTTPS 示例将 <paramref name="env"/> 和 <paramref name="name"/> 放入查询字符串，请求体原样传给云函数。</remarks>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="name">云函数名称。</param>
        /// <param name="postBody">云函数自定义请求体。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数返回数据。</returns>
        public static Task<CloudBaseBatchInvokeFunctionJsonResult> InvokeCloudFunctionAsync(
            string componentAccessToken, string env, string name, object postBody,
            int timeOut = Config.TIME_OUT) =>
            SendInvokeAsync<CloudBaseBatchInvokeFunctionJsonResult>(componentAccessToken,
                "/tcb/invokecloudfunction", env, name, postBody, timeOut);

        /// <summary>批量更新云函数代码（UploadCloudFunctionCode）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">云函数名称、环境列表和 ZIP 代码包。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新失败的环境列表。</returns>
        public static CloudBaseBatchFunctionFailJsonResult UploadCloudFunctionCode(string componentAccessToken,
            CloudBaseBatchUploadFunctionCodeRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchFunctionFailJsonResult>(componentAccessToken, "/componenttcb/batchuploadscfcode",
                request, timeOut);

        /// <summary>异步批量更新云函数代码（UploadCloudFunctionCode）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">云函数名称、环境列表和 ZIP 代码包。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新失败的环境列表。</returns>
        public static Task<CloudBaseBatchFunctionFailJsonResult> UploadCloudFunctionCodeAsync(
            string componentAccessToken, CloudBaseBatchUploadFunctionCodeRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchFunctionFailJsonResult>(componentAccessToken,
                "/componenttcb/batchuploadscfcode", request, timeOut);

        #endregion

        #region 数据库管理

        /// <summary>导入云开发数据库（DbImport）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">集合、文件及冲突处理参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移任务 ID。</returns>
        public static CloudBaseBatchJobJsonResult DbImport(string componentAccessToken,
            CloudBaseBatchDbImportRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchJobJsonResult>(componentAccessToken, "/componenttcb/dbimport", request, timeOut);

        /// <summary>异步导入云开发数据库（DbImport）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">集合、文件及冲突处理参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移任务 ID。</returns>
        public static Task<CloudBaseBatchJobJsonResult> DbImportAsync(string componentAccessToken,
            CloudBaseBatchDbImportRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchJobJsonResult>(componentAccessToken, "/componenttcb/dbimport", request,
                timeOut);

        /// <summary>导出云开发数据库（DbExport）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">导出路径、格式和查询条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移任务 ID。</returns>
        public static CloudBaseBatchJobJsonResult DbExport(string componentAccessToken,
            CloudBaseBatchDbExportRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchJobJsonResult>(componentAccessToken, "/componenttcb/dbexport", request, timeOut);

        /// <summary>异步导出云开发数据库（DbExport）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">导出路径、格式和查询条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移任务 ID。</returns>
        public static Task<CloudBaseBatchJobJsonResult> DbExportAsync(string componentAccessToken,
            CloudBaseBatchDbExportRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchJobJsonResult>(componentAccessToken, "/componenttcb/dbexport", request,
                timeOut);

        /// <summary>查询数据库迁移状态（GetMigrationState）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和迁移任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移进度和结果文件地址。</returns>
        public static CloudBaseBatchMigrationStateJsonResult GetMigrationState(string componentAccessToken,
            CloudBaseBatchMigrationStateRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchMigrationStateJsonResult>(componentAccessToken, "/componenttcb/dbmigrationstate",
                request, timeOut);

        /// <summary>异步查询数据库迁移状态（GetMigrationState）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和迁移任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移进度和结果文件地址。</returns>
        public static Task<CloudBaseBatchMigrationStateJsonResult> GetMigrationStateAsync(
            string componentAccessToken, CloudBaseBatchMigrationStateRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchMigrationStateJsonResult>(componentAccessToken,
                "/componenttcb/dbmigrationstate", request, timeOut);

        /// <summary>执行数据库聚合查询（DbAggregate）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和聚合查询语句。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>JSON 字符串形式的查询结果。</returns>
        public static CloudBaseBatchQueryJsonResult DbAggregate(string componentAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchQueryJsonResult>(componentAccessToken, "/componenttcb/dbaggregate", request,
                timeOut);

        /// <summary>异步执行数据库聚合查询（DbAggregate）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和聚合查询语句。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>JSON 字符串形式的查询结果。</returns>
        public static Task<CloudBaseBatchQueryJsonResult> DbAggregateAsync(string componentAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchQueryJsonResult>(componentAccessToken, "/componenttcb/dbaggregate", request,
                timeOut);

        /// <summary>获取数据库集合权限（GetPermission）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和集合名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>权限类型及自定义规则。</returns>
        public static CloudBaseBatchPermissionJsonResult GetPermission(string componentAccessToken,
            CloudBaseBatchCollectionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchPermissionJsonResult>(componentAccessToken, "/componenttcb/dbgetacl", request,
                timeOut);

        /// <summary>异步获取数据库集合权限（GetPermission）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和集合名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>权限类型及自定义规则。</returns>
        public static Task<CloudBaseBatchPermissionJsonResult> GetPermissionAsync(string componentAccessToken,
            CloudBaseBatchCollectionRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchPermissionJsonResult>(componentAccessToken, "/componenttcb/dbgetacl", request,
                timeOut);

        /// <summary>设置数据库集合权限（SetPermission）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">集合、权限类型及自定义规则。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SetPermission(string componentAccessToken,
            CloudBaseBatchSetPermissionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(componentAccessToken, "/componenttcb/dbmodifyacl", request, timeOut);

        /// <summary>异步设置数据库集合权限（SetPermission）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">集合、权限类型及自定义规则。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> SetPermissionAsync(string componentAccessToken,
            CloudBaseBatchSetPermissionRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(componentAccessToken, "/componenttcb/dbmodifyacl", request, timeOut);

        /// <summary>新增、删除、更新或查询数据库记录（DbRecordManage）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">操作类型、环境和数据库命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>记录操作结果。</returns>
        public static CloudBaseBatchRecordJsonResult DbRecordManage(string componentAccessToken,
            CloudBaseBatchRecordRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchRecordJsonResult>(componentAccessToken, "/componenttcb/dbrecord", request,
                timeOut);

        /// <summary>异步新增、删除、更新或查询数据库记录（DbRecordManage）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">操作类型、环境和数据库命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>记录操作结果。</returns>
        public static Task<CloudBaseBatchRecordJsonResult> DbRecordManageAsync(string componentAccessToken,
            CloudBaseBatchRecordRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchRecordJsonResult>(componentAccessToken, "/componenttcb/dbrecord", request,
                timeOut);

        /// <summary>创建或删除数据库索引（DbIndexManage）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">操作类型、集合和索引定义。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult DbIndexManage(string componentAccessToken,
            CloudBaseBatchIndexRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(componentAccessToken, "/componenttcb/dbindex", request, timeOut);

        /// <summary>异步创建或删除数据库索引（DbIndexManage）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">操作类型、集合和索引定义。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> DbIndexManageAsync(string componentAccessToken,
            CloudBaseBatchIndexRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(componentAccessToken, "/componenttcb/dbindex", request, timeOut);

        #endregion

        #region 文件管理

        /// <summary>获取云存储上传链接（GetUploadFileLink）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和上传路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传 URL、临时凭证和文件 ID。</returns>
        public static CloudBaseBatchUploadFileJsonResult GetUploadFileLink(string componentAccessToken,
            CloudBaseBatchFilePathRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchUploadFileJsonResult>(componentAccessToken, "/componenttcb/uploadfile", request,
                timeOut);

        /// <summary>异步获取云存储上传链接（GetUploadFileLink）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和上传路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传 URL、临时凭证和文件 ID。</returns>
        public static Task<CloudBaseBatchUploadFileJsonResult> GetUploadFileLinkAsync(
            string componentAccessToken, CloudBaseBatchFilePathRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchUploadFileJsonResult>(componentAccessToken, "/componenttcb/uploadfile",
                request, timeOut);

        /// <summary>批量删除云存储文件（DeleteTcbFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和文件 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>每个文件的删除结果。</returns>
        public static CloudBaseBatchDeleteFileJsonResult DeleteTcbFile(string componentAccessToken,
            CloudBaseBatchDeleteFileRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchDeleteFileJsonResult>(componentAccessToken, "/componenttcb/batchdeletefile",
                request, timeOut);

        /// <summary>异步批量删除云存储文件（DeleteTcbFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和文件 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>每个文件的删除结果。</returns>
        public static Task<CloudBaseBatchDeleteFileJsonResult> DeleteTcbFileAsync(string componentAccessToken,
            CloudBaseBatchDeleteFileRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchDeleteFileJsonResult>(componentAccessToken, "/componenttcb/batchdeletefile",
                request, timeOut);

        /// <summary>获取云存储文件列表（GetTcbFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和可选目录遍历参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件列表及是否被截断。</returns>
        public static CloudBaseBatchFileListJsonResult GetTcbFile(string componentAccessToken,
            CloudBaseBatchFileListRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchFileListJsonResult>(componentAccessToken, "/componenttcb/getbucket", request,
                timeOut);

        /// <summary>异步获取云存储文件列表（GetTcbFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和可选目录遍历参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件列表及是否被截断。</returns>
        public static Task<CloudBaseBatchFileListJsonResult> GetTcbFileAsync(string componentAccessToken,
            CloudBaseBatchFileListRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchFileListJsonResult>(componentAccessToken, "/componenttcb/getbucket", request,
                timeOut);

        /// <summary>批量获取云存储文件下载链接（GetDownloadFileLink）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID、文件 ID 和链接有效期。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>每个文件的下载链接和处理状态。</returns>
        public static CloudBaseBatchDownloadFileJsonResult GetDownloadFileLink(string componentAccessToken,
            CloudBaseBatchDownloadFileRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchDownloadFileJsonResult>(componentAccessToken, "/componenttcb/batchdownloadfile",
                request, timeOut);

        /// <summary>异步批量获取云存储文件下载链接（GetDownloadFileLink）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID、文件 ID 和链接有效期。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>每个文件的下载链接和处理状态。</returns>
        public static Task<CloudBaseBatchDownloadFileJsonResult> GetDownloadFileLinkAsync(
            string componentAccessToken, CloudBaseBatchDownloadFileRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchDownloadFileJsonResult>(componentAccessToken,
                "/componenttcb/batchdownloadfile", request, timeOut);

        #endregion

        #region 静态网站管理

        /// <summary>查看静态网站状态（GetStaticStore）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>静态网站域名、存储桶、区域和状态。</returns>
        public static CloudBaseBatchStaticStoreJsonResult GetStaticStore(string componentAccessToken, string env,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchStaticStoreJsonResult>(componentAccessToken, "/componenttcb/describestaticstore",
                new { env }, timeOut);

        /// <summary>异步查看静态网站状态（GetStaticStore）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>静态网站域名、存储桶、区域和状态。</returns>
        public static Task<CloudBaseBatchStaticStoreJsonResult> GetStaticStoreAsync(string componentAccessToken,
            string env, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchStaticStoreJsonResult>(componentAccessToken,
                "/componenttcb/describestaticstore", new { env }, timeOut);

        /// <summary>开通静态网站（CreateStaticStore）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult CreateStaticStore(string componentAccessToken, string env,
            int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(componentAccessToken, "/componenttcb/createstaticstore", new { env }, timeOut);

        /// <summary>异步开通静态网站（CreateStaticStore）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> CreateStaticStoreAsync(string componentAccessToken, string env,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(componentAccessToken, "/componenttcb/createstaticstore", new { env }, timeOut);

        /// <summary>获取静态网站文件上传链接（GetUploadStaticStoreFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和文件路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>签名上传链接和临时安全令牌。</returns>
        public static CloudBaseBatchStaticUploadJsonResult GetUploadStaticStoreFile(string componentAccessToken,
            CloudBaseBatchStaticFileRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchStaticUploadJsonResult>(componentAccessToken, "/componenttcb/staticuploadfile",
                request, timeOut);

        /// <summary>异步获取静态网站文件上传链接（GetUploadStaticStoreFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和文件路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>签名上传链接和临时安全令牌。</returns>
        public static Task<CloudBaseBatchStaticUploadJsonResult> GetUploadStaticStoreFileAsync(
            string componentAccessToken, CloudBaseBatchStaticFileRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchStaticUploadJsonResult>(componentAccessToken,
                "/componenttcb/staticuploadfile", request, timeOut);

        /// <summary>获取静态网站文件列表（GetStaticStoreFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和可选目录遍历参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>静态网站文件列表及是否被截断。</returns>
        public static CloudBaseBatchFileListJsonResult GetStaticStoreFile(string componentAccessToken,
            CloudBaseBatchFileListRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchFileListJsonResult>(componentAccessToken, "/componenttcb/staticfilelist", request,
                timeOut);

        /// <summary>异步获取静态网站文件列表（GetStaticStoreFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和可选目录遍历参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>静态网站文件列表及是否被截断。</returns>
        public static Task<CloudBaseBatchFileListJsonResult> GetStaticStoreFileAsync(
            string componentAccessToken, CloudBaseBatchFileListRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchFileListJsonResult>(componentAccessToken, "/componenttcb/staticfilelist",
                request, timeOut);

        #endregion

        /// <summary>创建查询共享环境的请求体，并固定来源类型为云开发。</summary>
        private static object CreateGetShareRequest(IEnumerable<string> appIds)
        {
            return new { appids = appIds, source_type = 0 };
        }

        /// <summary>创建共享或解除共享环境的请求体，并固定来源类型为云开发。</summary>
        private static object CreateShareRequest(IEnumerable<CloudBaseBatchEnvShareItem> data)
        {
            return new { data, source_type = 0 };
        }

        /// <summary>创建云开发环境请求体，并固定官方要求的环境类型。</summary>
        private static object CreateEnvRequest(string alias)
        {
            return new { alias, EnvType = "run" };
        }

        /// <summary>构造批量云开发接口地址。</summary>
        private static string BuildUrl(string componentAccessToken, string path)
        {
            return $"{Config.ApiMpHost}{path}?access_token={componentAccessToken.AsUrlData()}";
        }

        /// <summary>构造调用云函数接口地址。</summary>
        private static string BuildInvokeUrl(string componentAccessToken, string path, string env, string name)
        {
            return $"{BuildUrl(componentAccessToken, path)}&env={env.AsUrlData()}&name={name.AsUrlData()}";
        }

        /// <summary>同步发送批量云开发请求。</summary>
        private static T Send<T>(string componentAccessToken, string path, object request, int timeOut)
        {
            return CommonJsonSend.Send<T>(null, BuildUrl(componentAccessToken, path), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>异步发送批量云开发请求。</summary>
        private static Task<T> SendAsync<T>(string componentAccessToken, string path, object request, int timeOut)
        {
            return CommonJsonSend.SendAsync<T>(null, BuildUrl(componentAccessToken, path), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>同步调用云函数。</summary>
        private static T SendInvoke<T>(string componentAccessToken, string path, string env, string name,
            object request, int timeOut)
        {
            return CommonJsonSend.Send<T>(null, BuildInvokeUrl(componentAccessToken, path, env, name),
                request ?? new { }, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>异步调用云函数。</summary>
        private static Task<T> SendInvokeAsync<T>(string componentAccessToken, string path, string env, string name,
            object request, int timeOut)
        {
            return CommonJsonSend.SendAsync<T>(null, BuildInvokeUrl(componentAccessToken, path, env, name),
                request ?? new { }, CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);
        }
    }
}
