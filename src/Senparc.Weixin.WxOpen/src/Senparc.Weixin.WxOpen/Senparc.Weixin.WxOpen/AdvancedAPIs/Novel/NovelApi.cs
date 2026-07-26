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

    文件名：NovelApi.cs
    文件功能描述：NovelApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Novel
{
    /// <summary>小程序小说作品、章节、授权、预览和推荐服务端接口。</summary>
    /// <remarks>
    /// 官方目录共 23 个请求路径，均使用 POST，并支持第三方平台通过
    /// <c>authorizer_access_token</c> 代小程序调用，权限集 ID 为 169。
    /// </remarks>
    public static partial class NovelApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 作品管理

        /// <summary>创建小说作品。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品基础信息、类型、完结状态及可选推荐信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新作品 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_createbook.html"/>。</remarks>
        public static NovelBookIdJsonResult CreateBook(string accessTokenOrAppId, NovelCreateBookRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelBookIdJsonResult>(accessTokenOrAppId, "/wxa/book/createbook", request, timeOut);
        }

        /// <summary>异步创建小说作品。</summary>
        /// <inheritdoc cref="CreateBook"/>
        public static Task<NovelBookIdJsonResult> CreateBookAsync(string accessTokenOrAppId, NovelCreateBookRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelBookIdJsonResult>(accessTokenOrAppId, "/wxa/book/createbook", request, timeOut);
        }

        /// <summary>编辑小说作品的编辑版信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID 及需要修改的字段；未设置字段不会发送。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>编辑结果。</returns>
        /// <remarks>修改不会直接影响发布版，需提审通过后更新。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_updatebook.html"/>。</remarks>
        public static WxJsonResult UpdateBook(string accessTokenOrAppId, NovelUpdateBookRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/book/updatebook", request, timeOut);
        }

        /// <summary>异步编辑小说作品的编辑版信息。</summary>
        /// <inheritdoc cref="UpdateBook"/>
        public static Task<WxJsonResult> UpdateBookAsync(string accessTokenOrAppId, NovelUpdateBookRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/book/updatebook", request, timeOut);
        }

        /// <summary>删除小说作品及相关授权关系。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>删除同时作用于编辑版和发布版，审核中的作品不可删除。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_deletebook.html"/>。</remarks>
        public static WxJsonResult DeleteBook(string accessTokenOrAppId, NovelBookIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/book/deletebook", request, timeOut);
        }

        /// <summary>异步删除小说作品及相关授权关系。</summary>
        /// <inheritdoc cref="DeleteBook"/>
        public static Task<WxJsonResult> DeleteBookAsync(string accessTokenOrAppId, NovelBookIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/book/deletebook", request, timeOut);
        }

        /// <summary>分页获取小说作品列表。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">分页方式及编辑版/发布版选择。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>作品列表、总数及可选分页 ID。</returns>
        /// <remarks>作品超过 10 万时建议使用 last_id 分页。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_listbook.html"/>。</remarks>
        public static NovelListBooksJsonResult ListBooks(string accessTokenOrAppId, NovelListBooksRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelListBooksJsonResult>(accessTokenOrAppId, "/wxa/book/listbook", request, timeOut);
        }

        /// <summary>异步分页获取小说作品列表。</summary>
        /// <inheritdoc cref="ListBooks"/>
        public static Task<NovelListBooksJsonResult> ListBooksAsync(string accessTokenOrAppId, NovelListBooksRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelListBooksJsonResult>(accessTokenOrAppId, "/wxa/book/listbook", request, timeOut);
        }

        /// <summary>获取指定小说作品详情。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID 或提供方作品主键，以及编辑版/发布版选择。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>作品详情、分卷和审核信息。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_getbook.html"/>。</remarks>
        public static NovelGetBookJsonResult GetBook(string accessTokenOrAppId, NovelGetBookRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelGetBookJsonResult>(accessTokenOrAppId, "/wxa/book/getbook", request, timeOut);
        }

        /// <summary>异步获取指定小说作品详情。</summary>
        /// <inheritdoc cref="GetBook"/>
        public static Task<NovelGetBookJsonResult> GetBookAsync(string accessTokenOrAppId, NovelGetBookRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelGetBookJsonResult>(accessTokenOrAppId, "/wxa/book/getbook", request, timeOut);
        }

        #endregion

        #region 章节管理

        /// <summary>向小说作品编辑版上传一个章节。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID 和章节内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新章节 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_createchapter.html"/>。</remarks>
        public static NovelChapterIdJsonResult CreateChapter(string accessTokenOrAppId, NovelCreateChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelChapterIdJsonResult>(accessTokenOrAppId, "/wxa/book/createchapter", request, timeOut);
        }

        /// <summary>异步向小说作品编辑版上传一个章节。</summary>
        /// <inheritdoc cref="CreateChapter"/>
        public static Task<NovelChapterIdJsonResult> CreateChapterAsync(string accessTokenOrAppId, NovelCreateChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelChapterIdJsonResult>(accessTokenOrAppId, "/wxa/book/createchapter", request, timeOut);
        }

        /// <summary>向小说作品编辑版批量上传章节。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID 和最多 10 个章节。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>章节 ID 及冲突的提供方章节主键列表。</returns>
        /// <remarks>官方返回表写 <c>chapter_id</c>，示例写 <c>chapter_id_list</c>，结果模型兼容二者。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_batchcreatechapter.html"/>。</remarks>
        public static NovelBatchCreateChaptersJsonResult BatchCreateChapters(string accessTokenOrAppId, NovelBatchCreateChaptersRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelBatchCreateChaptersJsonResult>(accessTokenOrAppId, "/wxa/book/batchcreatechapter", request, timeOut);
        }

        /// <summary>异步向小说作品编辑版批量上传章节。</summary>
        /// <inheritdoc cref="BatchCreateChapters"/>
        public static Task<NovelBatchCreateChaptersJsonResult> BatchCreateChaptersAsync(string accessTokenOrAppId, NovelBatchCreateChaptersRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelBatchCreateChaptersJsonResult>(accessTokenOrAppId, "/wxa/book/batchcreatechapter", request, timeOut);
        }

        /// <summary>删除小说作品编辑版中的章节。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID 和章节 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_deletechapter.html"/>。</remarks>
        public static WxJsonResult DeleteChapter(string accessTokenOrAppId, NovelDeleteChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/book/deletechapter", request, timeOut);
        }

        /// <summary>异步删除小说作品编辑版中的章节。</summary>
        /// <inheritdoc cref="DeleteChapter"/>
        public static Task<WxJsonResult> DeleteChapterAsync(string accessTokenOrAppId, NovelDeleteChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/book/deletechapter", request, timeOut);
        }

        /// <summary>替换小说作品编辑版中的章节内容。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品、原章节以及新标题和正文。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>替换后生成的新章节 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_replacechapter.html"/>。</remarks>
        public static NovelReplaceChapterJsonResult ReplaceChapter(string accessTokenOrAppId, NovelReplaceChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelReplaceChapterJsonResult>(accessTokenOrAppId, "/wxa/book/replacechapter", request, timeOut);
        }

        /// <summary>异步替换小说作品编辑版中的章节内容。</summary>
        /// <inheritdoc cref="ReplaceChapter"/>
        public static Task<NovelReplaceChapterJsonResult> ReplaceChapterAsync(string accessTokenOrAppId, NovelReplaceChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelReplaceChapterJsonResult>(accessTokenOrAppId, "/wxa/book/replacechapter", request, timeOut);
        }

        /// <summary>分页获取小说章节列表。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID、版本、分页和可选分卷筛选。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>章节列表及总数。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_listchapter.html"/>。</remarks>
        public static NovelListChaptersJsonResult ListChapters(string accessTokenOrAppId, NovelListChaptersRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelListChaptersJsonResult>(accessTokenOrAppId, "/wxa/book/listchapter", request, timeOut);
        }

        /// <summary>异步分页获取小说章节列表。</summary>
        /// <inheritdoc cref="ListChapters"/>
        public static Task<NovelListChaptersJsonResult> ListChaptersAsync(string accessTokenOrAppId, NovelListChaptersRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelListChaptersJsonResult>(accessTokenOrAppId, "/wxa/book/listchapter", request, timeOut);
        }

        /// <summary>获取小说章节详情。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID、章节 ID 和编辑版/发布版选择。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>章节正文、审核、分卷和排序信息。</returns>
        /// <remarks>官方返回参数表漏列示例中的 book_id 和 content，结果模型已保留。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_getchapter.html"/>。</remarks>
        public static NovelGetChapterJsonResult GetChapter(string accessTokenOrAppId, NovelGetChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelGetChapterJsonResult>(accessTokenOrAppId, "/wxa/book/getchapter", request, timeOut);
        }

        /// <summary>异步获取小说章节详情。</summary>
        /// <inheritdoc cref="GetChapter"/>
        public static Task<NovelGetChapterJsonResult> GetChapterAsync(string accessTokenOrAppId, NovelGetChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelGetChapterJsonResult>(accessTokenOrAppId, "/wxa/book/getchapter", request, timeOut);
        }

        /// <summary>相对目标章节调整一个章节的位置。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品、待移动章节、目标章节和操作类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>排序结果。</returns>
        /// <remarks>仅适用于“追加”排序方式。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_reorderchapter.html"/>。</remarks>
        public static WxJsonResult ReorderChapter(string accessTokenOrAppId, NovelReorderChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/book/reorderchapter", request, timeOut);
        }

        /// <summary>异步相对目标章节调整一个章节的位置。</summary>
        /// <inheritdoc cref="ReorderChapter"/>
        public static Task<WxJsonResult> ReorderChapterAsync(string accessTokenOrAppId, NovelReorderChapterRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/book/reorderchapter", request, timeOut);
        }

        /// <summary>批量调整章节的相对顺序 seq。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID 和章节 seq 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新结果。</returns>
        /// <remarks>仅适用于“seq 递增”排序方式。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_updatechapterseq.html"/>。</remarks>
        public static WxJsonResult UpdateChapterSequence(string accessTokenOrAppId, NovelUpdateChapterSequenceRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/book/updatechapterseq", request, timeOut);
        }

        /// <summary>异步批量调整章节的相对顺序 seq。</summary>
        /// <inheritdoc cref="UpdateChapterSequence"/>
        public static Task<WxJsonResult> UpdateChapterSequenceAsync(string accessTokenOrAppId, NovelUpdateChapterSequenceRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/book/updatechapterseq", request, timeOut);
        }

        /// <summary>提交小说作品审核。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>提审结果。</returns>
        /// <remarks>审核通过后更新作品发布版信息。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/business/api_auditbook.html"/>。</remarks>
        public static WxJsonResult AuditBook(string accessTokenOrAppId, NovelBookIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/book/auditbook", request, timeOut);
        }

        /// <summary>异步提交小说作品审核。</summary>
        /// <inheritdoc cref="AuditBook"/>
        public static Task<WxJsonResult> AuditBookAsync(string accessTokenOrAppId, NovelBookIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/book/auditbook", request, timeOut);
        }

        #endregion

        private static T Send<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, BuildUrl(path), request ?? new { }, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting), accessTokenOrAppId);
        }

        private static Task<T> SendAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken, BuildUrl(path), request ?? new { }, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false), accessTokenOrAppId);
        }

        private static string BuildUrl(string path)
        {
            return Config.ApiMpHost + path + "?access_token={0}";
        }
    }
}
