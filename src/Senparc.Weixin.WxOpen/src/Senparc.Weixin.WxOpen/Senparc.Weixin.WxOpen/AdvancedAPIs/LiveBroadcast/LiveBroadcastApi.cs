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

    文件名：LiveBroadcastApi.cs
    文件功能描述：LiveBroadcastApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.LiveBroadcast
{
    /// <summary>小程序直播间、商品、成员及长期订阅服务端接口。</summary>
    /// <remarks>官方目录共 38 个请求路径，均支持第三方平台使用 authorizer_access_token 代调用，权限集 ID 为 52。</remarks>
    public static partial class LiveBroadcastApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 直播间基础管理

        /// <summary>创建小程序直播间。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间图片、时间、主播、类型和功能开关。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>直播间 ID 和小程序码 URL。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_createroom.html"/>。</remarks>
        public static LiveBroadcastCreateRoomJsonResult CreateRoom(string accessTokenOrAppId, LiveBroadcastCreateRoomRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastCreateRoomJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/create", request, timeOut);
        }

        /// <summary>异步创建小程序直播间。</summary>
        /// <inheritdoc cref="CreateRoom"/>
        public static Task<LiveBroadcastCreateRoomJsonResult> CreateRoomAsync(string accessTokenOrAppId, LiveBroadcastCreateRoomRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastCreateRoomJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/create", request, timeOut);
        }

        /// <summary>获取直播间列表或指定直播间回放。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">分页条件，或 action=get_replay 与直播间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>直播间列表或回放列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_getliveinfo.html"/>。</remarks>
        public static LiveBroadcastGetLiveInfoJsonResult GetLiveInfo(string accessTokenOrAppId, LiveBroadcastGetLiveInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastGetLiveInfoJsonResult>(accessTokenOrAppId, "/wxa/business/getliveinfo", request, timeOut);
        }

        /// <summary>异步获取直播间列表或指定直播间回放。</summary>
        /// <inheritdoc cref="GetLiveInfo"/>
        public static Task<LiveBroadcastGetLiveInfoJsonResult> GetLiveInfoAsync(string accessTokenOrAppId, LiveBroadcastGetLiveInfoRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastGetLiveInfoJsonResult>(accessTokenOrAppId, "/wxa/business/getliveinfo", request, timeOut);
        }

        /// <summary>删除直播间。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 id。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_deleteroom.html"/>。</remarks>
        public static WxJsonResult DeleteRoom(string accessTokenOrAppId, LiveBroadcastIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/deleteroom", request, timeOut);
        }

        /// <summary>异步删除直播间。</summary>
        /// <inheritdoc cref="DeleteRoom"/>
        public static Task<WxJsonResult> DeleteRoomAsync(string accessTokenOrAppId, LiveBroadcastIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/deleteroom", request, timeOut);
        }

        /// <summary>向直播间导入一个或多个商品。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和商品 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>导入结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_importgoods.html"/>。</remarks>
        public static WxJsonResult ImportGoods(string accessTokenOrAppId, LiveBroadcastImportGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/addgoods", request, timeOut);
        }

        /// <summary>异步向直播间导入一个或多个商品。</summary>
        /// <inheritdoc cref="ImportGoods"/>
        public static Task<WxJsonResult> ImportGoodsAsync(string accessTokenOrAppId, LiveBroadcastImportGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/addgoods", request, timeOut);
        }

        /// <summary>编辑直播间基础信息和功能开关。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 id 和完整编辑信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>编辑结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_editroom.html"/>。</remarks>
        public static WxJsonResult EditRoom(string accessTokenOrAppId, LiveBroadcastEditRoomRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/editroom", request, timeOut);
        }

        /// <summary>异步编辑直播间基础信息和功能开关。</summary>
        /// <inheritdoc cref="EditRoom"/>
        public static Task<WxJsonResult> EditRoomAsync(string accessTokenOrAppId, LiveBroadcastEditRoomRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/editroom", request, timeOut);
        }

        /// <summary>获取直播间推流地址。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="roomId">直播间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>RTMP 推流地址。</returns>
        /// <remarks>本接口使用 GET 查询参数。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_getpushurl.html"/>。</remarks>
        public static LiveBroadcastPushUrlJsonResult GetPushUrl(string accessTokenOrAppId, long roomId, int timeOut = Config.TIME_OUT)
        {
            return SendGet<LiveBroadcastPushUrlJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/getpushurl", Query("roomId", roomId), timeOut);
        }

        /// <summary>异步获取直播间推流地址。</summary>
        /// <inheritdoc cref="GetPushUrl"/>
        public static Task<LiveBroadcastPushUrlJsonResult> GetPushUrlAsync(string accessTokenOrAppId, long roomId, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<LiveBroadcastPushUrlJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/getpushurl", Query("roomId", roomId), timeOut);
        }

        /// <summary>获取直播间分享二维码、路径和海报。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="roomId">直播间 ID。</param>
        /// <param name="customParams">可选自定义参数 JSON，SDK 会进行 URL 编码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分享二维码、路径和海报 URL。</returns>
        /// <remarks>本接口使用 GET 查询参数。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_getsharedcode.html"/>。</remarks>
        public static LiveBroadcastSharedCodeJsonResult GetSharedCode(string accessTokenOrAppId, long roomId, string customParams = null, int timeOut = Config.TIME_OUT)
        {
            return SendGet<LiveBroadcastSharedCodeJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/getsharedcode",
                Query("roomId", roomId) + Query("params", customParams), timeOut);
        }

        /// <summary>异步获取直播间分享二维码、路径和海报。</summary>
        /// <inheritdoc cref="GetSharedCode"/>
        public static Task<LiveBroadcastSharedCodeJsonResult> GetSharedCodeAsync(string accessTokenOrAppId, long roomId, string customParams = null, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<LiveBroadcastSharedCodeJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/getsharedcode",
                Query("roomId", roomId) + Query("params", customParams), timeOut);
        }

        #endregion

        #region 主播副号

        /// <summary>获取直播间主播副号。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="roomId">直播间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>主播副号微信号。</returns>
        /// <remarks>本接口使用 GET 查询参数。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_getsubanchor.html"/>。</remarks>
        public static LiveBroadcastSubAnchorJsonResult GetSubAnchor(string accessTokenOrAppId, long roomId, int timeOut = Config.TIME_OUT)
        {
            return SendGet<LiveBroadcastSubAnchorJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/getsubanchor", Query("roomId", roomId), timeOut);
        }

        /// <summary>异步获取直播间主播副号。</summary>
        /// <inheritdoc cref="GetSubAnchor"/>
        public static Task<LiveBroadcastSubAnchorJsonResult> GetSubAnchorAsync(string accessTokenOrAppId, long roomId, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<LiveBroadcastSubAnchorJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/getsubanchor", Query("roomId", roomId), timeOut);
        }

        /// <summary>修改直播间主播副号。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和副号微信号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>修改结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_modifysubanchor.html"/>。</remarks>
        public static WxJsonResult ModifySubAnchor(string accessTokenOrAppId, LiveBroadcastRoomUserRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/modifysubanchor", request, timeOut);
        }

        /// <summary>异步修改直播间主播副号。</summary>
        /// <inheritdoc cref="ModifySubAnchor"/>
        public static Task<WxJsonResult> ModifySubAnchorAsync(string accessTokenOrAppId, LiveBroadcastRoomUserRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/modifysubanchor", request, timeOut);
        }

        /// <summary>删除直播间主播副号。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_deletesubanchor.html"/>。</remarks>
        public static WxJsonResult DeleteSubAnchor(string accessTokenOrAppId, LiveBroadcastRoomIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/deletesubanchor", request, timeOut);
        }

        /// <summary>异步删除直播间主播副号。</summary>
        /// <inheritdoc cref="DeleteSubAnchor"/>
        public static Task<WxJsonResult> DeleteSubAnchorAsync(string accessTokenOrAppId, LiveBroadcastRoomIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/deletesubanchor", request, timeOut);
        }

        /// <summary>添加直播间主播副号。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和副号微信号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>添加结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_addsubanchor.html"/>。</remarks>
        public static WxJsonResult AddSubAnchor(string accessTokenOrAppId, LiveBroadcastRoomUserRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/addsubanchor", request, timeOut);
        }

        /// <summary>异步添加直播间主播副号。</summary>
        /// <inheritdoc cref="AddSubAnchor"/>
        public static Task<WxJsonResult> AddSubAnchorAsync(string accessTokenOrAppId, LiveBroadcastRoomUserRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/addsubanchor", request, timeOut);
        }

        #endregion

        #region 直播间商品操作

        /// <summary>从直播间删除商品。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和商品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>请求路径中的 deleteInRoom 大小写遵循官方协议。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_deletedoods.html"/>。</remarks>
        public static WxJsonResult DeleteRoomGoods(string accessTokenOrAppId, LiveBroadcastRoomGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/deleteInRoom", request, timeOut);
        }

        /// <summary>异步从直播间删除商品。</summary>
        /// <inheritdoc cref="DeleteRoomGoods"/>
        public static Task<WxJsonResult> DeleteRoomGoodsAsync(string accessTokenOrAppId, LiveBroadcastRoomGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/deleteInRoom", request, timeOut);
        }

        /// <summary>将商品推送到直播间当前展示位。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和商品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>推送结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_pushgoods.html"/>。</remarks>
        public static WxJsonResult PushGoods(string accessTokenOrAppId, LiveBroadcastRoomGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/push", request, timeOut);
        }

        /// <summary>异步将商品推送到直播间当前展示位。</summary>
        /// <inheritdoc cref="PushGoods"/>
        public static Task<WxJsonResult> PushGoodsAsync(string accessTokenOrAppId, LiveBroadcastRoomGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/push", request, timeOut);
        }

        /// <summary>设置直播间商品上下架状态。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间、商品和上下架状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_salegoods.html"/>。</remarks>
        public static WxJsonResult SetGoodsOnSale(string accessTokenOrAppId, LiveBroadcastGoodsOnSaleRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/onsale", request, timeOut);
        }

        /// <summary>异步设置直播间商品上下架状态。</summary>
        /// <inheritdoc cref="SetGoodsOnSale"/>
        public static Task<WxJsonResult> SetGoodsOnSaleAsync(string accessTokenOrAppId, LiveBroadcastGoodsOnSaleRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/onsale", request, timeOut);
        }

        /// <summary>调整直播间商品顺序。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和有序商品列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>排序结果。</returns>
        /// <remarks>官方参数表将 goodsId 定义为 number，但示例同时出现字符串。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_sortgoods.html"/>。</remarks>
        public static WxJsonResult SortRoomGoods(string accessTokenOrAppId, LiveBroadcastSortGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/sort", request, timeOut);
        }

        /// <summary>异步调整直播间商品顺序。</summary>
        /// <inheritdoc cref="SortRoomGoods"/>
        public static Task<WxJsonResult> SortRoomGoodsAsync(string accessTokenOrAppId, LiveBroadcastSortGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/sort", request, timeOut);
        }

        #endregion

        #region 小助手和功能开关

        /// <summary>修改直播间小助手信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID、微信号和昵称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>修改结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_modifyassistant.html"/>。</remarks>
        public static WxJsonResult ModifyAssistant(string accessTokenOrAppId, LiveBroadcastModifyAssistantRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/modifyassistant", request, timeOut);
        }

        /// <summary>异步修改直播间小助手信息。</summary>
        /// <inheritdoc cref="ModifyAssistant"/>
        public static Task<WxJsonResult> ModifyAssistantAsync(string accessTokenOrAppId, LiveBroadcastModifyAssistantRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/modifyassistant", request, timeOut);
        }

        /// <summary>查询直播间小助手列表。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="roomId">直播间 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>小助手列表和数量上限。</returns>
        /// <remarks>本接口使用 GET 查询参数。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_getassistantlist.html"/>。</remarks>
        public static LiveBroadcastAssistantListJsonResult GetAssistantList(string accessTokenOrAppId, long roomId, int timeOut = Config.TIME_OUT)
        {
            return SendGet<LiveBroadcastAssistantListJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/getassistantlist", Query("roomId", roomId), timeOut);
        }

        /// <summary>异步查询直播间小助手列表。</summary>
        /// <inheritdoc cref="GetAssistantList"/>
        public static Task<LiveBroadcastAssistantListJsonResult> GetAssistantListAsync(string accessTokenOrAppId, long roomId, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<LiveBroadcastAssistantListJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/getassistantlist", Query("roomId", roomId), timeOut);
        }

        /// <summary>删除直播间小助手。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和用户微信号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_removeassistant.html"/>。</remarks>
        public static WxJsonResult RemoveAssistant(string accessTokenOrAppId, LiveBroadcastRoomUserRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/removeassistant", request, timeOut);
        }

        /// <summary>异步删除直播间小助手。</summary>
        /// <inheritdoc cref="RemoveAssistant"/>
        public static Task<WxJsonResult> RemoveAssistantAsync(string accessTokenOrAppId, LiveBroadcastRoomUserRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/removeassistant", request, timeOut);
        }

        /// <summary>批量添加直播间小助手。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和小助手用户列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>添加结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_addveassistant.html"/>。</remarks>
        public static WxJsonResult AddAssistants(string accessTokenOrAppId, LiveBroadcastAddAssistantsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/addassistant", request, timeOut);
        }

        /// <summary>异步批量添加直播间小助手。</summary>
        /// <inheritdoc cref="AddAssistants"/>
        public static Task<WxJsonResult> AddAssistantsAsync(string accessTokenOrAppId, LiveBroadcastAddAssistantsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/addassistant", request, timeOut);
        }

        /// <summary>开启或关闭直播间全局禁言。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 id 和禁言状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>官方参数表使用 id，示例误写 roomId，本模型遵循参数表。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_updatecomment.html"/>。</remarks>
        public static WxJsonResult UpdateComment(string accessTokenOrAppId, LiveBroadcastUpdateCommentRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/updatecomment", request, timeOut);
        }

        /// <summary>异步开启或关闭直播间全局禁言。</summary>
        /// <inheritdoc cref="UpdateComment"/>
        public static Task<WxJsonResult> UpdateCommentAsync(string accessTokenOrAppId, LiveBroadcastUpdateCommentRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/updatecomment", request, timeOut);
        }

        /// <summary>开启或关闭直播间官方收录。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和官方收录状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_updatefeedpublic.html"/>。</remarks>
        public static WxJsonResult UpdateFeedPublic(string accessTokenOrAppId, LiveBroadcastUpdateFeedPublicRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/updatefeedpublic", request, timeOut);
        }

        /// <summary>异步开启或关闭直播间官方收录。</summary>
        /// <inheritdoc cref="UpdateFeedPublic"/>
        public static Task<WxJsonResult> UpdateFeedPublicAsync(string accessTokenOrAppId, LiveBroadcastUpdateFeedPublicRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/updatefeedpublic", request, timeOut);
        }

        /// <summary>开启或关闭直播间客服功能。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和客服关闭状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_updatekf.html"/>。</remarks>
        public static WxJsonResult UpdateCustomerService(string accessTokenOrAppId, LiveBroadcastUpdateCustomerServiceRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/updatekf", request, timeOut);
        }

        /// <summary>异步开启或关闭直播间客服功能。</summary>
        /// <inheritdoc cref="UpdateCustomerService"/>
        public static Task<WxJsonResult> UpdateCustomerServiceAsync(string accessTokenOrAppId, LiveBroadcastUpdateCustomerServiceRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/updatekf", request, timeOut);
        }

        /// <summary>开启或关闭直播间回放功能。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和回放关闭状态。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_updatereplay.html"/>。</remarks>
        public static WxJsonResult UpdateReplay(string accessTokenOrAppId, LiveBroadcastUpdateReplayRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/updatereplay", request, timeOut);
        }

        /// <summary>异步开启或关闭直播间回放功能。</summary>
        /// <inheritdoc cref="UpdateReplay"/>
        public static Task<WxJsonResult> UpdateReplayAsync(string accessTokenOrAppId, LiveBroadcastUpdateReplayRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/room/updatereplay", request, timeOut);
        }

        /// <summary>获取商品讲解视频下载链接。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和商品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商品讲解视频 URL。</returns>
        /// <remarks>请求路径中的 getVideo 大小写遵循官方协议。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_downloadgoodsvideo.html"/>。</remarks>
        public static LiveBroadcastGoodsVideoJsonResult GetGoodsVideo(string accessTokenOrAppId, LiveBroadcastRoomGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastGoodsVideoJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/getVideo", request, timeOut);
        }

        /// <summary>异步获取商品讲解视频下载链接。</summary>
        /// <inheritdoc cref="GetGoodsVideo"/>
        public static Task<LiveBroadcastGoodsVideoJsonResult> GetGoodsVideoAsync(string accessTokenOrAppId, LiveBroadcastRoomGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastGoodsVideoJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/getVideo", request, timeOut);
        }

        /// <summary>设置直播挂件全局商品 Key。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">全局商品 Key 字段列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果。</returns>
        /// <remarks>官方建议全局 Key 只设置一次，修改可能使既有映射失效。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_setdefault_goodskey.html"/>。</remarks>
        public static WxJsonResult SetDefaultGoodsKey(string accessTokenOrAppId, LiveBroadcastSetGoodsKeyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/setkey", request, timeOut);
        }

        /// <summary>异步设置直播挂件全局商品 Key。</summary>
        /// <inheritdoc cref="SetDefaultGoodsKey"/>
        public static Task<WxJsonResult> SetDefaultGoodsKeyAsync(string accessTokenOrAppId, LiveBroadcastSetGoodsKeyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/setkey", request, timeOut);
        }

        /// <summary>获取直播挂件全局商品 Key。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>当前全局商品 Key 列表。</returns>
        /// <remarks>本接口使用 GET 且无业务查询参数。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/studio-management/api_getdefault_goodskey.html"/>。</remarks>
        public static LiveBroadcastGoodsKeyJsonResult GetDefaultGoodsKey(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendGet<LiveBroadcastGoodsKeyJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/getkey", string.Empty, timeOut);
        }

        /// <summary>异步获取直播挂件全局商品 Key。</summary>
        /// <inheritdoc cref="GetDefaultGoodsKey"/>
        public static Task<LiveBroadcastGoodsKeyJsonResult> GetDefaultGoodsKeyAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<LiveBroadcastGoodsKeyJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/getkey", string.Empty, timeOut);
        }

        #endregion

        private static string Query(string name, object value)
        {
            return value == null ? string.Empty : "&" + name + "=" + value.ToString().AsUrlData();
        }

        private static T SendGet<T>(string accessTokenOrAppId, string path, string query, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}" + query,
                    null, CommonJsonSendType.GET, timeOut: timeOut), accessTokenOrAppId);
        }

        private static Task<T> SendGetAsync<T>(string accessTokenOrAppId, string path, string query, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}" + query,
                    null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false), accessTokenOrAppId);
        }

        private static T SendPost<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", request ?? new { },
                    CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting), accessTokenOrAppId);
        }

        private static Task<T> SendPostAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", request ?? new { },
                    CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false), accessTokenOrAppId);
        }
    }
}
