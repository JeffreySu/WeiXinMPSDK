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
  
    文件名：StockNotifyJson.cs
    文件功能描述：分账动账回调通知Json
    
    
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
    /// 微信支付V3核销事件回调通知Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_10.shtml </para>
    /// </summary>
    public class ProfitsharingNotifyJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="mchid">直连商户号  <para>直连模式分账发起和出资商户。</para><para>示例值：1900000100</para></param>
        /// <param name="transaction_id">微信订单号  <para>微信支付订单号。</para><para>示例值：4200000000000000000000000000</para></param>
        /// <param name="order_id">微信分账/回退单号  <para>微信分账/回退单号。</para><para>示例值：1217752501201407033233368018</para></param>
        /// <param name="out_order_no">商户分账/回退单号  <para>分账方系统内部的分账/回退单号。</para><para>示例值：P20150806125346</para></param>
        /// <param name="receiver">分账接收方列表 <para>分账接收方对象</para></param>
        /// <param name="success_time">成功时间  <para>成功时间，遵循rfc3339标准格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2018-06-08T10:34:56+08:00</para></param>
        public ProfitsharingNotifyJson(string mchid, string transaction_id, string order_id, string out_order_no, Receiver receiver, string success_time)
        {
            this.mchid = mchid;
            this.transaction_id = transaction_id;
            this.order_id = order_id;
            this.out_order_no = out_order_no;
            this.receiver = receiver;
            this.success_time = success_time;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ProfitsharingNotifyJson()
        {
        }

        /// <summary>
        /// 直连商户号 
        /// <para>直连模式分账发起和出资商户。 </para>
        /// <para>示例值：1900000100</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 微信订单号 
        /// <para>微信支付订单号。 </para>
        /// <para>示例值：        4200000000000000000000000000</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 微信分账/回退单号 
        /// <para>微信分账/回退单号。 </para>
        /// <para>示例值：        1217752501201407033233368018 </para>
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 商户分账/回退单号 
        /// <para>分账方系统内部的分账/回退单号。 </para>
        /// <para>示例值：P20150806125346 </para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 分账接收方列表
        /// <para>分账接收方对象</para>
        /// </summary>
        public Receiver receiver { get; set; }

        /// <summary>
        /// 成功时间 
        /// <para>成功时间，遵循rfc3339标准 格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
        /// <para>示例值：2018-06-08T10:34:56+08:00 </para>
        /// </summary>
        public string success_time { get; set; }

        #region 子数据类型
        public class Receiver
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="type">分账接收方类型  <para>分账接收方的类型，枚举值：MERCHANT_ID：商户PERSONAL_OPENID：个人</para><para>示例值：MERCHANT_ID</para></param>
            /// <param name="account">分账接收方账号  <para>分账接收方的账号类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：190001001</para></param>
            /// <param name="amount">分账动账金额  <para>分账动账金额，单位为分，只能为整数。</para><para>示例值：888</para></param>
            /// <param name="description">分账/回退描述  <para>分账/回退描述</para><para>示例值：运费/交易分账/及时奖励</para></param>
            public Receiver(string type, string account, int amount, string description)
            {
                this.type = type;
                this.account = account;
                this.amount = amount;
                this.description = description;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Receiver()
            {
            }

            /// <summary>
            /// 分账接收方类型 
            /// <para> 分账接收方的类型，枚举值： MERCHANT_ID：商户 PERSONAL_OPENID：个人 </para>
            /// <para>示例值：MERCHANT_ID</para>
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 分账接收方账号 
            /// <para> 分账接收方的账号 类型是MERCHANT_ID时，是商户号 类型是PERSONAL_OPENID时，是个人openid </para>
            /// <para>示例值：190001001</para>
            /// </summary>
            public string account { get; set; }

            /// <summary>
            /// 分账动账金额 
            /// <para>分账动账金额，单位为分，只能为整数。 </para>
            /// <para>示例值：888 </para>
            /// </summary>
            public int amount { get; set; }

            /// <summary>
            /// 分账/回退描述 
            /// <para>分账/回退描述 </para>
            /// <para>示例值：运费/交易分账/及时奖励 </para>
            /// </summary>
            public string description { get; set; }

        }


        #endregion
    }
}
