#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ManagedOfficialAccountApi.cs
    文件功能描述：ManagedOfficialAccountApi 微信接口封装


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

    修改标识：Senparc - 20260725
    修改描述：v4.24.4 补齐二维码规则校验文件下载入口和模型

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxOpenAPIs
{
    /// <summary>
    /// 使用公众号 authorizer_access_token 代管公众号关联小程序。
    /// </summary>
    public static class ManagedOfficialAccountApi
    {
        /// <summary>
        /// 获取公众号关联的小程序。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxaMpLinkGetJsonResult GetLinkMiniprogram(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/wxamplinkget?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<WxaMpLinkGetJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步获取公众号关联的小程序。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxaMpLinkGetJsonResult> GetLinkMiniprogramAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/wxamplinkget?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<WxaMpLinkGetJsonResult>(null, url, new { }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 关联公众号与小程序。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="notifyUsers">接收关联通知的用户列表。</param>
        /// <param name="showProfile">是否在关联页面展示公众号资料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult LinkMiniprogram(string authorizerAccessToken, string appId,
            bool notifyUsers, bool showProfile, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/wxamplink?access_token={authorizerAccessToken.AsUrlData()}";
            var data = new { appid = appId, notify_users = notifyUsers ? 1 : 0, show_profile = showProfile ? 1 : 0 };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步关联公众号与小程序。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="notifyUsers">接收关联通知的用户列表。</param>
        /// <param name="showProfile">是否在关联页面展示公众号资料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> LinkMiniprogramAsync(string authorizerAccessToken, string appId,
            bool notifyUsers, bool showProfile, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/wxamplink?access_token={authorizerAccessToken.AsUrlData()}";
            var data = new { appid = appId, notify_users = notifyUsers ? 1 : 0, show_profile = showProfile ? 1 : 0 };
            return CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 解除公众号与小程序关联。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult UnlinkMiniprogram(string authorizerAccessToken, string appId,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/wxampunlink?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<WxJsonResult>(null, url, new { appid = appId }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步解除公众号与小程序关联。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> UnlinkMiniprogramAsync(string authorizerAccessToken, string appId,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/wxampunlink?access_token={authorizerAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<WxJsonResult>(null, url, new { appid = appId }, CommonJsonSendType.POST, timeOut);
        }
    }

    /// <summary>
    /// 使用 authorizer_access_token 管理普通二维码或服务号二维码跳转规则。
    /// </summary>
    public static class QrCodeJumpApi
    {
        /// <summary>
        /// 获取二维码跳转规则。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="getType">二维码跳转规则查询类型。</param>
        /// <param name="prefixList">二维码规则前缀列表。</param>
        /// <param name="pageNumber">页码。</param>
        /// <param name="pageSize">每页记录数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static QrCodeJumpGetJsonResult Get(string authorizerAccessToken, string appId = null,
            int getType = 0, IEnumerable<string> prefixList = null, int? pageNumber = null,
            int? pageSize = null, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                appid = appId,
                get_type = getType,
                prefix_list = prefixList,
                page_num = pageNumber,
                page_size = pageSize
            };
            return Post<QrCodeJumpGetJsonResult>(authorizerAccessToken, "/cgi-bin/wxopen/qrcodejumpget", data, timeOut);
        }

        /// <summary>
        /// 异步获取二维码跳转规则。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="getType">二维码跳转规则查询类型。</param>
        /// <param name="prefixList">二维码规则前缀列表。</param>
        /// <param name="pageNumber">页码。</param>
        /// <param name="pageSize">每页记录数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<QrCodeJumpGetJsonResult> GetAsync(string authorizerAccessToken, string appId = null,
            int getType = 0, IEnumerable<string> prefixList = null, int? pageNumber = null,
            int? pageSize = null, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                appid = appId,
                get_type = getType,
                prefix_list = prefixList,
                page_num = pageNumber,
                page_size = pageSize
            };
            return PostAsync<QrCodeJumpGetJsonResult>(authorizerAccessToken, "/cgi-bin/wxopen/qrcodejumpget", data, timeOut);
        }

        /// <summary>
        /// 获取二维码跳转规则所属权校验文件的名称及内容。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>校验文件名称及文件内容。</returns>
        /// <remarks>
        /// 官方接口英文名：downloadQRCodeText。调用方应将返回的校验文件部署到二维码规则对应的服务器目录。
        /// </remarks>
        public static QrCodeJumpDownloadJsonResult DownloadQRCodeText(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => Post<QrCodeJumpDownloadJsonResult>(authorizerAccessToken,
                "/cgi-bin/wxopen/qrcodejumpdownload", new { }, timeOut);

        /// <summary>
        /// 异步获取二维码跳转规则所属权校验文件的名称及内容。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>校验文件名称及文件内容。</returns>
        /// <remarks>官方接口英文名：downloadQRCodeText。</remarks>
        public static Task<QrCodeJumpDownloadJsonResult> DownloadQRCodeTextAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) => PostAsync<QrCodeJumpDownloadJsonResult>(authorizerAccessToken,
                "/cgi-bin/wxopen/qrcodejumpdownload", new { }, timeOut);

        /// <summary>
        /// 新增或更新二维码跳转规则。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="prefix">二维码规则前缀。</param>
        /// <param name="path">跳转的小程序页面路径。</param>
        /// <param name="isEdit">是否更新已有二维码规则。</param>
        /// <param name="openVersion">二维码规则开放版本。</param>
        /// <param name="debugUrls">二维码规则调试链接列表。</param>
        /// <param name="permitSubRule">是否允许子规则生效。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult AddOrUpdate(string authorizerAccessToken, string prefix, string path,
            bool isEdit, int? openVersion = null, IEnumerable<string> debugUrls = null,
            int? permitSubRule = null, string appId = null, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                prefix,
                appid = appId,
                path,
                is_edit = isEdit ? 1 : 0,
                open_version = openVersion,
                debug_url = debugUrls,
                permit_sub_rule = permitSubRule
            };
            return Post<WxJsonResult>(authorizerAccessToken, "/cgi-bin/wxopen/qrcodejumpadd", data, timeOut);
        }

        /// <summary>
        /// 异步新增或更新二维码跳转规则。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="prefix">二维码规则前缀。</param>
        /// <param name="path">跳转的小程序页面路径。</param>
        /// <param name="isEdit">是否更新已有二维码规则。</param>
        /// <param name="openVersion">二维码规则开放版本。</param>
        /// <param name="debugUrls">二维码规则调试链接列表。</param>
        /// <param name="permitSubRule">是否允许子规则生效。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> AddOrUpdateAsync(string authorizerAccessToken, string prefix, string path,
            bool isEdit, int? openVersion = null, IEnumerable<string> debugUrls = null,
            int? permitSubRule = null, string appId = null, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                prefix,
                appid = appId,
                path,
                is_edit = isEdit ? 1 : 0,
                open_version = openVersion,
                debug_url = debugUrls,
                permit_sub_rule = permitSubRule
            };
            return PostAsync<WxJsonResult>(authorizerAccessToken, "/cgi-bin/wxopen/qrcodejumpadd", data, timeOut);
        }

        /// <summary>
        /// 发布二维码跳转规则。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="prefix">二维码规则前缀。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult Publish(string authorizerAccessToken, string prefix,
            int timeOut = Config.TIME_OUT) => Post<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/wxopen/qrcodejumppublish", new { prefix }, timeOut);

        /// <summary>
        /// 异步发布二维码跳转规则。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="prefix">二维码规则前缀。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> PublishAsync(string authorizerAccessToken, string prefix,
            int timeOut = Config.TIME_OUT) => PostAsync<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/wxopen/qrcodejumppublish", new { prefix }, timeOut);

        /// <summary>
        /// 删除二维码跳转规则。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="prefix">二维码规则前缀。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult Delete(string authorizerAccessToken, string prefix, string appId = null,
            int timeOut = Config.TIME_OUT) => Post<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/wxopen/qrcodejumpdelete", new { prefix, appid = appId }, timeOut);

        /// <summary>
        /// 异步删除二维码跳转规则。
        /// </summary>
        /// <param name="authorizerAccessToken">授权账号接口调用凭证。</param>
        /// <param name="prefix">二维码规则前缀。</param>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> DeleteAsync(string authorizerAccessToken, string prefix,
            string appId = null, int timeOut = Config.TIME_OUT) => PostAsync<WxJsonResult>(authorizerAccessToken,
                "/cgi-bin/wxopen/qrcodejumpdelete", new { prefix, appid = appId }, timeOut);

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
    /// QrCodeJumpGet 接口返回结果。
    /// </summary>
    public class QrCodeJumpGetJsonResult : WxJsonResult
    {
        public List<QrCodeJumpRule> rule_list { get; set; }
        public int qrcodejump_open { get; set; }
        public int list_size { get; set; }
        public int qrcodejump_pub_quota { get; set; }
        public int total_count { get; set; }
    }

    /// <summary>
    /// 获取二维码跳转规则所属权校验文件接口返回结果。
    /// </summary>
    public class QrCodeJumpDownloadJsonResult : WxJsonResult
    {
        /// <summary>
        /// 校验文件名称。
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        /// 校验文件内容。
        /// </summary>
        public string file_content { get; set; }
    }

    /// <summary>
    /// QrCodeJumpRule 微信接口数据模型。
    /// </summary>
    public class QrCodeJumpRule
    {
        public string prefix { get; set; }
        public string path { get; set; }
        public int state { get; set; }
        public int open_version { get; set; }
        public List<string> debug_url { get; set; }
    }
}
