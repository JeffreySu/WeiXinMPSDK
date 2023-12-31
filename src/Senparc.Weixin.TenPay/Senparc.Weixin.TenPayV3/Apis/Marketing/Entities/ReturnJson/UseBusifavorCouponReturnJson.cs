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
  
    文件名：UseBusifavorCouponReturnJson.cs
    文件功能描述：核销商家券返回Json类
    
    
    创建标识：Senparc - 20210907
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 核销商家券返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_3.shtml </para>
    /// </summary>
    public class UseBusifavorCouponReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="stock_id">批次号  <para>微信为每个商家券批次分配的唯一ID</para><para>示例值： 100088</para></param>
        /// <param name="openid">用户标识  <para>用户在公众号内的唯一身份标识。</para><para>示例值：dsadas34345454545</para></param>
        /// <param name="wechatpay_use_time">系统核销券成功的时间  <para>系统成功核销券的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
        public UseBusifavorCouponReturnJson(string stock_id, string openid, DateTime wechatpay_use_time)
        {
            this.stock_id = stock_id;
            this.openid = openid;
            this.wechatpay_use_time = wechatpay_use_time;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public UseBusifavorCouponReturnJson()
        {
        }

        /// <summary>
        /// 批次号 
        /// <para>微信为每个商家券批次分配的唯一ID </para>
        /// <para>示例值： 100088 </para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 用户标识 
        /// <para>用户在公众号内的唯一身份标识。 </para>
        /// <para>示例值：dsadas34345454545 </para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 系统核销券成功的时间 
        /// <para>系统成功核销券的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
        /// </summary>
        public DateTime wechatpay_use_time { get; set; }

    }


}
