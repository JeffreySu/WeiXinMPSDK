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

    文件名：EcommerceAccountFundsReturnJson.cs
    文件功能描述：微信支付 V3 电商收付通账户资金管理返回及通知模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐电商收付通账户资金管理响应及通知模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 二级商户账户余额结果。
    /// </summary>
    public class EcommerceSubMerchantBalanceResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 可用于提现的余额，单位为分。
        /// </summary>
        public int available_amount { get; set; }

        /// <summary>
        /// 暂不可用余额，单位为分。
        /// </summary>
        public int? pending_amount { get; set; }

        /// <summary>
        /// 二级商户账户类型。
        /// </summary>
        public string account_type { get; set; }
    }

    /// <summary>
    /// 平台账户余额结果。
    /// </summary>
    public class EcommercePlatformBalanceResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 可用于提现的余额，单位为分。
        /// </summary>
        public int available_amount { get; set; }

        /// <summary>
        /// 暂不可用余额，单位为分。
        /// </summary>
        public int? pending_amount { get; set; }
    }

    /// <summary>
    /// 二级商户预约提现受理结果。
    /// </summary>
    public class EcommerceSubMerchantWithdrawalApplyResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 微信支付生成的预约提现单号。
        /// </summary>
        public string withdraw_id { get; set; }

        /// <summary>
        /// 商户自定义的预约提现单号。
        /// </summary>
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 平台账户预约提现受理结果。
    /// </summary>
    public class EcommercePlatformWithdrawalApplyResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付生成的预约提现单号。
        /// </summary>
        public string withdraw_id { get; set; }

        /// <summary>
        /// 商户自定义的预约提现单号。
        /// </summary>
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 预约提现查询结果的公共字段。
    /// </summary>
    public class EcommerceWithdrawalQueryResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 预约提现状态：INIT、CREATE_SUCCESS、SUCCESS、FAIL、REFUND 或 CLOSE。
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 微信支付生成的预约提现单号。
        /// </summary>
        public string withdraw_id { get; set; }

        /// <summary>
        /// 商户自定义的预约提现单号。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 提现金额，单位为分。
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 提交预约提现的 RFC 3339 时间。
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 提现状态最后更新的 RFC 3339 时间。
        /// </summary>
        public string update_time { get; set; }

        /// <summary>
        /// 提现失败、退票或关单时的原因。
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 提交提现时设置的备注。
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 展示在收款银行系统中的附言。
        /// </summary>
        public string bank_memo { get; set; }

        /// <summary>
        /// 出款账户类型：BASIC、FEES 或 OPERATION。
        /// </summary>
        public string account_type { get; set; }

        /// <summary>
        /// 入账银行账号后四位。
        /// </summary>
        public string account_number { get; set; }

        /// <summary>
        /// 入账银行简称。
        /// </summary>
        public string account_bank { get; set; }

        /// <summary>
        /// 入账银行全称。
        /// </summary>
        public string bank_name { get; set; }
    }

    /// <summary>
    /// 二级商户预约提现查询结果。
    /// <para>可作为二级商户提现状态变更通知的解密目标类型。</para>
    /// </summary>
    public class EcommerceSubMerchantWithdrawalQueryResultJson :
        EcommerceWithdrawalQueryResultJson
    {
        /// <summary>
        /// 发起提现的平台商户号。
        /// </summary>
        public string sp_mchid { get; set; }

        /// <summary>
        /// 提现对应的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 平台账户预约提现查询结果。
    /// <para>可作为平台提现状态变更通知的解密目标类型。</para>
    /// </summary>
    public class EcommercePlatformWithdrawalQueryResultJson :
        EcommerceWithdrawalQueryResultJson
    {
        /// <summary>
        /// 提现失败时微信支付给出的处理建议。
        /// </summary>
        public string solution { get; set; }
    }

    /// <summary>
    /// 二级商户按日终余额预约提现结果。
    /// <para>可作为日终余额提现状态变更通知的解密目标类型。</para>
    /// </summary>
    public class EcommerceSubMerchantDayEndWithdrawalResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 发起提现的平台商户号。
        /// </summary>
        public string sp_mchid { get; set; }

        /// <summary>
        /// 提现对应的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 处理状态：CREATED、PROCESSING、FINISHED 或 ABNORMAL。
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 微信支付生成的预约提现单号。
        /// </summary>
        public string withdraw_id { get; set; }

        /// <summary>
        /// 商户自定义的预约提现单号。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 本次预约提现总金额，单位为分。
        /// </summary>
        public int total_amount { get; set; }

        /// <summary>
        /// 已成功提现金额，单位为分。
        /// </summary>
        public int? success_amount { get; set; }

        /// <summary>
        /// 提现失败金额，单位为分。
        /// </summary>
        public int? fail_amount { get; set; }

        /// <summary>
        /// 提现退票金额，单位为分。
        /// </summary>
        public int? refund_amount { get; set; }

        /// <summary>
        /// 提交预约提现的 RFC 3339 时间。
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 提现状态最后更新的 RFC 3339 时间。
        /// </summary>
        public string update_time { get; set; }

        /// <summary>
        /// 异常原因。
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 提交提现时设置的备注。
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 展示在收款银行系统中的附言。
        /// </summary>
        public string bank_memo { get; set; }

        /// <summary>
        /// 出款账户类型：BASIC、FEES 或 OPERATION。
        /// </summary>
        public string account_type { get; set; }

        /// <summary>
        /// 入账银行账号后四位。
        /// </summary>
        public string account_number { get; set; }

        /// <summary>
        /// 入账银行简称。
        /// </summary>
        public string account_bank { get; set; }

        /// <summary>
        /// 入账银行全称。
        /// </summary>
        public string bank_name { get; set; }
    }

    /// <summary>
    /// 提现异常文件下载信息。
    /// </summary>
    public class EcommerceWithdrawalAbnormalBillResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 文件摘要算法，当前为 SHA1。
        /// </summary>
        public string hash_type { get; set; }

        /// <summary>
        /// 下载文件的摘要值。
        /// </summary>
        public string hash_value { get; set; }

        /// <summary>
        /// 提现异常文件的临时下载地址。
        /// </summary>
        public string download_url { get; set; }
    }

    /// <summary>
    /// 商户提现状态变更通知契约常量。
    /// <para>通知外层可使用 <see cref="NotifyRequest"/>，解密后的数据按提现类型使用本文件中的对应查询结果类型。</para>
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4013049135</para>
    /// </summary>
    public static class EcommerceWithdrawalNotificationTypes
    {
        /// <summary>
        /// 商户提现状态变更通知事件类型。
        /// </summary>
        public const string EventType = "MCHWITHDRAW.CHANGE";

        /// <summary>
        /// 商户提现通知资源原始类型。
        /// </summary>
        public const string OriginalType = "mch_withdraw";
    }
}
