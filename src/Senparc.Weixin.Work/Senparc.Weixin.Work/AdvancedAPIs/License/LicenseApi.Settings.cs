/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LicenseApi.Settings.cs
    文件功能描述：企业微信服务商许可应用、设置、优惠及余额接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐许可状态、自动激活、优惠及余额接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.License
{
    /// <summary>
    /// 企业微信服务商许可应用、设置、优惠及余额接口。
    /// </summary>
    public static partial class LicenseApi
    {
        private const string GetAppLicenseInfoPath =
            "/cgi-bin/license/get_app_license_info";
        private const string SetAutoActiveStatusPath =
            "/cgi-bin/license/set_auto_active_status";
        private const string GetAutoActiveStatusPath =
            "/cgi-bin/license/get_auto_active_status";
        private const string SupportPolicyQueryPath =
            "/cgi-bin/license/support_policy_query";
        private const string GetAccountBalancePath =
            "/cgi-bin/service/get_account_balance";

        /// <summary>
        /// 获取企业内指定应用的接口许可状态及试用信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97194"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业、第三方应用 SuiteId 或自建应用 AppId。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>许可状态、试用期限和许可校验时间。</returns>
        public static LicenseAppInfoResult GetAppLicenseInfo(
            string providerAccessToken, LicenseGetAppInfoRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseAppInfoResult>(providerAccessToken,
                GetAppLicenseInfoPath, data, timeOut);

        /// <summary>
        /// 异步获取企业内指定应用的接口许可状态及试用信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97194"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业、第三方应用 SuiteId 或自建应用 AppId。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>许可状态、试用期限和许可校验时间。</returns>
        public static Task<LicenseAppInfoResult> GetAppLicenseInfoAsync(
            string providerAccessToken, LicenseGetAppInfoRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseAppInfoResult>(providerAccessToken,
                GetAppLicenseInfoPath, data, timeOut);

        /// <summary>
        /// 设置企业是否允许许可账号自动激活。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97199"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业和自动激活状态。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Senparc.Weixin.Entities.WorkJsonResult SetAutoActiveStatus(
            string providerAccessToken, LicenseSetAutoActiveStatusRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<Senparc.Weixin.Entities.WorkJsonResult>(providerAccessToken,
                SetAutoActiveStatusPath, data, timeOut);

        /// <summary>
        /// 异步设置企业是否允许许可账号自动激活。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97199"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业和自动激活状态。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<Senparc.Weixin.Entities.WorkJsonResult>
            SetAutoActiveStatusAsync(string providerAccessToken,
                LicenseSetAutoActiveStatusRequest data,
                int timeOut = Config.TIME_OUT)
            => PostAsync<Senparc.Weixin.Entities.WorkJsonResult>(providerAccessToken,
                SetAutoActiveStatusPath, data, timeOut);

        /// <summary>
        /// 查询企业当前的许可账号自动激活状态。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97200"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询企业。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>自动激活状态。</returns>
        public static LicenseAutoActiveStatusResult GetAutoActiveStatus(
            string providerAccessToken, LicenseCorpRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseAutoActiveStatusResult>(providerAccessToken,
                GetAutoActiveStatusPath, data, timeOut);

        /// <summary>
        /// 异步查询企业当前的许可账号自动激活状态。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97200"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询企业。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>自动激活状态。</returns>
        public static Task<LicenseAutoActiveStatusResult> GetAutoActiveStatusAsync(
            string providerAccessToken, LicenseCorpRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseAutoActiveStatusResult>(providerAccessToken,
                GetAutoActiveStatusPath, data, timeOut);

        /// <summary>
        /// 查询企业是否满足民生行业优惠条件及不满足原因。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97208"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询企业。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>查询结果和未满足条件原因码。</returns>
        public static LicenseSupportPolicyResult QuerySupportPolicy(
            string providerAccessToken, LicenseCorpRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseSupportPolicyResult>(providerAccessToken,
                SupportPolicyQueryPath, data, timeOut);

        /// <summary>
        /// 异步查询企业是否满足民生行业优惠条件及不满足原因。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97208"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询企业。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>查询结果和未满足条件原因码。</returns>
        public static Task<LicenseSupportPolicyResult> QuerySupportPolicyAsync(
            string providerAccessToken, LicenseCorpRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseSupportPolicyResult>(providerAccessToken,
                SupportPolicyQueryPath, data, timeOut);

        /// <summary>
        /// 获取服务商充值账户余额。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100138"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>以分为单位的充值账户余额。</returns>
        public static LicenseAccountBalanceResult GetAccountBalance(
            string providerAccessToken, int timeOut = Config.TIME_OUT)
            => Get<LicenseAccountBalanceResult>(providerAccessToken,
                GetAccountBalancePath, timeOut);

        /// <summary>
        /// 异步获取服务商充值账户余额。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100138"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>以分为单位的充值账户余额。</returns>
        public static Task<LicenseAccountBalanceResult> GetAccountBalanceAsync(
            string providerAccessToken, int timeOut = Config.TIME_OUT)
            => GetAsync<LicenseAccountBalanceResult>(providerAccessToken,
                GetAccountBalancePath, timeOut);
    }
}
