/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Layout.cs
    文件功能描述：企业微信会议布局、高级布局和背景图管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议布局、高级布局和背景图管理接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string GetMeetingLayoutTemplatesPath = "/cgi-bin/meeting/layout/list_template";
        private const string AddMeetingLayoutsPath = "/cgi-bin/meeting/layout/add";
        private const string UpdateMeetingLayoutPath = "/cgi-bin/meeting/layout/update";
        private const string SetDefaultMeetingLayoutPath = "/cgi-bin/meeting/layout/set_default";
        private const string AddMeetingAdvancedLayoutsPath = "/cgi-bin/meeting/advanced_layout/add";
        private const string UpdateMeetingAdvancedLayoutPath = "/cgi-bin/meeting/advanced_layout/update";
        private const string ApplyMeetingAdvancedLayoutPath = "/cgi-bin/meeting/advanced_layout/apply";
        private const string GetMeetingAdvancedLayoutsPath = "/cgi-bin/meeting/advanced_layout/list";
        private const string GetMeetingUserLayoutPath = "/cgi-bin/meeting/advanced_layout/get_user_layout";
        private const string DeleteMeetingAdvancedLayoutsPath = "/cgi-bin/meeting/advanced_layout/batch_delete";
        private const string AddMeetingLayoutBackgroundsPath = "/cgi-bin/meeting/layout/add_background";
        private const string SetDefaultMeetingLayoutBackgroundPath =
            "/cgi-bin/meeting/layout/set_default_background";
        private const string GetMeetingLayoutBackgroundsPath = "/cgi-bin/meeting/layout/list_background";
        private const string DeleteMeetingLayoutBackgroundPath = "/cgi-bin/meeting/layout/delete_background";
        private const string DeleteMeetingLayoutBackgroundsPath =
            "/cgi-bin/meeting/layout/batch_delete_background";

        /// <summary>
        /// 获取企业微信会议支持的布局模板。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98844"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>布局模板、预览图和渲染规则列表。</returns>
        public static GetMeetingLayoutTemplatesResult GetMeetingLayoutTemplates(
            string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
            => Get<GetMeetingLayoutTemplatesResult>(accessTokenOrAppKey,
                GetMeetingLayoutTemplatesPath, timeOut);

        /// <summary>
        /// 异步获取企业微信会议支持的布局模板。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98844"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>布局模板、预览图和渲染规则列表。</returns>
        public static Task<GetMeetingLayoutTemplatesResult> GetMeetingLayoutTemplatesAsync(
            string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
            => GetAsync<GetMeetingLayoutTemplatesResult>(accessTokenOrAppKey,
                GetMeetingLayoutTemplatesPath, timeOut);

        /// <summary>
        /// 为企业微信会议添加一个或多个基础布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98845"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、布局页面和可选的默认布局序号。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的布局列表和当前默认布局 ID。</returns>
        public static AddMeetingLayoutsResult AddMeetingLayouts(string accessTokenOrAppKey,
            AddMeetingLayoutsRequest request, int timeOut = Config.TIME_OUT)
            => Post<AddMeetingLayoutsResult>(accessTokenOrAppKey,
                AddMeetingLayoutsPath, request, timeOut);

        /// <summary>
        /// 异步为企业微信会议添加一个或多个基础布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98845"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、布局页面和可选的默认布局序号。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的布局列表和当前默认布局 ID。</returns>
        public static Task<AddMeetingLayoutsResult> AddMeetingLayoutsAsync(
            string accessTokenOrAppKey, AddMeetingLayoutsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<AddMeetingLayoutsResult>(accessTokenOrAppKey,
                AddMeetingLayoutsPath, request, timeOut);

        /// <summary>
        /// 更新企业微信会议的基础布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98846"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、布局 ID、页面列表和默认布局开关。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>基础布局更新结果。</returns>
        public static UpdateMeetingLayoutResult UpdateMeetingLayout(string accessTokenOrAppKey,
            UpdateMeetingLayoutRequest request, int timeOut = Config.TIME_OUT)
            => Post<UpdateMeetingLayoutResult>(accessTokenOrAppKey,
                UpdateMeetingLayoutPath, request, timeOut);

        /// <summary>
        /// 异步更新企业微信会议的基础布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98846"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、布局 ID、页面列表和默认布局开关。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>基础布局更新结果。</returns>
        public static Task<UpdateMeetingLayoutResult> UpdateMeetingLayoutAsync(
            string accessTokenOrAppKey, UpdateMeetingLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<UpdateMeetingLayoutResult>(accessTokenOrAppKey,
                UpdateMeetingLayoutPath, request, timeOut);

        /// <summary>
        /// 设置企业微信会议的默认基础布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98847"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要应用的布局 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>默认布局设置结果。</returns>
        public static SetDefaultMeetingLayoutResult SetDefaultMeetingLayout(
            string accessTokenOrAppKey, SetDefaultMeetingLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SetDefaultMeetingLayoutResult>(accessTokenOrAppKey,
                SetDefaultMeetingLayoutPath, request, timeOut);

        /// <summary>
        /// 异步设置企业微信会议的默认基础布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98847"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要应用的布局 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>默认布局设置结果。</returns>
        public static Task<SetDefaultMeetingLayoutResult> SetDefaultMeetingLayoutAsync(
            string accessTokenOrAppKey, SetDefaultMeetingLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SetDefaultMeetingLayoutResult>(accessTokenOrAppKey,
                SetDefaultMeetingLayoutPath, request, timeOut);

        /// <summary>
        /// 为企业微信会议添加一个或多个高级布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98861"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 及包含轮询和多成员座次的高级布局。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的高级布局列表。</returns>
        public static AddMeetingAdvancedLayoutsResult AddMeetingAdvancedLayouts(
            string accessTokenOrAppKey, AddMeetingAdvancedLayoutsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<AddMeetingAdvancedLayoutsResult>(accessTokenOrAppKey,
                AddMeetingAdvancedLayoutsPath, request, timeOut);

        /// <summary>
        /// 异步为企业微信会议添加一个或多个高级布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98861"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 及包含轮询和多成员座次的高级布局。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的高级布局列表。</returns>
        public static Task<AddMeetingAdvancedLayoutsResult> AddMeetingAdvancedLayoutsAsync(
            string accessTokenOrAppKey, AddMeetingAdvancedLayoutsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<AddMeetingAdvancedLayoutsResult>(accessTokenOrAppKey,
                AddMeetingAdvancedLayoutsPath, request, timeOut);

        /// <summary>
        /// 更新企业微信会议的高级布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98868"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、布局 ID、布局名称和页面列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级布局更新结果。</returns>
        public static UpdateMeetingAdvancedLayoutResult UpdateMeetingAdvancedLayout(
            string accessTokenOrAppKey, UpdateMeetingAdvancedLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<UpdateMeetingAdvancedLayoutResult>(accessTokenOrAppKey,
                UpdateMeetingAdvancedLayoutPath, request, timeOut);

        /// <summary>
        /// 异步更新企业微信会议的高级布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98868"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、布局 ID、布局名称和页面列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级布局更新结果。</returns>
        public static Task<UpdateMeetingAdvancedLayoutResult> UpdateMeetingAdvancedLayoutAsync(
            string accessTokenOrAppKey, UpdateMeetingAdvancedLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<UpdateMeetingAdvancedLayoutResult>(accessTokenOrAppKey,
                UpdateMeetingAdvancedLayoutPath, request, timeOut);

        /// <summary>
        /// 向指定成员应用企业微信会议高级布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98869"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、布局 ID 和成员临时 OpenId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级布局应用结果。</returns>
        public static ApplyMeetingAdvancedLayoutResult ApplyMeetingAdvancedLayout(
            string accessTokenOrAppKey, ApplyMeetingAdvancedLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ApplyMeetingAdvancedLayoutResult>(accessTokenOrAppKey,
                ApplyMeetingAdvancedLayoutPath, request, timeOut);

        /// <summary>
        /// 异步向指定成员应用企业微信会议高级布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98869"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、布局 ID 和成员临时 OpenId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级布局应用结果。</returns>
        public static Task<ApplyMeetingAdvancedLayoutResult> ApplyMeetingAdvancedLayoutAsync(
            string accessTokenOrAppKey, ApplyMeetingAdvancedLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ApplyMeetingAdvancedLayoutResult>(accessTokenOrAppKey,
                ApplyMeetingAdvancedLayoutPath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议的高级布局列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98862"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级布局列表和当前默认布局 ID。</returns>
        public static GetMeetingAdvancedLayoutsResult GetMeetingAdvancedLayouts(
            string accessTokenOrAppKey, GetMeetingAdvancedLayoutsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingAdvancedLayoutsResult>(accessTokenOrAppKey,
                GetMeetingAdvancedLayoutsPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议的高级布局列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98862"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级布局列表和当前默认布局 ID。</returns>
        public static Task<GetMeetingAdvancedLayoutsResult> GetMeetingAdvancedLayoutsAsync(
            string accessTokenOrAppKey, GetMeetingAdvancedLayoutsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingAdvancedLayoutsResult>(accessTokenOrAppKey,
                GetMeetingAdvancedLayoutsPath, request, timeOut);

        /// <summary>
        /// 获取指定成员终端当前使用的企业微信会议布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98865"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、成员临时 OpenId 和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员当前布局 ID、类型、名称和页面内容。</returns>
        public static GetMeetingUserLayoutResult GetMeetingUserLayout(string accessTokenOrAppKey,
            GetMeetingUserLayoutRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingUserLayoutResult>(accessTokenOrAppKey,
                GetMeetingUserLayoutPath, request, timeOut);

        /// <summary>
        /// 异步获取指定成员终端当前使用的企业微信会议布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98865"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、成员临时 OpenId 和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员当前布局 ID、类型、名称和页面内容。</returns>
        public static Task<GetMeetingUserLayoutResult> GetMeetingUserLayoutAsync(
            string accessTokenOrAppKey, GetMeetingUserLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingUserLayoutResult>(accessTokenOrAppKey,
                GetMeetingUserLayoutPath, request, timeOut);

        /// <summary>
        /// 批量删除企业微信会议的高级布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98866"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要删除的布局 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级布局批量删除结果。</returns>
        public static DeleteMeetingAdvancedLayoutsResult DeleteMeetingAdvancedLayouts(
            string accessTokenOrAppKey, DeleteMeetingAdvancedLayoutsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<DeleteMeetingAdvancedLayoutsResult>(accessTokenOrAppKey,
                DeleteMeetingAdvancedLayoutsPath, request, timeOut);

        /// <summary>
        /// 异步批量删除企业微信会议的高级布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98866"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要删除的布局 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级布局批量删除结果。</returns>
        public static Task<DeleteMeetingAdvancedLayoutsResult> DeleteMeetingAdvancedLayoutsAsync(
            string accessTokenOrAppKey, DeleteMeetingAdvancedLayoutsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeleteMeetingAdvancedLayoutsResult>(accessTokenOrAppKey,
                DeleteMeetingAdvancedLayoutsPath, request, timeOut);

        /// <summary>
        /// 为企业微信会议添加一个或多个背景图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98851"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、背景图片地址、MD5 和可选默认图片序号。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的背景图列表和当前默认背景图 ID。</returns>
        public static AddMeetingLayoutBackgroundsResult AddMeetingLayoutBackgrounds(
            string accessTokenOrAppKey, AddMeetingLayoutBackgroundsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<AddMeetingLayoutBackgroundsResult>(accessTokenOrAppKey,
                AddMeetingLayoutBackgroundsPath, request, timeOut);

        /// <summary>
        /// 异步为企业微信会议添加一个或多个背景图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98851"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、背景图片地址、MD5 和可选默认图片序号。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的背景图列表和当前默认背景图 ID。</returns>
        public static Task<AddMeetingLayoutBackgroundsResult> AddMeetingLayoutBackgroundsAsync(
            string accessTokenOrAppKey, AddMeetingLayoutBackgroundsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<AddMeetingLayoutBackgroundsResult>(accessTokenOrAppKey,
                AddMeetingLayoutBackgroundsPath, request, timeOut);

        /// <summary>
        /// 设置企业微信会议的默认背景图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98852"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要应用的背景图 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>默认背景图设置结果。</returns>
        public static SetDefaultMeetingLayoutBackgroundResult SetDefaultMeetingLayoutBackground(
            string accessTokenOrAppKey, SetDefaultMeetingLayoutBackgroundRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SetDefaultMeetingLayoutBackgroundResult>(accessTokenOrAppKey,
                SetDefaultMeetingLayoutBackgroundPath, request, timeOut);

        /// <summary>
        /// 异步设置企业微信会议的默认背景图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98852"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要应用的背景图 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>默认背景图设置结果。</returns>
        public static Task<SetDefaultMeetingLayoutBackgroundResult>
            SetDefaultMeetingLayoutBackgroundAsync(string accessTokenOrAppKey,
                SetDefaultMeetingLayoutBackgroundRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SetDefaultMeetingLayoutBackgroundResult>(accessTokenOrAppKey,
                SetDefaultMeetingLayoutBackgroundPath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议的背景图列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98856"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>背景图列表和当前默认背景图 ID。</returns>
        public static GetMeetingLayoutBackgroundsResult GetMeetingLayoutBackgrounds(
            string accessTokenOrAppKey, GetMeetingLayoutBackgroundsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingLayoutBackgroundsResult>(accessTokenOrAppKey,
                GetMeetingLayoutBackgroundsPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议的背景图列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98856"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>背景图列表和当前默认背景图 ID。</returns>
        public static Task<GetMeetingLayoutBackgroundsResult> GetMeetingLayoutBackgroundsAsync(
            string accessTokenOrAppKey, GetMeetingLayoutBackgroundsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingLayoutBackgroundsResult>(accessTokenOrAppKey,
                GetMeetingLayoutBackgroundsPath, request, timeOut);

        /// <summary>
        /// 删除企业微信会议的指定背景图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98853"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要删除的背景图 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>背景图删除结果。</returns>
        public static DeleteMeetingLayoutBackgroundResult DeleteMeetingLayoutBackground(
            string accessTokenOrAppKey, DeleteMeetingLayoutBackgroundRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<DeleteMeetingLayoutBackgroundResult>(accessTokenOrAppKey,
                DeleteMeetingLayoutBackgroundPath, request, timeOut);

        /// <summary>
        /// 异步删除企业微信会议的指定背景图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98853"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要删除的背景图 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>背景图删除结果。</returns>
        public static Task<DeleteMeetingLayoutBackgroundResult> DeleteMeetingLayoutBackgroundAsync(
            string accessTokenOrAppKey, DeleteMeetingLayoutBackgroundRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeleteMeetingLayoutBackgroundResult>(accessTokenOrAppKey,
                DeleteMeetingLayoutBackgroundPath, request, timeOut);

        /// <summary>
        /// 批量删除企业微信会议的背景图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98854"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要删除的背景图 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>背景图批量删除结果。</returns>
        public static DeleteMeetingLayoutBackgroundsResult DeleteMeetingLayoutBackgrounds(
            string accessTokenOrAppKey, DeleteMeetingLayoutBackgroundsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<DeleteMeetingLayoutBackgroundsResult>(accessTokenOrAppKey,
                DeleteMeetingLayoutBackgroundsPath, request, timeOut);

        /// <summary>
        /// 异步批量删除企业微信会议的背景图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98854"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要删除的背景图 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>背景图批量删除结果。</returns>
        public static Task<DeleteMeetingLayoutBackgroundsResult> DeleteMeetingLayoutBackgroundsAsync(
            string accessTokenOrAppKey, DeleteMeetingLayoutBackgroundsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeleteMeetingLayoutBackgroundsResult>(accessTokenOrAppKey,
                DeleteMeetingLayoutBackgroundsPath, request, timeOut);

        private static T Get<T>(string accessTokenOrAppKey, string path, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", null, CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

        private static Task<T> GetAsync<T>(string accessTokenOrAppKey, string path, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", null, CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);
    }
}
