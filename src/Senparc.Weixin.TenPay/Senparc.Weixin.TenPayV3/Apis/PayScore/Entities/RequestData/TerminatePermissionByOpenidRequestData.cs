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
  
    文件名：TerminatePermissionByOpenidRequestData.cs
    文件功能描述：微信支付V3解除用户授权关系（openid）接口请求数据
    
    
    创建标识：Senparc - 20210924
    
----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3解除用户授权关系（openid）接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_6.shtml </para>
    /// </summary>
    public class TerminatePermissionByOpenidRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="openid">用户标识 <para>path微信用户在商户对应appid下的唯一标识</para><para>示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</para><para></para></param>
        /// <param name="service_id">服务ID <para>body该服务ID有本接口对应产品的权限.</para><para>示例值：2002000000000558128851361561536</para></param>
        /// <param name="appid">应用ID <para>body微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系.需要在本系统先进行配置.</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="reason">撤销原因 <para>body撤销原因</para><para>示例值：reason</para></param>
        public TerminatePermissionByOpenidRequestData(string openid, string service_id, string appid, string reason)
        {
            this.openid = openid;
            this.service_id = service_id;
            this.appid = appid;
            this.reason = reason;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TerminatePermissionByOpenidRequestData()
        {
        }

        /// <summary>
        /// 用户标识
        /// <para>path微信用户在商户对应appid下的唯一标识 </para>
        /// <para>示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</para>
        /// <para></para>
        /// </summary>
        [JsonIgnore]
        public string openid { get; set; }

        /// <summary>
        /// 服务ID
        /// <para>body该服务ID有本接口对应产品的权限. </para>
        /// <para>示例值：2002000000000558128851361561536</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 应用ID
        /// <para>body微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系.需要在本系统先进行配置. </para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 撤销原因
        /// <para>body撤销原因</para>
        /// <para>示例值：reason</para>
        /// </summary>
        public string reason { get; set; }

    }
}
