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
  
    文件名：ComplaintNotifyJson.cs
    文件功能描述：投诉通知回调通知Json
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Complaint
{
    /// <summary>
    /// 投诉通知回调通知Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_1_16.shtml </para>
    /// </summary>
    public class ComplaintNotifyJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="id">通知ID  <para>通知的唯一ID</para><para>示例值：EV-2018022511223320873</para></param>
        /// <param name="create_time">通知创建时间  <para>通知创建的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
        /// <param name="event_type">通知类型  <para>通知的类型，投诉事件通知的类型，具体如下：COMPLAINT.CREATE：产生新投诉COMPLAINT.STATE_CHANGE：投诉状态变化</para><para>示例值：COMPLAINT.CREATE</para></param>
        /// <param name="resource_type">通知数据类型  <para>通知的资源数据类型，支付成功通知为encrypt-resource</para><para>示例值：encrypt-resource</para></param>
        /// <param name="summary">回调摘要 <para>回调摘要</para><para>示例值：产生新投诉</para></param>
        /// <param name="resource">通知数据 <para>通知资源数据json格式，见示例</para></param>
        public ComplaintNotifyJson(string id, string create_time, string event_type, string resource_type, string summary, Resource resource)
        {
            this.id = id;
            this.create_time = create_time;
            this.event_type = event_type;
            this.resource_type = resource_type;
            this.summary = summary;
            this.resource = resource;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ComplaintNotifyJson()
        {
        }

        /// <summary>
        /// 通知ID 
        /// <para>通知的唯一ID </para>
        /// <para>示例值：EV-2018022511223320873</para>
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 通知创建时间 
        /// <para>通知创建的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒。 </para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 通知类型 
        /// <para>通知的类型，投诉事件通知的类型，具体如 下： COMPLAINT.CREATE：产生新投诉 COMPLAINT.STATE_CHANGE：投诉状态变化</para>
        /// <para>示例值：COMPLAINT.CREATE </para>
        /// </summary>
        public string event_type { get; set; }

        /// <summary>
        /// 通知数据类型 
        /// <para>通知的资源数据类型，支付成功通知为encrypt-resource </para>
        /// <para>示例值：encrypt-resource </para>
        /// </summary>
        public string resource_type { get; set; }

        /// <summary>
        /// 回调摘要
        /// <para>回调摘要</para>
        /// <para>示例值：产生新投诉 </para>
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 通知数据
        /// <para>通知资源数据 json格式，见示例 </para>
        /// </summary>
        public Resource resource { get; set; }

        #region 子数据类型
        public class Resource
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="algorithm">加密算法类型  <para>对开启结果数据进行加密的加密算法，目前只支持AEAD_AES_256_GCM</para><para>示例值：AEAD_AES_256_GCM</para></param>
            /// <param name="ciphertext">数据密文  <para>Base64编码后的开启/停用结果数据密文</para><para>示例值：sadsadsadsad</para></param>
            /// <param name="original_type">原始回调类型  <para>Base64编码后的开启/停用结果数据密文</para><para>示例值：sadsadsadsad</para></param>
            /// <param name="associated_data">附加数据  <para>附加数据</para><para>示例值：fdasfwqewlkja484w</para><para>可为null</para></param>
            /// <param name="nonce">随机串  <para>加密使用的随机串</para><para>示例值：fdasflkja484w</para></param>
            public Resource(string algorithm, string ciphertext, string original_type, string associated_data, string nonce)
            {
                this.algorithm = algorithm;
                this.ciphertext = ciphertext;
                this.original_type = original_type;
                this.associated_data = associated_data;
                this.nonce = nonce;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Resource()
            {
            }

            /// <summary>
            /// 加密算法类型 
            /// <para>对开启结果数据进行加密的加密算法，目前只支持AEAD_AES_256_GCM </para>
            /// <para>示例值：AEAD_AES_256_GCM </para>
            /// </summary>
            public string algorithm { get; set; }

            /// <summary>
            /// 数据密文 
            /// <para>Base64编码后的开启/停用结果数据密文</para>
            /// <para>示例值：sadsadsadsad</para>
            /// </summary>
            public string ciphertext { get; set; }

            /// <summary>
            /// 原始回调类型 
            /// <para>Base64编码后的开启/停用结果数据密文</para>
            /// <para>示例值：sadsadsadsad</para>
            /// </summary>
            public string original_type { get; set; }

            /// <summary>
            /// 附加数据 
            /// <para>附加数据 </para>
            /// <para>示例值：fdasfwqewlkja484w</para>
            /// <para>可为null</para>
            /// </summary>
            public string associated_data { get; set; }

            /// <summary>
            /// 随机串 
            /// <para>加密使用的随机串 </para>
            /// <para>示例值：fdasflkja484w</para>
            /// </summary>
            public string nonce { get; set; }

        }


        #endregion
    }



}
