#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2023 Senparc
  
    文件名：QueryProfitsharingAmountsRequestData.cs
    文件功能描述：查询分账剩余待分金额接口请求数据
    
    
    创建标识：Senparc - 20210915

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 查询分账剩余待分金额接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_6.shtml </para>
    /// </summary>
    public class QueryProfitsharingAmountsRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="transaction_id">微信订单号 <para>path微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        public QueryProfitsharingAmountsRequestData(string transaction_id)
        {
            this.transaction_id = transaction_id;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryProfitsharingAmountsRequestData()
        {
        }

        /// <summary>
        /// 微信订单号
        /// <para>path微信支付订单号</para>
        /// <para>示例值：4208450740201411110007820472</para>
        /// </summary>
        public string transaction_id { get; set; }

    }




}
