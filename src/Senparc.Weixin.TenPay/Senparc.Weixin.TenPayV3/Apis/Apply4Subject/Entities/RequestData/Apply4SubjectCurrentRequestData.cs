#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：Apply4SubjectCurrentRequestData.cs
    文件功能描述：微信支付 V3 现行商户开户意愿确认请求模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Subject
{
    /// <summary>
    /// 现行商户开户意愿确认申请资料。
    /// </summary>
    public class Apply4SubjectApplicationRequestData
    {
        /// <summary>从业机构调用时必填的渠道商户号；渠道商户自行调用时留空。</summary>
        public string channel_id { get; set; }

        /// <summary>渠道商自定义的唯一业务申请编号，仅支持数字、字母和下划线。</summary>
        public string business_code { get; set; }

        /// <summary>接收开户信息和日常管理信息的商户联系人。</summary>
        public Apply4SubjectApplicationContactInfo contact_info { get; set; }

        /// <summary>营业执照、登记证书或小微商户经营材料等主体信息。</summary>
        public Apply4SubjectApplicationSubjectInfo subject_info { get; set; }

        /// <summary>法定代表人、经营者或经办人的身份信息。</summary>
        public Apply4SubjectApplicationIdentificationInfo identification_info { get; set; }

        /// <summary>待确认商户号等补充材料。</summary>
        public Apply4SubjectApplicationAdditionInfo addition_info { get; set; }

        /// <summary>最终受益人信息列表；官方已标记为即将下线，保留用于存量兼容。</summary>
        public List<Apply4SubjectApplicationUboInfo> ubo_info_list { get; set; }
    }

    /// <summary>
    /// 商户开户联系人信息。
    /// </summary>
    public class Apply4SubjectApplicationContactInfo
    {
        /// <summary>联系人姓名，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string name { get; set; }

        /// <summary>联系人手机号，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string mobile { get; set; }

        /// <summary>联系人证件号码；联系人为经办人时按官方要求填写敏感信息。</summary>
        public string id_card_number { get; set; }

        /// <summary>联系人类型：法定代表人 LEGAL 或经办人 SUPER。</summary>
        public string contact_type { get; set; }

        /// <summary>经办人证件类型。</summary>
        public string contact_id_doc_type { get; set; }

        /// <summary>经办人证件正面照片 MediaID。</summary>
        public string contact_id_doc_copy { get; set; }

        /// <summary>经办人证件反面照片 MediaID；护照等无反面的证件可不填。</summary>
        public string contact_id_doc_copy_back { get; set; }

        /// <summary>经办人证件有效期开始日期，格式为 yyyy-MM-dd。</summary>
        public string contact_period_begin { get; set; }

        /// <summary>经办人证件有效期结束日期，格式为 yyyy-MM-dd 或“长期”。</summary>
        public string contact_period_end { get; set; }
    }

    /// <summary>
    /// 商户开户主体信息。
    /// </summary>
    public class Apply4SubjectApplicationSubjectInfo
    {
        /// <summary>主体类型，例如 SUBJECT_TYPE_ENTERPRISE 或 SUBJECT_TYPE_MICRO。</summary>
        public string subject_type { get; set; }

        /// <summary>是否为金融机构；未填写时微信支付按 false 处理。</summary>
        public bool? is_finance_institution { get; set; }

        /// <summary>企业或个体工商户的营业执照信息。</summary>
        public Apply4SubjectBusinessLicenceInfo business_licence_info { get; set; }

        /// <summary>政府机关、事业单位或社会组织的登记证书信息。</summary>
        public Apply4SubjectCertificateInfo certificate_info { get; set; }

        /// <summary>政府机关或事业单位的单位证明函照片 MediaID。</summary>
        public string company_prove_copy { get; set; }

        /// <summary>小微商户的经营场景辅助证明材料。</summary>
        public Apply4SubjectAssistProveInfo assist_prove_info { get; set; }

        /// <summary>特殊行业经营许可证列表，最多填写 5 个行业。</summary>
        public List<Apply4SubjectSpecialOperationInfo> special_operation_list { get; set; }

        /// <summary>金融机构许可证信息。</summary>
        public Apply4SubjectFinanceInstitutionInfo finance_institution_info { get; set; }
    }

    /// <summary>
    /// 企业或个体工商户营业执照信息。
    /// </summary>
    public class Apply4SubjectBusinessLicenceInfo
    {
        /// <summary>营业执照注册号或统一社会信用代码。</summary>
        public string licence_number { get; set; }

        /// <summary>营业执照照片 MediaID。</summary>
        public string licence_copy { get; set; }

        /// <summary>营业执照上的商户名称。</summary>
        public string merchant_name { get; set; }

        /// <summary>营业执照上的经营者或法定代表人姓名。</summary>
        public string legal_person { get; set; }

        /// <summary>营业执照注册地址。</summary>
        public string company_address { get; set; }

        /// <summary>营业执照有效期 JSON 数组字符串，例如 ["2017-10-28","长期"]。</summary>
        public string licence_valid_date { get; set; }
    }

    /// <summary>
    /// 政府机关、事业单位或社会组织登记证书信息。
    /// </summary>
    public class Apply4SubjectCertificateInfo
    {
        /// <summary>登记证书类型，例如 CERTIFICATE_TYPE_2388。</summary>
        public string cert_type { get; set; }

        /// <summary>登记证书编号。</summary>
        public string cert_number { get; set; }

        /// <summary>登记证书照片 MediaID。</summary>
        public string cert_copy { get; set; }

        /// <summary>登记证书上的商户名称。</summary>
        public string merchant_name { get; set; }

        /// <summary>登记证书上的法定代表人姓名。</summary>
        public string legal_person { get; set; }

        /// <summary>登记证书注册地址。</summary>
        public string company_address { get; set; }

        /// <summary>登记证书有效期 JSON 数组字符串。</summary>
        public string cert_valid_date { get; set; }
    }

    /// <summary>
    /// 小微商户经营场景辅助证明。
    /// </summary>
    public class Apply4SubjectAssistProveInfo
    {
        /// <summary>小微经营类型：门店、流动经营或线上交易。</summary>
        public string micro_biz_type { get; set; }

        /// <summary>门店、服务或线上店铺名称。</summary>
        public string store_name { get; set; }

        /// <summary>门店或经营所在地的省市编码。</summary>
        public string store_address_code { get; set; }

        /// <summary>经营地址；线上交易场景填写电商平台名称。</summary>
        public string store_address { get; set; }

        /// <summary>门店门头、岗亭或出入闸口照片 MediaID。</summary>
        public string store_header_copy { get; set; }

        /// <summary>店内环境或经营场景照片 MediaID。</summary>
        public string store_indoor_copy { get; set; }
    }

    /// <summary>
    /// 特殊行业经营许可证信息。
    /// </summary>
    public class Apply4SubjectSpecialOperationInfo
    {
        /// <summary>微信支付特殊行业对照表中的行业类目 ID。</summary>
        public int category_id { get; set; }

        /// <summary>经营许可证资质照片 MediaID 列表，最多 5 张。</summary>
        public List<string> operation_copy_list { get; set; }
    }

    /// <summary>
    /// 金融机构许可证信息。
    /// </summary>
    public class Apply4SubjectFinanceInstitutionInfo
    {
        /// <summary>金融机构类型，例如 BANK_AGENT、PAYMENT_AGENT 或 INSURANCE。</summary>
        public string finance_type { get; set; }

        /// <summary>金融机构许可证照片 MediaID 列表，最多 5 张。</summary>
        public List<string> finance_license_pics { get; set; }
    }

    /// <summary>
    /// 商户法定代表人、经营者或经办人身份信息。
    /// </summary>
    public class Apply4SubjectApplicationIdentificationInfo
    {
        /// <summary>证件持有人类型，例如 LEGAL 或 SUPER。</summary>
        public string id_holder_type { get; set; }

        /// <summary>证件类型，例如 IDENTIFICATION_TYPE_IDCARD。</summary>
        public string identification_type { get; set; }

        /// <summary>证件姓名，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string identification_name { get; set; }

        /// <summary>证件号码，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string identification_number { get; set; }

        /// <summary>证件有效期 JSON 数组字符串。</summary>
        public string identification_valid_date { get; set; }

        /// <summary>证件正面或身份证人像面照片 MediaID。</summary>
        public string identification_front_copy { get; set; }

        /// <summary>证件反面或身份证国徽面照片 MediaID。</summary>
        public string identification_back_copy { get; set; }

        /// <summary>证件持有人为经办人时使用的法定代表人说明函 MediaID。</summary>
        public string authorize_letter_copy { get; set; }

        /// <summary>经营者或法定代表人是否为受益人；官方已标记为即将下线。</summary>
        public bool? owner { get; set; }

        /// <summary>证件居住地址，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string identification_address { get; set; }
    }

    /// <summary>
    /// 商户开户意愿确认补充材料。
    /// </summary>
    public class Apply4SubjectApplicationAdditionInfo
    {
        /// <summary>商家可在申请流程中提前授权的特约商户号列表，最多 20 个。</summary>
        public List<string> confirm_mchid_list { get; set; }
    }

    /// <summary>
    /// 最终受益人信息；官方已标记相关字段为即将下线。
    /// </summary>
    public class Apply4SubjectApplicationUboInfo
    {
        /// <summary>最终受益人证件类型。</summary>
        public string ubo_id_doc_type { get; set; }

        /// <summary>最终受益人证件正面照片 MediaID。</summary>
        public string ubo_id_doc_copy { get; set; }

        /// <summary>最终受益人证件反面照片 MediaID。</summary>
        public string ubo_id_doc_copy_back { get; set; }

        /// <summary>最终受益人姓名，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string ubo_id_doc_name { get; set; }

        /// <summary>最终受益人证件号码，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string ubo_id_doc_number { get; set; }

        /// <summary>最终受益人居住地址，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string ubo_id_doc_address { get; set; }

        /// <summary>最终受益人证件有效期开始日期，格式为 yyyy-MM-dd。</summary>
        public string ubo_period_begin { get; set; }

        /// <summary>最终受益人证件有效期结束日期，格式为 yyyy-MM-dd 或“长期”。</summary>
        public string ubo_period_end { get; set; }
    }
}
