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
  
    文件名：CreateParkingRequestData.cs
    文件功能描述：创建停车入场接口请求数据
    
    
    创建标识：Senparc - 20210925

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.VehicleParking
{
    /// <summary>
    /// 创建停车入场接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_2.shtml </para>
    /// </summary>
    public class CreateParkingRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="out_parking_no">商户入场id <para>body商户侧入场标识id，在同一个商户号下唯一</para><para>示例值：1231243</para></param>
        /// <param name="plate_number">车牌号 <para>body车牌号，仅包括省份+车牌，不包括特殊字符。</para><para>示例值：粤B888888</para></param>
        /// <param name="plate_color">车牌颜色 <para>body车牌颜色，枚举值BLUE：蓝色GREEN：绿色YELLOW：黄色BLACK：黑色WHITE：白色LIMEGREEN：黄绿色</para><para>示例值：BLUE</para></param>
        /// <param name="notify_url">回调通知url <para>body接受入场状态变更回调通知的url，注意回调url只接受https</para><para>示例值：https://yoursite.com/wxpay.html</para></param>
        /// <param name="start_time">入场时间 <para>body入场时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2017-08-26T10:43:39+08:00</para></param>
        /// <param name="parking_name">停车场名称 <para>body所在停车位车场的名称</para><para>示例值：欢乐海岸停车场</para></param>
        /// <param name="free_duration">免费时长 <para>body停车场的免费停车时长，单位为秒</para><para>示例值：3600</para></param>
        public CreateParkingRequestData(string out_parking_no, string plate_number, string plate_color, string notify_url, TenpayDateTime start_time, string parking_name, int free_duration)
        {
            this.out_parking_no = out_parking_no;
            this.plate_number = plate_number;
            this.plate_color = plate_color;
            this.notify_url = notify_url;
            this.start_time = start_time.ToString();
            this.parking_name = parking_name;
            this.free_duration = free_duration;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CreateParkingRequestData()
        {
        }

        /// <summary>
        /// 商户入场id
        /// <para>body商户侧入场标识id，在同一个商户号下唯一 </para>
        /// <para>示例值：1231243</para>
        /// </summary>
        public string out_parking_no { get; set; }

        /// <summary>
        /// 车牌号
        /// <para>body车牌号，仅包括省份+车牌，不包括特殊字符。 </para>
        /// <para>示例值：粤B888888</para>
        /// </summary>
        public string plate_number { get; set; }

        /// <summary>
        /// 车牌颜色
        /// <para>body车牌颜色，枚举值 BLUE：蓝色 GREEN：绿色 YELLOW：黄色 BLACK：黑色 WHITE：白色 LIMEGREEN：黄绿色 </para>
        /// <para>示例值：BLUE</para>
        /// </summary>
        public string plate_color { get; set; }

        /// <summary>
        /// 回调通知url
        /// <para>body接受入场状态变更回调通知的url，注意回调url只接受https </para>
        /// <para>示例值：https://yoursite.com/wxpay.html</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 入场时间
        /// <para>body入场时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
        /// <para>示例值：2017-08-26T10:43:39+08:00</para>
        /// </summary>
        public string start_time { get; set; }

        /// <summary>
        /// 停车场名称
        /// <para>body所在停车位车场的名称 </para>
        /// <para>示例值：欢乐海岸停车场</para>
        /// </summary>
        public string parking_name { get; set; }

        /// <summary>
        /// 免费时长
        /// <para>body停车场的免费停车时长，单位为秒 </para>
        /// <para>示例值：3600</para>
        /// </summary>
        public int free_duration { get; set; }

    }




}
