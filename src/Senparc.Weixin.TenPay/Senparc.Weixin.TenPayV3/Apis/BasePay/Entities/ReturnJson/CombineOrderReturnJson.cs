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
  
    文件名：CombineOrderJson.cs
    文件功能描述：微信合单支付订单实体类
    
    
    创建标识：Senparc - 20210813
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class CombineOrderReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 合单发起方的appid。
        /// 示例值：wxd678efh567hg6787
        /// </summary>
        public string combine_appid { get; set; }

        /// <summary>
        /// 合单发起方商户号。
        /// 示例值：1900000109
        /// </summary>
        public string combine_mchid { get; set; }

        /// <summary>
        /// 合单支付总订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
        /// 示例值：P20150806125346
        /// </summary>
        public string combine_out_trade_no { get; set; }

        /// <summary>
        /// 支付场景信息描述
        /// </summary>
        public Scene_Info scene_info { get; set; }

        /// <summary>
        /// 子单信息数组
        /// 最多支持子单条数：10
        /// </summary>
        public Sub_Order[] sub_orders { get; set; }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public Combine_Payer_Info combine_payer_info { get; set; }

        /// <summary>
        /// 支付场景信息描述
        /// </summary>
        public class Scene_Info
        {
            /// <summary>
            /// 商户端设备号
            /// 商户端设备号（门店号或收银设备ID）。
            /// 示例值：013467007045764
            /// </summary>
            public string device_id { get; set; }
        }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public class Combine_Payer_Info
        {
            /// <summary>
            /// 用户标识	
            /// 用户在直连商户appid下的唯一标识。 下单前需获取到用户的Openid，Openid获取详见
            /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
            /// </summary>
            public string openid { get; set; }
        }

        /// <summary>
        /// 子单信息
        /// </summary>
        public class Sub_Order
        {
            /// <summary>
            /// 子单商户号
            /// 子单发起方商户号即合单参与方商户号，必须与发起方appid有绑定关系。
            /// 示例值：1900000109
            /// </summary>
            public string mchid { get; set; }

            /// <summary>
            /// 交易类型
            /// 枚举值：
            /// NATIVE：扫码支付
            /// JSAPI：公众号支付
            /// APP：APP支付
            /// MWEB：H5支付
            /// 示例值： JSAPI
            /// </summary>
            public string trade_type { get; set; }

            /// <summary>
            /// 交易状态
            /// 枚举值：
            /// SUCCESS：支付成功
            /// REFUND：转入退款
            /// NOTPAY：未支付
            /// CLOSED：已关闭
            /// USERPAYING：用户支付中
            /// PAYERROR：支付失败(其他原因，如银行返回失败)
            /// 示例值： SUCCESS
            /// </summary>
            public string trade_state { get; set; }

            /// <summary>
            /// 付款银行
            /// 银行类型，采用字符串类型的银行标识。银行标识请参考《银行类型对照表》
            /// 示例值：CMC
            /// </summary>
            public string bank_type { get; set; }

            /// <summary>
            /// 附加信息
            /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。
            /// 示例值：深圳分店
            /// </summary>
            public string attach { get; set; }

            /// <summary>
            /// 支付完成时间
            /// 支付完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE
            /// 示例值：2018-06-08T10:34:56+08:00
            /// </summary>
            public DateTime /*TenpayDateTime*/ success_time { get; set; }

            /// <summary>
            /// 微信支付订单号
            /// 微信支付系统生成的订单号。
            /// 示例值：1217752501201407033233368018
            /// </summary>
            public string transaction_id { get; set; }

            /// <summary>
            /// 子单商户订单号
            /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
            /// 特殊规则：最小字符长度为6
            /// 示例值：20150806125346
            /// </summary>
            public string out_trade_no { get; set; }

            /// <summary>
            /// 订单金额
            /// </summary>
            public Amount amount { get; set; }


        }

        public class Amount
        {
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
        }

        /// <summary>
        /// 优惠功能
        /// </summary>
        public class Promotion_Detail
        {
            /// <summary>
            /// 券ID
            /// 示例值：109519
            /// </summary>
            public string coupon_id { get; set; }

            /// <summary>
            /// 优惠名称
            /// 示例值：单品惠-6
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 优惠范围
            /// GLOBAL：全场代金券
            /// SINGLE：单品优惠
            /// 示例值：GLOBAL
            /// </summary>
            public string scope { get; set; }

            /// <summary>
            /// 优惠类型
            /// CASH- 代金券，需要走结算资金的预充值型代金券
            /// NOCASH- 优惠券，不走结算资金的免充值型优惠券
            /// 示例值：CASH
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 优惠券面额，单位为分
            /// 示例值：100
            /// </summary>
            public int amount { get; set; }

            /// <summary>
            /// 活动ID
            /// 示例值：931386
            /// </summary>
            public string stock_id { get; set; }

            /// <summary>
            /// 微信出资，单位为分
            /// 示例值：0
            /// </summary>
            public string wechatpay_contribute { get; set; }

            /// <summary>
            /// 商户出资，单位为分
            /// 示例值：0
            /// </summary>
            public string merchant_contribute { get; set; }

            /// <summary>
            /// 其他出资，单位为分
            /// 示例值：0
            /// </summary>
            public string other_contribute { get; set; }

            /// <summary>
            /// 优惠币种	
            /// CNY：人民币，境内商户号仅支持人民币。
            /// 示例值：CNY
            /// </summary>
            public string currency { get; set; }

            /// <summary>
            /// 单品列表信息
            /// </summary>
            public Goods_Detail[] goods_detail { get; set; }
        }

        /// <summary>
        /// 单品列表信息
        /// </summary>
        public class Goods_Detail
        {
            /// <summary>
            /// 商品编码
            /// 商品编码
            /// 示例值：M1006
            /// </summary>
            public string goods_id { get; set; }

            /// <summary>
            /// 商品数量
            /// 用户购买的数量
            /// 示例值：1
            /// </summary>
            public int quantity { get; set; }

            /// <summary>
            /// 商品单价
            /// 商品单价，单位为分
            /// 示例值：828800 (8288元)
            /// </summary>
            public int unit_price { get; set; }

            /// <summary>
            /// 商品优惠金额，单位为分
            /// 示例值：0  
            /// </summary>
            public int discount_amount { get; set; }

            /// <summary>
            /// 商品备注信息
            /// 示例值：商品备注信息
            /// </summary>
            public string goods_remark { get; set; }
        }
    }
}