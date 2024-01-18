#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：QueryBusifavorPayReceiptsReturnJson.cs
    文件功能描述：查询营销补差付款单详情返回Json类
    
    
    创建标识：Senparc - 20210914
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 查询营销补差付款单详情返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_18.shtml </para>
    /// </summary>
    public class QueryBusifavorPayReceiptsReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="subsidy_receipt_id">补差付款单号 <para>补差付款唯一单号，由微信支付生成，仅在补差付款成功后有返回</para><para>示例值：1120200119165100000000000001</para></param>
        /// <param name="stock_id">商家券批次号 <para>由微信支付生成，调用创建商家券API成功时返回的唯一批次ID</para><para>示例值：128888000000001</para></param>
        /// <param name="coupon_code">商家券Code <para>券的唯一标识</para><para>示例值：ABCD12345678</para></param>
        /// <param name="transaction_id">微信支付订单号 <para>微信支付下单支付成功返回的订单号</para><para>示例值：4200000913202101152566792388</para></param>
        /// <param name="payer_merchant">营销补差扣款商户号 <para>营销补差扣款商户号</para><para>示例值：1900000001</para></param>
        /// <param name="payee_merchant">营销补差入账商户号 <para>营销补差入账商户号</para><para>示例值：1900000002</para></param>
        /// <param name="amount">补差付款金额 <para>单位为分，单笔订单补差金额不得超过券的优惠金额，最高补差金额为5000元>券的优惠金额定义：满减券：满减金额即为优惠金额</para><para>换购券：不支持</para><para>示例值：100</para></param>
        /// <param name="description">补差付款描述 <para>付款备注描述，查询的时候原样带回</para><para>示例值：20210115DESCRIPTION</para></param>
        /// <param name="status">补差付款单据状态 <para>补差付款单据状态：ACCEPTED：受理成功SUCCESS：补差补款成功FAIL：补差付款失败RETURNING：补差回退中PARTIAL_RETURN：补差部分回退FULL_RETURN：补差全额回退</para><para>示例值：SUCCESS</para></param>
        /// <param name="fail_reason">补差付款失败原因 <para>仅在补差付款失败时，返回告知对应失败的原因：INSUFFICIENT_BALANCE：扣款商户余额不足NOT_INCOMESPLIT_ORDER：非分账订单EXCEED_SUBSIDY_AMOUNT_QUOTA：超出订单补差总额限制EXCEED_SUBSIDY_COUNT_QUOTA：超出订单补差总数限制OTHER：其他原因</para><para>示例值：INSUFFICIENT_BALANCE</para><para>可为null</para></param>
        /// <param name="success_time">补差付款完成时间 <para>仅在补差付款成功时，返回完成时间。遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2021-01-20T10:29:35+08:00</para><para>可为null</para></param>
        /// <param name="out_subsidy_no">业务请求唯一单号 <para>商户侧需保证唯一性。可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号</para><para>示例值：subsidy-abcd-12345678</para></param>
        /// <param name="create_time">补差付款发起时间 <para>补差付款单据创建时间。遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2021-01-20T10:29:35+08:00</para><para>可为null</para></param>
        public QueryBusifavorPayReceiptsReturnJson(string subsidy_receipt_id, string stock_id, string coupon_code, string transaction_id, string payer_merchant, string payee_merchant, int amount, string description, string status, string fail_reason, string success_time, string out_subsidy_no, string create_time)
        {
            this.subsidy_receipt_id = subsidy_receipt_id;
            this.stock_id = stock_id;
            this.coupon_code = coupon_code;
            this.transaction_id = transaction_id;
            this.payer_merchant = payer_merchant;
            this.payee_merchant = payee_merchant;
            this.amount = amount;
            this.description = description;
            this.status = status;
            this.fail_reason = fail_reason;
            this.success_time = success_time;
            this.out_subsidy_no = out_subsidy_no;
            this.create_time = create_time;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryBusifavorPayReceiptsReturnJson()
        {
        }

        /// <summary>
        /// 补差付款单号
        /// <para>补差付款唯一单号，由微信支付生成，仅在补差付款成功后有返回 </para>
        /// <para>示例值：1120200119165100000000000001</para>
        /// </summary>
        public string subsidy_receipt_id { get; set; }

        /// <summary>
        /// 商家券批次号
        /// <para>由微信支付生成，调用创建商家券API成功时返回的唯一批次ID </para>
        /// <para>示例值：128888000000001</para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 商家券Code
        /// <para>券的唯一标识 </para>
        /// <para>示例值：ABCD12345678</para>
        /// </summary>
        public string coupon_code { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// <para>微信支付下单支付成功返回的订单号 </para>
        /// <para>示例值：4200000913202101152566792388</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 营销补差扣款商户号
        /// <para>营销补差扣款商户号 </para>
        /// <para>示例值：1900000001</para>
        /// </summary>
        public string payer_merchant { get; set; }

        /// <summary>
        /// 营销补差入账商户号
        /// <para>营销补差入账商户号 </para>
        /// <para>示例值：1900000002</para>
        /// </summary>
        public string payee_merchant { get; set; }

        /// <summary>
        /// 补差付款金额
        /// <para>单位为分，单笔订单补差金额不得超过券的优惠金额，最高补差金额为5000元 > 券的优惠金额定义： 满减券：满减金额即为优惠金额</para>
        /// <para> 换购券：不支持 </para>
        /// <para>示例值：100</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 补差付款描述
        /// <para>付款备注描述，查询的时候原样带回 </para>
        /// <para>示例值：20210115DESCRIPTION</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 补差付款单据状态
        /// <para>补差付款单据状态： ACCEPTED：受理成功  SUCCESS：补差补款成功 FAIL：补差付款失败 RETURNING：补差回退中  PARTIAL_RETURN：补差部分回退 FULL_RETURN：补差全额回退 </para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 补差付款失败原因
        /// <para>仅在补差付款失败时，返回告知对应失败的原因： INSUFFICIENT_BALANCE：扣款商户余额不足  NOT_INCOMESPLIT_ORDER：非分账订单 EXCEED_SUBSIDY_AMOUNT_QUOTA：超出订单补差总额限制 EXCEED_SUBSIDY_COUNT_QUOTA：超出订单补差总数限制  OTHER：其他原因 </para>
        /// <para>示例值：INSUFFICIENT_BALANCE</para>
        /// <para>可为null</para>
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 补差付款完成时间
        /// <para>仅在补差付款成功时，返回完成时间。遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
        /// <para>示例值：2021-01-20T10:29:35+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string success_time { get; set; }

        /// <summary>
        /// 业务请求唯一单号
        /// <para>商户侧需保证唯一性。可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号 </para>
        /// <para>示例值：subsidy-abcd-12345678</para>
        /// </summary>
        public string out_subsidy_no { get; set; }

        /// <summary>
        /// 补差付款发起时间
        /// <para>补差付款单据创建时间。遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
        /// <para>示例值：2021-01-20T10:29:35+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string create_time { get; set; }
    }
}