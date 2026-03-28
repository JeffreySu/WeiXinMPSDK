#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc
  
    文件名：CancelTransferReturnJson.cs
    文件功能描述：商家转账 - 撤销转账API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 商家转账 - 撤销转账API 返回信息
    /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716458</para>
    /// </summary>
    public class CancelTransferReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 商户单号
        /// <para>商户系统内部的商家单号，要求此参数只能由数字、大小写字母组成，在商户系统内部唯一</para>
        /// <para>示例值：plfk2020042013</para>
        /// </summary>
        public string out_bill_no { get; set; }

        /// <summary>
        /// 微信转账单号
        /// <para>商家转账订单的主键，唯一定义此资源的标识</para>
        /// <para>示例值：1330000071100999991182020050700019480001</para>
        /// </summary>
        public string transfer_bill_no { get; set; }

        /// <summary>
        /// 单据状态
        /// <para>CANCELING: 撤销中；CANCELLED:已撤销</para>
        /// <para>示例值：CANCELING</para>
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 最后一次单据状态变更时间
        /// <para>按照使用rfc3339所定义的格式，格式为yyyy-MM-DDThh:mm:ss+TIMEZONE</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime update_time { get; set; }
    }
}

