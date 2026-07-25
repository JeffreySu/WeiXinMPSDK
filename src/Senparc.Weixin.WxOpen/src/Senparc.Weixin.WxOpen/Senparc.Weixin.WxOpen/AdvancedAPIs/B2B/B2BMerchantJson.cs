#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：B2BMerchantJson.cs
    文件功能描述：B2BMerchantJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.B2B
{
    #region 商户进件请求

    /// <summary>
    /// B2B 商户号进件请求。
    /// </summary>
    public class B2BRegisterMerchantRequest
    {
        /// <summary>
        /// 法人证件类型：0/1 大陆身份证，2 护照，3 香港通行证，4 澳门通行证，5 台湾通行证，6 外国人居留证，7 港澳居民证，8 台湾居民证。
        /// </summary>
        public int id_doc_type_num { get; set; }

        /// <summary>
        /// 经营者或法人身份证信息；证件类型为 0 或 1 时必填。
        /// </summary>
        public B2BIdCardInfo id_card_info { get; set; }

        /// <summary>
        /// 经营者或法人其他证件信息；证件类型不是 0 或 1 时必填。
        /// </summary>
        public B2BIdDocumentInfo id_doc_info { get; set; }

        /// <summary>
        /// 结算银行账户信息。
        /// </summary>
        public B2BMerchantAccountInfo account_info { get; set; }

        /// <summary>
        /// 超级管理员信息。
        /// </summary>
        public B2BMerchantContactInfo contact_info { get; set; }

        /// <summary>
        /// 营业执照信息。
        /// </summary>
        public B2BBusinessLicense business_license { get; set; }

        /// <summary>
        /// 商户简称。
        /// </summary>
        public string merchant_shortname { get; set; }

        /// <summary>
        /// 主体类型：0 个体户，1 企业。官方参数表误标为布尔类型，示例和枚举均使用数字。
        /// </summary>
        public int organization_type { get; set; }

        /// <summary>
        /// 行业特殊资质资料。
        /// </summary>
        public B2BMerchantQualification qualification { get; set; }

        /// <summary>
        /// 可选的补充说明。
        /// </summary>
        public string business_addition_desc { get; set; }

        /// <summary>
        /// 可选的补充材料图片 ID；多张图片应先拼接为一张再上传。
        /// </summary>
        public string business_addition_pics { get; set; }

        /// <summary>
        /// 支付方式：0 只开通微信支付，1 同时开通微信支付和银行转账。
        /// </summary>
        public int open_type { get; set; }

        /// <summary>
        /// 门头、商城、支付页及企业规模等补充信息。
        /// </summary>
        public B2BMerchantExtendedRegisterInfo ext_register_info { get; set; }

        /// <summary>
        /// 商户客户端 IP，支持 IPv4 和 IPv6。
        /// </summary>
        public string client_ip { get; set; }

        /// <summary>
        /// 是否忽略同主体校验。该字段出现在官方请求示例但未列入参数表，因此建模为可空值。
        /// </summary>
        public bool? ignore_same_entity { get; set; }

        /// <summary>
        /// 是否启动轮询任务。该字段出现在官方请求示例但未列入参数表，因此建模为可空值。
        /// </summary>
        public bool? launch_poll_task { get; set; }
    }

    /// <summary>
    /// 大陆身份证信息。
    /// </summary>
    public class B2BIdCardInfo
    {
        /// <summary>身份证人像面图片 ID。</summary>
        public string id_card_copy { get; set; }

        /// <summary>身份证国徽面图片 ID。</summary>
        public string id_card_national { get; set; }

        /// <summary>身份证姓名。</summary>
        public string id_card_name { get; set; }

        /// <summary>身份证号码。</summary>
        public string id_card_number { get; set; }

        /// <summary>身份证有效期结束日期，格式为 yyyy-MM-dd 或“长期”。</summary>
        public string id_card_valid_time { get; set; }

        /// <summary>身份证地址。</summary>
        public string id_card_address { get; set; }

        /// <summary>身份证有效期开始日期，格式为 yyyy-MM-dd。</summary>
        public string id_card_valid_time_begin { get; set; }
    }

    /// <summary>
    /// 非大陆身份证件信息。
    /// </summary>
    public class B2BIdDocumentInfo
    {
        /// <summary>证件姓名。</summary>
        public string id_doc_name { get; set; }

        /// <summary>证件号码。</summary>
        public string id_doc_number { get; set; }

        /// <summary>证件正面图片 ID。</summary>
        public string id_doc_copy { get; set; }

        /// <summary>证件有效期结束日期，格式为 yyyy-MM-dd 或“长期”。</summary>
        public string doc_period_end { get; set; }

        /// <summary>证件有效期开始日期，格式为 yyyy-MM-dd。</summary>
        public string doc_period_begin { get; set; }

        /// <summary>证件居住地址。</summary>
        public string id_doc_address { get; set; }

        /// <summary>证件反面图片 ID。</summary>
        public string id_doc_copy_back { get; set; }
    }

    /// <summary>
    /// 商户结算银行账户信息。
    /// </summary>
    public class B2BMerchantAccountInfo
    {
        /// <summary>账户类型：74 对公账户，75 对私账户。</summary>
        public string bank_account_type { get; set; }

        /// <summary>开户银行，例如“工商银行”。</summary>
        public string account_bank { get; set; }

        /// <summary>可选的开户名称。</summary>
        public string account_name { get; set; }

        /// <summary>开户银行省市编码。</summary>
        public string bank_address_code { get; set; }

        /// <summary>可选的开户银行联行号；与开户银行全称二选一。</summary>
        public string bank_branch_id { get; set; }

        /// <summary>开户银行全称，包含支行名称。</summary>
        public string bank_name { get; set; }

        /// <summary>银行账号。</summary>
        public string account_number { get; set; }
    }

    /// <summary>
    /// 商户超级管理员信息。
    /// </summary>
    public class B2BMerchantContactInfo
    {
        /// <summary>超级管理员类型：65 经营者或法人，66 经办人。</summary>
        public string contact_type { get; set; }

        /// <summary>超级管理员姓名。</summary>
        public string contact_name { get; set; }

        /// <summary>超级管理员证件类型；经办人场景需要上传。</summary>
        public string contact_id_doc_type { get; set; }

        /// <summary>超级管理员证件号码。</summary>
        public string contact_id_card_number { get; set; }

        /// <summary>超级管理员证件正面图片 ID。</summary>
        public string contact_id_doc_copy { get; set; }

        /// <summary>超级管理员证件反面图片 ID。</summary>
        public string contact_id_doc_copy_back { get; set; }

        /// <summary>超级管理员证件有效期开始日期。</summary>
        public string contact_id_doc_period_begin { get; set; }

        /// <summary>超级管理员证件有效期结束日期。</summary>
        public string contact_id_doc_period_end { get; set; }

        /// <summary>业务办理授权函图片 ID；经办人场景必填。</summary>
        public string business_authorization_letter { get; set; }

        /// <summary>超级管理员手机号。</summary>
        public string mobile_phone { get; set; }

        /// <summary>超级管理员邮箱；小微商户或个人卖家可选，其他主体必填。</summary>
        public string contact_email { get; set; }
    }

    /// <summary>
    /// 商户营业执照信息。
    /// </summary>
    public class B2BBusinessLicense
    {
        /// <summary>营业执照扫描件图片 ID。</summary>
        public string business_license_copy { get; set; }

        /// <summary>营业执照注册号。</summary>
        public string business_license_number { get; set; }

        /// <summary>商户名称。</summary>
        public string merchant_name { get; set; }

        /// <summary>经营者或法定代表人姓名。</summary>
        public string legal_person { get; set; }

        /// <summary>可选的注册地址；部分组织主体必填。</summary>
        public string company_address { get; set; }

        /// <summary>可选的营业期限；部分组织主体必填。</summary>
        public string business_time { get; set; }

        /// <summary>可选的登记证书类型。</summary>
        public string cert_type { get; set; }
    }

    /// <summary>
    /// 商户行业特殊资质资料。
    /// </summary>
    public class B2BMerchantQualification
    {
        /// <summary>行业特殊资质类型。</summary>
        public string qualification_type { get; set; }

        /// <summary>行业特殊资质图片 ID 数组编码后的 JSON 字符串；需要资质的行业必填。</summary>
        public string qualifications { get; set; }
    }

    /// <summary>
    /// 商户进件补充信息。
    /// </summary>
    public class B2BMerchantExtendedRegisterInfo
    {
        /// <summary>企业门头照图片 ID。</summary>
        public string door_head_file_id { get; set; }

        /// <summary>商城截图图片 ID。</summary>
        public string store_file_id { get; set; }

        /// <summary>确认订单付款界面截图图片 ID。</summary>
        public string online_pay_file_id { get; set; }

        /// <summary>企业规模：LARGE、MIDDLE、SMALL 或 TINY。</summary>
        public string merchant_scale { get; set; }

        /// <summary>可选的银行转账授权书图片 ID。</summary>
        public string authorization_letter_file_id { get; set; }
    }

    /// <summary>
    /// 商户号进件结果。
    /// </summary>
    public class B2BRegisterMerchantJsonResult : WxJsonResult
    {
        /// <summary>进件申请单号，可用于查询开通状态。</summary>
        public string order_no { get; set; }
    }

    /// <summary>
    /// 上传商户图片请求。
    /// </summary>
    public class B2BUploadMerchantFileRequest
    {
        /// <summary>原始文件名。</summary>
        public string file_name { get; set; }

        /// <summary>文件二进制内容的 Base64 编码字符串。</summary>
        public string file { get; set; }
    }

    /// <summary>
    /// 上传商户图片结果。
    /// </summary>
    public class B2BUploadMerchantFileJsonResult : WxJsonResult
    {
        /// <summary>微信返回的商户图片文件 ID。</summary>
        public string file_id { get; set; }
    }

    #endregion

    #region 商户状态与银行转账

    /// <summary>
    /// 查询商户号开通状态请求。
    /// </summary>
    public class B2BGetMerchantApplicationRequest
    {
        /// <summary>可选的指定进件申请单号；不填时查询当前小程序全部进件单。</summary>
        public string out_registration_id { get; set; }

        /// <summary>可选的分页起始偏移量。</summary>
        public int? page_index { get; set; }

        /// <summary>可选的分页数量限制。</summary>
        public int? page_size { get; set; }
    }

    /// <summary>
    /// 微信支付进件账户验证信息。
    /// </summary>
    public class B2BMerchantAccountValidation
    {
        /// <summary>待验证账户名称。</summary>
        public string account_name { get; set; }

        /// <summary>待验证账号。</summary>
        public string account_no { get; set; }

        /// <summary>需要支付的验证金额。</summary>
        public decimal pay_amount { get; set; }

        /// <summary>收款验证账号。</summary>
        public string destination_account_number { get; set; }

        /// <summary>收款验证账户名。</summary>
        public string destination_account_name { get; set; }

        /// <summary>收款验证银行。</summary>
        public string destination_account_bank { get; set; }

        /// <summary>开户城市。</summary>
        public string city { get; set; }

        /// <summary>汇款备注。</summary>
        public string remark { get; set; }

        /// <summary>账户验证截止时间。</summary>
        public string deadline { get; set; }
    }

    /// <summary>
    /// 商户进件驳回详情。
    /// </summary>
    public class B2BMerchantAuditDetail
    {
        /// <summary>被驳回的参数名称。</summary>
        public string param_name { get; set; }

        /// <summary>驳回原因。</summary>
        public string reject_reason { get; set; }
    }

    /// <summary>
    /// 微信支付子商户进件状态。
    /// </summary>
    public class B2BSubMerchantRegistrationStatus
    {
        /// <summary>申请状态。</summary>
        public string applyment_state { get; set; }

        /// <summary>申请状态说明。</summary>
        public string applyment_state_desc { get; set; }

        /// <summary>签约状态。</summary>
        public string sign_state { get; set; }

        /// <summary>签约链接。</summary>
        public string sign_url { get; set; }

        /// <summary>微信支付子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>账户汇款验证信息；申请状态为 ACCOUNT_NEED_VERIFY 时返回。</summary>
        public B2BMerchantAccountValidation account_validation { get; set; }

        /// <summary>驳回原因详情；申请状态为 REJECTED 或 FROZEN 时返回。</summary>
        public IList<B2BMerchantAuditDetail> audit_detail { get; set; }

        /// <summary>法人验证链接。</summary>
        public string legal_validation_url { get; set; }
    }

    /// <summary>
    /// 商户进件内部响应。
    /// </summary>
    public class B2BMerchantApplicationInnerResponse
    {
        /// <summary>微信支付子商户进件状态。</summary>
        public B2BSubMerchantRegistrationStatus sub_merchant_registration_status { get; set; }
    }

    /// <summary>
    /// 银行转账开通状态。
    /// </summary>
    public class B2BBankTransferRegistrationStatus
    {
        /// <summary>银行转账进件状态。</summary>
        public int wqf_register_state { get; set; }

        /// <summary>银行转账进件状态说明。</summary>
        public string wqf_register_state_desc { get; set; }

        /// <summary>银行转账开通申请单号。</summary>
        public string request_no { get; set; }
    }

    /// <summary>
    /// 单条商户号开通申请状态。
    /// </summary>
    public class B2BMerchantApplication
    {
        /// <summary>总体状态：0 初始化，1 资料校验中，2 待账户验证，3 审核中，4 已驳回，5 待签约，6 完成，7 已冻结，8 已作废，9 完成前平台额外准备。</summary>
        public int status { get; set; }

        /// <summary>微信支付进件响应。</summary>
        public B2BMerchantApplicationInnerResponse inner_resp { get; set; }

        /// <summary>银行转账开通状态；申请开通银行转账时返回。</summary>
        public B2BBankTransferRegistrationStatus wqf_register_statement { get; set; }

        /// <summary>微信支付技术服务费率，单位为万分比。</summary>
        public decimal wx_pay_rate { get; set; }

        /// <summary>银行转账技术服务费率，单位为万分比。</summary>
        public decimal wqf_certified_rate { get; set; }

        /// <summary>小程序关联状态：1 申请中，2 失败，3 待商户超管同意，4 待小程序超管同意，5 关联中，6 已关联。</summary>
        public int bind_scene_status { get; set; }
    }

    /// <summary>
    /// 商户号开通状态查询结果。
    /// </summary>
    public class B2BGetMerchantApplicationJsonResult : WxJsonResult
    {
        /// <summary>进件申请列表。</summary>
        public IList<B2BMerchantApplication> list { get; set; }

        /// <summary>进件申请总数。</summary>
        public int total { get; set; }
    }

    /// <summary>
    /// 使用进件申请单号的请求。
    /// </summary>
    public class B2BOutRegistrationIdRequest
    {
        /// <summary>原进件申请单号。</summary>
        public string out_registration_id { get; set; }
    }

    /// <summary>
    /// 创建银行转账页面链接请求。
    /// </summary>
    public class B2BCreateBankTransferLinkRequest
    {
        /// <summary>银行转账开通申请单号。</summary>
        public string request_no { get; set; }
    }

    /// <summary>
    /// 银行转账页面链接结果。
    /// </summary>
    public class B2BCreateBankTransferLinkJsonResult : WxJsonResult
    {
        /// <summary>银行转账业务页面链接。</summary>
        public string url { get; set; }

        /// <summary>链接过期时间，ISO 8601 格式。</summary>
        public string expire_time { get; set; }
    }

    /// <summary>
    /// 当前小程序下的商户信息。
    /// </summary>
    public class B2BMerchantInfo
    {
        /// <summary>微信支付子商户号；只有微信支付状态为“完成”时才可使用。</summary>
        public string sub_mchid { get; set; }

        /// <summary>企业名称。</summary>
        public string company_name { get; set; }

        /// <summary>开户银行。</summary>
        public string bank_name { get; set; }

        /// <summary>脱敏银行账号，仅保留前两位和后两位。</summary>
        public string bank_account { get; set; }

        /// <summary>微信支付开通状态。</summary>
        public string wxpay_status { get; set; }

        /// <summary>银行转账开通状态。</summary>
        public string bank_transfer_status { get; set; }
    }

    /// <summary>
    /// 当前小程序下的商户信息查询结果。
    /// </summary>
    public class B2BGetMerchantInfoJsonResult : WxJsonResult
    {
        /// <summary>商户列表。</summary>
        public IList<B2BMerchantInfo> mch_list { get; set; }

        /// <summary>商户总数。</summary>
        public int total { get; set; }
    }

    /// <summary>
    /// 报名微信支付技术服务费优惠活动请求。
    /// </summary>
    public class B2BSetMerchantProfitRateRequest
    {
        /// <summary>微信支付子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>技术服务费率，单位为万分比，例如 40 表示 0.40%。</summary>
        public int profit_rate { get; set; }
    }

    /// <summary>
    /// 报名银行转账技术服务费优惠活动请求。
    /// </summary>
    public class B2BUpdateBankTransferFeeRequest
    {
        /// <summary>微信支付子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>认证门店费率分子，分母固定为 10000，取值范围为 22 至 40。</summary>
        public int certified_charge_fee_numerator { get; set; }
    }

    /// <summary>
    /// 仅包含微信支付子商户号的请求。
    /// </summary>
    public class B2BSubMerchantRequest
    {
        /// <summary>微信支付子商户号。</summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 银行转账技术服务费率结果。
    /// </summary>
    public class B2BBankTransferFeeJsonResult : WxJsonResult
    {
        /// <summary>商户名称。</summary>
        public string merchant_name { get; set; }

        /// <summary>认证门店费率分子，分母固定为 10000。</summary>
        public int certified_charge_fee_numerator { get; set; }

        /// <summary>未认证门店费率分子，分母固定为 10000。</summary>
        public int uncertified_charge_fee_numerator { get; set; }

        /// <summary>费率生效时间。该字段出现在官方返回示例但未列入参数表。</summary>
        public string effect_time { get; set; }

        /// <summary>费率过期时间。该字段出现在官方返回示例但未列入参数表。</summary>
        public string expire_time { get; set; }
    }

    #endregion
}
