/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AppCurrentJson.cs
    文件功能描述：企业微信应用迁移、权限和管理员强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐应用迁移、权限及管理员模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.App
{
    /// <summary>
    /// 将代开发应用迁移为自建应用请求。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/96072">企业微信官方文档</see></para>
    /// </summary>
    public class MigrateToCustomizedAppRequest
    {
        /// <summary>代开发应用模板接口调用凭证。</summary>
        public string suite_access_token { get; set; }
    }

    /// <summary>应用权限查询结果。</summary>
    public class GetAppPermissionsResult : WorkJsonResult
    {
        /// <summary>应用需要添加的权限标识列表。</summary>
        public IList<string> app_permissions { get; set; }
    }

    /// <summary>应用管理员信息。</summary>
    public class AppAdministrator
    {
        /// <summary>管理员成员 UserID。</summary>
        public string userid { get; set; }

        /// <summary>管理权限类型。</summary>
        public int auth_type { get; set; }
    }

    /// <summary>应用管理员列表结果。</summary>
    public class GetAppAdminListResult : WorkJsonResult
    {
        /// <summary>应用管理员及其管理权限列表。</summary>
        public IList<AppAdministrator> admin { get; set; }
    }
}
