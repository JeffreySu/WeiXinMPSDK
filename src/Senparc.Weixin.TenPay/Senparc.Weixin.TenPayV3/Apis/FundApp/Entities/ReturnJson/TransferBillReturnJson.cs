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
  
    文件名：TransferBillReturnJson.cs
    文件功能描述：商家转账 - 发起转账API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 商家转账 - 发起转账API 返回信息
    /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716434</para>
    /// </summary>
    public class TransferBillReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 商户单号
        /// <para>商户系统内部的商家单号，要求此参数只能由数字、大小写字母组成，在商户系统内部唯一</para>
        /// <para>示例值：plfk2020042013</para>
        /// </summary>
        public string out_bill_no { get; set; }

        /// <summary>
        /// 微信转账单号
        /// <para>微信转账单号，微信商家转账系统返回的唯一标识</para>
        /// <para>示例值：1330000071100999991182020050700019480001</para>
        /// </summary>
        public string transfer_bill_no { get; set; }

        /// <summary>
        /// 单据创建时间
        /// <para>单据受理成功时返回，按照使用rfc3339所定义的格式，格式为yyyy-MM-DDThh:mm:ss+TIMEZONE</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 单据状态
        /// <para>商家转账订单状态</para>
        /// <para>可选取值：</para>
        /// <para>ACCEPTED: 转账已受理</para>
        /// <para>PROCESSING: 转账锁定资金中。如果一直停留在该状态，建议检查账户余额是否足够，如余额不足，可充值后再原单重试。</para>
        /// <para>WAIT_USER_CONFIRM: 待收款用户确认，可拉起微信收款确认页面进行收款确认</para>
        /// <para>TRANSFERING: 转账中，可拉起微信收款确认页面再次重试确认收款</para>
        /// <para>SUCCESS: 转账成功</para>
        /// <para>FAIL: 转账失败</para>
        /// <para>CANCELING: 商户撤销请求受理成功，该笔转账正在撤销中</para>
        /// <para>CANCELLED: 转账撤销完成</para>
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 跳转领取页面的package信息
        /// <para>跳转微信支付收款页的package信息，APP调起用户确认收款或者JSAPI调起用户确认收款 时需要使用的参数。</para>
        /// <para>单据创建后，用户24小时内不领取将过期关闭，建议拉起用户确认收款页面前，先查单据状态：如单据状态为待收款用户确认，可用之前的package信息拉起；单据到终态时需更换单号重新发起转账。</para>
        /// <para>示例值：affffddafdfafddffda==</para>
        /// </summary>
        public string package_info { get; set; }
    }
}
