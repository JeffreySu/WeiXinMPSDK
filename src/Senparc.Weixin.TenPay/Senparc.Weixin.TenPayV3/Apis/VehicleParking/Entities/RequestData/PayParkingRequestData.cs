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
  
    文件名：PayParkingRequestData.cs
    文件功能描述：扣费受理接口请求数据
    
    
    创建标识：Senparc - 20210925

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.VehicleParking
{
    /// <summary>
    /// 扣费受理接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_3.shtml </para>
    /// </summary>
    public class PayParkingRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="appid">应用ID <para>bodyappid是商户在微信申请公众号或移动应用成功后分配的账号ID，登录平台为mp.weixin.qq.com或open.weixin.qq.com</para><para>示例值：wxcbda96de0b165486</para></param>
        /// <param name="description">服务描述 <para>body商户自定义字段，用于交易账单中对扣费服务的描述。</para><para>示例值：停车场扣费</para></param>
        /// <param name="attach">附加数据 <para>body附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用</para><para>示例值：深圳分店</para><para>可为null</para></param>
        /// <param name="out_trade_no">商户订单号 <para>body商户系统内部订单号，只能是数字、大小写字母，且在同一个商户号下唯一</para><para>示例值：20150806125346</para></param>
        /// <param name="trade_scene">交易场景 <para>body交易场景值，目前支持PARKING：车场停车场景</para><para>示例值：PARKING</para></param>
        /// <param name="goods_tag">订单优惠标记 <para>body代金券或立减优惠功能的参数，说明详见代金券或立减优惠</para><para>示例值：WXG</para><para>可为null</para></param>
        /// <param name="notify_url">回调通知url <para>body接受扣款结果异步回调通知的url，注意回调url只接受https</para><para>示例值：https://yoursite.com/wxpay.html</para></param>
        /// <param name="profit_sharing">分账标识 <para>body枚举值：Y：是，需要分账N：否，不分账字母要求大写，不传默认不分账，分账详细说明见直连分账API、服务商分账API文档</para><para>示例值：Y</para><para>可为null</para></param>
        /// <param name="amount">订单金额 <para>body订单金额信息</para></param>
        /// <param name="parking_info">停车场景信息 <para>body当交易场景为PARKING时，需要在该字段添加停车场景信息</para><para>可为null</para></param>
        public PayParkingRequestData(string appid, string description, string attach, string out_trade_no, string trade_scene, string goods_tag, string notify_url, string profit_sharing, Amount amount, Parking_Info parking_info)
        {
            this.appid = appid;
            this.description = description;
            this.attach = attach;
            this.out_trade_no = out_trade_no;
            this.trade_scene = trade_scene;
            this.goods_tag = goods_tag;
            this.notify_url = notify_url;
            this.profit_sharing = profit_sharing;
            this.amount = amount;
            this.parking_info = parking_info;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PayParkingRequestData()
        {
        }

        /// <summary>
        /// 应用ID
        /// <para>bodyappid是商户在微信申请公众号或移动应用成功后分配的账号ID，登录平台为mp.weixin.qq.com或open.weixin.qq.com </para>
        /// <para>示例值：wxcbda96de0b165486</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 服务描述
        /// <para>body商户自定义字段，用于交易账单中对扣费服务的描述。 </para>
        /// <para>示例值：停车场扣费</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 附加数据
        /// <para>body附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用 </para>
        /// <para>示例值：深圳分店</para>
        /// <para>可为null</para>
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 商户订单号
        /// <para>body商户系统内部订单号，只能是数字、大小写字母，且在同一个商户号下唯一 </para>
        /// <para>示例值：20150806125346</para>
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 交易场景
        /// <para>body交易场景值，目前支持 PARKING：车场停车场景 </para>
        /// <para>示例值：PARKING</para>
        /// </summary>
        public string trade_scene { get; set; }

        /// <summary>
        /// 订单优惠标记
        /// <para>body代金券或立减优惠功能的参数，说明详见 代金券或立减优惠 </para>
        /// <para>示例值：WXG</para>
        /// <para>可为null</para>
        /// </summary>
        public string goods_tag { get; set; }

        /// <summary>
        /// 回调通知url
        /// <para>body接受扣款结果异步回调通知的url，注意回调url只接受https </para>
        /// <para>示例值：https://yoursite.com/wxpay.html</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 分账标识
        /// <para>body枚举值： Y：是，需要分账  N：否，不分账  字母要求大写，不传默认不分账，分账详细说明见直连分账API、服务商分账API文档 </para>
        /// <para>示例值：Y</para>
        /// <para>可为null</para>
        /// </summary>
        public string profit_sharing { get; set; }

        /// <summary>
        /// 订单金额
        /// <para>body订单金额信息</para>
        /// </summary>
        public Amount amount { get; set; }

        /// <summary>
        /// 停车场景信息
        /// <para>body当交易场景为PARKING时，需要在该字段添加停车场景信息 </para>
        /// <para>可为null</para>
        /// </summary>
        public Parking_Info parking_info { get; set; }

        #region 子数据类型
        public class Amount
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="total">订单金额 <para>订单总金额，单位为分，只能为整数</para><para>示例值：888</para></param>
            /// <param name="currency">货币类型 <para>符合ISO4217标准的三位字母代码，目前只支持人民币：CNY</para><para>示例值：CNY</para></param>
            public Amount(int total, string currency)
            {
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
            /// 订单金额
            /// <para>订单总金额，单位为分，只能为整数 </para>
            /// <para>示例值：888</para>
            /// </summary>
            public int total { get; set; }

            /// <summary>
            /// 货币类型
            /// <para>符合ISO 4217标准的三位字母代码，目前只支持人民币：CNY </para>
            /// <para>示例值：CNY</para>
            /// </summary>
            public string currency { get; set; }

        }

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
            /// 由CreateParkingReturnJson类型生成结算停车数据 并且自动计算计费时长
            /// </summary>
            /// <param name="createParkingReturnJson"></param>
            public Parking_Info(CreateParkingReturnJson createParkingReturnJson)
            {
                this.parking_id = createParkingReturnJson.id;
                this.plate_number = createParkingReturnJson.plate_number;
                this.plate_color = createParkingReturnJson.plate_color;
                this.start_time = createParkingReturnJson.start_time;
                this.parking_name = createParkingReturnJson.parking_name;

                // 记录出场时间并计算收费时长
                this.end_time = DateTime.Now.ToString();
                this.charging_duration = (new TimeSpan(TenPayDateTimeHelper.PraseDateTimeFromString(end_time).Ticks) - new TimeSpan(TenPayDateTimeHelper.PraseDateTimeFromString(start_time).Ticks)).Seconds - createParkingReturnJson.free_duration;
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


        #endregion
    }
}
