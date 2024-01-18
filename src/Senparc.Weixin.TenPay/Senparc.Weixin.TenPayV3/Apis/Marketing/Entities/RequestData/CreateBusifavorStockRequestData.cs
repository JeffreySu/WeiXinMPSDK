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
  
    文件名：CreateBusifavorStockRequestData.cs
    文件功能描述：创建商家券批次接口请求数据
    
    
    创建标识：Senparc - 20210907
    
    修改标识：Senparc - 20230109
    修改描述：v0.6.8.4 修改 week_day 类型为 int[]

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 创建商家券批次接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_1.shtml </para>
    /// </summary>
    public class CreateBusifavorStockRequestData
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="stock_name">商家券批次名称  <para>body批次名称，字数上限为21个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：8月1日活动券</para></param>
        /// <param name="belong_merchant">批次归属商户号  <para>body批次归属于哪个商户。</para><para>注：普通直连模式，该参数为直连商户号</para><para>示例值：10000022</para></param>
        /// <param name="comment">批次备注  <para>body仅配置商户可见，用于自定义信息。字数上限为20个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：活动使用</para><para>可为null</para></param>
        /// <param name="goods_name">适用商品范围  <para>body用来描述批次在哪些商品可用，会显示在微信卡包中。字数上限为15个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：xxx商品使用</para></param>
        /// <param name="stock_type">批次类型  <para>body批次类型NORMAL：固定面额满减券批次DISCOUNT：折扣券批次EXCHANGE：换购券批次</para><para>示例值：NORMAL</para></param>
        /// <param name="coupon_use_rule">核销规则 <para>body券核销相关规则</para></param>
        /// <param name="stock_send_rule">发放规则 <para>body券发放相关规则</para></param>
        /// <param name="out_request_no">商户请求单号  <para>body商户创建批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性。</para><para>示例值：100002322019090134234sfdf</para></param>
        /// <param name="custom_entrance">自定义入口 <para>body卡详情页面，可选择多种入口引导用户。</para><para>可为null</para></param>
        /// <param name="display_pattern_info">样式信息 <para>body创建批次时的样式信息。</para><para>可为null</para></param>
        /// <param name="coupon_code_mode">券code模式  <para>body枚举值：WECHATPAY_MODE：系统分配券code。（固定22位纯数字）MERCHANT_API：商户发放时接口指定券code。MERCHANT_UPLOAD：商户上传自定义code，发券时系统随机选取上传的券code。</para><para>特殊规则：1、券code模式为WECHATPAY_MODE时，是微信自动分配券code，商户不需要预存code；适用于多种场景2、券code模式为MERCHANT_API时，无需调用上传预存code接口，调用发券接口时需指定券code；更多用在商家自有流量场景（例如：商家自有小程序、H5网页等）3、券code模式为MERCHANT_UPLOAD，需要调用上传预存code接口上传code，调用发券接口时无需指定code；更多适用在微信支付平台流量场景（例如：支付有礼、支付有优惠等）</para><para>示例值：WECHATPAY_MODE</para></param>
        /// <param name="notify_config">事件通知配置 <para>body事件回调通知商户的配置。</para><para>可为null</para></param>
        /// <param name="subsidy">是否允许营销补贴 <para>body该批次发放的券是否允许进行补差，默认为false</para><para>示例值：false</para></param>
        public CreateBusifavorStockRequestData(string stock_name, string belong_merchant, string comment, string goods_name, string stock_type, Coupon_Use_Rule coupon_use_rule, Stock_Send_Rule stock_send_rule, string out_request_no, Custom_Entrance custom_entrance, Display_Pattern_Info display_pattern_info, string coupon_code_mode, Notify_Config notify_config, bool subsidy = false)
        {
            this.stock_name = stock_name;
            this.belong_merchant = belong_merchant;
            this.comment = comment;
            this.goods_name = goods_name;
            this.stock_type = stock_type;
            this.coupon_use_rule = coupon_use_rule;
            this.stock_send_rule = stock_send_rule;
            this.out_request_no = out_request_no;
            this.custom_entrance = custom_entrance;
            this.display_pattern_info = display_pattern_info;
            this.coupon_code_mode = coupon_code_mode;
            this.notify_config = notify_config;
            this.subsidy = subsidy;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CreateBusifavorStockRequestData()
        {
        }

        /// <summary>
        /// 商家券批次名称 
        /// <para>body 批次名称，字数上限为21个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
        /// <para>示例值：8月1日活动券 </para>
        /// </summary>
        public string stock_name { get; set; }

        /// <summary>
        /// 批次归属商户号 
        /// <para>body 批次归属于哪个商户。 </para>
        /// <para>注：普通直连模式，该参数为直连商户号</para>
        /// <para>示例值：10000022 </para>
        /// </summary>
        public string belong_merchant { get; set; }

        /// <summary>
        /// 批次备注 
        /// <para>body 仅配置商户可见，用于自定义信息。字数上限为20个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
        /// <para>示例值：活动使用 </para>
        /// <para>可为null</para>
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 适用商品范围 
        /// <para>body 用来描述批次在哪些商品可用，会显示在微信卡包中。字数上限为15个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
        /// <para>示例值：xxx商品使用 </para>
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 批次类型 
        /// <para>body 批次类型 NORMAL：固定面额满减券批次 DISCOUNT：折扣券批次 EXCHANGE：换购券批次 </para>
        /// <para>示例值：NORMAL</para>
        /// </summary>
        public string stock_type { get; set; }

        /// <summary>
        /// 核销规则
        /// <para>body 券核销相关规则</para>
        /// </summary>
        public Coupon_Use_Rule coupon_use_rule { get; set; }

        /// <summary>
        /// 发放规则
        /// <para>body 券发放相关规则</para>
        /// </summary>
        public Stock_Send_Rule stock_send_rule { get; set; }

        /// <summary>
        /// 商户请求单号 
        /// <para>body 商户创建批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性。 </para>
        /// <para>示例值：100002322019090134234sfdf </para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 自定义入口
        /// <para>body 卡详情页面，可选择多种入口引导用户。 </para>
        /// <para>可为null</para>
        /// </summary>
        public Custom_Entrance custom_entrance { get; set; }

        /// <summary>
        /// 样式信息
        /// <para>body 创建批次时的样式信息。</para>
        /// <para>可为null</para>
        /// </summary>
        public Display_Pattern_Info display_pattern_info { get; set; }

        /// <summary>
        /// 券code模式 
        /// <para>body 枚举值： WECHATPAY_MODE：系统分配券code。（固定22位纯数字） MERCHANT_API：商户发放时接口指定券code。 MERCHANT_UPLOAD：商户上传自定义code，发券时系统随机选取上传的券code。 </para>
        /// <para>特殊规则：1、券code模式为WECHATPAY_MODE时，是微信自动分配券code，商户不需要预存code；适用于多种场景2、券code模式为MERCHANT_API时，无需调用上传预存code接口，调用发券接口时需指定券code；更多用在商家自有流量场景（例如：商家自有小程序、H5网页等）3、券code模式为MERCHANT_UPLOAD，需要调用上传预存code接口上传code，调用发券接口时无需指定code；更多适用在微信支付平台流量场景（例如：支付有礼、支付有优惠等）</para>
        /// <para>示例值：WECHATPAY_MODE </para>
        /// </summary>
        public string coupon_code_mode { get; set; }

        /// <summary>
        /// 事件通知配置
        /// <para>body 事件回调通知商户的配置。</para>
        /// <para>可为null</para>
        /// </summary>
        public Notify_Config notify_config { get; set; }

        /// <summary>
        /// 是否允许营销补贴 
        /// <para>body 该批次发放的券是否允许进行补差，默认为false </para>
        /// <para>示例值：false </para>
        /// <para>可为null</para>
        /// </summary>
        public bool subsidy { get; set; }

        #region 子数据类型
        /// <summary>
        /// 核销规则
        /// </summary>
        public class Coupon_Use_Rule
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="coupon_available_time">券可核销时间 <para>日期区间内可以使用优惠。</para></param>
            /// <param name="fixed_normal_coupon">固定面额满减券使用规则 <para>stock_type为NORMAL时必填。</para><para>fixed_normal_coupon discount_coupon exchange_coupon三选一</para></param>
            /// <param name="discount_coupon">折扣券使用规则 <para>stock_type为DISCOUNT时必填。</para><para>fixed_normal_coupon discount_coupon exchange_coupon三选一</para></param>
            /// <param name="exchange_coupon">换购券使用规则 <para>stock_type为EXCHANGE时必填。</para><para>fixed_normal_coupon discount_coupon exchange_coupon三选一</para></param>
            /// <param name="use_method">核销方式  <para>枚举值：OFF_LINE：线下滴码核销，点击券“立即使用”跳转展示券二维码详情。MINI_PROGRAMS：线上小程序核销，点击券“立即使用”跳转至配置的商家小程序（需要添加小程序appid和path）。PAYMENT_CODE：微信支付付款码核销，点击券“立即使用”跳转至微信支付钱包付款码。SELF_CONSUME：用户自助核销，点击券“立即使用”跳转至用户自助操作核销界面（当前暂不支持用户自助核销）。</para><para>示例值：OFF_LINE</para></param>
            /// <param name="mini_programs_appid">小程序appid  <para>核销方式为线上小程序核销才有效。</para><para>示例值：wx23232232323</para></param>
            /// <param name="mini_programs_path">小程序path  <para>核销方式为线上小程序核销才有效。</para><para>示例值：/path/index/index</para></param>
            public Coupon_Use_Rule(Coupon_Available_Time coupon_available_time, Fixed_Normal_Coupon fixed_normal_coupon, Discount_Coupon discount_coupon, Exchange_Coupon exchange_coupon, string use_method, string mini_programs_appid, string mini_programs_path)
            {
                this.coupon_available_time = coupon_available_time;
                this.fixed_normal_coupon = fixed_normal_coupon;
                this.discount_coupon = discount_coupon;
                this.exchange_coupon = exchange_coupon;
                this.use_method = use_method;
                this.mini_programs_appid = mini_programs_appid;
                this.mini_programs_path = mini_programs_path;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Coupon_Use_Rule()
            {
            }

            /// <summary>
            /// 券可核销时间
            /// <para>日期区间内可以使用优惠。</para>
            /// </summary>
            public Coupon_Available_Time coupon_available_time { get; set; }

            /// <summary>
            /// 固定面额满减券使用规则
            /// <para>stock_type为NORMAL时必填。</para><para>fixed_normal_coupon discount_coupon exchange_coupon三选一</para>
            /// </summary>
            public Fixed_Normal_Coupon fixed_normal_coupon { get; set; }

            /// <summary>
            /// 折扣券使用规则
            /// <para>stock_type为DISCOUNT时必填。</para><para>fixed_normal_coupon discount_coupon exchange_coupon三选一</para>
            /// </summary>
            public Discount_Coupon discount_coupon { get; set; }

            /// <summary>
            /// 换购券使用规则
            /// <para>stock_type为EXCHANGE时必填。</para><para>fixed_normal_coupon discount_coupon exchange_coupon三选一</para>
            /// </summary>
            public Exchange_Coupon exchange_coupon { get; set; }

            /// <summary>
            /// 核销方式 
            /// <para>枚举值： OFF_LINE：线下滴码核销，点击券“立即使用”跳转展示券二维码详情。 MINI_PROGRAMS：线上小程序核销，点击券“立即使用”跳转至配置的商家小程序（需要添加小程序appid和path）。 PAYMENT_CODE：微信支付付款码核销，点击券“立即使用”跳转至微信支付钱包付款码。 SELF_CONSUME：用户自助核销，点击券“立即使用”跳转至用户自助操作核销界面（当前暂不支持用户自助核销）。</para>
            /// <para>示例值：OFF_LINE </para>
            /// </summary>
            public string use_method { get; set; }

            /// <summary>
            /// 小程序appid 
            /// <para>核销方式为线上小程序核销才有效。 </para>
            /// <para>示例值：wx23232232323 </para>
            /// </summary>
            public string mini_programs_appid { get; set; }

            /// <summary>
            /// 小程序path 
            /// <para>核销方式为线上小程序核销才有效。 </para>
            /// <para>示例值：/path/index/index </para>
            /// </summary>
            public string mini_programs_path { get; set; }

            #region 子数据类型
            public class Coupon_Available_Time
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="available_begin_time">开始时间<para>批次开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>注意：开始时间设置有效期最长为1年。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
                /// <param name="available_end_time">结束时间<para>批次结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>注意：结束时间设置有效期最长为1年。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
                /// <param name="available_day_after_receive">生效后N天内有效 <para>日期区间内，券生效后x天内有效。例如生效当天内有效填1，生效后2天内有效填2，以此类推。注意，用户在有效期开始前领取商家券，则从有效期第1天开始计算天数，用户在有效期内领取商家券，则从领取当天开始计算天数，无论用户何时领取商家券，商家券在活动有效期结束后均不可用。可配合wait_days_after_receive一同填写，也可单独填写。单独填写时，有效期内领券后立即生效，生效后x天内有效。</para><para>示例值：3</para><para>可为null</para></param>
                /// <param name="available_week">固定周期有效时间段 <para>可以设置多个星期下的多个可用时间段，比如每周二10点到18点，用户自定义字段。</para><para>可为null</para></param>
                /// <param name="irregulary_avaliable_time">无规律的有效时间段 <para>无规律的有效时间，多个无规律时间段，用户自定义字段。</para><para>可为null</para></param>
                /// <param name="wait_days_after_receive">领取后N天开始生效 <para>日期区间内，用户领券后需等待x天开始生效。例如领券后当天开始生效则无需填写，领券后第2天开始生效填1，以此类推。用户在有效期开始前领取商家券，则从有效期第1天开始计算天数，用户在有效期内领取商家券，则从领取当天开始计算天数。无论用户何时领取商家券，商家券在活动有效期结束后均不可用。需配合available_day_after_receive一同填写，不可单独填写。</para><para>示例值：7</para><para>可为null</para></param>
                public Coupon_Available_Time(TenpayDateTime available_begin_time, TenpayDateTime available_end_time, int available_day_after_receive, Available_Week available_week, Irregulary_Avaliable_Time[] irregulary_avaliable_time, int? wait_days_after_receive)
                {
                    this.available_begin_time = available_begin_time.ToString();
                    this.available_end_time = available_end_time.ToString();
                    this.available_day_after_receive = available_day_after_receive;
                    this.available_week = available_week;
                    this.irregulary_avaliable_time = irregulary_avaliable_time;
                    this.wait_days_after_receive = wait_days_after_receive;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Coupon_Available_Time()
                {
                }

                /// <summary>
                /// 开始时间 
                /// <para>批次开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
                /// <para>注意：开始时间设置有效期最长为1年。</para>
                /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
                /// </summary>
                public string available_begin_time { get; set; }

                /// <summary>
                /// 结束时间 
                /// <para>批次结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
                /// <para>注意：结束时间设置有效期最长为1年。</para>
                /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
                /// </summary>
                public string available_end_time { get; set; }

                /// <summary>
                /// 生效后N天内有效
                /// <para>日期区间内，券生效后x天内有效。例如生效当天内有效填1，生效后2天内有效填2，以此类推。注意，用户在有效期开始前领取商家券，则从有效期第1天开始计算天数，用户在有效期内领取商家券，则从领取当天开始计算天数，无论用户何时领取商家券，商家券在活动有效期结束后均不可用。可配合wait_days_after_receive一同填写，也可单独填写。单独填写时，有效期内领券后立即生效，生效后x天内有效。 </para>
                /// <para>示例值：3</para>
                /// <para>可为null</para>
                /// </summary>
                public int available_day_after_receive { get; set; }

                /// <summary>
                /// 固定周期有效时间段
                /// <para>可以设置多个星期下的多个可用时间段，比如每周二10点到18点，用户自定义字段。</para>
                /// <para>可为null</para>
                /// </summary>
                public Available_Week available_week { get; set; }

                /// <summary>
                /// 无规律的有效时间段
                /// <para>无规律的有效时间，多个无规律时间段，用户自定义字段。</para>
                /// <para>可为null</para>
                /// </summary>
                public Irregulary_Avaliable_Time[] irregulary_avaliable_time { get; set; }

                /// <summary>
                /// 领取后N天开始生效
                /// <para>日期区间内，用户领券后需等待x天开始生效。例如领券后当天开始生效则无需填写，领券后第2天开始生效填1，以此类推。用户在有效期开始前领取商家券，则从有效期第1天开始计算天数，用户在有效期内领取商家券，则从领取当天开始计算天数。无论用户何时领取商家券，商家券在活动有效期结束后均不可用。需配合available_day_after_receive一同填写，不可单独填写。 </para>
                /// <para>示例值：7</para>
                /// <para>可为null</para>
                /// </summary>
                public int? wait_days_after_receive { get; set; }

                #region 子数据类型
                public class Available_Week
                {

                    /// <summary>
                    /// 含参构造函数
                    /// </summary>
                    /// <param name="week_day">可用星期数  <para>0代表周日，1代表周一，以此类推当填写available_day_time时，week_day必填</para><para>示例值：1,2</para></param>
                    /// <param name="available_day_time">当天可用时间段 <para>可以填写多个时间段，最多不超过2个。</para><para>可为null</para></param>
                    public Available_Week(int[] week_day, Available_Day_Time[] available_day_time)
                    {
                        this.week_day = week_day;
                        this.available_day_time = available_day_time;
                    }

                    /// <summary>
                    /// 无参构造函数
                    /// </summary>
                    public Available_Week()
                    {
                    }

                    /// <summary>
                    /// 可用星期数 
                    /// <para>0代表周日，1代表周一，以此类推 当填写available_day_time时，week_day必填</para>
                    /// <para>示例值：1, 2 </para>
                    /// </summary>
                    public int[] week_day { get; set; }

                    /// <summary>
                    /// 当天可用时间段
                    /// <para>可以填写多个时间段，最多不超过2个。 </para>
                    /// <para>可为null</para>
                    /// </summary>
                    public Available_Day_Time[] available_day_time { get; set; }

                    #region 子数据类型
                    public class Available_Day_Time
                    {

                        /// <summary>
                        /// 含参构造函数
                        /// </summary>
                        /// <param name="begin_time">当天可用开始时间  <para>当天可用开始时间，单位：秒，1代表当天0点0分1秒。</para><para>示例值：3600</para><para>可为null</para></param>
                        /// <param name="end_time">当天可用结束时间  <para>当天可用结束时间，单位：秒，86399代表当天23点59分59秒。</para><para>示例值：86399</para><para>可为null</para></param>
                        public Available_Day_Time(int begin_time, int end_time)
                        {
                            this.begin_time = begin_time;
                            this.end_time = end_time;
                        }

                        /// <summary>
                        /// 无参构造函数
                        /// </summary>
                        public Available_Day_Time()
                        {
                        }

                        /// <summary>
                        /// 当天可用开始时间 
                        /// <para>当天可用开始时间，单位：秒，1代表当天0点0分1秒。 </para>
                        /// <para>示例值：3600 </para>
                        /// <para>可为null</para>
                        /// </summary>
                        public int begin_time { get; set; }

                        /// <summary>
                        /// 当天可用结束时间 
                        /// <para>当天可用结束时间，单位：秒，86399代表当天23点59分59秒。 </para>
                        /// <para>示例值：86399 </para>
                        /// <para>可为null</para>
                        /// </summary>
                        public int end_time { get; set; }

                    }


                    #endregion
                }

                public class Irregulary_Avaliable_Time
                {

                    /// <summary>
                    /// 含参构造函数
                    /// </summary>
                    /// <param name="begin_time">开始时间  <para>开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para><para>可为null</para></param>
                    /// <param name="end_time">结束时间  <para>结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para><para>可为null</para></param>
                    public Irregulary_Avaliable_Time(string begin_time, string end_time)
                    {
                        this.begin_time = begin_time;
                        this.end_time = end_time;
                    }

                    /// <summary>
                    /// 无参构造函数
                    /// </summary>
                    public Irregulary_Avaliable_Time()
                    {
                    }

                    /// <summary>
                    /// 开始时间 
                    /// <para>开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
                    /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
                    /// <para>可为null</para>
                    /// </summary>
                    public string begin_time { get; set; }

                    /// <summary>
                    /// 结束时间 
                    /// <para>结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
                    /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
                    /// <para>可为null</para>
                    /// </summary>
                    public string end_time { get; set; }

                }


                #endregion
            }

            public class Fixed_Normal_Coupon
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="discount_amount">优惠金额  <para>优惠金额，单位：分。</para><para>特殊规则：取值范围1≤value≤10000000</para><para>示例值：5</para></param>
                /// <param name="transaction_minimum">消费门槛  <para>消费门槛，单位：分。</para><para>特殊规则：取值范围1≤value≤10000000</para><para>示例值：100</para></param>
                public Fixed_Normal_Coupon(int discount_amount, int transaction_minimum)
                {
                    this.discount_amount = discount_amount;
                    this.transaction_minimum = transaction_minimum;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Fixed_Normal_Coupon()
                {
                }

                /// <summary>
                /// 优惠金额 
                /// <para>优惠金额，单位：分。</para>
                /// <para>特殊规则：取值范围 1 ≤ value ≤ 10000000</para>
                /// <para>示例值：5 </para>
                /// </summary>
                public int discount_amount { get; set; }

                /// <summary>
                /// 消费门槛 
                /// <para>消费门槛，单位：分。</para>
                /// <para>特殊规则：取值范围 1 ≤ value ≤ 10000000</para>
                /// <para>示例值：100 </para>
                /// </summary>
                public int transaction_minimum { get; set; }

            }

            public class Discount_Coupon
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="discount_percent">折扣比例  <para>折扣百分比，例如：86为八六折。</para><para>示例值：86</para></param>
                /// <param name="transaction_minimum">消费门槛  <para>消费门槛，单位：分。</para><para>特殊规则：取值范围1≤value≤10000000</para><para>示例值：100</para></param>
                public Discount_Coupon(int discount_percent, int transaction_minimum)
                {
                    this.discount_percent = discount_percent;
                    this.transaction_minimum = transaction_minimum;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Discount_Coupon()
                {
                }

                /// <summary>
                /// 折扣比例 
                /// <para>折扣百分比，例如：86为八六折。 </para>
                /// <para>示例值：86 </para>
                /// </summary>
                public int discount_percent { get; set; }

                /// <summary>
                /// 消费门槛 
                /// <para>消费门槛，单位：分。 </para>
                /// <para>特殊规则：取值范围 1 ≤ value ≤ 10000000</para>
                /// <para>示例值：100 </para>
                /// </summary>
                public int transaction_minimum { get; set; }

            }

            public class Exchange_Coupon
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="exchange_price">单品换购价  <para>单品换购价，单位：分。</para><para>特殊规则：取值范围0≤value≤10000000</para><para>示例值：100</para></param>
                /// <param name="transaction_minimum">消费门槛  <para>消费门槛，单位：分。</para><para>特殊规则：取值范围0≤value≤10000000</para><para>示例值：100</para></param>
                public Exchange_Coupon(int exchange_price, int transaction_minimum)
                {
                    this.exchange_price = exchange_price;
                    this.transaction_minimum = transaction_minimum;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Exchange_Coupon()
                {
                }

                /// <summary>
                /// 单品换购价 
                /// <para>单品换购价，单位：分。 </para>
                /// <para>特殊规则：取值范围 0 ≤ value ≤ 10000000</para>
                /// <para>示例值：100 </para>
                /// </summary>
                public int exchange_price { get; set; }

                /// <summary>
                /// 消费门槛 
                /// <para>消费门槛，单位：分。 </para>
                /// <para>特殊规则：取值范围 0 ≤ value ≤ 10000000</para>
                /// <para>示例值：100 </para>
                /// </summary>
                public int transaction_minimum { get; set; }

            }

            #endregion
        }

        /// <summary>
        /// 发放规则
        /// </summary>
        public class Stock_Send_Rule
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="max_coupons">批次最大发放个数  <para>批次最大可发放个数限制</para><para>特殊规则：取值范围1≤value≤1000000000</para><para>示例值：100</para></param>
            /// <param name="max_coupons_per_user">用户最大可领个数  <para>用户可领个数，每个用户最多100张券 。</para><para>示例值：5</para></param>
            /// <param name="max_coupons_by_day">单天发放上限个数  <para>单天发放上限个数（stock_type为DISCOUNT或EXCHANGE时可传入此字段控制单天发放上限）。</para><para>特殊规则：取值范围1≤value≤1000000000</para><para>示例值：100</para><para>可为null</para></param>
            /// <param name="natural_person_limit">是否开启自然人限制  <para>不填默认否，枚举值：true：是false：否</para><para>注：自然人防刷即同证件号下的所有账户合并计算的限领次数（限领次数指的是参数字段“用户最大领取个数”填写的值）</para><para>示例值：false</para></param>
            /// <param name="prevent_api_abuse">可疑账号拦截  <para>不填默认否，枚举值：true：是false：否</para><para>如：黑灰产账号</para><para>示例值：false</para></param>
            /// <param name="transferable">是否允许转赠  <para>不填默认否，枚举值：true：是false：否</para><para>该字段暂未开放</para><para>示例值：false</para></param>
            /// <param name="shareable">是否允许分享链接  <para>不填默认否，枚举值：true：是false：否</para><para>该字段暂未开放</para><para>示例值：false</para></param>
            public Stock_Send_Rule(int max_coupons, int max_coupons_per_user, int max_coupons_by_day, bool natural_person_limit = false, bool prevent_api_abuse = false, bool transferable = false, bool shareable = false)
            {
                this.max_coupons = max_coupons;
                this.max_coupons_per_user = max_coupons_per_user;
                this.max_coupons_by_day = max_coupons_by_day;
                this.natural_person_limit = natural_person_limit;
                this.prevent_api_abuse = prevent_api_abuse;
                this.transferable = transferable;
                this.shareable = shareable;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Stock_Send_Rule()
            {
            }

            /// <summary>
            /// 批次最大发放个数 
            /// <para>批次最大可发放个数限制 </para>
            /// <para>特殊规则：取值范围 1 ≤ value ≤ 1000000000</para>
            /// <para>示例值：100 </para>
            /// </summary>
            public int max_coupons { get; set; }

            /// <summary>
            /// 用户最大可领个数 
            /// <para>用户可领个数，每个用户最多100张券 。 </para>
            /// <para>示例值：5 </para>
            /// </summary>
            public int max_coupons_per_user { get; set; }

            /// <summary>
            /// 单天发放上限个数 
            /// <para>单天发放上限个数（stock_type为DISCOUNT或EXCHANGE时可传入此字段控制单天发放上限）。 </para>
            /// <para>特殊规则：取值范围 1 ≤ value ≤ 1000000000</para>
            /// <para>示例值：100 </para>
            /// <para>可为null</para>
            /// </summary>
            public int max_coupons_by_day { get; set; }

            /// <summary>
            /// 是否开启自然人限制 
            /// <para>不填默认否，枚举值： true：是 false：否 </para>
            /// <para>注：自然人防刷即同证件号下的所有账户合并计算的限领次数（限领次数指的是参数字段“用户最大领取个数”填写的值） </para>
            /// <para>示例值：false </para>
            /// <para>可为null</para>
            /// </summary>
            public bool natural_person_limit { get; set; }

            /// <summary>
            /// 可疑账号拦截 
            /// <para>不填默认否，枚举值： true：是 false：否 </para>
            /// <para>如：黑灰产账号 </para>
            /// <para>示例值：false </para>
            /// <para>可为null</para>
            /// </summary>
            public bool prevent_api_abuse { get; set; }

            /// <summary>
            /// 是否允许转赠 
            /// <para>不填默认否，枚举值： true：是 false：否 </para>
            /// <para>该字段暂未开放</para>
            /// <para>示例值：false </para>
            /// <para>可为null</para>
            /// </summary>
            public bool transferable { get; set; }

            /// <summary>
            /// 是否允许分享链接 
            /// <para>不填默认否，枚举值： true：是 false：否 </para>
            /// <para>该字段暂未开放</para>
            /// <para>示例值：false </para>
            /// <para>可为null</para>
            /// </summary>
            public bool shareable { get; set; }

        }

        public class Custom_Entrance
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="mini_programs_info">小程序入口 <para>需要小程序APPID、path、入口文案、引导文案。如果需要跳转小程序，APPID、path、入口文案为必填，引导文案非必填。appid要与归属商户号有M-AorM-m-suba关系。</para><para>注：请查看绑定关系说明文档</para><para>可为null</para></param>
            /// <param name="appid">商户公众号appid  <para>可配置商户公众号，从券详情可跳转至公众号，用户自定义字段。</para><para>校验规则：传入的APPID得是与调用方商户号（即请求头里面的商户号）有绑定关系的APPID或传入的APPID得是归属商户号有绑定关系的APPID</para><para>示例值：wx324345hgfhfghfg</para><para>可为null</para></param>
            /// <param name="hall_id">营销馆id <para>填写微信支付营销馆的馆id，用户自定义字段。营销馆需在商户平台创建。</para><para>示例值：233455656</para><para>可为null</para></param>
            /// <param name="store_id">可用门店id  <para>填写代金券可用门店id，用户自定义字段。</para><para>示例值：233554655</para><para>可为null</para></param>
            /// <param name="code_display_mode">code展示模式 <para>枚举值：NOT_SHOW：不展示codeBARCODE：一维码QRCODE：二维码</para><para>示例值：BARCODE</para><para>可为null</para></param>
            public Custom_Entrance(Mini_Programs_Info mini_programs_info, string appid, string hall_id, string store_id, string code_display_mode)
            {
                this.mini_programs_info = mini_programs_info;
                this.appid = appid;
                this.hall_id = hall_id;
                this.store_id = store_id;
                this.code_display_mode = code_display_mode;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Custom_Entrance()
            {
            }

            /// <summary>
            /// 小程序入口
            /// <para>需要小程序APPID、path、入口文案、引导文案。如果需要跳转小程序，APPID、path、入口文案为必填，引导文案非必填。 appid要与归属商户号有M-A or M-m-suba关系。</para>
            /// <para>注：请查看绑定关系说明文档 </para>
            /// <para>可为null</para>
            /// </summary>
            public Mini_Programs_Info mini_programs_info { get; set; }

            /// <summary>
            /// 商户公众号appid 
            /// <para>可配置商户公众号，从券详情可跳转至公众号，用户自定义字段。 </para>
            /// <para>校验规则：传入的APPID得是与调用方商户号（即请求头里面的商户号）有绑定关系的APPID 或 传入的APPID得是归属商户号有绑定关系的APPID </para>
            /// <para>示例值：wx324345hgfhfghfg </para>
            /// <para>可为null</para>
            /// </summary>
            public string appid { get; set; }

            /// <summary>
            /// 营销馆id
            /// <para>填写微信支付营销馆的馆id，用户自定义字段。 营销馆需在商户平台 创建。 </para>
            /// <para>示例值：233455656 </para>
            /// <para>可为null</para>
            /// </summary>
            public string hall_id { get; set; }

            /// <summary>
            /// 可用门店id 
            /// <para>填写代金券可用门店id，用户自定义字段。 </para>
            /// <para>示例值：233554655 </para>
            /// <para>可为null</para>
            /// </summary>
            public string store_id { get; set; }

            /// <summary>
            /// code展示模式
            /// <para>枚举值：NOT_SHOW：不展示codeBARCODE：一维码QRCODE：二维码 </para>
            /// <para>示例值：BARCODE </para>
            /// <para>可为null</para>
            /// </summary>
            public string code_display_mode { get; set; }

            #region 子数据类型
            public class Mini_Programs_Info
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="mini_programs_appid">商家小程序appid  <para>商家小程序appid要与归属商户号有M-AorM-m-suba关系。</para><para>校验规则：传入的APPID得是与调用方商户号（即请求头里面的商户号）有绑定关系的APPID或传入的APPID得是归属商户号有绑定关系的APPID</para><para>示例值：wx234545656765876</para></param>
                /// <param name="mini_programs_path">商家小程序path  <para>商家小程序path</para><para>示例值：/path/index/index</para></param>
                /// <param name="entrance_words">入口文案  <para>入口文案，字数上限为5个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：欢迎选购</para></param>
                /// <param name="guiding_words">引导文案  <para>小程序入口引导文案，用户自定义字段。字数上限为6个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：获取更多优惠</para><para>可为null</para></param>
                public Mini_Programs_Info(string mini_programs_appid, string mini_programs_path, string entrance_words, string guiding_words)
                {
                    this.mini_programs_appid = mini_programs_appid;
                    this.mini_programs_path = mini_programs_path;
                    this.entrance_words = entrance_words;
                    this.guiding_words = guiding_words;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Mini_Programs_Info()
                {
                }

                /// <summary>
                /// 商家小程序appid 
                /// <para>商家小程序appid要与归属商户号有M-A or M-m-suba关系。</para>
                /// <para>校验规则：传入的APPID得是与调用方商户号（即请求头里面的商户号）有绑定关系的APPID 或 传入的APPID得是归属商户号有绑定关系的APPID </para>
                /// <para>示例值：wx234545656765876 </para>
                /// </summary>
                public string mini_programs_appid { get; set; }

                /// <summary>
                /// 商家小程序path 
                /// <para>商家小程序path </para>
                /// <para>示例值：/path/index/index </para>
                /// </summary>
                public string mini_programs_path { get; set; }

                /// <summary>
                /// 入口文案 
                /// <para>入口文案，字数上限为5个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
                /// <para>示例值：欢迎选购 </para>
                /// </summary>
                public string entrance_words { get; set; }

                /// <summary>
                /// 引导文案 
                /// <para>小程序入口引导文案，用户自定义字段。字数上限为6个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
                /// <para>示例值：获取更多优惠 </para>
                /// <para>可为null</para>
                /// </summary>
                public string guiding_words { get; set; }

            }

            #endregion
        }

        public class Display_Pattern_Info
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="description">使用须知  <para>用于说明详细的活动规则，会展示在代金券详情页。</para><para>示例值：xxx门店可用</para><para>可为null</para></param>
            /// <param name="merchant_logo_url">商户logo  <para>商户logo的URL地址，仅支持通过《图片上传API》接口获取的图片URL地址。1、商户logo大小需为120像素*120像素。2、支持JPG/JPEG/PNG格式，且图片小于1M。</para><para>示例值：https://qpic.cn/xxx</para><para>可为null</para></param>
            /// <param name="merchant_name">商户名称  <para>商户名称，字数上限为16个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：微信支付</para><para>可为null</para></param>
            /// <param name="background_color">背景颜色  <para>券的背景颜色，可设置10种颜色，色值请参考卡券背景颜色图。颜色取值为颜色图中的颜色名称。</para><para>示例值：Color020</para><para>可为null</para></param>
            /// <param name="coupon_image_url">券详情图片  <para>券详情图片，850像素*350像素，且图片大小不超过2M，支持JPG/PNG格式，仅支持通过《图片上传API》接口获取的图片URL地址。</para><para>示例值：https://qpic.cn/xxx</para><para>可为null</para></param>
            public Display_Pattern_Info(string description, string merchant_logo_url, string merchant_name, string background_color, string coupon_image_url)
            {
                this.description = description;
                this.merchant_logo_url = merchant_logo_url;
                this.merchant_name = merchant_name;
                this.background_color = background_color;
                this.coupon_image_url = coupon_image_url;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Display_Pattern_Info()
            {
            }

            /// <summary>
            /// 使用须知 
            /// <para>用于说明详细的活动规则，会展示在代金券详情页。 </para>
            /// <para>示例值：xxx门店可用 </para>
            /// <para>可为null</para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 商户logo 
            /// <para>商户logo的URL地址，仅支持通过《图片上传API》接口获取的图片URL地址。 1、商户logo大小需为120像素*120像素。 2、支持JPG/JPEG/PNG格式，且图片小于1M。</para>
            /// <para>示例值：https://qpic.cn/xxx</para>
            /// <para>可为null</para>
            /// </summary>
            public string merchant_logo_url { get; set; }

            /// <summary>
            /// 商户名称 
            /// <para>商户名称，字数上限为16个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
            /// <para>示例值：微信支付 </para>
            /// <para>可为null</para>
            /// </summary>
            public string merchant_name { get; set; }

            /// <summary>
            /// 背景颜色 
            /// <para>券的背景颜色，可设置10种颜色，色值请参考卡券背景颜色图。颜色取值为颜色图中的颜色名称。</para>
            /// <para>示例值：Color020 </para>
            /// <para>可为null</para>
            /// </summary>
            public string background_color { get; set; }

            /// <summary>
            /// 券详情图片 
            /// <para>券详情图片，850像素*350像素，且图片大小不超过2M，支持JPG/PNG格式，仅支持通过《图片上传API》接口获取的图片URL地址。</para>
            /// <para>示例值：https://qpic.cn/xxx </para>
            /// <para>可为null</para>
            /// </summary>
            public string coupon_image_url { get; set; }

        }

        public class Notify_Config
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="notify_appid">事件通知appid  <para>用于回调通知时，计算返回操作用户的openid（诸如领券用户），支持小程序or公众号的APPID；如该字段不填写，则回调通知中涉及到用户身份信息的openid与unionid都将为空。</para><para>示例值：wx23232232323</para><para>可为null</para></param>
            public Notify_Config(string notify_appid)
            {
                this.notify_appid = notify_appid;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Notify_Config()
            {
            }

            /// <summary>
            /// 事件通知appid 
            /// <para>用于回调通知时，计算返回操作用户的openid（诸如领券用户），支持小程序or公众号的APPID；如该字段不填写，则回调通知中涉及到用户身份信息的openid与unionid都将为空。 </para>
            /// <para>示例值：wx23232232323 </para>
            /// <para>可为null</para>
            /// </summary>
            public string notify_appid { get; set; }

        }

        #endregion
    }
}
