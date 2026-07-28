#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CustomsResults.cs
    文件功能描述：微信支付海关报关 V2 强类型返回结果


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v1.20.0 新增报关、报关查询及重新申报结果解析

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>支付订单海关报关结果。</summary>
    public class CustomsDeclareOrderResult : Result
    {
        /// <summary>签名类型。</summary>
        public string sign_type { get; set; }

        /// <summary>海关申报状态。</summary>
        public string state { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户子订单号。</summary>
        public string sub_order_no { get; set; }

        /// <summary>微信子订单号。</summary>
        public string sub_order_id { get; set; }

        /// <summary>最后修改时间。</summary>
        public string modify_time { get; set; }

        /// <summary>身份核验结果。</summary>
        public string cert_check_result { get; set; }

        /// <summary>核验机构。</summary>
        public string verify_department { get; set; }

        /// <summary>核验机构交易流水号。</summary>
        public string verify_department_trade_id { get; set; }

        /// <summary>从微信支付 XML 构造报关结果。</summary>
        public CustomsDeclareOrderResult(string resultXml) : base(resultXml)
        {
            sign_type = GetXmlValue("sign_type");
            state = GetXmlValue("state");
            transaction_id = GetXmlValue("transaction_id");
            sub_order_no = GetXmlValue("sub_order_no");
            sub_order_id = GetXmlValue("sub_order_id");
            modify_time = GetXmlValue("modify_time");
            cert_check_result = GetXmlValue("cert_check_result");
            verify_department = GetXmlValue("verify_department");
            verify_department_trade_id = GetXmlValue("verify_department_trade_id");
        }
    }

    /// <summary>海关报关状态查询结果。</summary>
    public class CustomsDeclareQueryResult : Result
    {
        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>返回的申报子单数量。</summary>
        public int count { get; set; }

        /// <summary>强类型申报子单明细。</summary>
        public List<CustomsDeclareQueryItem> sub_orders { get; private set; }

        /// <summary>核验机构。</summary>
        public string verify_department { get; set; }

        /// <summary>核验机构交易流水号。</summary>
        public string verify_department_trade_id { get; set; }

        /// <summary>从微信支付 XML 构造报关查询结果。</summary>
        public CustomsDeclareQueryResult(string resultXml) : base(resultXml)
        {
            transaction_id = GetXmlValue("transaction_id");
            count = GetXmlValueAsInt("count");
            verify_department = GetXmlValue("verify_department");
            verify_department_trade_id = GetXmlValue("verify_department_trade_id");
            sub_orders = ParseSubOrders();
        }

        private List<CustomsDeclareQueryItem> ParseSubOrders()
        {
            var result = new List<CustomsDeclareQueryItem>();
            var root = _resultXml.Element("xml");
            if (root == null || count <= 0)
            {
                return result;
            }

            var hasZeroBasedField = root.Elements().Any(element =>
                element.Name.LocalName.EndsWith("_0"));
            var startIndex = hasZeroBasedField ? 0 : 1;
            for (var offset = 0; offset < count; offset++)
            {
                var index = (startIndex + offset).ToString();
                result.Add(new CustomsDeclareQueryItem
                {
                    sub_order_no = GetXmlValue("sub_order_no_" + index),
                    sub_order_id = GetXmlValue("sub_order_id_" + index),
                    mch_customs_no = GetXmlValue("mch_customs_no_" + index),
                    customs = GetXmlValue("customs_" + index),
                    fee_type = GetXmlValue("fee_type_" + index),
                    order_fee = GetXmlValueAsInt("order_fee_" + index),
                    duty = GetXmlValueAsInt("duty_" + index),
                    transport_fee = GetXmlValueAsInt("transport_fee_" + index),
                    product_fee = GetXmlValueAsInt("product_fee_" + index),
                    state = GetXmlValue("state_" + index),
                    explanation = GetXmlValue("explanation_" + index),
                    modify_time = GetXmlValue("modify_time_" + index),
                    cert_check_result = GetXmlValue("cert_check_result_" + index)
                });
            }
            return result;
        }
    }

    /// <summary>海关报关状态查询中的子单明细。</summary>
    public class CustomsDeclareQueryItem
    {
        /// <summary>商户子订单号。</summary>
        public string sub_order_no { get; set; }

        /// <summary>微信子订单号。</summary>
        public string sub_order_id { get; set; }

        /// <summary>商户在海关备案的编号。</summary>
        public string mch_customs_no { get; set; }

        /// <summary>海关编号。</summary>
        public string customs { get; set; }

        /// <summary>币种。</summary>
        public string fee_type { get; set; }

        /// <summary>子订单金额，单位为分。</summary>
        public int order_fee { get; set; }

        /// <summary>应付关税，单位为分。</summary>
        public int duty { get; set; }

        /// <summary>物流费用，单位为分。</summary>
        public int transport_fee { get; set; }

        /// <summary>商品费用，单位为分。</summary>
        public int product_fee { get; set; }

        /// <summary>海关申报状态。</summary>
        public string state { get; set; }

        /// <summary>申报状态说明。</summary>
        public string explanation { get; set; }

        /// <summary>最后修改时间。</summary>
        public string modify_time { get; set; }

        /// <summary>身份核验结果。</summary>
        public string cert_check_result { get; set; }
    }

    /// <summary>海关重新申报结果。</summary>
    public class CustomsRedeclareResult : Result
    {
        /// <summary>海关申报状态。</summary>
        public string state { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户子订单号。</summary>
        public string sub_order_no { get; set; }

        /// <summary>微信子订单号。</summary>
        public string sub_order_id { get; set; }

        /// <summary>最后修改时间。</summary>
        public string modify_time { get; set; }

        /// <summary>申报状态说明。</summary>
        public string explanation { get; set; }

        /// <summary>从微信支付 XML 构造重新申报结果。</summary>
        public CustomsRedeclareResult(string resultXml) : base(resultXml)
        {
            state = GetXmlValue("state");
            transaction_id = GetXmlValue("transaction_id");
            sub_order_no = GetXmlValue("sub_order_no");
            sub_order_id = GetXmlValue("sub_order_id");
            modify_time = GetXmlValue("modify_time");
            explanation = GetXmlValue("explanation");
        }
    }
}
