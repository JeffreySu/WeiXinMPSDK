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
  
    文件名：BillData.cs
    文件功能描述：新微信支付V3账单数据
    
    
    创建标识：Senparc - 20210814
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    /// <summary>
    /// 
    /// </summary>
    public class SubmerchantBillReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 下载信息总数
        /// 下载信息总数
        /// 示例值：1
        /// </summary>
        public int download_bill_count { get; set; }

        /// <summary>
        /// 下载信息明细
        /// </summary>
        public List<SubmerchantBillDownloadItem> download_bill_list { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SubmerchantBillDownloadItem
    {
        /// <summary>
        /// 账单文件序号
        /// 商户将多个文件按账单文件序号的顺序合并为完整的资金账单文件，起始值为1
        /// 示例值：1
        /// </summary>
        public int bill_sequence { get; set; }

        /// <summary>
        /// 下载地址
        /// 下载地址30s内有效
        /// 示例值：https://api.mch.weixin.qq.com/v3/bill/downloadurl?token=xxx
        /// </summary>
        public string download_url { get; set; }

        /// <summary>
        /// 加密密钥
        /// 加密账单文件使用的加密密钥。密钥用商户证书的公钥进行加密，然后进行Base64编码
        /// 示例值：a0YK7p+9XaKzE9N4qtFfG/9za1oqKlLXXJWBkH+kX84onAs2Ol/E1fk+6S+FuBXczGDRU8I8D+6PfbwKYBGm0wANUTqHOSezzfbieIo2t51UIId7sP9SoN38W2+IcYDviIsu59KSdyiL3TY2xqZNT8UDcnMWzTNZdSv+CLsSgblB6OKGN9JONTadOFGfv1OKkTp86Li+X7S9bG62wsa572/5Rm4MmDCiKwY4bX2EynWQHBEOExD5URxT6/MX3F1D3BNYrE4fUu1F03k25xVlXnZDjksy6Rf3SCgadR+Cepc6mdfF9b2gTxNsJFMEdYXbqL0W1WQZ3UqSPQCguK6uLA==
        /// </summary>
        public string encrypt_key { get; set; }

        /// <summary>
        /// 哈希类型
        /// 原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性。
        /// 示例值：SHA1
        /// </summary>
        public string hash_type { get; set; }

        /// <summary>
        /// 哈希值
        /// 原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性
        /// 示例值：79bb0f45fc4c42234a918000b2668d689e2bde04
        /// </summary>
        public string hash_value { get; set; }

        /// <summary>
        /// 随机字符串
        /// 加密账单文件使用的随机字符串
        /// 示例值：a8607ef79034c49c
        /// </summary>
        public string nonce { get; set; }
    }
}
