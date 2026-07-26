/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDriveApi.Management.cs
    文件功能描述：企业微信微盘专业版、容量和高级账号管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐微盘专业版、容量与高级账号管理接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive
{
    /// <summary>
    /// 企业微信微盘专业版、容量和高级账号管理接口。
    /// </summary>
    public static partial class WeDriveApi
    {
        private const string ProfessionalInfoPath = "/cgi-bin/wedrive/mng_pro_info";
        private const string CapacityPath = "/cgi-bin/wedrive/mng_capacity";
        private const string VipBatchAddPath = "/cgi-bin/wedrive/vip/batch_add";
        private const string VipBatchDeletePath = "/cgi-bin/wedrive/vip/batch_del";
        private const string VipListPath = "/cgi-bin/wedrive/vip/list";

        /// <summary>
        /// 获取企业微盘专业版及高级功能账号使用信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">可选的操作者 UserID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>专业版状态、账号数量和到期时间。</returns>
        public static WeDriveProfessionalInfoResult GetProfessionalInfo(string accessTokenOrAppKey,
            WeDriveProfessionalInfoRequest request = null, int timeOut = Config.TIME_OUT)
            => Post<WeDriveProfessionalInfoResult>(accessTokenOrAppKey, ProfessionalInfoPath,
                request ?? new WeDriveProfessionalInfoRequest(), timeOut);

        /// <summary>
        /// 异步获取企业微盘专业版及高级功能账号使用信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">可选的操作者 UserID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>专业版状态、账号数量和到期时间。</returns>
        public static Task<WeDriveProfessionalInfoResult> GetProfessionalInfoAsync(string accessTokenOrAppKey,
            WeDriveProfessionalInfoRequest request = null, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveProfessionalInfoResult>(accessTokenOrAppKey, ProfessionalInfoPath,
                request ?? new WeDriveProfessionalInfoRequest(), timeOut);

        /// <summary>
        /// 获取企业微盘全员与高级账号容量信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>总容量和剩余容量，单位为字节。</returns>
        public static WeDriveCapacityResult GetCapacity(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Post<WeDriveCapacityResult>(accessTokenOrAppKey, CapacityPath, new { }, timeOut);

        /// <summary>
        /// 异步获取企业微盘全员与高级账号容量信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>总容量和剩余容量，单位为字节。</returns>
        public static Task<WeDriveCapacityResult> GetCapacityAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveCapacityResult>(accessTokenOrAppKey, CapacityPath, new { }, timeOut);

        /// <summary>
        /// 批量为应用可见范围内的企业成员分配微盘高级功能账号。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待分配的成员 UserID 列表，最多 100 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功和失败的成员 UserID 列表。</returns>
        public static WeDriveVipBatchResult AddVipAccounts(string accessTokenOrAppKey,
            WeDriveVipBatchRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveVipBatchResult>(accessTokenOrAppKey, VipBatchAddPath, request, timeOut);

        /// <summary>
        /// 异步批量为应用可见范围内的企业成员分配微盘高级功能账号。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待分配的成员 UserID 列表，最多 100 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功和失败的成员 UserID 列表。</returns>
        public static Task<WeDriveVipBatchResult> AddVipAccountsAsync(string accessTokenOrAppKey,
            WeDriveVipBatchRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveVipBatchResult>(accessTokenOrAppKey, VipBatchAddPath, request, timeOut);

        /// <summary>
        /// 批量撤销应用可见范围内企业成员的微盘高级功能账号。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待撤销的成员 UserID 列表，最多 100 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功和失败的成员 UserID 列表。</returns>
        public static WeDriveVipBatchResult RemoveVipAccounts(string accessTokenOrAppKey,
            WeDriveVipBatchRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveVipBatchResult>(accessTokenOrAppKey, VipBatchDeletePath, request, timeOut);

        /// <summary>
        /// 异步批量撤销应用可见范围内企业成员的微盘高级功能账号。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待撤销的成员 UserID 列表，最多 100 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功和失败的成员 UserID 列表。</returns>
        public static Task<WeDriveVipBatchResult> RemoveVipAccountsAsync(string accessTokenOrAppKey,
            WeDriveVipBatchRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveVipBatchResult>(accessTokenOrAppKey, VipBatchDeletePath, request, timeOut);

        /// <summary>
        /// 分页获取已分配微盘高级功能且在应用可见范围内的账号列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">分页游标和每页数量。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>高级功能账号列表和下一页游标。</returns>
        public static WeDriveVipListResult GetVipAccountList(string accessTokenOrAppKey,
            WeDriveVipListRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveVipListResult>(accessTokenOrAppKey, VipListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取已分配微盘高级功能且在应用可见范围内的账号列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">分页游标和每页数量。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>高级功能账号列表和下一页游标。</returns>
        public static Task<WeDriveVipListResult> GetVipAccountListAsync(string accessTokenOrAppKey,
            WeDriveVipListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveVipListResult>(accessTokenOrAppKey, VipListPath, request, timeOut);
    }
}
