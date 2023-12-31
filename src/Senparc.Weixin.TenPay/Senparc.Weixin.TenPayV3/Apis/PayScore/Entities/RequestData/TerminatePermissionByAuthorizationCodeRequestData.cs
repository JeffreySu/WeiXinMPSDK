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
  
    文件名：TerminatePermissionByAuthorizationCodeRequestData.cs
    文件功能描述：微信支付V3解除用户授权关系（授权协议号）接口请求数据
    
    
    创建标识：Senparc - 20210924
    
----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3解除用户授权关系（授权协议号）接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_4.shtml </para>
    /// </summary>
    public class TerminatePermissionByAuthorizationCodeRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="authorization_code">授权协议号 <para>path商户系统内部授权协议号，要求此参数只能由数字、大小写字母_-*组成，且在同一个商户号下唯一。</para><para>示例值：1234323JKHDFE1243252</para><para></para></param>
        /// <param name="service_id">服务id <para>body该服务ID有本接口对应产品的权限. </para><para>示例值：500001</para></param>
        /// <param name="reason">撤销原因 <para>body解除授权原因 </para><para>示例值：撤销原因</para></param>
        public TerminatePermissionByAuthorizationCodeRequestData(string authorization_code, string service_id, string reason)
        {
            this.authorization_code = authorization_code;
            this.service_id = service_id;
            this.reason = reason;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TerminatePermissionByAuthorizationCodeRequestData()
        {
        }

        /// <summary>
        /// 授权协议号
        /// <para>path商户系统内部授权协议号，要求此参数只能由数字、大小写字母_-*组成，且在同一个商户号下唯一。 </para>
        /// <para>示例值：1234323JKHDFE1243252</para>
        /// <para></para>
        /// </summary>
        [JsonIgnore]
        public string authorization_code { get; set; }

        /// <summary>
        /// 服务id
        /// <para>body该服务ID有本接口对应产品的权限. </para>
        /// <para>示例值：500001</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 撤销原因
        /// <para>body解除授权原因 </para>
        /// <para>示例值：撤销原因</para>
        /// </summary>
        public string reason { get; set; }

    }




}
