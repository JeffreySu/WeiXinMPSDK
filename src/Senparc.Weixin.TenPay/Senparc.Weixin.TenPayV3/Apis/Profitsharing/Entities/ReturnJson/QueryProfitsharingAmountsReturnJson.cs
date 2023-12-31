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
  
    文件名：QueryProfitsharingAmountsReturnJson.cs
    文件功能描述：查询分账剩余待分金额返回Json类
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 查询分账剩余待分金额返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_6.shtml </para>
    /// </summary>
    public class QueryProfitsharingAmountsReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="transaction_id">微信订单号 <para>微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="unsplit_amount">订单剩余待分金额 <para>订单剩余待分金额，整数，单元为分</para><para>示例值：1000</para></param>
        public QueryProfitsharingAmountsReturnJson(string transaction_id, int unsplit_amount)
        {
            this.transaction_id = transaction_id;
            this.unsplit_amount = unsplit_amount;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryProfitsharingAmountsReturnJson()
        {
        }

        /// <summary>
        /// 微信订单号
        /// <para>微信支付订单号</para>
        /// <para>示例值：4208450740201411110007820472</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 订单剩余待分金额
        /// <para>订单剩余待分金额，整数，单元为分</para>
        /// <para>示例值：1000</para>
        /// </summary>
        public int unsplit_amount { get; set; }

    }


}
