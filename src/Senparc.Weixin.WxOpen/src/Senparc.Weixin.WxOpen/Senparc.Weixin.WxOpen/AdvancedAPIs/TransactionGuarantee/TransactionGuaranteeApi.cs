#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TransactionGuaranteeApi.cs
    文件功能描述：TransactionGuaranteeApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.TransactionGuarantee
{
    /// <summary>
    /// 小程序交易保障接口。
    /// </summary>
    /// <remarks>本类中的接口均支持使用 authorizer_access_token 由第三方平台代商家调用。</remarks>
    public static class TransactionGuaranteeApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 基础能力

        /// <summary>
        /// 获取小程序交易体验分违规记录。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="offset">起始偏移量，从 0 开始。</param>
        /// <param name="limit">返回数量，最大为 100。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>违规记录、当前交易体验分和记录总数。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/basic/api_getpenaltylist"/>。</remarks>
        public static TransactionGuaranteePenaltyListJsonResult GetPenaltyList(string accessTokenOrAppId, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            var query = Query("offset", offset) + Query("limit", limit);
            return SendGet<TransactionGuaranteePenaltyListJsonResult>(accessTokenOrAppId, "/wxaapi/wxamptrade/get_penalty_list", query, timeOut);
        }

        /// <summary>
        /// 异步获取小程序交易体验分违规记录。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="offset">起始偏移量，从 0 开始。</param>
        /// <param name="limit">返回数量，最大为 100。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>违规记录、当前交易体验分和记录总数。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/basic/api_getpenaltylist"/>。</remarks>
        public static Task<TransactionGuaranteePenaltyListJsonResult> GetPenaltyListAsync(string accessTokenOrAppId, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            var query = Query("offset", offset) + Query("limit", limit);
            return SendGetAsync<TransactionGuaranteePenaltyListJsonResult>(accessTokenOrAppId, "/wxaapi/wxamptrade/get_penalty_list", query, timeOut);
        }

        /// <summary>
        /// 获取小程序交易保障标状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>保障标开通状态及未开通原因。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/basic/api_getguaranteestatus"/>。</remarks>
        public static TransactionGuaranteeStatusJsonResult GetGuaranteeStatus(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendGet<TransactionGuaranteeStatusJsonResult>(accessTokenOrAppId, "/wxaapi/wxamptrade/get_guarantee_status", null, timeOut);
        }

        /// <summary>
        /// 异步获取小程序交易保障标状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>保障标开通状态及未开通原因。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/basic/api_getguaranteestatus"/>。</remarks>
        public static Task<TransactionGuaranteeStatusJsonResult> GetGuaranteeStatusAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<TransactionGuaranteeStatusJsonResult>(accessTokenOrAppId, "/wxaapi/wxamptrade/get_guarantee_status", null, timeOut);
        }

        #endregion

        #region 评价与评论

        /// <summary>
        /// 查询小程序交易评价列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">查询时间、过滤条件和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>符合条件的评价列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_getccommentlist"/>。</remarks>
        public static TransactionGuaranteeCommentListJsonResult GetCommentList(string accessTokenOrAppId, TransactionGuaranteeCommentListRequest request, int timeOut = Config.TIME_OUT)
        {
            var query = BuildCommentListQuery(request);
            return SendGet<TransactionGuaranteeCommentListJsonResult>(accessTokenOrAppId, "/wxaapi/comment/mpcommentlist/get", query, timeOut);
        }

        /// <summary>
        /// 异步查询小程序交易评价列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">查询时间、过滤条件和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>符合条件的评价列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_getccommentlist"/>。</remarks>
        public static Task<TransactionGuaranteeCommentListJsonResult> GetCommentListAsync(string accessTokenOrAppId, TransactionGuaranteeCommentListRequest request, int timeOut = Config.TIME_OUT)
        {
            var query = BuildCommentListQuery(request);
            return SendGetAsync<TransactionGuaranteeCommentListJsonResult>(accessTokenOrAppId, "/wxaapi/comment/mpcommentlist/get", query, timeOut);
        }

        /// <summary>
        /// 查询指定评价下的评论和回复列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="commentId">评价 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>第一条评论及其后续回复。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_getcommentreplylist"/>。</remarks>
        public static TransactionGuaranteeReplyListJsonResult GetCommentReplyList(string accessTokenOrAppId, string commentId, int timeOut = Config.TIME_OUT)
        {
            return SendGet<TransactionGuaranteeReplyListJsonResult>(accessTokenOrAppId, "/wxaapi/comment/replyandcommentreplylist/get", Query("commentId", commentId), timeOut);
        }

        /// <summary>
        /// 异步查询指定评价下的评论和回复列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="commentId">评价 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>第一条评论及其后续回复。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_getcommentreplylist"/>。</remarks>
        public static Task<TransactionGuaranteeReplyListJsonResult> GetCommentReplyListAsync(string accessTokenOrAppId, string commentId, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<TransactionGuaranteeReplyListJsonResult>(accessTokenOrAppId, "/wxaapi/comment/replyandcommentreplylist/get", Query("commentId", commentId), timeOut);
        }

        /// <summary>
        /// 查询指定交易评价的详情和处理进度。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="commentId">评价 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>评价详情、差评处理进度和可选的改评前评价。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_getcommentinfo"/>。</remarks>
        public static TransactionGuaranteeCommentInfoJsonResult GetCommentInfo(string accessTokenOrAppId, string commentId, int timeOut = Config.TIME_OUT)
        {
            return SendGet<TransactionGuaranteeCommentInfoJsonResult>(accessTokenOrAppId, "/wxaapi/comment/commentinfo/get", Query("commentId", commentId), timeOut);
        }

        /// <summary>
        /// 异步查询指定交易评价的详情和处理进度。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="commentId">评价 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>评价详情、差评处理进度和可选的改评前评价。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_getcommentinfo"/>。</remarks>
        public static Task<TransactionGuaranteeCommentInfoJsonResult> GetCommentInfoAsync(string accessTokenOrAppId, string commentId, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<TransactionGuaranteeCommentInfoJsonResult>(accessTokenOrAppId, "/wxaapi/comment/commentinfo/get", Query("commentId", commentId), timeOut);
        }

        /// <summary>
        /// 为交易评价创建商家评论。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID 和评论内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_addreply"/>。</remarks>
        public static TransactionGuaranteeActionJsonResult AddReply(string accessTokenOrAppId, TransactionGuaranteeAddReplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/reply/add", request, timeOut);
        }

        /// <summary>
        /// 异步为交易评价创建商家评论。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID 和评论内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_addreply"/>。</remarks>
        public static Task<TransactionGuaranteeActionJsonResult> AddReplyAsync(string accessTokenOrAppId, TransactionGuaranteeAddReplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/reply/add", request, timeOut);
        }

        /// <summary>
        /// 删除指定交易评价下的商家评论。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID 和评论 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_deletereply"/>。</remarks>
        public static TransactionGuaranteeActionJsonResult DeleteReply(string accessTokenOrAppId, TransactionGuaranteeDeleteReplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/reply/delete", request, timeOut);
        }

        /// <summary>
        /// 异步删除指定交易评价下的商家评论。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID 和评论 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_deletereply"/>。</remarks>
        public static Task<TransactionGuaranteeActionJsonResult> DeleteReplyAsync(string accessTokenOrAppId, TransactionGuaranteeDeleteReplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/reply/delete", request, timeOut);
        }

        /// <summary>
        /// 回复指定交易评价下的评论。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID、评论 ID 和回复内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>回复结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_addcommentreply"/>。</remarks>
        public static TransactionGuaranteeActionJsonResult AddCommentReply(string accessTokenOrAppId, TransactionGuaranteeAddCommentReplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/commentreply/add", request, timeOut);
        }

        /// <summary>
        /// 异步回复指定交易评价下的评论。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID、评论 ID 和回复内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>回复结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_addcommentreply"/>。</remarks>
        public static Task<TransactionGuaranteeActionJsonResult> AddCommentReplyAsync(string accessTokenOrAppId, TransactionGuaranteeAddCommentReplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/commentreply/add", request, timeOut);
        }

        /// <summary>
        /// 删除指定交易评论下的一条回复。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID、评论 ID 和回复 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_deletecommentreply"/>。</remarks>
        public static TransactionGuaranteeActionJsonResult DeleteCommentReply(string accessTokenOrAppId, TransactionGuaranteeDeleteCommentReplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/commentreply/delete", request, timeOut);
        }

        /// <summary>
        /// 异步删除指定交易评论下的一条回复。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID、评论 ID 和回复 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_deletecommentreply"/>。</remarks>
        public static Task<TransactionGuaranteeActionJsonResult> DeleteCommentReplyAsync(string accessTokenOrAppId, TransactionGuaranteeDeleteCommentReplyRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/commentreply/delete", request, timeOut);
        }

        /// <summary>
        /// 重置指定差评的 API 客服会话额度。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">需要重置额度的评价 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>重置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_resetapikfquota"/>。</remarks>
        public static TransactionGuaranteeActionJsonResult ResetApiCustomerServiceQuota(string accessTokenOrAppId, TransactionGuaranteeCommentIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/apikfquota/reset", request, timeOut);
        }

        /// <summary>
        /// 异步重置指定差评的 API 客服会话额度。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">需要重置额度的评价 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>重置结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_resetapikfquota"/>。</remarks>
        public static Task<TransactionGuaranteeActionJsonResult> ResetApiCustomerServiceQuotaAsync(string accessTokenOrAppId, TransactionGuaranteeCommentIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/apikfquota/reset", request, timeOut);
        }

        /// <summary>
        /// 向差评用户提交和解材料并确认和解。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID、和解说明和图片素材。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_confirmcompromise"/>。</remarks>
        public static TransactionGuaranteeActionJsonResult ConfirmCompromise(string accessTokenOrAppId, TransactionGuaranteeCompromiseRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/confirmcompromise", request, timeOut);
        }

        /// <summary>
        /// 异步向差评用户提交和解材料并确认和解。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">评价 ID、和解说明和图片素材。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/comment/api_confirmcompromise"/>。</remarks>
        public static Task<TransactionGuaranteeActionJsonResult> ConfirmCompromiseAsync(string accessTokenOrAppId, TransactionGuaranteeCompromiseRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<TransactionGuaranteeActionJsonResult>(accessTokenOrAppId, "/wxaapi/comment/confirmcompromise", request, timeOut);
        }

        #endregion

        #region 投诉处理

        /// <summary>
        /// 回应小程序交易投诉。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">投诉单号、处理意见及文字或图片材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>回应结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_respondcomplaint"/>。</remarks>
        public static WxJsonResult RespondComplaint(string accessTokenOrAppId, TransactionGuaranteeRespondComplaintRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/bussiRespondComplaint", request, timeOut);
        }

        /// <summary>
        /// 异步回应小程序交易投诉。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">投诉单号、处理意见及文字或图片材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>回应结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_respondcomplaint"/>。</remarks>
        public static Task<WxJsonResult> RespondComplaintAsync(string accessTokenOrAppId, TransactionGuaranteeRespondComplaintRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/bussiRespondComplaint", request, timeOut);
        }

        /// <summary>
        /// 为小程序交易投诉补充凭证。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">投诉单号及文字或图片凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>补充结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_supplyproof"/>。</remarks>
        public static WxJsonResult SupplyComplaintProof(string accessTokenOrAppId, TransactionGuaranteeComplaintMaterialRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/bussiSupplyProof", request, timeOut);
        }

        /// <summary>
        /// 异步为小程序交易投诉补充凭证。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">投诉单号及文字或图片凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>补充结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_supplyproof"/>。</remarks>
        public static Task<WxJsonResult> SupplyComplaintProofAsync(string accessTokenOrAppId, TransactionGuaranteeComplaintMaterialRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/bussiSupplyProof", request, timeOut);
        }

        /// <summary>
        /// 提交小程序交易投诉退款处理凭证。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">投诉单号、退款凭证及退货确认信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_submitrefund"/>。</remarks>
        public static WxJsonResult SubmitComplaintRefund(string accessTokenOrAppId, TransactionGuaranteeRefundProofRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/bussiSupplyRefund", request, timeOut);
        }

        /// <summary>
        /// 异步提交小程序交易投诉退款处理凭证。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">投诉单号、退款凭证及退货确认信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_submitrefund"/>。</remarks>
        public static Task<WxJsonResult> SubmitComplaintRefundAsync(string accessTokenOrAppId, TransactionGuaranteeRefundProofRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/bussiSupplyRefund", request, timeOut);
        }

        /// <summary>
        /// 查询小程序交易投诉单详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="complaintOrderId">投诉单 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>投诉单、处理进度和退货运单信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_getorderdetail"/>。</remarks>
        public static TransactionGuaranteeComplaintDetailJsonResult GetComplaintOrderDetail(string accessTokenOrAppId, string complaintOrderId, int timeOut = Config.TIME_OUT)
        {
            return SendGet<TransactionGuaranteeComplaintDetailJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/complaintOrderDetail", Query("complaintOrderId", complaintOrderId), timeOut);
        }

        /// <summary>
        /// 异步查询小程序交易投诉单详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="complaintOrderId">投诉单 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>投诉单、处理进度和退货运单信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_getorderdetail"/>。</remarks>
        public static Task<TransactionGuaranteeComplaintDetailJsonResult> GetComplaintOrderDetailAsync(string accessTokenOrAppId, string complaintOrderId, int timeOut = Config.TIME_OUT)
        {
            return SendGetAsync<TransactionGuaranteeComplaintDetailJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/complaintOrderDetail", Query("complaintOrderId", complaintOrderId), timeOut);
        }

        /// <summary>
        /// 对小程序交易投诉的责任判定提交商家申诉。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">投诉单号及文字或图片申诉材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申诉提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_busiappeal"/>。</remarks>
        public static WxJsonResult SubmitComplaintAppeal(string accessTokenOrAppId, TransactionGuaranteeComplaintMaterialRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPost<WxJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/busiAppeal", request, timeOut);
        }

        /// <summary>
        /// 异步对小程序交易投诉的责任判定提交商家申诉。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">投诉单号及文字或图片申诉材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申诉提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/transaction-guarantee/complaint/api_busiappeal"/>。</remarks>
        public static Task<WxJsonResult> SubmitComplaintAppealAsync(string accessTokenOrAppId, TransactionGuaranteeComplaintMaterialRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendPostAsync<WxJsonResult>(accessTokenOrAppId, "/wxaapi/minishop/busiAppeal", request, timeOut);
        }

        #endregion

        private static string BuildCommentListQuery(TransactionGuaranteeCommentListRequest request)
        {
            return Query("startTime", request.startTime)
                   + Query("endTime", request.endTime)
                   + Query("filterType", request.filterType)
                   + Query("offset", request.offset)
                   + Query("limit", request.limit);
        }

        private static string Query(string name, object value)
        {
            return value == null ? string.Empty : "&" + name + "=" + value.ToString().AsUrlData();
        }

        private static T SendGet<T>(string accessTokenOrAppId, string path, string query, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken,
                    Config.ApiMpHost + path + "?access_token={0}" + query,
                    null, CommonJsonSendType.GET, timeOut: timeOut),
                accessTokenOrAppId);
        }

        private static Task<T> SendGetAsync<T>(string accessTokenOrAppId, string path, string query, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken,
                    Config.ApiMpHost + path + "?access_token={0}" + query,
                    null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false),
                accessTokenOrAppId);
        }

        private static T SendPost<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken,
                    Config.ApiMpHost + path + "?access_token={0}",
                    request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting),
                accessTokenOrAppId);
        }

        private static Task<T> SendPostAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken,
                    Config.ApiMpHost + path + "?access_token={0}",
                    request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false),
                accessTokenOrAppId);
        }
    }
}
