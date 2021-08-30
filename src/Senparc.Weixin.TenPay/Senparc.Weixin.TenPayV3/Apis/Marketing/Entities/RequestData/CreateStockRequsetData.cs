#region Apache License Version 2.0
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
  
    文件名：CreateStockRequsetData.cs
    文件功能描述：创建代金券批次请求数据
    
    
    创建标识：Senparc - 20210832
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    public class CreateStockRequsetData
    {
        /// <summary>
        /// 批次名称
        /// 校验规则：
        /// 1、批次名称最多9个中文汉字
        /// 2、批次名称最多20个字母
        /// 3、批次名称中不能包含不当内容和特殊字符 _, ; |
        /// 示例值：微信支付代金券批次
        /// </summary>
        public string stock_name { get; set; }

        /// <summary>
        /// 批次备注
        ///  仅制券商户可见，用于自定义信息。
        /// 校验规则：批次备注最多60个UTF8字符数
        /// 示例值：零售批次
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 批次归属商户号
        /// 本字段暂未开放生效，但入参时请设置为当前创建代金券商户号即不会报错，暂时不支持入参其他的商户号
        /// 示例值：98568865
        /// </summary>
        public string belong_merchant { get; set; }

        /// <summary>
        /// 可用时间-开始时间
        /// 批次开始时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE
        /// </summary>
        public string available_begin_time { get; set; }

        /// <summary>
        /// 可用时间-结束时间	
        /// 批次结束时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE
        /// </summary>
        public string available_end_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Stock_Use_Rule stock_use_rule { get; set; }
        public Pattern_Info pattern_info { get; set; }
        public Coupon_Use_Rule coupon_use_rule { get; set; }
        public bool no_cash { get; set; }
        public string stock_type { get; set; }
        public string out_request_no { get; set; }


        #region 请求数据类

        /// <summary>
        /// 发放规则
        /// </summary>
        public class Stock_Use_Rule
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="max_coupons">发放总上限 最大发券数</param>
            /// <param name="max_amount">最大发券预算</param>
            /// <param name="max_amount_by_day">单天预算发放上限，可为null</param>
            /// <param name="max_coupons_per_user">单个用户可领个数</param>
            /// <param name="natural_person_limit">是否开启自然人限制</param>
            /// <param name="prevent_api_abuse">是否开启防刷拦截</param>
            public Stock_Use_Rule(int max_coupons, int max_amount, int max_amount_by_day, int max_coupons_per_user, bool natural_person_limit, bool prevent_api_abuse)
            {
                this.max_coupons = max_coupons;
                this.max_amount = max_amount;
                this.max_amount_by_day = max_amount_by_day;
                this.max_coupons_per_user = max_coupons_per_user;
                this.natural_person_limit = natural_person_limit;
                this.prevent_api_abuse = prevent_api_abuse;
            }

            /// <summary>
            /// 发放总上限 最大发券数
            /// 校验规则：
            /// 1、发放总个数最少5个
            /// 2、发放总个数最多1000万个
            /// 示例值：100
            /// </summary>
            public int max_coupons { get; set; }

            /// <summary>
            /// 总预算
            /// 最大发券预算，当营销经费no_cash选择预充值false时，激活批次时会从制券商户的余额中扣除预算，请保证账户金额充足，单位：分
            /// max_amount需要等于coupon_amount（面额） * max_coupons（发放总上限）
            /// 校验规则：批次总预算最多1亿元
            /// 示例值：5000
            /// </summary>
            public int max_amount { get; set; }

            /// <summary>
            /// 单天预算发放上限
            /// 最大发券预算，当营销经费no_cash选择预充值false时，激活批次时会从制券商户的余额中扣除预算，请保证账户金额充足，单位：分
            /// max_amount需要等于coupon_amount（面额） * max_coupons（发放总上限）
            /// 校验规则：批次总预算最多1亿元
            /// 示例值：5000
            /// </summary>
            public int max_amount_by_day { get; set; }

            /// <summary>
            /// 单个用户可领个数
            /// 活动期间每个用户可领个数，当开启了自然人限领时，多个微信号同属于一个身份证时，视为同一用户。
            /// 校验规则：
            /// 1、不能大于发放总个数
            /// 2、最少为1个，最多为60个
            /// 示例值：3
            /// </summary>
            public int max_coupons_per_user { get; set; }

            /// <summary>
            /// 是否开启自然人限制
            /// 当开启了自然人限领时，多个微信号同属于一个身份证时，视为同一用户，枚举值
            /// true：是
            /// false：否
            /// 示例值：false
            /// </summary>
            public bool natural_person_limit { get; set; }

            /// <summary>
            /// 是否开启防刷拦截
            /// 若开启防刷拦截，当用户命中恶意、小号、机器、羊毛党、黑产等风险行为时，无法成功发放代金券。
            /// 枚举值
            /// true：是
            /// false：否
            /// 示例值：false
            /// </summary>
            public bool prevent_api_abuse { get; set; }
        }

        #endregion


        public class Pattern_Info
        {
            public string description { get; set; }
            public string merchant_logo { get; set; }
            public string merchant_name { get; set; }
            public string background_color { get; set; }
            public string coupon_image { get; set; }
        }

        public class Coupon_Use_Rule
        {
            public Fixed_Normal_Coupon fixed_normal_coupon { get; set; }
            public string[] goods_tag { get; set; }
            public string[] trade_type { get; set; }
            public bool combine_use { get; set; }
            public string[] available_items { get; set; }
            public string[] available_merchants { get; set; }
        }

        public class Fixed_Normal_Coupon
        {
            public int coupon_amount { get; set; }
            public int transaction_minimum { get; set; }
        }

    }
}
