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

    文件名：EcommerceAccountFundsRequestData.cs
    文件功能描述：微信支付 V3 电商收付通账户资金管理请求模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐电商收付通账户资金管理请求模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 二级商户预约提现请求数据。
    /// </summary>
    public class EcommerceSubMerchantWithdrawalRequestData
    {
        /// <summary>
        /// 收付通平台的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 商户自定义且唯一的预约提现单号，仅包含字母和数字。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 提现金额，单位为分。
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 提现备注，最多 56 个字符。
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
        /// 提现状态变更通知地址，仅支持 HTTPS。
        /// </summary>
        public string notify_url { get; set; }
    }

    /// <summary>
    /// 平台账户预约提现请求数据。
    /// </summary>
    public class EcommercePlatformWithdrawalRequestData
    {
        /// <summary>
        /// 商户自定义且唯一的预约提现单号，仅包含字母和数字。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 提现金额，单位为分。
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 提现备注，最多 56 个字符。
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
        /// 提现状态变更通知地址，仅支持 HTTPS。
        /// </summary>
        public string notify_url { get; set; }
    }

    /// <summary>
    /// 二级商户按日终余额预约提现请求数据。
    /// </summary>
    public class EcommerceSubMerchantDayEndWithdrawalRequestData
    {
        /// <summary>
        /// 收付通平台的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 商户自定义且唯一的预约提现单号。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 提现金额计算方式：ONLY_DAY_END_BALANCE 或 ALLOW_CURRENT_BALANCE。
        /// </summary>
        public string calculate_amount_type { get; set; }

        /// <summary>
        /// 提现备注，最多 56 个字符。
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 展示在收款银行系统中的附言。
        /// </summary>
        public string bank_memo { get; set; }

        /// <summary>
        /// 提现状态变更通知地址，仅支持 HTTPS。
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 提现后希望保留在账户中的金额，单位为分。
        /// </summary>
        public int? reserve_amount { get; set; }
    }
}
