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
  
    文件名：CreateProfitsharingReturnJson.cs
    文件功能描述：请求分账返回Json类
    
    
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
    /// 请求分账返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chFapter8_1_1.shtml </para>
    /// </summary>
    public class CreateProfitsharingReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CreateProfitsharingReturnJson()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="transaction_id">微信订单号 <para>微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        /// <param name="order_id">微信分账单号 <para>微信分账单号，微信系统返回的唯一标识</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="state">分账单状态 <para>分账单状态（每个接收方的分账结果请查看receivers中的result字段），枚举值：1、PROCESSING：处理中2、FINISHED：分账完成</para><para>示例值：FINISHED</para></param>
        /// <param name="receivers">分账接收方列表 <para>分账接收方列表</para><para>可为null</para></param>
        public CreateProfitsharingReturnJson(string transaction_id, string out_order_no, string order_id, string state, Receiver[] receivers)
        {
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
            this.order_id = order_id;
            this.state = state;
            this.receivers = receivers;
        }

        /// <summary>
        /// 含参构造函数（服务商模式）
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para><para>示例值：1900000109</para></param>
        /// <param name="transaction_id">微信订单号 <para>微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        /// <param name="order_id">微信分账单号 <para>微信分账单号，微信系统返回的唯一标识</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="state">分账单状态 <para>分账单状态（每个接收方的分账结果请查看receivers中的result字段），枚举值：1、PROCESSING：处理中2、FINISHED：分账完成</para><para>示例值：FINISHED</para></param>
        /// <param name="receivers">分账接收方列表 <para>分账接收方列表</para><para>可为null</para></param>
        public CreateProfitsharingReturnJson(string sub_mchid, string transaction_id, string out_order_no, string order_id, string state, Receiver[] receivers)
        {
            this.sub_mchid = sub_mchid;
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
            this.order_id = order_id;
            this.state = state;
            this.receivers = receivers;
        }

        /// <summary>
        /// 含参构造函数（服务商模式-连锁品牌）
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para><para>示例值：1900000109</para></param>
        /// <param name="transaction_id">微信订单号 <para>微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        /// <param name="order_id">微信分账单号 <para>微信分账单号，微信系统返回的唯一标识</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="state">分账单状态 <para>分账单状态（每个接收方的分账结果请查看receivers中的result字段），枚举值：1、PROCESSING：处理中2、FINISHED：分账完成</para><para>示例值：FINISHED</para></param>
        /// <param name="receivers">分账接收方列表 <para>分账接收方列表</para><para>可为null</para></param>
        public CreateProfitsharingReturnJson(string brand_mchid, string sub_mchid, string transaction_id, string out_order_no, string order_id, string state, Receiver[] receivers)
        {
            this.brand_mchid = brand_mchid;
            this.sub_mchid = sub_mchid;
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
            this.order_id = order_id;
            this.state = state;
            this.receivers = receivers;
        }

        #region 品牌连锁
        /// <summary>
        /// 品牌主商户号 
        /// 连锁平台需要
        /// <para>品牌主商户号，填写微信支付分配的商户号。</para>
        /// </summary>
        public string brand_mchid { get; set; }
        #endregion

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

        /// <summary>
        /// 分账单状态
        /// <para>分账单状态（每个接收方的分账结果请查看receivers中的result字段），枚举值： 1、PROCESSING：处理中  2、FINISHED：分账完成 </para>
        /// <para>示例值：FINISHED</para>
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 分账接收方列表
        /// <para>分账接收方列表</para>
        /// <para>可为null</para>
        /// </summary>
        public Receiver[] receivers { get; set; }

        #region 子数据类型
        public class Receiver
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="amount">分账金额 <para>分账金额，单位为分，只能为整数，不能超过原订单支付金额及最大分账比例金额</para><para>示例值：100</para></param>
            /// <param name="description">分账描述 <para>分账的原因描述，分账账单中需要体现</para><para>示例值：分给商户1900000110</para></param>
            /// <param name="type">接收方类型 <para>1、MERCHANT_ID：商户号2、PERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
            /// <param name="account">接收方账号 <para>1、分账接收方类型为MERCHANT_ID时，分账接收方账号为商户号2、分账接收方类型为PERSONAL_OPENID时，分账接收方账号为个人openid</para><para>示例值：1900000109</para></param>
            /// <param name="result">分账结果 <para>枚举值：1、PENDING：待分账2、SUCCESS：分账成功3、CLOSED：已关闭</para><para>示例值：SUCCESS</para></param>
            /// <param name="fail_reason">分账失败原因 <para>分账失败原因。包含以下枚举值：1、ACCOUNT_ABNORMAL:分账接收账户异常2、NO_RELATION:分账关系已解除3、RECEIVER_HIGH_RISK:高风险接收方4、RECEIVER_REAL_NAME_NOT_VERIFIED:接收方未实名5、NO_AUTH:分账权限已解除</para><para>示例值：ACCOUNT_ABNORMAL</para></param>
            /// <param name="create_time">分账创建时间 <para>分账创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
            /// <param name="finish_time">分账完成时间 <para>分账完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
            /// <param name="detail_id">分账明细单号 <para>微信分账明细单号，每笔分账业务执行的明细单号，可与资金账单对账使用</para><para>示例值：36011111111111111111111</para></param>
            public Receiver(int amount, string description, string type, string account, string result, string fail_reason, string create_time, string finish_time, string detail_id)
            {
                this.amount = amount;
                this.description = description;
                this.type = type;
                this.account = account;
                this.result = result;
                this.fail_reason = fail_reason;
                this.create_time = create_time;
                this.finish_time = finish_time;
                this.detail_id = detail_id;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Receiver()
            {
            }

            /// <summary>
            /// 分账金额
            /// <para>分账金额，单位为分，只能为整数，不能超过原订单支付金额及最大分账比例金额</para>
            /// <para>示例值：100</para>
            /// </summary>
            public int amount { get; set; }

            /// <summary>
            /// 分账描述
            /// <para>分账的原因描述，分账账单中需要体现</para>
            /// <para>示例值：分给商户1900000110</para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 接收方类型
            /// <para>1、MERCHANT_ID：商户号 2、PERSONAL_OPENID：个人openid（由父商户APPID转换得到） </para>
            /// <para>示例值：MERCHANT_ID</para>
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 接收方账号
            /// <para>1、分账接收方类型为MERCHANT_ID时，分账接收方账号为商户号2、分账接收方类型为PERSONAL_OPENID时，分账接收方账号为个人openid</para>
            /// <para>示例值：1900000109</para>
            /// </summary>
            public string account { get; set; }

            /// <summary>
            /// 分账结果
            /// <para>枚举值： 1、PENDING：待分账 2、SUCCESS：分账成功 3、CLOSED：已关闭 </para>
            /// <para>示例值：SUCCESS</para>
            /// </summary>
            public string result { get; set; }

            /// <summary>
            /// 分账失败原因
            /// <para>分账失败原因。包含以下枚举值： 1、ACCOUNT_ABNORMAL : 分账接收账户异常  2、NO_RELATION : 分账关系已解除 3、RECEIVER_HIGH_RISK : 高风险接收方  4、RECEIVER_REAL_NAME_NOT_VERIFIED : 接收方未实名 5、NO_AUTH : 分账权限已解除 </para>
            /// <para>示例值：ACCOUNT_ABNORMAL</para>
            /// </summary>
            public string fail_reason { get; set; }

            /// <summary>
            /// 分账创建时间
            /// <para>分账创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
            /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
            /// </summary>
            public string create_time { get; set; }

            /// <summary>
            /// 分账完成时间
            /// <para>分账完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
            /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
            /// </summary>
            public string finish_time { get; set; }

            /// <summary>
            /// 分账明细单号
            /// <para>微信分账明细单号，每笔分账业务执行的明细单号，可与资金账单对账使用</para>
            /// <para>示例值：36011111111111111111111</para>
            /// </summary>
            public string detail_id { get; set; }

        }
        #endregion
    }
}
