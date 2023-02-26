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
  
    文件名：CreateProfitsharingRequestData.cs
    文件功能描述：查询分账回退结果接口请求数据
    
    
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
    /// 查询分账回退结果账接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_4.shtml </para>
    /// </summary>
    public class QueryReturnProfitsharingRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="out_return_no">商户回退单号 <para>path调用回退接口提供的商户系统内部的回退单号</para><para>示例值：R20190516001</para></param>
        /// <param name="out_order_no">商户分账单号 <para>query原发起分账请求时使用的商户系统内部的分账单号</para><para>示例值：P20190806125346</para></param>
        public QueryReturnProfitsharingRequestData(string out_return_no, string out_order_no)
        {
            this.out_return_no = out_return_no;
            this.out_order_no = out_order_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryReturnProfitsharingRequestData()
        {
        }

        /// <summary>
        /// 商户回退单号
        /// <para>path调用回退接口提供的商户系统内部的回退单号</para>
        /// <para>示例值：R20190516001</para>
        /// </summary>
        public string out_return_no { get; set; }

        /// <summary>
        /// 商户分账单号
        /// <para>query原发起分账请求时使用的商户系统内部的分账单号</para>
        /// <para>示例值：P20190806125346</para>
        /// </summary>
        public string out_order_no { get; set; }

    }
}
