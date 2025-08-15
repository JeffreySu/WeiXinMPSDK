#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：EcommerceReturnJson.cs
    文件功能描述：电商收付通 - API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 电商收付通 - 二级商户进件API 返回信息
    /// </summary>
    public class SubMerchantApplymentReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 申请单号
        /// <para>微信支付分配的申请单号</para>
        /// <para>示例值：2000002124775691</para>
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 业务申请编号
        /// <para>服务商自定义的商户唯一编号</para>
        /// <para>示例值：APPLYMENT_00000000001</para>
        /// </summary>
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 查询二级商户进件申请状态API 返回信息
    /// </summary>
    public class QuerySubMerchantApplymentReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 申请单号
        /// <para>微信支付分配的申请单号</para>
        /// <para>示例值：2000002124775691</para>
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 业务申请编号
        /// <para>服务商自定义的商户唯一编号</para>
        /// <para>示例值：APPLYMENT_00000000001</para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 申请状态
        /// <para>CHECKING：资料校验中；ACCOUNT_NEED_VERIFY：待账户验证；AUDITING：审核中；REJECTED：已驳回；NEED_SIGN：待签约；FINISH：完成</para>
        /// <para>示例值：FINISH</para>
        /// </summary>
        public string applyment_state { get; set; }

        /// <summary>
        /// 申请状态描述
        /// <para>申请状态描述</para>
        /// <para>示例值：申请已通过</para>
        /// </summary>
        public string applyment_state_msg { get; set; }

        /// <summary>
        /// 签约链接
        /// <para>当申请状态为NEED_SIGN时，返回签约链接</para>
        /// <para>示例值：https://pay.weixin.qq.com/apply/applyment_detail/applyment_id</para>
        /// </summary>
        public string sign_url { get; set; }

        /// <summary>
        /// 汇款账户验证信息
        /// <para>当申请状态为ACCOUNT_NEED_VERIFY时，返回汇款验证信息</para>
        /// </summary>
        public AccountVerificationInfo account_validation { get; set; }

        /// <summary>
        /// 驳回原因详情
        /// <para>各项资料的审核情况</para>
        /// </summary>
        public AuditDetail[] audit_detail { get; set; }

        /// <summary>
        /// 法人验证链接
        /// <para>法人验证链接</para>
        /// <para>示例值：https://pay.weixin.qq.com/apply/applyment_detail/legal_validation_url</para>
        /// </summary>
        public string legal_validation_url { get; set; }

        /// <summary>
        /// 商户号
        /// <para>申请成功后分配的商户号</para>
        /// <para>示例值：1900013511</para>
        /// </summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 账户验证信息
    /// </summary>
    public class AccountVerificationInfo
    {
        /// <summary>
        /// 汇款金额
        /// <para>汇款金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int account_validation_amount { get; set; }

        /// <summary>
        /// 汇款账户名称
        /// <para>汇款账户名称</para>
        /// <para>示例值：腾讯科技有限公司</para>
        /// </summary>
        public string account_name { get; set; }

        /// <summary>
        /// 汇款备注
        /// <para>汇款备注</para>
        /// <para>示例值：微信支付-商户号123456789</para>
        /// </summary>
        public string destination_account_remark { get; set; }

        /// <summary>
        /// 汇款截止时间
        /// <para>汇款截止时间</para>
        /// <para>示例值：2018-12-01T12:00:00+08:00</para>
        /// </summary>
        public DateTime destination_account_deadline { get; set; }
    }

    /// <summary>
    /// 审核详情
    /// </summary>
    public class AuditDetail
    {
        /// <summary>
        /// 字段名
        /// <para>提交申请单的资料项名称</para>
        /// <para>示例值：id_card_copy</para>
        /// </summary>
        public string field { get; set; }

        /// <summary>
        /// 字段名称
        /// <para>提交申请单的资料项中文名称</para>
        /// <para>示例值：身份证人像面照片</para>
        /// </summary>
        public string field_name { get; set; }

        /// <summary>
        /// 驳回原因
        /// <para>驳回原因</para>
        /// <para>示例值：证件照片不清晰</para>
        /// </summary>
        public string reject_reason { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 合单下单API 返回信息
    /// </summary>
    public class CombineTransactionsReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 预支付交易会话标识
        /// <para>预支付交易会话标识</para>
        /// <para>示例值：wx201410272009395522657a690389285100</para>
        /// </summary>
        public string prepay_id { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 合单查询订单API 返回信息
    /// </summary>
    public class QueryCombineTransactionsReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 合单商户appid
        /// <para>合单发起方的appid</para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string combine_appid { get; set; }

        /// <summary>
        /// 合单商户号
        /// <para>合单发起方商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string combine_mchid { get; set; }

        /// <summary>
        /// 合单商户订单号
        /// <para>合单发起方商户订单号</para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string combine_out_trade_no { get; set; }

        /// <summary>
        /// 场景信息
        /// <para>支付场景描述</para>
        /// </summary>
        public object scene_info { get; set; }

        /// <summary>
        /// 子单信息
        /// <para>子单信息</para>
        /// </summary>
        public SubOrderResult[] sub_orders { get; set; }

        /// <summary>
        /// 支付者信息
        /// <para>支付者信息</para>
        /// </summary>
        public object combine_payer_info { get; set; }
    }

    /// <summary>
    /// 子订单结果信息
    /// </summary>
    public class SubOrderResult
    {
        /// <summary>
        /// 子商户号
        /// <para>子商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 交易类型
        /// <para>交易类型</para>
        /// <para>示例值：JSAPI</para>
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 交易状态
        /// <para>交易状态</para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string trade_state { get; set; }

        /// <summary>
        /// 付款银行
        /// <para>付款银行</para>
        /// <para>示例值：CMC</para>
        /// </summary>
        public string bank_type { get; set; }

        /// <summary>
        /// 附加数据
        /// <para>附加数据</para>
        /// <para>示例值：自定义数据</para>
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 支付完成时间
        /// <para>支付完成时间</para>
        /// <para>示例值：2018-06-08T10:34:56+08:00</para>
        /// </summary>
        public DateTime success_time { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// <para>微信支付订单号</para>
        /// <para>示例值：1217752501201407033233368018</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 子商户订单号
        /// <para>子商户订单号</para>
        /// <para>示例值：20150806125346</para>
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 订单金额信息
        /// <para>订单金额信息</para>
        /// </summary>
        public SubOrderAmountInfo amount { get; set; }
    }

    /// <summary>
    /// 子订单金额信息
    /// </summary>
    public class SubOrderAmountInfo
    {
        /// <summary>
        /// 总金额
        /// <para>订单总金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int total_amount { get; set; }

        /// <summary>
        /// 货币类型
        /// <para>货币类型</para>
        /// <para>示例值：CNY</para>
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// 用户支付金额
        /// <para>用户支付金额</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int payer_amount { get; set; }

        /// <summary>
        /// 用户支付币种
        /// <para>用户支付币种</para>
        /// <para>示例值：CNY</para>
        /// </summary>
        public string payer_currency { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 合单关闭订单API 返回信息
    /// </summary>
    public class CloseCombineTransactionsReturnJson : ReturnJsonBase
    {
        // 关闭订单成功时，微信支付无特定返回内容，HTTP状态码为204
    }

    // 由于内容过多，我先创建基础的返回类型，分账和补差相关的返回类型将在后续补充
    
    /// <summary>
    /// 电商收付通 - 请求分账API 返回信息
    /// </summary>
    public class EcommerceProfitsharingReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信订单号
        /// <para>微信支付订单号</para>
        /// <para>示例值：1217752501201407033233368018</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户分账单号
        /// <para>商户系统内部的分账单号</para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 微信分账单号
        /// <para>微信支付系统返回的分账单号</para>
        /// <para>示例值：3008450740201411110007820472</para>
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 分账单状态
        /// <para>PROCESSING：处理中；FINISHED：分账完成</para>
        /// <para>示例值：FINISHED</para>
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 分账接收方列表
        /// <para>分账接收方列表</para>
        /// </summary>
        public ProfitsharingReceiver[] receivers { get; set; }
    }

    /// <summary>
    /// 分账接收方信息
    /// </summary>
    public class ProfitsharingReceiver
    {
        /// <summary>
        /// 分账接收方类型
        /// <para>MERCHANT_ID：商户号；PERSONAL_OPENID：个人openid</para>
        /// <para>示例值：MERCHANT_ID</para>
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 分账接收方账号
        /// <para>分账接收方账号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 分账金额
        /// <para>分账金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 分账描述
        /// <para>分账的原因描述</para>
        /// <para>示例值：分给商户1900000109</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 分账结果
        /// <para>PENDING：待分账；SUCCESS：分账成功；CLOSED：已关闭</para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 分账失败原因
        /// <para>分账失败的原因</para>
        /// <para>示例值：ACCOUNT_ABNORMAL</para>
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 分账创建时间
        /// <para>分账创建时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 分账完成时间
        /// <para>分账完成时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime finish_time { get; set; }

        /// <summary>
        /// 分账详情单号
        /// <para>微信分账明细单号</para>
        /// <para>示例值：36011111111111111111111</para>
        /// </summary>
        public string detail_id { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 查询分账结果API 返回信息
    /// </summary>
    public class QueryEcommerceProfitsharingReturnJson : EcommerceProfitsharingReturnJson
    {
        // 继承自EcommerceProfitsharingReturnJson，字段相同
    }

    /// <summary>
    /// 电商收付通 - 分账回退API 返回信息
    /// </summary>
    public class EcommerceProfitsharingReturnOrderReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 商户回退单号
        /// <para>商户系统内部的回退单号</para>
        /// <para>示例值：R20190516001</para>
        /// </summary>
        public string out_return_no { get; set; }

        /// <summary>
        /// 微信回退单号
        /// <para>微信分账回退单号</para>
        /// <para>示例值：3008450740201411110007820472</para>
        /// </summary>
        public string return_id { get; set; }

        /// <summary>
        /// 回退商户号
        /// <para>回退商户号</para>
        /// <para>示例值：1900000100</para>
        /// </summary>
        public string return_mchid { get; set; }

        /// <summary>
        /// 回退金额
        /// <para>回退金额，单位为分</para>
        /// <para>示例值：10</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 回退描述
        /// <para>回退的原因描述</para>
        /// <para>示例值：分账错误</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 回退结果
        /// <para>PROCESSING：处理中；SUCCESS：已成功；FAILED：已失败</para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 回退失败原因
        /// <para>回退失败的原因</para>
        /// <para>示例值：ACCOUNT_ABNORMAL</para>
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 回退创建时间
        /// <para>回退创建时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 回退完成时间
        /// <para>回退完成时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime finish_time { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 查询分账回退结果API 返回信息
    /// </summary>
    public class QueryEcommerceProfitsharingReturnOrderReturnJson : EcommerceProfitsharingReturnOrderReturnJson
    {
        // 继承自EcommerceProfitsharingReturnOrderReturnJson，字段相同
    }

    /// <summary>
    /// 电商收付通 - 完结分账API 返回信息
    /// </summary>
    public class EcommerceProfitsharingFinishReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信订单号
        /// <para>微信支付订单号</para>
        /// <para>示例值：1217752501201407033233368018</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户分账单号
        /// <para>商户系统内部的分账单号</para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 微信分账单号
        /// <para>微信支付系统返回的分账单号</para>
        /// <para>示例值：3008450740201411110007820472</para>
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 请求补差API 返回信息
    /// </summary>
    public class EcommerceSubsidiesReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 补差单号
        /// <para>微信补差单号</para>
        /// <para>示例值：1217752501201407033233368018</para>
        /// </summary>
        public string subsidy_id { get; set; }

        /// <summary>
        /// 补差单状态
        /// <para>SUCCESS：补差成功；FAIL：补差失败</para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 补差描述
        /// <para>补差备注描述</para>
        /// <para>示例值：解冻费用</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 补差金额
        /// <para>补差金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 补差创建时间
        /// <para>补差创建时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 补差回退API 返回信息
    /// </summary>
    public class EcommerceSubsidiesReturnReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 补差回退单号
        /// <para>微信补差回退单号</para>
        /// <para>示例值：1217752501201407033233368018</para>
        /// </summary>
        public string subsidy_refund_id { get; set; }

        /// <summary>
        /// 补差回退结果
        /// <para>SUCCESS：补差回退成功；FAIL：补差回退失败</para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 补差回退金额
        /// <para>补差回退金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 补差回退描述
        /// <para>补差回退描述</para>
        /// <para>示例值：补差回退</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 补差回退创建时间
        /// <para>补差回退创建时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 取消补差API 返回信息
    /// </summary>
    public class EcommerceSubsidiesCancelReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 补差单号
        /// <para>微信补差单号</para>
        /// <para>示例值：1217752501201407033233368018</para>
        /// </summary>
        public string subsidy_id { get; set; }

        /// <summary>
        /// 取消补差结果
        /// <para>SUCCESS：取消补差成功；FAIL：取消补差失败</para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 取消补差描述
        /// <para>取消补差描述</para>
        /// <para>示例值：取消补差</para>
        /// </summary>
        public string description { get; set; }
    }
}
