#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CloudBaseApi.cs
    文件功能描述：第三方平台普通代云开发接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.CloudBase;
using Senparc.Weixin.Open.WxaAPIs.CloudBaseBatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 第三方平台普通代云开发接口。
    /// </summary>
    /// <remarks>
    /// <para>普通接口使用授权小程序的 <c>authorizer_access_token</c>；官方明确标注为批量能力的接口使用 <c>component_access_token</c>。</para>
    /// <para><see href="https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/cloudbase-common/admin-management/setCloudAccessToken.html">微信官方文档</see></para>
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class CloudBaseApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 管理与环境

        /// <summary>查询或设置授权小程序的微信令牌调用权限（SetCloudAccessToken）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">权限查询或设置参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前权限开关、接口白名单和配置版本。</returns>
        public static CloudBaseBatchAccessTokenJsonResult SetCloudAccessToken(string authorizerAccessToken,
            CloudBaseBatchAccessTokenRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchAccessTokenJsonResult>(authorizerAccessToken, "/tcb/usecloudaccesstoken", request,
                timeOut);

        /// <summary>异步查询或设置授权小程序的微信令牌调用权限（SetCloudAccessToken）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">权限查询或设置参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前权限开关、接口白名单和配置版本。</returns>
        public static Task<CloudBaseBatchAccessTokenJsonResult> SetCloudAccessTokenAsync(
            string authorizerAccessToken, CloudBaseBatchAccessTokenRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchAccessTokenJsonResult>(authorizerAccessToken, "/tcb/usecloudaccesstoken",
                request, timeOut);

        /// <summary>为授权小程序开通云开发（CreateCloudUser）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult CreateCloudUser(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(authorizerAccessToken, "/tcb/createclouduser", new { }, timeOut);

        /// <summary>异步为授权小程序开通云开发（CreateCloudUser）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> CreateCloudUserAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(authorizerAccessToken, "/tcb/createclouduser", new { }, timeOut);

        /// <summary>获取腾讯云 API 临时调用凭证（GetCloudToken）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="lifespan">凭证有效期，单位秒，最大 7200。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>腾讯云临时密钥、令牌及过期时间。</returns>
        public static CloudBaseQCloudTokenJsonResult GetCloudToken(string authorizerAccessToken, int lifespan,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseQCloudTokenJsonResult>(authorizerAccessToken, "/tcb/getqcloudtoken",
                new { lifespan }, timeOut);

        /// <summary>异步获取腾讯云 API 临时调用凭证（GetCloudToken）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="lifespan">凭证有效期，单位秒，最大 7200。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>腾讯云临时密钥、令牌及过期时间。</returns>
        public static Task<CloudBaseQCloudTokenJsonResult> GetCloudTokenAsync(string authorizerAccessToken,
            int lifespan, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseQCloudTokenJsonResult>(authorizerAccessToken, "/tcb/getqcloudtoken",
                new { lifespan }, timeOut);

        /// <summary>查询授权小程序是否绑定手机号（CheckMobileConfig）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="pushTemplateMessage">未绑定时是否向管理员推送收集手机号的模板消息；不指定则不发送该字段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>手机号绑定状态。</returns>
        public static CloudBaseMobileConfigJsonResult CheckMobileConfig(string authorizerAccessToken,
            bool? pushTemplateMessage = null, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseMobileConfigJsonResult>(authorizerAccessToken, "/tcb/checkmobile",
                new { push_tmpl = pushTemplateMessage }, timeOut);

        /// <summary>异步查询授权小程序是否绑定手机号（CheckMobileConfig）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="pushTemplateMessage">未绑定时是否向管理员推送收集手机号的模板消息；不指定则不发送该字段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>手机号绑定状态。</returns>
        public static Task<CloudBaseMobileConfigJsonResult> CheckMobileConfigAsync(string authorizerAccessToken,
            bool? pushTemplateMessage = null, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseMobileConfigJsonResult>(authorizerAccessToken, "/tcb/checkmobile",
                new { push_tmpl = pushTemplateMessage }, timeOut);

        /// <summary>转换授权小程序当前关联的云开发环境（ChangeTcbEnv）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="env">目标云开发环境 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ChangeTcbEnv(string authorizerAccessToken, string env,
            int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(authorizerAccessToken, "/tcb/modifyenv", new { env }, timeOut);

        /// <summary>异步转换授权小程序当前关联的云开发环境（ChangeTcbEnv）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="env">目标云开发环境 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ChangeTcbEnvAsync(string authorizerAccessToken, string env,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(authorizerAccessToken, "/tcb/modifyenv", new { env }, timeOut);

        /// <summary>为授权小程序创建后付费云开发环境（CreateEnv）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="alias">环境别名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新创建的环境 ID。</returns>
        public static CloudBaseCreateEnvJsonResult CreateEnv(string authorizerAccessToken, string alias,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseCreateEnvJsonResult>(authorizerAccessToken, "/tcb/createenvandresource",
                CreateEnvRequest(alias), timeOut);

        /// <summary>异步为授权小程序创建后付费云开发环境（CreateEnv）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="alias">环境别名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新创建的环境 ID。</returns>
        public static Task<CloudBaseCreateEnvJsonResult> CreateEnvAsync(string authorizerAccessToken, string alias,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseCreateEnvJsonResult>(authorizerAccessToken, "/tcb/createenvandresource",
                CreateEnvRequest(alias), timeOut);

        /// <summary>获取授权小程序的云开发环境信息（GetEnvInfo）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="env">可选环境 ID；为空时返回全部环境。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云开发环境列表。</returns>
        public static CloudBaseEnvInfoJsonResult GetEnvInfo(string authorizerAccessToken, string env = null,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseEnvInfoJsonResult>(authorizerAccessToken, "/tcb/getenvinfo", new { env }, timeOut);

        /// <summary>异步获取授权小程序的云开发环境信息（GetEnvInfo）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="env">可选环境 ID；为空时返回全部环境。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云开发环境列表。</returns>
        public static Task<CloudBaseEnvInfoJsonResult> GetEnvInfoAsync(string authorizerAccessToken,
            string env = null, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseEnvInfoJsonResult>(authorizerAccessToken, "/tcb/getenvinfo", new { env }, timeOut);

        /// <summary>使用第三方平台凭证批量共享云开发环境（ShareEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境与目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>共享结果及需要管理员确认的链接。</returns>
        public static CloudBaseBatchShareEnvJsonResult ShareEnv(string componentAccessToken,
            IEnumerable<CloudBaseBatchEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchshareenv",
                CreateShareRequest(data), timeOut);

        /// <summary>异步使用第三方平台凭证批量共享云开发环境（ShareEnv）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="data">环境与目标小程序 AppID 的对应关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>共享结果及需要管理员确认的链接。</returns>
        public static Task<CloudBaseBatchShareEnvJsonResult> ShareEnvAsync(string componentAccessToken,
            IEnumerable<CloudBaseBatchEnvShareItem> data, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchShareEnvJsonResult>(componentAccessToken, "/componenttcb/batchshareenv",
                CreateShareRequest(data), timeOut);

        #endregion

        #region 消息推送

        /// <summary>上传云函数或云托管消息推送配置（SetCallBackConfig）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">消息推送配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SetCallBackConfig(string authorizerAccessToken,
            CloudBaseCallbackConfig request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(authorizerAccessToken, "/tcb/setcallbackconfig", request, timeOut);

        /// <summary>异步上传云函数或云托管消息推送配置（SetCallBackConfig）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">消息推送配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> SetCallBackConfigAsync(string authorizerAccessToken,
            CloudBaseCallbackConfig request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(authorizerAccessToken, "/tcb/setcallbackconfig", request, timeOut);

        /// <summary>获取云开发消息推送配置（GetCallBackConfig）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数和云托管消息推送配置。</returns>
        public static CloudBaseCallbackConfigJsonResult GetCallBackConfig(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseCallbackConfigJsonResult>(authorizerAccessToken, "/tcb/getcallbackconfig", new { },
                timeOut);

        /// <summary>异步获取云开发消息推送配置（GetCallBackConfig）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数和云托管消息推送配置。</returns>
        public static Task<CloudBaseCallbackConfigJsonResult> GetCallBackConfigAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseCallbackConfigJsonResult>(authorizerAccessToken, "/tcb/getcallbackconfig", new { },
                timeOut);

        #endregion

        #region 云函数

        /// <summary>触发授权小程序的云函数（InvokeCloudFunction）。</summary>
        /// <remarks>官方参数表将 <c>env</c>、<c>name</c>、<c>req_data</c>列为请求参数，但 HTTPS 示例要求前两项位于查询字符串；本方法遵循可执行示例并原样传递请求体。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="name">云函数名称。</param>
        /// <param name="postBody">云函数自定义请求体。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数返回数据。</returns>
        public static CloudBaseBatchInvokeFunctionJsonResult InvokeCloudFunction(string authorizerAccessToken,
            string env, string name, object postBody, int timeOut = Config.TIME_OUT) =>
            SendInvoke<CloudBaseBatchInvokeFunctionJsonResult>(authorizerAccessToken, "/tcb/invokecloudfunction",
                env, name, postBody, timeOut);

        /// <summary>异步触发授权小程序的云函数（InvokeCloudFunction）。</summary>
        /// <remarks>官方参数表将 <c>env</c>、<c>name</c>、<c>req_data</c>列为请求参数，但 HTTPS 示例要求前两项位于查询字符串；本方法遵循可执行示例并原样传递请求体。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="name">云函数名称。</param>
        /// <param name="postBody">云函数自定义请求体。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数返回数据。</returns>
        public static Task<CloudBaseBatchInvokeFunctionJsonResult> InvokeCloudFunctionAsync(
            string authorizerAccessToken, string env, string name, object postBody,
            int timeOut = Config.TIME_OUT) =>
            SendInvokeAsync<CloudBaseBatchInvokeFunctionJsonResult>(authorizerAccessToken,
                "/tcb/invokecloudfunction", env, name, postBody, timeOut);

        /// <summary>使用第三方平台凭证批量创建云函数（CreateFunction）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">函数名称、环境列表和 ZIP 代码包。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建失败的环境列表。</returns>
        public static CloudBaseBatchFunctionFailJsonResult CreateFunction(string componentAccessToken,
            CloudBaseBatchUploadFunctionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchFunctionFailJsonResult>(componentAccessToken, "/componenttcb/batchuploadscf",
                request, timeOut);

        /// <summary>异步使用第三方平台凭证批量创建云函数（CreateFunction）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">函数名称、环境列表和 ZIP 代码包。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建失败的环境列表。</returns>
        public static Task<CloudBaseBatchFunctionFailJsonResult> CreateFunctionAsync(string componentAccessToken,
            CloudBaseBatchUploadFunctionRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchFunctionFailJsonResult>(componentAccessToken, "/componenttcb/batchuploadscf",
                request, timeOut);

        /// <summary>获取云函数代码保护密钥（GetCodeSecret）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>代码保护密钥。</returns>
        public static CloudBaseCodeSecretJsonResult GetCodeSecret(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseCodeSecretJsonResult>(authorizerAccessToken, "/tcb/getcodesecret", new { }, timeOut);

        /// <summary>异步获取云函数代码保护密钥（GetCodeSecret）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>代码保护密钥。</returns>
        public static Task<CloudBaseCodeSecretJsonResult> GetCodeSecretAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseCodeSecretJsonResult>(authorizerAccessToken, "/tcb/getcodesecret", new { }, timeOut);

        /// <summary>获取上传云函数代码所需的签名请求头（GetUploadSignature）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getYploadSignature</c>，SDK 使用正确拼写。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="hashedPayload">上传腾讯云 SCF 请求体的 SHA-256 小写摘要。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>带签名的请求头字符串。</returns>
        public static CloudBaseUploadSignatureJsonResult GetUploadSignature(string authorizerAccessToken,
            string hashedPayload, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseUploadSignatureJsonResult>(authorizerAccessToken, "/tcb/getuploadsignature",
                new { hashed_payload = hashedPayload }, timeOut);

        /// <summary>异步获取上传云函数代码所需的签名请求头（GetUploadSignature）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getYploadSignature</c>，SDK 使用正确拼写。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="hashedPayload">上传腾讯云 SCF 请求体的 SHA-256 小写摘要。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>带签名的请求头字符串。</returns>
        public static Task<CloudBaseUploadSignatureJsonResult> GetUploadSignatureAsync(
            string authorizerAccessToken, string hashedPayload, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseUploadSignatureJsonResult>(authorizerAccessToken, "/tcb/getuploadsignature",
                new { hashed_payload = hashedPayload }, timeOut);

        /// <summary>获取云函数列表（GetFunctionList）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getFuntionList</c>，SDK 使用正确拼写。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数列表。</returns>
        public static CloudBaseBatchFunctionListJsonResult GetFunctionList(string authorizerAccessToken,
            CloudBaseFunctionListRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchFunctionListJsonResult>(authorizerAccessToken, "/tcb/listfunctions", request,
                timeOut);

        /// <summary>异步获取云函数列表（GetFunctionList）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getFuntionList</c>，SDK 使用正确拼写。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>云函数列表。</returns>
        public static Task<CloudBaseBatchFunctionListJsonResult> GetFunctionListAsync(
            string authorizerAccessToken, CloudBaseFunctionListRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchFunctionListJsonResult>(authorizerAccessToken, "/tcb/listfunctions", request,
                timeOut);

        /// <summary>获取云函数代码下载地址（GetFunctionLink）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getFuntionLink</c>，SDK 使用正确拼写。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载地址和代码 SHA-256 摘要。</returns>
        public static CloudBaseFunctionLinkJsonResult GetFunctionLink(string authorizerAccessToken,
            CloudBaseFunctionIdentityRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseFunctionLinkJsonResult>(authorizerAccessToken, "/tcb/downloadfunction", request,
                timeOut);

        /// <summary>异步获取云函数代码下载地址（GetFunctionLink）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getFuntionLink</c>，SDK 使用正确拼写。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载地址和代码 SHA-256 摘要。</returns>
        public static Task<CloudBaseFunctionLinkJsonResult> GetFunctionLinkAsync(string authorizerAccessToken,
            CloudBaseFunctionIdentityRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseFunctionLinkJsonResult>(authorizerAccessToken, "/tcb/downloadfunction", request,
                timeOut);

        /// <summary>上传云函数配置（UploadFunctionConfig）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getUploadFuntionConfig</c>，SDK 使用符合操作语义的名称。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">配置类型、环境、函数名和 JSON 配置字符串。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult UploadFunctionConfig(string authorizerAccessToken,
            CloudBaseUploadFunctionConfigRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(authorizerAccessToken, "/tcb/uploadfuncconfig", request, timeOut);

        /// <summary>异步上传云函数配置（UploadFunctionConfig）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getUploadFuntionConfig</c>，SDK 使用符合操作语义的名称。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">配置类型、环境、函数名和 JSON 配置字符串。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> UploadFunctionConfigAsync(string authorizerAccessToken,
            CloudBaseUploadFunctionConfigRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(authorizerAccessToken, "/tcb/uploadfuncconfig", request, timeOut);

        /// <summary>获取云函数配置（GetFunctionConfig）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getFuntionConfig</c>，SDK 使用正确拼写。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">配置类型、环境 ID 和云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>JSON 字符串形式的云函数配置。</returns>
        public static CloudBaseFunctionConfigJsonResult GetFunctionConfig(string authorizerAccessToken,
            CloudBaseFunctionConfigRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseFunctionConfigJsonResult>(authorizerAccessToken, "/tcb/getfuncconfig", request,
                timeOut);

        /// <summary>异步获取云函数配置（GetFunctionConfig）。</summary>
        /// <remarks>微信官方目录英文名误写为 <c>getFuntionConfig</c>，SDK 使用正确拼写。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">配置类型、环境 ID 和云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>JSON 字符串形式的云函数配置。</returns>
        public static Task<CloudBaseFunctionConfigJsonResult> GetFunctionConfigAsync(
            string authorizerAccessToken, CloudBaseFunctionConfigRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseFunctionConfigJsonResult>(authorizerAccessToken, "/tcb/getfuncconfig", request,
                timeOut);

        #endregion

        #region 文件管理

        /// <summary>获取云存储文件上传链接（GetUploadTcbFileLink）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和上传路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传 URL、临时令牌、签名和文件 ID。</returns>
        public static CloudBaseBatchUploadFileJsonResult GetUploadTcbFileLink(string authorizerAccessToken,
            CloudBaseBatchFilePathRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchUploadFileJsonResult>(authorizerAccessToken, "/tcb/uploadfile", request, timeOut);

        /// <summary>异步获取云存储文件上传链接（GetUploadTcbFileLink）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和上传路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传 URL、临时令牌、签名和文件 ID。</returns>
        public static Task<CloudBaseBatchUploadFileJsonResult> GetUploadTcbFileLinkAsync(
            string authorizerAccessToken, CloudBaseBatchFilePathRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchUploadFileJsonResult>(authorizerAccessToken, "/tcb/uploadfile", request,
                timeOut);

        /// <summary>使用第三方平台凭证批量删除云存储文件（DeleteTcbCloudFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和文件 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>每个文件的删除结果。</returns>
        public static CloudBaseBatchDeleteFileJsonResult DeleteTcbCloudFile(string componentAccessToken,
            CloudBaseBatchDeleteFileRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchDeleteFileJsonResult>(componentAccessToken, "/componenttcb/batchdeletefile",
                request, timeOut);

        /// <summary>异步使用第三方平台凭证批量删除云存储文件（DeleteTcbCloudFile）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和文件 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>每个文件的删除结果。</returns>
        public static Task<CloudBaseBatchDeleteFileJsonResult> DeleteTcbCloudFileAsync(
            string componentAccessToken, CloudBaseBatchDeleteFileRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchDeleteFileJsonResult>(componentAccessToken,
                "/componenttcb/batchdeletefile", request, timeOut);

        /// <summary>批量获取云存储文件下载链接（GetDownloadTcbFileLink）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID、文件 ID 和链接有效期。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>每个文件的下载链接和状态。</returns>
        public static CloudBaseBatchDownloadFileJsonResult GetDownloadTcbFileLink(string authorizerAccessToken,
            CloudBaseBatchDownloadFileRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchDownloadFileJsonResult>(authorizerAccessToken, "/tcb/batchdownloadfile", request,
                timeOut);

        /// <summary>异步批量获取云存储文件下载链接（GetDownloadTcbFileLink）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID、文件 ID 和链接有效期。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>每个文件的下载链接和状态。</returns>
        public static Task<CloudBaseBatchDownloadFileJsonResult> GetDownloadTcbFileLinkAsync(
            string authorizerAccessToken, CloudBaseBatchDownloadFileRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchDownloadFileJsonResult>(authorizerAccessToken, "/tcb/batchdownloadfile",
                request, timeOut);

        #endregion

        #region 数据库管理

        /// <summary>使用第三方平台凭证执行数据库聚合查询（AggregateDatabase）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和聚合命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>JSON 字符串形式的数据列表。</returns>
        public static CloudBaseBatchQueryJsonResult AggregateDatabase(string componentAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchQueryJsonResult>(componentAccessToken, "/componenttcb/dbaggregate", request,
                timeOut);

        /// <summary>异步使用第三方平台凭证执行数据库聚合查询（AggregateDatabase）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境 ID 和聚合命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>JSON 字符串形式的数据列表。</returns>
        public static Task<CloudBaseBatchQueryJsonResult> AggregateDatabaseAsync(string componentAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchQueryJsonResult>(componentAccessToken, "/componenttcb/dbaggregate", request,
                timeOut);

        /// <summary>查询数据库迁移任务状态（GetDatabaseMigrateStatus）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和迁移任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移状态、成功失败数和结果文件地址。</returns>
        public static CloudBaseBatchMigrationStateJsonResult GetDatabaseMigrateStatus(
            string authorizerAccessToken, CloudBaseBatchMigrationStateRequest request,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchMigrationStateJsonResult>(authorizerAccessToken,
                "/tcb/databasemigratequeryinfo", request, timeOut);

        /// <summary>异步查询数据库迁移任务状态（GetDatabaseMigrateStatus）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和迁移任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>迁移状态、成功失败数和结果文件地址。</returns>
        public static Task<CloudBaseBatchMigrationStateJsonResult> GetDatabaseMigrateStatusAsync(
            string authorizerAccessToken, CloudBaseBatchMigrationStateRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchMigrationStateJsonResult>(authorizerAccessToken,
                "/tcb/databasemigratequeryinfo", request, timeOut);

        /// <summary>更新数据库记录（UpdateDatabaseRecord）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和数据库命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>匹配、修改数量及可能的新记录 ID。</returns>
        public static CloudBaseDatabaseUpdateJsonResult UpdateDatabaseRecord(string authorizerAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseDatabaseUpdateJsonResult>(authorizerAccessToken, "/tcb/databaseupdate", request,
                timeOut);

        /// <summary>异步更新数据库记录（UpdateDatabaseRecord）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和数据库命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>匹配、修改数量及可能的新记录 ID。</returns>
        public static Task<CloudBaseDatabaseUpdateJsonResult> UpdateDatabaseRecordAsync(
            string authorizerAccessToken, CloudBaseBatchDbQueryRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseDatabaseUpdateJsonResult>(authorizerAccessToken, "/tcb/databaseupdate", request,
                timeOut);

        /// <summary>使用第三方平台凭证查询、新增或删除数据库集合（DbCollectionManage）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、操作类型及可选集合和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>集合列表及总数。</returns>
        public static CloudBaseCollectionManageJsonResult DbCollectionManage(string componentAccessToken,
            CloudBaseCollectionManageRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseCollectionManageJsonResult>(componentAccessToken, "/componenttcb/dbcollection", request,
                timeOut);

        /// <summary>异步使用第三方平台凭证查询、新增或删除数据库集合（DbCollectionManage）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、操作类型及可选集合和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>集合列表及总数。</returns>
        public static Task<CloudBaseCollectionManageJsonResult> DbCollectionManageAsync(
            string componentAccessToken, CloudBaseCollectionManageRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseCollectionManageJsonResult>(componentAccessToken, "/componenttcb/dbcollection",
                request, timeOut);

        /// <summary>向数据库插入记录（AddDatabaseItem）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和数据库新增命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新增记录 ID 列表。</returns>
        public static CloudBaseDatabaseAddJsonResult AddDatabaseItem(string authorizerAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseDatabaseAddJsonResult>(authorizerAccessToken, "/tcb/databaseadd", request, timeOut);

        /// <summary>异步向数据库插入记录（AddDatabaseItem）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和数据库新增命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新增记录 ID 列表。</returns>
        public static Task<CloudBaseDatabaseAddJsonResult> AddDatabaseItemAsync(string authorizerAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseDatabaseAddJsonResult>(authorizerAccessToken, "/tcb/databaseadd", request, timeOut);

        /// <summary>新增数据库集合（AddDatabaseCollection）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和集合名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult AddDatabaseCollection(string authorizerAccessToken,
            CloudBaseBatchCollectionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(authorizerAccessToken, "/tcb/databasecollectionadd", request, timeOut);

        /// <summary>异步新增数据库集合（AddDatabaseCollection）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和集合名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> AddDatabaseCollectionAsync(string authorizerAccessToken,
            CloudBaseBatchCollectionRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(authorizerAccessToken, "/tcb/databasecollectionadd", request, timeOut);

        /// <summary>删除数据库集合（DeleteDatabaseCollection）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和集合名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult DeleteDatabaseCollection(string authorizerAccessToken,
            CloudBaseBatchCollectionRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(authorizerAccessToken, "/tcb/databasecollectiondelete", request, timeOut);

        /// <summary>异步删除数据库集合（DeleteDatabaseCollection）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和集合名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> DeleteDatabaseCollectionAsync(string authorizerAccessToken,
            CloudBaseBatchCollectionRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(authorizerAccessToken, "/tcb/databasecollectiondelete", request, timeOut);

        /// <summary>获取数据库集合信息（GetDatabaseCollection）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境和可选分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>集合信息和分页结果。</returns>
        public static CloudBaseCollectionListJsonResult GetDatabaseCollection(string authorizerAccessToken,
            CloudBaseCollectionListRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseCollectionListJsonResult>(authorizerAccessToken, "/tcb/databasecollectionget", request,
                timeOut);

        /// <summary>异步获取数据库集合信息（GetDatabaseCollection）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境和可选分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>集合信息和分页结果。</returns>
        public static Task<CloudBaseCollectionListJsonResult> GetDatabaseCollectionAsync(
            string authorizerAccessToken, CloudBaseCollectionListRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseCollectionListJsonResult>(authorizerAccessToken, "/tcb/databasecollectionget",
                request, timeOut);

        /// <summary>统计数据库集合记录数（GetDatabaseCount）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和统计命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>记录数量。</returns>
        public static CloudBaseDatabaseCountJsonResult GetDatabaseCount(string authorizerAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseDatabaseCountJsonResult>(authorizerAccessToken, "/tcb/databasecount", request, timeOut);

        /// <summary>异步统计数据库集合记录数（GetDatabaseCount）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和统计命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>记录数量。</returns>
        public static Task<CloudBaseDatabaseCountJsonResult> GetDatabaseCountAsync(string authorizerAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseDatabaseCountJsonResult>(authorizerAccessToken, "/tcb/databasecount", request,
                timeOut);

        /// <summary>删除数据库记录（DeleteDatabaseItem）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和数据库删除命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除的记录数。</returns>
        public static CloudBaseDatabaseDeleteJsonResult DeleteDatabaseItem(string authorizerAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseDatabaseDeleteJsonResult>(authorizerAccessToken, "/tcb/databasedelete", request,
                timeOut);

        /// <summary>异步删除数据库记录（DeleteDatabaseItem）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和数据库删除命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除的记录数。</returns>
        public static Task<CloudBaseDatabaseDeleteJsonResult> DeleteDatabaseItemAsync(
            string authorizerAccessToken, CloudBaseBatchDbQueryRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseDatabaseDeleteJsonResult>(authorizerAccessToken, "/tcb/databasedelete", request,
                timeOut);

        /// <summary>使用第三方平台凭证导出数据库（ExportDatabaseItem）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、文件路径、格式和导出条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>导出任务 ID。</returns>
        public static CloudBaseBatchJobJsonResult ExportDatabaseItem(string componentAccessToken,
            CloudBaseBatchDbExportRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchJobJsonResult>(componentAccessToken, "/componenttcb/dbexport", request, timeOut);

        /// <summary>异步使用第三方平台凭证导出数据库（ExportDatabaseItem）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、文件路径、格式和导出条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>导出任务 ID。</returns>
        public static Task<CloudBaseBatchJobJsonResult> ExportDatabaseItemAsync(string componentAccessToken,
            CloudBaseBatchDbExportRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchJobJsonResult>(componentAccessToken, "/componenttcb/dbexport", request,
                timeOut);

        /// <summary>使用第三方平台凭证导入数据库（ImportDatabaseItem）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、集合、文件及冲突处理参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>导入任务 ID。</returns>
        public static CloudBaseBatchJobJsonResult ImportDatabaseItem(string componentAccessToken,
            CloudBaseBatchDbImportRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseBatchJobJsonResult>(componentAccessToken, "/componenttcb/dbimport", request, timeOut);

        /// <summary>异步使用第三方平台凭证导入数据库（ImportDatabaseItem）。</summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">环境、集合、文件及冲突处理参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>导入任务 ID。</returns>
        public static Task<CloudBaseBatchJobJsonResult> ImportDatabaseItemAsync(string componentAccessToken,
            CloudBaseBatchDbImportRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseBatchJobJsonResult>(componentAccessToken, "/componenttcb/dbimport", request,
                timeOut);

        /// <summary>查询数据库记录（GetDatabaseRecord）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和数据库查询命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分页信息和 JSON 字符串形式的数据列表。</returns>
        public static CloudBaseDatabaseQueryJsonResult GetDatabaseRecord(string authorizerAccessToken,
            CloudBaseBatchDbQueryRequest request, int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseDatabaseQueryJsonResult>(authorizerAccessToken, "/tcb/databasequery", request, timeOut);

        /// <summary>异步查询数据库记录（GetDatabaseRecord）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境 ID 和数据库查询命令。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分页信息和 JSON 字符串形式的数据列表。</returns>
        public static Task<CloudBaseDatabaseQueryJsonResult> GetDatabaseRecordAsync(
            string authorizerAccessToken, CloudBaseBatchDbQueryRequest request,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseDatabaseQueryJsonResult>(authorizerAccessToken, "/tcb/databasequery", request,
                timeOut);

        /// <summary>更新数据库索引（UpdateDatabaseIndex）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境、集合及新增和删除索引定义。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult UpdateDatabaseIndex(string authorizerAccessToken,
            CloudBaseUpdateIndexRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(authorizerAccessToken, "/tcb/updateindex", request, timeOut);

        /// <summary>异步更新数据库索引（UpdateDatabaseIndex）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">环境、集合及新增和删除索引定义。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> UpdateDatabaseIndexAsync(string authorizerAccessToken,
            CloudBaseUpdateIndexRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(authorizerAccessToken, "/tcb/updateindex", request, timeOut);

        #endregion

        #region 微信支付授权

        /// <summary>获取授权小程序已绑定的微信支付商户号列表（GetWechatPayList）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商户号及绑定、JSAPI、退款授权状态。</returns>
        public static CloudBaseWechatPayListJsonResult GetWechatPayList(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<CloudBaseWechatPayListJsonResult>(authorizerAccessToken, "/tcb/wxpaylist", new { }, timeOut);

        /// <summary>异步获取授权小程序已绑定的微信支付商户号列表（GetWechatPayList）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商户号及绑定、JSAPI、退款授权状态。</returns>
        public static Task<CloudBaseWechatPayListJsonResult> GetWechatPayListAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<CloudBaseWechatPayListJsonResult>(authorizerAccessToken, "/tcb/wxpaylist", new { }, timeOut);

        /// <summary>申请绑定商户号或开通 JSAPI、退款授权（GetWechatPayAuth）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">操作类型和商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult GetWechatPayAuth(string authorizerAccessToken,
            CloudBaseWechatPayAuthRequest request, int timeOut = Config.TIME_OUT) =>
            Send<WxJsonResult>(authorizerAccessToken, "/tcb/wxpayopenauth", request, timeOut);

        /// <summary>异步申请绑定商户号或开通 JSAPI、退款授权（GetWechatPayAuth）。</summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">操作类型和商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> GetWechatPayAuthAsync(string authorizerAccessToken,
            CloudBaseWechatPayAuthRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<WxJsonResult>(authorizerAccessToken, "/tcb/wxpayopenauth", request, timeOut);

        #endregion

        /// <summary>创建固定为后付费套餐的云环境请求体。</summary>
        private static object CreateEnvRequest(string alias)
        {
            return new { type = "CreatePostpayPackage", alias };
        }

        /// <summary>创建环境共享请求体，并固定来源类型为云开发。</summary>
        private static object CreateShareRequest(IEnumerable<CloudBaseBatchEnvShareItem> data)
        {
            return new { data, source_type = 0 };
        }

        /// <summary>构造普通代云开发接口地址。</summary>
        private static string BuildUrl(string accessToken, string path)
        {
            return $"{Config.ApiMpHost}{path}?access_token={accessToken.AsUrlData()}";
        }

        /// <summary>构造调用云函数接口地址。</summary>
        private static string BuildInvokeUrl(string accessToken, string path, string env, string name)
        {
            return $"{BuildUrl(accessToken, path)}&env={env.AsUrlData()}&name={name.AsUrlData()}";
        }

        /// <summary>同步发送普通代云开发请求。</summary>
        private static T Send<T>(string accessToken, string path, object request, int timeOut)
        {
            return CommonJsonSend.Send<T>(null, BuildUrl(accessToken, path), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>异步发送普通代云开发请求。</summary>
        private static Task<T> SendAsync<T>(string accessToken, string path, object request, int timeOut)
        {
            return CommonJsonSend.SendAsync<T>(null, BuildUrl(accessToken, path), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>同步调用授权小程序的云函数。</summary>
        private static T SendInvoke<T>(string accessToken, string path, string env, string name,
            object request, int timeOut)
        {
            return CommonJsonSend.Send<T>(null, BuildInvokeUrl(accessToken, path, env, name), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>异步调用授权小程序的云函数。</summary>
        private static Task<T> SendInvokeAsync<T>(string accessToken, string path, string env, string name,
            object request, int timeOut)
        {
            return CommonJsonSend.SendAsync<T>(null, BuildInvokeUrl(accessToken, path, env, name),
                request ?? new { }, CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);
        }
    }
}
