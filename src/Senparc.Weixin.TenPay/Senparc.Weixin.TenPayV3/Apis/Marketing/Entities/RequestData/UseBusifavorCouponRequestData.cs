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
  
    文件名：UseBusifavorCouponRequestData.cs
    文件功能描述：核销商家券接口请求数据
    
    
    创建标识：Senparc - 20210907
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 核销商家券接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_3.shtml </para>
    /// </summary>
    public class UseBusifavorCouponRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="coupon_code">券code  <para>body券的唯一标识。</para><para>示例值：sxxe34343434</para></param>
        /// <param name="stock_id">批次号  <para>body微信为每个商家券批次分配的唯一ID，当你在创建商家券接口中的coupon_code_mode参数传值为MERCHANT_API或者MERCHANT_UPLOAD时，则核销接口中该字段必传，否则该字段可不传</para><para>示例值：100088</para><para>可为null</para></param>
        /// <param name="appid">公众账号ID  <para>body支持传入与当前调用接口商户号有绑定关系的appid。支持小程序appid与公众号appid。核销接口返回的openid会在该传入appid下进行计算获得。</para><para>校验规则：传入的APPID得是与调用方商户号（即请求头里面的商户号）有绑定关系的APPID或传入的APPID得是归属商户号有绑定关系的APPID</para><para>示例值：wx1234567889999</para></param>
        /// <param name="use_time">请求核销时间  <para>body商户请求核销用户券的时间。遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
        /// <param name="use_request_no">核销请求单据号  <para>body每次核销请求的唯一标识，商户需保证唯一。</para><para>示例值：1002600620019090123143254435</para></param>
        /// <param name="openid">用户标识  <para>body用户的唯一标识，做安全校验使用，非必填。</para><para>校验规则：传入的openid得是调用方商户号（即请求头里面的商户号）有绑定关系的APPID获取的openid或传入的openid得是归属商户号有绑定关系的APPID获取的openid。获取openid文档</para><para>示例值：xsd3434454567676</para><para>可为null</para></param>
        public UseBusifavorCouponRequestData(string coupon_code, string stock_id, string appid, TenpayDateTime use_time, string use_request_no, string openid)
        {
            this.coupon_code = coupon_code;
            this.stock_id = stock_id;
            this.appid = appid;
            this.use_time = use_time.ToString();
            this.use_request_no = use_request_no;
            this.openid = openid;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public UseBusifavorCouponRequestData()
        {
        }

        /// <summary>
        /// 券code 
        /// <para>body 券的唯一标识。 </para>
        /// <para>示例值：sxxe34343434 </para>
        /// </summary>
        public string coupon_code { get; set; }

        /// <summary>
        /// 批次号 
        /// <para>body 微信为每个商家券批次分配的唯一ID，当你在创建商家券接口中的coupon_code_mode参数传值为MERCHANT_API或者MERCHANT_UPLOAD时，则核销接口中该字段必传，否则该字段可不传</para>
        /// <para>示例值：100088 </para>
        /// <para>可为null</para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 公众账号ID 
        /// <para>body 支持传入与当前调用接口商户号有绑定关系的appid。支持小程序appid与公众号appid。核销接口返回的openid会在该传入appid下进行计算获得。 </para>
        /// <para>校验规则：传入的APPID得是与调用方商户号（即请求头里面的商户号）有绑定关系的APPID或传入的APPID得是归属商户号有绑定关系的APPID</para>
        /// <para>示例值：wx1234567889999 </para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 请求核销时间 
        /// <para>body 商户请求核销用户券的时间。 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
        /// </summary>
        public string use_time { get; set; }

        /// <summary>
        /// 核销请求单据号 
        /// <para>body 每次核销请求的唯一标识，商户需保证唯一。 </para>
        /// <para>示例值：1002600620019090123143254435 </para>
        /// </summary>
        public string use_request_no { get; set; }

        /// <summary>
        /// 用户标识 
        /// <para>body 用户的唯一标识，做安全校验使用，非必填。 </para>
        /// <para>校验规则：传入的openid得是调用方商户号（即请求头里面的商户号）有绑定关系的APPID获取的openid或传入的openid得是归属商户号有绑定关系的APPID获取的openid。获取openid文档 </para>
        /// <para>示例值：xsd3434454567676 </para>
        /// <para>可为null</para>
        /// </summary>
        public string openid { get; set; }

    }
}
