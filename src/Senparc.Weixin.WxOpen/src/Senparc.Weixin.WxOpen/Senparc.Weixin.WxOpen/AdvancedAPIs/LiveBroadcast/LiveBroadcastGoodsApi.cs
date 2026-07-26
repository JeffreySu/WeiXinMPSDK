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

    文件名：LiveBroadcastGoodsApi.cs
    文件功能描述：LiveBroadcastGoodsApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.LiveBroadcast
{
    /// <summary>小程序直播商品库管理接口。</summary>
    public static partial class LiveBroadcastApi
    {
        /// <summary>添加并提审直播商品。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商品封面、名称、价格和详情页信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商品 ID 和审核单 ID。</returns>
        /// <remarks>新增时 goodsInfo 中除 price2、thirdPartyAppid 和 goodsId 外的字段均为必填。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/commodity-management/api_addgoods.html"/>。</remarks>
        public static LiveBroadcastAddGoodsJsonResult AddGoods(string accessTokenOrAppId, LiveBroadcastGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastAddGoodsJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/add", request, timeOut);
        }

        /// <summary>异步添加并提审直播商品。</summary>
        /// <inheritdoc cref="AddGoods"/>
        public static Task<LiveBroadcastAddGoodsJsonResult> AddGoodsAsync(string accessTokenOrAppId, LiveBroadcastGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastAddGoodsJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/add", request, timeOut);
        }

        /// <summary>重新提交已撤回的商品审核。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新的审核单 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/commodity-management/api_resubmitaudit.html"/>。</remarks>
        public static LiveBroadcastAuditIdJsonResult ResubmitGoodsAudit(string accessTokenOrAppId, LiveBroadcastGoodsIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastAuditIdJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/audit", request, timeOut);
        }

        /// <summary>异步重新提交已撤回的商品审核。</summary>
        /// <inheritdoc cref="ResubmitGoodsAudit"/>
        public static Task<LiveBroadcastAuditIdJsonResult> ResubmitGoodsAuditAsync(string accessTokenOrAppId, LiveBroadcastGoodsIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastAuditIdJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/audit", request, timeOut);
        }

        /// <summary>批量获取商品信息与审核状态。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">最多 20 个商品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商品信息和审核状态列表。</returns>
        /// <remarks>该接口返回 snake_case 字段，模型同时兼容商品列表接口的 camelCase 字段。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/commodity-management/api_getgoodsauditinfo.html"/>。</remarks>
        public static LiveBroadcastGoodsListJsonResult GetGoodsWarehouse(string accessTokenOrAppId, LiveBroadcastGetGoodsWarehouseRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<LiveBroadcastGoodsListJsonResult>(accessTokenOrAppId, "/wxa/business/getgoodswarehouse", request, timeOut);
        }

        /// <summary>异步批量获取商品信息与审核状态。</summary>
        /// <inheritdoc cref="GetGoodsWarehouse"/>
        public static Task<LiveBroadcastGoodsListJsonResult> GetGoodsWarehouseAsync(string accessTokenOrAppId, LiveBroadcastGetGoodsWarehouseRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<LiveBroadcastGoodsListJsonResult>(accessTokenOrAppId, "/wxa/business/getgoodswarehouse", request, timeOut);
        }

        /// <summary>撤回商品审核。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商品 ID 和审核单 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>撤回结果。</returns>
        /// <remarks>撤回不会返还已消耗的提审次数。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/commodity-management/api_resetaudit.html"/>。</remarks>
        public static WxJsonResult ResetGoodsAudit(string accessTokenOrAppId, LiveBroadcastResetGoodsAuditRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/resetaudit", request, timeOut);
        }

        /// <summary>异步撤回商品审核。</summary>
        /// <inheritdoc cref="ResetGoodsAudit"/>
        public static Task<WxJsonResult> ResetGoodsAuditAsync(string accessTokenOrAppId, LiveBroadcastResetGoodsAuditRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/resetaudit", request, timeOut);
        }

        /// <summary>更新直播商品信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">包含必填 goodsId 以及需要修改字段的商品信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果。</returns>
        /// <remarks>审核通过后仅允许更新价格相关字段；模型采用可空字段，未设置值不会发送。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/commodity-management/api_updategoodsinfo.html"/>。</remarks>
        public static WxJsonResult UpdateGoods(string accessTokenOrAppId, LiveBroadcastGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/update", request, timeOut);
        }

        /// <summary>异步更新直播商品信息。</summary>
        /// <inheritdoc cref="UpdateGoods"/>
        public static Task<WxJsonResult> UpdateGoodsAsync(string accessTokenOrAppId, LiveBroadcastGoodsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/update", request, timeOut);
        }

        /// <summary>按审核状态分页获取直播商品列表。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="status">审核状态：0 未审核，1 审核中，2 审核通过，3 审核驳回。</param>
        /// <param name="offset">分页起点。</param>
        /// <param name="limit">分页大小，默认 30，最大 100。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商品列表和总数。</returns>
        /// <remarks>本接口使用 GET 查询参数。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/commodity-management/api_getgoodsinfo.html"/>。</remarks>
        public static LiveBroadcastGoodsListJsonResult GetApprovedGoods(string accessTokenOrAppId, int status, int offset = 0, int limit = 30, int timeOut = Config.TIME_OUT)
        {
            return SendGet<LiveBroadcastGoodsListJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/getapproved",
                Query("offset", offset) + Query("limit", limit) + Query("status", status), timeOut);
        }

        /// <summary>异步按审核状态分页获取直播商品列表。</summary>
        /// <inheritdoc cref="GetApprovedGoods"/>
        public static Task<LiveBroadcastGoodsListJsonResult> GetApprovedGoodsAsync(string accessTokenOrAppId, int status, int offset = 0, int limit = 30, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<LiveBroadcastGoodsListJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/getapproved",
                Query("offset", offset) + Query("limit", limit) + Query("status", status), timeOut);
        }

        /// <summary>从直播商品库删除商品。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">商品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>删除不可恢复，直播间内同一商品也会同步删除。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/livebroadcast/commodity-management/api_deletegoodsinfo.html"/>。</remarks>
        public static WxJsonResult DeleteGoods(string accessTokenOrAppId, LiveBroadcastGoodsIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/delete", request, timeOut);
        }

        /// <summary>异步从直播商品库删除商品。</summary>
        /// <inheritdoc cref="DeleteGoods"/>
        public static Task<WxJsonResult> DeleteGoodsAsync(string accessTokenOrAppId, LiveBroadcastGoodsIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/broadcast/goods/delete", request, timeOut);
        }
    }
}
