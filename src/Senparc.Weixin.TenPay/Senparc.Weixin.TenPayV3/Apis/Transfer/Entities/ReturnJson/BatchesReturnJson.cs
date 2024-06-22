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
  
    文件名：BatchesReturnJson.cs
    文件功能描述：发起商家转账API 返回信息
    
    
    创建标识：Senparc - 20220629

    修改标识：Guili95 - 20240623
    修改描述：v1.4.0 添加：微信支付-发起商家转账入参添加转账场景ID、通知地址；返回结果添加批次状态

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Transfer
{
    /// <summary>
    /// 发起商家转账API 返回信息
    /// </summary>
    public class BatchesReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 商家批次单号
        /// <para>商户系统内部的商家批次单号。示例值：plfk2020042013</para>
        /// </summary>
        public string out_batch_no { get; set; }
        /// <summary>
        /// 微信批次单号
        /// <para>微信批次单号，微信商家转账系统返回的唯一标识。示例值：1030000071100999991182020050700019480001</para>
        /// </summary>
        public string batch_id { get; set; }
        /// <summary>
        /// 批次创建时间
        /// <para>批次受理成功时返回，遵循rfc3339标准格式，格式为yyyy-MM-DDTHH:mm:ss.sss+TIMEZONE，yyyy-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }
        /// <summary>
        /// 批次状态
        /// <para>ACCEPTED:已受理。批次已受理成功，若发起批量转账的30分钟后，转账批次单仍处于该状态，可能原因是商户账户余额不足等。商户可查询账户资金流水，若该笔转账批次单的扣款已经发生，则表示批次已经进入转账中，请再次查单确认</para>
        /// <para>PROCESSING:转账中。已开始处理批次内的转账明细单</para>
        /// <para>FINISHED:已完成。批次内的所有转账明细单都已处理完成</para>
        /// <para>CLOSED:已关闭。可查询具体的批次关闭原因确认</para>
        /// </summary>
        public string batch_status { get; set; }
    }

}
