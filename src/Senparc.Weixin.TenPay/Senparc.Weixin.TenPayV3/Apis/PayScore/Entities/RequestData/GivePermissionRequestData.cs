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
  
    文件名：GivePermissionRequestData.cs
    文件功能描述：商户预授权请求数据
    
    
    创建标识：Senparc - 20210924
    
----------------------------------------------------------------*/


using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 商户预授权请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_2.shtml </para>
    /// </summary>
    public class GivePermissionRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="service_id">服务id <para>body该服务ID有本接口对应产品的权限.</para><para>示例值：500001</para></param>
        /// <param name="appid">应用ID <para>body服务商申请的公众号或移动应用APPID</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="authorization_code">授权协议号 <para>body商户系统内部授权协议号，要求此参数只能由数字、大小写字母_-*组成，且在同一个商户号下唯一。</para><para>示例值：1234323JKHDFE1243252</para></param>
        /// <param name="notify_url">通知地址 <para>body商户接收授权回调通知的地址 </para><para>示例值：http://www.qq.com</para><para>可为null</para></param>
        public GivePermissionRequestData(string service_id, string appid, string authorization_code, string notify_url)
        {
            this.service_id = service_id;
            this.appid = appid;
            this.authorization_code = authorization_code;
            this.notify_url = notify_url;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public GivePermissionRequestData()
        {
        }

        /// <summary>
        /// 服务id
        /// <para>body该服务ID有本接口对应产品的权限. </para>
        /// <para>示例值：500001</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 应用ID
        /// <para>body服务商申请的公众号或移动应用APPID </para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 授权协议号
        /// <para>body商户系统内部授权协议号，要求此参数只能由数字、大小写字母_-*组成，且在同一个商户号下唯一。</para>
        /// <para>示例值：1234323JKHDFE1243252</para>
        /// </summary>
        public string authorization_code { get; set; }

        /// <summary>
        /// 通知地址
        /// <para>body商户接收授权回调通知的地址 </para>
        /// <para>示例值：http://www.qq.com</para>
        /// <para>可为null</para>
        /// </summary>
        public string notify_url { get; set; }

    }




}
