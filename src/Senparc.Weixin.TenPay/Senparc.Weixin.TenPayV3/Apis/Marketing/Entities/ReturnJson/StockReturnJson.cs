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
  
    文件名：StockReturnJson.cs
    文件功能描述：代金券批次数据
    
    
    创建标识：Senparc - 20210823
    
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
    /// 代金券批次数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_5.shtml </para>
    /// </summary>
    public class StockReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 批次号
        /// <para>微信为每个代金券批次分配的唯一id</para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 创建批次的商户号
        /// </summary>
        public string stock_creator_mchid { get; set; }

        /// <summary>
        /// 批次名称	
        /// </summary>
        public string stock_name { get; set; }

        /// <summary>
        /// 批次状态
        /// <para>枚举值：unactivated：未激活 audit：审核中 running：运行中 stoped：已停止 paused：暂停发放</para>
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 创建时间
        /// <para>遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE</para>
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 批次描述信息
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 满减券批次使用规则
        /// </summary>
        public Stock_Use_Rule stock_use_rule { get; set; }

        /// <summary>
        /// 可用开始时间
        /// <para>遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE</para>
        /// </summary>
        public DateTime available_begin_time { get; set; }

        /// <summary>
        /// 可用结束时间
        /// <para>遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE</para>
        /// </summary>
        public DateTime available_end_time { get; set; }

        /// <summary>
        /// 已发券数量
        /// </summary>
        public uint distributed_coupons { get; set; }

        /// <summary>
        /// 是否无资金流。
        /// <para>ture：是 false：否</para>
        /// </summary>
        public bool no_cash { get; set; }

        /// <summary>
        /// 激活批次的时间	
        /// <para>遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE</para>
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// 终止批次的时间	
        /// <para>遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE</para>
        /// </summary>
        public DateTime stop_time { get; set; }

        /// <summary>
        /// 单品优惠特定信息
        /// </summary>
        public Cut_To_Message cut_to_message { get; set; }

        /// <summary>
        /// 是否单品优惠
        /// <para>枚举值： true：是 false：否</para>
        /// </summary>
        public bool singleitem { get; set; }

        /// <summary>
        /// 批次类型
        /// <para>枚举值： NORMAL：代金券批次 DISCOUNT_CUT：立减与折扣 OTHER：其他</para>
        /// </summary>
        public string stock_type { get; set; }

        #region 请求数据类型

        /// <summary>
        /// 满减券批次使用规则
        /// </summary>
        public class Stock_Use_Rule
        {
            /// <summary>
            /// 发放总上限
            /// </summary>
            public ulong max_coupons { get; set; }

            /// <summary>
            /// 总预算
            /// </summary>
            public ulong max_amount { get; set; }

            /// <summary>
            /// 单天发放上限金额
            /// </summary>
            public ulong max_amount_by_day { get; set; }

            /// <summary>
            /// 固定面额批次特定信息
            /// </summary>
            public Fixed_Normal_Coupon fixed_normal_coupon { get; set; }

            /// <summary>
            /// 单个用户可领个数
            /// </summary>
            public uint max_coupons_per_user { get; set; }

            /// <summary>
            /// 券类型
            /// <para>枚举值：NORMAL：满减券 CUT_TO：减至券</para>
            /// </summary>
            public string coupon_type { get; set; }

            /// <summary>
            /// 订单优惠标记 (该字段暂未开放返回)
            /// </summary>
            public string[] goods_tag { get; set; }

            /// <summary>
            /// 支付方式
            /// <para>默认不限制</para>
            /// <para>枚举值： MICROAPP：小程序支付 APPPAY：APP支付 PPAY：免密支付 CARD：付款码支付 FACE：人脸支付 OTHER：（公众号、扫码等）</para>
            /// </summary>
            public string[] trade_type { get; set; }

            /// <summary>
            /// 是否可叠加其他优惠
            /// <para>枚举值： true：是 false：否 示例值：true</para>
            /// </summary>
            public bool combine_use { get; set; }

            #region 请求数据类型

            /// <summary>
            /// 固定面额批次特定信息
            /// </summary>
            public class Fixed_Normal_Coupon
            {
                /// <summary>
                /// 面额
                /// </summary>
                public ulong coupon_amount { get; set; }

                /// <summary>
                /// 使用券金额门槛
                /// </summary>
                public ulong transaction_minimum { get; set; }
            }

            #endregion
        }

        /// <summary>
        /// 单品优惠特定信息
        /// </summary>
        public class Cut_To_Message
        {
            /// <summary>
            /// 可用优惠的商品最高单价
            /// </summary>
            public ulong single_price_max { get; set; }

            /// <summary>
            /// 减至后的优惠单价
            /// </summary>
            public ulong cut_to_price { get; set; }
        }

        #endregion
    }
}
