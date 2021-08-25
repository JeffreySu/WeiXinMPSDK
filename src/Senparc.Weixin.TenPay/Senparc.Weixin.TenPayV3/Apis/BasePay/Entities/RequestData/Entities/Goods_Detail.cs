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
  
    文件名：Goods_Detail.cs
    文件功能描述：下单请求单品信息
    
    
    创建标识：Senparc - 20210825
    
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay.Entities.RequestData.Entities
{
    /// <summary>
    /// 单品信息
    /// </summary>
    public class Goods_Detail
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="merchant_goods_id">商户侧商品编码</param>
        /// <param name="wechatpay_goods_id">微信侧商品编码，可为null</param>
        /// <param name="goods_name">商品名称，可为null</param>
        /// <param name="quantity">用户购买的数量</param>
        /// <param name="unit_price">商品单价，单位为分</param>
        public Goods_Detail(string merchant_goods_id, string wechatpay_goods_id, string goods_name, int quantity, int unit_price)
        {
            this.goods_name = goods_name;
            this.wechatpay_goods_id = wechatpay_goods_id;
            this.quantity = quantity;
            this.merchant_goods_id = merchant_goods_id;
            this.unit_price = unit_price;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Goods_Detail()
        {
        }

        /// <summary>
        /// 商品名称
        /// 商品的实际名称
        /// 示例值：iPhoneX 256G
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 微信侧商品编码
        /// 微信支付定义的统一商品编号（没有可不传）
        /// 示例值：1001
        /// </summary>
        public string wechatpay_goods_id { get; set; }

        /// <summary>
        /// 商品数量
        /// 用户购买的数量
        /// 示例值：1
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// 商户侧商品编码
        /// 由半角的大小写字母、数字、中划线、下划线中的一种或几种组成。
        /// 示例值：1246464644
        /// </summary>
        public string merchant_goods_id { get; set; }

        /// <summary>
        /// 商品单价
        /// 商品单价，单位为分
        /// 示例值：828800 (8288元)
        /// </summary>
        public int unit_price { get; set; }
    }
}
