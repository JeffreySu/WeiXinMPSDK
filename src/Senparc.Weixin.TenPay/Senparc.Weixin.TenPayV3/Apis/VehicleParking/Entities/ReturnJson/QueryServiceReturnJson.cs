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
  
    文件名：FindServiceReturnJson.cs
    文件功能描述：查询车牌服务开通信息返回Json类
    
    
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
    /// 查询车牌服务开通信息返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_1.shtml </para>
    /// </summary>
    public class QueryServiceReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="plate_number">车牌号 <para>车牌号，仅包括省份+车牌，不包括特殊字符。</para><para>示例值：粤B888888</para></param>
        /// <param name="plate_color">车牌颜色 <para>车牌颜色，枚举值：BLUE：蓝色GREEN：绿色YELLOW：黄色BLACK：黑色WHITE：白色LIMEGREEN：黄绿色</para><para>示例值：BLUE</para></param>
        /// <param name="service_open_time">车牌服务开通时间 <para>车牌服务开通时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2017-08-26T10:43:39+08:00</para><para>可为null</para></param>
        /// <param name="openid">用户标识 <para>用户在商户对应appid下的唯一标识，此处返回商户请求中的openid</para><para>示例值：oUpF8uMuAJOM2pxb1Q</para></param>
        /// <param name="service_state">车牌服务开通状态 <para>车牌服务开通状态，NORMAL：正常服务PAUSE：暂停服务，OUT_SERVICE：未开通，商户根据状态带用户跳转至对应的微信支付分停车服务小程序页面。其中NORMAL和PAUSE状态，可跳转至车牌管理页，进行车牌服务状态管理。OUT_SERVICE状态，可跳转至服务开通页面。</para><para>示例值：PAUSE</para></param>
        public QueryServiceReturnJson(string plate_number, string plate_color, string service_open_time, string openid, string service_state)
        {
            this.plate_number = plate_number;
            this.plate_color = plate_color;
            this.service_open_time = service_open_time;
            this.openid = openid;
            this.service_state = service_state;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryServiceReturnJson()
        {
        }

        /// <summary>
        /// 车牌号
        /// <para>车牌号，仅包括省份+车牌，不包括特殊字符。 </para>
        /// <para>示例值：粤B888888</para>
        /// </summary>
        public string plate_number { get; set; }

        /// <summary>
        /// 车牌颜色
        /// <para>车牌颜色，枚举值： BLUE：蓝色  GREEN：绿色 YELLOW：黄色  BLACK：黑色  WHITE：白色 LIMEGREEN：黄绿色 </para>
        /// <para>示例值：BLUE</para>
        /// </summary>
        public string plate_color { get; set; }

        /// <summary>
        /// 车牌服务开通时间
        /// <para>车牌服务开通时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
        /// <para>示例值：2017-08-26T10:43:39+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string service_open_time { get; set; }

        /// <summary>
        /// 用户标识
        /// <para>用户在商户对应appid下的唯一标识，此处返回商户请求中的openid </para>
        /// <para>示例值：oUpF8uMuAJOM2pxb1Q</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 车牌服务开通状态
        /// <para>车牌服务开通状态， NORMAL：正常服务  PAUSE：暂停服务， OUT_SERVICE：未开通，商户根据状态带用户跳转至对应的微信支付分停车服务小程序页面。  其中NORMAL 和 PAUSE状态，可跳转至车牌管理页，进行车牌服务状态管理。OUT_SERVICE状态，可跳转至服务开通页面。 </para>
        /// <para>示例值：PAUSE</para>
        /// </summary>
        public string service_state { get; set; }

    }
}
