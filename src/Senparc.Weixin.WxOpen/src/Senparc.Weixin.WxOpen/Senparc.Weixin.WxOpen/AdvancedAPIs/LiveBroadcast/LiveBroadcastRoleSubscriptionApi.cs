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

    文件名：LiveBroadcastRoleSubscriptionApi.cs
    文件功能描述：LiveBroadcastRoleSubscriptionApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.LiveBroadcast
{
    /// <summary>小程序直播成员角色与长期订阅接口。</summary>
    public static partial class LiveBroadcastApi
    {
        /// <summary>为小程序直播成员添加角色。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">用户微信号和角色。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置结果；主播未实名时包含认证小程序码 URL。</returns>
        /// <remarks>角色取值：1 管理员、2 主播、3 运营者。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/role-management/api_getrolelistdw.html"/>。</remarks>
        public static LiveBroadcastRoleJsonResult AddRole(string accessTokenOrAppId, LiveBroadcastRoleRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastRoleJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/role/addrole", request, timeOut);
        }

        /// <summary>异步为小程序直播成员添加角色。</summary>
        /// <inheritdoc cref="AddRole"/>
        public static Task<LiveBroadcastRoleJsonResult> AddRoleAsync(string accessTokenOrAppId, LiveBroadcastRoleRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastRoleJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/role/addrole", request, timeOut);
        }

        /// <summary>移除小程序直播成员角色。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">用户微信号和角色。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>移除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/role-management/api_deleterole.html"/>。</remarks>
        public static WxJsonResult DeleteRole(string accessTokenOrAppId, LiveBroadcastRoleRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/role/deleterole", request, timeOut);
        }

        /// <summary>异步移除小程序直播成员角色。</summary>
        /// <inheritdoc cref="DeleteRole"/>
        public static Task<WxJsonResult> DeleteRoleAsync(string accessTokenOrAppId, LiveBroadcastRoleRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/role/deleterole", request, timeOut);
        }

        /// <summary>分页查询小程序直播成员列表。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="offset">可选起始偏移量，默认 0。</param>
        /// <param name="limit">可选查询数量，默认 10，最大 30。</param>
        /// <param name="keyword">可选微信号或昵称关键词。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成员角色列表和总数。</returns>
        /// <remarks>本接口使用 GET 查询参数。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/role-management/api_getrolelist.html"/>。</remarks>
        public static LiveBroadcastRoleListJsonResult GetRoleList(string accessTokenOrAppId, int? offset = null, int? limit = null, string keyword = null, int timeOut = Config.TIME_OUT)
        {
            return SendGet<LiveBroadcastRoleListJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/role/getrolelist",
                Query("offset", offset) + Query("limit", limit) + Query("keyword", keyword), timeOut);
        }

        /// <summary>异步分页查询小程序直播成员列表。</summary>
        /// <inheritdoc cref="GetRoleList"/>
        public static Task<LiveBroadcastRoleListJsonResult> GetRoleListAsync(string accessTokenOrAppId, int? offset = null, int? limit = null, string keyword = null, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<LiveBroadcastRoleListJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/role/getrolelist",
                Query("offset", offset) + Query("limit", limit) + Query("keyword", keyword), timeOut);
        }

        /// <summary>向长期订阅用户群发直播开始事件。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">直播间 ID 和订阅用户 OpenId 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>群发消息 ID。</returns>
        /// <remarks>接口返回成功仅代表触发成功，最终结果以长期订阅群发结果回调为准。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/subscribe-management/api_pushmessage.html"/>。</remarks>
        public static LiveBroadcastPushMessageJsonResult PushLiveStartMessage(string accessTokenOrAppId, LiveBroadcastPushMessageRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastPushMessageJsonResult>(accessTokenOrAppId, "/wxa/business/push_message", request, timeOut);
        }

        /// <summary>异步向长期订阅用户群发直播开始事件。</summary>
        /// <inheritdoc cref="PushLiveStartMessage"/>
        public static Task<LiveBroadcastPushMessageJsonResult> PushLiveStartMessageAsync(string accessTokenOrAppId, LiveBroadcastPushMessageRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastPushMessageJsonResult>(accessTokenOrAppId, "/wxa/business/push_message", request, timeOut);
        }

        /// <summary>分页获取小程序直播长期订阅用户。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">拉取数量和可选翻页标记。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>订阅用户列表和下一页标记。</returns>
        /// <remarks>官方返回表将 followers 与其子字段并列，模型按对象数组表达。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/subscribe-management/api_getfollowers.html"/>。</remarks>
        public static LiveBroadcastGetFollowersJsonResult GetFollowers(string accessTokenOrAppId, LiveBroadcastGetFollowersRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastGetFollowersJsonResult>(accessTokenOrAppId, "/wxa/business/get_wxa_followers", request, timeOut);
        }

        /// <summary>异步分页获取小程序直播长期订阅用户。</summary>
        /// <inheritdoc cref="GetFollowers"/>
        public static Task<LiveBroadcastGetFollowersJsonResult> GetFollowersAsync(string accessTokenOrAppId, LiveBroadcastGetFollowersRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastGetFollowersJsonResult>(accessTokenOrAppId, "/wxa/business/get_wxa_followers", request, timeOut);
        }
    }
}
