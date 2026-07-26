#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MerchantGovernanceModels.cs
    文件功能描述：不活跃商户身份核实与子商户管控查询模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐不活跃商户身份核实与子商户管控查询接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.TenPayV3.Apis.MerchantGovernance
{
    /// <summary>
    /// 发起不活跃商户身份核实的请求数据。
    /// </summary>
    public class InactiveMerchantVerificationRequestData
    {
        /// <summary>
        /// 微信支付分配给特约商户的唯一商户号，最长 32 个字符。
        /// </summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 发起不活跃商户身份核实的返回结果。
    /// </summary>
    public class InactiveMerchantVerificationSubmitResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付生成的核实单唯一标识。
        /// </summary>
        public string verification_id { get; set; }
    }

    /// <summary>
    /// 不活跃商户身份核实结果。
    /// </summary>
    public class InactiveMerchantVerificationResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 被核实身份的特约商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 核实单唯一标识。
        /// </summary>
        public string verification_id { get; set; }

        /// <summary>
        /// 核实单状态：PROCESSING、SUCCESS 或 FAIL。
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 核实失败原因；当前可能为 MATERIALS_ABNORMAL 或 PROCESS_TIMEOUT。
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 核实单创建时间，采用 RFC 3339 格式。
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 核实完成时间；核实成功或失败后返回，采用 RFC 3339 格式。
        /// </summary>
        public string finish_time { get; set; }
    }

    /// <summary>
    /// 子商户管控情况查询结果。
    /// </summary>
    public class MerchantLimitationResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 被查询子商户的商户号。
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 商户被管控的标准能力列表，例如 NO_TRANSACTION、NO_REFUND。
        /// </summary>
        public List<string> limited_functions { get; set; }

        /// <summary>
        /// 标准能力列表之外的其他被管控能力描述；多项以英文逗号分隔。
        /// </summary>
        public string other_limited_functions { get; set; }

        /// <summary>
        /// 被管控原因及对应解脱路径列表。
        /// </summary>
        public List<MerchantRecoverySpecification> recovery_specifications { get; set; }
    }

    /// <summary>
    /// 单项管控原因、影响能力和解脱路径。
    /// </summary>
    public class MerchantRecoverySpecification
    {
        /// <summary>
        /// 唯一标记本次管控动作的单据号，可与管控流水订阅通知关联。
        /// </summary>
        public string limitation_case_id { get; set; }

        /// <summary>
        /// 管控原因类型，例如 LICENSE_ABNORMAL、NO_TRADE 或 RISK_ABNORMAL。
        /// </summary>
        public string limitation_reason_type { get; set; }

        /// <summary>
        /// 商户被管控的原因。
        /// </summary>
        public string limitation_reason { get; set; }

        /// <summary>
        /// 当前管控原因的详细说明。
        /// </summary>
        public string limitation_reason_describe { get; set; }

        /// <summary>
        /// 当前原因导致商户被管控的标准能力列表。
        /// </summary>
        public List<string> relate_limitations { get; set; }

        /// <summary>
        /// 当前原因导致的其他被管控能力描述；多项以英文逗号分隔。
        /// </summary>
        public string other_relate_limitations { get; set; }

        /// <summary>
        /// 解脱路径，例如 MODIFY_SUBJECT_INFORMATION 或 VERIFY_INACTIVE_MERCHANT_IDENTITY。
        /// </summary>
        public string recover_way { get; set; }

        /// <summary>
        /// 解脱路径所需的业务单号、机关信息或经营类型确认单号等参数。
        /// </summary>
        public string recover_way_param { get; set; }

        /// <summary>
        /// 对应解脱路径的帮助说明链接。
        /// </summary>
        public string recover_help_url { get; set; }

        /// <summary>
        /// 处置方式：LIMIT_ACTION_TYPE_IMMEDIATE_CONTROL 或 LIMIT_ACTION_TYPE_DELAY_CONTROL。
        /// </summary>
        public string limitation_action_type { get; set; }

        /// <summary>
        /// 延迟管控的预计开始时间，采用 RFC 3339 格式。
        /// </summary>
        public string limitation_start_date { get; set; }

        /// <summary>
        /// 商户实际被当前原因管控的时间，采用 RFC 3339 格式。
        /// </summary>
        public string limitation_date { get; set; }
    }
}
