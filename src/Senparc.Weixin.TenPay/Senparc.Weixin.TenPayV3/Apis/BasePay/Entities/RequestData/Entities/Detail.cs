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
  
    文件名：Detail.cs
    文件功能描述：下单请求优惠功能信息
    
    
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
    /// 优惠功能
    /// </summary>
    public class Detail
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="cost_price">订单原价，可为null</param>
        /// <param name="invoice_id">商家小票ID，可为null</param>
        /// <param name="goods_detail">单品列表 条目个数限制：[1，6000]，可为null</param>
        public Detail(int cost_price, string invoice_id, Goods_Detail[] goods_detail)
        {
            this.invoice_id = invoice_id;
            this.goods_detail = goods_detail;
            this.cost_price = cost_price;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Detail()
        {
        }

        /// <summary>
        /// 商家小票ID
        /// 示例值：微信123
        /// </summary>
        public string invoice_id { get; set; }

        /// <summary>
        /// 单品列表
        /// 条目个数限制：[1，6000]
        /// </summary>
        public Goods_Detail[] goods_detail { get; set; }

        /// <summary>
        /// 订单原价
        /// 1、商户侧一张小票订单可能被分多次支付，订单原价用于记录整张小票的交易金额。
        /// 2、当订单原价与支付金额不相等，则不享受优惠。
        /// 3、该字段主要用于防止同一张小票分多次支付，以享受多次优惠的情况，正常支付订单不必上传此参数。
        /// 示例值：608800
        /// </summary>
        public int cost_price { get; set; }
    }
}
