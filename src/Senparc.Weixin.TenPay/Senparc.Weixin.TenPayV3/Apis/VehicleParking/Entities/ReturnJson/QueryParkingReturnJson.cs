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
  
    文件名：QueryParkingReturnJson.cs
    文件功能描述：查询订单返回Json类
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.VehicleParking
{
    /// <summary>
    /// 查询订单返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_4.shtml </para>
    /// </summary>
    public class QueryParkingReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="appid">应用ID <para>appid是商户在微信申请公众号或移动应用成功后分配的账号ID，登录平台为mp.weixin.qq.com或open.weixin.qq.com</para><para>示例值：wxcbda96de0b165486</para></param>
        /// <param name="sp_mchid">商户号 <para>微信支付分配的商户号</para><para>示例值：1230000109</para></param>
        /// <param name="description">服务描述 <para>商户自定义字段，用于交易账单中对扣费服务的描述。</para><para>示例值：停车场扣费</para></param>
        /// <param name="create_time">订单创建时间 <para>订单成功创建时返回，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2017-08-26T10:43:39+08:00</para></param>
        /// <param name="out_trade_no">商户订单号 <para>商户系统内部订单号，只能是数字、大小写字母，且在同一个商户号下唯一</para><para>示例值：20150806125346</para></param>
        /// <param name="transaction_id">微信支付订单号 <para>微信支付订单号</para><para>示例值：1009660380201506130728806387</para><para>可为null</para></param>
        /// <param name="trade_state">交易状态 <para>枚举值：SUCCESS：支付成功ACCEPTED：已接收，等待扣款PAY_FAIL：支付失败(其他原因，如银行返回失败)REFUND：转入退款</para><para>示例值：SUCCESS</para></param>
        /// <param name="trade_state_description">交易状态描述 <para>对当前订单状态的描述和下一步操作的指引</para><para>示例值：支付失败，请重新下单支付</para><para>可为null</para></param>
        /// <param name="success_time">支付完成时间 <para>订单支付完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2017-08-26T10:43:39+08:00</para><para>可为null</para></param>
        /// <param name="bank_type">付款银行 <para>银行类型，采用字符串类型的银行标识。BPA：该笔订单由微信进行垫付</para><para>示例值：CMC</para><para>可为null</para></param>
        /// <param name="user_repaid">用户是否已还款 <para>枚举值：Y：用户已还款N：用户未还款注意：使用此字段前需先确认bank_type字段值为BPA以及trade_state字段值为SUCCESS。</para><para>示例值：Y</para><para>可为null</para></param>
        /// <param name="attach">附加数据 <para>附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用</para><para>示例值：深圳分店</para><para>可为null</para></param>
        /// <param name="trade_scene">交易场景 <para>交易场景值，目前支持PARKING：车场停车场景</para><para>示例值：PARKING</para></param>
        /// <param name="parking_info">停车场景信息 <para>返回信息中的trade_scene为PARKING，返回该场景信息</para><para>可为null</para></param>
        /// <param name="payer">支付者信息 <para>支付者信息</para></param>
        /// <param name="amount">订单金额信息 <para>订单金额信息</para></param>
        /// <param name="promotion_detail">优惠信息 <para>优惠信息</para><para>可为null</para></param>
        public QueryParkingReturnJson(string appid, string sp_mchid, string description, string create_time, string out_trade_no, string transaction_id, string trade_state, string trade_state_description, string success_time, string bank_type, string user_repaid, string attach, string trade_scene, Parking_Info parking_info, Payer payer, Amount amount, Promotion_Detail[] promotion_detail)
        {
            this.appid = appid;
            this.sp_mchid = sp_mchid;
            this.description = description;
            this.create_time = create_time;
            this.out_trade_no = out_trade_no;
            this.transaction_id = transaction_id;
            this.trade_state = trade_state;
            this.trade_state_description = trade_state_description;
            this.success_time = success_time;
            this.bank_type = bank_type;
            this.user_repaid = user_repaid;
            this.attach = attach;
            this.trade_scene = trade_scene;
            this.parking_info = parking_info;
            this.payer = payer;
            this.amount = amount;
            this.promotion_detail = promotion_detail;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryParkingReturnJson()
        {
        }

        /// <summary>
        /// 应用ID
        /// <para>appid是商户在微信申请公众号或移动应用成功后分配的账号ID，登录平台为mp.weixin.qq.com或open.weixin.qq.com </para>
        /// <para>示例值：wxcbda96de0b165486</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// <para>微信支付分配的商户号 </para>
        /// <para>示例值：1230000109</para>
        /// </summary>
        public string sp_mchid { get; set; }

        /// <summary>
        /// 服务描述
        /// <para>商户自定义字段，用于交易账单中对扣费服务的描述。 </para>
        /// <para>示例值：停车场扣费</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 订单创建时间
        /// <para>订单成功创建时返回，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
        /// <para>示例值：2017-08-26T10:43:39+08:00</para>
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 商户订单号
        /// <para>商户系统内部订单号，只能是数字、大小写字母，且在同一个商户号下唯一 </para>
        /// <para>示例值：20150806125346</para>
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// <para>微信支付订单号 </para>
        /// <para>示例值：1009660380201506130728806387</para>
        /// <para>可为null</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 交易状态
        /// <para>枚举值： SUCCESS：支付成功 ACCEPTED：已接收，等待扣款 PAY_FAIL：支付失败(其他原因，如银行返回失败) REFUND：转入退款 </para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string trade_state { get; set; }

        /// <summary>
        /// 交易状态描述
        /// <para>对当前订单状态的描述和下一步操作的指引 </para>
        /// <para>示例值：支付失败，请重新下单支付</para>
        /// <para>可为null</para>
        /// </summary>
        public string trade_state_description { get; set; }

        /// <summary>
        /// 支付完成时间
        /// <para>订单支付完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
        /// <para>示例值：2017-08-26T10:43:39+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string success_time { get; set; }

        /// <summary>
        /// 付款银行
        /// <para>银行类型，采用字符串类型的银行标识。 BPA：该笔订单由微信进行垫付 </para>
        /// <para>示例值：CMC</para>
        /// <para>可为null</para>
        /// </summary>
        public string bank_type { get; set; }

        /// <summary>
        /// 用户是否已还款
        /// <para> 枚举值： Y：用户已还款  N：用户未还款  注意：使用此字段前需先确认bank_type字段值为BPA以及 trade_state字段值为SUCCESS。 </para>
        /// <para>示例值：Y</para>
        /// <para>可为null</para>
        /// </summary>
        public string user_repaid { get; set; }

        /// <summary>
        /// 附加数据
        /// <para>附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用 </para>
        /// <para>示例值：深圳分店</para>
        /// <para>可为null</para>
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 交易场景
        /// <para>交易场景值，目前支持 PARKING：车场停车场景 </para>
        /// <para>示例值：PARKING</para>
        /// </summary>
        public string trade_scene { get; set; }

        /// <summary>
        /// 停车场景信息
        /// <para>返回信息中的trade_scene为PARKING，返回该场景信息 </para>
        /// <para>可为null</para>
        /// </summary>
        public Parking_Info parking_info { get; set; }

        /// <summary>
        /// 支付者信息
        /// <para>支付者信息</para>
        /// </summary>
        public Payer payer { get; set; }

        /// <summary>
        /// 订单金额信息
        /// <para>订单金额信息</para>
        /// </summary>
        public Amount amount { get; set; }

        /// <summary>
        /// 优惠信息
        /// <para>优惠信息</para>
        /// <para>可为null</para>
        /// </summary>
        public Promotion_Detail[] promotion_detail { get; set; }

        #region 子数据类型
        public class Parking_Info
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="parking_id">停车入场id <para>微信支付分停车服务为商户分配的入场id，商户通过入场通知接口获取入场id</para><para>示例值：5K8264ILTKCH16CQ250</para></param>
            /// <param name="plate_number">车牌号 <para>车牌号，仅包括省份+车牌，不包括特殊字符。</para><para>示例值：粤B888888</para></param>
            /// <param name="plate_color">车牌颜色 <para>车牌颜色，枚举值：BLUE：蓝色GREEN：绿色YELLOW：黄色BLACK：黑色WHITE：白色LIMEGREEN：黄绿色</para><para>示例值：BLUE</para></param>
            /// <param name="start_time">入场时间 <para>用户入场时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2017-08-26T10:43:39+08:00</para></param>
            /// <param name="end_time">出场时间 <para>用户出场时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2017-08-26T10:43:39+08:00</para></param>
            /// <param name="parking_name">停车场名称 <para>所在停车位车场的名称</para><para>示例值：欢乐海岸停车场</para></param>
            /// <param name="charging_duration">计费时长 <para>计费的时间长，单位为秒</para><para>示例值：3600</para></param>
            /// <param name="device_id">停车场设备id <para>停车场设备id</para><para>示例值：12313</para></param>
            public Parking_Info(string parking_id, string plate_number, string plate_color, string start_time, string end_time, string parking_name, int charging_duration, string device_id)
            {
                this.parking_id = parking_id;
                this.plate_number = plate_number;
                this.plate_color = plate_color;
                this.start_time = start_time;
                this.end_time = end_time;
                this.parking_name = parking_name;
                this.charging_duration = charging_duration;
                this.device_id = device_id;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Parking_Info()
            {
            }

            /// <summary>
            /// 停车入场id
            /// <para>微信支付分停车服务为商户分配的入场id，商户通过入场通知接口获取入场id </para>
            /// <para>示例值：5K8264ILTKCH16CQ250</para>
            /// </summary>
            public string parking_id { get; set; }

            /// <summary>
            /// 车牌号
            /// <para>车牌号，仅包括省份+车牌，不包括特殊字符。 </para>
            /// <para>示例值：粤B888888</para>
            /// </summary>
            public string plate_number { get; set; }

            /// <summary>
            /// 车牌颜色
            /// <para>车牌颜色，枚举值： BLUE：蓝色 GREEN：绿色 YELLOW：黄色 BLACK：黑色 WHITE：白色 LIMEGREEN：黄绿色 </para>
            /// <para>示例值：BLUE</para>
            /// </summary>
            public string plate_color { get; set; }

            /// <summary>
            /// 入场时间
            /// <para>用户入场时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
            /// <para>示例值：2017-08-26T10:43:39+08:00</para>
            /// </summary>
            public string start_time { get; set; }

            /// <summary>
            /// 出场时间
            /// <para>用户出场时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
            /// <para>示例值：2017-08-26T10:43:39+08:00</para>
            /// </summary>
            public string end_time { get; set; }

            /// <summary>
            /// 停车场名称
            /// <para>所在停车位车场的名称 </para>
            /// <para>示例值：欢乐海岸停车场</para>
            /// </summary>
            public string parking_name { get; set; }

            /// <summary>
            /// 计费时长
            /// <para>计费的时间长，单位为秒 </para>
            /// <para>示例值：3600</para>
            /// </summary>
            public int charging_duration { get; set; }

            /// <summary>
            /// 停车场设备id
            /// <para>停车场设备id </para>
            /// <para>示例值：12313</para>
            /// </summary>
            public string device_id { get; set; }

        }

        public class Payer
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="openid">用户在appid下的标识 <para>用户在appid下的唯一标识</para><para>示例值：oUpF8uMuAJOM2pxb1Q</para></param>
            /// <param name="sub_openid">用户在sub_appid下的标识 <para>用户在sub_appid下的标识，商户扣费时传入了sub_appid，则会返回该用户在sub_appid下的标识</para><para>注意：仅适用于服务商模式</para><para>示例值：oUpF8uMuAJOM2pxb1Q</para><para>可为null</para></param>
            public Payer(string openid, string sub_openid)
            {
                this.openid = openid;
                this.sub_openid = sub_openid;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Payer()
            {
            }

            /// <summary>
            /// 用户在appid下的标识
            /// <para>用户在appid下的唯一标识 </para>
            /// <para>示例值：oUpF8uMuAJOM2pxb1Q</para>
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 用户在sub_appid下的标识
            /// <para>用户在sub_appid下的标识，商户扣费时传入了sub_appid，则会返回该用户在sub_appid下的标识 </para>
            /// <para>注意：仅适用于服务商模式</para>
            /// <para>示例值：oUpF8uMuAJOM2pxb1Q</para>
            /// <para>可为null</para>
            /// </summary>
            public string sub_openid { get; set; }

        }

        public class Amount
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="total">订单金额 <para>订单总金额，单位为分，只能为整数</para><para>示例值：888</para></param>
            /// <param name="currency">货币类型 <para>符合ISO4217标准的三位字母代码，目前只支持人民币：CNY</para><para>示例值：CNY</para></param>
            /// <param name="payer_total">用户实际支付金额 <para>用户实际支付金额，单位为分，只能为整数</para><para>示例值：100</para><para>可为null</para></param>
            /// <param name="discount_total">折扣 <para>订单折扣</para><para>示例值：100</para><para>可为null</para></param>
            public Amount(int total, string currency, int payer_total, int discount_total)
            {
                this.total = total;
                this.currency = currency;
                this.payer_total = payer_total;
                this.discount_total = discount_total;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Amount()
            {
            }

            /// <summary>
            /// 订单金额
            /// <para>订单总金额，单位为分，只能为整数</para>
            /// <para>示例值：888</para>
            /// </summary>
            public int total { get; set; }

            /// <summary>
            /// 货币类型
            /// <para>符合ISO 4217标准的三位字母代码，目前只支持人民币：CNY </para>
            /// <para>示例值：CNY</para>
            /// </summary>
            public string currency { get; set; }

            /// <summary>
            /// 用户实际支付金额
            /// <para>用户实际支付金额，单位为分，只能为整数 </para>
            /// <para>示例值：100</para>
            /// <para>可为null</para>
            /// </summary>
            public int payer_total { get; set; }

            /// <summary>
            /// 折扣
            /// <para>订单折扣 </para>
            /// <para>示例值：100</para>
            /// <para>可为null</para>
            /// </summary>
            public int discount_total { get; set; }

        }

        public class Promotion_Detail
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="coupon_id">券ID <para>券或者立减优惠id</para><para>示例值：109519</para></param>
            /// <param name="name">优惠名称 <para>优惠名称</para><para>示例值：单品惠-6</para><para>可为null</para></param>
            /// <param name="scope">优惠范围 <para>枚举值：GLOBAL：全场代金券SINGLE：单品优惠</para><para>示例值：SINGLE</para><para>可为null</para></param>
            /// <param name="type">优惠类型 <para>枚举值：CASH：充值NOCASH：预充值</para><para>示例值：CASH</para><para>可为null</para></param>
            /// <param name="stock_id">活动ID <para>在微信商户后台配置的批次ID</para><para>示例值：931386</para><para>可为null</para></param>
            /// <param name="amount">优惠券面额 <para>用户享受优惠的金额</para><para>示例值：5</para></param>
            /// <param name="wechatpay_contribute">微信出资 <para>特指由微信支付商户平台创建的优惠，出资金额等于本项优惠总金额，单位为分</para><para>示例值：1</para><para>可为null</para></param>
            /// <param name="merchant_contribute">商户出资 <para>特指商户自己创建的优惠，出资金额等于本项优惠总金额，单位为分</para><para>示例值：1</para><para>可为null</para></param>
            /// <param name="other_contribute">其他出资 <para>其他出资方出资金额，单位为分</para><para>示例值：1</para><para>可为null</para></param>
            /// <param name="currency">优惠币种 <para>CNY：人民币，境内商户号仅支持人民币。</para><para>示例值：CNY</para><para>可为null</para></param>
            public Promotion_Detail(string coupon_id, string name, string scope, string type, string stock_id, int amount, int wechatpay_contribute, int merchant_contribute, int other_contribute, string currency)
            {
                this.coupon_id = coupon_id;
                this.name = name;
                this.scope = scope;
                this.type = type;
                this.stock_id = stock_id;
                this.amount = amount;
                this.wechatpay_contribute = wechatpay_contribute;
                this.merchant_contribute = merchant_contribute;
                this.other_contribute = other_contribute;
                this.currency = currency;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Promotion_Detail()
            {
            }

            /// <summary>
            /// 券ID
            /// <para>券或者立减优惠id </para>
            /// <para>示例值：109519</para>
            /// </summary>
            public string coupon_id { get; set; }

            /// <summary>
            /// 优惠名称
            /// <para>优惠名称 </para>
            /// <para>示例值：单品惠-6</para>
            /// <para>可为null</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 优惠范围
            /// <para>枚举值： GLOBAL：全场代金券 SINGLE：单品优惠 </para>
            /// <para>示例值：SINGLE</para>
            /// <para>可为null</para>
            /// </summary>
            public string scope { get; set; }

            /// <summary>
            /// 优惠类型
            /// <para>枚举值： CASH：充值 NOCASH：预充值 </para>
            /// <para>示例值：CASH</para>
            /// <para>可为null</para>
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 活动ID
            /// <para>在微信商户后台配置的批次ID </para>
            /// <para>示例值：931386</para>
            /// <para>可为null</para>
            /// </summary>
            public string stock_id { get; set; }

            /// <summary>
            /// 优惠券面额
            /// <para>用户享受优惠的金额 </para>
            /// <para>示例值：5</para>
            /// </summary>
            public int amount { get; set; }

            /// <summary>
            /// 微信出资
            /// <para>特指由微信支付商户平台创建的优惠，出资金额等于本项优惠总金额，单位为分 </para>
            /// <para>示例值：1</para>
            /// <para>可为null</para>
            /// </summary>
            public int wechatpay_contribute { get; set; }

            /// <summary>
            /// 商户出资
            /// <para>特指商户自己创建的优惠，出资金额等于本项优惠总金额，单位为分 </para>
            /// <para>示例值：1</para>
            /// <para>可为null</para>
            /// </summary>
            public int merchant_contribute { get; set; }

            /// <summary>
            /// 其他出资
            /// <para>其他出资方出资金额，单位为分 </para>
            /// <para>示例值：1</para>
            /// <para>可为null</para>
            /// </summary>
            public int other_contribute { get; set; }

            /// <summary>
            /// 优惠币种
            /// <para>CNY：人民币，境内商户号仅支持人民币。 </para>
            /// <para>示例值：CNY</para>
            /// <para>可为null</para>
            /// </summary>
            public string currency { get; set; }

        }


        #endregion
    }


}
