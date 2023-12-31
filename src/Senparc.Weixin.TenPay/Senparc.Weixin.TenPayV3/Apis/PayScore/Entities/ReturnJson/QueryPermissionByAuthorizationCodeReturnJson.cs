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
  
    文件名：QueryPermissionReturnJson.cs
    文件功能描述：查询用户授权记录（授权协议号）Json类
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 查询用户授权记录（授权协议号）Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_3.shtml </para>
    /// </summary>
    public class QueryPermissionByAuthorizationCodeReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="service_id">服务id <para>该服务ID有本接口对应产品的权限。 </para><para>示例值：2002000000000558128851361561536</para></param>
        /// <param name="appid">应用ID <para>与授权成功时记录的商户号绑定的appid（返回用户最近一次授权成功时记录的appid，不受当前请求参数影响）</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="mchid">商户号 <para>商户号（返回用户最近一次授权成功时记录的mchid，不受当前请求参数影响）</para><para>示例值：1230000109</para></param>
        /// <param name="openid">用户标识 <para>微信用户在商户对应appid下的唯一标识。 </para><para>示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</para><para>可为null</para></param>
        /// <param name="authorization_code">授权协议号 <para>预授权成功时的授权协议号。</para><para>示例值：1275342195190894594</para></param>
        /// <param name="authorization_state">授权状态 <para>标识用户授权服务情况。UNAVAILABLE：用户未授权服务AVAILABLE：用户已授权服务UNBINDUSER：未绑定用户（已经预授权但未完成正式授权）</para><para>示例值：UNAVAILABLE</para></param>
        /// <param name="notify_url">授权通知地址 <para>授权通知地址</para><para>示例值：https://www.weixin.com</para><para>可为null</para></param>
        /// <param name="cancel_authorization_time">最近一次解除授权时间 <para>最近一次解除授权时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒</para><para>示例值：2015-05-20T13:29:35.120+08:00</para><para>可为null</para></param>
        /// <param name="authorization_success_time">最近一次授权成功时间 <para>最近一次授权成功时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒</para><para>示例值：2015-05-20T13:29:35.120+08:00</para><para>可为null</para></param>
        public QueryPermissionByAuthorizationCodeReturnJson(string service_id, string appid, string mchid, string openid, string authorization_code, string authorization_state, string notify_url, string cancel_authorization_time, string authorization_success_time)
        {
            this.service_id = service_id;
            this.appid = appid;
            this.mchid = mchid;
            this.openid = openid;
            this.authorization_code = authorization_code;
            this.authorization_state = authorization_state;
            this.notify_url = notify_url;
            this.cancel_authorization_time = cancel_authorization_time;
            this.authorization_success_time = authorization_success_time;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryPermissionByAuthorizationCodeReturnJson()
        {
        }

        /// <summary>
        /// 服务id
        /// <para>该服务ID有本接口对应产品的权限。 </para>
        /// <para>示例值：2002000000000558128851361561536</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 应用ID
        /// <para>与授权成功时记录的商户号绑定的appid（返回用户最近一次授权成功时记录的appid，不受当前请求参数影响）</para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// <para>商户号（返回用户最近一次授权成功时记录的mchid，不受当前请求参数影响）</para>
        /// <para>示例值：1230000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 用户标识
        /// <para>微信用户在商户对应appid下的唯一标识。 </para>
        /// <para>示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</para>
        /// <para>可为null</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 授权协议号
        /// <para>预授权成功时的授权协议号。</para>
        /// <para>示例值：1275342195190894594</para>
        /// </summary>
        public string authorization_code { get; set; }

        /// <summary>
        /// 授权状态
        /// <para>标识用户授权服务情况。UNAVAILABLE：用户未授权服务 AVAILABLE：用户已授权服务 UNBINDUSER：未绑定用户（已经预授权但未完成正式授权） </para>
        /// <para>示例值：UNAVAILABLE</para>
        /// </summary>
        public string authorization_state { get; set; }

        /// <summary>
        /// 授权通知地址
        /// <para>授权通知地址</para>
        /// <para>示例值：https://www.weixin.com</para>
        /// <para>可为null</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 最近一次解除授权时间
        /// <para>最近一次解除授权时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string cancel_authorization_time { get; set; }

        /// <summary>
        /// 最近一次授权成功时间
        /// <para>最近一次授权成功时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string authorization_success_time { get; set; }

    }






}
