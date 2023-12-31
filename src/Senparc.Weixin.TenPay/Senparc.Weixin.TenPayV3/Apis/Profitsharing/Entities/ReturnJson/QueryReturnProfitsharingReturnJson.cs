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
    文件功能描述：查询分账回退结果返回Json类
    
    
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
    /// 查询分账回退结果返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_4.shtml </para>
    /// </summary>
    public class QueryReturnProfitsharingReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryReturnProfitsharingReturnJson()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="order_id">微信分账单号 <para>原发起分账请求时，微信返回的微信分账单号，与商户分账单号一一对应。</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>原发起分账请求时使用的商户系统内部的分账单号</para><para>示例值：P20150806125346</para></param>
        /// <param name="out_return_no">商户回退单号 <para>调用回退接口提供的商户系统内部的回退单号</para><para>示例值：R20190516001</para></param>
        /// <param name="return_id">微信回退单号 <para>微信分账回退单号，微信系统返回的唯一标识</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="return_mchid">回退商户号 <para>只能对原分账请求中成功分给商户接收方进行回退</para><para>示例值：86693852</para></param>
        /// <param name="amount">回退金额 <para>需要从分账接收方回退的金额，单位为分，只能为整数</para><para>示例值：10</para></param>
        /// <param name="description">回退描述 <para>分账回退的原因描述</para><para>示例值：用户退款</para></param>
        /// <param name="result">回退结果 <para>如果请求返回为处理中，则商户可以通过调用回退结果查询接口获取请求的最终处理结果。如果查询到回退结果在处理中，请勿变更商户回退单号，使用相同的参数再次发起分账回退，否则会出现资金风险。在处理中状态的回退单如果5天没有成功，会因为超时被设置为已失败。枚举值：PROCESSING：处理中SUCCESS：已成功FAILED：已失败</para><para>示例值：SUCCESS</para></param>
        /// <param name="fail_reason">失败原因 <para>失败原因。包含以下枚举值：ACCOUNT_ABNORMAL:分账接收方账户异常TIME_OUT_CLOSED:超时关单</para><para>示例值：TIME_OUT_CLOSED</para><para>可为null</para></param>
        /// <param name="create_time">创建时间 <para>分账回退创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
        /// <param name="finish_time">完成时间 <para>分账回退完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
        public QueryReturnProfitsharingReturnJson(string order_id, string out_order_no, string out_return_no, string return_id, string return_mchid, int amount, string description, string result, string fail_reason, string create_time, string finish_time)
        {
            this.order_id = order_id;
            this.out_order_no = out_order_no;
            this.out_return_no = out_return_no;
            this.return_id = return_id;
            this.return_mchid = return_mchid;
            this.amount = amount;
            this.description = description;
            this.result = result;
            this.fail_reason = fail_reason;
            this.create_time = create_time;
            this.finish_time = finish_time;
        }

        /// <summary>
        /// 含参构造函数（服务商模式）
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para><para>示例值：1900000109</para></param>
        /// <param name="order_id">微信分账单号 <para>原发起分账请求时，微信返回的微信分账单号，与商户分账单号一一对应。</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>原发起分账请求时使用的商户系统内部的分账单号</para><para>示例值：P20150806125346</para></param>
        /// <param name="out_return_no">商户回退单号 <para>调用回退接口提供的商户系统内部的回退单号</para><para>示例值：R20190516001</para></param>
        /// <param name="return_id">微信回退单号 <para>微信分账回退单号，微信系统返回的唯一标识</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="return_mchid">回退商户号 <para>只能对原分账请求中成功分给商户接收方进行回退</para><para>示例值：86693852</para></param>
        /// <param name="amount">回退金额 <para>需要从分账接收方回退的金额，单位为分，只能为整数</para><para>示例值：10</para></param>
        /// <param name="description">回退描述 <para>分账回退的原因描述</para><para>示例值：用户退款</para></param>
        /// <param name="result">回退结果 <para>如果请求返回为处理中，则商户可以通过调用回退结果查询接口获取请求的最终处理结果。如果查询到回退结果在处理中，请勿变更商户回退单号，使用相同的参数再次发起分账回退，否则会出现资金风险。在处理中状态的回退单如果5天没有成功，会因为超时被设置为已失败。枚举值：PROCESSING：处理中SUCCESS：已成功FAILED：已失败</para><para>示例值：SUCCESS</para></param>
        /// <param name="fail_reason">失败原因 <para>失败原因。包含以下枚举值：ACCOUNT_ABNORMAL:分账接收方账户异常TIME_OUT_CLOSED:超时关单</para><para>示例值：TIME_OUT_CLOSED</para><para>可为null</para></param>
        /// <param name="create_time">创建时间 <para>分账回退创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
        /// <param name="finish_time">完成时间 <para>分账回退完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
        public QueryReturnProfitsharingReturnJson(string sub_mchid, string order_id, string out_order_no, string out_return_no, string return_id, string return_mchid, int amount, string description, string result, string fail_reason, string create_time, string finish_time)
        {
            this.sub_mchid = sub_mchid;
            this.order_id = order_id;
            this.out_order_no = out_order_no;
            this.out_return_no = out_return_no;
            this.return_id = return_id;
            this.return_mchid = return_mchid;
            this.amount = amount;
            this.description = description;
            this.result = result;
            this.fail_reason = fail_reason;
            this.create_time = create_time;
            this.finish_time = finish_time;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式返回
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 微信分账单号
        /// <para>原发起分账请求时，微信返回的微信分账单号，与商户分账单号一一对应。</para>
        /// <para>示例值：3008450740201411110007820472</para>
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 商户分账单号
        /// <para>原发起分账请求时使用的商户系统内部的分账单号</para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 商户回退单号
        /// <para>调用回退接口提供的商户系统内部的回退单号</para>
        /// <para>示例值：R20190516001</para>
        /// </summary>
        public string out_return_no { get; set; }

        /// <summary>
        /// 微信回退单号
        /// <para>微信分账回退单号，微信系统返回的唯一标识</para>
        /// <para>示例值：3008450740201411110007820472</para>
        /// </summary>
        public string return_id { get; set; }

        /// <summary>
        /// 回退商户号
        /// <para>只能对原分账请求中成功分给商户接收方进行回退</para>
        /// <para>示例值：86693852</para>
        /// </summary>
        public string return_mchid { get; set; }

        /// <summary>
        /// 回退金额
        /// <para>需要从分账接收方回退的金额，单位为分，只能为整数</para>
        /// <para>示例值：10</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 回退描述
        /// <para>分账回退的原因描述</para>
        /// <para>示例值：用户退款</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 回退结果
        /// <para>如果请求返回为处理中，则商户可以通过调用回退结果查询接口获取请求的最终处理结果。如果查询到回退结果在处理中，请勿变更商户回退单号，使用相同的参数再次发起分账回退，否则会出现资金风险。在处理中状态的回退单如果5天没有成功，会因为超时被设置为已失败。 枚举值： PROCESSING：处理中 SUCCESS：已成功 FAILED：已失败</para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 失败原因
        /// <para>失败原因。包含以下枚举值： ACCOUNT_ABNORMAL : 分账接收方账户异常 TIME_OUT_CLOSED : 超时关单 </para>
        /// <para>示例值：TIME_OUT_CLOSED</para>
        /// <para>可为null</para>
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 创建时间
        /// <para>分账回退创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 完成时间
        /// <para>分账回退完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public string finish_time { get; set; }

    }


}
