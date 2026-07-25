#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：EcommerceMerchantCancellationReturnJson.cs
    文件功能描述：微信支付 V3 电商收付通商户注销响应模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐商户注销校验、注销和提现响应字段

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 商户注销资格校验结果。
    /// </summary>
    public class EcommerceCancellationValidationResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 接受注销资格校验的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 商户号状态：NORMAL 或 HAS_BEEN_CANCELLED。
        /// </summary>
        public string merchant_state { get; set; }

        /// <summary>
        /// 校验结果：ALLOW_CANCEL_WITHDRAW 或 NOT_ALLOW_CANCEL_WITHDRAW。
        /// </summary>
        public string validate_result { get; set; }

        /// <summary>
        /// 二级商户已开通资金账户的实时余额。
        /// </summary>
        public EcommerceCancellationAccountInfo[] account_info { get; set; }

        /// <summary>
        /// 当前不可发起注销的原因列表。
        /// </summary>
        public EcommerceCancellationBlockReason[] block_reasons { get; set; }
    }

    /// <summary>
    /// 商户注销流程中的资金账户信息。
    /// </summary>
    public class EcommerceCancellationAccountInfo
    {
        /// <summary>
        /// 出款子账户类型，如 BASIC_ACCOUNT、OPERATE_ACCOUNT、MARGIN_ACCOUNT 或
        /// TRADE_FEE_ACCOUNT。
        /// </summary>
        public string out_account_type { get; set; }

        /// <summary>
        /// 账户金额，单位为分。
        /// </summary>
        public int amount { get; set; }
    }

    /// <summary>
    /// 不可发起商户注销的原因。
    /// </summary>
    public class EcommerceCancellationBlockReason
    {
        /// <summary>
        /// 原因类型，如 CONSUMER_COMPLAINT_UNPROCESSED、HAS_BLOCKING_CONTROL、
        /// FUNDS_PENDING_PROCESSING 或 OTHER_REASON。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 不可注销原因描述。
        /// </summary>
        public string description { get; set; }
    }

    /// <summary>
    /// 注销或提现申请提交结果。
    /// </summary>
    public class EcommerceCancellationApplyResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付生成的注销提现或提现申请单号。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 商户提交的注销或提现申请单号。
        /// </summary>
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 新流程注销提现申请查询结果。
    /// </summary>
    public class EcommerceCancelWithdrawQueryResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付生成的注销提现申请单号。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 商户提交的注销申请单号。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 注销提现申请状态，如 ACCEPTED、REVIEWING、WAITING_MERCHANT_CONFIRM、
        /// FUND_PROCESSING 或 FINISH。
        /// </summary>
        public string cancel_state { get; set; }

        /// <summary>
        /// 注销提现申请状态的文字描述。
        /// </summary>
        public string cancel_state_description { get; set; }

        /// <summary>
        /// 是否提取资金：NOT_APPLY_WITHDRAW 或 APPLY_WITHDRAW。
        /// </summary>
        public string withdraw { get; set; }

        /// <summary>
        /// 提现状态：WITHDRAW_PROCESSING、WITHDRAW_EXCEPTION 或 WITHDRAW_SUCCEED。
        /// </summary>
        public string withdraw_state { get; set; }

        /// <summary>
        /// 提现状态的文字描述。
        /// </summary>
        public string withdraw_state_description { get; set; }

        /// <summary>
        /// 各资金账户的提现付款结果。
        /// </summary>
        public EcommerceCancellationAccountWithdrawResult[] account_withdraw_result { get; set; }

        /// <summary>
        /// 申请单最后更新时间，格式遵循 RFC 3339。
        /// </summary>
        public string modify_time { get; set; }

        /// <summary>
        /// 申请注销的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 涉及资金提取时返回的商户账户余额。
        /// </summary>
        public EcommerceCancellationAccountInfo[] account_info { get; set; }

        /// <summary>
        /// 等待商户确认注销时返回的确认入口。
        /// </summary>
        public EcommerceCancellationConfirmInfo confirm_cancel { get; set; }
    }

    /// <summary>
    /// 商户账户注销提现付款结果。
    /// </summary>
    public class EcommerceCancellationAccountWithdrawResult
    {
        /// <summary>
        /// 出款子账户类型。
        /// </summary>
        public string out_account_type { get; set; }

        /// <summary>
        /// 付款状态：PAY_PROCESSING、PAY_SUCCEED、PAY_FAIL 或 BANK_REFUNDED。
        /// </summary>
        public string pay_state { get; set; }

        /// <summary>
        /// 付款状态描述。
        /// </summary>
        public string state_description { get; set; }
    }

    /// <summary>
    /// 商户超级管理员确认注销信息。
    /// </summary>
    public class EcommerceCancellationConfirmInfo
    {
        /// <summary>
        /// 商户员工确认注销的页面 URL，可转换为二维码展示。
        /// </summary>
        public string confirm_cancel_url { get; set; }
    }

    /// <summary>
    /// 旧流程商户注销申请结果。
    /// </summary>
    public class EcommerceLegacyCancelApplicationResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 商户提交的注销申请单号。
        /// </summary>
        public string out_apply_no { get; set; }

        /// <summary>
        /// 申请注销的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 审核驳回或受理失败原因。
        /// </summary>
        public string reject_reason { get; set; }

        /// <summary>
        /// 注销状态：REVIEWING、REJECTED 或 CANCEL_SUCCESS。
        /// </summary>
        public string cancel_state { get; set; }

        /// <summary>
        /// 注销申请最后更新时间，格式遵循 RFC 3339。
        /// </summary>
        public string update_time { get; set; }
    }

    /// <summary>
    /// 商户注销图片上传结果。
    /// </summary>
    public class EcommerceCancellationMediaUploadResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付返回的媒体文件标识 ID。
        /// </summary>
        public string media_id { get; set; }
    }

    /// <summary>
    /// 旧流程注销后提现申请查询结果。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawQueryResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 提现申请单信息。字段名 <c>withdrawl_apply</c> 沿用微信支付接口拼写。
        /// </summary>
        public EcommerceLegacyCancelWithdrawStatus withdrawl_apply { get; set; }
    }

    /// <summary>
    /// 旧流程注销后提现申请单状态。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawStatus
    {
        /// <summary>
        /// 微信支付生成的提现申请单号。
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 商户提交的提现申请单号。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 申请状态：SUBMITTED、PASSED、PAY_SUCCEED、REJECTED、PAY_FAILED 或
        /// BANK_REFUNDED。
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 审批驳回、付款失败或银行退票原因。
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 申请单最后更新时间，格式遵循 RFC 3339。
        /// </summary>
        public string modify_time { get; set; }
    }
}
