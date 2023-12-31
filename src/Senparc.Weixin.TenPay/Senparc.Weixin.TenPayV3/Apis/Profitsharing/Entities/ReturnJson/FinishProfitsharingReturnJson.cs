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
  
    文件名：FinishProfitsharingReturnJson.cs
    文件功能描述：连锁品牌分账 - 完结分账接口响应数据
    
    
    创建标识：Senparc - 20210915

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing.Entities.ReturnJson
{
    /// <summary>
    /// 连锁品牌分账 - 完结分账接口响应数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_7_5.shtml </para>
    /// </summary>
    public class FinishProfitsharingReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public FinishProfitsharingReturnJson()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para><para>示例值：1900000109</para></param>
        /// <param name="transaction_id">微信订单号 <para>微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        /// <param name="order_id">微信分账单号 <para>微信分账单号，微信系统返回的唯一标识</para><para>示例值：3008450740201411110007820472</para></param>
        public FinishProfitsharingReturnJson(string sub_mchid, string transaction_id, string out_order_no, string order_id)
        {
            this.sub_mchid = sub_mchid;
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
            this.order_id = order_id;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式返回
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 微信订单号
        /// <para>微信支付订单号</para>
        /// <para>示例值：4208450740201411110007820472</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户分账单号
        /// <para>商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@ </para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 微信分账单号
        /// <para>微信分账单号，微信系统返回的唯一标识</para>
        /// <para>示例值：3008450740201411110007820472</para>
        /// </summary>
        public string order_id { get; set; }
    }
}
