#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AmsApi.cs
    文件功能描述：AmsApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Open.WxaAPIs.Ams;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 第三方平台小程序流量主代运营接口。
    /// </summary>
    /// <remarks>
    /// <para>服务商级配置和结算接口使用 <c>component_access_token</c>；小程序级接口使用 <c>authorizer_access_token</c>。</para>
    /// <para><see href="https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/ams/percentage/SetShareRatio.html">微信官方文档</see></para>
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class AmsApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 结算分成比例

        /// <summary>
        /// 设置服务商默认分账比例（SetShareRatio）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="shareRatio">服务商分账比例，例如 40 表示获得广告收益的 40%。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static AmsJsonResult SetShareRatio(string componentAccessToken, decimal shareRatio,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsJsonResult>(componentAccessToken, "/wxa/setdefaultamsinfo", "set_share_ratio",
                new AmsShareRatioRequest { share_ratio = shareRatio }, timeOut);

        /// <summary>
        /// 异步设置服务商默认分账比例（SetShareRatio）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="shareRatio">服务商分账比例，例如 40 表示获得广告收益的 40%。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<AmsJsonResult> SetShareRatioAsync(string componentAccessToken, decimal shareRatio,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsJsonResult>(componentAccessToken, "/wxa/setdefaultamsinfo", "set_share_ratio",
                new AmsShareRatioRequest { share_ratio = shareRatio }, timeOut);

        /// <summary>
        /// 查询指定 AppID 当前生效的分账比例（GetShareRatio）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appId">服务商或小程序 AppID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前生效的分账比例。</returns>
        public static AmsShareRatioJsonResult GetShareRatio(string componentAccessToken, string appId,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsShareRatioJsonResult>(componentAccessToken, "/wxa/getdefaultamsinfo", "get_share_ratio",
                new AmsAppIdRequest { appid = appId }, timeOut);

        /// <summary>
        /// 异步查询指定 AppID 当前生效的分账比例（GetShareRatio）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appId">服务商或小程序 AppID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前生效的分账比例。</returns>
        public static Task<AmsShareRatioJsonResult> GetShareRatioAsync(string componentAccessToken, string appId,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsShareRatioJsonResult>(componentAccessToken, "/wxa/getdefaultamsinfo", "get_share_ratio",
                new AmsAppIdRequest { appid = appId }, timeOut);

        /// <summary>
        /// 为指定小程序设置自定义分账比例（SetCustomShareRatio）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appId">目标小程序 AppID。</param>
        /// <param name="shareRatio">服务商自定义分账比例。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static AmsJsonResult SetCustomShareRatio(string componentAccessToken, string appId,
            decimal shareRatio, int timeOut = Config.TIME_OUT) =>
            Send<AmsJsonResult>(componentAccessToken, "/wxa/setdefaultamsinfo", "agency_set_custom_share_ratio",
                new AmsShareRatioRequest { appid = appId, share_ratio = shareRatio }, timeOut);

        /// <summary>
        /// 异步为指定小程序设置自定义分账比例（SetCustomShareRatio）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appId">目标小程序 AppID。</param>
        /// <param name="shareRatio">服务商自定义分账比例。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<AmsJsonResult> SetCustomShareRatioAsync(string componentAccessToken, string appId,
            decimal shareRatio, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsJsonResult>(componentAccessToken, "/wxa/setdefaultamsinfo", "agency_set_custom_share_ratio",
                new AmsShareRatioRequest { appid = appId, share_ratio = shareRatio }, timeOut);

        /// <summary>
        /// 查询指定小程序已配置的自定义分账比例（GetCustomShareRatio）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appId">目标小程序 AppID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>自定义分账比例。</returns>
        public static AmsShareRatioJsonResult GetCustomShareRatio(string componentAccessToken, string appId,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsShareRatioJsonResult>(componentAccessToken, "/wxa/getdefaultamsinfo", "agency_get_custom_share_ratio",
                new AmsAppIdRequest { appid = appId }, timeOut);

        /// <summary>
        /// 异步查询指定小程序已配置的自定义分账比例（GetCustomShareRatio）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="appId">目标小程序 AppID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>自定义分账比例。</returns>
        public static Task<AmsShareRatioJsonResult> GetCustomShareRatioAsync(string componentAccessToken, string appId,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsShareRatioJsonResult>(componentAccessToken, "/wxa/getdefaultamsinfo", "agency_get_custom_share_ratio",
                new AmsAppIdRequest { appid = appId }, timeOut);

        #endregion

        #region 开通流量主

        /// <summary>
        /// 检测授权小程序是否达到开通流量主门槛（AgencyCheckCanOpenPublisher）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>能否开通流量主的状态。</returns>
        public static AmsPublisherStatusJsonResult AgencyCheckCanOpenPublisher(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsPublisherStatusJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_check_can_open_publisher", new { }, timeOut);

        /// <summary>
        /// 异步检测授权小程序是否达到开通流量主门槛（AgencyCheckCanOpenPublisher）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>能否开通流量主的状态。</returns>
        public static Task<AmsPublisherStatusJsonResult> AgencyCheckCanOpenPublisherAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsPublisherStatusJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_check_can_open_publisher", new { }, timeOut);

        /// <summary>
        /// 为授权小程序开通流量主（AgencyCreatePublisher）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static AmsJsonResult AgencyCreatePublisher(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_create_publisher",
                new { }, timeOut);

        /// <summary>
        /// 异步为授权小程序开通流量主（AgencyCreatePublisher）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<AmsJsonResult> AgencyCreatePublisherAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_create_publisher",
                new { }, timeOut);

        #endregion

        #region 广告位管理

        /// <summary>
        /// 为授权小程序创建广告单元（AgencyCreateAdunit）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">广告单元创建参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新创建的广告单元信息。</returns>
        public static AmsCreateAdUnitJsonResult AgencyCreateAdunit(string authorizerAccessToken,
            AmsCreateAdUnitRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsCreateAdUnitJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_create_adunit",
                request, timeOut);

        /// <summary>
        /// 异步为授权小程序创建广告单元（AgencyCreateAdunit）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">广告单元创建参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新创建的广告单元信息。</returns>
        public static Task<AmsCreateAdUnitJsonResult> AgencyCreateAdunitAsync(string authorizerAccessToken,
            AmsCreateAdUnitRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsCreateAdUnitJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_create_adunit",
                request, timeOut);

        /// <summary>
        /// 更新授权小程序的广告单元（AgencyUpdateAdunit）。
        /// </summary>
        /// <remarks>官方页面的请求示例误写为创建 action；此处按接口英文名使用 <c>agency_update_adunit</c>。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">广告单元更新参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static AmsJsonResult AgencyUpdateAdunit(string authorizerAccessToken,
            AmsUpdateAdUnitRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_update_adunit",
                request, timeOut);

        /// <summary>
        /// 异步更新授权小程序的广告单元（AgencyUpdateAdunit）。
        /// </summary>
        /// <remarks>官方页面的请求示例误写为创建 action；此处按接口英文名使用 <c>agency_update_adunit</c>。</remarks>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">广告单元更新参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<AmsJsonResult> AgencyUpdateAdunitAsync(string authorizerAccessToken,
            AmsUpdateAdUnitRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_update_adunit",
                request, timeOut);

        /// <summary>
        /// 获取指定原生模板广告单元的模板类型（AgencyGetTmplType）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="adUnitId">广告单元 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>原生模板类型。</returns>
        public static AmsTemplateTypeJsonResult AgencyGetTmplType(string authorizerAccessToken, string adUnitId,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsTemplateTypeJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_tmpl_type",
                new AmsAdUnitIdRequest { ad_unit_id = adUnitId }, timeOut);

        /// <summary>
        /// 异步获取指定原生模板广告单元的模板类型（AgencyGetTmplType）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="adUnitId">广告单元 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>原生模板类型。</returns>
        public static Task<AmsTemplateTypeJsonResult> AgencyGetTmplTypeAsync(string authorizerAccessToken,
            string adUnitId, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsTemplateTypeJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_tmpl_type",
                new AmsAdUnitIdRequest { ad_unit_id = adUnitId }, timeOut);

        /// <summary>
        /// 获取服务商原生模板及其小程序广告单元绑定情况（GetAgencyTmplIdList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">原生模板分页及绑定关系查询参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>服务商原生模板及绑定关系。</returns>
        public static AmsAgencyTemplateListJsonResult GetAgencyTmplIdList(string authorizerAccessToken,
            AmsAgencyTemplateListRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsAgencyTemplateListJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "get_agency_ad_unit_list", request, timeOut);

        /// <summary>
        /// 异步获取服务商原生模板及其小程序广告单元绑定情况（GetAgencyTmplIdList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">原生模板分页及绑定关系查询参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>服务商原生模板及绑定关系。</returns>
        public static Task<AmsAgencyTemplateListJsonResult> GetAgencyTmplIdListAsync(string authorizerAccessToken,
            AmsAgencyTemplateListRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsAgencyTemplateListJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "get_agency_ad_unit_list", request, timeOut);

        /// <summary>
        /// 设置授权小程序的封面广告位开关状态（SetCoverAdposStatus）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="status">封面广告位状态：1 开启，4 关闭。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static AmsJsonResult SetCoverAdposStatus(string authorizerAccessToken, int status,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_set_cover_adpos_status",
                new AmsCoverStatusRequest { status = status }, timeOut);

        /// <summary>
        /// 异步设置授权小程序的封面广告位开关状态（SetCoverAdposStatus）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="status">封面广告位状态：1 开启，4 关闭。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<AmsJsonResult> SetCoverAdposStatusAsync(string authorizerAccessToken, int status,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_set_cover_adpos_status",
                new AmsCoverStatusRequest { status = status }, timeOut);

        /// <summary>
        /// 设置授权小程序封面广告位的场景值（SetCoverAdposScene）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="coverSceneList">以英文逗号分隔的场景值。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static AmsJsonResult SetCoverAdposScene(string authorizerAccessToken, string coverSceneList,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_set_cover_adpos_scene",
                new AmsCoverSceneRequest { cover_scene_list = coverSceneList }, timeOut);

        /// <summary>
        /// 异步设置授权小程序封面广告位的场景值（SetCoverAdposScene）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="coverSceneList">以英文逗号分隔的场景值。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<AmsJsonResult> SetCoverAdposSceneAsync(string authorizerAccessToken, string coverSceneList,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_set_cover_adpos_scene",
                new AmsCoverSceneRequest { cover_scene_list = coverSceneList }, timeOut);

        /// <summary>
        /// 获取授权小程序封面广告位的开关状态（GetCoverAdposStatus）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>封面广告位开关状态。</returns>
        public static AmsCoverStatusJsonResult GetCoverAdposStatus(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsCoverStatusJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_cover_adpos_status", new { }, timeOut);

        /// <summary>
        /// 异步获取授权小程序封面广告位的开关状态（GetCoverAdposStatus）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>封面广告位开关状态。</returns>
        public static Task<AmsCoverStatusJsonResult> GetCoverAdposStatusAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsCoverStatusJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_cover_adpos_status", new { }, timeOut);

        /// <summary>
        /// 获取授权小程序封面广告位的场景设置（GetCoverAdposScene）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>封面广告位场景设置。</returns>
        public static AmsCoverSceneJsonResult GetCoverAdposScene(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsCoverSceneJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_cover_adpos_scene", new { }, timeOut);

        /// <summary>
        /// 异步获取授权小程序封面广告位的场景设置（GetCoverAdposScene）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>封面广告位场景设置。</returns>
        public static Task<AmsCoverSceneJsonResult> GetCoverAdposSceneAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsCoverSceneJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_cover_adpos_scene", new { }, timeOut);

        /// <summary>
        /// 获取授权小程序的广告位或指定广告单元信息（GetAdunitList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">广告单元分页和筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>广告单元列表。</returns>
        public static AmsAdUnitListJsonResult GetAdunitList(string authorizerAccessToken,
            AmsAdUnitListRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsAdUnitListJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_adunit_list",
                request, timeOut);

        /// <summary>
        /// 异步获取授权小程序的广告位或指定广告单元信息（GetAdunitList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">广告单元分页和筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>广告单元列表。</returns>
        public static Task<AmsAdUnitListJsonResult> GetAdunitListAsync(string authorizerAccessToken,
            AmsAdUnitListRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsAdUnitListJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_adunit_list",
                request, timeOut);

        /// <summary>
        /// 获取指定广告单元的小程序组件代码（GetAdunitCode）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="adUnitId">广告单元 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>广告单元代码。</returns>
        public static AmsAdUnitCodeJsonResult GetAdunitCode(string authorizerAccessToken, string adUnitId,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsAdUnitCodeJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_adunit_code",
                new AmsAdUnitIdRequest { ad_unit_id = adUnitId }, timeOut);

        /// <summary>
        /// 异步获取指定广告单元的小程序组件代码（GetAdunitCode）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="adUnitId">广告单元 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>广告单元代码。</returns>
        public static Task<AmsAdUnitCodeJsonResult> GetAdunitCodeAsync(string authorizerAccessToken, string adUnitId,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsAdUnitCodeJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_adunit_code",
                new AmsAdUnitIdRequest { ad_unit_id = adUnitId }, timeOut);

        #endregion

        #region 广告屏蔽

        /// <summary>
        /// 获取授权小程序已屏蔽的广告主（GetBlackList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各类型已屏蔽广告主列表。</returns>
        public static AmsBlackListJsonResult GetBlackList(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsBlackListJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_black_list",
                new { }, timeOut);

        /// <summary>
        /// 异步获取授权小程序已屏蔽的广告主（GetBlackList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各类型已屏蔽广告主列表。</returns>
        public static Task<AmsBlackListJsonResult> GetBlackListAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsBlackListJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_black_list",
                new { }, timeOut);

        /// <summary>
        /// 设置或删除授权小程序屏蔽的广告主（SetBlackList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">广告主屏蔽操作参数；其中 list 必须是 JSON 数组序列化后的字符串。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static AmsJsonResult SetBlackList(string authorizerAccessToken, AmsSetBlackListRequest request,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_set_black_list",
                request, timeOut);

        /// <summary>
        /// 异步设置或删除授权小程序屏蔽的广告主（SetBlackList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">广告主屏蔽操作参数；其中 list 必须是 JSON 数组序列化后的字符串。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<AmsJsonResult> SetBlackListAsync(string authorizerAccessToken,
            AmsSetBlackListRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_set_black_list",
                request, timeOut);

        /// <summary>
        /// 获取授权小程序的行业屏蔽信息（GetAmsCategoryBlackList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已屏蔽行业信息。</returns>
        public static AmsCategoryBlackListJsonResult GetAmsCategoryBlackList(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsCategoryBlackListJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_mp_amscategory_blacklist", new { }, timeOut);

        /// <summary>
        /// 异步获取授权小程序的行业屏蔽信息（GetAmsCategoryBlackList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已屏蔽行业信息。</returns>
        public static Task<AmsCategoryBlackListJsonResult> GetAmsCategoryBlackListAsync(string authorizerAccessToken,
            int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsCategoryBlackListJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_mp_amscategory_blacklist", new { }, timeOut);

        /// <summary>
        /// 设置授权小程序的行业屏蔽信息（SetAmsCategoryBlackList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="amsCategory">以竖线分隔的行业枚举值。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static AmsJsonResult SetAmsCategoryBlackList(string authorizerAccessToken, string amsCategory,
            int timeOut = Config.TIME_OUT) =>
            Send<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_set_mp_amscategory_blacklist", new AmsCategoryBlackListRequest { ams_category = amsCategory }, timeOut);

        /// <summary>
        /// 异步设置授权小程序的行业屏蔽信息（SetAmsCategoryBlackList）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="amsCategory">以竖线分隔的行业枚举值。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<AmsJsonResult> SetAmsCategoryBlackListAsync(string authorizerAccessToken,
            string amsCategory, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_set_mp_amscategory_blacklist", new AmsCategoryBlackListRequest { ams_category = amsCategory }, timeOut);

        #endregion

        #region 广告数据

        /// <summary>
        /// 获取授权小程序的广告汇总数据（GetAdposGenenral，名称沿用官方拼写）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">日期范围、分页和广告位筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>小程序广告汇总数据。</returns>
        public static AmsAdPositionGeneralJsonResult GetAdposGenenral(string authorizerAccessToken,
            AmsAdDataRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsAdPositionGeneralJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_adpos_genenral", request, timeOut);

        /// <summary>
        /// 异步获取授权小程序的广告汇总数据（GetAdposGenenral，名称沿用官方拼写）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">日期范围、分页和广告位筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>小程序广告汇总数据。</returns>
        public static Task<AmsAdPositionGeneralJsonResult> GetAdposGenenralAsync(string authorizerAccessToken,
            AmsAdDataRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsAdPositionGeneralJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_adpos_genenral", request, timeOut);

        /// <summary>
        /// 获取授权小程序的广告单元细分数据（GetAdposDetail）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">日期范围、分页和广告单元筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>广告单元细分数据。</returns>
        public static AmsAdPositionDetailJsonResult GetAdposDetail(string authorizerAccessToken,
            AmsAdDetailDataRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsAdPositionDetailJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_adunit_general", request, timeOut);

        /// <summary>
        /// 异步获取授权小程序的广告单元细分数据（GetAdposDetail）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">日期范围、分页和广告单元筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>广告单元细分数据。</returns>
        public static Task<AmsAdPositionDetailJsonResult> GetAdposDetailAsync(string authorizerAccessToken,
            AmsAdDetailDataRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsAdPositionDetailJsonResult>(authorizerAccessToken, "/wxa/operationams",
                "agency_get_adunit_general", request, timeOut);

        /// <summary>
        /// 获取服务商代运营小程序的广告汇总数据（GetAgencyAdsStat）。
        /// </summary>
        /// <param name="authorizerAccessToken">官方文档指定的 authorizer_access_token。</param>
        /// <param name="request">日期范围、分页和广告位筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>服务商广告汇总数据。</returns>
        public static AmsAgencyAdsStatJsonResult GetAgencyAdsStat(string authorizerAccessToken,
            AmsAdDataRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsAgencyAdsStatJsonResult>(authorizerAccessToken, "/wxa/getdefaultamsinfo",
                "get_agency_ads_stat", request, timeOut);

        /// <summary>
        /// 异步获取服务商代运营小程序的广告汇总数据（GetAgencyAdsStat）。
        /// </summary>
        /// <param name="authorizerAccessToken">官方文档指定的 authorizer_access_token。</param>
        /// <param name="request">日期范围、分页和广告位筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>服务商广告汇总数据。</returns>
        public static Task<AmsAgencyAdsStatJsonResult> GetAgencyAdsStatAsync(string authorizerAccessToken,
            AmsAdDataRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsAgencyAdsStatJsonResult>(authorizerAccessToken, "/wxa/getdefaultamsinfo",
                "get_agency_ads_stat", request, timeOut);

        /// <summary>
        /// 获取服务商代运营小程序的广告单元明细数据（GetAgencyAdsDetail）。
        /// </summary>
        /// <param name="authorizerAccessToken">官方文档指定的 authorizer_access_token。</param>
        /// <param name="request">日期范围、分页和广告位筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>服务商广告单元明细数据。</returns>
        public static AmsAgencyAdsDetailJsonResult GetAgencyAdsDetail(string authorizerAccessToken,
            AmsAdDataRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsAgencyAdsDetailJsonResult>(authorizerAccessToken, "/wxa/getdefaultamsinfo",
                "get_agency_ads_detail", request, timeOut);

        /// <summary>
        /// 异步获取服务商代运营小程序的广告单元明细数据（GetAgencyAdsDetail）。
        /// </summary>
        /// <param name="authorizerAccessToken">官方文档指定的 authorizer_access_token。</param>
        /// <param name="request">日期范围、分页和广告位筛选参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>服务商广告单元明细数据。</returns>
        public static Task<AmsAgencyAdsDetailJsonResult> GetAgencyAdsDetailAsync(string authorizerAccessToken,
            AmsAdDataRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsAgencyAdsDetailJsonResult>(authorizerAccessToken, "/wxa/getdefaultamsinfo",
                "get_agency_ads_detail", request, timeOut);

        #endregion

        #region 结算数据

        /// <summary>
        /// 获取授权小程序的结算收入数据（GetSettlement）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">日期范围和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>小程序结算收入数据。</returns>
        public static AmsSettlementJsonResult GetSettlement(string authorizerAccessToken,
            AmsSettlementRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsSettlementJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_settlement",
                request, timeOut);

        /// <summary>
        /// 异步获取授权小程序的结算收入数据（GetSettlement）。
        /// </summary>
        /// <param name="authorizerAccessToken">授权小程序的 authorizer_access_token。</param>
        /// <param name="request">日期范围和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>小程序结算收入数据。</returns>
        public static Task<AmsSettlementJsonResult> GetSettlementAsync(string authorizerAccessToken,
            AmsSettlementRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsSettlementJsonResult>(authorizerAccessToken, "/wxa/operationams", "agency_get_settlement",
                request, timeOut);

        /// <summary>
        /// 获取服务商结算收入数据（GetAgencySettlement）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">日期范围和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>服务商结算收入数据。</returns>
        public static AmsSettlementJsonResult GetAgencySettlement(string componentAccessToken,
            AmsSettlementRequest request, int timeOut = Config.TIME_OUT) =>
            Send<AmsSettlementJsonResult>(componentAccessToken, "/wxa/getdefaultamsinfo",
                "get_agency_settled_revenue", request, timeOut);

        /// <summary>
        /// 异步获取服务商结算收入数据（GetAgencySettlement）。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台的 component_access_token。</param>
        /// <param name="request">日期范围和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>服务商结算收入数据。</returns>
        public static Task<AmsSettlementJsonResult> GetAgencySettlementAsync(string componentAccessToken,
            AmsSettlementRequest request, int timeOut = Config.TIME_OUT) =>
            SendAsync<AmsSettlementJsonResult>(componentAccessToken, "/wxa/getdefaultamsinfo",
                "get_agency_settled_revenue", request, timeOut);

        #endregion

        /// <summary>
        /// 构造流量主代运营接口地址。
        /// </summary>
        private static string BuildUrl(string accessToken, string path, string action)
        {
            return $"{Config.ApiMpHost}{path}?action={action.AsUrlData()}&access_token={accessToken.AsUrlData()}";
        }

        /// <summary>
        /// 同步发送流量主代运营请求。
        /// </summary>
        private static T Send<T>(string accessToken, string path, string action, object request, int timeOut)
        {
            return CommonJsonSend.Send<T>(null, BuildUrl(accessToken, path, action), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>
        /// 异步发送流量主代运营请求。
        /// </summary>
        private static Task<T> SendAsync<T>(string accessToken, string path, string action, object request, int timeOut)
        {
            return CommonJsonSend.SendAsync<T>(null, BuildUrl(accessToken, path, action), request ?? new { },
                CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }
    }
}
