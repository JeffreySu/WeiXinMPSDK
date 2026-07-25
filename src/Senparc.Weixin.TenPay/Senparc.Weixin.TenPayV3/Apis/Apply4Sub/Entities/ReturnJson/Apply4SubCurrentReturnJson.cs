#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：Apply4SubCurrentReturnJson.cs
    文件功能描述：微信支付 V3 特约商户进件现行响应模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐特约商户进件现行 8 项接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Sub
{
    /// <summary>
    /// 提交特约商户进件申请单的返回结果。
    /// </summary>
    public class Apply4SubCurrentApplymentResultJson : ReturnJsonBase
    {
        /// <summary>微信支付生成的申请单号。</summary>
        public long applyment_id { get; set; }
    }

    /// <summary>
    /// 特约商户进件申请单状态查询结果。
    /// </summary>
    public class Apply4SubCurrentApplymentQueryResultJson : ReturnJsonBase
    {
        /// <summary>服务商自定义的业务申请编号。</summary>
        public string business_code { get; set; }

        /// <summary>微信支付生成的申请单号。</summary>
        public long applyment_id { get; set; }

        /// <summary>申请进入待签约、开通权限中或已完成状态后返回的特约商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>供超级管理员核对联系信息、验证账户并签约的链接。</summary>
        public string sign_url { get; set; }

        /// <summary>申请单状态，例如 APPLYMENT_STATE_AUDITING 或 APPLYMENT_STATE_FINISHED。</summary>
        public string applyment_state { get; set; }

        /// <summary>申请单状态的文字描述。</summary>
        public string applyment_state_msg { get; set; }

        /// <summary>申请被驳回时返回的资料项审核详情。</summary>
        public Apply4SubCurrentAuditDetail[] audit_detail { get; set; }
    }

    /// <summary>
    /// 特约商户进件资料项审核详情。
    /// </summary>
    public class Apply4SubCurrentAuditDetail
    {
        /// <summary>被审核资料项的字段名。</summary>
        public string field { get; set; }

        /// <summary>被审核资料项的中文名称。</summary>
        public string field_name { get; set; }

        /// <summary>资料项被驳回的具体原因。</summary>
        public string reject_reason { get; set; }
    }

    /// <summary>
    /// 修改结算账户的提交结果。
    /// </summary>
    public class Apply4SubModifySettlementResultJson : ReturnJsonBase
    {
        /// <summary>微信支付生成的结算账户修改申请单号。</summary>
        public string application_no { get; set; }
    }

    /// <summary>
    /// 特约商户当前结算账户及验证结果。
    /// </summary>
    public class Apply4SubSettlementResultJson : ReturnJsonBase
    {
        /// <summary>账户类型：ACCOUNT_TYPE_BUSINESS 或 ACCOUNT_TYPE_PRIVATE。</summary>
        public string account_type { get; set; }

        /// <summary>开户银行名称。</summary>
        public string account_bank { get; set; }

        /// <summary>开户银行全称，包含支行名称。</summary>
        public string bank_name { get; set; }

        /// <summary>开户银行联行号。</summary>
        public string bank_branch_id { get; set; }

        /// <summary>按指定掩码规则展示的银行账号。</summary>
        public string account_number { get; set; }

        /// <summary>账户验证结果：VERIFY_SUCCESS、VERIFY_FAIL 或 VERIFYING。</summary>
        public string verify_result { get; set; }

        /// <summary>账户验证失败时返回的具体原因。</summary>
        public string verify_fail_reason { get; set; }
    }

    /// <summary>
    /// 结算账户修改申请的审核状态。
    /// </summary>
    public class Apply4SubSettlementModificationResultJson : ReturnJsonBase
    {
        /// <summary>使用掩码展示的开户名称。</summary>
        public string account_name { get; set; }

        /// <summary>账户类型：ACCOUNT_TYPE_BUSINESS 或 ACCOUNT_TYPE_PRIVATE。</summary>
        public string account_type { get; set; }

        /// <summary>开户银行名称。</summary>
        public string account_bank { get; set; }

        /// <summary>开户银行全称，包含支行名称。</summary>
        public string bank_name { get; set; }

        /// <summary>开户银行联行号。</summary>
        public string bank_branch_id { get; set; }

        /// <summary>按指定掩码规则展示的银行账号。</summary>
        public string account_number { get; set; }

        /// <summary>审核状态：AUDIT_SUCCESS、AUDITING 或 AUDIT_FAIL。</summary>
        public string verify_result { get; set; }

        /// <summary>审核驳回时返回的具体原因。</summary>
        public string verify_fail_reason { get; set; }

        /// <summary>审核结果更新时间，采用 RFC 3339 格式。</summary>
        public string verify_finish_time { get; set; }
    }

    /// <summary>
    /// 特约商户进件文件或视频上传结果。
    /// </summary>
    public class Apply4SubMediaUploadResultJson : ReturnJsonBase
    {
        /// <summary>微信支付返回的媒体文件标识 ID。</summary>
        public string media_id { get; set; }
    }
}
