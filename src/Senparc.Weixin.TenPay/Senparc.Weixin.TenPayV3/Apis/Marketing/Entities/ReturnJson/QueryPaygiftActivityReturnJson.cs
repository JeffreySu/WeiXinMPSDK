﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
  
    文件名：QueryPaygiftActivityReturnJson.cs
    文件功能描述：查询支付有礼活动详情接口返回Json类
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 查询支付有礼活动详情接口返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_4.shtml </para>
    /// </summary>
    public class QueryPaygiftActivityReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="activity_id">活动id  <para>活动id</para><para>示例值：10028001</para></param>
        /// <param name="activity_type">活动类型  <para>活动类型枚举值：FULL_SEND_ACT_TYPE：满额送</para><para></para><para>示例值：FULL_SEND_ACT_TYPE</para><para>可为null</para></param>
        /// <param name="activity_base_info">活动基本信息 <para>创建活动时录入的基本信息。</para></param>
        /// <param name="award_send_rule">活动奖品发放规则 <para>支付有礼活动奖品派送规则，多门槛送券。</para></param>
        /// <param name="advanced_setting">活动高级设置 <para>其他高级配置项，商户自定义。</para><para>可为null</para></param>
        /// <param name="activity_status">活动状态  <para>活动当前状态。枚举值：CREATE_ACT_STATUS：已创建ONGOING_ACT_STATUS：运行中TERMINATE_ACT_STATUS：已终止STOP_ACT_STATUS：已暂停OVER_TIME_ACT_STATUS：已过期CREATE_ACT_FAILED：创建活动失败示例值：CREATE_ACT_STATUS</para></param>
        /// <param name="creator_merchant_id">创建商户号  <para>支付有礼活动创建商户的商户号</para><para>注：创建商户号即调用“创建全场满额送活动接口”请求头商户号</para><para>示例值：10000022</para></param>
        /// <param name="belong_merchant_id">所属商户号  <para>支付有礼活动所属商户的商户号</para><para>注：1，所属商户号即调用“创建全场满额送活动接口”请求头商户号2，该字体暂未启用</para><para>示例值：10000022</para></param>
        /// <param name="pause_time">活动暂停时间  <para>暂停活动的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T00:00:00+08:00</para><para>可为null</para></param>
        /// <param name="recovery_time">活动恢复时间  <para>恢复活动的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T00:00:00+08:00</para><para>可为null</para></param>
        /// <param name="create_time">活动创建时间  <para>创建活动的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T00:00:00+08:00</para><para>可为null</para></param>
        /// <param name="update_time">活动更新时间 <para>更新活动的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T00:00:00+08:00</para><para>可为null</para></param>
        public QueryPaygiftActivityReturnJson(string activity_id, string activity_type, Activity_Base_Info activity_base_info, Award_Send_Rule award_send_rule, Advanced_Setting advanced_setting, string activity_status, string creator_merchant_id, string belong_merchant_id, string pause_time, string recovery_time, string create_time, string update_time)
        {
            this.activity_id = activity_id;
            this.activity_type = activity_type;
            this.activity_base_info = activity_base_info;
            this.award_send_rule = award_send_rule;
            this.advanced_setting = advanced_setting;
            this.activity_status = activity_status;
            this.creator_merchant_id = creator_merchant_id;
            this.belong_merchant_id = belong_merchant_id;
            this.pause_time = pause_time;
            this.recovery_time = recovery_time;
            this.create_time = create_time;
            this.update_time = update_time;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryPaygiftActivityReturnJson()
        {
        }

        /// <summary>
        /// 活动id 
        /// <para>活动id </para>
        /// <para>示例值：10028001 </para>
        /// </summary>
        public string activity_id { get; set; }

        /// <summary>
        /// 活动类型 
        /// <para>活动类型 枚举值： FULL_SEND_ACT_TYPE：满额送 </para>
        /// <para></para>
        /// <para>示例值：FULL_SEND_ACT_TYPE </para>
        /// <para>可为null</para>
        /// </summary>
        public string activity_type { get; set; }

        /// <summary>
        /// 活动基本信息
        /// <para>创建活动时录入的基本信息。 </para>
        /// </summary>
        public Activity_Base_Info activity_base_info { get; set; }

        /// <summary>
        /// 活动奖品发放规则
        /// <para>支付有礼活动奖品派送规则，多门槛送券。 </para>
        /// </summary>
        public Award_Send_Rule award_send_rule { get; set; }

        /// <summary>
        /// 活动高级设置
        /// <para>其他高级配置项，商户自定义。 </para>
        /// <para>可为null</para>
        /// </summary>
        public Advanced_Setting advanced_setting { get; set; }

        /// <summary>
        /// 活动状态 
        /// <para>活动当前状态。 枚举值： CREATE_ACT_STATUS：已创建 ONGOING_ACT_STATUS：运行中 TERMINATE_ACT_STATUS：已终止 STOP_ACT_STATUS：已暂停 OVER_TIME_ACT_STATUS：已过期 CREATE_ACT_FAILED：创建活动失败 示例值：CREATE_ACT_STATUS </para>
        /// </summary>
        public string activity_status { get; set; }

        /// <summary>
        /// 创建商户号 
        /// <para>支付有礼活动创建商户的商户号 </para>
        /// <para>注：创建商户号即调用“创建全场满额送活动接口”请求头商户号</para>
        /// <para>示例值：10000022 </para>
        /// </summary>
        public string creator_merchant_id { get; set; }

        /// <summary>
        /// 所属商户号 
        /// <para>支付有礼活动所属商户的商户号 </para>
        /// <para>注：1，所属商户号即调用“创建全场满额送活动接口”请求头商户号2，该字体暂未启用</para>
        /// <para>示例值：10000022 </para>
        /// </summary>
        public string belong_merchant_id { get; set; }

        /// <summary>
        /// 活动暂停时间 
        /// <para>暂停活动的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para>
        /// <para>示例值：2015-05-20T00:00:00+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string pause_time { get; set; }

        /// <summary>
        /// 活动恢复时间 
        /// <para>恢复活动的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para>
        /// <para>示例值：2015-05-20T00:00:00+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string recovery_time { get; set; }

        /// <summary>
        /// 活动创建时间 
        /// <para>创建活动的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para>
        /// <para>示例值：2015-05-20T00:00:00+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 活动更新时间
        /// <para>更新活动的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para>
        /// <para>示例值：2015-05-20T00:00:00+08:00</para>
        /// <para>可为null</para>
        /// </summary>
        public string update_time { get; set; }

        #region 子数据类型
        public class Activity_Base_Info
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="activity_name">活动名称  <para>活动名称</para><para>示例值：良品铺子回馈活动</para></param>
            /// <param name="activity_second_title">活动副标题  <para>活动副标题</para><para>示例值：海飞丝的券</para></param>
            /// <param name="merchant_logo_url">商户logo  <para>商户logo，送出优惠券时展示，可通过《图片上传API》获得图片cdn地址。</para><para>示例值：https://tool.oschina.net/regex.jpg</para></param>
            /// <param name="background_color">背景颜色  <para>代金券的背景颜色，可设置10种颜色，颜色取值请参见色卡图，默认为微信支付绿色，颜色取值为颜色图中的颜色名称。</para><para>示例值：COLOR020</para><para>可为null</para></param>
            /// <param name="begin_time">活动开始时间  <para>1、活动开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。2、最长可以配置1年内的活动，活动有效期最长90天。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
            /// <param name="end_time">活动结束时间  <para>1、活动结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。2、最长可以配置1年内的活动，活动有效期最长90天。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
            /// <param name="available_periods">可用时间段 <para>可自定义活动有效时间内可用的多个时间段以及每天可用的时间点。</para><para>可为null</para></param>
            /// <param name="out_request_no">商户请求单号  <para>商户创建批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性，可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号。</para><para>示例值：100002322019090134234sfdf</para></param>
            /// <param name="delivery_purpose">投放目的  <para>枚举值：OFF_LINE_PAY：拉用户回店消费JUMP_MINI_APP：引导用户前往小程序消费</para><para>示例值：OFF_LINE_PAY</para></param>
            /// <param name="mini_programs_appid">商家小程序appid  <para>投放目的为JUMP_MINI_APP时必填。</para><para>示例值：wx23232232323</para><para>可为null</para></param>
            /// <param name="mini_programs_path">商家小程序path  <para>如果传入此参数，则跳转至商家小程序；反之则跳转至券详情页面。</para><para>示例值：/path/index/index</para><para>可为null</para></param>
            public Activity_Base_Info(string activity_name, string activity_second_title, string merchant_logo_url, string background_color, string begin_time, string end_time, Available_Periods available_periods, string out_request_no, string delivery_purpose, string mini_programs_appid, string mini_programs_path)
            {
                this.activity_name = activity_name;
                this.activity_second_title = activity_second_title;
                this.merchant_logo_url = merchant_logo_url;
                this.background_color = background_color;
                this.begin_time = begin_time;
                this.end_time = end_time;
                this.available_periods = available_periods;
                this.out_request_no = out_request_no;
                this.delivery_purpose = delivery_purpose;
                this.mini_programs_appid = mini_programs_appid;
                this.mini_programs_path = mini_programs_path;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Activity_Base_Info()
            {
            }

            /// <summary>
            /// 活动名称 
            /// <para>活动名称 </para>
            /// <para>示例值：良品铺子回馈活动 </para>
            /// </summary>
            public string activity_name { get; set; }

            /// <summary>
            /// 活动副标题 
            /// <para>活动副标题 </para>
            /// <para>示例值：海飞丝的券 </para>
            /// </summary>
            public string activity_second_title { get; set; }

            /// <summary>
            /// 商户logo 
            /// <para>商户logo，送出优惠券时展示， 可通过《图片上传API》获得图片cdn地址。 </para>
            /// <para>示例值：https://tool.oschina.net/regex.jpg </para>
            /// </summary>
            public string merchant_logo_url { get; set; }

            /// <summary>
            /// 背景颜色 
            /// <para>代金券的背景颜色，可设置10种颜色，颜色取值请参见色卡图，默认为微信支付绿色， 颜色取值为颜色图中的颜色名称。 </para>
            /// <para>示例值：COLOR020 </para>
            /// <para>可为null</para>
            /// </summary>
            public string background_color { get; set; }

            /// <summary>
            /// 活动开始时间 
            /// <para>1、活动开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。 2、最长可以配置1年内的活动，活动有效期最长90天。 </para>
            /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
            /// </summary>
            public string begin_time { get; set; }

            /// <summary>
            /// 活动结束时间 
            /// <para>1、活动结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。 2、最长可以配置1年内的活动，活动有效期最长90天。 </para>
            /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
            /// </summary>
            public string end_time { get; set; }

            /// <summary>
            /// 可用时间段
            /// <para>可自定义活动有效时间内可用的多个时间段以及每天可用的时间点。 </para>
            /// <para>可为null</para>
            /// </summary>
            public Available_Periods available_periods { get; set; }

            /// <summary>
            /// 商户请求单号 
            /// <para>商户创建批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性，可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号。 </para>
            /// <para>示例值：100002322019090134234sfdf </para>
            /// </summary>
            public string out_request_no { get; set; }

            /// <summary>
            /// 投放目的 
            /// <para>枚举值： OFF_LINE_PAY：拉用户回店消费  JUMP_MINI_APP：引导用户前往小程序消费 </para>
            /// <para>示例值：OFF_LINE_PAY </para>
            /// </summary>
            public string delivery_purpose { get; set; }

            /// <summary>
            /// 商家小程序appid 
            /// <para>投放目的为JUMP_MINI_APP时必填。 </para>
            /// <para>示例值：wx23232232323 </para>
            /// <para>可为null</para>
            /// </summary>
            public string mini_programs_appid { get; set; }

            /// <summary>
            /// 商家小程序path 
            /// <para>如果传入此参数，则跳转至商家小程序；反之则跳转至券详情页面。 </para>
            /// <para>示例值：/path/index/index </para>
            /// <para>可为null</para>
            /// </summary>
            public string mini_programs_path { get; set; }

            #region 子数据类型
            public class Available_Periods
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="available_time">可用时间 <para>可用时间，可选择多个时间段，在可用时间段内会发放优惠券。当传入available_periods（可用时间段）后，该字段必填。</para><para>可为null</para></param>
                /// <param name="available_day_time">每日可用时间 <para>每日可用时间，基于“可用时间（available_time）”范围内，可再设置每日可用时间，取两者的交集时间段发券。无限制填 000000-235959 即可。当传入available_periods（可用时间段）后，该字段必填。</para><para>可为null</para></param>
                public Available_Periods(Available_Time[] available_time, Available_Day_Time[] available_day_time)
                {
                    this.available_time = available_time;
                    this.available_day_time = available_day_time;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Available_Periods()
                {
                }

                /// <summary>
                /// 可用时间
                /// <para>可用时间，可选择多个时间段，在可用时间段内会发放优惠券。当传入available_periods（可用时间段）后，该字段必填。 </para>
                /// <para>可为null</para>
                /// </summary>
                public Available_Time[] available_time { get; set; }

                /// <summary>
                /// 每日可用时间
                /// <para>每日可用时间，基于“可用时间（available_time）”范围内，可再设置每日可用时间，取两者的交集时间段发券。无限制填 000000-235959 即可。当传入available_periods（可用时间段）后，该字段必填。 </para>
                /// <para>可为null</para>
                /// </summary>
                public Available_Day_Time[] available_day_time { get; set; }

                #region 子数据类型
                public class Available_Time
                {

                    /// <summary>
                    /// 含参构造函数
                    /// </summary>
                    /// <param name="begin_time">可用开始时间  <para>可用开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T00:00:00+08:00</para></param>
                    /// <param name="end_time">可用结束时间  <para>可用结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T00:00:00+08:00</para></param>
                    public Available_Time(string begin_time, string end_time)
                    {
                        this.begin_time = begin_time;
                        this.end_time = end_time;
                    }

                    /// <summary>
                    /// 无参构造函数
                    /// </summary>
                    public Available_Time()
                    {
                    }

                    /// <summary>
                    /// 可用开始时间 
                    /// <para>可用开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para>
                    /// <para>示例值：2015-05-20T00:00:00+08:00</para>
                    /// </summary>
                    public string begin_time { get; set; }

                    /// <summary>
                    /// 可用结束时间 
                    /// <para>可用结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para>
                    /// <para>示例值：2015-05-20T00:00:00+08:00</para>
                    /// </summary>
                    public string end_time { get; set; }

                }

                public class Available_Day_Time
                {

                    /// <summary>
                    /// 含参构造函数
                    /// </summary>
                    /// <param name="begin_day_time">每日可用开始时间  <para>每日可用开始时间，固定字符长度为6（格式：110000，表示11：00：00）。</para><para>示例值：110000</para></param>
                    /// <param name="end_day_time">每日可用结束时间  <para>每日可用结束时间，固定字符长度为6（格式：135959，表示13：59：59）。</para><para>示例值：135959</para></param>
                    public Available_Day_Time(string begin_day_time, string end_day_time)
                    {
                        this.begin_day_time = begin_day_time;
                        this.end_day_time = end_day_time;
                    }

                    /// <summary>
                    /// 无参构造函数
                    /// </summary>
                    public Available_Day_Time()
                    {
                    }

                    /// <summary>
                    /// 每日可用开始时间 
                    /// <para>每日可用开始时间，固定字符长度为6（格式：110000，表示11：00：00）。 </para>
                    /// <para>示例值：110000</para>
                    /// </summary>
                    public string begin_day_time { get; set; }

                    /// <summary>
                    /// 每日可用结束时间 
                    /// <para>每日可用结束时间，固定字符长度为6（格式：135959，表示13：59：59）。 </para>
                    /// <para>示例值：135959 </para>
                    /// </summary>
                    public string end_day_time { get; set; }

                }


                #endregion
            }


            #endregion
        }

        public class Award_Send_Rule
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="full_send_rule">满送活动奖品发放规则 <para>满送活动奖品派送规则。</para><para>可为null</para></param>
            public Award_Send_Rule(Full_Send_Rule full_send_rule)
            {
                this.full_send_rule = full_send_rule;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Award_Send_Rule()
            {
            }

            /// <summary>
            /// 满送活动奖品发放规则
            /// <para>满送活动奖品派送规则。</para>
            /// <para>可为null</para>
            /// </summary>
            public Full_Send_Rule full_send_rule { get; set; }

            #region 子数据类型
            public class Full_Send_Rule
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="transaction_amount_minimum">消费金额门槛  <para>消费金额门槛，单位：分。</para><para>示例值：100</para></param>
                /// <param name="send_content">发放内容  <para>发放内容，可选单张券或礼包，选礼包时奖品限定3-5个。枚举值：SINGLE_COUPON：单张券GIFT_PACKAGE：礼包</para><para>示例值：SINGLE_COUPON</para></param>
                /// <param name="award_type">奖品类型  <para>奖品类型，暂时只支持商家券。枚举值：BUSIFAVOR：商家券</para><para>示例值：BUSIFAVOR</para></param>
                /// <param name="award_list">奖品基本信息列表 <para>奖品基本信息列表，当send_content（发放内容）选择GIFT_PACKAGE（礼包）时，奖品信息必须是3-5个，与send_content（发放内容）中礼包的个数保持一致。</para><para>条目个数限制：[1，5]</para><para>可为null</para></param>
                /// <param name="merchant_option">发券商户号选项  <para>发券商户号选取规则，支持选择在用券商户号和手动输入发券商户号两种规则。枚举值：IN_SEVICE_COUPON_MERCHANT：在用券商户号MANUAL_INPUT_MERCHANT：手动输入发券商户号</para><para>示例值：MANUAL_INPUT_MERCHANT</para></param>
                public Full_Send_Rule(int transaction_amount_minimum, string send_content, string award_type, Award_List[] award_list, string merchant_option)
                {
                    this.transaction_amount_minimum = transaction_amount_minimum;
                    this.send_content = send_content;
                    this.award_type = award_type;
                    this.award_list = award_list;
                    this.merchant_option = merchant_option;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Full_Send_Rule()
                {
                }

                /// <summary>
                /// 消费金额门槛 
                /// <para>消费金额门槛，单位：分。 </para>
                /// <para>示例值：100 </para>
                /// </summary>
                public int transaction_amount_minimum { get; set; }

                /// <summary>
                /// 发放内容 
                /// <para>发放内容，可选单张券或礼包，选礼包时奖品限定3-5个。 枚举值： SINGLE_COUPON：单张券 GIFT_PACKAGE：礼包 </para>
                /// <para>示例值：SINGLE_COUPON </para>
                /// </summary>
                public string send_content { get; set; }

                /// <summary>
                /// 奖品类型 
                /// <para>奖品类型，暂时只支持商家券。枚举值： BUSIFAVOR：商家券 </para>
                /// <para>示例值：BUSIFAVOR </para>
                /// </summary>
                public string award_type { get; set; }

                /// <summary>
                /// 奖品基本信息列表
                /// <para>奖品基本信息列表，当send_content（发放内容）选择GIFT_PACKAGE（礼包）时，奖品信息必须是3-5个，与send_content（发放内容）中礼包的个数保持一致。 </para>
                /// <para>条目个数限制：[1，5] </para>
                /// <para>可为null</para>
                /// </summary>
                public Award_List[] award_list { get; set; }

                /// <summary>
                /// 发券商户号选项 
                /// <para>发券商户号选取规则，支持选择在用券商户号和手动输入发券商户号两种规则。 枚举值： IN_SEVICE_COUPON_MERCHANT：在用券商户号 MANUAL_INPUT_MERCHANT：手动输入发券商户号 </para>
                /// <para>示例值：MANUAL_INPUT_MERCHANT </para>
                /// </summary>
                public string merchant_option { get; set; }

                #region 子数据类型
                public class Award_List
                {

                    /// <summary>
                    /// 含参构造函数
                    /// </summary>
                    /// <param name="stock_id">批次id  <para>批次id</para><para>示例值：98065001</para></param>
                    /// <param name="original_image_url">奖品原始图（大图） <para>奖品大图，图片建议尺寸：678像素*232像素，支持JPG、PNG格式；仅支持通过《图片上传API》接口获取的图片URL地址。</para><para>示例值：https://tool.oschina.net/regex.jpg</para></param>
                    /// <param name="thumbnail_url">奖品缩略图（小图）  <para>奖品小图，图片建议尺寸：120像素*120像素，支持JPG、PNG格式；当选多张券时必填，仅支持通过《图片上传API》接口获取的图片URL地址。</para><para>示例值：https://tool.oschina.net/regex.jpg</para><para>可为null</para></param>
                    public Award_List(string stock_id, string original_image_url, string thumbnail_url)
                    {
                        this.stock_id = stock_id;
                        this.original_image_url = original_image_url;
                        this.thumbnail_url = thumbnail_url;
                    }

                    /// <summary>
                    /// 无参构造函数
                    /// </summary>
                    public Award_List()
                    {
                    }

                    /// <summary>
                    /// 批次id 
                    /// <para>批次id</para>
                    /// <para>示例值：98065001 </para>
                    /// </summary>
                    public string stock_id { get; set; }

                    /// <summary>
                    /// 奖品原始图（大图）
                    /// <para>奖品大图，图片建议尺寸：678像素*232像素，支持JPG、PNG格式； 仅支持通过《图片上传API》接口获取的图片URL地址。 </para>
                    /// <para>示例值：https://tool.oschina.net/regex.jpg </para>
                    /// </summary>
                    public string original_image_url { get; set; }

                    /// <summary>
                    /// 奖品缩略图（小图） 
                    /// <para>奖品小图，图片建议尺寸：120像素*120像素，支持JPG、PNG格式；当选多张券时必填， 仅支持通过《图片上传API》接口获取的图片URL地址。</para>
                    /// <para>示例值：https://tool.oschina.net/regex.jpg</para>
                    /// <para>可为null</para>
                    /// </summary>
                    public string thumbnail_url { get; set; }

                }


                #endregion
            }


            #endregion
        }

        public class Advanced_Setting
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="delivery_user_category">投放用户类别  <para>枚举值：DELIVERY_ALL_PERSON：所有用户DELIVERY_MEMBER_PERSON：会员用户</para><para>示例值：DELIVERY_MEMBER_PERSON</para><para>可为null</para></param>
            /// <param name="merchant_member_appid">商家会员appid  <para>当投放用户类别为会员用户时必填。</para><para>示例值：34567890</para><para>可为null</para></param>
            /// <param name="goods_tags">订单优惠标记  <para>商户下单时需要传入相同的标记(goods_tag)，最多只能传入50个订单优惠标记，用户同时符合其他规则才能享受优惠。</para><para>示例值：xxx，yyy</para><para>可为null</para></param>
            public Advanced_Setting(string delivery_user_category, string merchant_member_appid, string[] goods_tags)
            {
                this.delivery_user_category = delivery_user_category;
                this.merchant_member_appid = merchant_member_appid;
                this.goods_tags = goods_tags;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Advanced_Setting()
            {
            }

            /// <summary>
            /// 投放用户类别 
            /// <para>枚举值： DELIVERY_ALL_PERSON：所有用户  DELIVERY_MEMBER_PERSON：会员用户 </para>
            /// <para>示例值：DELIVERY_MEMBER_PERSON </para>
            /// <para>可为null</para>
            /// </summary>
            public string delivery_user_category { get; set; }

            /// <summary>
            /// 商家会员appid 
            /// <para>当投放用户类别为会员用户时必填。 </para>
            /// <para>示例值：34567890 </para>
            /// <para>可为null</para>
            /// </summary>
            public string merchant_member_appid { get; set; }

            /// <summary>
            /// 订单优惠标记 
            /// <para>商户下单时需要传入相同的标记(goods_tag)，最多只能传入50个订单优惠标记，用户同时符合其他规则才能享受优惠。 </para>
            /// <para>示例值：xxx，yyy</para>
            /// <para>可为null</para>
            /// </summary>
            public string[] goods_tags { get; set; }

        }

        #endregion
    }




}


