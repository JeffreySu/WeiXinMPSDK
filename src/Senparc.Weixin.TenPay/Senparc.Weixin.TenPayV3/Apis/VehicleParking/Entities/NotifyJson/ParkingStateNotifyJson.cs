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
  
    文件名：ParkingStateNotifyJson.cs
    文件功能描述：停车入场状态变更通知Json
    
    
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
    /// 停车入场状态变更通知Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_5.shtml </para>
    /// </summary>
    public class ParkingStateNotifyJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="sp_mchid">商户号  <para>调用接口提交的商户号</para><para>示例值：1230000109</para></param>
        /// <param name="parking_id">停车入场id  <para>车主服务为商户分配的入场id</para><para>示例值：5K8264ILTKCH16CQ250</para></param>
        /// <param name="out_parking_no">商户入场id  <para>商户侧入场标识id，在同一个商户号下唯一</para><para>示例值：1213134</para></param>
        /// <param name="plate_number">车牌号  <para>车牌号，仅包括省份+车牌，不包括特殊字符。</para><para>示例值：粤B888888</para></param>
        /// <param name="plate_color">车牌颜色  <para>车牌颜色，枚举值：BLUE：蓝色GREEN：绿色YELLOW：黄色BLACK：黑色WHITE：白色LIMEGREEN：黄绿色</para><para>示例值：BLUE</para></param>
        /// <param name="start_time">入场时间  <para>入场时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
        /// <param name="parking_name">停车场名称  <para>所在停车位车场的名称</para><para>示例值：欢乐海岸停车场</para></param>
        /// <param name="free_duration">免费时长  <para>停车场的免费停车时长</para><para>示例值：3600</para></param>
        /// <param name="parking_state">停车入场状态  <para>本次入场车牌的服务状态NORMAL：正常状态，可以使用车主服务BLOCKED：不可用状态，暂时不可以使用车主服务</para><para>示例值：NORMAL</para></param>
        /// <param name="blocked_state_description">不可用状态描述  <para>不可用服务状态描述，返回车牌状态为BLOCKED，会返回该字段，描述具体BLOCKED的原因，PAUSE：已暂停车主服务；OVERDUE：已授权签约但欠费，不能提供服务，商户提示用户进行还款，REMOVE：用户移除车牌导致车牌不可用。请跳转到授权/开通接口。</para><para>示例值：PAUSE</para><para>可为null</para></param>
        /// <param name="state_update_time">状态变更时间  <para>状态变更的发生时间(毫秒级),遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
        public ParkingStateNotifyJson(string sp_mchid, string parking_id, string out_parking_no, string plate_number, string plate_color, string start_time, string parking_name, int free_duration, string parking_state, string blocked_state_description, string state_update_time)
        {
            this.sp_mchid = sp_mchid;
            this.parking_id = parking_id;
            this.out_parking_no = out_parking_no;
            this.plate_number = plate_number;
            this.plate_color = plate_color;
            this.start_time = start_time;
            this.parking_name = parking_name;
            this.free_duration = free_duration;
            this.parking_state = parking_state;
            this.blocked_state_description = blocked_state_description;
            this.state_update_time = state_update_time;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ParkingStateNotifyJson()
        {
        }

        /// <summary>
        /// 商户号 
        /// <para>调用接口提交的商户号 </para>
        /// <para>示例值：1230000109 </para>
        /// </summary>
        public string sp_mchid { get; set; }

        /// <summary>
        /// 停车入场id 
        /// <para>车主服务为商户分配的入场id </para>
        /// <para>示例值：5K8264ILTKCH16CQ250 </para>
        /// </summary>
        public string parking_id { get; set; }

        /// <summary>
        /// 商户入场id 
        /// <para>商户侧入场标识id，在同一个商户号下唯一 </para>
        /// <para>示例值：1213134 </para>
        /// </summary>
        public string out_parking_no { get; set; }

        /// <summary>
        /// 车牌号 
        /// <para>车牌号，仅包括省份+车牌，不包括特殊字符。 </para>
        /// <para>示例值：粤B888888 </para>
        /// </summary>
        public string plate_number { get; set; }

        /// <summary>
        /// 车牌颜色 
        /// <para>车牌颜色，枚举值： BLUE：蓝色 GREEN：绿色 YELLOW：黄色 BLACK：黑色 WHITE：白色 LIMEGREEN：黄绿色 </para>
        /// <para>示例值：BLUE </para>
        /// </summary>
        public string plate_color { get; set; }

        /// <summary>
        /// 入场时间 
        /// <para>入场时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00 </para>
        /// </summary>
        public string start_time { get; set; }

        /// <summary>
        /// 停车场名称 
        /// <para>所在停车位车场的名称 </para>
        /// <para>示例值：欢乐海岸停车场 </para>
        /// </summary>
        public string parking_name { get; set; }

        /// <summary>
        /// 免费时长 
        /// <para>停车场的免费停车时长 </para>
        /// <para>示例值：3600 </para>
        /// </summary>
        public int free_duration { get; set; }

        /// <summary>
        /// 停车入场状态 
        /// <para>本次入场车牌的服务状态 NORMAL：正常状态，可以使用车主服务 BLOCKED：不可用状态，暂时不可以使用车主服务 </para>
        /// <para>示例值：NORMAL </para>
        /// </summary>
        public string parking_state { get; set; }

        /// <summary>
        /// 不可用状态描述 
        /// <para>不可用服务状态描述，返回车牌状态为BLOCKED，会返回该字段，描述具体BLOCKED的原因， PAUSE：已暂停车主服务； OVERDUE：已授权签约但欠费，不能提供服务，商户提示用户进行还款， REMOVE：用户移除车牌导致车牌不可用。请跳转到授权/开通接口。 </para>
        /// <para>示例值：PAUSE </para>
        /// <para>可为null</para>
        /// </summary>
        public string blocked_state_description { get; set; }

        /// <summary>
        /// 状态变更时间 
        /// <para>状态变更的发生时间(毫秒级),遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。。 </para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00 </para>
        /// </summary>
        public string state_update_time { get; set; }

    }
}
