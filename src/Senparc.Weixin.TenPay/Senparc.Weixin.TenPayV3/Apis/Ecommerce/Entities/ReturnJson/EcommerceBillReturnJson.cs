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

    文件名：EcommerceBillReturnJson.cs
    文件功能描述：微信支付 V3 电商收付通资金账单申请结果模型


    创建标识：Senparc - 20130113

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐加密资金账单分片、下载和解密元数据模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 电商二级商户资金账单申请结果。
    /// </summary>
    public class EcommerceFundflowBillResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 可下载的账单文件分片总数。
        /// </summary>
        public int download_bill_count { get; set; }

        /// <summary>
        /// 账单文件分片、摘要和解密信息。
        /// </summary>
        public EcommerceFundflowBillDownloadItem[] download_bill_list { get; set; }
    }

    /// <summary>
    /// 电商二级商户资金账单的单个下载分片。
    /// </summary>
    public class EcommerceFundflowBillDownloadItem
    {
        /// <summary>
        /// 账单文件序号；多个文件按该序号从 1 开始合并。
        /// </summary>
        public int bill_sequence { get; set; }

        /// <summary>
        /// 原始账单摘要类型，当前为 SHA1。
        /// </summary>
        public string hash_type { get; set; }

        /// <summary>
        /// 解密且解压后的原始账单摘要值。
        /// </summary>
        public string hash_value { get; set; }

        /// <summary>
        /// 账单下载地址，有效期为五分钟。
        /// </summary>
        public string download_url { get; set; }

        /// <summary>
        /// 使用商户证书公钥加密并 Base64 编码的账单加密密钥。
        /// </summary>
        public string encrypt_key { get; set; }

        /// <summary>
        /// GCM 算法解密账单文件所需的随机字符串。
        /// </summary>
        public string nonce { get; set; }
    }
}
