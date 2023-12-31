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
  
    文件名：NotifyBusinessCirclePointsRequestData.cs
    文件功能描述：商圈积分同步接口请求数据
    
    
    创建标识：Senparc - 20210925

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BusinessCircle
{
    /// <summary>
    /// 商圈积分同步接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_6_2.shtml </para>
    /// </summary>
    public class NotifyBusinessCirclePointsRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="transaction_id">微信订单号 <para>body微信支付推送的商圈内交易通知里携带的微信订单号</para><para>示例值：1217752501201407033233368018</para></param>
        /// <param name="appid">小程序appid <para>body顾客授权积分时使用的小程序的appid</para><para>示例值：wx1234567890abcdef</para></param>
        /// <param name="openid">用户标识 <para>body顾客授权时使用的小程序上的openid</para><para>示例值：oWmnN4xxxxxxxxxxe92NHIGf1xd8</para></param>
        /// <param name="earn_points">是否获得积分 <para>body用于标明此单是否获得积分，true为获得积分，false为未获得</para><para>示例值：true</para></param>
        /// <param name="increased_points">订单新增积分值 <para>body顾客此笔交易新增的积分值</para><para>示例值：100</para></param>
        /// <param name="points_update_time">积分更新时间 <para>body为顾客此笔交易成功积分的时间</para><para>示例值：2020-05-20T13:29:35.120+08:00</para></param>
        /// <param name="no_points_remarks">未获得积分的备注信息 <para>body当未获得积分时，提供未获得积分的原因等备注信息</para><para>示例值：商品不参与积分活动</para><para>可为null</para></param>
        /// <param name="total_points">顾客积分总额 <para>body当前顾客积分总额</para><para>示例值：888888</para><para>可为null</para></param>
        public NotifyBusinessCirclePointsRequestData(string transaction_id, string appid, string openid, bool earn_points, int increased_points, TenpayDateTime points_update_time, string no_points_remarks, int? total_points)
        {
            this.transaction_id = transaction_id;
            this.appid = appid;
            this.openid = openid;
            this.earn_points = earn_points;
            this.increased_points = increased_points;
            this.points_update_time = points_update_time.ToString();
            this.no_points_remarks = no_points_remarks;
            this.total_points = total_points;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public NotifyBusinessCirclePointsRequestData()
        {
        }

        /// <summary>
        /// 微信订单号
        /// <para>body微信支付推送的商圈内交易通知里携带的微信订单号 </para>
        /// <para>示例值：1217752501201407033233368018</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 小程序appid
        /// <para>body顾客授权积分时使用的小程序的appid </para>
        /// <para>示例值：wx1234567890abcdef</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 用户标识
        /// <para>body顾客授权时使用的小程序上的openid </para>
        /// <para>示例值：oWmnN4xxxxxxxxxxe92NHIGf1xd8</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 是否获得积分
        /// <para>body用于标明此单是否获得积分，true为获得积分，false为未获得 </para>
        /// <para>示例值：true</para>
        /// </summary>
        public bool earn_points { get; set; }

        /// <summary>
        /// 订单新增积分值
        /// <para>body顾客此笔交易新增的积分值 </para>
        /// <para>示例值：100</para>
        /// </summary>
        public int increased_points { get; set; }

        /// <summary>
        /// 积分更新时间
        /// <para>body为顾客此笔交易成功积分的时间 </para>
        /// <para>示例值：2020-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public string points_update_time { get; set; }

        /// <summary>
        /// 未获得积分的备注信息
        /// <para>body当未获得积分时，提供未获得积分的原因等备注信息 </para>
        /// <para>示例值：商品不参与积分活动</para>
        /// <para>可为null</para>
        /// </summary>
        public string no_points_remarks { get; set; }

        /// <summary>
        /// 顾客积分总额
        /// <para>body当前顾客积分总额 </para>
        /// <para>示例值：888888</para>
        /// <para>可为null</para>
        /// </summary>
        public int? total_points { get; set; }

    }
}
