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
  
    文件名：StockNotifyJson.cs
    文件功能描述：微信支付V3核销事件回调通知Json
    
    
    创建标识：Senparc - 20210902
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay.Entities
{
    /// <summary>
    /// 微信支付V3核销事件回调通知Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_15.shtml </para>
    /// </summary>
    public class StockNotifyJson : ReturnJsonBase
    {
        /// <summary>
        /// 创建批次的商户号
        /// </summary>
        public string stock_creator_mchid { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 代金券id
        /// </summary>
        public string coupon_id { get; set; }

        /// <summary>
        /// 单品优惠特定信息
        /// </summary>
        public Singleitem_Discount_Off singleitem_discount_off { get; set; }

        /// <summary>
        /// 减至优惠特定信息
        /// </summary>
        public Discount_To discount_to { get; set; }

        /// <summary>
        /// 代金券名称
        /// </summary>
        public string coupon_name { get; set; }

        /// <summary>
        /// 代金券状态：
        /// <para>枚举值: SENDED：可用 USED：已实扣 EXPIRED：已过期</para>
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 代金券描述说明字段
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 领券时间
        /// <para>遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE</para>
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 券类型
        /// <para>枚举值: NORMAL：满减券 CUT_TO：减至券</para>
        /// </summary>
        public string coupon_type { get; set; }

        /// <summary>
        /// 是否无资金流
        /// <para>枚举值: true：是 false：否</para>
        /// </summary>
        public bool no_cash { get; set; }

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
        /// 是否单品优惠
        /// 枚举值: true：是 false：否
        /// </summary>
        public bool singleitem { get; set; }

        /// <summary>
        /// 普通满减券信息
        /// </summary>
        public Normal_Coupon_Information normal_coupon_information { get; set; }

        /// <summary>
        /// 实扣代金券信息
        /// </summary>
        public Consume_Information consume_information { get; set; }

        #region 回调数据类型

        /// <summary>
        /// 单品优惠特定信息
        /// </summary>
        public class Singleitem_Discount_Off
        {
            /// <summary>
            /// 单品最高优惠价格
            /// </summary>
            public long single_price_max { get; set; }
        }

        /// <summary>
        /// 减至优惠特定信息
        /// </summary>
        public class Discount_To
        {
            /// <summary>
            /// 减至后优惠单价
            /// </summary>
            public long cut_to_price { get; set; }

            /// <summary>
            /// 可享受优惠的最高价格
            /// </summary>
            public long max_price { get; set; }
        }

        /// <summary>
        ///	普通满减券面额、门槛信息
        /// </summary>
        public class Normal_Coupon_Information
        {
            /// <summary>
            /// 面额
            /// </summary>
            public long coupon_amount { get; set; }

            /// <summary>
            /// 门槛
            /// </summary>
            public long transaction_minimum { get; set; }
        }

        /// <summary>
        /// 实扣代金券信息
        /// </summary>
        public class Consume_Information
        {
            /// <summary>
            /// 代金券核销时间
            /// 遵循rfc3339标准格式，
            /// </summary>
            public DateTime consume_time { get; set; }

            /// <summary>
            /// 核销代金券的商户号
            /// </summary>
            public string consume_mchid { get; set; }

            /// <summary>
            /// 核销订单号
            /// </summary>
            public string transaction_id { get; set; }

            /// <summary>
            /// 单品信息
            /// </summary>
            public Goods_Detail[] goods_detail { get; set; }

            #region 回调数据类型

            /// <summary>
            /// 单品信息
            /// </summary>
            public class Goods_Detail
            {
                /// <summary>
                /// 单品编码
                /// </summary>
                public string goods_id { get; set; }

                /// <summary>
                /// 单品数量
                /// </summary>
                public int quantity { get; set; }

                /// <summary>
                /// 单品单价
                /// </summary>
                public int price { get; set; }

                /// <summary>
                /// 优惠金额
                /// </summary>
                public int discount_amount { get; set; }
            }

            #endregion
        }

        #endregion

    }
}
