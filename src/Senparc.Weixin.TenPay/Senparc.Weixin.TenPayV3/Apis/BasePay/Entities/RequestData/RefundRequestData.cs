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
  
    文件名：RefundRequestData.cs
    文件功能描述：微信支付申请退款请求数据
    
    
    创建标识：Senparc - 20210814

    修改标识：Senparc - 20210819
    修改描述：完善注释; 增加构造函数
    
----------------------------------------------------------------*/
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
  
    文件名：RefundRequestData.cs
    文件功能描述：微信支付申请退款请求数据
    
    
    创建标识：Senparc - 20210814

    修改标识：Senparc - 20210819
    修改描述：完善注释; 增加构造函数

    修改标识：Senparc - 20220423
    修改描述：修改类名 RefundRequsetData 为 RefundRequestData
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class RefundRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public RefundRequestData() { }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="transaction_id">原支付交易对应的微信订单号,transaction_id与out_trade_no二选一传入</param>
        /// <param name="out_trade_no">微信支付订单号,transaction_id与out_trade_no二选一传入</param>
        /// <param name="out_refund_no">商户系统内部的退款单号，商户系统内部唯一</param>
        /// <param name="reason">若商户传入，会在下发给用户的退款消息中体现退款原因，可为null</param>
        /// <param name="notify_url">异步接收微信支付退款结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。 如果参数中传了notify_url，则商户平台上配置的回调地址将不会生效，优先回调当前传的这个地址，可为null</param>
        /// <param name="funds_account">退款资金来源 若传递此参数则使用对应的资金账户退款，否则默认使用未结算资金退款（仅对老资金流商户适用），可为null</param>
        /// <param name="amount">订单金额信息</param>
        /// <param name="goods_detail">退款商品，可为null</param>
        public RefundRequestData(string transaction_id, string out_trade_no, string out_refund_no, string reason, string notify_url, string funds_account, Amount amount, Goods_Detail[] goods_detail)
        {
            this.transaction_id = transaction_id;
            this.out_trade_no = out_trade_no;
            this.out_refund_no = out_refund_no;
            this.reason = reason;
            this.notify_url = notify_url;
            this.funds_account = funds_account;
            this.amount = amount;
            this.goods_detail = goods_detail;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">子商户的商户号，由微信支付生成并下发。</param>
        /// <param name="transaction_id">原支付交易对应的微信订单号,transaction_id与out_trade_no二选一传入</param>
        /// <param name="out_trade_no">微信支付订单号,transaction_id与out_trade_no二选一传入</param>
        /// <param name="out_refund_no">商户系统内部的退款单号，商户系统内部唯一</param>
        /// <param name="reason">若商户传入，会在下发给用户的退款消息中体现退款原因，可为null</param>
        /// <param name="notify_url">异步接收微信支付退款结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。 如果参数中传了notify_url，则商户平台上配置的回调地址将不会生效，优先回调当前传的这个地址，可为null</param>
        /// <param name="funds_account">退款资金来源 若传递此参数则使用对应的资金账户退款，否则默认使用未结算资金退款（仅对老资金流商户适用），可为null</param>
        /// <param name="amount">订单金额信息</param>
        /// <param name="goods_detail">退款商品，可为null</param>
        public RefundRequestData(string sub_mchid, string transaction_id, string out_trade_no, string out_refund_no, string reason, string notify_url, string funds_account, Amount amount, Goods_Detail[] goods_detail)
        {
            this.sub_mchid = sub_mchid;
            this.transaction_id = transaction_id;
            this.out_trade_no = out_trade_no;
            this.out_refund_no = out_refund_no;
            this.reason = reason;
            this.notify_url = notify_url;
            this.funds_account = funds_account;
            this.amount = amount;
            this.goods_detail = goods_detail;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式需要
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 微信支付订单号
        /// 原支付交易对应的微信订单号
        /// 示例值：1217752501201407033233368018
        /// </summary>
        public string transaction_id { get; set; }


        /// <summary>
        /// 商户订单号
        /// 原支付交易对应的商户订单号
        /// 示例值：1217752501201407033233368018
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商户退款单号
        /// 商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
        /// 示例值：1217752501201407033233368018
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 退款原因
        /// 若商户传入，会在下发给用户的退款消息中体现退款原因
        /// 示例值：商品已售完
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 退款结果回调url
        /// 异步接收微信支付退款结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。 如果参数中传了notify_url，则商户平台上配置的回调地址将不会生效，优先回调当前传的这个地址。
        /// 示例值：https://weixin.qq.com
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 退款资金来源
        /// 若传递此参数则使用对应的资金账户退款，否则默认使用未结算资金退款（仅对老资金流商户适用）
        /// 枚举值：
        /// AVAILABLE：可用余额账户
        /// 示例值：AVAILABLE
        /// </summary>
        public string funds_account { get; set; }

        /// <summary>
        /// 订单金额信息
        /// </summary>
        public Amount amount { get; set; }

        /// <summary>
        /// 退款商品
        /// 指定商品退款需要传此参数，其他场景无需传递
        /// </summary>
        public Goods_Detail[] goods_detail { get; set; }

        #region 请求数据类型

        /// <summary>
        /// 订单金额信息
        /// </summary>
        public class Amount
        {
            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="refund">退款金额 币种的最小单位，只能为整数，不能超过原订单支付金额</param>
            /// <param name="from">退款出资账户及金额，可为null</param>
            /// <param name="total">订单总金额，单位为分</param>
            /// <param name="currency">货币类型 境内商户号仅支持人民币</param>
            public Amount(int refund, From[] from, int total, string currency)
            {
                this.refund = refund;
                this.from = from;
                this.total = total;
                this.currency = currency;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Amount()
            {
            }

            /// <summary>
            /// 退款金额，币种的最小单位，只能为整数，不能超过原订单支付金额。
            /// 示例值：888
            /// </summary>
            public int refund { get; set; }


            /// <summary>
            /// 退款金额，币种的最小单位，只能为整数，不能超过原订单支付金额。
            /// 示例值：888
            /// </summary>
            public From[] from { get; set; }

            /// <summary>
            /// 总金额
            /// 订单总金额，单位为分。
            /// 示例值：100 (1元)
            /// </summary>
            public int total { get; set; }

            /// <summary>
            /// 货币类型
            /// CNY：人民币，境内商户号仅支持人民币。
            /// 示例值：CNY
            /// </summary>
            public string currency { get; set; }

            #region 请求数据类型

            /// <summary>
            /// 退款出资账户及金额
            /// </summary>
            public class From
            {
                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="account">出资账户类型</param>
                /// <param name="amount">对应账户出资金额</param>
                public From(string account, string amount)
                {
                    this.account = account;
                    this.amount = amount;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public From()
                {
                }

                /// <summary>
                /// 出资账户类型
                /// 下面枚举值多选一。
                /// 枚举值：
                /// AVAILABLE : 可用余额
                /// UNAVAILABLE : 不可用余额
                /// 示例值：AVAILABLE
                /// </summary>
                public string account { get; set; }

                /// <summary>
                /// 出资金额
                /// 对应账户出资金额
                /// 示例值：444
                /// </summary>
                public string amount { get; set; }
            }

            #endregion
        }

        /// <summary>
        /// 单品列表信息
        /// </summary>
        public class Goods_Detail
        {
            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="merchant_goods_id">商户侧商品编码</param>
            /// <param name="wechatpay_goods_id">微信侧商品编码，可为null</param>
            /// <param name="goods_name">商品的实际名称，可为null</param>
            /// <param name="unit_price">商品单价，单位为分</param>
            /// <param name="refund_amount">商品退款金额，单位为分</param>
            /// <param name="refund_quantity">单品的退款数量</param>
            public Goods_Detail(string merchant_goods_id, string wechatpay_goods_id, string goods_name, int unit_price, int refund_amount, int refund_quantity)
            {
                this.merchant_goods_id = merchant_goods_id;
                this.wechatpay_goods_id = wechatpay_goods_id;
                this.goods_name = goods_name;
                this.unit_price = unit_price;
                this.refund_amount = refund_amount;
                this.refund_quantity = refund_quantity;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Goods_Detail()
            {
            }

            /// <summary>
            /// 商户侧商品编码
            /// 由半角的大小写字母、数字、中划线、下划线中的一种或几种组成
            /// 示例值：1217752501201407033233368018
            /// </summary>
            public string merchant_goods_id { get; set; }

            /// <summary>
            /// 微信侧商品编码
            /// 微信支付定义的统一商品编号（没有可不传）
            /// 示例值：1001
            /// </summary>
            public string wechatpay_goods_id { get; set; }

            /// <summary>
            /// 商品的实际名称
            /// 示例值：iPhone6s 16G
            /// </summary>
            public string goods_name { get; set; }

            /// <summary>
            /// 商品单价
            /// 商品单价，单位为分
            /// 示例值：828800 (8288元)
            /// </summary>
            public int unit_price { get; set; }

            /// <summary>
            /// 商品退款金额，单位为分
            /// 示例值：528800
            /// </summary>
            public int refund_amount { get; set; }

            /// <summary>
            /// 单品的退款数量
            /// 示例值：1
            /// </summary>
            public int refund_quantity { get; set; }
        }
        #endregion
    }
}
