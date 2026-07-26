/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailApi.Vip.cs
    文件功能描述：企业微信邮件高级功能账号管理接口


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐邮件高级功能账号分配、撤销和列表接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 企业微信邮件高级功能账号管理接口。
    /// <para>自建应用需先在邮件“可调用应用”中配置，再使用该应用 Secret 获取的 access_token 调用。</para>
    /// </summary>
    public static partial class ExmailApi
    {
        private const string VipBatchAddPath = "/cgi-bin/exmail/vip/batch_add";
        private const string VipBatchDeletePath = "/cgi-bin/exmail/vip/batch_del";
        private const string VipListPath = "/cgi-bin/exmail/vip/list";

        /// <summary>
        /// 批量为应用可见范围内的企业成员分配邮件高级功能账号。
        /// <para>单次最多操作 100 个成员。</para>
        /// <para>官方文档：<see href="https://developer.work.weixin.qq.com/document/path/99316"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置为邮件可调用应用的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待分配的成员 UserID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分配成功和分配失败的成员 UserID 列表。</returns>
        public static ExmailVipBatchResult AddVipAccounts(
            string accessTokenOrAppKey, ExmailVipBatchRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ExmailVipBatchResult>(accessTokenOrAppKey,
                VipBatchAddPath, request, timeOut);

        /// <summary>
        /// 异步批量为应用可见范围内的企业成员分配邮件高级功能账号。
        /// <para>单次最多操作 100 个成员。</para>
        /// <para>官方文档：<see href="https://developer.work.weixin.qq.com/document/path/99316"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置为邮件可调用应用的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待分配的成员 UserID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分配成功和分配失败的成员 UserID 列表。</returns>
        public static Task<ExmailVipBatchResult> AddVipAccountsAsync(
            string accessTokenOrAppKey, ExmailVipBatchRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailVipBatchResult>(accessTokenOrAppKey,
                VipBatchAddPath, request, timeOut);

        /// <summary>
        /// 批量撤销应用可见范围内企业成员的邮件高级功能账号。
        /// <para>单次最多操作 100 个成员。</para>
        /// <para>官方文档：<see href="https://developer.work.weixin.qq.com/document/path/99317"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置为邮件可调用应用的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待撤销分配的成员 UserID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>撤销成功和撤销失败的成员 UserID 列表。</returns>
        public static ExmailVipBatchResult RemoveVipAccounts(
            string accessTokenOrAppKey, ExmailVipBatchRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ExmailVipBatchResult>(accessTokenOrAppKey,
                VipBatchDeletePath, request, timeOut);

        /// <summary>
        /// 异步批量撤销应用可见范围内企业成员的邮件高级功能账号。
        /// <para>单次最多操作 100 个成员。</para>
        /// <para>官方文档：<see href="https://developer.work.weixin.qq.com/document/path/99317"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置为邮件可调用应用的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待撤销分配的成员 UserID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>撤销成功和撤销失败的成员 UserID 列表。</returns>
        public static Task<ExmailVipBatchResult> RemoveVipAccountsAsync(
            string accessTokenOrAppKey, ExmailVipBatchRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailVipBatchResult>(accessTokenOrAppKey,
                VipBatchDeletePath, request, timeOut);

        /// <summary>
        /// 分页获取已分配邮件高级功能且在应用可见范围内的成员账号。
        /// <para>每页默认 100 条，最多 200 条；应依据返回的 has_more 继续分页。</para>
        /// <para>官方文档：<see href="https://developer.work.weixin.qq.com/document/path/99318"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置为邮件可调用应用的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">可选的游标和每页数量。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已分配高级功能的成员 UserID 列表和分页信息。</returns>
        public static ExmailVipListResult GetVipAccountList(
            string accessTokenOrAppKey, ExmailVipListRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ExmailVipListResult>(accessTokenOrAppKey,
                VipListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取已分配邮件高级功能且在应用可见范围内的成员账号。
        /// <para>每页默认 100 条，最多 200 条；应依据返回的 has_more 继续分页。</para>
        /// <para>官方文档：<see href="https://developer.work.weixin.qq.com/document/path/99318"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置为邮件可调用应用的调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">可选的游标和每页数量。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已分配高级功能的成员 UserID 列表和分页信息。</returns>
        public static Task<ExmailVipListResult> GetVipAccountListAsync(
            string accessTokenOrAppKey, ExmailVipListRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailVipListResult>(accessTokenOrAppKey,
                VipListPath, request, timeOut);
    }
}
