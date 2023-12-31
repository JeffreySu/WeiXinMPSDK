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
  
    文件名：RefundNotifyJson.cs
    文件功能描述：微信支付V3退款回调通知Json
    
    
    创建标识：Senparc - 20210820
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay.Entities
{
    /// <summary>
    /// 微信支付V3退款回调通知Json
    /// 本类型为微信支付回调通知退款信息 请勿与RefundReturnJson混淆
    /// </summary>
    public class RefundNotifyJson : ReturnJsonBase
    {
        /// <summary>
        /// 直连商户号
        /// 直连商户的商户号，由微信支付生成并下发。
        /// 示例值：1900000100
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 商户订单号
        /// 原支付交易对应的商户订单号
        /// 示例值：1217752501201407033233368018
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 微信支付交易订单号
        /// 示例值：1217752501201407033233368018
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户退款单号
        /// 商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
        /// 示例值：1217752501201407033233368018
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 微信支付退款单号
        /// 示例值：50000000382019052709732678859
        /// </summary>
        public string refund_id { get; set; }

        /// <summary>
        /// 退款状态，枚举值：
        /// SUCCESS：退款成功
        /// CLOSE：退款关闭
        /// ABNORMAL：退款异常，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，可前往【商户平台—>交易中心】，手动处理此笔退款
        /// 示例值：SUCCESS
        /// </summary>
        public string refund_status { get; set; }

        /// <summary>
        /// 退款成功时间
        /// 退款成功时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD
        /// 示例值：2018-06-08T10:34:56+08:00
        /// </summary>
        public string success_time { get; set; }

        /// <summary>
        /// 退款入账账户
        /// 取当前退款单的退款入账方，有以下几种情况：
        /// 1）退回银行卡：{银行名称}{卡类型}{卡尾号}
        /// 2）退回支付用户零钱:支付用户零钱
        /// 3）退还商户:商户基本账户商户结算银行账户
        /// 4）退回支付用户零钱通:支付用户零钱通
        /// 示例值：招商银行信用卡0403
        /// </summary>
        public string user_received_account { get; set; }

        /// <summary>
        /// 金额详细信息
        /// </summary>
        public Amount amount { get; set; }

        /// <summary>
        /// 金额详细信息
        /// </summary>
        public class Amount
        {
            /// <summary>
            /// 总金额
            /// 订单总金额，单位为分。
            /// 示例值：100 (1元)
            /// </summary>
            public int total { get; set; }

            /// <summary>
            /// 退款金额	
            /// 退款标价金额，单位为分，可以做部分退款
            /// 示例值：100
            public int refund { get; set; }

            /// <summary>
            /// 用户支付金额
            /// 现金支付金额，单位为分，只能为整数
            /// 示例值：90
            /// </summary>
            public int payer_total { get; set; }

            /// <summary>
            /// 用户退款金额
            /// 退款给用户的金额，不包含所有优惠券金额
            /// 示例值：90
            /// </summary>
            public int payer_refund { get; set; }
        }
    }
}
