/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LicenseSettingsJson.cs
    文件功能描述：企业微信服务商许可应用、设置、优惠和余额强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐许可设置及余额强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.License
{
    /// <summary>仅包含企业 CorpId 的许可请求。</summary>
    public class LicenseCorpRequest
    {
        /// <summary>待操作企业 CorpId。</summary>
        public string corpid { get; set; }
    }

    /// <summary>获取应用接口许可状态请求。</summary>
    public class LicenseGetAppInfoRequest : LicenseCorpRequest
    {
        /// <summary>第三方应用 SuiteId；查询第三方应用时填写。</summary>
        public string suite_id { get; set; }

        /// <summary>自建应用 AgentId；查询自建应用时填写。</summary>
        public int? appid { get; set; }
    }

    /// <summary>应用接口许可状态结果。</summary>
    public class LicenseAppInfoResult : WorkJsonResult
    {
        /// <summary>接口许可状态。</summary>
        public int license_status { get; set; }

        /// <summary>试用期限信息；非试用状态时可能为空。</summary>
        public LicenseTrialInfo trail_info { get; set; }

        /// <summary>许可状态最近一次校验时间，Unix 时间戳。</summary>
        public long license_check_time { get; set; }
    }

    /// <summary>应用接口许可试用期限。</summary>
    public class LicenseTrialInfo
    {
        /// <summary>试用开始时间，Unix 时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>试用结束时间，Unix 时间戳。</summary>
        public long end_time { get; set; }
    }

    /// <summary>设置许可自动激活状态请求。</summary>
    public class LicenseSetAutoActiveStatusRequest : LicenseCorpRequest
    {
        /// <summary>自动激活状态：零关闭，一开启。</summary>
        public int auto_active_status { get; set; }
    }

    /// <summary>查询许可自动激活状态结果。</summary>
    public class LicenseAutoActiveStatusResult : WorkJsonResult
    {
        /// <summary>自动激活状态：零关闭，一开启。</summary>
        public int auto_active_status { get; set; }
    }

    /// <summary>民生优惠条件查询结果。</summary>
    public class LicenseSupportPolicyResult : WorkJsonResult
    {
        /// <summary>是否满足优惠条件。</summary>
        public int query_result { get; set; }

        /// <summary>未满足条件的原因码列表。</summary>
        public List<int> unsatisfied_reason { get; set; }
    }

    /// <summary>服务商充值账户余额结果。</summary>
    public class LicenseAccountBalanceResult : WorkJsonResult
    {
        /// <summary>充值账户余额，单位为分。</summary>
        public long balance { get; set; }
    }
}
