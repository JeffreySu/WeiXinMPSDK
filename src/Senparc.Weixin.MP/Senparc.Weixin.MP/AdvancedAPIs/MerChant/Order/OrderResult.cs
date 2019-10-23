#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：OrderResult.cs
    文件功能描述：根据订单ID获取订单详情返回结果
    
    
    创建标识：Senparc - 2015828

    修改标识：Senparc - 20160127
    修改描述：v13.5.6 添加receiver_zone属性。感谢@nsoff
----------------------------------------------------------------*/


using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 根据订单ID获取订单详情返回结果
    /// </summary>
    public class GetByIdOrderResult : WxJsonResult
    {
        /// <summary>
        /// 订单详情
        /// </summary>
        public Order order { get; set; }
    }

    public class Order
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// 订单总价格(单位 : 分)
        /// </summary>
        public int order_total_price { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public string order_create_time { get; set; }
        /// <summary>
        /// 订单运费价格(单位 : 分)
        /// </summary>
        public int order_express_price { get; set; }
        /// <summary>
        /// 买家微信OPENID
        /// </summary>
        public string buyer_openid { get; set; }
        /// <summary>
        /// 买家微信昵称
        /// </summary>
        public string buyer_nick { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string receiver_name { get; set; }
        /// <summary>
        /// 收货地址省份
        /// </summary>
        public string receiver_province { get; set; }
        /// <summary>
        /// 收货地址城市
        /// </summary>
        public string receiver_city { get; set; }
        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string receiver_address { get; set; }
        /// <summary>
        /// 收货人移动电话
        /// </summary>
        public string receiver_mobile { get; set; }
        /// <summary>
        /// 收货人固定电话
        /// </summary>
        public string receiver_phone { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 商品价格(单位 : 分)
        /// </summary>
        public int product_price { get; set; }
        /// <summary>
        /// 商品SKU
        /// </summary>
        public string product_sku { get; set; }
        /// <summary>
        /// 商品个数
        /// </summary>
        public int product_count { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string product_img { get; set; }
        /// <summary>
        /// 运单ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string delivery_company { get; set; }
        /// <summary>
        /// 交易ID
        /// </summary>
        public string trans_id { get; set; }

        /// <summary>
        /// 收货人区
        /// </summary>
        public string receiver_zone { get; set; }
    }

    /// <summary>
    /// 根据订单状态/创建时间获取订单详情返回结果
    /// </summary>
    public class GetByFilterResult : WxJsonResult
    {
        public List<Order> order_list { get; set; }
    }
}

