#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：EcommerceCrossBorderRequestData.cs
    文件功能描述：微信支付 V3 电商收付通跨境付款请求模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐资金出境申请及购付汇账单请求模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 电商收付通申请资金出境的请求数据。
    /// </summary>
    public class EcommerceFundsToOverseaRequestData
    {
        /// <summary>
        /// 商户出境单号；同一商户号下唯一，只能包含数字、大小写字母、下划线和连字符。
        /// </summary>
        public string out_order_id { get; set; }

        /// <summary>
        /// 申请资金出境的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 资金出境对应的微信支付订单号。
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 请求出境的人民币金额，单位为分。
        /// </summary>
        public long amount { get; set; }

        /// <summary>
        /// 境外收款币种，例如 USD、HKD 或 EUR。
        /// </summary>
        public string foreign_currency { get; set; }

        /// <summary>
        /// 商品信息；官方业务说明要求必填，最多十项。
        /// </summary>
        public EcommerceFundsToOverseaGoodsInfo[] goods_info { get; set; }

        /// <summary>
        /// 境外卖家信息。
        /// </summary>
        public EcommerceFundsToOverseaSellerInfo seller_info { get; set; }

        /// <summary>
        /// 物流信息；仅预售定金订单单独出境时可不填，其他场景必填。
        /// </summary>
        public EcommerceFundsToOverseaExpressInfo express_info { get; set; }

        /// <summary>
        /// 境外收款人信息。
        /// </summary>
        public EcommerceFundsToOverseaPayeeInfo payee_info { get; set; }

        /// <summary>
        /// 预售信息；非预售场景不填。
        /// </summary>
        public EcommerceFundsToOverseaPresaleInfo presale_info { get; set; }
    }

    /// <summary>
    /// 资金出境申请的商品信息。
    /// </summary>
    public class EcommerceFundsToOverseaGoodsInfo
    {
        /// <summary>
        /// 商品名称。
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 商户自定义商品类目；二级类目使用斜杠分隔。
        /// </summary>
        public string goods_category { get; set; }

        /// <summary>
        /// 商品单价，单位为分。
        /// </summary>
        public long goods_unit_price { get; set; }

        /// <summary>
        /// 商品数量。
        /// </summary>
        public long goods_quantity { get; set; }
    }

    /// <summary>
    /// 资金出境申请的境外卖家信息。
    /// </summary>
    public class EcommerceFundsToOverseaSellerInfo
    {
        /// <summary>
        /// 境外卖家经营主体名称。
        /// </summary>
        public string oversea_business_name { get; set; }

        /// <summary>
        /// 境外卖家店铺名称。
        /// </summary>
        public string oversea_shop_name { get; set; }

        /// <summary>
        /// 商户系统内部的卖家标识。
        /// </summary>
        public string seller_id { get; set; }
    }

    /// <summary>
    /// 资金出境申请的物流信息。
    /// </summary>
    public class EcommerceFundsToOverseaExpressInfo
    {
        /// <summary>
        /// 物流单号。
        /// </summary>
        public string courier_number { get; set; }

        /// <summary>
        /// 物流商名称。
        /// </summary>
        public string express_company_name { get; set; }
    }

    /// <summary>
    /// 资金出境申请的境外收款人信息。
    /// </summary>
    public class EcommerceFundsToOverseaPayeeInfo
    {
        /// <summary>
        /// 微信支付分配的收款人识别号。
        /// </summary>
        public string payee_id { get; set; }
    }

    /// <summary>
    /// 资金出境申请的预售信息。
    /// </summary>
    public class EcommerceFundsToOverseaPresaleInfo
    {
        /// <summary>
        /// 预售订单类型：DEPOSIT 表示定金订单，BALANCE 表示尾款订单。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 预售订单人民币总金额，单位为分。
        /// </summary>
        public long total_amount { get; set; }

        /// <summary>
        /// 关联定金订单的微信支付订单号；尾款订单出境时必填。
        /// </summary>
        public string deposit_transaction_id { get; set; }

        /// <summary>
        /// 关联尾款订单的微信支付订单号；定金订单出境时按官方场景填写。
        /// </summary>
        public string balance_transaction_id { get; set; }
    }

    /// <summary>
    /// 获取购付汇账单文件下载链接的请求数据。
    /// </summary>
    public class EcommerceFundsToOverseaBillRequestData
    {
        /// <summary>
        /// 账单日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string bill_date { get; set; }

        /// <summary>
        /// 可选的二级商户号；服务商不填时返回服务商范围内的账单。
        /// </summary>
        public string sub_mchid { get; set; }
    }
}
