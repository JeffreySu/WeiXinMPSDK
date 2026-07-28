#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CustomsRequestData.cs
    文件功能描述：微信支付海关报关 V2 请求模型与 XML 签名生成


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v1.20.0 新增报关、报关查询及重新申报请求模型

----------------------------------------------------------------*/

using System;
using System.Globalization;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>微信支付海关报关 V2 请求的公共字段。</summary>
    public abstract class CustomsRequestDataBase
    {
        /// <summary>微信分配的公众账号或小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>微信支付分配的商户号。</summary>
        public string mch_id { get; set; }

        /// <summary>海关编号，例如 GUANGZHOU_ZS。</summary>
        public string customs { get; set; }

        /// <summary>
        /// 使用商户 API 密钥生成带签名的 XML 请求正文。
        /// </summary>
        /// <param name="key">商户 API 密钥，仅用于签名，不写入请求正文。</param>
        /// <returns>可直接提交到微信支付海关接口的 XML。</returns>
        public string ToXml(string key)
        {
            Validate(key);
            var handler = new RequestHandler();
            SetParameters(handler);
            handler.SetParameter("sign", handler.CreateMd5Sign("key", key));
            return handler.ParseXML();
        }

        /// <summary>校验公共必填字段和商户 API 密钥。</summary>
        protected virtual void Validate(string key)
        {
            Require(appid, "appid");
            Require(mch_id, "mch_id");
            Require(customs, "customs");
            Require(key, "key");
        }

        /// <summary>向签名处理器写入公共字段。</summary>
        protected virtual void SetParameters(RequestHandler handler)
        {
            handler.SetParameter("appid", appid);
            handler.SetParameter("mch_id", mch_id);
            handler.SetParameter("customs", customs);
        }

        /// <summary>校验字符串必填参数。</summary>
        protected static void Require(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(parameterName + " 不能为空。", parameterName);
            }
        }

        /// <summary>按不变区域格式写入可选整数。</summary>
        protected static void SetOptionalInt(RequestHandler handler,
            string name, int? value)
        {
            if (value.HasValue)
            {
                handler.SetParameter(name,
                    value.Value.ToString(CultureInfo.InvariantCulture));
            }
        }
    }

    /// <summary>
    /// 支付订单海关报关请求。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4011985151</para>
    /// </summary>
    public class CustomsDeclareOrderRequestData : CustomsRequestDataBase
    {
        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户在海关备案的编号。</summary>
        public string mch_customs_no { get; set; }

        /// <summary>应付关税，单位为分。</summary>
        public int? duty { get; set; }

        /// <summary>申报类型：ADD 为新增，MODIFY 为修改。</summary>
        public string action_type { get; set; }

        /// <summary>商户子订单号。</summary>
        public string sub_order_no { get; set; }

        /// <summary>币种，符合 ISO 4217 标准，默认人民币 CNY。</summary>
        public string fee_type { get; set; }

        /// <summary>子订单金额，单位为分。</summary>
        public int? order_fee { get; set; }

        /// <summary>物流费用，单位为分。</summary>
        public int? transport_fee { get; set; }

        /// <summary>商品费用，单位为分。</summary>
        public int? product_fee { get; set; }

        /// <summary>订购人证件类型。</summary>
        public string cert_type { get; set; }

        /// <summary>订购人证件号码。</summary>
        public string cert_id { get; set; }

        /// <summary>订购人姓名。</summary>
        public string name { get; set; }

        /// <inheritdoc />
        protected override void Validate(string key)
        {
            base.Validate(key);
            Require(transaction_id, "transaction_id");
            Require(mch_customs_no, "mch_customs_no");
        }

        /// <inheritdoc />
        protected override void SetParameters(RequestHandler handler)
        {
            base.SetParameters(handler);
            handler.SetParameter("transaction_id", transaction_id);
            handler.SetParameter("mch_customs_no", mch_customs_no);
            SetOptionalInt(handler, "duty", duty);
            handler.SetParameterWhenNotNull("action_type", action_type);
            handler.SetParameterWhenNotNull("sub_order_no", sub_order_no);
            handler.SetParameterWhenNotNull("fee_type", fee_type);
            SetOptionalInt(handler, "order_fee", order_fee);
            SetOptionalInt(handler, "transport_fee", transport_fee);
            SetOptionalInt(handler, "product_fee", product_fee);
            handler.SetParameterWhenNotNull("cert_type", cert_type);
            handler.SetParameterWhenNotNull("cert_id", cert_id);
            handler.SetParameterWhenNotNull("name", name);
        }
    }

    /// <summary>
    /// 海关报关状态查询请求。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4011985273</para>
    /// </summary>
    public class CustomsDeclareQueryRequestData : CustomsRequestDataBase
    {
        /// <summary>签名类型；当前接口使用 MD5。</summary>
        public string sign_type { get; set; }

        /// <summary>商户订单号，与其他订单标识至少填写一个。</summary>
        public string out_trade_no { get; set; }

        /// <summary>微信支付订单号，与其他订单标识至少填写一个。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户子订单号，与其他订单标识至少填写一个。</summary>
        public string sub_order_no { get; set; }

        /// <summary>微信子订单号，与其他订单标识至少填写一个。</summary>
        public string sub_order_id { get; set; }

        /// <inheritdoc />
        protected override void Validate(string key)
        {
            base.Validate(key);
            if (string.IsNullOrWhiteSpace(out_trade_no) &&
                string.IsNullOrWhiteSpace(transaction_id) &&
                string.IsNullOrWhiteSpace(sub_order_no) &&
                string.IsNullOrWhiteSpace(sub_order_id))
            {
                throw new ArgumentException(
                    "out_trade_no、transaction_id、sub_order_no 和 sub_order_id 至少填写一个。");
            }
        }

        /// <inheritdoc />
        protected override void SetParameters(RequestHandler handler)
        {
            base.SetParameters(handler);
            handler.SetParameterWhenNotNull("sign_type", sign_type);
            handler.SetParameterWhenNotNull("out_trade_no", out_trade_no);
            handler.SetParameterWhenNotNull("transaction_id", transaction_id);
            handler.SetParameterWhenNotNull("sub_order_no", sub_order_no);
            handler.SetParameterWhenNotNull("sub_order_id", sub_order_id);
        }
    }

    /// <summary>
    /// 海关重新申报请求。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4011985318</para>
    /// </summary>
    public class CustomsRedeclareRequestData : CustomsRequestDataBase
    {
        /// <summary>签名类型；当前接口使用 MD5。</summary>
        public string sign_type { get; set; }

        /// <summary>商户在海关备案的编号。</summary>
        public string mch_customs_no { get; set; }

        /// <summary>商户订单号，与 transaction_id 至少填写一个。</summary>
        public string out_trade_no { get; set; }

        /// <summary>微信支付订单号，与 out_trade_no 至少填写一个。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户子订单号。</summary>
        public string sub_order_no { get; set; }

        /// <summary>微信子订单号。</summary>
        public string sub_order_id { get; set; }

        /// <inheritdoc />
        protected override void Validate(string key)
        {
            base.Validate(key);
            Require(mch_customs_no, "mch_customs_no");
            if (string.IsNullOrWhiteSpace(out_trade_no) &&
                string.IsNullOrWhiteSpace(transaction_id))
            {
                throw new ArgumentException(
                    "out_trade_no 和 transaction_id 至少填写一个。");
            }
        }

        /// <inheritdoc />
        protected override void SetParameters(RequestHandler handler)
        {
            base.SetParameters(handler);
            handler.SetParameterWhenNotNull("sign_type", sign_type);
            handler.SetParameter("mch_customs_no", mch_customs_no);
            handler.SetParameterWhenNotNull("out_trade_no", out_trade_no);
            handler.SetParameterWhenNotNull("transaction_id", transaction_id);
            handler.SetParameterWhenNotNull("sub_order_no", sub_order_no);
            handler.SetParameterWhenNotNull("sub_order_id", sub_order_id);
        }
    }
}
