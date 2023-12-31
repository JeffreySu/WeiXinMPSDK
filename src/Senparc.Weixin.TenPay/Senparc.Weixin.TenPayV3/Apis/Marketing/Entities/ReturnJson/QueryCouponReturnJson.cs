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
  
    文件名：QueryCouponReturnJson.cs
    文件功能描述：查询代金券详情返回Json
    
    
    创建标识：Senparc - 20210902
    
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
    /// 查询代金券详情返回Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_6.shtml </para>
    /// </summary>
    public class QueryCouponReturnJson : ReturnJsonBase
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
        public Cut_To_Message cut_to_message { get; set; }

        /// <summary>
        /// 代金券名称
        /// </summary>
        public string coupon_name { get; set; }

        /// <summary>
        /// 代金券状态
        /// <para>枚举值：SENDED：可用 USED：已实扣 EXPIRED：已过期</para>
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
        /// <para>枚举值：NORMAL：满减券 CUT_TO：减至券</para>
        /// </summary>
        public string coupon_type { get; set; }

        /// <summary>
        /// 是否无资金流
        /// <para>枚举值：true：是 false：否</para>
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
        /// <para>枚举值：true：是 false：否</para>
        /// </summary>
        public bool singleitem { get; set; }

        /// <summary>
        /// 普通满减券面额、门槛信息
        /// </summary>
        public Normal_Coupon_Information normal_coupon_information { get; set; }

        /// <summary>
        /// 已实扣代金券核销信息
        /// </summary>
        public Consume_Information consume_information { get; set; }

        #region 返回数据类型

        /// <summary>
        /// 单品优惠特定信息
        /// </summary>
        public class Cut_To_Message
        {
            public ulong single_price_max { get; set; }
            public ulong cut_to_price { get; set; }
        }

        /// <summary>
        /// 普通满减券面额、门槛信息
        /// </summary>
        public class Normal_Coupon_Information
        {
            /// <summary>
            /// 面额
            /// </summary>
            public ulong coupon_amount { get; set; }

            /// <summary>
            /// 门槛
            /// </summary>
            public ulong transaction_minimum { get; set; }
        }

        /// <summary>
        /// 已实扣代金券核销信息
        /// </summary>
        public class Consume_Information
        {
            /// <summary>
            /// 核销时间
            /// <para>遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE</para>
            /// </summary>
            public DateTime consume_time { get; set; }

            /// <summary>
            /// 核销商户号
            /// </summary>
            public string consume_mchid { get; set; }

            /// <summary>
            /// 支付单号
            /// </summary>
            public string transaction_id { get; set; }

            /// <summary>
            /// 单品信息
            /// </summary>
            public Goods_Detail[] goods_detail { get; set; }

            #region 返回数据类型

            /// <summary>
            /// 单品信息
            /// </summary>
            public class Goods_Detail
            {
                /// <summary>
                /// 商品编码
                /// </summary>
                public string goods_id { get; set; }

                /// <summary>
                /// 商品数量
                /// </summary>
                public uint quantity { get; set; }

                /// <summary>
                /// 商品价格
                /// </summary>
                public ulong price { get; set; }

                /// <summary>
                /// 优惠金额
                /// </summary>
                public ulong discount_amount { get; set; }
            }

            #endregion
        }

        #endregion
    }
}
