#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：P1Api.cs
    文件功能描述：P1Api 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口；补齐开放平台账号绑定状态查询

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxOpenAPIs.GetCategoryJson;
using Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson;
using Senparc.Weixin.Open.WxaAPIs.P1;
using Senparc.Weixin.Open.WxaAPIs.Sec;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 代商家管理小程序的基础信息、类目和代码服务状态接口。
    /// </summary>
    public static class WxaManagementApi
    {
        /// <summary>
        /// 查询小程序数据预拉取和周期性拉取设置。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static FetchDataSettingJsonResult GetFetchDataSetting(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostFetchDataSetting(authorizerAccessToken, new { action = "get" }, timeOut);

        /// <summary>
        /// 异步查询小程序数据预拉取和周期性拉取设置。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<FetchDataSettingJsonResult> GetFetchDataSettingAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostFetchDataSettingAsync(authorizerAccessToken, new { action = "get" }, timeOut);

        /// <summary>
        /// 设置小程序数据预拉取。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="isOpen">是否开启对应能力。</param>
        /// <param name="fetchType">数据拉取类型。</param>
        /// <param name="fetchUrl">数据拉取地址。</param>
        /// <param name="environmentId">智能机器人环境 ID。</param>
        /// <param name="functionName">云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static FetchDataSettingJsonResult SetPreFetchDataSetting(string authorizerAccessToken, bool isOpen,
            int fetchType, string fetchUrl = null, string environmentId = null, string functionName = null,
            int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                action = "set_pre_fetch",
                is_pre_fetch_open = isOpen,
                pre_fetch_type = fetchType,
                pre_fetch_url = fetchUrl,
                pre_env = environmentId,
                pre_function_name = functionName
            };
            return PostFetchDataSetting(authorizerAccessToken, data, timeOut);
        }

        /// <summary>
        /// 异步设置小程序数据预拉取。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="isOpen">是否开启对应能力。</param>
        /// <param name="fetchType">数据拉取类型。</param>
        /// <param name="fetchUrl">数据拉取地址。</param>
        /// <param name="environmentId">智能机器人环境 ID。</param>
        /// <param name="functionName">云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<FetchDataSettingJsonResult> SetPreFetchDataSettingAsync(string authorizerAccessToken,
            bool isOpen, int fetchType, string fetchUrl = null, string environmentId = null,
            string functionName = null, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                action = "set_pre_fetch",
                is_pre_fetch_open = isOpen,
                pre_fetch_type = fetchType,
                pre_fetch_url = fetchUrl,
                pre_env = environmentId,
                pre_function_name = functionName
            };
            return PostFetchDataSettingAsync(authorizerAccessToken, data, timeOut);
        }

        /// <summary>
        /// 设置小程序周期性数据拉取。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="isOpen">是否开启对应能力。</param>
        /// <param name="fetchType">数据拉取类型。</param>
        /// <param name="fetchUrl">数据拉取地址。</param>
        /// <param name="environmentId">智能机器人环境 ID。</param>
        /// <param name="functionName">云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static FetchDataSettingJsonResult SetPeriodFetchDataSetting(string authorizerAccessToken, bool isOpen,
            int fetchType, string fetchUrl = null, string environmentId = null, string functionName = null,
            int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                action = "set_period_fetch",
                is_period_fetch_open = isOpen,
                period_fetch_type = fetchType,
                period_fetch_url = fetchUrl,
                period_env = environmentId,
                period_function_name = functionName
            };
            return PostFetchDataSetting(authorizerAccessToken, data, timeOut);
        }

        /// <summary>
        /// 异步设置小程序周期性数据拉取。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="isOpen">是否开启对应能力。</param>
        /// <param name="fetchType">数据拉取类型。</param>
        /// <param name="fetchUrl">数据拉取地址。</param>
        /// <param name="environmentId">智能机器人环境 ID。</param>
        /// <param name="functionName">云函数名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<FetchDataSettingJsonResult> SetPeriodFetchDataSettingAsync(string authorizerAccessToken,
            bool isOpen, int fetchType, string fetchUrl = null, string environmentId = null,
            string functionName = null, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                action = "set_period_fetch",
                is_period_fetch_open = isOpen,
                period_fetch_type = fetchType,
                period_fetch_url = fetchUrl,
                period_env = environmentId,
                period_function_name = functionName
            };
            return PostFetchDataSettingAsync(authorizerAccessToken, data, timeOut);
        }

        /// <summary>
        /// 查询公众号或小程序是否已绑定开放平台账号。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>是否已绑定开放平台账号。</returns>
        /// <remarks>官方接口英文名：getBindOpenAccount。</remarks>
        public static BindOpenAccountJsonResult GetBindOpenAccount(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/open/have?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<BindOpenAccountJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 异步查询公众号或小程序是否已绑定开放平台账号。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>是否已绑定开放平台账号。</returns>
        /// <remarks>官方接口英文名：getBindOpenAccount。</remarks>
        public static Task<BindOpenAccountJsonResult> GetBindOpenAccountAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/open/have?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<BindOpenAccountJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 查询绑定的开放平台账号主体。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static SameEntityJsonResult GetBindOpenAccountEntity(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/open/sameentity?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<SameEntityJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 异步查询绑定的开放平台账号主体。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<SameEntityJsonResult> GetBindOpenAccountEntityAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/open/sameentity?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<SameEntityJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取小程序已设置类目。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetCategoryJsonResult GetSettingCategories(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/getcategory?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<GetCategoryJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 异步获取小程序已设置类目。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetCategoryJsonResult> GetSettingCategoriesAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/getcategory?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<GetCategoryJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 按认证类型查询小程序类目。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="verifyType">小程序认证类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static CategoriesByTypeJsonResult GetCategoriesByType(string authorizerAccessToken, int verifyType,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/getcategoriesbytype?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<CategoriesByTypeJsonResult>(null, url, new { verify_type = verifyType },
                CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步按认证类型查询小程序类目。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="verifyType">小程序认证类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<CategoriesByTypeJsonResult> GetCategoriesByTypeAsync(string authorizerAccessToken,
            int verifyType, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/getcategoriesbytype?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<CategoriesByTypeJsonResult>(null, url, new { verify_type = verifyType },
                CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询小程序类目名称。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetCategoryResultJson GetCategoryNames(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/get_category?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<GetCategoryResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 异步查询小程序类目名称。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetCategoryResultJson> GetCategoryNamesAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/get_category?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<GetCategoryResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 查询小程序访问状态。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static VisitStatusJsonResult GetVisitStatus(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/getvisitstatus?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<VisitStatusJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步查询小程序访问状态。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<VisitStatusJsonResult> GetVisitStatusAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/getvisitstatus?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<VisitStatusJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询小程序代码隐私接口信息。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static CodePrivacyInfoJsonResult GetCodePrivacyInfo(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/security/get_code_privacy_info?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<CodePrivacyInfoJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 异步查询小程序代码隐私接口信息。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<CodePrivacyInfoJsonResult> GetCodePrivacyInfoAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/security/get_code_privacy_info?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<CodePrivacyInfoJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        private static FetchDataSettingJsonResult PostFetchDataSetting(string accessToken, object data, int timeOut)
        {
            var url = $"{Config.ApiMpHost}/wxa/fetchdatasetting?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<FetchDataSettingJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        private static Task<FetchDataSettingJsonResult> PostFetchDataSettingAsync(string accessToken, object data,
            int timeOut)
        {
            var url = $"{Config.ApiMpHost}/wxa/fetchdatasetting?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<FetchDataSettingJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
    }

    /// <summary>
    /// 小程序认证及备案合并办理接口。
    /// </summary>
    public static class AuthAndIcpApi
    {
        /// <summary>
        /// 下载小程序备案媒体文件。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="mediaId">媒体文件 MediaId。</param>
        /// <param name="stream">接收下载内容的可写流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult GetIcpMedia(string authorizerAccessToken, string mediaId, Stream stream,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/icp/get_icp_media?access_token={authorizerAccessToken.AsUrlData()}&media_id={mediaId.AsUrlData()}";
            Get.Download(CommonDI.CommonSP, url, stream);
            return new WxJsonResult { errcode = ReturnCode.请求成功 };
        }

        /// <summary>
        /// 异步下载小程序备案媒体文件。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="mediaId">媒体文件 MediaId。</param>
        /// <param name="stream">接收下载内容的可写流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> GetIcpMediaAsync(string authorizerAccessToken, string mediaId,
            Stream stream, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/icp/get_icp_media?access_token={authorizerAccessToken.AsUrlData()}&media_id={mediaId.AsUrlData()}";
            await Get.DownloadAsync(CommonDI.CommonSP, url, stream).ConfigureAwait(false);
            return new WxJsonResult { errcode = ReturnCode.请求成功 };
        }

        /// <summary>
        /// 提交小程序认证及备案任务。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="authData">小程序认证资料。</param>
        /// <param name="icpSubject">小程序备案主体信息。</param>
        /// <param name="icpApplets">小程序备案信息。</param>
        /// <param name="icpMaterials">小程序备案材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static SubmitAuthAndIcpJsonResult SubmitAuthAndIcp(string authorizerAccessToken, AuthData authData,
            IcpSubjectModel icpSubject, IcpAppletsModel icpApplets, IcpMaterialsModel icpMaterials,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/sec/submit_auth_and_icp?access_token={authorizerAccessToken.AsUrlData()}";
            var data = new
            {
                auth_data = authData,
                icp_subject = icpSubject,
                icp_applets = icpApplets,
                icp_materials = icpMaterials
            };
            return CommonJsonSend.Send<SubmitAuthAndIcpJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步提交小程序认证及备案任务。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="authData">小程序认证资料。</param>
        /// <param name="icpSubject">小程序备案主体信息。</param>
        /// <param name="icpApplets">小程序备案信息。</param>
        /// <param name="icpMaterials">小程序备案材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<SubmitAuthAndIcpJsonResult> SubmitAuthAndIcpAsync(string authorizerAccessToken,
            AuthData authData, IcpSubjectModel icpSubject, IcpAppletsModel icpApplets,
            IcpMaterialsModel icpMaterials, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/sec/submit_auth_and_icp?access_token={authorizerAccessToken.AsUrlData()}";
            var data = new
            {
                auth_data = authData,
                icp_subject = icpSubject,
                icp_applets = icpApplets,
                icp_materials = icpMaterials
            };
            return CommonJsonSend.SendAsync<SubmitAuthAndIcpJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询小程序认证及备案任务。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="procedureId">认证及备案任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static QueryAuthAndIcpJsonResult QueryAuthAndIcp(string authorizerAccessToken,
            string procedureId, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/sec/query_auth_and_icp?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<QueryAuthAndIcpJsonResult>(null, url,
                new { procedure_id = procedureId }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步查询小程序认证及备案任务。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="procedureId">认证及备案任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<QueryAuthAndIcpJsonResult> QueryAuthAndIcpAsync(string authorizerAccessToken,
            string procedureId, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/wxa/sec/query_auth_and_icp?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<QueryAuthAndIcpJsonResult>(null, url,
                new { procedure_id = procedureId }, CommonJsonSendType.POST, timeOut);
        }
    }

    /// <summary>
    /// 直播和微信物流服务能力申请。
    /// </summary>
    public static class WxaCapabilityApi
    {
        /// <summary>
        /// 申请小程序直播能力。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ApplyLiveInfoJsonResult ApplyLiveInfo(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => Post<ApplyLiveInfoJsonResult>(authorizerAccessToken,
                "/wxa/business/applyliveinfo", new { action = "apply" }, timeOut);

        /// <summary>
        /// 异步申请小程序直播能力。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ApplyLiveInfoJsonResult> ApplyLiveInfoAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostAsync<ApplyLiveInfoJsonResult>(authorizerAccessToken,
                "/wxa/business/applyliveinfo", new { action = "apply" }, timeOut);

        /// <summary>
        /// 申请物流消息插件。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ApplyLogisticsMessagePlugin(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => Post<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/express/delivery/open_msg/open_openmsg", new { }, timeOut);

        /// <summary>
        /// 异步申请物流消息插件。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ApplyLogisticsMessagePluginAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostAsync<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/express/delivery/open_msg/open_openmsg", new { }, timeOut);

        /// <summary>
        /// 申请物流退货插件。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ApplyLogisticsReturnPlugin(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => Post<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/express/delivery/return/open_return", new { }, timeOut);

        /// <summary>
        /// 异步申请物流退货插件。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ApplyLogisticsReturnPluginAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostAsync<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/express/delivery/return/open_return", new { }, timeOut);

        /// <summary>
        /// 申请物流查询插件。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult ApplyLogisticsQueryPlugin(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => Post<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/express/delivery/open_msg/open_query_plugin", new { }, timeOut);

        /// <summary>
        /// 异步申请物流查询插件。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ApplyLogisticsQueryPluginAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostAsync<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/express/delivery/open_msg/open_query_plugin", new { }, timeOut);

        private static T Post<T>(string accessToken, string path, object data, int timeOut) where T : WxJsonResult
        {
            var url = $"{Config.ApiMpHost}{path}?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<T>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        private static Task<T> PostAsync<T>(string accessToken, string path, object data, int timeOut)
            where T : WxJsonResult
        {
            var url = $"{Config.ApiMpHost}{path}?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<T>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
    }

    /// <summary>
    /// We 分析登录与权限管理。
    /// </summary>
    public static class WeDataApi
    {
        /// <summary>
        /// 获取微信云开发登录配置。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WeDataLoginConfigJsonResult GetLoginConfig(string componentAccessToken,
            int timeOut = Config.TIME_OUT) => Post<WeDataLoginConfigJsonResult>(componentAccessToken,
                "/wedata/wedata_get_login_config", new { }, timeOut);

        /// <summary>
        /// 异步获取微信云开发登录配置。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WeDataLoginConfigJsonResult> GetLoginConfigAsync(string componentAccessToken,
            int timeOut = Config.TIME_OUT) => PostAsync<WeDataLoginConfigJsonResult>(componentAccessToken,
                "/wedata/wedata_get_login_config", new { }, timeOut);

        /// <summary>
        /// 设置微信云开发登录配置。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="setType">登录配置设置类型。</param>
        /// <param name="recheckUrl">认证复审回调地址。</param>
        /// <param name="associateAppIds">需要关联的小程序 AppId 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SetLoginConfig(string componentAccessToken, int setType,
            string recheckUrl = null, IEnumerable<string> associateAppIds = null, int timeOut = Config.TIME_OUT) =>
            Post<WxJsonResult>(componentAccessToken, "/wedata/wedata_set_login_config",
                new { set_type = setType, recheck_url = recheckUrl, associate_appid = associateAppIds }, timeOut);

        /// <summary>
        /// 异步设置微信云开发登录配置。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="setType">登录配置设置类型。</param>
        /// <param name="recheckUrl">认证复审回调地址。</param>
        /// <param name="associateAppIds">需要关联的小程序 AppId 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> SetLoginConfigAsync(string componentAccessToken, int setType,
            string recheckUrl = null, IEnumerable<string> associateAppIds = null, int timeOut = Config.TIME_OUT) =>
            PostAsync<WxJsonResult>(componentAccessToken, "/wedata/wedata_set_login_config",
                new { set_type = setType, recheck_url = recheckUrl, associate_appid = associateAppIds }, timeOut);

        /// <summary>
        /// 获取微信云开发用户权限列表。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WeDataPermissionListJsonResult GetPermissionList(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => Post<WeDataPermissionListJsonResult>(authorizerAccessToken,
                "/wedata/wedata_get_perm_list", new { }, timeOut);

        /// <summary>
        /// 异步获取微信云开发用户权限列表。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WeDataPermissionListJsonResult> GetPermissionListAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostAsync<WeDataPermissionListJsonResult>(authorizerAccessToken,
                "/wedata/wedata_get_perm_list", new { }, timeOut);

        /// <summary>
        /// 设置微信云开发用户权限。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="uid">微信云开发用户 ID。</param>
        /// <param name="permissions">微信云开发用户权限列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult SetUserPermission(string authorizerAccessToken, string uid,
            IEnumerable<WeDataPermission> permissions, int timeOut = Config.TIME_OUT) =>
            Post<WxJsonResult>(authorizerAccessToken, "/wedata/wedata_set_user_perm",
                new { uid, perm = permissions }, timeOut);

        /// <summary>
        /// 异步设置微信云开发用户权限。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="uid">微信云开发用户 ID。</param>
        /// <param name="permissions">微信云开发用户权限列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> SetUserPermissionAsync(string authorizerAccessToken, string uid,
            IEnumerable<WeDataPermission> permissions, int timeOut = Config.TIME_OUT) =>
            PostAsync<WxJsonResult>(authorizerAccessToken, "/wedata/wedata_set_user_perm",
                new { uid, perm = permissions }, timeOut);

        /// <summary>
        /// 查询微信云开发账号绑定列表。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WeDataBindListJsonResult QueryBindList(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => Post<WeDataBindListJsonResult>(authorizerAccessToken,
                "/wedata/wedata_query_bind_list", new { }, timeOut);

        /// <summary>
        /// 异步查询微信云开发账号绑定列表。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WeDataBindListJsonResult> QueryBindListAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostAsync<WeDataBindListJsonResult>(authorizerAccessToken,
                "/wedata/wedata_query_bind_list", new { }, timeOut);

        /// <summary>
        /// 解绑微信云开发用户。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="uid">微信云开发用户 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult UnbindUser(string authorizerAccessToken, string uid,
            int timeOut = Config.TIME_OUT) => Post<WxJsonResult>(authorizerAccessToken,
                "/wedata/wedata_unbind_user", new { uid }, timeOut);

        /// <summary>
        /// 异步解绑微信云开发用户。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="uid">微信云开发用户 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> UnbindUserAsync(string authorizerAccessToken, string uid,
            int timeOut = Config.TIME_OUT) => PostAsync<WxJsonResult>(authorizerAccessToken,
                "/wedata/wedata_unbind_user", new { uid }, timeOut);

        /// <summary>
        /// 登录微信云开发控制台。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="userSession">用户登录态。</param>
        /// <param name="uid">微信云开发用户 ID。</param>
        /// <param name="clientIp">客户端 IP 地址。</param>
        /// <param name="userAgent">客户端 User-Agent。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WeDataLoginJsonResult Login(string authorizerAccessToken, string userSession, string uid,
            string clientIp, string userAgent, int timeOut = Config.TIME_OUT) =>
            Post<WeDataLoginJsonResult>(authorizerAccessToken, "/wedata/wedata_login",
                new { user_session = userSession, uid, client_ip = clientIp, user_agent = userAgent }, timeOut);

        /// <summary>
        /// 异步登录微信云开发控制台。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="userSession">用户登录态。</param>
        /// <param name="uid">微信云开发用户 ID。</param>
        /// <param name="clientIp">客户端 IP 地址。</param>
        /// <param name="userAgent">客户端 User-Agent。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WeDataLoginJsonResult> LoginAsync(string authorizerAccessToken, string userSession,
            string uid, string clientIp, string userAgent, int timeOut = Config.TIME_OUT) =>
            PostAsync<WeDataLoginJsonResult>(authorizerAccessToken, "/wedata/wedata_login",
                new { user_session = userSession, uid, client_ip = clientIp, user_agent = userAgent }, timeOut);

        private static T Post<T>(string accessToken, string path, object data, int timeOut) where T : WxJsonResult
        {
            var url = $"{Config.ApiMpHost}{path}?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<T>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        private static Task<T> PostAsync<T>(string accessToken, string path, object data, int timeOut)
            where T : WxJsonResult
        {
            var url = $"{Config.ApiMpHost}{path}?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<T>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
    }
}
