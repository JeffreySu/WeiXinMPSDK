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

    文件名：EcommerceBillRequestData.cs
    文件功能描述：微信支付 V3 电商收付通资金账单申请模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐全部及单个二级商户资金账单申请模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 电商平台申请全部二级商户资金账单的请求数据。
    /// </summary>
    public class EcommerceAllSubMerchantFundflowBillRequestData
    {
        /// <summary>
        /// 账单日期，格式为 yyyy-MM-dd，仅支持三个月内的账单。
        /// </summary>
        public string bill_date { get; set; }

        /// <summary>
        /// 资金账户类型；该场景当前必须为 ALL。
        /// </summary>
        public string account_type { get; set; } = "ALL";

        /// <summary>
        /// 压缩类型；可选值为 GZIP，不传时返回未压缩数据流。
        /// </summary>
        public string tar_type { get; set; }

        /// <summary>
        /// 账单文件加密算法：AEAD_AES_256_GCM 或 SM4_GCM。
        /// </summary>
        public string algorithm { get; set; } = "AEAD_AES_256_GCM";
    }

    /// <summary>
    /// 申请指定单个子商户资金账单的请求数据。
    /// </summary>
    public class EcommerceSingleSubMerchantFundflowBillRequestData
    {
        /// <summary>
        /// 需要下载资金账单的子商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 账单日期，格式为 yyyy-MM-dd，仅支持三个月内的账单。
        /// </summary>
        public string bill_date { get; set; }

        /// <summary>
        /// 资金账户类型：BASIC、OPERATION、FEES 或 DEPOSIT。
        /// </summary>
        public string account_type { get; set; } = "BASIC";

        /// <summary>
        /// 账单文件加密算法：AEAD_AES_256_GCM 或 SM4_GCM。
        /// </summary>
        public string algorithm { get; set; } = "AEAD_AES_256_GCM";

        /// <summary>
        /// 压缩类型；可选值为 GZIP，不传时返回未压缩数据流。
        /// </summary>
        public string tar_type { get; set; }
    }
}
