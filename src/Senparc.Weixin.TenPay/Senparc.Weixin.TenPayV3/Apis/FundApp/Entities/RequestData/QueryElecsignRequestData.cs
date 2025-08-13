#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：QueryElecsignRequestData.cs
    文件功能描述：商家转账 - 查询电子回单API 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 商家转账 - 商户单号查询电子回单API 请求数据
    /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716436</para>
    /// </summary>
    public class QueryElecsignByOutBillNoRequestData
    {
        /// <summary>
        /// 商户转账单号
        /// <para>商户创建转账单据使用的单号</para>
        /// <para>示例值：plfk2020042013</para>
        /// </summary>
        public string out_bill_no { get; set; }
    }

    /// <summary>
    /// 商家转账 - 微信单号查询电子回单API 请求数据
    /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716455</para>
    /// </summary>
    public class QueryElecsignByBillNoRequestData
    {
        /// <summary>
        /// 微信转账单号
        /// <para>微信转账单号，微信商家转账系统返回的唯一标识</para>
        /// <para>示例值：1330000071100999991182020050700019480001</para>
        /// </summary>
        public string transfer_bill_no { get; set; }
    }
}
