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
  
    文件名：ReturnBusifavorCouponsRequestData.cs
    文件功能描述：申请退券接口请求数据
    
    
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
    /// 申请退券接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_13.shtml </para>
    /// </summary>
    public class ReturnBusifavorCouponRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="coupon_code">券code <para>body券的唯一标识</para><para>示例值：sxxe34343434</para></param>
        /// <param name="stock_id">批次号 <para>body券的所属批次号</para><para>示例值：1234567891</para></param>
        /// <param name="return_request_no">退券请求单据号 <para>body每次退券请求的唯一标识，商户需保证唯一</para><para>示例值：1002600620019090123143254436</para></param>
        public ReturnBusifavorCouponRequestData(string coupon_code, string stock_id, string return_request_no)
        {
            this.coupon_code = coupon_code;
            this.stock_id = stock_id;
            this.return_request_no = return_request_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ReturnBusifavorCouponRequestData()
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
        /// 退券请求单据号
        /// <para>body每次退券请求的唯一标识，商户需保证唯一 </para>
        /// <para>示例值：1002600620019090123143254436</para>
        /// </summary>
        public string return_request_no { get; set; }

    }


}
