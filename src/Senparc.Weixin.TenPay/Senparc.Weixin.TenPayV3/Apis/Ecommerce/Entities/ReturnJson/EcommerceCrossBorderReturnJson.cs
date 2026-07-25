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

    文件名：EcommerceCrossBorderReturnJson.cs
    文件功能描述：微信支付 V3 电商收付通跨境付款返回模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐可出境余额、出境结果及购付汇账单返回模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 微信支付订单剩余可出境金额查询结果。
    /// </summary>
    public class EcommerceFundsToOverseaAvailableAmountResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付订单号。
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 订单剩余可出境金额，单位为分。
        /// </summary>
        public long available_abroad_amount { get; set; }
    }

    /// <summary>
    /// 资金出境申请或结果查询响应。
    /// </summary>
    public class EcommerceFundsToOverseaOrderResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 商户出境单号。
        /// </summary>
        public string out_order_id { get; set; }

        /// <summary>
        /// 申请资金出境的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 微信支付生成的出境单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 出境结果：ACCEPT、SUCCESS 或 FAIL。
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 出境失败原因；仅 result 为 FAIL 时返回。
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 请求出境的人民币金额，单位为分。
        /// </summary>
        public long amount { get; set; }

        /// <summary>
        /// 实际出境外币金额，单位为对应币种的最小计价单位。
        /// </summary>
        public long? foreign_amount { get; set; }

        /// <summary>
        /// 出境目标外币币种。
        /// </summary>
        public string foreign_currency { get; set; }

        /// <summary>
        /// 汇率乘以十的八次方后的整数值。
        /// </summary>
        public long? rate { get; set; }

        /// <summary>
        /// 实际购汇时间，遵循 RFC 3339 格式。
        /// </summary>
        public string exchange_rate_time { get; set; }

        /// <summary>
        /// 预计购汇时间，遵循 RFC 3339 格式。
        /// </summary>
        public string estimate_exchange_rate_time { get; set; }

        /// <summary>
        /// 实际出境的人民币金额，单位为分。
        /// </summary>
        public long? departure_amount { get; set; }

        /// <summary>
        /// 资金出境手续费，单位为分。
        /// </summary>
        public long? fee { get; set; }

        /// <summary>
        /// 手续费承担商户号。
        /// </summary>
        public string charge_mchid { get; set; }

        /// <summary>
        /// 手续费承担账户：BASIC 或 FEES。
        /// </summary>
        public string charge_account_type { get; set; }
    }

    /// <summary>
    /// 购付汇账单文件下载链接查询结果。
    /// </summary>
    public class EcommerceFundsToOverseaBillResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 账单摘要类型，当前为 SHA1。
        /// </summary>
        public string hash_type { get; set; }

        /// <summary>
        /// 原始账单摘要值；gzip 文件需要解压后再进行校验。
        /// </summary>
        public string hash_value { get; set; }

        /// <summary>
        /// 需要按微信支付 API v3 规则签名的账单下载地址。
        /// </summary>
        public string download_url { get; set; }
    }
}
