#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：EcommerceMerchantCancellationRequestData.cs
    文件功能描述：微信支付 V3 电商收付通商户注销请求模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐商户注销新旧流程请求字段

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 新流程注销提现申请数据。
    /// </summary>
    public class EcommerceApplyCancelWithdrawRequestData
    {
        /// <summary>
        /// 申请注销的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 商户自定义且在服务商维度唯一的注销申请单号，仅包含字母和数字。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 是否同时提取资金：NOT_APPLY_WITHDRAW 或 APPLY_WITHDRAW。
        /// </summary>
        public string withdraw { get; set; }

        /// <summary>
        /// 申请提现时使用的收款账号信息。
        /// </summary>
        public EcommerceCancelWithdrawPayeeInfo payee_info { get; set; }

        /// <summary>
        /// 付款申请材料；部分主体类型或经营证照已注吊撤时必填。
        /// </summary>
        public EcommerceCancelWithdrawProofMedia[] proof_medias { get; set; }

        /// <summary>
        /// 其他补充材料的媒体文件 ID，最多 10 个。
        /// </summary>
        public string[] additional_materials { get; set; }

        /// <summary>
        /// 展示在收款银行系统中的付款申请备注。
        /// </summary>
        public string remark { get; set; }
    }

    /// <summary>
    /// 新流程注销提现收款账号信息。
    /// </summary>
    public class EcommerceCancelWithdrawPayeeInfo
    {
        /// <summary>
        /// 账户类型：ACCOUNT_TYPE_CORPORATE 或 ACCOUNT_TYPE_PERSONAL。
        /// </summary>
        public string account_type { get; set; }

        /// <summary>
        /// 银行账户信息。
        /// </summary>
        public EcommerceCancelWithdrawBankAccountInfo bank_account_info { get; set; }

        /// <summary>
        /// 对私银行卡开户人的证件信息。
        /// </summary>
        public EcommerceCancelWithdrawIdentityInfo identity_info { get; set; }
    }

    /// <summary>
    /// 新流程注销提现银行账户信息。
    /// </summary>
    public class EcommerceCancelWithdrawBankAccountInfo
    {
        /// <summary>
        /// 经微信支付公钥或平台证书公钥加密的开户名称。
        /// </summary>
        public string account_name { get; set; }

        /// <summary>
        /// 开户银行名称。
        /// </summary>
        public string account_bank { get; set; }

        /// <summary>
        /// 开户银行联行号；是否必填取决于开户银行。
        /// </summary>
        public string bank_branch_id { get; set; }

        /// <summary>
        /// 开户银行全称（含支行）；可按官方规则与联行号二选一。
        /// </summary>
        public string bank_branch_name { get; set; }

        /// <summary>
        /// 经微信支付公钥或平台证书公钥加密的银行账号。
        /// </summary>
        public string account_number { get; set; }
    }

    /// <summary>
    /// 新流程注销提现对私账户开户人证件信息。
    /// </summary>
    public class EcommerceCancelWithdrawIdentityInfo
    {
        /// <summary>
        /// 证件类型，如 IDENTIFICATION_TYPE_ID_CARD。
        /// </summary>
        public string id_doc_type { get; set; }

        /// <summary>
        /// 经微信支付公钥或平台证书公钥加密的证件姓名。
        /// </summary>
        public string identification_name { get; set; }

        /// <summary>
        /// 经微信支付公钥或平台证书公钥加密的证件号码。
        /// </summary>
        public string identification_no { get; set; }
    }

    /// <summary>
    /// 新流程注销提现付款申请材料。
    /// </summary>
    public class EcommerceCancelWithdrawProofMedia
    {
        /// <summary>
        /// 材料类型，当前支持 WITHDRAWAL_APPLICATION。
        /// </summary>
        public string proof_media_type { get; set; }

        /// <summary>
        /// 通过图片上传接口取得的证明材料媒体文件 ID。
        /// </summary>
        public string proof_media { get; set; }
    }

    /// <summary>
    /// 旧流程二级商户注销申请数据。
    /// </summary>
    public class EcommerceLegacyCancelApplicationRequestData
    {
        /// <summary>
        /// 申请注销的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 商户自定义且在服务商维度唯一的注销申请单号。
        /// </summary>
        public string out_apply_no { get; set; }

        /// <summary>
        /// 注销申请材料列表。
        /// </summary>
        public EcommerceLegacyCancelApplicationMaterial[] application_info { get; set; }
    }

    /// <summary>
    /// 旧流程注销申请材料。
    /// </summary>
    public class EcommerceLegacyCancelApplicationMaterial
    {
        /// <summary>
        /// 材料类型；建议使用 SP_CANCEL_ACCOUNT_APPLICATION 或
        /// SUB_CANCEL_ACCOUNT_APPLICATION。
        /// </summary>
        public string application_type { get; set; }

        /// <summary>
        /// 通过注销图片上传接口取得的材料媒体文件 ID。
        /// </summary>
        public string application_media_id { get; set; }
    }

    /// <summary>
    /// 旧流程已注销商户可用余额提现申请数据。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawRequestData
    {
        /// <summary>
        /// 已注销且需要出款的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 出款子账户类型：BASIC_ACCOUNT、OPERATE_ACCOUNT 或 MARGIN_ACCOUNT。
        /// </summary>
        public string out_account_type { get; set; }

        /// <summary>
        /// 提现金额，单位为分，不能超过出款子账户可用余额。
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 商户自定义且唯一的提现申请单号，仅包含字母和数字。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 收款对象类型，如 CONTRIBUTION_MERCHANT、SERVICE_PROVIDER_MERCHANT、
        /// OTHER_MERCHANT 或 INDIVIDUAL。
        /// </summary>
        public string payee_type { get; set; }

        /// <summary>
        /// 收款对象对应的商户号；收款对象不是个人时必填。
        /// </summary>
        public string payee_mchid { get; set; }

        /// <summary>
        /// 收款账号及证件信息。
        /// </summary>
        public EcommerceLegacyCancelWithdrawPayeeInfo payee_info { get; set; }

        /// <summary>
        /// 付款申请证明材料。
        /// </summary>
        public EcommerceLegacyCancelWithdrawProofMediaList proof_media_list { get; set; }

        /// <summary>
        /// 其他补充材料。
        /// </summary>
        public EcommerceLegacyCancelWithdrawAdditionalMaterials additional_materials { get; set; }

        /// <summary>
        /// 方便平台说明原主体状态或其他特殊情况的备注。
        /// </summary>
        public string remark { get; set; }
    }

    /// <summary>
    /// 旧流程注销后提现收款账号信息。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawPayeeInfo
    {
        /// <summary>
        /// 账户类型：ACCOUNT_TYPE_CORPORATE 或 ACCOUNT_TYPE_PERSONAL。
        /// </summary>
        public string account_type { get; set; }

        /// <summary>
        /// 银行账户信息。
        /// </summary>
        public EcommerceLegacyCancelWithdrawBankAccountInfo bank_account_info { get; set; }

        /// <summary>
        /// 对私银行卡开户人的证件信息。
        /// </summary>
        public EcommerceLegacyCancelWithdrawIdentityInfo identity_info { get; set; }
    }

    /// <summary>
    /// 旧流程注销后提现银行账户信息。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawBankAccountInfo
    {
        /// <summary>
        /// 经微信支付公钥或平台证书公钥加密的开户名称。
        /// </summary>
        public string account_name { get; set; }

        /// <summary>
        /// 开户银行名称。
        /// </summary>
        public string account_bank { get; set; }

        /// <summary>
        /// 开户银行联行号；非直连银行可与支行全称二选一。
        /// </summary>
        public string bank_branch_id { get; set; }

        /// <summary>
        /// 开户银行全称（含支行）。旧流程字段名为 <c>bank_name</c>。
        /// </summary>
        public string bank_name { get; set; }

        /// <summary>
        /// 经微信支付公钥或平台证书公钥加密的银行账号。
        /// </summary>
        public string account_number { get; set; }
    }

    /// <summary>
    /// 旧流程注销后提现对私账户开户人证件信息。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawIdentityInfo
    {
        /// <summary>
        /// 证件类型；旧流程身份证枚举值为 IDENTIFICATION_TYPE_IDCARD。
        /// </summary>
        public string id_doc_type { get; set; }

        /// <summary>
        /// 经微信支付公钥或平台证书公钥加密的证件姓名。
        /// </summary>
        public string identification_name { get; set; }

        /// <summary>
        /// 经微信支付公钥或平台证书公钥加密的证件号码。
        /// </summary>
        public string identification_no { get; set; }
    }

    /// <summary>
    /// 旧流程注销后提现证明材料集合。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawProofMediaList
    {
        /// <summary>
        /// 收款对象相关的申请证明材料列表。
        /// </summary>
        public EcommerceLegacyCancelWithdrawProofMedia[] proof_payee_media { get; set; }
    }

    /// <summary>
    /// 旧流程注销后提现证明材料。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawProofMedia
    {
        /// <summary>
        /// 证明材料类型，如 BASIC_TRANSACTION_INFORMATION、LEGAL_ID_CARD 或
        /// WECHAT_PAY_WITHDRAWAL_APPLICATION_TYPE_6。
        /// </summary>
        public string proof_media_type { get; set; }

        /// <summary>
        /// 通过图片上传接口取得的证明材料媒体文件 ID。
        /// </summary>
        public string proof_media { get; set; }
    }

    /// <summary>
    /// 旧流程注销后提现补充材料。
    /// </summary>
    public class EcommerceLegacyCancelWithdrawAdditionalMaterials
    {
        /// <summary>
        /// 补充材料媒体文件 ID，最多 10 个。
        /// </summary>
        public string[] additional_media { get; set; }
    }
}
