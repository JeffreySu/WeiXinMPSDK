#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MedicalInsuranceModels.cs
    文件功能描述：微信支付 V3 医保自费混合支付强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.TenPayV3.Apis.MedicalInsurance
{
    /// <summary>
    /// 医保支付人或代付亲属的身份信息。姓名和证件摘要应先使用微信支付公钥或平台证书公钥加密。
    /// </summary>
    public class MedicalInsuranceIdentity
    {
        /// <summary>加密后的真实姓名。</summary>
        public string name { get; set; }

        /// <summary>加密后的证件号码摘要，目前身份证摘要使用 MD5。</summary>
        public string id_digest { get; set; }

        /// <summary>证件类型，例如 ID_CARD。</summary>
        public string card_type { get; set; }
    }

    /// <summary>
    /// 医保订单外的现金补充费用。
    /// </summary>
    public class MedicalInsuranceCashAddDetail
    {
        /// <summary>现金补充金额，单位为分。</summary>
        public long cash_add_fee { get; set; }

        /// <summary>现金补充类型，例如 FREIGHT。</summary>
        public string cash_add_type { get; set; }
    }

    /// <summary>
    /// 医保订单外的现金减免费用。
    /// </summary>
    public class MedicalInsuranceCashReduceDetail
    {
        /// <summary>现金减免金额，单位为分。</summary>
        public long cash_reduce_fee { get; set; }

        /// <summary>现金减免类型，例如 HOSPITAL_REDUCE。</summary>
        public string cash_reduce_type { get; set; }
    }

    /// <summary>
    /// 医保自费混合收款下单请求。普通商户不填写 sub_* 字段；服务商和间连模式按官方文档填写这些字段。
    /// </summary>
    public class MedicalInsuranceOrderRequestData
    {
        /// <summary>混合支付类型，例如 CASH_ONLY、INSURANCE_ONLY 或 CASH_AND_INSURANCE。</summary>
        public string mix_pay_type { get; set; }

        /// <summary>订单类型，例如 REG_PAY、DIAG_PAY 或 PHARMACY_PAY。</summary>
        public string order_type { get; set; }

        /// <summary>普通商户 AppId；服务商模式下为服务商 AppId。</summary>
        public string appid { get; set; }

        /// <summary>服务商或间连模式下的子商户 AppId。</summary>
        public string sub_appid { get; set; }

        /// <summary>服务商或间连模式下的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>普通商户用户 OpenId；服务商模式可与 sub_openid 二选一。</summary>
        public string openid { get; set; }

        /// <summary>用户在子商户 AppId 下的 OpenId。</summary>
        public string sub_openid { get; set; }

        /// <summary>支付人身份信息。</summary>
        public MedicalInsuranceIdentity payer { get; set; }

        /// <summary>是否替亲属支付。</summary>
        public bool? pay_for_relatives { get; set; }

        /// <summary>代付亲属身份信息；pay_for_relatives 为 true 时填写。</summary>
        public MedicalInsuranceIdentity relative { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>医疗机构订单号。</summary>
        public string serial_no { get; set; }

        /// <summary>医保局返回的支付单 ID。</summary>
        public string pay_order_id { get; set; }

        /// <summary>医保局返回的支付授权码。</summary>
        public string pay_auth_no { get; set; }

        /// <summary>用户定位信息，格式为“经度,纬度”。</summary>
        public string geo_location { get; set; }

        /// <summary>医保接入城市 ID。</summary>
        public string city_id { get; set; }

        /// <summary>医疗机构名称。</summary>
        public string med_inst_name { get; set; }

        /// <summary>医保局分配的医疗机构编码。</summary>
        public string med_inst_no { get; set; }

        /// <summary>医保下单时间，使用 RFC 3339 格式。</summary>
        public string med_ins_order_create_time { get; set; }

        /// <summary>订单总金额，单位为分。</summary>
        public long total_fee { get; set; }

        /// <summary>医保统筹支付金额，单位为分。</summary>
        public long? med_ins_gov_fee { get; set; }

        /// <summary>医保个账支付金额，单位为分。</summary>
        public long? med_ins_self_fee { get; set; }

        /// <summary>医保其他支付金额，单位为分。</summary>
        public long? med_ins_other_fee { get; set; }

        /// <summary>医保结算后需自费金额，单位为分。</summary>
        public long? med_ins_cash_fee { get; set; }

        /// <summary>实际需要用户微信支付的金额，单位为分。</summary>
        public long? wechat_pay_cash_fee { get; set; }

        /// <summary>现金补充费用列表。</summary>
        public IList<MedicalInsuranceCashAddDetail> cash_add_detail { get; set; }

        /// <summary>现金减免费用列表。</summary>
        public IList<MedicalInsuranceCashReduceDetail> cash_reduce_detail { get; set; }

        /// <summary>医保混合收款成功通知地址，必须为 HTTPS 且不能带查询串。</summary>
        public string callback_url { get; set; }

        /// <summary>自费预下单 ID；存在自费支付时填写。</summary>
        public string prepay_id { get; set; }

        /// <summary>医疗机构透传给医保的数据。</summary>
        public string passthrough_request_content { get; set; }

        /// <summary>医疗机构与微信医保约定的扩展字段。</summary>
        public string extends { get; set; }

        /// <summary>医疗机构附加数据。</summary>
        public string attach { get; set; }

        /// <summary>腾讯分配的医保支付渠道号。</summary>
        public string channel_no { get; set; }

        /// <summary>是否向医保局测试环境下单。</summary>
        public bool? med_ins_test_env { get; set; }
    }

    /// <summary>
    /// 医保自费混合订单返回结果，同时可作为收款成功通知解密后的资源模型。
    /// </summary>
    public class MedicalInsuranceOrderResultJson : ReturnJsonBase
    {
        /// <summary>医保自费混合订单号。</summary>
        public string mix_trade_no { get; set; }

        /// <summary>医保自费混合订单支付状态。</summary>
        public string mix_pay_status { get; set; }

        /// <summary>自费部分支付状态。</summary>
        public string self_pay_status { get; set; }

        /// <summary>医保部分支付状态。</summary>
        public string med_ins_pay_status { get; set; }

        /// <summary>订单支付时间，使用 RFC 3339 格式。</summary>
        public string paid_time { get; set; }

        /// <summary>医保局返回并透传给医疗机构的内容。</summary>
        public string passthrough_response_content { get; set; }

        /// <summary>混合支付类型。</summary>
        public string mix_pay_type { get; set; }

        /// <summary>订单类型。</summary>
        public string order_type { get; set; }

        /// <summary>普通商户或服务商 AppId。</summary>
        public string appid { get; set; }

        /// <summary>服务商或间连模式下的子商户 AppId。</summary>
        public string sub_appid { get; set; }

        /// <summary>服务商或间连模式下的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>普通商户用户 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>用户在子商户 AppId 下的 OpenId。</summary>
        public string sub_openid { get; set; }

        /// <summary>是否替亲属支付。</summary>
        public bool? pay_for_relatives { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>医疗机构订单号。</summary>
        public string serial_no { get; set; }

        /// <summary>医保局支付单 ID。</summary>
        public string pay_order_id { get; set; }

        /// <summary>医保局支付授权码。</summary>
        public string pay_auth_no { get; set; }

        /// <summary>用户定位信息。</summary>
        public string geo_location { get; set; }

        /// <summary>医保接入城市 ID。</summary>
        public string city_id { get; set; }

        /// <summary>医疗机构名称。</summary>
        public string med_inst_name { get; set; }

        /// <summary>医疗机构编码。</summary>
        public string med_inst_no { get; set; }

        /// <summary>医保下单时间。</summary>
        public string med_ins_order_create_time { get; set; }

        /// <summary>订单总金额，单位为分。</summary>
        public long? total_fee { get; set; }

        /// <summary>医保统筹支付金额，单位为分。</summary>
        public long? med_ins_gov_fee { get; set; }

        /// <summary>医保个账支付金额，单位为分。</summary>
        public long? med_ins_self_fee { get; set; }

        /// <summary>医保其他支付金额，单位为分。</summary>
        public long? med_ins_other_fee { get; set; }

        /// <summary>医保结算后需自费金额，单位为分。</summary>
        public long? med_ins_cash_fee { get; set; }

        /// <summary>微信支付金额，单位为分。</summary>
        public long? wechat_pay_cash_fee { get; set; }

        /// <summary>现金补充费用列表。</summary>
        public IList<MedicalInsuranceCashAddDetail> cash_add_detail { get; set; }

        /// <summary>现金减免费用列表。</summary>
        public IList<MedicalInsuranceCashReduceDetail> cash_reduce_detail { get; set; }

        /// <summary>通知地址。</summary>
        public string callback_url { get; set; }

        /// <summary>自费预下单 ID。</summary>
        public string prepay_id { get; set; }

        /// <summary>请求时透传给医保的数据。</summary>
        public string passthrough_request_content { get; set; }

        /// <summary>扩展字段。</summary>
        public string extends { get; set; }

        /// <summary>附加数据。</summary>
        public string attach { get; set; }

        /// <summary>医保支付渠道号。</summary>
        public string channel_no { get; set; }

        /// <summary>是否使用医保局测试环境。</summary>
        public bool? med_ins_test_env { get; set; }
    }

    /// <summary>
    /// 医保退款成功通知请求。
    /// </summary>
    public class MedicalInsuranceRefundNotifyRequestData
    {
        /// <summary>医保退款总金额，单位为分。</summary>
        public long med_refund_total_fee { get; set; }

        /// <summary>医保统筹退款金额，单位为分。</summary>
        public long med_refund_gov_fee { get; set; }

        /// <summary>医保个账退款金额，单位为分。</summary>
        public long med_refund_self_fee { get; set; }

        /// <summary>医保其他退款金额，单位为分。</summary>
        public long med_refund_other_fee { get; set; }

        /// <summary>医保退款成功时间，使用 RFC 3339 格式。</summary>
        public string refund_time { get; set; }

        /// <summary>医疗机构退款单号。</summary>
        public string out_refund_no { get; set; }
    }

    /// <summary>
    /// 小程序或 JSAPI 调起医保自费混合支付所需的前端参数。
    /// </summary>
    public class MedicalInsurancePayPackage
    {
        /// <summary>医保自费混合订单号。</summary>
        public string mixTradeNo { get; set; }

        /// <summary>JSAPI 场景使用的 AppId；小程序可不输出。</summary>
        public string appid { get; set; }

        /// <summary>支付签名时间戳；存在自费支付时输出。</summary>
        public string timeStamp { get; set; }

        /// <summary>支付签名随机串；存在自费支付时输出。</summary>
        public string nonceStr { get; set; }

        /// <summary>自费预支付包，格式为 prepay_id=***。</summary>
        public string package { get; set; }

        /// <summary>签名类型，当前固定为 RSA。</summary>
        public string signType { get; set; }

        /// <summary>使用 AppId、时间戳、随机串和 package 生成的支付签名。</summary>
        public string paySign { get; set; }
    }
}
