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
  
    文件名：DownloadProfitsharingBillReturnJson.cs
    文件功能描述：申请分账账单Json类
    
    
    创建标识：Senparc - 20210920
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 申请分账账单Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_11.shtml </para>
    /// </summary>
    public class DownloadProfitsharingBillReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="hash_type">哈希类型  <para>原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性。</para><para>示例值：SHA1</para></param>
        /// <param name="hash_value">哈希值  <para>原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性。</para><para>示例值：79bb0f45fc4c42234a918000b2668d689e2bde04</para></param>
        /// <param name="download_url">账单下载地址  <para>供下一步请求账单文件的下载地址，该地址30s内有效。</para><para>示例值：https://api.mch.weixin.qq.com/v3/billdownload/file?token=xxx</para></param>
        public DownloadProfitsharingBillReturnJson(string hash_type, string hash_value, string download_url)
        {
            this.hash_type = hash_type;
            this.hash_value = hash_value;
            this.download_url = download_url;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DownloadProfitsharingBillReturnJson()
        {
        }

        /// <summary>
        /// 哈希类型 
        /// <para>原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性。 </para>
        /// <para>示例值：SHA1</para>
        /// </summary>
        public string hash_type { get; set; }

        /// <summary>
        /// 哈希值 
        /// <para>原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性。 </para>
        /// <para>示例值：79bb0f45fc4c42234a918000b2668d689e2bde04 </para>
        /// </summary>
        public string hash_value { get; set; }

        /// <summary>
        /// 账单下载地址 
        /// <para>供下一步请求账单文件的下载地址，该地址30s内有效。 </para>
        /// <para>示例值：https://api.mch.weixin.qq.com/v3/billdownload/file?token=xxx </para>
        /// </summary>
        public string download_url { get; set; }

    }
}
