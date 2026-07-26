/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LicenseApi.Account.cs
    文件功能描述：企业微信服务商许可账号管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐许可账号激活、查询、继承和分配接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.License
{
    /// <summary>
    /// 企业微信服务商许可账号管理接口。
    /// </summary>
    public static partial class LicenseApi
    {
        private const string ActivateAccountPath = "/cgi-bin/license/active_account";
        private const string BatchActivateAccountPath =
            "/cgi-bin/license/batch_active_account";
        private const string ActivateAccountByTypePath =
            "/cgi-bin/license/active_account_by_type";
        private const string GetActiveInfoByCodePath =
            "/cgi-bin/license/get_active_info_by_code";
        private const string BatchGetActiveInfoByCodePath =
            "/cgi-bin/license/batch_get_active_info_by_code";
        private const string ListActivatedAccountPath =
            "/cgi-bin/license/list_actived_account";
        private const string GetActiveInfoByUserPath =
            "/cgi-bin/license/get_active_info_by_user";
        private const string BatchTransferLicensePath =
            "/cgi-bin/license/batch_transfer_license";
        private const string BatchShareActiveCodePath =
            "/cgi-bin/license/batch_share_active_code";

        /// <summary>
        /// 使用指定激活码为企业成员激活许可账号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97188"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">激活码、所属企业和待激活成员。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult ActivateAccount(string providerAccessToken,
            LicenseActivateAccountRequest data, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(providerAccessToken, ActivateAccountPath, data,
                timeOut);

        /// <summary>
        /// 异步使用指定激活码为企业成员激活许可账号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97188"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">激活码、所属企业和待激活成员。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> ActivateAccountAsync(
            string providerAccessToken, LicenseActivateAccountRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(providerAccessToken, ActivateAccountPath, data,
                timeOut);

        /// <summary>
        /// 一次为同一企业的多个成员激活许可账号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97188"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业及不超过一千个成员的激活列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>逐项激活结果。</returns>
        public static LicenseBatchActivateAccountResult BatchActivateAccount(
            string providerAccessToken, LicenseBatchActivateAccountRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseBatchActivateAccountResult>(providerAccessToken,
                BatchActivateAccountPath, data, timeOut);

        /// <summary>
        /// 异步一次为同一企业的多个成员激活许可账号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97188"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业及不超过一千个成员的激活列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>逐项激活结果。</returns>
        public static Task<LicenseBatchActivateAccountResult>
            BatchActivateAccountAsync(string providerAccessToken,
                LicenseBatchActivateAccountRequest data,
                int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseBatchActivateAccountResult>(providerAccessToken,
                BatchActivateAccountPath, data, timeOut);

        /// <summary>
        /// 从企业未使用的激活码中选择指定账号类型为成员激活。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97188"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">账号类型、企业和待激活成员。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult ActivateAccountByType(string providerAccessToken,
            LicenseActivateAccountByTypeRequest data, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(providerAccessToken, ActivateAccountByTypePath, data,
                timeOut);

        /// <summary>
        /// 异步从企业未使用的激活码中选择指定账号类型为成员激活。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97188"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">账号类型、企业和待激活成员。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> ActivateAccountByTypeAsync(
            string providerAccessToken, LicenseActivateAccountByTypeRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(providerAccessToken,
                ActivateAccountByTypePath, data, timeOut);

        /// <summary>
        /// 获取单个许可激活码的状态、成员和流转信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97189"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业及待查询激活码。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>激活码详情。</returns>
        public static LicenseGetActiveInfoResult GetActiveInfoByCode(
            string providerAccessToken, LicenseGetActiveInfoByCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseGetActiveInfoResult>(providerAccessToken,
                GetActiveInfoByCodePath, data, timeOut);

        /// <summary>
        /// 异步获取单个许可激活码的状态、成员和流转信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97189"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业及待查询激活码。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>激活码详情。</returns>
        public static Task<LicenseGetActiveInfoResult> GetActiveInfoByCodeAsync(
            string providerAccessToken, LicenseGetActiveInfoByCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseGetActiveInfoResult>(providerAccessToken,
                GetActiveInfoByCodePath, data, timeOut);

        /// <summary>
        /// 批量获取许可激活码详情和无效激活码列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97189"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业及待查询激活码列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>激活码详情和无效激活码列表。</returns>
        public static LicenseBatchGetActiveInfoResult BatchGetActiveInfoByCode(
            string providerAccessToken, LicenseBatchGetActiveInfoByCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseBatchGetActiveInfoResult>(providerAccessToken,
                BatchGetActiveInfoByCodePath, data, timeOut);

        /// <summary>
        /// 异步批量获取许可激活码详情和无效激活码列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97189"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业及待查询激活码列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>激活码详情和无效激活码列表。</returns>
        public static Task<LicenseBatchGetActiveInfoResult>
            BatchGetActiveInfoByCodeAsync(string providerAccessToken,
                LicenseBatchGetActiveInfoByCodeRequest data,
                int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseBatchGetActiveInfoResult>(providerAccessToken,
                BatchGetActiveInfoByCodePath, data, timeOut);

        /// <summary>
        /// 分页获取企业已经激活的许可账号列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97190"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>已激活账号列表和下一页游标。</returns>
        public static LicenseActivatedAccountListResult ListActivatedAccount(
            string providerAccessToken, LicenseListActivatedAccountRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseActivatedAccountListResult>(providerAccessToken,
                ListActivatedAccountPath, data, timeOut);

        /// <summary>
        /// 异步分页获取企业已经激活的许可账号列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97190"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>已激活账号列表和下一页游标。</returns>
        public static Task<LicenseActivatedAccountListResult>
            ListActivatedAccountAsync(string providerAccessToken,
                LicenseListActivatedAccountRequest data,
                int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseActivatedAccountListResult>(providerAccessToken,
                ListActivatedAccountPath, data, timeOut);

        /// <summary>
        /// 获取指定企业成员的许可激活状态和账号详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97191"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业和成员 UserId。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>成员激活状态和许可账号列表。</returns>
        public static LicenseGetActiveInfoByUserResult GetActiveInfoByUser(
            string providerAccessToken, LicenseGetActiveInfoByUserRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseGetActiveInfoByUserResult>(providerAccessToken,
                GetActiveInfoByUserPath, data, timeOut);

        /// <summary>
        /// 异步获取指定企业成员的许可激活状态和账号详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97191"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业和成员 UserId。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>成员激活状态和许可账号列表。</returns>
        public static Task<LicenseGetActiveInfoByUserResult>
            GetActiveInfoByUserAsync(string providerAccessToken,
                LicenseGetActiveInfoByUserRequest data,
                int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseGetActiveInfoByUserResult>(providerAccessToken,
                GetActiveInfoByUserPath, data, timeOut);

        /// <summary>
        /// 批量将离职成员的许可账号继承给接替成员。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97192"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业及交接成员列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>逐项继承结果。</returns>
        public static LicenseTransferResult TransferAccount(
            string providerAccessToken, LicenseTransferRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseTransferResult>(providerAccessToken,
                BatchTransferLicensePath, data, timeOut);

        /// <summary>
        /// 异步批量将离职成员的许可账号继承给接替成员。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97192"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业及交接成员列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>逐项继承结果。</returns>
        public static Task<LicenseTransferResult> TransferAccountAsync(
            string providerAccessToken, LicenseTransferRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseTransferResult>(providerAccessToken,
                BatchTransferLicensePath, data, timeOut);

        /// <summary>
        /// 批量分配激活码给下游或下级企业。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97193"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">来源企业、目标企业、关联类型和激活码列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>逐项分配结果。</returns>
        public static LicenseShareActiveCodeResult ShareActiveCode(
            string providerAccessToken, LicenseShareActiveCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseShareActiveCodeResult>(providerAccessToken,
                BatchShareActiveCodePath, data, timeOut);

        /// <summary>
        /// 异步批量分配激活码给下游或下级企业。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97193"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">来源企业、目标企业、关联类型和激活码列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>逐项分配结果。</returns>
        public static Task<LicenseShareActiveCodeResult> ShareActiveCodeAsync(
            string providerAccessToken, LicenseShareActiveCodeRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseShareActiveCodeResult>(providerAccessToken,
                BatchShareActiveCodePath, data, timeOut);
    }
}
