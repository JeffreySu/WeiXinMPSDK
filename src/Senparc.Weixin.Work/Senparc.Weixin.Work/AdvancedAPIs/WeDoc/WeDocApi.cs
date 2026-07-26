/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.cs
    文件功能描述：企业微信文档基础管理与高级账号接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐文档基础管理、权限及高级账号接口

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>
    /// 企业微信文档接口。
    /// <para>调用前需要为自建应用配置文档接口权限，并使用该应用 Secret 获取的 access_token。</para>
    /// </summary>
    public static partial class WeDocApi
    {
        private const string CreateDocumentPath = "/cgi-bin/wedoc/create_doc";
        private const string RenameDocumentPath = "/cgi-bin/wedoc/rename_doc";
        private const string DeleteDocumentPath = "/cgi-bin/wedoc/del_doc";
        private const string GetDocumentBaseInfoPath = "/cgi-bin/wedoc/get_doc_base_info";
        private const string GetDocumentSharePath = "/cgi-bin/wedoc/doc_share";
        private const string GetDocumentAuthPath = "/cgi-bin/wedoc/doc_get_auth";
        private const string ModifyDocumentJoinRulePath = "/cgi-bin/wedoc/mod_doc_join_rule";
        private const string ModifyDocumentMemberPath = "/cgi-bin/wedoc/mod_doc_member";
        private const string ModifyDocumentSafetySettingPath = "/cgi-bin/wedoc/mod_doc_safty_setting";
        private const string BatchAddDocumentVipPath = "/cgi-bin/wedoc/vip/batch_add";
        private const string BatchDeleteDocumentVipPath = "/cgi-bin/wedoc/vip/batch_del";
        private const string GetDocumentVipListPath = "/cgi-bin/wedoc/vip/list";

        /// <summary>新建在线文档或电子表格。</summary>
        public static WeDocCreateResult CreateDocument(string accessTokenOrAppKey, WeDocCreateRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WeDocCreateResult>(accessTokenOrAppKey, CreateDocumentPath, request, timeOut);

        /// <summary>异步新建在线文档或电子表格。</summary>
        public static Task<WeDocCreateResult> CreateDocumentAsync(string accessTokenOrAppKey,
            WeDocCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocCreateResult>(accessTokenOrAppKey, CreateDocumentPath, request, timeOut);

        /// <summary>重命名文档或收集表。</summary>
        public static WorkJsonResult RenameDocumentOrForm(string accessTokenOrAppKey, WeDocRenameRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, RenameDocumentPath, request, timeOut);

        /// <summary>异步重命名文档或收集表。</summary>
        public static Task<WorkJsonResult> RenameDocumentOrFormAsync(string accessTokenOrAppKey,
            WeDocRenameRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, RenameDocumentPath, request, timeOut);

        /// <summary>删除文档或收集表。</summary>
        public static WorkJsonResult DeleteDocumentOrForm(string accessTokenOrAppKey, WeDocResourceIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteDocumentPath, request, timeOut);

        /// <summary>异步删除文档或收集表。</summary>
        public static Task<WorkJsonResult> DeleteDocumentOrFormAsync(string accessTokenOrAppKey,
            WeDocResourceIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteDocumentPath, request, timeOut);

        /// <summary>获取文档名称、类型和创建、修改时间。</summary>
        public static WeDocBaseInfoResult GetDocumentBaseInfo(string accessTokenOrAppKey,
            WeDocIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocBaseInfoResult>(accessTokenOrAppKey, GetDocumentBaseInfoPath, request, timeOut);

        /// <summary>异步获取文档名称、类型和创建、修改时间。</summary>
        public static Task<WeDocBaseInfoResult> GetDocumentBaseInfoAsync(string accessTokenOrAppKey,
            WeDocIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocBaseInfoResult>(accessTokenOrAppKey, GetDocumentBaseInfoPath, request, timeOut);

        /// <summary>获取文档或收集表分享链接。</summary>
        public static WeDocShareResult GetShareLink(string accessTokenOrAppKey, WeDocResourceIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WeDocShareResult>(accessTokenOrAppKey, GetDocumentSharePath, request, timeOut);

        /// <summary>异步获取文档或收集表分享链接。</summary>
        public static Task<WeDocShareResult> GetShareLinkAsync(string accessTokenOrAppKey,
            WeDocResourceIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocShareResult>(accessTokenOrAppKey, GetDocumentSharePath, request, timeOut);

        /// <summary>获取文档查看规则、成员权限和安全设置。</summary>
        public static WeDocAuthResult GetDocumentAuth(string accessTokenOrAppKey, WeDocIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WeDocAuthResult>(accessTokenOrAppKey, GetDocumentAuthPath, request, timeOut);

        /// <summary>异步获取文档查看规则、成员权限和安全设置。</summary>
        public static Task<WeDocAuthResult> GetDocumentAuthAsync(string accessTokenOrAppKey,
            WeDocIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocAuthResult>(accessTokenOrAppKey, GetDocumentAuthPath, request, timeOut);

        /// <summary>修改文档的企业内外查看规则和协作范围。</summary>
        public static WorkJsonResult UpdateDocumentJoinRule(string accessTokenOrAppKey,
            WeDocModifyJoinRuleRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ModifyDocumentJoinRulePath, request, timeOut);

        /// <summary>异步修改文档的企业内外查看规则和协作范围。</summary>
        public static Task<WorkJsonResult> UpdateDocumentJoinRuleAsync(string accessTokenOrAppKey,
            WeDocModifyJoinRuleRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ModifyDocumentJoinRulePath, request, timeOut);

        /// <summary>修改文档通知范围及成员权限。</summary>
        public static WorkJsonResult UpdateDocumentMembers(string accessTokenOrAppKey,
            WeDocModifyMemberRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ModifyDocumentMemberPath, request, timeOut);

        /// <summary>异步修改文档通知范围及成员权限。</summary>
        public static Task<WorkJsonResult> UpdateDocumentMembersAsync(string accessTokenOrAppKey,
            WeDocModifyMemberRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ModifyDocumentMemberPath, request, timeOut);

        /// <summary>修改文档只读复制和水印安全设置。</summary>
        public static WorkJsonResult UpdateDocumentSafetySetting(string accessTokenOrAppKey,
            WeDocModifySafetySettingRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ModifyDocumentSafetySettingPath, request, timeOut);

        /// <summary>异步修改文档只读复制和水印安全设置。</summary>
        public static Task<WorkJsonResult> UpdateDocumentSafetySettingAsync(string accessTokenOrAppKey,
            WeDocModifySafetySettingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ModifyDocumentSafetySettingPath, request, timeOut);

        /// <summary>批量分配文档高级功能账号。</summary>
        public static WeDocVipBatchResult BatchAddDocumentVip(string accessTokenOrAppKey,
            WeDocVipBatchRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocVipBatchResult>(accessTokenOrAppKey, BatchAddDocumentVipPath, request, timeOut);

        /// <summary>异步批量分配文档高级功能账号。</summary>
        public static Task<WeDocVipBatchResult> BatchAddDocumentVipAsync(string accessTokenOrAppKey,
            WeDocVipBatchRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocVipBatchResult>(accessTokenOrAppKey, BatchAddDocumentVipPath, request, timeOut);

        /// <summary>批量撤销文档高级功能账号。</summary>
        public static WeDocVipBatchResult BatchRemoveDocumentVip(string accessTokenOrAppKey,
            WeDocVipBatchRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocVipBatchResult>(accessTokenOrAppKey, BatchDeleteDocumentVipPath, request, timeOut);

        /// <summary>异步批量撤销文档高级功能账号。</summary>
        public static Task<WeDocVipBatchResult> BatchRemoveDocumentVipAsync(string accessTokenOrAppKey,
            WeDocVipBatchRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocVipBatchResult>(accessTokenOrAppKey, BatchDeleteDocumentVipPath, request, timeOut);

        /// <summary>分页获取文档高级功能账号列表。</summary>
        public static WeDocVipListResult GetDocumentVipList(string accessTokenOrAppKey,
            WeDocVipListRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocVipListResult>(accessTokenOrAppKey, GetDocumentVipListPath, request, timeOut);

        /// <summary>异步分页获取文档高级功能账号列表。</summary>
        public static Task<WeDocVipListResult> GetDocumentVipListAsync(string accessTokenOrAppKey,
            WeDocVipListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocVipListResult>(accessTokenOrAppKey, GetDocumentVipListPath, request, timeOut);

        /// <summary>
        /// 为单个成员分配文档高级功能账号。
        /// </summary>
        /// <remarks>兼容早期未发布命名；当前协议为批量成员接口。</remarks>
        [Obsolete("请使用 BatchAddDocumentVip() 批量分配高级功能账号。")]
        public static WorkJsonResult AddDocumentAdmin(string accessTokenOrAppKey, WeDocAdminRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, BatchAddDocumentVipPath,
                ToVipBatchRequest(request), timeOut);

        /// <summary>异步为单个成员分配文档高级功能账号。</summary>
        /// <remarks>兼容早期未发布命名；当前协议为批量成员接口。</remarks>
        [Obsolete("请使用 BatchAddDocumentVipAsync() 批量分配高级功能账号。")]
        public static Task<WorkJsonResult> AddDocumentAdminAsync(string accessTokenOrAppKey,
            WeDocAdminRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, BatchAddDocumentVipPath,
                ToVipBatchRequest(request), timeOut);

        /// <summary>撤销单个成员的文档高级功能账号。</summary>
        /// <remarks>兼容早期未发布命名；当前协议为批量成员接口。</remarks>
        [Obsolete("请使用 BatchRemoveDocumentVip() 批量撤销高级功能账号。")]
        public static WorkJsonResult RemoveDocumentAdmin(string accessTokenOrAppKey, WeDocAdminRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, BatchDeleteDocumentVipPath,
                ToVipBatchRequest(request), timeOut);

        /// <summary>异步撤销单个成员的文档高级功能账号。</summary>
        /// <remarks>兼容早期未发布命名；当前协议为批量成员接口。</remarks>
        [Obsolete("请使用 BatchRemoveDocumentVipAsync() 批量撤销高级功能账号。")]
        public static Task<WorkJsonResult> RemoveDocumentAdminAsync(string accessTokenOrAppKey,
            WeDocAdminRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, BatchDeleteDocumentVipPath,
                ToVipBatchRequest(request), timeOut);

        /// <summary>获取文档高级功能账号列表。</summary>
        /// <remarks>兼容早期未发布命名；当前协议使用游标分页且不接收文档 ID。</remarks>
        [Obsolete("请使用 GetDocumentVipList() 分页获取高级功能账号。")]
        public static WeDocAdminListResult GetDocumentAdminList(string accessTokenOrAppKey,
            WeDocIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocAdminListResult>(accessTokenOrAppKey, GetDocumentVipListPath,
                new WeDocVipListRequest(), timeOut);

        /// <summary>异步获取文档高级功能账号列表。</summary>
        /// <remarks>兼容早期未发布命名；当前协议使用游标分页且不接收文档 ID。</remarks>
        [Obsolete("请使用 GetDocumentVipListAsync() 分页获取高级功能账号。")]
        public static Task<WeDocAdminListResult> GetDocumentAdminListAsync(string accessTokenOrAppKey,
            WeDocIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocAdminListResult>(accessTokenOrAppKey, GetDocumentVipListPath,
                new WeDocVipListRequest(), timeOut);

        private static WeDocVipBatchRequest ToVipBatchRequest(WeDocAdminRequest request)
            => new WeDocVipBatchRequest
            {
                userid_list = new List<string> { request?.userid }
            };

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);
    }
}
