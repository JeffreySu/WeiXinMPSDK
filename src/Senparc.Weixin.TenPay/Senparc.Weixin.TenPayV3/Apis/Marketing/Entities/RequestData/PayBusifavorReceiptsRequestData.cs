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
  
    文件名：PayBusifavorReceiptsRequestData.cs
    文件功能描述：营销补差付款请求数据
    
    
    创建标识：Senparc - 20210914
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 营销补差付款请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_16.shtml </para>
    /// </summary>
    public class PayBusifavorReceiptsRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="stock_id">商家券批次号 <para>body由微信支付生成，调用创建商家券API成功时返回的唯一批次ID仅支持“满减券”，“换购券”批次不支持</para><para>示例值：128888000000001</para></param>
        /// <param name="coupon_code">商家券Code <para>body券的唯一标识。在WECHATPAY_MODE的券Code模式下，商家券Code是由微信支付生成的唯一ID；在MERCHANT_UPLOAD、MERCHANT_API的券Code模式下，商家券Code是由商户上传或指定，在批次下保证唯一；</para><para>示例值：ABCD12345678</para></param>
        /// <param name="transaction_id">微信支付订单号 <para>body微信支付下单支付成功返回的订单号</para><para>示例值：4200000913202101152566792388</para></param>
        /// <param name="payer_merchant">营销补差扣款商户号 <para>body营销补差扣款商户号</para><para>注：补差扣款商户号=制券商户号或补差扣款商户号=归属商户号</para><para>示例值：1900000001</para></param>
        /// <param name="payee_merchant">营销补差入账商户号 <para>body营销补差入账商户号</para><para>注：补差入帐商户号=券归属商户号或者和券归属商户号有连锁品牌关系</para><para>示例值：1900000002</para></param>
        /// <param name="amount">补差付款金额 <para>body单位为分，单笔订单补差金额不得超过券的优惠金额，最高补差金额为5000元>券的优惠金额定义：满减券：满减金额即为优惠金额</para><para>换购券：不支持</para><para>示例值：100</para></param>
        /// <param name="description">补差付款描述 <para>body付款备注描述，查询的时候原样带回</para><para>示例值：20210115DESCRIPTION</para></param>
        /// <param name="out_subsidy_no">业务请求唯一单号 <para>body商户侧需保证唯一性。可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号</para><para>示例值：subsidy-abcd-12345678</para></param>
        public PayBusifavorReceiptsRequestData(string stock_id, string coupon_code, string transaction_id, string payer_merchant, string payee_merchant, int amount, string description, string out_subsidy_no)
        {
            this.stock_id = stock_id;
            this.coupon_code = coupon_code;
            this.transaction_id = transaction_id;
            this.payer_merchant = payer_merchant;
            this.payee_merchant = payee_merchant;
            this.amount = amount;
            this.description = description;
            this.out_subsidy_no = out_subsidy_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PayBusifavorReceiptsRequestData()
        {
        }

        /// <summary>
        /// 商家券批次号
        /// <para>body由微信支付生成，调用创建商家券API成功时返回的唯一批次ID 仅支持“满减券”，“换购券”批次不支持 </para>
        /// <para>示例值：128888000000001</para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 商家券Code
        /// <para>body券的唯一标识。 在WECHATPAY_MODE的券Code模式下，商家券Code是由微信支付生成的唯一ID； 在MERCHANT_UPLOAD、MERCHANT_API的券Code模式下，商家券Code是由商户上传或指定，在批次下保证唯一； </para>
        /// <para>示例值：ABCD12345678</para>
        /// </summary>
        public string coupon_code { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// <para>body微信支付下单支付成功返回的订单号 </para>
        /// <para>示例值：4200000913202101152566792388</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 营销补差扣款商户号
        /// <para>body营销补差扣款商户号 </para>
        /// <para>注：补差扣款商户号 = 制券商户号 或 补差扣款商户号 = 归属商户号</para>
        /// <para>示例值：1900000001</para>
        /// </summary>
        public string payer_merchant { get; set; }

        /// <summary>
        /// 营销补差入账商户号
        /// <para>body营销补差入账商户号 </para>
        /// <para>注：补差入帐商户号 = 券归属商户号 或者和 券归属商户号有连锁品牌关系</para>
        /// <para>示例值：1900000002</para>
        /// </summary>
        public string payee_merchant { get; set; }

        /// <summary>
        /// 补差付款金额
        /// <para>body单位为分，单笔订单补差金额不得超过券的优惠金额，最高补差金额为5000元 > 券的优惠金额定义： 满减券：满减金额即为优惠金额</para>
        /// <para> 换购券：不支持 </para>
        /// <para>示例值：100</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 补差付款描述
        /// <para>body付款备注描述，查询的时候原样带回 </para>
        /// <para>示例值：20210115DESCRIPTION</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 业务请求唯一单号
        /// <para>body商户侧需保证唯一性。可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号 </para>
        /// <para>示例值：subsidy-abcd-12345678</para>
        /// </summary>
        public string out_subsidy_no { get; set; }

    }
}
