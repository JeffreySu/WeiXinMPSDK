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
  
    文件名：DeactivateBusifavorCouponsRequestData.cs
    文件功能描述：使商家券失效接口请求数据
    
    
    创建标识：Senparc - 20210914
    
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
    /// 使商家券失效接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_12.shtml </para>
    /// </summary>
    public class DeactivateBusifavorCouponRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="coupon_code">券code <para>body券的唯一标识</para><para>示例值：sxxe34343434</para></param>
        /// <param name="stock_id">批次号 <para>body券的所属批次号</para><para>示例值：1234567891</para></param>
        /// <param name="deactivate_request_no">失效请求单据号 <para>body每次失效请求的唯一标识，商户需保证唯一</para><para>示例值：1002600620019090123143254436</para></param>
        /// <param name="deactivate_reason">失效原因 <para>body商户失效券的原因</para><para>示例值：此券使用时间设置错误</para><para>可为null</para></param>
        public DeactivateBusifavorCouponRequestData(string coupon_code, string stock_id, string deactivate_request_no, string deactivate_reason)
        {
            this.coupon_code = coupon_code;
            this.stock_id = stock_id;
            this.deactivate_request_no = deactivate_request_no;
            this.deactivate_reason = deactivate_reason;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DeactivateBusifavorCouponRequestData()
        {
        }

        /// <summary>
        /// 券code
        /// <para>body券的唯一标识 </para>
        /// <para>示例值：sxxe34343434</para>
        /// </summary>
        public string coupon_code { get; set; }

        /// <summary>
        /// 批次号
        /// <para>body券的所属批次号 </para>
        /// <para>示例值：1234567891</para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 失效请求单据号
        /// <para>body每次失效请求的唯一标识，商户需保证唯一 </para>
        /// <para>示例值：1002600620019090123143254436</para>
        /// </summary>
        public string deactivate_request_no { get; set; }

        /// <summary>
        /// 失效原因
        /// <para>body商户失效券的原因 </para>
        /// <para>示例值：此券使用时间设置错误</para>
        /// <para>可为null</para>
        /// </summary>
        public string deactivate_reason { get; set; }

    }
}
