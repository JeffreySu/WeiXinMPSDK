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
  
    文件名：NotifyRequest.cs
    文件功能描述：微信支付V3回调通知请求类 数据未解密
    
    
    创建标识：Senparc - 20210813
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Entities
{
    /// <summary>
    /// NotifyRequest
    /// </summary>
    public class NotifyRequest
    {
        /// <summary>
        /// 通知ID
        /// 通知的唯一ID
        /// 示例值：EV-2018022511223320873
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 通知创建时间
        /// 通知创建的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE
        /// 示例值：2015-05-20T13:29:35+08:00 
        /// </summary>
        public DateTime /*TenpayDateTime*/ create_time { get; set; }

        /// <summary>
        /// 通知类型
        /// 通知的类型，支付成功通知的类型为TRANSACTION.SUCCESS
        /// 示例值：TRANSACTION.SUCCESS
        /// </summary>
        public string event_type { get; set; }

        /// <summary>
        /// 通知数据类型
        /// 通知的资源数据类型，支付成功通知为encrypt-resource
        /// 示例值：encrypt-resource
        /// </summary>
        public string resource_type { get; set; }

        /// <summary>
        /// 通知数据
        /// </summary>
        public Resource resource { get; set; }

        /// <summary>
        /// 回调摘要
        /// 示例值：支付成功
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 通知资源数据
        /// </summary>
        public class Resource
        {
            /// <summary>
            /// 加密算法类型
            /// 对开启结果数据进行加密的加密算法，目前只支持AEAD_AES_256_GCM
            /// 示例值：AEAD_AES_256_GCM
            /// </summary>
            public string algorithm { get; set; }

            /// <summary>
            /// 数据密文
            /// Base64编码后的开启/停用结果数据密文
            /// 示例值：sadsadsadsad
            /// </summary>
            public string ciphertext { get; set; }

            /// <summary>
            /// 附加数据
            /// 附加数据
            /// 示例值：fdasfwqewlkja484w
            /// </summary>
            public string associated_data { get; set; }

            /// <summary>
            /// 原始类型
            /// 原始回调类型，为transaction
            /// 示例值：transaction
            /// </summary>
            public string original_type { get; set; }

            /// <summary>
            /// 随机串
            /// 加密使用的随机串
            /// 示例值：fdasflkja484w
            /// </summary>
            public string nonce { get; set; }
        }
    }
}
