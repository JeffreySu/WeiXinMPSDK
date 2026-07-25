#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandApplymentModels.cs
    文件功能描述：微信支付 V3 服务商品牌入驻强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.TenPayV3.Apis.BrandApplyment
{
    /// <summary>
    /// 品牌入驻申请资料。
    /// </summary>
    public class BrandApplymentRequestData
    {
        /// <summary>服务商自定义的唯一业务申请编号，仅支持数字、字母和下划线。</summary>
        public string business_code { get; set; }

        /// <summary>负责品牌经营和授权确认的管理员信息。</summary>
        public BrandApplymentAdminInfo admin_info { get; set; }

        /// <summary>品牌所属企业或个体工商户的主体信息。</summary>
        public BrandApplymentSubjectInfo subject_info { get; set; }

        /// <summary>品牌名称和品牌 Logo 等基础信息。</summary>
        public BrandApplymentBasicInfo brand_basic_info { get; set; }

        /// <summary>品牌商标或无商标证明资料。</summary>
        public BrandApplymentTrademarkInfo trademark { get; set; }
    }

    /// <summary>
    /// 品牌管理员信息。
    /// </summary>
    public class BrandApplymentAdminInfo
    {
        /// <summary>管理员姓名，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string admin_name { get; set; }

        /// <summary>管理员证件类型，例如 IDENTIFICATION_TYPE_MAINLAND_ID_CARD。</summary>
        public string id_doc_type { get; set; }

        /// <summary>管理员证件号码，传输前需使用微信支付公钥或平台证书公钥加密。</summary>
        public string id_card_number { get; set; }
    }

    /// <summary>
    /// 品牌所属主体信息。
    /// </summary>
    public class BrandApplymentSubjectInfo
    {
        /// <summary>主体类型：企业 SUBJECT_TYPE_ENTERPRISE 或个体户 SUBJECT_TYPE_INDIVIDUAL。</summary>
        public string subject_type { get; set; }

        /// <summary>营业执照上的主体名称。</summary>
        public string subject_name { get; set; }

        /// <summary>由数字和大写英文字母组成的统一社会信用代码。</summary>
        public string unified_social_credit_code { get; set; }
    }

    /// <summary>
    /// 品牌基础信息。
    /// </summary>
    public class BrandApplymentBasicInfo
    {
        /// <summary>申请入驻的品牌名称，最长 30 个字符。</summary>
        public string brand_name { get; set; }

        /// <summary>通过图片上传接口获得的品牌 Logo MediaID。</summary>
        public string brand_logo { get; set; }
    }

    /// <summary>
    /// 品牌商标资料。
    /// </summary>
    public class BrandApplymentTrademarkInfo
    {
        /// <summary>是否已有商标：TRADEMARK_EXISTS 或 TRADEMARK_NONE。</summary>
        public string trademark_exists { get; set; }

        /// <summary>品牌名称对应的商标注册资料；选择有商标时填写。</summary>
        public BrandApplymentTrademarkCertificate trademark_registration_certificate { get; set; }

        /// <summary>品牌 Logo 对应的商标注册资料；Logo 已注册商标时填写。</summary>
        public BrandApplymentTrademarkCertificate logo_trademark_registration_certificate { get; set; }

        /// <summary>
        /// 无商标时的单张经营证明 MediaID。该字段仅供存量接入方兼容，
        /// 新接入方应使用 <see cref="no_trademark_addition_prove_list"/>。
        /// </summary>
        public string no_trademark_addition_prove { get; set; }

        /// <summary>无商标时的经营证明 MediaID 列表，可上传 1 至 5 张图片。</summary>
        public List<string> no_trademark_addition_prove_list { get; set; }
    }

    /// <summary>
    /// 品牌名称或 Logo 的商标注册证及授权书资料。
    /// </summary>
    public class BrandApplymentTrademarkCertificate
    {
        /// <summary>
        /// 单张商标注册证 MediaID。该字段仅供存量接入方兼容，
        /// 新接入方应使用 <see cref="certificate_list"/>。
        /// </summary>
        public string certificate { get; set; }

        /// <summary>商标注册时登记的名称。</summary>
        public string name { get; set; }

        /// <summary>商标注册号。</summary>
        public string number { get; set; }

        /// <summary>商标有效期开始日期，格式为 yyyy-MM-dd；仅供存量接入方兼容。</summary>
        public string valid_begin_time { get; set; }

        /// <summary>商标有效期结束日期，格式为 yyyy-MM-dd。</summary>
        public string valid_end_time { get; set; }

        /// <summary>商标国际分类，取值范围为 1 至 45。</summary>
        public string international_class { get; set; }

        /// <summary>商标注册人名称。</summary>
        public string holder { get; set; }

        /// <summary>
        /// 单张商标许可使用授权书 MediaID。该字段仅供存量接入方兼容，
        /// 新接入方应使用 <see cref="license_list"/>。
        /// </summary>
        public string license { get; set; }

        /// <summary>商标授权有效期开始日期，格式为 yyyy-MM-dd；仅供存量接入方兼容。</summary>
        public string authorization_begin_time { get; set; }

        /// <summary>商标授权有效期结束日期，格式为 yyyy-MM-dd。</summary>
        public string authorization_end_time { get; set; }

        /// <summary>商标注册证 MediaID 列表，可上传 1 至 5 张图片。</summary>
        public List<string> certificate_list { get; set; }

        /// <summary>商标许可使用授权书 MediaID 列表，可上传 1 至 5 张图片。</summary>
        public List<string> license_list { get; set; }
    }

    /// <summary>
    /// 撤销品牌入驻申请的请求参数。
    /// </summary>
    public class BrandApplymentCancelRequestData
    {
        /// <summary>服务商业务申请编号，与 applyment_id 二选一。</summary>
        public string business_code { get; set; }

        /// <summary>微信支付申请单号，与 business_code 二选一。</summary>
        public string applyment_id { get; set; }
    }

    /// <summary>
    /// 提交或撤销品牌入驻申请的返回结果。
    /// </summary>
    public class BrandApplymentResultJson : ReturnJsonBase
    {
        /// <summary>微信支付生成的申请单号。</summary>
        public string applyment_id { get; set; }

        /// <summary>服务商自定义的业务申请编号。</summary>
        public string business_code { get; set; }
    }

    /// <summary>
    /// 品牌入驻申请状态查询结果。
    /// </summary>
    public class BrandApplymentQueryResultJson : ReturnJsonBase
    {
        /// <summary>微信支付生成的申请单号。</summary>
        public string applyment_id { get; set; }

        /// <summary>服务商自定义的业务申请编号。</summary>
        public string business_code { get; set; }

        /// <summary>申请状态，例如 APPLYMENT_STATE_WAITING_AUDIT 或 APPLYMENT_STATE_FINISH。</summary>
        public string applyment_state { get; set; }

        /// <summary>对并行处理环节进行补充说明的申请状态描述。</summary>
        public string applyment_state_desc { get; set; }

        /// <summary>待主体或管理员确认时返回的授权确认二维码链接。</summary>
        public string authorization_confirmation_qr_code { get; set; }

        /// <summary>申请被审核驳回时返回的具体原因。</summary>
        public string reject_reason { get; set; }

        /// <summary>入驻完成后返回的品牌唯一 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 品牌入驻材料图片上传结果。
    /// </summary>
    public class BrandMediaUploadResultJson : ReturnJsonBase
    {
        /// <summary>微信支付返回的媒体文件标识，可用于品牌和商标材料字段。</summary>
        public string media_id { get; set; }
    }
}
