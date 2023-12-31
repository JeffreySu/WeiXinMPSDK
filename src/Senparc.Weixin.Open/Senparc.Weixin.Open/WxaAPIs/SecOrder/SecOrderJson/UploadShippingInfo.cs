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
    
    文件名：UploadShippingInfo.cs
    文件功能描述：发货信息录入接口
    
    
    创建标识：Yaofeng - 20231026

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.SecOrder
{
    /// <summary>
    /// 发货信息录入接口
    /// </summary>
    public class UploadShippingInfo
    {
        /// <summary>
        /// 【必填】订单，需要上传物流信息的订单
        /// </summary>
        public OrderKey order_key { get; set; }

        /// <summary>
        /// 【必填】物流模式，发货方式枚举值：1、实体物流配送采用快递公司进行实体物流配送形式 2、同城配送 3、虚拟商品，虚拟商品，例如话费充值，点卡等，无实体配送形式 4、用户自提
        /// </summary>
        public int logistics_type { get; set; }

        /// <summary>
        /// 【必填】发货模式，发货模式枚举值：1、UNIFIED_DELIVERY（统一发货）2、SPLIT_DELIVERY（分拆发货） 示例值: UNIFIED_DELIVERY
        /// </summary>
        public int delivery_mode { get; set; }

        /// <summary>
        /// 【非必填】分拆发货模式时必填，用于标识分拆发货模式下是否已全部发货完成，只有全部发货完成的情况下才会向用户推送发货完成通知。示例值: true/false
        /// </summary>
        public bool is_all_delivered { get; set; }

        /// <summary>
        /// 【必填】物流信息列表，发货物流单列表，支持统一发货（单个物流单）和分拆发货（多个物流单）两种模式，多重性: [1, 10]
        /// </summary>
        public List<Shipping> shipping_list { get; set; }

        /// <summary>
        /// 【必填】上传时间，用于标识请求的先后顺序 示例值: `2022-12-15T13:29:35.120+08:00`
        /// </summary>
        public string upload_time { get; set; }

        /// <summary>
        /// 【必填】支付者，支付者信息
        /// </summary>
        public Payer payer { get; set; }
    }
}