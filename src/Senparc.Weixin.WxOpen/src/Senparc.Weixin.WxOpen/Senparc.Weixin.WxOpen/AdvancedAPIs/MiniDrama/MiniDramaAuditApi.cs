#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MiniDramaAuditApi.cs
    文件功能描述：MiniDramaAuditApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama
{
    /// <summary>小程序短剧剧目审核与数据统计接口。</summary>
    public static partial class MiniDramaApi
    {
        #region 剧目审核

        /// <summary>提交短剧剧目审核或重新提审。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">剧目、剧集、资质、演员和版权材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>剧目 ID。</returns>
        /// <remarks>模型同时覆盖官方已公布的 2026-07-28 字段调整。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/auditdrama/api_auditdrama.html"/>。</remarks>
        public static MiniDramaAuditDramaJsonResult AuditDrama(string accessTokenOrAppId, MiniDramaAuditDramaRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaAuditDramaJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/auditdrama", request, timeOut);
        }

        /// <summary>异步提交短剧剧目审核或重新提审。</summary>
        /// <inheritdoc cref="AuditDrama"/>
        public static Task<MiniDramaAuditDramaJsonResult> AuditDramaAsync(string accessTokenOrAppId, MiniDramaAuditDramaRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaAuditDramaJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/auditdrama", request, timeOut);
        }

        /// <summary>分页查询已提交的短剧剧目。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>剧目信息列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/auditdrama/api_listdramas.html"/>。</remarks>
        public static MiniDramaListDramasJsonResult ListDramas(string accessTokenOrAppId, MiniDramaPageRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaListDramasJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/listdramas", request, timeOut);
        }

        /// <summary>异步分页查询已提交的短剧剧目。</summary>
        /// <inheritdoc cref="ListDramas"/>
        public static Task<MiniDramaListDramasJsonResult> ListDramasAsync(string accessTokenOrAppId, MiniDramaPageRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaListDramasJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/listdramas", request, timeOut);
        }

        /// <summary>查询指定短剧剧目信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">剧目 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>剧目、剧集和审核信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/auditdrama/api_getdrama.html"/>。</remarks>
        public static MiniDramaGetDramaJsonResult GetDrama(string accessTokenOrAppId, MiniDramaDramaIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetDramaJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getdrama", request, timeOut);
        }

        /// <summary>异步查询指定短剧剧目信息。</summary>
        /// <inheritdoc cref="GetDrama"/>
        public static Task<MiniDramaGetDramaJsonResult> GetDramaAsync(string accessTokenOrAppId, MiniDramaDramaIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetDramaJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getdrama", request, timeOut);
        }

        /// <summary>提交替换短剧剧集审核。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">剧目 ID 和剧集替换关系。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/auditdrama/api_submitreplacemedias.html"/>。</remarks>
        public static WxJsonResult SubmitReplaceDramaMedias(string accessTokenOrAppId, MiniDramaSubmitReplaceMediasRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/submitreplacedramamedias", request, timeOut);
        }

        /// <summary>异步提交替换短剧剧集审核。</summary>
        /// <inheritdoc cref="SubmitReplaceDramaMedias"/>
        public static Task<WxJsonResult> SubmitReplaceDramaMediasAsync(string accessTokenOrAppId, MiniDramaSubmitReplaceMediasRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/submitreplacedramamedias", request, timeOut);
        }

        /// <summary>替换已审核通过的短剧剧集。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">剧目 ID、旧媒资 ID 和新媒资 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>替换结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/auditdrama/api_replacedramamedia.html"/>。</remarks>
        public static WxJsonResult ReplaceDramaMedia(string accessTokenOrAppId, MiniDramaReplaceMediaRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/replacedramamedia", request, timeOut);
        }

        /// <summary>异步替换已审核通过的短剧剧集。</summary>
        /// <inheritdoc cref="ReplaceDramaMedia"/>
        public static Task<WxJsonResult> ReplaceDramaMediaAsync(string accessTokenOrAppId, MiniDramaReplaceMediaRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/replacedramamedia", request, timeOut);
        }

        /// <summary>修改短剧剧目基本信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">剧目 ID 及待修改的信息和材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>修改提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/auditdrama/api_submitmodifydramabasicinforeq.html"/>。</remarks>
        public static WxJsonResult ModifyDramaBasicInfo(string accessTokenOrAppId, MiniDramaModifyBasicInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/modifydramabasicinfo", request, timeOut);
        }

        /// <summary>异步修改短剧剧目基本信息。</summary>
        /// <inheritdoc cref="ModifyDramaBasicInfo"/>
        public static Task<WxJsonResult> ModifyDramaBasicInfoAsync(string accessTokenOrAppId, MiniDramaModifyBasicInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/modifydramabasicinfo", request, timeOut);
        }

        /// <summary>查询短剧剧目指定类型的最后一条审核信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">剧目 ID 和审核类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>最后审核状态和时间。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/auditdrama/api_getdramalatestauditinfo.html"/>。</remarks>
        public static MiniDramaGetLatestAuditInfoJsonResult GetDramaLatestAuditInfo(string accessTokenOrAppId, MiniDramaGetLatestAuditInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetLatestAuditInfoJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getdramalatestauditinfo", request, timeOut);
        }

        /// <summary>异步查询短剧剧目指定类型的最后一条审核信息。</summary>
        /// <inheritdoc cref="GetDramaLatestAuditInfo"/>
        public static Task<MiniDramaGetLatestAuditInfoJsonResult> GetDramaLatestAuditInfoAsync(string accessTokenOrAppId, MiniDramaGetLatestAuditInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetLatestAuditInfoJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getdramalatestauditinfo", request, timeOut);
        }

        #endregion

        #region 数据统计

        /// <summary>查询短剧点播 CDN 用量数据。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">时间范围、粒度和流量类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分时 CDN 流量数据。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/usagedata/api_getcdnusagedata.html"/>。</remarks>
        public static MiniDramaGetCdnUsageDataJsonResult GetCdnUsageData(string accessTokenOrAppId, MiniDramaGetCdnUsageDataRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetCdnUsageDataJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getcdnusagedata", request, timeOut);
        }

        /// <summary>异步查询短剧点播 CDN 用量数据。</summary>
        /// <inheritdoc cref="GetCdnUsageData"/>
        public static Task<MiniDramaGetCdnUsageDataJsonResult> GetCdnUsageDataAsync(string accessTokenOrAppId, MiniDramaGetCdnUsageDataRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetCdnUsageDataJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getcdnusagedata", request, timeOut);
        }

        /// <summary>查询短剧 CDN 日志下载链接。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">不超过 48 小时的时间范围和流量类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>24 小时内有效的日志下载链接。</returns>
        /// <remarks>返回表把日期、名称、URL 错标为 number 且把列表错标为 object，本模型按官方示例采用字符串数组。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/usagedata/api_getcdnlogs.html"/>。</remarks>
        public static MiniDramaGetCdnLogsJsonResult GetCdnLogs(string accessTokenOrAppId, MiniDramaGetCdnLogsRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetCdnLogsJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getcdnlogs", request, timeOut);
        }

        /// <summary>异步查询短剧 CDN 日志下载链接。</summary>
        /// <inheritdoc cref="GetCdnLogs"/>
        public static Task<MiniDramaGetCdnLogsJsonResult> GetCdnLogsAsync(string accessTokenOrAppId, MiniDramaGetCdnLogsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetCdnLogsJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getcdnlogs", request, timeOut);
        }

        /// <summary>分页查询短剧流量包详情。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">状态、分页和流量类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>流量包总额、消耗量和有效期。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/usagedata/api_listpackages.html"/>。</remarks>
        public static MiniDramaListPackagesJsonResult ListPackages(string accessTokenOrAppId, MiniDramaListPackagesRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaListPackagesJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/listpackages", request, timeOut);
        }

        /// <summary>异步分页查询短剧流量包详情。</summary>
        /// <inheritdoc cref="ListPackages"/>
        public static Task<MiniDramaListPackagesJsonResult> ListPackagesAsync(string accessTokenOrAppId, MiniDramaListPackagesRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaListPackagesJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/listpackages", request, timeOut);
        }

        #endregion
    }
}
