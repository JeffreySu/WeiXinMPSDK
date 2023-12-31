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
  
    文件名：BusinessCircleRefundNotifyJson.cs
    文件功能描述：商圈退款成功通知Json
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BusinessCircle
{
    /// <summary>
    /// 商圈退款成功通知Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_6_3.shtml </para>
    /// </summary>
    public class BusinessCircleRefundNotifyJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="mchid">商户号  <para>微信支付分配的商户号</para><para>示例值：1230000109</para></param>
        /// <param name="merchant_name">商圈商户名称  <para>商圈商户名称</para><para>示例值：微信支付</para></param>
        /// <param name="shop_name">门店名称 <para>门店名称，商圈在商圈小程序上圈店时填写的门店名称</para><para>示例值：微信支付</para></param>
        /// <param name="shop_number">门店编号 <para>门店编号，商圈在商圈小程序上圈店时填写的门店编号，用于跟商圈自身已有的商户识别码对齐</para><para>示例值：123456</para></param>
        /// <param name="appid">小程序APPID  <para>顾客授权积分时使用的小程序的appid</para><para>示例值：wx1234567890abcdef</para></param>
        /// <param name="openid">用户标识  <para>顾客授权时使用的小程序上的openid</para><para>示例值：oWmnN4xxxxxxxxxxe92NHIGf1xd8</para></param>
        /// <param name="refund_time">退款完成时间 <para>交易完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示北京时间2015年05月20日13点29分35秒（需要增加所有跟时间有关的参数的描述）</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
        /// <param name="pay_amount">消费金额 <para>用户实际消费金额，单位（分）</para><para>示例值：100</para></param>
        /// <param name="refund_amount">退款金额 <para>用户退款金额，单位（分）</para><para>示例值：100</para></param>
        /// <param name="transaction_id">微信支付订单号 <para>微信支付订单号</para><para>示例值：1234567890</para></param>
        /// <param name="refund_id">微信支付退款单号 <para>微信支付退款单号</para><para>示例值：1217752501201407033233368999</para></param>
        public BusinessCircleRefundNotifyJson(string mchid, string merchant_name, string shop_name, string shop_number, string appid, string openid, string refund_time, long pay_amount, long refund_amount, string transaction_id, string refund_id)
        {
            this.mchid = mchid;
            this.merchant_name = merchant_name;
            this.shop_name = shop_name;
            this.shop_number = shop_number;
            this.appid = appid;
            this.openid = openid;
            this.refund_time = refund_time;
            this.pay_amount = pay_amount;
            this.refund_amount = refund_amount;
            this.transaction_id = transaction_id;
            this.refund_id = refund_id;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BusinessCircleRefundNotifyJson()
        {
        }

        /// <summary>
        /// 商户号 
        /// <para>微信支付分配的商户号 </para>
        /// <para>示例值：1230000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 商圈商户名称 
        /// <para>商圈商户名称 </para>
        /// <para>示例值：微信支付</para>
        /// </summary>
        public string merchant_name { get; set; }

        /// <summary>
        /// 门店名称
        /// <para>门店名称，商圈在商圈小程序上圈店时填写的门店名称 </para>
        /// <para>示例值：微信支付</para>
        /// </summary>
        public string shop_name { get; set; }

        /// <summary>
        /// 门店编号
        /// <para>门店编号，商圈在商圈小程序上圈店时填写的门店编号，用于跟商圈自身已有的商户识别码对齐</para>
        /// <para>示例值：123456</para>
        /// </summary>
        public string shop_number { get; set; }

        /// <summary>
        /// 小程序APPID 
        /// <para>顾客授权积分时使用的小程序的appid</para>
        /// <para>示例值：wx1234567890abcdef </para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 用户标识 
        /// <para>顾客授权时使用的小程序上的openid </para>
        /// <para>示例值：oWmnN4xxxxxxxxxxe92NHIGf1xd8</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 退款完成时间
        /// <para>交易完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示北京时间2015年05月20日13点29分35秒（需要增加所有跟时间有关的参数的描述） </para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public string refund_time { get; set; }

        /// <summary>
        /// 消费金额
        /// <para>用户实际消费金额，单位（分） </para>
        /// <para>示例值：100</para>
        /// </summary>
        public long pay_amount { get; set; }

        /// <summary>
        /// 退款金额
        /// <para>用户退款金额，单位（分） </para>
        /// <para>示例值：100</para>
        /// </summary>
        public long refund_amount { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// <para>微信支付订单号</para>
        /// <para>示例值：1234567890</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 微信支付退款单号
        /// <para>微信支付退款单号</para>
        /// <para>示例值：1217752501201407033233368999</para>
        /// </summary>
        public string refund_id { get; set; }

    }




}
