#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：Apply4SubCurrentRequestData.cs
    文件功能描述：微信支付 V3 特约商户进件现行请求模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐特约商户进件现行 8 项接口

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Sub
{
    /// <summary>
    /// 现行特约商户进件申请单请求数据。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012719997</para>
    /// </summary>
    public class Apply4SubCurrentApplymentRequestData
    {
        /// <summary>服务商自定义且唯一的业务申请编号。</summary>
        public string business_code { get; set; }

        /// <summary>商户超级管理员信息。</summary>
        public Apply4SubCurrentContactInfo contact_info { get; set; }

        /// <summary>营业执照、登记证书、主体负责人及最终受益人等主体资料。</summary>
        public Apply4SubCurrentSubjectInfo subject_info { get; set; }

        /// <summary>商户简称、客服电话和经营场景等经营资料。</summary>
        public Apply4SubCurrentBusinessInfo business_info { get; set; }

        /// <summary>行业、结算规则及优惠费率资料。</summary>
        public Apply4SubCurrentSettlementInfo settlement_info { get; set; }

        /// <summary>商户提现使用的结算银行账户。</summary>
        public Apply4SubCurrentBankAccountInfo bank_account_info { get; set; }

        /// <summary>根据审核需要提供的补充资料。</summary>
        public Apply4SubCurrentAdditionInfo addition_info { get; set; }
    }

    /// <summary>
    /// 特约商户超级管理员信息。
    /// </summary>
    public class Apply4SubCurrentContactInfo
    {
        /// <summary>超级管理员类型：LEGAL 或 SUPER。</summary>
        public string contact_type { get; set; }

        /// <summary>使用微信支付公钥或平台证书公钥加密的超级管理员姓名。</summary>
        public string contact_name { get; set; }

        /// <summary>经办人的证件类型。</summary>
        public string contact_id_doc_type { get; set; }

        /// <summary>使用微信支付公钥或平台证书公钥加密的经办人证件号码。</summary>
        public string contact_id_number { get; set; }

        /// <summary>经办人证件正面照片的媒体文件 ID。</summary>
        public string contact_id_doc_copy { get; set; }

        /// <summary>经办人证件反面照片的媒体文件 ID。</summary>
        public string contact_id_doc_copy_back { get; set; }

        /// <summary>经办人证件有效期开始日期，格式为 YYYY-MM-DD。</summary>
        public string contact_period_begin { get; set; }

        /// <summary>经办人证件有效期结束日期，格式为 YYYY-MM-DD 或“长期”。</summary>
        public string contact_period_end { get; set; }

        /// <summary>即将下线的业务办理授权函媒体文件 ID；通常无需传入。</summary>
        public string business_authorization_letter { get; set; }

        /// <summary>可选的超级管理员微信 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>使用微信支付公钥或平台证书公钥加密的联系手机。</summary>
        public string mobile_phone { get; set; }

        /// <summary>使用微信支付公钥或平台证书公钥加密的联系邮箱。</summary>
        public string contact_email { get; set; }
    }

    /// <summary>
    /// 特约商户主体资料。
    /// </summary>
    public class Apply4SubCurrentSubjectInfo
    {
        /// <summary>主体类型，例如 SUBJECT_TYPE_INDIVIDUAL 或 SUBJECT_TYPE_ENTERPRISE。</summary>
        public string subject_type { get; set; }

        /// <summary>是否为金融机构；不传时微信支付默认为 false。</summary>
        public bool? finance_institution { get; set; }

        /// <summary>个体工商户或企业的营业执照信息。</summary>
        public Apply4SubCurrentBusinessLicenseInfo business_license_info { get; set; }

        /// <summary>政府机关、事业单位或社会组织的登记证书信息。</summary>
        public Apply4SubCurrentCertificateInfo certificate_info { get; set; }

        /// <summary>金融机构许可证信息。</summary>
        public Apply4SubCurrentFinanceInstitutionInfo finance_institution_info { get; set; }

        /// <summary>经营者、法定代表人或经办人的身份证件信息。</summary>
        public Apply4SubCurrentIdentityInfo identity_info { get; set; }

        /// <summary>最终受益人列表。</summary>
        public Apply4SubCurrentUboInfo[] ubo_info_list { get; set; }
    }

    /// <summary>
    /// 营业执照信息。
    /// </summary>
    public class Apply4SubCurrentBusinessLicenseInfo
    {
        /// <summary>营业执照照片的媒体文件 ID。</summary>
        public string license_copy { get; set; }

        /// <summary>营业执照注册号或统一社会信用代码。</summary>
        public string license_number { get; set; }

        /// <summary>营业执照上的商户名称。</summary>
        public string merchant_name { get; set; }

        /// <summary>个体户经营者或企业法定代表人姓名。</summary>
        public string legal_person { get; set; }

        /// <summary>营业执照注册地址。</summary>
        public string license_address { get; set; }

        /// <summary>营业执照有效期限开始日期。</summary>
        public string period_begin { get; set; }

        /// <summary>营业执照有效期限结束日期，长期有效时填写“长期”。</summary>
        public string period_end { get; set; }
    }

    /// <summary>
    /// 政府机关、事业单位或社会组织登记证书信息。
    /// </summary>
    public class Apply4SubCurrentCertificateInfo
    {
        /// <summary>登记证书照片的媒体文件 ID。</summary>
        public string cert_copy { get; set; }

        /// <summary>官方定义的登记证书类型。</summary>
        public string cert_type { get; set; }

        /// <summary>登记证书号码。</summary>
        public string cert_number { get; set; }

        /// <summary>登记证书上的商户名称。</summary>
        public string merchant_name { get; set; }

        /// <summary>登记证书上的注册地址。</summary>
        public string company_address { get; set; }

        /// <summary>登记证书上的法定代表人姓名。</summary>
        public string legal_person { get; set; }

        /// <summary>登记证书有效期限开始日期。</summary>
        public string period_begin { get; set; }

        /// <summary>登记证书有效期限结束日期，长期有效时填写“长期”。</summary>
        public string period_end { get; set; }

        /// <summary>政府机关或事业单位的单位证明函媒体文件 ID。</summary>
        public string certificate_letter_copy { get; set; }
    }

    /// <summary>
    /// 金融机构许可证信息。
    /// </summary>
    public class Apply4SubCurrentFinanceInstitutionInfo
    {
        /// <summary>金融机构类型，例如 BANK_AGENT、PAYMENT_AGENT 或 INSURANCE。</summary>
        public string finance_type { get; set; }

        /// <summary>金融机构许可证图片的媒体文件 ID 列表。</summary>
        public string[] finance_license_pics { get; set; }
    }

    /// <summary>
    /// 经营者、法定代表人或经办人的身份证件信息。
    /// </summary>
    public class Apply4SubCurrentIdentityInfo
    {
        /// <summary>证件持有人类型：LEGAL 或 SUPER。</summary>
        public string id_holder_type { get; set; }

        /// <summary>官方定义的证件类型。</summary>
        public string id_doc_type { get; set; }

        /// <summary>使用经办人证件时提交的法定代表人说明函媒体文件 ID。</summary>
        public string authorize_letter_copy { get; set; }

        /// <summary>中国大陆居民身份证资料。</summary>
        public Apply4SubCurrentIdCardInfo id_card_info { get; set; }

        /// <summary>身份证以外的其他证件资料。</summary>
        public Apply4SubCurrentIdDocInfo id_doc_info { get; set; }

        /// <summary>经营者或法定代表人是否为最终受益人。</summary>
        public bool? owner { get; set; }
    }

    /// <summary>
    /// 中国大陆居民身份证资料。
    /// </summary>
    public class Apply4SubCurrentIdCardInfo
    {
        /// <summary>身份证人像面照片的媒体文件 ID。</summary>
        public string id_card_copy { get; set; }

        /// <summary>身份证国徽面照片的媒体文件 ID。</summary>
        public string id_card_national { get; set; }

        /// <summary>加密后的身份证姓名。</summary>
        public string id_card_name { get; set; }

        /// <summary>加密后的身份证号码。</summary>
        public string id_card_number { get; set; }

        /// <summary>加密后的身份证居住地址。</summary>
        public string id_card_address { get; set; }

        /// <summary>身份证有效期开始日期。</summary>
        public string card_period_begin { get; set; }

        /// <summary>身份证有效期结束日期，长期有效时填写“长期”。</summary>
        public string card_period_end { get; set; }
    }

    /// <summary>
    /// 身份证以外的其他身份证件资料。
    /// </summary>
    public class Apply4SubCurrentIdDocInfo
    {
        /// <summary>证件正面照片的媒体文件 ID。</summary>
        public string id_doc_copy { get; set; }

        /// <summary>证件反面照片的媒体文件 ID；护照等无反面证件可不填。</summary>
        public string id_doc_copy_back { get; set; }

        /// <summary>加密后的证件姓名。</summary>
        public string id_doc_name { get; set; }

        /// <summary>加密后的证件号码。</summary>
        public string id_doc_number { get; set; }

        /// <summary>加密后的证件居住地址。</summary>
        public string id_doc_address { get; set; }

        /// <summary>证件有效期开始日期。</summary>
        public string doc_period_begin { get; set; }

        /// <summary>证件有效期结束日期，长期有效时填写“长期”。</summary>
        public string doc_period_end { get; set; }
    }

    /// <summary>
    /// 最终受益人身份证件资料。
    /// </summary>
    public class Apply4SubCurrentUboInfo
    {
        /// <summary>最终受益人的证件类型。</summary>
        public string ubo_id_doc_type { get; set; }

        /// <summary>最终受益人证件正面照片的媒体文件 ID。</summary>
        public string ubo_id_doc_copy { get; set; }

        /// <summary>最终受益人证件反面照片的媒体文件 ID。</summary>
        public string ubo_id_doc_copy_back { get; set; }

        /// <summary>加密后的最终受益人证件姓名。</summary>
        public string ubo_id_doc_name { get; set; }

        /// <summary>加密后的最终受益人证件号码。</summary>
        public string ubo_id_doc_number { get; set; }

        /// <summary>加密后的最终受益人证件居住地址。</summary>
        public string ubo_id_doc_address { get; set; }

        /// <summary>最终受益人证件有效期开始日期。</summary>
        public string ubo_period_begin { get; set; }

        /// <summary>最终受益人证件有效期结束日期，长期有效时填写“长期”。</summary>
        public string ubo_period_end { get; set; }
    }

    /// <summary>
    /// 特约商户经营资料。
    /// </summary>
    public class Apply4SubCurrentBusinessInfo
    {
        /// <summary>展示给消费者的商户简称。</summary>
        public string merchant_shortname { get; set; }

        /// <summary>交易记录中展示的客服电话。</summary>
        public string service_phone { get; set; }

        /// <summary>商户实际经营场景资料。</summary>
        public Apply4SubCurrentSalesInfo sales_info { get; set; }
    }

    /// <summary>
    /// 特约商户经营场景资料。
    /// </summary>
    public class Apply4SubCurrentSalesInfo
    {
        /// <summary>经营场景类型列表，例如 SALES_SCENES_STORE 或 SALES_SCENES_APP。</summary>
        public string[] sales_scenes_type { get; set; }

        /// <summary>线下场所经营资料。</summary>
        public Apply4SubCurrentBizStoreInfo biz_store_info { get; set; }

        /// <summary>服务号或公众号经营资料。</summary>
        public Apply4SubCurrentMpInfo mp_info { get; set; }

        /// <summary>小程序经营资料。</summary>
        public Apply4SubCurrentMiniProgramInfo mini_program_info { get; set; }

        /// <summary>App 经营资料。</summary>
        public Apply4SubCurrentAppInfo app_info { get; set; }

        /// <summary>互联网网站经营资料。</summary>
        public Apply4SubCurrentWebInfo web_info { get; set; }

        /// <summary>企业微信经营资料。</summary>
        public Apply4SubCurrentWeworkInfo wework_info { get; set; }
    }

    /// <summary>
    /// 线下场所经营资料。
    /// </summary>
    public class Apply4SubCurrentBizStoreInfo
    {
        /// <summary>线下场所名称。</summary>
        public string biz_store_name { get; set; }

        /// <summary>线下场所省市编码。</summary>
        public string biz_address_code { get; set; }

        /// <summary>线下场所详细地址。</summary>
        public string biz_store_address { get; set; }

        /// <summary>门头照片媒体文件 ID 列表。</summary>
        public string[] store_entrance_pic { get; set; }

        /// <summary>场所内部照片媒体文件 ID 列表。</summary>
        public string[] indoor_pic { get; set; }

        /// <summary>线下场所对应的商家 AppID。</summary>
        public string biz_sub_appid { get; set; }
    }

    /// <summary>
    /// 服务号或公众号经营资料。
    /// </summary>
    public class Apply4SubCurrentMpInfo
    {
        /// <summary>服务商服务号或公众号 AppID。</summary>
        public string mp_appid { get; set; }

        /// <summary>商家服务号或公众号 AppID。</summary>
        public string mp_sub_appid { get; set; }

        /// <summary>展示商品或服务的页面截图媒体文件 ID 列表。</summary>
        public string[] mp_pics { get; set; }
    }

    /// <summary>
    /// 小程序经营资料。
    /// </summary>
    public class Apply4SubCurrentMiniProgramInfo
    {
        /// <summary>服务商小程序 AppID。</summary>
        public string mini_program_appid { get; set; }

        /// <summary>商家小程序 AppID。</summary>
        public string mini_program_sub_appid { get; set; }

        /// <summary>小程序页面截图媒体文件 ID 列表。</summary>
        public string[] mini_program_pics { get; set; }
    }

    /// <summary>
    /// App 经营资料。
    /// </summary>
    public class Apply4SubCurrentAppInfo
    {
        /// <summary>服务商应用 AppID。</summary>
        public string app_appid { get; set; }

        /// <summary>商家应用 AppID。</summary>
        public string app_sub_appid { get; set; }

        /// <summary>App 首页、尾页、应用内及支付页截图媒体文件 ID 列表。</summary>
        public string[] app_pics { get; set; }
    }

    /// <summary>
    /// 互联网网站经营资料。
    /// </summary>
    public class Apply4SubCurrentWebInfo
    {
        /// <summary>已完成 ICP 备案的互联网网站域名。</summary>
        public string domain { get; set; }

        /// <summary>备案主体与申请主体不一致时的网站授权函媒体文件 ID。</summary>
        public string web_authorisation { get; set; }

        /// <summary>互联网网站对应的商家 AppID。</summary>
        public string web_appid { get; set; }
    }

    /// <summary>
    /// 企业微信经营资料。
    /// </summary>
    public class Apply4SubCurrentWeworkInfo
    {
        /// <summary>商家企业微信 CorpID。</summary>
        public string sub_corp_id { get; set; }

        /// <summary>企业微信页面截图媒体文件 ID 列表。</summary>
        public string[] wework_pics { get; set; }
    }

    /// <summary>
    /// 特约商户结算规则和优惠费率资料。
    /// </summary>
    public class Apply4SubCurrentSettlementInfo
    {
        /// <summary>入驻结算规则 ID。</summary>
        public string settlement_id { get; set; }

        /// <summary>费率结算规则所对应的行业名称。</summary>
        public string qualification_type { get; set; }

        /// <summary>特殊资质图片媒体文件 ID 列表。</summary>
        public string[] qualifications { get; set; }

        /// <summary>优惠费率活动 ID。</summary>
        public string activities_id { get; set; }

        /// <summary>旧版统一优惠活动费率值；应优先使用借记卡和信用卡独立费率字段。</summary>
        public string activities_rate { get; set; }

        /// <summary>优惠费率活动补充材料媒体文件 ID 列表。</summary>
        public string[] activities_additions { get; set; }

        /// <summary>借记卡支付使用的非信用卡活动费率值。</summary>
        public string debit_activities_rate { get; set; }

        /// <summary>信用卡活动费率值。</summary>
        public string credit_activities_rate { get; set; }
    }

    /// <summary>
    /// 特约商户进件时提交的结算银行账户。
    /// </summary>
    public class Apply4SubCurrentBankAccountInfo
    {
        /// <summary>账户类型：BANK_ACCOUNT_TYPE_CORPORATE 或 BANK_ACCOUNT_TYPE_PERSONAL。</summary>
        public string bank_account_type { get; set; }

        /// <summary>使用微信支付公钥或平台证书公钥加密的开户名称。</summary>
        public string account_name { get; set; }

        /// <summary>开户银行名称。</summary>
        public string account_bank { get; set; }

        /// <summary>即将下线的开户银行省市编码；通常无需传入。</summary>
        public string bank_address_code { get; set; }

        /// <summary>开户银行联行号。</summary>
        public string bank_branch_id { get; set; }

        /// <summary>开户银行全称，包含支行名称。</summary>
        public string bank_name { get; set; }

        /// <summary>使用微信支付公钥或平台证书公钥加密的银行账号。</summary>
        public string account_number { get; set; }
    }

    /// <summary>
    /// 特约商户进件补充资料。
    /// </summary>
    public class Apply4SubCurrentAdditionInfo
    {
        /// <summary>法定代表人或负责人亲笔签署的开户承诺函媒体文件 ID。</summary>
        public string legal_person_commitment { get; set; }

        /// <summary>通过视频上传接口取得的法定代表人开户意愿视频媒体文件 ID。</summary>
        public string legal_person_video { get; set; }

        /// <summary>其他补充图片或 PDF 文件的媒体文件 ID 列表。</summary>
        public string[] business_addition_pics { get; set; }

        /// <summary>资金来源、资金用途或其他特殊情况补充说明。</summary>
        public string business_addition_msg { get; set; }
    }

    /// <summary>
    /// 修改特约商户结算账户请求数据。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012761102</para>
    /// </summary>
    public class Apply4SubModifySettlementRequestData
    {
        /// <summary>账户类型：ACCOUNT_TYPE_BUSINESS 或 ACCOUNT_TYPE_PRIVATE。</summary>
        public string account_type { get; set; }

        /// <summary>开户银行名称。</summary>
        public string account_bank { get; set; }

        /// <summary>开户银行全称，包含支行名称。</summary>
        public string bank_name { get; set; }

        /// <summary>开户银行联行号。</summary>
        public string bank_branch_id { get; set; }

        /// <summary>使用微信支付公钥或平台证书公钥加密的新银行账号。</summary>
        public string account_number { get; set; }

        /// <summary>需要修改时填写的加密开户名称。</summary>
        public string account_name { get; set; }
    }
}
